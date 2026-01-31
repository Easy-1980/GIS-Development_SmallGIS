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

namespace Small_ArcGis
{
    public partial class FormAddLegend : Form
    {
        public FormAddLegend()
        {
            InitializeComponent();
            SelectedLayers = new List<ILayer>();
            WireEvents();
        }
        private readonly IMap _map;

        public IList<ILayer> SelectedLayers { get; private set; }

        public FormAddLegend(IMap map)
            : this()
        {
            _map = map;
            LoadSourceLayers();
        }

        private void WireEvents()
        {
            btnAdd.Click += btnAdd_Click;
            btnRemove.Click += btnRemove_Click;
            btnUp.Click += btnUp_Click;
            btnDown.Click += btnDown_Click;
            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
            Load += FormAddLegend_Load;
        }

        private void FormAddLegend_Load(object sender, EventArgs e)
        {
            if (_map == null || lbSourceLayers.Items.Count > 0)
            {
                return;
            }

            LoadSourceLayers();
        }

        private void LoadSourceLayers()
        {
            lbSourceLayers.Items.Clear();
            if (_map == null)
            {
                return;
            }

            for (int i = 0; i < _map.LayerCount; i++)
            {
                ILayer layer = _map.get_Layer(i);
                if (layer != null)
                {
                    lbSourceLayers.Items.Add(new LayerWrapper(layer));
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (var item in lbSourceLayers.SelectedItems.Cast<LayerWrapper>().ToList())
            {
                if (!lbLegendItems.Items.Cast<LayerWrapper>().Any(x => x.Layer == item.Layer))
                {
                    lbLegendItems.Items.Add(item);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (var item in lbLegendItems.SelectedItems.Cast<object>().ToList())
            {
                lbLegendItems.Items.Remove(item);
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = lbLegendItems.SelectedIndex;
            if (index > 0)
            {
                var item = lbLegendItems.Items[index];
                lbLegendItems.Items.RemoveAt(index);
                lbLegendItems.Items.Insert(index - 1, item);
                lbLegendItems.SelectedIndex = index - 1;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = lbLegendItems.SelectedIndex;
            if (index >= 0 && index < lbLegendItems.Items.Count - 1)
            {
                var item = lbLegendItems.Items[index];
                lbLegendItems.Items.RemoveAt(index);
                lbLegendItems.Items.Insert(index + 1, item);
                lbLegendItems.SelectedIndex = index + 1;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedLayers = lbLegendItems.Items.Cast<LayerWrapper>().Select(l => l.Layer).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class LayerWrapper
        {
            public LayerWrapper(ILayer layer)
            {
                Layer = layer;
            }

            public ILayer Layer { get; private set; }

            public override string ToString()
            {
                return Layer == null ? string.Empty : Layer.Name;
            }
        }
    }
}
