namespace Small_ArcGis
{
    partial class frmSymbolSelector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSymbolSelector));
            this.SymbologyCtr = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.grbPreview = new System.Windows.Forms.GroupBox();
            this.ptbPreview = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grbSet = new System.Windows.Forms.GroupBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblOutlineColor = new System.Windows.Forms.Label();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnOutlineColor = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoreSymbols = new System.Windows.Forms.Button();
            this.contextMenuStripMoreSymbol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.SymbologyCtr)).BeginInit();
            this.grbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).BeginInit();
            this.grbSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // SymbologyCtr
            // 
            this.SymbologyCtr.Dock = System.Windows.Forms.DockStyle.Left;
            this.SymbologyCtr.Location = new System.Drawing.Point(0, 0);
            this.SymbologyCtr.Name = "SymbologyCtr";
            this.SymbologyCtr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SymbologyCtr.OcxState")));
            this.SymbologyCtr.Size = new System.Drawing.Size(204, 382);
            this.SymbologyCtr.TabIndex = 0;
            this.SymbologyCtr.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.SymbologyCtr_OnDoubleClick);
            this.SymbologyCtr.OnStyleClassChanged += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnStyleClassChangedEventHandler(this.SymbologyCtr_OnStyleClassChanged);
            this.SymbologyCtr.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.SymbologyCtr_OnItemSelected);
            // 
            // grbPreview
            // 
            this.grbPreview.Controls.Add(this.ptbPreview);
            this.grbPreview.Location = new System.Drawing.Point(352, 12);
            this.grbPreview.Name = "grbPreview";
            this.grbPreview.Size = new System.Drawing.Size(228, 100);
            this.grbPreview.TabIndex = 1;
            this.grbPreview.TabStop = false;
            this.grbPreview.Text = "预览";
            // 
            // ptbPreview
            // 
            this.ptbPreview.Location = new System.Drawing.Point(6, 11);
            this.ptbPreview.Name = "ptbPreview";
            this.ptbPreview.Size = new System.Drawing.Size(216, 83);
            this.ptbPreview.TabIndex = 0;
            this.ptbPreview.TabStop = false;
            // 
            // grbSet
            // 
            this.grbSet.Controls.Add(this.btnOutlineColor);
            this.grbSet.Controls.Add(this.btnColor);
            this.grbSet.Controls.Add(this.nudAngle);
            this.grbSet.Controls.Add(this.nudWidth);
            this.grbSet.Controls.Add(this.nudSize);
            this.grbSet.Controls.Add(this.lblOutlineColor);
            this.grbSet.Controls.Add(this.lblAngle);
            this.grbSet.Controls.Add(this.lblWidth);
            this.grbSet.Controls.Add(this.lblSize);
            this.grbSet.Controls.Add(this.lblColor);
            this.grbSet.Location = new System.Drawing.Point(352, 137);
            this.grbSet.Name = "grbSet";
            this.grbSet.Size = new System.Drawing.Size(228, 164);
            this.grbSet.TabIndex = 2;
            this.grbSet.TabStop = false;
            this.grbSet.Text = "设置";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(7, 21);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(41, 12);
            this.lblColor.TabIndex = 0;
            this.lblColor.Text = "颜色：";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(7, 47);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(41, 12);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "大小：";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(6, 74);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(41, 12);
            this.lblWidth.TabIndex = 2;
            this.lblWidth.Text = "线宽：";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(6, 101);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(41, 12);
            this.lblAngle.TabIndex = 3;
            this.lblAngle.Text = "角度：";
            // 
            // lblOutlineColor
            // 
            this.lblOutlineColor.AutoSize = true;
            this.lblOutlineColor.Location = new System.Drawing.Point(7, 131);
            this.lblOutlineColor.Name = "lblOutlineColor";
            this.lblOutlineColor.Size = new System.Drawing.Size(65, 12);
            this.lblOutlineColor.TabIndex = 4;
            this.lblOutlineColor.Text = "外框颜色：";
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(84, 45);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(120, 21);
            this.nudSize.TabIndex = 5;
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(84, 72);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(120, 21);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudAngle
            // 
            this.nudAngle.Location = new System.Drawing.Point(84, 99);
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(120, 21);
            this.nudAngle.TabIndex = 7;
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(84, 16);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(120, 23);
            this.btnColor.TabIndex = 8;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnOutlineColor
            // 
            this.btnOutlineColor.Location = new System.Drawing.Point(84, 126);
            this.btnOutlineColor.Name = "btnOutlineColor";
            this.btnOutlineColor.Size = new System.Drawing.Size(120, 23);
            this.btnOutlineColor.TabIndex = 9;
            this.btnOutlineColor.UseVisualStyleBackColor = true;
            this.btnOutlineColor.Click += new System.EventHandler(this.btnOutlineColor_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(352, 308);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(497, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoreSymbols
            // 
            this.btnMoreSymbols.Location = new System.Drawing.Point(415, 346);
            this.btnMoreSymbols.Name = "btnMoreSymbols";
            this.btnMoreSymbols.Size = new System.Drawing.Size(98, 24);
            this.btnMoreSymbols.TabIndex = 5;
            this.btnMoreSymbols.Text = "更多符号";
            this.btnMoreSymbols.UseVisualStyleBackColor = true;
            this.btnMoreSymbols.Click += new System.EventHandler(this.btnMoreSymbols_Click);
            // 
            // contextMenuStripMoreSymbol
            // 
            this.contextMenuStripMoreSymbol.Name = "contextMenuStripMoreSymbol";
            this.contextMenuStripMoreSymbol.Size = new System.Drawing.Size(153, 26);
            this.contextMenuStripMoreSymbol.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStripMoreSymbol_ItemClicked);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // frmSymbolSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 382);
            this.Controls.Add(this.btnMoreSymbols);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grbSet);
            this.Controls.Add(this.grbPreview);
            this.Controls.Add(this.SymbologyCtr);
            this.Name = "frmSymbolSelector";
            this.Text = "frmSymbolSelector";
            this.Load += new System.EventHandler(this.frmSymbolSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SymbologyCtr)).EndInit();
            this.grbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).EndInit();
            this.grbSet.ResumeLayout(false);
            this.grbSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxSymbologyControl SymbologyCtr;
        private System.Windows.Forms.GroupBox grbPreview;
        private System.Windows.Forms.PictureBox ptbPreview;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox grbSet;
        private System.Windows.Forms.Button btnOutlineColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.Label lblOutlineColor;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoreSymbols;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMoreSymbol;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}