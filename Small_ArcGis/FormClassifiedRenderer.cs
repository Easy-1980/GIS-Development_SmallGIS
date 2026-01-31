using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Small_ArcGis
{
    public partial class FormClassifiedRenderer : Form
    {
        private readonly ClassifiedRendererHelper rendererHelper = new ClassifiedRendererHelper();
        private AxMapControl buddyMap;
        private IFeatureLayer targetLayer;

        public FormClassifiedRenderer()
        {
            InitializeComponent();
            HookEvents();
        }

        public FormClassifiedRenderer(AxMapControl mapControl, IFeatureLayer featureLayer): this()
        {
            buddyMap = mapControl;
            targetLayer = featureLayer;
        }

        public AxMapControl BuddyMap
        {
            get { return buddyMap; }
            set { buddyMap = value; }
        }

        public IFeatureLayer TargetLayer
        {
            get { return targetLayer; }
            set { targetLayer = value; }
        }

        private void HookEvents()
        {
            Load += FormClassifiedRenderer_Load;
            btnApply.Click += BtnApply_Click;
            btnCancel.Click += (s, e) => Close();
        }

        private void FormClassifiedRenderer_Load(object sender, EventArgs e)
        {
            if (cmbColors.Items.Count > 0 && cmbColors.SelectedIndex < 0)
            {
                cmbColors.SelectedIndex = 0;
            }

            if (cmbMethods.Items.Count > 0 && cmbMethods.SelectedIndex < 0)
            {
                cmbMethods.SelectedIndex = 0;
            }

            InitializeLayerInfo();
        }

        private void InitializeLayerInfo()
        {
            if (targetLayer == null)
            {
                return;
            }

            lblLayer.Text = targetLayer.Name;
            PopulateNumericFields();
        }

        private void PopulateNumericFields()
        {
            if (targetLayer == null || targetLayer.FeatureClass == null)
            {
                return;
            }

            cmbFields.Items.Clear();
            IFields fields = targetLayer.FeatureClass.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if (rendererHelper.IsNumericField(field))
                {
                    cmbFields.Items.Add(field.Name);
                }
            }

            if (cmbFields.Items.Count > 0)
            {
                cmbFields.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("当前图层没有可用的数值型字段。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (buddyMap == null)
            {
                MessageBox.Show("未绑定地图控件。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (targetLayer == null || targetLayer.FeatureClass == null)
            {
                MessageBox.Show("请选择有效的要素图层。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IGeoFeatureLayer geoLayer = targetLayer as IGeoFeatureLayer;
            if (geoLayer == null)
            {
                MessageBox.Show("仅支持面或线要素图层。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fieldName = cmbFields.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                MessageBox.Show("请选择分级字段。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classCount = (int)numClasses.Value;
            if (classCount < 2 || classCount > 5)
            {
                MessageBox.Show("分级数需在 2 到 5 之间。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string methodName = cmbMethods.SelectedItem as string ?? string.Empty;
            string colorSchemeName = cmbColors.SelectedItem as string ?? string.Empty;

            try
            {
                IClassBreaksRenderer renderer = rendererHelper.BuildRenderer(targetLayer, fieldName, classCount, methodName, colorSchemeName);
                geoLayer.Renderer = renderer as IFeatureRenderer;

                IActiveView activeView = buddyMap.ActiveView;
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                activeView.Refresh();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

