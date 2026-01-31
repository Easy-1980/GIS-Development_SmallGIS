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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace Small_ArcGis
{
    public partial class FromExportMap : Form
    {
        public FromExportMap(AxMapControl mainAxMapControl)
        {
            InitializeComponent();
            pActiveView = mainAxMapControl.ActiveView;
        }

        private string pSavePath = "";
        private IActiveView pActiveView;
        private IGeometry pGeometry = null;

        public IGeometry GetGeometry
        {
            set { pGeometry = value; }
        }
        private bool bRegion = true;
        public bool IsRegion
        {
            set { bRegion = value; }
        }

        private void FromExportMap_Load(object sender, EventArgs e)
        {
            cboResolution.Text = pActiveView.ScreenDisplay.DisplayTransformation.Resolution.ToString();
            cboResolution.Items.Add(cboResolution.Text);
            if (bRegion)
            {
                IEnvelope pEnvelope = pGeometry.Envelope;
                tagRECT ptagRECT = new tagRECT();
                pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnvelope, ref ptagRECT, 9);
                if (cboResolution.Text!="")
                {
                    txtWidth.Text = ptagRECT.right.ToString();
                    txtHeight.Text = ptagRECT.bottom.ToString();
                }
            }
            else
            {
                txtWidth.Text = pActiveView.ExportFrame.right.ToString();
                txtHeight.Text = pActiveView.ExportFrame.bottom.ToString();
            }
        }

        private void cboResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            double num = (int)Math.Round(pActiveView.ScreenDisplay.DisplayTransformation.Resolution);
            if (cboResolution.Text=="")
            {
                txtWidth.Text = "";
                txtHeight.Text = "";
                return;
            }
            if (bRegion)
            {
                IEnvelope pEnvelope = pGeometry.Envelope;
                tagRECT ptagRECT = new tagRECT();
                pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnvelope, ref ptagRECT, 9);
                txtWidth.Text = Math.Round((double)(ptagRECT.right * (double.Parse(cboResolution.Text) / (double)num))).ToString();
                txtHeight.Text = Math.Round((double)(ptagRECT.bottom * (double.Parse(cboResolution.Text) / (double)num))).ToString();
            }
            else
            {
                txtWidth.Text = Math.Round((double)(pActiveView.ExportFrame.right * (double.Parse(cboResolution.Text) / (double)num))).ToString();
                txtHeight.Text = Math.Round((double)(pActiveView.ExportFrame.bottom * (double.Parse(cboResolution.Text) / (double)num))).ToString();
            }
        }

        // 浏览按钮：设置保存位置和保存格式
        private void btnExPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSaveFileD = new SaveFileDialog();
            pSaveFileD.DefaultExt = "jpg|bmp|gif";
            pSaveFileD.Filter = "JPEG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp";
            pSaveFileD.OverwritePrompt = true;
            pSaveFileD.Title = "另存为";
            txtExPath.Text = "";
            if (pSaveFileD.ShowDialog()!=DialogResult.Cancel)
            {
                pSavePath = pSaveFileD.FileName;
                txtExPath.Text = pSaveFileD.FileName;

            }
        }

        // 导出按钮
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (txtExPath.Text=="")
            {
                MessageBox.Show("请先确定导出路径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int resolution = int.Parse(cboResolution.Text);     // 输出分辨率
            int width = int.Parse(txtWidth.Text);
            int height = int.Parse(txtHeight.Text);
            // 地图导出核心代码
            ExportMap.ExportView(pActiveView, pGeometry, resolution, width, height, pSavePath, bRegion);
            pActiveView.GraphicsContainer.DeleteAllElements();
            pActiveView.Refresh();

            this.Close();
        }

        // 取消按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            pActiveView.GraphicsContainer.DeleteAllElements();
            pActiveView.Refresh();
            Dispose();
        }

        private void FromExportMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            pActiveView.GraphicsContainer.DeleteAllElements();
            pActiveView.Refresh();
            Dispose();
        }

    }
}
