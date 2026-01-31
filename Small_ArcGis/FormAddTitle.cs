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
    public partial class FormAddTitle : Form
    {
        public string TitleText { get; private set; }

        public double FontSize { get; private set; }

        public FormAddTitle()
        {
            InitializeComponent();
            // 初始化默认值和事件
            nudSize.Minimum = 6;
            nudSize.Maximum = 200;
            nudSize.Value = 18;

            btnOK.Click += btnOK_Click;
            btnCancel.Click += btnCancel_Click;
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            TitleText = txtContent.Text.Trim();
            FontSize = Convert.ToDouble(nudSize.Value);
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
