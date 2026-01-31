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
using ESRI.ArcGIS.Geodatabase;

namespace Small_ArcGis
{
    public partial class FormAttribute : Form
    {
        public FormAttribute()
        {
            InitializeComponent();
        }

        // 生成图层属性
        private IFeatureLayer _curFeatureLayer;
        public IFeatureLayer CurFeatureLayer
        {
            get { return _curFeatureLayer; }
            set { _curFeatureLayer = value; }
        }

        public void InitUI()
        {
            if (_curFeatureLayer==null)
            {
                return;
            }
            IFeature pFeature = null;
            DataTable pFeatDT = new DataTable();
            DataRow pDataRow = null;
            DataColumn pDataColumn = null;
            IField pField = null;
            for (int i = 0; i < _curFeatureLayer.FeatureClass.Fields.FieldCount; i++)
            {
                pDataColumn = new DataColumn();
                pField = _curFeatureLayer.FeatureClass.Fields.get_Field(i);
                pDataColumn.ColumnName = pField.AliasName;
                pDataColumn.DataType = Type.GetType("System.Object");
                pFeatDT.Columns.Add(pDataColumn);
            }
            IFeatureCursor pFeatureCursor = _curFeatureLayer.Search(null, true);
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature!=null)
            {
                pDataRow = pFeatDT.NewRow();
                for (int K = 0; K < pFeatDT.Columns.Count; K++)
                {
                    pDataRow[K] = pFeature.get_Value(K);
                }
                pFeatDT.Rows.Add(pDataRow);
                pFeature = pFeatureCursor.NextFeature();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            dataGridAtrribute.DataSource = pFeatDT;     // 绑定数据源
        }
    }
}
