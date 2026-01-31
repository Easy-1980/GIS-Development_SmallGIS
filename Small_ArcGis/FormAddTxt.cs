using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using System.IO;
using ESRI.ArcGIS.Geometry;

namespace Small_ArcGis
{
    /// <summary>
    /// 伙伴控件属性
    /// 打开按钮，获取TXT路径及点信息
    /// 保存按钮，获取SHP文件保存路径
    /// 创建按钮，生成SHP图层，并加载到主窗体控件MapControl1中
    /// 取消按钮，关闭窗体
    /// </summary>
    public partial class FormAddTxt : Form
    {
        public FormAddTxt()
        {
            InitializeComponent();
            //const string FormAddTxt = null;
        }

        // 伙伴控件属性：私有字段，公开属性
        private AxMapControl buddyMap;
        public AxMapControl BuddyMap
        {
            get { return buddyMap; }
            set { buddyMap = value; }
        }

        // +
        public event EventHandler LayerCreated;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // 打开按钮，获取TXT路径及点信息
        private void button1_Click(object sender, EventArgs e)
        {
            // 获取TXT路径
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Multiselect = false;
            pOFD.Title = "打开坐标文件";
            pOFD.Filter = "坐标文件(*.txt)|*.txt";
            pOFD.InitialDirectory = Directory.GetCurrentDirectory();
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = pOFD.FileName;
            }
            // 读取TXT内的点信息(转换为SHP图层)并保存
            pList = GetPoints(pOFD.FileName);
        }
        // 点结构，存储点信息：并不是常规意义上的点，需要有名称对应：Name、X、Y
        struct CPoint
        {
            public string Name;
            public double X;
            public double Y;
        }

        List<string> pStr = new List<string>();
        List<CPoint> pList = new List<CPoint>();

        List<CPoint> GetPoints(string DataFullName)
        {
            try
            {
                // 常用的分割符号
                char[] charArray = new char[] { ',', ',', '\t' };
                // 读取文件相应信息
                FileStream fs = new FileStream(DataFullName, FileMode.Open);    // 用FileStream打开文件并读取
                StreamReader sr = new StreamReader(fs, Encoding.Default);    // 用StreamReader编码为可读文本
                string strLine = sr.ReadLine();
                if (strLine != null)
                {
                    // 获取分割后的字符串，charArray作为分割符参数输入到Split中
                    string[] strArray = strLine.Split(charArray);
                    if (strArray.Length > 0)
                    {
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            pStr.Add(strArray[i]);
                        }
                    }
                    while ((strLine = sr.ReadLine()) != null)
                    {
                        // 获取点信息
                        strArray = strLine.Split(charArray);
                        CPoint pCPoint = new CPoint();
                        pCPoint.Name = strArray[0];
                        pCPoint.X = Convert.ToDouble(strArray[1]);
                        pCPoint.Y = Convert.ToDouble(strArray[2]);
                        pList.Add(pCPoint);
                    }
                }
                else
                {
                    return null;
                }
                sr.Close();
                return pList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // 保存按钮,获取SHP文件保存路径
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SHP文件(*.shp)|*.shp";
            if (File.Exists(textBox1.Text))
            {
                saveFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(textBox1.Text);
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog.FileName;
            }
        }

        // 创建按钮，生成SHP图层，并加载到主窗体MapControl1控件中
        private void button3_Click(object sender, EventArgs e)
        {
            if (pList == null)
            {
                MessageBox.Show("文件为空，请重新选择");
            }
            else
            {
                // 生成SHP文件，并将其加载到MapControl1控件中

                // 生成SHP图层shpLayer
                IFeatureLayer shpLayer = CreateSHPLayer(pList, textBox2.Text);

                if (buddyMap.Map == null)
                {
                    IMap newMap = new MapClass();
                    newMap.Name = "Map";
                    buddyMap.Map = newMap;
                }

                buddyMap.Map.AddLayer(shpLayer);
                buddyMap.ActiveView.Refresh();

                // +
                EventHandler handler = LayerCreated;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }


                this.Close();
            }
        }

        private IFeatureLayer CreateSHPLayer(List<CPoint> pList, string p)
        {
            int index = p.LastIndexOf("\\");
            string folder = p.Substring(0, index);
            string shpname = p.Substring(index + 1);

            // 生成工作空间
            IWorkspaceFactory pwsf = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pfws = (IFeatureWorkspace)pwsf.OpenFromFile(folder, 0);
            // 字段集合
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            // 几何字段
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;

            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            // 定义坐标系
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSR = pSRF.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_Beijing1954);
            pGeometryDefEdit.SpatialReference_2 = pSR;

            pFieldEdit.GeometryDef_2 = pGeometryDef;


            // 将几何字段添加到字段集合中
            pFieldsEdit.AddField(pField);

            IFeatureClass pFeatureClass = pfws.CreateFeatureClass(shpname, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            IPoint pPoint = new PointClass();
            for (int i = 0; i < pList.Count; i++)
            {
                pPoint.X = pList[i].X;
                pPoint.Y = pList[i].Y;
                // 创建单个要素
                IFeature pFeature = pFeatureClass.CreateFeature();
                pFeature.Shape = pPoint;
                pFeature.Store();

            }
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = shpname;
            return pFeatureLayer;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
