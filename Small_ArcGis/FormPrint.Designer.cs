namespace Small_ArcGis
{
    partial class FormPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbExport = new System.Windows.Forms.GroupBox();
            this.cmbDPI = new System.Windows.Forms.ComboBox();
            this.btnExPath2 = new System.Windows.Forms.Button();
            this.txtExPath2 = new System.Windows.Forms.TextBox();
            this.lblExport2 = new System.Windows.Forms.Label();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cmbExportFormat = new System.Windows.Forms.ComboBox();
            this.lblDPI = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.gbPageSetup = new System.Windows.Forms.GroupBox();
            this.btnExPath1 = new System.Windows.Forms.Button();
            this.txtExPath1 = new System.Windows.Forms.TextBox();
            this.lblExport1 = new System.Windows.Forms.Label();
            this.btnCancel1 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.lblOrient = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.panel2.SuspendLayout();
            this.gbExport.SuspendLayout();
            this.gbPageSetup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.gbPreview);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 516);
            this.panel1.TabIndex = 0;
            // 
            // gbPreview
            // 
            this.gbPreview.Controls.Add(this.picPreview);
            this.gbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPreview.Location = new System.Drawing.Point(0, 0);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(448, 516);
            this.gbPreview.TabIndex = 0;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "打印预览";
            // 
            // picPreview
            // 
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(3, 17);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(442, 496);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.gbExport);
            this.panel2.Controls.Add(this.gbPageSetup);
            this.panel2.Location = new System.Drawing.Point(448, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(286, 516);
            this.panel2.TabIndex = 1;
            // 
            // gbExport
            // 
            this.gbExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExport.Controls.Add(this.cmbDPI);
            this.gbExport.Controls.Add(this.btnExPath2);
            this.gbExport.Controls.Add(this.txtExPath2);
            this.gbExport.Controls.Add(this.lblExport2);
            this.gbExport.Controls.Add(this.btnCancel2);
            this.gbExport.Controls.Add(this.btnExport);
            this.gbExport.Controls.Add(this.cmbExportFormat);
            this.gbExport.Controls.Add(this.lblDPI);
            this.gbExport.Controls.Add(this.lblFormat);
            this.gbExport.Location = new System.Drawing.Point(0, 263);
            this.gbExport.Name = "gbExport";
            this.gbExport.Size = new System.Drawing.Size(286, 253);
            this.gbExport.TabIndex = 1;
            this.gbExport.TabStop = false;
            this.gbExport.Text = "导出设置";
            // 
            // cmbDPI
            // 
            this.cmbDPI.FormattingEnabled = true;
            this.cmbDPI.Items.AddRange(new object[] {
            "200",
            "300",
            "400",
            "500",
            "600"});
            this.cmbDPI.Location = new System.Drawing.Point(81, 90);
            this.cmbDPI.Name = "cmbDPI";
            this.cmbDPI.Size = new System.Drawing.Size(167, 20);
            this.cmbDPI.TabIndex = 22;
            // 
            // btnExPath2
            // 
            this.btnExPath2.Location = new System.Drawing.Point(214, 135);
            this.btnExPath2.Name = "btnExPath2";
            this.btnExPath2.Size = new System.Drawing.Size(66, 23);
            this.btnExPath2.TabIndex = 21;
            this.btnExPath2.Text = "浏览";
            this.btnExPath2.UseVisualStyleBackColor = true;
            // 
            // txtExPath2
            // 
            this.txtExPath2.Location = new System.Drawing.Point(77, 135);
            this.txtExPath2.Name = "txtExPath2";
            this.txtExPath2.Size = new System.Drawing.Size(131, 21);
            this.txtExPath2.TabIndex = 20;
            // 
            // lblExport2
            // 
            this.lblExport2.AutoSize = true;
            this.lblExport2.Location = new System.Drawing.Point(6, 138);
            this.lblExport2.Name = "lblExport2";
            this.lblExport2.Size = new System.Drawing.Size(65, 12);
            this.lblExport2.TabIndex = 19;
            this.lblExport2.Text = "保存路径：";
            // 
            // btnCancel2
            // 
            this.btnCancel2.Location = new System.Drawing.Point(159, 176);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(89, 31);
            this.btnCancel2.TabIndex = 18;
            this.btnCancel2.Text = "取消";
            this.btnCancel2.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(54, 176);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 31);
            this.btnExport.TabIndex = 17;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // cmbExportFormat
            // 
            this.cmbExportFormat.FormattingEnabled = true;
            this.cmbExportFormat.Items.AddRange(new object[] {
            "JPG 图片 (.jpg)",
            "PNG 图片 (.png)"});
            this.cmbExportFormat.Location = new System.Drawing.Point(81, 45);
            this.cmbExportFormat.Name = "cmbExportFormat";
            this.cmbExportFormat.Size = new System.Drawing.Size(167, 20);
            this.cmbExportFormat.TabIndex = 16;
            // 
            // lblDPI
            // 
            this.lblDPI.AutoSize = true;
            this.lblDPI.Location = new System.Drawing.Point(18, 93);
            this.lblDPI.Name = "lblDPI";
            this.lblDPI.Size = new System.Drawing.Size(53, 12);
            this.lblDPI.TabIndex = 1;
            this.lblDPI.Text = "分辨率：";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(18, 48);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(65, 12);
            this.lblFormat.TabIndex = 0;
            this.lblFormat.Text = "导出格式：";
            // 
            // gbPageSetup
            // 
            this.gbPageSetup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbPageSetup.Controls.Add(this.btnExPath1);
            this.gbPageSetup.Controls.Add(this.txtExPath1);
            this.gbPageSetup.Controls.Add(this.lblExport1);
            this.gbPageSetup.Controls.Add(this.btnCancel1);
            this.gbPageSetup.Controls.Add(this.btnPrint);
            this.gbPageSetup.Controls.Add(this.comboBox2);
            this.gbPageSetup.Controls.Add(this.lblPrinter);
            this.gbPageSetup.Controls.Add(this.lblOrient);
            this.gbPageSetup.Controls.Add(this.radioButton2);
            this.gbPageSetup.Controls.Add(this.radioButton1);
            this.gbPageSetup.Controls.Add(this.cmbPaperSize);
            this.gbPageSetup.Controls.Add(this.lblPaperSize);
            this.gbPageSetup.Location = new System.Drawing.Point(0, 0);
            this.gbPageSetup.Name = "gbPageSetup";
            this.gbPageSetup.Size = new System.Drawing.Size(286, 244);
            this.gbPageSetup.TabIndex = 0;
            this.gbPageSetup.TabStop = false;
            this.gbPageSetup.Text = "打印机设置";
            // 
            // btnExPath1
            // 
            this.btnExPath1.Location = new System.Drawing.Point(214, 136);
            this.btnExPath1.Name = "btnExPath1";
            this.btnExPath1.Size = new System.Drawing.Size(66, 23);
            this.btnExPath1.TabIndex = 16;
            this.btnExPath1.Text = "浏览";
            this.btnExPath1.UseVisualStyleBackColor = true;
            // 
            // txtExPath1
            // 
            this.txtExPath1.Location = new System.Drawing.Point(77, 136);
            this.txtExPath1.Name = "txtExPath1";
            this.txtExPath1.Size = new System.Drawing.Size(131, 21);
            this.txtExPath1.TabIndex = 15;
            // 
            // lblExport1
            // 
            this.lblExport1.AutoSize = true;
            this.lblExport1.Location = new System.Drawing.Point(6, 139);
            this.lblExport1.Name = "lblExport1";
            this.lblExport1.Size = new System.Drawing.Size(65, 12);
            this.lblExport1.TabIndex = 14;
            this.lblExport1.Text = "保存路径：";
            // 
            // btnCancel1
            // 
            this.btnCancel1.Location = new System.Drawing.Point(159, 177);
            this.btnCancel1.Name = "btnCancel1";
            this.btnCancel1.Size = new System.Drawing.Size(89, 31);
            this.btnCancel1.TabIndex = 13;
            this.btnCancel1.Text = "取消";
            this.btnCancel1.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(54, 177);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 31);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(81, 24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(159, 20);
            this.comboBox2.TabIndex = 11;
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(6, 27);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(77, 12);
            this.lblPrinter.TabIndex = 10;
            this.lblPrinter.Text = "选择打印机：";
            // 
            // lblOrient
            // 
            this.lblOrient.AutoSize = true;
            this.lblOrient.Location = new System.Drawing.Point(6, 104);
            this.lblOrient.Name = "lblOrient";
            this.lblOrient.Size = new System.Drawing.Size(65, 12);
            this.lblOrient.TabIndex = 9;
            this.lblOrient.Text = "纸张方向：";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(161, 102);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "纵向";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(81, 102);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "横向";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Items.AddRange(new object[] {
            "A4",
            "A3",
            "A2",
            "Letter"});
            this.cmbPaperSize.Location = new System.Drawing.Point(81, 62);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(159, 20);
            this.cmbPaperSize.TabIndex = 6;
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.AutoSize = true;
            this.lblPaperSize.Location = new System.Drawing.Point(6, 65);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(65, 12);
            this.lblPaperSize.TabIndex = 5;
            this.lblPaperSize.Text = "纸张大小：";
            // 
            // FormPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 516);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormPrint";
            this.Text = "FormPrint";
            this.panel1.ResumeLayout(false);
            this.gbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.gbExport.ResumeLayout(false);
            this.gbExport.PerformLayout();
            this.gbPageSetup.ResumeLayout(false);
            this.gbPageSetup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbExport;
        private System.Windows.Forms.ComboBox cmbExportFormat;
        private System.Windows.Forms.Label lblDPI;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.GroupBox gbPageSetup;
        private System.Windows.Forms.Button btnCancel1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label lblOrient;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.Label lblExport1;
        private System.Windows.Forms.Button btnExPath2;
        private System.Windows.Forms.TextBox txtExPath2;
        private System.Windows.Forms.Label lblExport2;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtExPath1;
        private System.Windows.Forms.ComboBox cmbDPI;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Button btnExPath1;
    }
}