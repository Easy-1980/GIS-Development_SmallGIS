namespace Small_ArcGis
{
    partial class FormClassifiedRenderer
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLayer = new System.Windows.Forms.Label();
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.cmbColors = new System.Windows.Forms.ComboBox();
            this.cmbMethods = new System.Windows.Forms.ComboBox();
            this.numClasses = new System.Windows.Forms.NumericUpDown();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前图层：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "分级字段：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "分级级数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "分级色彩：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "分级方式：";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(122, 22);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(0, 12);
            this.lblLayer.TabIndex = 5;
            // 
            // cmbFields
            // 
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(124, 57);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(124, 20);
            this.cmbFields.TabIndex = 6;
            // 
            // cmbColors
            // 
            this.cmbColors.FormattingEnabled = true;
            this.cmbColors.Items.AddRange(new object[] {
            "蓝 -> 红",
            "绿- >黄",
            "灰 -> 黑",
            "浅红 -> 深红"});
            this.cmbColors.Location = new System.Drawing.Point(124, 143);
            this.cmbColors.Name = "cmbColors";
            this.cmbColors.Size = new System.Drawing.Size(124, 20);
            this.cmbColors.TabIndex = 7;
            // 
            // cmbMethods
            // 
            this.cmbMethods.FormattingEnabled = true;
            this.cmbMethods.Items.AddRange(new object[] {
            "自然间断分级",
            "等间距分级",
            "分位数分级",
            "几何间隔分级"});
            this.cmbMethods.Location = new System.Drawing.Point(124, 184);
            this.cmbMethods.Name = "cmbMethods";
            this.cmbMethods.Size = new System.Drawing.Size(124, 20);
            this.cmbMethods.TabIndex = 8;
            // 
            // numClasses
            // 
            this.numClasses.Location = new System.Drawing.Point(124, 102);
            this.numClasses.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numClasses.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numClasses.Name = "numClasses";
            this.numClasses.Size = new System.Drawing.Size(124, 21);
            this.numClasses.TabIndex = 9;
            this.numClasses.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(47, 228);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "确定";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormClassifiedRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 291);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.numClasses);
            this.Controls.Add(this.cmbMethods);
            this.Controls.Add(this.cmbColors);
            this.Controls.Add(this.cmbFields);
            this.Controls.Add(this.lblLayer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormClassifiedRenderer";
            this.Text = "FormClassifiedRenderer";
            ((System.ComponentModel.ISupportInitialize)(this.numClasses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.ComboBox cmbFields;
        private System.Windows.Forms.ComboBox cmbColors;
        private System.Windows.Forms.ComboBox cmbMethods;
        private System.Windows.Forms.NumericUpDown numClasses;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}