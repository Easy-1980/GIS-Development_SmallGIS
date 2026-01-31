using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Small_ArcGis
{
    public partial class FormStatistics : Form
    {
        public FormStatistics()
        {
            InitializeComponent();
            layersHashtable = new Hashtable();
        }
        private IMap currentMap;
        private Hashtable layersHashtable;
        private IFeatureLayer currentFeatureLayer = null;
        ///<summary>
        /// 获得当前MapControl控件中的Map对象
        ///</summary>
        public IMap CurrentMap
        {
            set
            {
                currentMap = value;
            }
        }

        private void FormStatistics_Load(object sender, EventArgs e)
        {
            IFeatureLayer featureLayer;
            string layerName;
            int layersCount = 0;
            int allSelectedFeatures = 0;

            layersHashtable.Clear();

            for (int i = 0; i < currentMap.LayerCount; i++)
            {
                // 如果该图层为图层组类型，则分别对所包含的每个图层进行操作
                if (currentMap.get_Layer(i) is GroupLayer)
                {
                    // 使用ICompositeLayer接口进行遍历操作
                    ICompositeLayer compositeLayer = currentMap.get_Layer(i) as ICompositeLayer;
                    for (int j = 0; j < compositeLayer.Count; j++)
                    {
                        // 得到图层的名称
                        layerName = compositeLayer.get_Layer(j).Name;
                        // 得到矢量图层对象的IFeatureLayer接口
                        featureLayer = (IFeatureLayer)compositeLayer.get_Layer(j);
                        // 如果该图层选择集中的要素不为空，则在TreeView控件中添加一个树节点
                        if (((IFeatureSelection)featureLayer).SelectionSet.Count > 0)
                        {
                            comboBoxLayers.Items.Add(layerName);
                            layersHashtable.Add(layerName, featureLayer);
                            layersCount += 1;
                            allSelectedFeatures += ((IFeatureSelection)featureLayer).SelectionSet.Count;
                        }
                    }
                }
                else
                {
                    layerName = currentMap.get_Layer(i).Name;
                    featureLayer = (IFeatureLayer)currentMap.get_Layer(i);
                    // 如果该图层选择集中的要素不为空，则在TreeView控件中添加一个树节点
                    if (((IFeatureSelection)featureLayer).SelectionSet.Count > 0)
                    {
                        comboBoxLayers.Items.Add(layerName);
                        layersHashtable.Add(layerName, featureLayer);
                        layersCount += 1;
                        allSelectedFeatures += ((IFeatureSelection)featureLayer).SelectionSet.Count;
                    }
                }
            }
            labelSelection.Text = "当前地图选择集共有 " + layersCount + " 个图层的 " + allSelectedFeatures + " 个要素被选中。";
            if (comboBoxLayers.Items.Count>0)
            {
                comboBoxLayers.SelectedIndex = 0;
            }

        }

        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxFields.Items.Clear();
            foreach (DictionaryEntry de in layersHashtable)
            {
                if (de.Key.ToString()==comboBoxLayers.SelectedItem.ToString())
                {
                    currentFeatureLayer = de.Value as IFeatureLayer;
                    break;
                }
            }
            IFields iFields;
            iFields = currentFeatureLayer.FeatureClass.Fields;
            IField field;
            for (int i = 0; i < iFields.FieldCount; i++)
            {
                 //根据索引得到字段 
                field = iFields.get_Field(i); 
                //如果字段名称不为"OBJECTID"或 "SHAPE" 
                if (field.Name.ToUpper() != "OBJECTID" && field.Name.ToUpper() != "SHAPE") 
                { 
                    //如果字段类型为可以进行统计的数值类型，则将该字段添加到comboBoxFields中 
                    if (field.Type == esriFieldType.esriFieldTypeInteger
                        || field.Type == esriFieldType.esriFieldTypeDouble
                        || field.Type == esriFieldType.esriFieldTypeSingle ||
                        field.Type == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        comboBoxFields.Items.Add(field.Name); 
                    }
                }
            }
            //显示第一个可以选择的字段 
            if (comboBoxFields.Items.Count > 0)
            {
                comboBoxFields.SelectedIndex = 0;
            }
                
        }

        private void comboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            //定义及新建IDataStatistics接口的对象进行字段统计 
            IDataStatistics dataStatistics = new DataStatisticsClass();
            //获取需要统计的字段 
            dataStatistics.Field = comboBoxFields.SelectedItem.ToString();
            //将当前矢量图层对象进行接口转换以进行选择集操作 
            IFeatureSelection featureSelection = currentFeatureLayer as IFeatureSelection;
            //定义选择集的游标 
            ICursor cursor = null;
            //使用null参数的Search方法获取整个选择集中的要素，得到相应的游标 
            featureSelection.SelectionSet.Search(null, false, out cursor);
            //将该游标赋值给IDataStatistics接口对象的游标 
            dataStatistics.Cursor = cursor;
            //执行统计 
            IStatisticsResults statisticsResults = dataStatistics.Statistics;
            //定义StringBuilder对象进行字符串的操作 
            StringBuilder stringBuilder = new StringBuilder();
            //以下语句依次增加各类统计结果 
            stringBuilder.AppendLine("统计总数： " + statisticsResults.Count.ToString() + "\n");
            stringBuilder.AppendLine("最小值：" + statisticsResults.Minimum.ToString() + "\n");
            stringBuilder.AppendLine("最大值：" + statisticsResults.Maximum.ToString() + "\n");
            stringBuilder.AppendLine("总计： " + statisticsResults.Sum.ToString() + "\n");
            stringBuilder.AppendLine("平均值： " + statisticsResults.Mean.ToString() + "\n");
            stringBuilder.AppendLine("标准差： " + statisticsResults.StandardDeviation.ToString());
            //将统计结果显示在窗体中 
            labelStatisticsResult.Text = stringBuilder.ToString();
        }
    }
}
