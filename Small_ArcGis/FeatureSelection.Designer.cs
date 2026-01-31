namespace Small_ArcGis
{
    partial class FormSelection
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("图层");
            this.treeViewLayers = new System.Windows.Forms.TreeView();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.labelLayerSelectionCount = new System.Windows.Forms.Label();
            this.labelMapSelectionCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewLayers
            // 
            this.treeViewLayers.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeViewLayers.Location = new System.Drawing.Point(0, 0);
            this.treeViewLayers.Name = "treeViewLayers";
            treeNode1.Name = "Layers";
            treeNode1.Text = "图层";
            this.treeViewLayers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewLayers.Size = new System.Drawing.Size(146, 428);
            this.treeViewLayers.TabIndex = 0;
            this.treeViewLayers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLayers_NodeMouseClick);
            this.treeViewLayers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewLayers_MouseClick);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(143, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(589, 356);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // labelLayerSelectionCount
            // 
            this.labelLayerSelectionCount.AutoSize = true;
            this.labelLayerSelectionCount.Location = new System.Drawing.Point(548, 386);
            this.labelLayerSelectionCount.Name = "labelLayerSelectionCount";
            this.labelLayerSelectionCount.Size = new System.Drawing.Size(155, 12);
            this.labelLayerSelectionCount.TabIndex = 2;
            this.labelLayerSelectionCount.Text = "当前图层选择了 0 个要素。";
            // 
            // labelMapSelectionCount
            // 
            this.labelMapSelectionCount.AutoSize = true;
            this.labelMapSelectionCount.Location = new System.Drawing.Point(170, 386);
            this.labelMapSelectionCount.Name = "labelMapSelectionCount";
            this.labelMapSelectionCount.Size = new System.Drawing.Size(167, 12);
            this.labelMapSelectionCount.TabIndex = 4;
            this.labelMapSelectionCount.Text = "当前地图共选择了 0 个要素。";
            // 
            // FormSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 428);
            this.Controls.Add(this.labelMapSelectionCount);
            this.Controls.Add(this.labelLayerSelectionCount);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.treeViewLayers);
            this.Name = "FormSelection";
            this.Text = "FeatureSelection";
            this.Load += new System.EventHandler(this.FeatureSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewLayers;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelLayerSelectionCount;
        private System.Windows.Forms.Label labelMapSelectionCount;

    }
}