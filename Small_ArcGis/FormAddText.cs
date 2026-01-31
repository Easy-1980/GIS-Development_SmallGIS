using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Small_ArcGis
{
    public partial class FormAddText : Form
    {
        public string TextContent { get; private set; }

        public double FontSize { get; private set; }

        public string FontName { get; private set; }

        public FormAddText()
        {
            InitializeComponent();
            // 初始化控件和事件
            nudSize.Minimum = 6;
            nudSize.Maximum = 200;
            nudSize.Value = 12;
            if (cmbFont.Items.Count > 0)
            {
                cmbFont.SelectedIndex = 0;
            }

            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            TextContent = txtContent.Text.Trim();
            FontSize = Convert.ToDouble(nudSize.Value);
            FontName = string.IsNullOrWhiteSpace(cmbFont.Text) ? "Arial" : cmbFont.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
