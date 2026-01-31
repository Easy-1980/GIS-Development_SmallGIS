namespace Small_ArcGis
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.导出地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.局部导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全局导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载MxdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMxFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imxDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMxDocCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载SHP数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通过工作空间加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通过AddShapefile加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载栅格数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载个人地理数据库数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载文件地理数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载TXT文本数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.要素选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.要素SelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缩放至选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除至SelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.距离量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.面积量测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkCustomize = new System.Windows.Forms.ToolStripMenuItem();
            this.查询统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图选择集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计选择集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStartEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEndEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbSelLayer = new System.Windows.Forms.ToolStripComboBox();
            this.tsmAddFeature = new System.Windows.Forms.ToolStripMenuItem();
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.比例尺ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图例ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.指北针ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.barCooTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axMapControl2 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemoveLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLayerSel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLayerUnSel = new System.Windows.Forms.ToolStripMenuItem();
            this.分级符号化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.加载数据ToolStripMenuItem,
            this.地图浏览ToolStripMenuItem,
            this.要素选择ToolStripMenuItem,
            this.地图量测ToolStripMenuItem,
            this.chkCustomize,
            this.查询统计ToolStripMenuItem,
            this.tsmEdit,
            this.插入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1051, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.新建地图ToolStripMenuItem,
            this.保存地图ToolStripMenuItem,
            this.另存地图ToolStripMenuItem,
            this.toolStripSeparator1,
            this.导出地图ToolStripMenuItem,
            this.打印ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.打开文件ToolStripMenuItem.Text = "打开地图";
            // 
            // 新建地图ToolStripMenuItem
            // 
            this.新建地图ToolStripMenuItem.Name = "新建地图ToolStripMenuItem";
            this.新建地图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新建地图ToolStripMenuItem.Text = "新建地图";
            // 
            // 保存地图ToolStripMenuItem
            // 
            this.保存地图ToolStripMenuItem.Name = "保存地图ToolStripMenuItem";
            this.保存地图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.保存地图ToolStripMenuItem.Text = "保存地图";
            // 
            // 另存地图ToolStripMenuItem
            // 
            this.另存地图ToolStripMenuItem.Name = "另存地图ToolStripMenuItem";
            this.另存地图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.另存地图ToolStripMenuItem.Text = "另存地图";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // 导出地图ToolStripMenuItem
            // 
            this.导出地图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.局部导出ToolStripMenuItem,
            this.全局导出ToolStripMenuItem});
            this.导出地图ToolStripMenuItem.Name = "导出地图ToolStripMenuItem";
            this.导出地图ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导出地图ToolStripMenuItem.Text = "导出地图";
            // 
            // 局部导出ToolStripMenuItem
            // 
            this.局部导出ToolStripMenuItem.Name = "局部导出ToolStripMenuItem";
            this.局部导出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.局部导出ToolStripMenuItem.Text = "局部导出";
            this.局部导出ToolStripMenuItem.Click += new System.EventHandler(this.局部导出ToolStripMenuItem_Click);
            // 
            // 全局导出ToolStripMenuItem
            // 
            this.全局导出ToolStripMenuItem.Name = "全局导出ToolStripMenuItem";
            this.全局导出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.全局导出ToolStripMenuItem.Text = "全局导出";
            this.全局导出ToolStripMenuItem.Click += new System.EventHandler(this.全局导出ToolStripMenuItem_Click);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打印ToolStripMenuItem.Text = "打印";
            this.打印ToolStripMenuItem.Click += new System.EventHandler(this.打印ToolStripMenuItem_Click);
            // 
            // 加载数据ToolStripMenuItem
            // 
            this.加载数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加载MxdToolStripMenuItem,
            this.加载SHP数据ToolStripMenuItem,
            this.加载栅格数据ToolStripMenuItem,
            this.加载个人地理数据库数据ToolStripMenuItem,
            this.加载文件地理数据库ToolStripMenuItem,
            this.加载TXT文本数据ToolStripMenuItem});
            this.加载数据ToolStripMenuItem.Name = "加载数据ToolStripMenuItem";
            this.加载数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.加载数据ToolStripMenuItem.Text = "数据加载";
            // 
            // 加载MxdToolStripMenuItem
            // 
            this.加载MxdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMxFileToolStripMenuItem,
            this.imxDocumentToolStripMenuItem,
            this.openMxDocCommandToolStripMenuItem});
            this.加载MxdToolStripMenuItem.Name = "加载MxdToolStripMenuItem";
            this.加载MxdToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载MxdToolStripMenuItem.Text = "加载Mxd";
            // 
            // loadMxFileToolStripMenuItem
            // 
            this.loadMxFileToolStripMenuItem.Name = "loadMxFileToolStripMenuItem";
            this.loadMxFileToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.loadMxFileToolStripMenuItem.Text = "LoadMxFile";
            this.loadMxFileToolStripMenuItem.Click += new System.EventHandler(this.loadMxFileToolStripMenuItem_Click);
            // 
            // imxDocumentToolStripMenuItem
            // 
            this.imxDocumentToolStripMenuItem.Name = "imxDocumentToolStripMenuItem";
            this.imxDocumentToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.imxDocumentToolStripMenuItem.Text = "IMxDocument";
            this.imxDocumentToolStripMenuItem.Click += new System.EventHandler(this.imxDocumentToolStripMenuItem_Click);
            // 
            // openMxDocCommandToolStripMenuItem
            // 
            this.openMxDocCommandToolStripMenuItem.Name = "openMxDocCommandToolStripMenuItem";
            this.openMxDocCommandToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openMxDocCommandToolStripMenuItem.Text = "OpenMxDocCommand";
            this.openMxDocCommandToolStripMenuItem.Click += new System.EventHandler(this.openMxDocCommandToolStripMenuItem_Click);
            // 
            // 加载SHP数据ToolStripMenuItem
            // 
            this.加载SHP数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通过工作空间加载ToolStripMenuItem,
            this.通过AddShapefile加载ToolStripMenuItem});
            this.加载SHP数据ToolStripMenuItem.Name = "加载SHP数据ToolStripMenuItem";
            this.加载SHP数据ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载SHP数据ToolStripMenuItem.Text = "加载SHP数据";
            // 
            // 通过工作空间加载ToolStripMenuItem
            // 
            this.通过工作空间加载ToolStripMenuItem.Name = "通过工作空间加载ToolStripMenuItem";
            this.通过工作空间加载ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.通过工作空间加载ToolStripMenuItem.Text = "通过工作空间加载";
            this.通过工作空间加载ToolStripMenuItem.Click += new System.EventHandler(this.通过工作空间加载ToolStripMenuItem_Click);
            // 
            // 通过AddShapefile加载ToolStripMenuItem
            // 
            this.通过AddShapefile加载ToolStripMenuItem.Name = "通过AddShapefile加载ToolStripMenuItem";
            this.通过AddShapefile加载ToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.通过AddShapefile加载ToolStripMenuItem.Text = "通过AddShapefile加载";
            this.通过AddShapefile加载ToolStripMenuItem.Click += new System.EventHandler(this.通过AddShapefile加载ToolStripMenuItem_Click);
            // 
            // 加载栅格数据ToolStripMenuItem
            // 
            this.加载栅格数据ToolStripMenuItem.Name = "加载栅格数据ToolStripMenuItem";
            this.加载栅格数据ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载栅格数据ToolStripMenuItem.Text = "加载栅格数据";
            this.加载栅格数据ToolStripMenuItem.Click += new System.EventHandler(this.加载栅格数据ToolStripMenuItem_Click);
            // 
            // 加载个人地理数据库数据ToolStripMenuItem
            // 
            this.加载个人地理数据库数据ToolStripMenuItem.Name = "加载个人地理数据库数据ToolStripMenuItem";
            this.加载个人地理数据库数据ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载个人地理数据库数据ToolStripMenuItem.Text = "加载个人地理数据库数据";
            this.加载个人地理数据库数据ToolStripMenuItem.Click += new System.EventHandler(this.加载个人地理数据库数据ToolStripMenuItem_Click);
            // 
            // 加载文件地理数据库ToolStripMenuItem
            // 
            this.加载文件地理数据库ToolStripMenuItem.Name = "加载文件地理数据库ToolStripMenuItem";
            this.加载文件地理数据库ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载文件地理数据库ToolStripMenuItem.Text = "加载文件地理数据库数据";
            this.加载文件地理数据库ToolStripMenuItem.Click += new System.EventHandler(this.加载文件地理数据库ToolStripMenuItem_Click);
            // 
            // 加载TXT文本数据ToolStripMenuItem
            // 
            this.加载TXT文本数据ToolStripMenuItem.Name = "加载TXT文本数据ToolStripMenuItem";
            this.加载TXT文本数据ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.加载TXT文本数据ToolStripMenuItem.Text = "加载TXT文本数据";
            this.加载TXT文本数据ToolStripMenuItem.Click += new System.EventHandler(this.加载TXT文本数据ToolStripMenuItem_Click);
            // 
            // 地图浏览ToolStripMenuItem
            // 
            this.地图浏览ToolStripMenuItem.Name = "地图浏览ToolStripMenuItem";
            this.地图浏览ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图浏览ToolStripMenuItem.Text = "地图浏览";
            // 
            // 要素选择ToolStripMenuItem
            // 
            this.要素选择ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.要素SelectToolStripMenuItem,
            this.缩放至选择ToolStripMenuItem,
            this.清除至SelectToolStripMenuItem});
            this.要素选择ToolStripMenuItem.Name = "要素选择ToolStripMenuItem";
            this.要素选择ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.要素选择ToolStripMenuItem.Text = "要素选择";
            // 
            // 要素SelectToolStripMenuItem
            // 
            this.要素SelectToolStripMenuItem.Name = "要素SelectToolStripMenuItem";
            this.要素SelectToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.要素SelectToolStripMenuItem.Text = "要素Select";
            this.要素SelectToolStripMenuItem.Click += new System.EventHandler(this.要素SelectToolStripMenuItem_Click);
            // 
            // 缩放至选择ToolStripMenuItem
            // 
            this.缩放至选择ToolStripMenuItem.Name = "缩放至选择ToolStripMenuItem";
            this.缩放至选择ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.缩放至选择ToolStripMenuItem.Text = "缩放至Select";
            this.缩放至选择ToolStripMenuItem.Click += new System.EventHandler(this.缩放至选择ToolStripMenuItem_Click);
            // 
            // 清除至SelectToolStripMenuItem
            // 
            this.清除至SelectToolStripMenuItem.Name = "清除至SelectToolStripMenuItem";
            this.清除至SelectToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.清除至SelectToolStripMenuItem.Text = "清除至Select";
            this.清除至SelectToolStripMenuItem.Click += new System.EventHandler(this.清除至SelectToolStripMenuItem_Click);
            // 
            // 地图量测ToolStripMenuItem
            // 
            this.地图量测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.距离量测ToolStripMenuItem,
            this.面积量测ToolStripMenuItem});
            this.地图量测ToolStripMenuItem.Name = "地图量测ToolStripMenuItem";
            this.地图量测ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图量测ToolStripMenuItem.Text = "地图量测";
            // 
            // 距离量测ToolStripMenuItem
            // 
            this.距离量测ToolStripMenuItem.Name = "距离量测ToolStripMenuItem";
            this.距离量测ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.距离量测ToolStripMenuItem.Text = "距离量测";
            this.距离量测ToolStripMenuItem.Click += new System.EventHandler(this.距离量测ToolStripMenuItem_Click);
            // 
            // 面积量测ToolStripMenuItem
            // 
            this.面积量测ToolStripMenuItem.Name = "面积量测ToolStripMenuItem";
            this.面积量测ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.面积量测ToolStripMenuItem.Text = "面积量测";
            this.面积量测ToolStripMenuItem.Click += new System.EventHandler(this.面积量测ToolStripMenuItem_Click);
            // 
            // chkCustomize
            // 
            this.chkCustomize.Name = "chkCustomize";
            this.chkCustomize.Size = new System.Drawing.Size(80, 21);
            this.chkCustomize.Text = "定制对话框";
            this.chkCustomize.CheckedChanged += new System.EventHandler(this.chkCustomize_CheckedChanged);
            // 
            // 查询统计ToolStripMenuItem
            // 
            this.查询统计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图选择集ToolStripMenuItem,
            this.统计选择集ToolStripMenuItem});
            this.查询统计ToolStripMenuItem.Name = "查询统计ToolStripMenuItem";
            this.查询统计ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.查询统计ToolStripMenuItem.Text = "查询统计";
            // 
            // 地图选择集ToolStripMenuItem
            // 
            this.地图选择集ToolStripMenuItem.Name = "地图选择集ToolStripMenuItem";
            this.地图选择集ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.地图选择集ToolStripMenuItem.Text = "地图选择集";
            this.地图选择集ToolStripMenuItem.Click += new System.EventHandler(this.地图选择集ToolStripMenuItem_Click);
            // 
            // 统计选择集ToolStripMenuItem
            // 
            this.统计选择集ToolStripMenuItem.Name = "统计选择集ToolStripMenuItem";
            this.统计选择集ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.统计选择集ToolStripMenuItem.Text = "统计选择集";
            this.统计选择集ToolStripMenuItem.Click += new System.EventHandler(this.统计选择集ToolStripMenuItem_Click);
            // 
            // tsmEdit
            // 
            this.tsmEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmStartEdit,
            this.tsmSaveEdit,
            this.tsmEndEdit,
            this.cmbSelLayer,
            this.tsmAddFeature});
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(44, 21);
            this.tsmEdit.Text = "编辑";
            // 
            // tsmStartEdit
            // 
            this.tsmStartEdit.Name = "tsmStartEdit";
            this.tsmStartEdit.Size = new System.Drawing.Size(181, 22);
            this.tsmStartEdit.Text = "开始编辑";
            this.tsmStartEdit.Click += new System.EventHandler(this.tsmStartEdit_Click);
            // 
            // tsmSaveEdit
            // 
            this.tsmSaveEdit.Name = "tsmSaveEdit";
            this.tsmSaveEdit.Size = new System.Drawing.Size(181, 22);
            this.tsmSaveEdit.Text = "保存编辑";
            this.tsmSaveEdit.Click += new System.EventHandler(this.tsmSaveEdit_Click);
            // 
            // tsmEndEdit
            // 
            this.tsmEndEdit.Name = "tsmEndEdit";
            this.tsmEndEdit.Size = new System.Drawing.Size(181, 22);
            this.tsmEndEdit.Text = "结束编辑";
            this.tsmEndEdit.Click += new System.EventHandler(this.tsmEndEdit_Click);
            // 
            // cmbSelLayer
            // 
            this.cmbSelLayer.Name = "cmbSelLayer";
            this.cmbSelLayer.Size = new System.Drawing.Size(121, 25);
            this.cmbSelLayer.SelectedIndexChanged += new System.EventHandler(this.cmbSelLayer_SelectedIndexChanged);
            // 
            // tsmAddFeature
            // 
            this.tsmAddFeature.Name = "tsmAddFeature";
            this.tsmAddFeature.Size = new System.Drawing.Size(181, 22);
            this.tsmAddFeature.Text = "添加要素";
            this.tsmAddFeature.Click += new System.EventHandler(this.tsmAddFeature_Click);
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文本ToolStripMenuItem,
            this.比例尺ToolStripMenuItem,
            this.图例ToolStripMenuItem,
            this.指北针ToolStripMenuItem,
            this.标题ToolStripMenuItem});
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.插入ToolStripMenuItem.Text = "插入";
            // 
            // 文本ToolStripMenuItem
            // 
            this.文本ToolStripMenuItem.Name = "文本ToolStripMenuItem";
            this.文本ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.文本ToolStripMenuItem.Text = "文本";
            this.文本ToolStripMenuItem.Click += new System.EventHandler(this.文本ToolStripMenuItem_Click);
            // 
            // 比例尺ToolStripMenuItem
            // 
            this.比例尺ToolStripMenuItem.Name = "比例尺ToolStripMenuItem";
            this.比例尺ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.比例尺ToolStripMenuItem.Text = "比例尺";
            this.比例尺ToolStripMenuItem.Click += new System.EventHandler(this.比例尺ToolStripMenuItem_Click);
            // 
            // 图例ToolStripMenuItem
            // 
            this.图例ToolStripMenuItem.Name = "图例ToolStripMenuItem";
            this.图例ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.图例ToolStripMenuItem.Text = "图例";
            this.图例ToolStripMenuItem.Click += new System.EventHandler(this.图例ToolStripMenuItem_Click);
            // 
            // 指北针ToolStripMenuItem
            // 
            this.指北针ToolStripMenuItem.Name = "指北针ToolStripMenuItem";
            this.指北针ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.指北针ToolStripMenuItem.Text = "指北针";
            this.指北针ToolStripMenuItem.Click += new System.EventHandler(this.指北针ToolStripMenuItem_Click);
            // 
            // 标题ToolStripMenuItem
            // 
            this.标题ToolStripMenuItem.Name = "标题ToolStripMenuItem";
            this.标题ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.标题ToolStripMenuItem.Text = "标题";
            this.标题ToolStripMenuItem.Click += new System.EventHandler(this.标题ToolStripMenuItem_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 25);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(1051, 28);
            this.axToolbarControl1.TabIndex = 1;
            this.axToolbarControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IToolbarControlEvents_Ax_OnMouseDownEventHandler(this.axToolbarControl1_OnMouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barCooTxt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 615);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1051, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // barCooTxt
            // 
            this.barCooTxt.Name = "barCooTxt";
            this.barCooTxt.Size = new System.Drawing.Size(80, 17);
            this.barCooTxt.Text = "地图坐标为：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 53);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1051, 562);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.axMapControl2);
            this.splitContainer2.Size = new System.Drawing.Size(277, 562);
            this.splitContainer2.SplitterDistance = 184;
            this.splitContainer2.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(277, 184);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            this.axTOCControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCControl1_OnDoubleClick);
            // 
            // axMapControl2
            // 
            this.axMapControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl2.Location = new System.Drawing.Point(0, 0);
            this.axMapControl2.Name = "axMapControl2";
            this.axMapControl2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl2.OcxState")));
            this.axMapControl2.Size = new System.Drawing.Size(277, 374);
            this.axMapControl2.TabIndex = 0;
            this.axMapControl2.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl2_OnMouseDown);
            this.axMapControl2.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl2_OnMouseUp);
            this.axMapControl2.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl2_OnMouseMove);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(770, 562);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axLicenseControl1);
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(762, 536);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(187, 98);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(756, 530);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnDoubleClickEventHandler(this.axMapControl1_OnDoubleClick);
            this.axMapControl1.OnExtentUpdated += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(this.axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(762, 536);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(3, 3);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(756, 530);
            this.axPageLayoutControl1.TabIndex = 0;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            this.axPageLayoutControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnAfterScreenDrawEventHandler(this.axPageLayoutControl1_OnAfterScreenDraw);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAttribute,
            this.btnZoomToLayer,
            this.btnRemoveLayer,
            this.btnLayerSel,
            this.btnLayerUnSel,
            this.分级符号化ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 136);
            // 
            // btnAttribute
            // 
            this.btnAttribute.Name = "btnAttribute";
            this.btnAttribute.Size = new System.Drawing.Size(136, 22);
            this.btnAttribute.Text = "属性表";
            this.btnAttribute.Click += new System.EventHandler(this.btnAttribute_Click);
            // 
            // btnZoomToLayer
            // 
            this.btnZoomToLayer.Name = "btnZoomToLayer";
            this.btnZoomToLayer.Size = new System.Drawing.Size(136, 22);
            this.btnZoomToLayer.Text = "缩放至图层";
            this.btnZoomToLayer.Click += new System.EventHandler(this.btnZoomToLayer_Click);
            // 
            // btnRemoveLayer
            // 
            this.btnRemoveLayer.Name = "btnRemoveLayer";
            this.btnRemoveLayer.Size = new System.Drawing.Size(136, 22);
            this.btnRemoveLayer.Text = "移除图层";
            this.btnRemoveLayer.Click += new System.EventHandler(this.btnRemoveLayer_Click);
            // 
            // btnLayerSel
            // 
            this.btnLayerSel.Name = "btnLayerSel";
            this.btnLayerSel.Size = new System.Drawing.Size(136, 22);
            this.btnLayerSel.Text = "图层可选";
            this.btnLayerSel.Click += new System.EventHandler(this.btnLayerSel_Click);
            // 
            // btnLayerUnSel
            // 
            this.btnLayerUnSel.Name = "btnLayerUnSel";
            this.btnLayerUnSel.Size = new System.Drawing.Size(136, 22);
            this.btnLayerUnSel.Text = "图层不可选";
            this.btnLayerUnSel.Click += new System.EventHandler(this.btnLayerUnSel_Click);
            // 
            // 分级符号化ToolStripMenuItem
            // 
            this.分级符号化ToolStripMenuItem.Name = "分级符号化ToolStripMenuItem";
            this.分级符号化ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.分级符号化ToolStripMenuItem.Text = "分级符号化";
            this.分级符号化ToolStripMenuItem.Click += new System.EventHandler(this.分级符号化ToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 637);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "   ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl2;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 导出地图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 局部导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全局导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图浏览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 要素选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chkCustomize;
        private System.Windows.Forms.ToolStripMenuItem 查询统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripStatusLabel barCooTxt;
        private System.Windows.Forms.ToolStripMenuItem 加载MxdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMxFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imxDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMxDocCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载SHP数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通过工作空间加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通过AddShapefile加载ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载栅格数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载个人地理数据库数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载文件地理数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载TXT文本数据ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 要素SelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩放至选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除至SelectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnAttribute;
        private System.Windows.Forms.ToolStripMenuItem btnZoomToLayer;
        private System.Windows.Forms.ToolStripMenuItem btnRemoveLayer;
        private System.Windows.Forms.ToolStripMenuItem btnLayerSel;
        private System.Windows.Forms.ToolStripMenuItem btnLayerUnSel;
        private System.Windows.Forms.ToolStripMenuItem 距离量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 面积量测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 地图选择集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计选择集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmStartEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmEndEdit;
        private System.Windows.Forms.ToolStripComboBox cmbSelLayer;
        private System.Windows.Forms.ToolStripMenuItem tsmAddFeature;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 比例尺ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图例ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 指北针ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分级符号化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标题ToolStripMenuItem;
    }
}

