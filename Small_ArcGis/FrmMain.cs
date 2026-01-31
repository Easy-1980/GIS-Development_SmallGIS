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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using Symbology;
using stdole;

namespace Small_ArcGis
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            axTOCControl1.SetBuddyControl(axMapControl1.Object);
            // 必须使用 .Object 获取底层 COM 对象
            axToolbarControl1.SetBuddyControl(axMapControl1.Object);
            // PageLayout 视图
            axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);
            InitObject();
        }

        string pMouseOperate = null;        // 标记鼠标操作功能
        FromExportMap frmExpMap = null;

        private IPoint pPointPt = null;     // 标记鼠标点击点

        // 鹰眼同步全局变量
        private bool bCanDrag;      // 鹰眼地图上的矩形框可移动的标志
        private IPoint pMoveRectPoint;      // 记录在移动鹰眼地图上的矩形框时鼠标的位置
        private IEnvelope pEnv;     // 记录数据视图的Extent
        IFeatureLayer pTocFeatureLayer = null;
        private ILayer pMoveLayer;
        private int toIndex=-1;     // 防止点击图层置顶
        private bool isPageLayoutSyncing = false;       // 防止 PageLayout 刷新重入导致闪烁
        private IEnvelope lastPageLayoutEnv = null;     // 记录上次同步到 PageLayout 的范围，避免重复刷新
        private ESRI.ArcGIS.Geometry.Point pMoveLayerPoint = new ESRI.ArcGIS.Geometry.Point();
        private FormAttribute frmAtrribute = null;

        // 定制对话框
        private ICustomizeDialog cd = new CustomizeDialogClass();
        // 定义事件委托
        private ICustomizeDialogEvents_OnStartDialogEventHandler startDialogE;
        private ICustomizeDialogEvents_OnCloseDialogEventHandler closeDialogE;

        // 长度、面积量算
        private FormMeasureResult frmMeasureResult = null;      // 量算结果窗体
        private INewLineFeedback pNewLineFeedback;      // 追踪线对象
        private INewPolygonFeedback pNewPolygonFeedback;        // 追踪面对象
        private IPoint pMovePt = null;      // 鼠标移动时的当前点
        private double dToltalLength = 0;       // 量测总长度
        private double dSegmentLength = 0;      // 片段距离
        private IPointCollection pAreaPointCol = new MultipointClass();     // 面积量算时画的点进行存储
        private object missing = Type.Missing;

        #region 布局插入状态
        private enum InsertMode
        {
            None = 0,
            Title = 1,
            Text = 2,
            Legend = 3,
            ScaleBar = 4,
            NorthArrow = 5
        }

        private InsertMode _currentInsertMode = InsertMode.None;
        private bool _isLayoutView = false;
        #endregion

        #region 编辑功能全局变量
        private IMap pMap = null;
        private IActiveView pActiveView = null;
        private List<ILayer> plstLayers = null;
        private IFeatureLayer pCurrentLyr = null;
        private IEngineEditor pEngineEditor = null;
        private IEngineEditTask pEngineEditTask = null;
        private IEngineEditLayers pEngineEditLayers = null;
        #endregion

        private ILayer pTocLayer = null;

        private void InitObject()
        {
            try
            {
                ChangeButtonState(false);
                pEngineEditor = new EngineEditorClass();
                MapManager.EngineEditor = pEngineEditor;
                pEngineEditTask = pEngineEditor as IEngineEditTask;
                pEngineEditLayers = pEngineEditor as IEngineEditLayers;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ChangeButtonState(bool bEnable)
        {
            tsmStartEdit.Enabled = !bEnable;
            tsmSaveEdit.Enabled = bEnable;
            tsmEndEdit.Enabled = bEnable;
            cmbSelLayer.Enabled = bEnable;
            tsmAddFeature.Enabled = bEnable;
        }

        private void InitComboBox(List<ILayer> plstLyr)
        {
            cmbSelLayer.Items.Clear();
            for (int i = 0; i < plstLyr.Count; i++)
            {
                if (!cmbSelLayer.Items.Contains(plstLyr[i].Name))
                {
                    cmbSelLayer.Items.Add(plstLyr[i].Name);
                }
                if (cmbSelLayer.Items.Count != 0)
                {
                    cmbSelLayer.SelectedIndex = 0;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chkCustomize.Checked = false;
            chkCustomize.CheckOnClick = true;
            CreateCusDialog();

            axToolbarControl1.AddItem(new ClearCurrentToolCmd(), -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);
            // 添加“添加日期元素”工具
            ICommand addDate = new AddDateTool(axToolbarControl1, axPageLayoutControl1);
            axToolbarControl1.AddItem(addDate, -1, -1, false, 0, esriCommandStyles.esriCommandStyleIconAndText);

            IMenuDef menuDef = new Symbology.SymbologyMenu();
            axToolbarControl1.AddItem(menuDef, -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconAndText);
            axToolbarControl1.SetBuddyControl(axMapControl1.Object);

            // +
            // 注册TabControl切换事件，实现Buddy控件切换
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);


            _isLayoutView = tabControl1.SelectedIndex == 1;
            UpdateInsertMenuState();
            ResetInsertMode();
        }

        // +
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                axToolbarControl1.SetBuddyControl(axMapControl1.Object);
            }
            else
            {
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);
            }


            _isLayoutView = tabControl1.SelectedIndex == 1;
            UpdateInsertMenuState();
            ResetInsertMode();
        }

        private void UpdateInsertMenuState()
        {
            插入ToolStripMenuItem.Enabled = true; // 显示但根据子项控制可用性
            bool enableChild = _isLayoutView;
            foreach (ToolStripItem item in 插入ToolStripMenuItem.DropDownItems)
            {
                item.Enabled = enableChild;
            }
        }

        private void BeginInsertMode(InsertMode mode)
        {
            if (!_isLayoutView)
            {
                return;
            }

            _currentInsertMode = mode;
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
        }

        private void ResetInsertMode()
        {
            _currentInsertMode = InsertMode.None;
            if (axPageLayoutControl1 != null)
            {
                axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerArrow;
            }
        }

        private void CreateCusDialog()
        {
            // 定义事件接口
            ICustomizeDialogEvents_Event pCusEvents = cd as ICustomizeDialogEvents_Event;

            // 实例化事件委托
            startDialogE = new ICustomizeDialogEvents_OnStartDialogEventHandler(OnStartDialog);
            closeDialogE = new ICustomizeDialogEvents_OnCloseDialogEventHandler(OnCloseDialog);
            // 将事件与委托绑定
            pCusEvents.OnStartDialog += startDialogE;
            pCusEvents.OnCloseDialog += closeDialogE;

            cd.SetDoubleClickDestination(axToolbarControl1);
        }

        // 关闭对话框的响应函数
        private void OnCloseDialog()
        {
            axToolbarControl1.Customize = false;
            chkCustomize.Checked = false;
        }

        // 打开对话框的响应函数
        private void OnStartDialog()
        {
            axToolbarControl1.Customize = true;
        }

        #region 数据加载
        private void loadMxFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog pOpenFileDialog = new OpenFileDialog()
                {
                    CheckFileExists = true,
                    Title = "打开地图文档",
                    Filter = "ArcMap文档(*.mxd)|*.mxd",
                    Multiselect = false,
                    RestoreDirectory = true
                };
                if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pFileName = pOpenFileDialog.FileName;
                    if (pFileName == "")
                    {
                        return;
                    }
                    // 检查文档有效性
                    if (axMapControl1.CheckMxFile(pFileName))
                    {
                        // repair
                        //ClearAllData();
                        //// LoadMxFile
                        //axMapControl1.LoadMxFile(pFileName);
                        IMapDocument pMapDocument = new MapDocumentClass();
                        pMapDocument.Open(pFileName);

                        IMap sourceMap = pMapDocument.ActiveView.FocusMap;

                        // repair
                        //IMap targetMap = axMapControl1.Map;
                        IMap targetMap = axMapControl1.Map ?? new MapClass();
                        ILayer lastLayerAdded = null;

                        for (int i = 0; i < sourceMap.LayerCount; i++)
                        {
                            ILayer layerToAdd = sourceMap.get_Layer(i);
                            targetMap.AddLayer(layerToAdd);

                            // +
                            lastLayerAdded = layerToAdd;
                        }

                        // +
                        axMapControl1.Map = targetMap;

                        pMapDocument.Close();

                        // repair
                        //axMapControl1.ActiveView.Refresh();
                        Synchronize_axMapControl2();
                        ZoomToLayer(lastLayerAdded);
                    }
                    else
                    {
                        MessageBox.Show(pFileName + "地图无效", "信息提示");
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearAllData()
        {
            if (axMapControl1 != null && axMapControl1.Map.LayerCount > 0)
            {
                // 新建Map
                IMap dataMap = new MapClass();
                dataMap.Name = "Map";
                axMapControl1.DocumentFilename = string.Empty;
                axMapControl1.Map = dataMap;

                // +
                axMapControl1.ActiveView.Refresh();
            }

            if (axMapControl2 != null)
            {
                axMapControl2.ClearLayers();
                IMap overviewMap = new MapClass();
                overviewMap.Name = "Overview";
                axMapControl2.Map = overviewMap;
                axMapControl2.ActiveView.Refresh();
            }
            // +
        }

        // 在添加新图层后自动缩放至该图层
        private void ZoomToLayer(ILayer layer)
        {
            if (layer == null)
            {
                return;
            }
            IEnvelope extent = layer.AreaOfInterest;
            if (extent != null && !extent.IsEmpty)
            {
                axMapControl1.Extent = extent;
                axMapControl1.ActiveView.Refresh();
                Synchronize_axMapControl2();
            }
        }


        private void imxDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Title = "打开地图文档",
                Filter = "ArcMap文档(*.mxd)|*.mxd",
                Multiselect = false,
                RestoreDirectory = true
            };
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pFileName = pOpenFileDialog.FileName;
                if (pFileName == "")
                {
                    return;
                }
                if (axMapControl1.CheckMxFile(pFileName))
                {
                    IMapDocument pMapDocument = new MapDocumentClass();
                    pMapDocument.Open(pFileName);

                    IMap sourceMap = pMapDocument.ActiveView.FocusMap;
                    IMap targetMap = axMapControl1.Map ?? new MapClass();

                    ILayer lastLayerAdded = null;
                    for (int i = 0; i < sourceMap.LayerCount; i++)
                    {
                        ILayer layerToAdd = sourceMap.get_Layer(i);
                        targetMap.AddLayer(layerToAdd);
                        lastLayerAdded = layerToAdd;
                    }

                    axMapControl1.Map = targetMap;

                    pMapDocument.Close();

                    Synchronize_axMapControl2();
                    ZoomToLayer(lastLayerAdded);
                }
                else
                {
                    MessageBox.Show(pFileName + "地图无效", "信息提示");
                    return;
                }
            }
        }

        private void openMxDocCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmd = new ControlsOpenDocCommand();
            cmd.OnCreate(axMapControl1.GetOcx());
            cmd.OnClick();
        }
        #endregion

        private void 通过工作空间加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "加载SHP数据";
                openFileDialog1.Filter = "Shapefile文件(*.shp)|*.shp";
                DialogResult pDialogResult = openFileDialog1.ShowDialog();
                if (pDialogResult != DialogResult.OK)
                {
                    return;
                }

                // 获取全路径
                string path = openFileDialog1.FileName;
                // 获取文件路径
                string folder = System.IO.Path.GetDirectoryName(path);
                // 获取文件名
                string filename = System.IO.Path.GetFileName(path);

                //// +
                //ClearAllData();

                IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(folder, 0);
                IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
                IFeatureClass pFC = pFeatureWorkspace.OpenFeatureClass(filename);
                IFeatureLayer pFL = new FeatureLayerClass();
                pFL.FeatureClass = pFC;
                pFL.Name = pFC.AliasName;
                ILayer pLayer = pFL as ILayer;
                IMap pMap = axMapControl1.Map;
                pMap.AddLayer(pLayer);
                axMapControl1.Refresh();

                // +
                ZoomToLayer(pLayer);

                // +
                // 同步鹰眼控件显示栅格图层
                Synchronize_axMapControl2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("请上传SHP文件");
            }
        }

        private void 通过AddShapefile加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "加载SHP数据";
            openFileDialog1.Filter = "Shapefile文件(*.shp)|*.shp";
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
            {
                return;
            }
            // 获取全路径
            string pPath = openFileDialog1.FileName;
            // 获取文件路径
            string pFolder = System.IO.Path.GetDirectoryName(pPath);

            //// +
            //ClearAllData();

            // 获取文件名
            string pFilename = System.IO.Path.GetFileName(pPath);
            axMapControl1.AddShapeFile(pFolder, pFilename);

            // +
            // 同步鹰眼控件显示栅格图层
            Synchronize_axMapControl2();

            // +
            ILayer addedLayer = axMapControl1.get_Layer(0);
            ZoomToLayer(addedLayer);
        }

        private void 加载栅格数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Title = "加载栅格数据";
            openFileDialog1.Filter = "栅格文件(*.img)|*.img";
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
            {
                return;
            }
            // 获取全路径
            string pPath = openFileDialog1.FileName;
            // 获取文件路径
            string pFolder = System.IO.Path.GetDirectoryName(pPath);

            //// +
            //ClearAllData();

            // 获取文件名
            string pFilename = System.IO.Path.GetFileName(pPath);

            IWorkspaceFactory pWorkSpaceFactory = new RasterWorkspaceFactory();
            IRasterWorkspace pRasterWorkspace = pWorkSpaceFactory.OpenFromFile(pFolder, 0) as IRasterWorkspace;
            IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pFilename);
            IRasterPyramid pRasterPyramid = pRasterDataset as IRasterPyramid;
            if (pRasterPyramid != null)
            {
                if (!(pRasterPyramid.Present))
                {
                    pRasterPyramid.Create();
                }
            }
            IRasterLayer pRasterLayer = new RasterLayerClass();
            pRasterLayer.CreateFromDataset(pRasterDataset);
            // -
            //axMapControl1.ClearLayers();

            axMapControl1.AddLayer(pRasterLayer);
            axMapControl1.Refresh();
            // +
            // 同步鹰眼控件显示栅格图层
            Synchronize_axMapControl2();
            //+
            ZoomToLayer(pRasterLayer);
        }

        private void 加载个人地理数据库数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "打开个人地理数据库文件";
            openFileDialog1.Filter = "个人地理数据库(*.mdb)|*.mdb";
            DialogResult pDialogResult = openFileDialog1.ShowDialog();
            if (pDialogResult != DialogResult.OK)
            {
                return;
            }
            // 获取全路径
            string pPath = openFileDialog1.FileName;
            // 获取文件路径
            string pFolder = System.IO.Path.GetDirectoryName(pPath);
            // 获取文件名
            string pFilename = System.IO.Path.GetFileName(pPath);
            IWorkspaceFactory pWorkspaceFactory = new AccessWorkspaceFactory();
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pPath, 0) as IWorkspace;
            // -
            //// 删除数据
            //ClearAllData();

            // 加载数据
            AddAllDataset(pWorkspace, axMapControl1);

            // +
            // 同步鹰眼控件显示栅格图层
            Synchronize_axMapControl2();
            // +
            if (axMapControl1.LayerCount > 0)
            {
                ILayer top = axMapControl1.get_Layer(0);
                ZoomToLayer(top);
            }
        }

        /// <summary>
        /// 记载工作空间要素或栅格数据
        /// </summary>
        /// <param name="pWorkspace">工作空间</param>
        /// <param name="axMapControl1">要加载数据的空间</param>
        private void AddAllDataset(IWorkspace pWorkspace, AxMapControl axMapControl1)
        {
            IEnumDataset pEnumDataset = pWorkspace.get_Datasets(esriDatasetType.esriDTAny);
            pEnumDataset.Reset();
            IDataset pDataset = pEnumDataset.Next();
            // 判断数据集是否包含数据

            // 改pEnumDataset为pDataset----385行while和387行if

            while (pDataset != null)
            {
                if (pDataset is IFeatureDataset) // 要素数据集
                {
                    IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(pDataset.Name);
                    IEnumDataset pEnumDataset1 = pFeatureDataset.Subsets;
                    pEnumDataset1.Reset();
                    IDataset pDataset1 = pEnumDataset1.Next();
                    IGroupLayer pGroupLayer = new GroupLayerClass();
                    pGroupLayer.Name = pFeatureDataset.Name;
                    while (pDataset1 != null)
                    {
                        if (pDataset1 is IFeatureClass)    // 要素类
                        {
                            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                            pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset1.Name);
                            if (pFeatureLayer.FeatureClass != null)
                            {
                                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                                pGroupLayer.Add(pFeatureLayer);
                                axMapControl1.Map.AddLayer(pFeatureLayer);
                            }

                        }
                        pDataset1 = pEnumDataset1.Next();
                    }
                }
                else if (pDataset is IFeatureClass)   // 要素类
                {
                    IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspace;
                    IFeatureLayer pFeatureLayer = new FeatureLayerClass();
                    pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                    pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                    axMapControl1.Map.AddLayer(pFeatureLayer);
                }
                else if (pDataset is IRasterDataset)    // 栅格数据集
                {
                    IRasterWorkspaceEx pRasterWorkspace = (IRasterWorkspaceEx)pWorkspace;
                    IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pDataset.Name);
                    IRasterPyramid pRasterPyramid = pRasterDataset as IRasterPyramid3;
                    if (pRasterPyramid != null)
                    {
                        if (!(pRasterPyramid.Present))
                        {
                            // 创建影像金字塔
                            pRasterPyramid.Create();
                        }
                    }
                    IRasterLayer pRasterLayer = new RasterLayerClass();
                    pRasterLayer.CreateFromDataset(pRasterDataset);
                    ILayer pLayer = pRasterLayer as ILayer;
                    axMapControl1.AddLayer(pLayer);
                }
                pDataset = pEnumDataset.Next();
            }
            axMapControl1.ActiveView.Refresh();
        }

        private void 加载文件地理数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory pFileGDBWorkspaceFactory;
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string pFullPath = dlg.SelectedPath;
            if (pFullPath == " ")
            {
                return;
            }
            pFileGDBWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
            // -
            //// 新增删除数据
            //ClearAllData();

            // 获取工作空间
            IWorkspace pWorkspace = pFileGDBWorkspaceFactory.OpenFromFile(pFullPath, 0);
            AddAllDataset(pWorkspace, axMapControl1);

            // +
            // 同步鹰眼控件显示栅格图层
            Synchronize_axMapControl2();
            // +
            if (axMapControl1.LayerCount > 0)
            {
                ILayer top = axMapControl1.get_Layer(0);
                ZoomToLayer(top);
            }
        }

        private void 加载TXT文本数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormAddTxt Fat = new FormAddTxt();
            //Fat.BuddyMap = axMapControl1;
            //Fat.Show();

            // repair
            FormAddTxt fat = new FormAddTxt();
            fat.BuddyMap = axMapControl1;

            EventHandler syncHandler = null;
            syncHandler = (s, args) =>
            {
                Synchronize_axMapControl2();
                // +
                if (axMapControl1.LayerCount > 0)
                {
                    ILayer top = axMapControl1.get_Layer(0);
                    ZoomToLayer(top);
                }
                fat.LayerCreated -= syncHandler;
            };
            fat.LayerCreated += syncHandler;

            fat.Show();

        }

        private void 要素SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            ControlsSelectFeaturesToolClass pTool = new ControlsSelectFeaturesToolClass();
            pTool.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;

        }

        private void 缩放至选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 调用自带命令
            //ICommand cmd = new ControlsZoomToSelectedCommandClass();
            //cmd.OnCreate(axMapControl1.Object);
            //cmd.OnClick();

            // 重写缩放至选择命令
            int pSelection = axMapControl1.Map.SelectionCount;
            if (pSelection == 0)
            {
                MessageBox.Show("请选择要素");
            }
            ISelection sel = axMapControl1.Map.FeatureSelection;
            IEnumFeature enumsel = sel as IEnumFeature;
            enumsel.Reset();
            IEnvelope pEnv = new EnvelopeClass();
            IFeature pFeature = enumsel.Next();
            while (pFeature != null)
            {
                pEnv.Union(pFeature.Extent);
                /*     
                if (pFeature.Extent!=null&&!pFeature.Extent.IsEmpty)
                {
                    pEnv.Union(pFeature.Extent);
                }*/
                pFeature = enumsel.Next();
            }
            pEnv.Expand(1.1, 1.1, true);
            axMapControl1.ActiveView.Extent = pEnv;
            axMapControl1.ActiveView.Refresh();

        }

        private void 清除至SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand cmd = new ControlsClearSelectionCommandClass();
            cmd.OnCreate(axMapControl1.Object);
            cmd.OnClick();
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            pPointPt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            if (e.button == 1)
            {
                IActiveView pActiveView = axMapControl1.ActiveView;
                IEnvelope env = new EnvelopeClass();
                switch (pMouseOperate)
                {
                    #region 距离量算
                    case "MeasureLength":
                        if (pNewLineFeedback == null)
                        {
                            // 实例化追踪线对象
                            pNewLineFeedback = new NewLineFeedback();
                            pNewLineFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            // 设置起点，开始动态绘制
                            pNewLineFeedback.Start(pPointPt);
                            dToltalLength = 0;
                        }
                        // 如果追踪线对象不为空，则添加当前鼠标点
                        else
                        {
                            pNewLineFeedback.AddPoint(pPointPt);
                        }
                        if (dSegmentLength != 0)
                        {
                            dToltalLength = dToltalLength + dSegmentLength;
                        }
                        break;
                    #endregion
                    #region 面积量算
                    case "MeasureArea":
                        if (pNewPolygonFeedback == null)
                        {
                            // 实例化追踪面对象
                            pNewPolygonFeedback = new NewPolygonFeedback();
                            pNewPolygonFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);
                            // 开始绘制多边形
                            pNewPolygonFeedback.Start(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        else
                        {
                            pNewPolygonFeedback.AddPoint(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        break;
                    #endregion
                    #region 区域(局部)导出
                    case "ExportRegion":
                        axMapControl1.ActiveView.GraphicsContainer.DeleteAllElements();
                        axMapControl1.ActiveView.Refresh();
                        IPolygon pPolygon = DrawPolygon(axMapControl1);
                        if (pPolygon == null)
                        {
                            return;
                        }

                        ExportMap.AddElement(pPolygon, axMapControl1.ActiveView);
                        if (frmExpMap == null || frmExpMap.IsDisposed)
                        {
                            frmExpMap = new FromExportMap(axMapControl1);
                        }
                        frmExpMap.IsRegion = true;
                        frmExpMap.GetGeometry = pPolygon as IGeometry;
                        frmExpMap.Show();
                        frmExpMap.Activate();
                        break;
                    #endregion
                    #region 要素选择
                    case "SelFeature":
                        IEnvelope pEnv = axMapControl1.TrackRectangle();
                        IGeometry pGeo = pEnv as IGeometry;
                        if (pEnv.IsEmpty == true)
                        {
                            tagRECT r;
                            r.left = e.x - 5;
                            r.top = e.y - 5;
                            r.right = e.x + 5;
                            r.bottom = e.y + 5;
                            pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(pEnv, ref r, 4);
                            pEnv.SpatialReference = pActiveView.FocusMap.SpatialReference;
                        }
                        pGeo = pEnv as IGeometry;
                        axMapControl1.Map.SelectByShape(pGeo, null, false);
                        axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        break;
                    #endregion
                    default:
                        break;
                }
            }
            else if (e.button == 2)
            {
                pMouseOperate = "";
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            //IActiveView pActiveView = axMapControl1.ActiveView;
            //IEnvelope env = new EnvelopeClass();
            //IGeometry pGeo = env as IGeometry;
            //if (env.IsEmpty == true)
            //{
            //    tagRECT r;
            //    r.left = e.x - 5;
            //    r.top = e.y - 5;
            //    r.right = e.x + 5;
            //    r.bottom = e.y + 5;
            //    pActiveView.ScreenDisplay.DisplayTransformation.TransformRect(env, ref r, 4);

            //}
            //pGeo = env as IGeometry;
            //axMapControl1.Map.SelectByShape(pGeo, null, false);
            //axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        private IPolygon DrawPolygon(AxMapControl mapCtrl)
        {
            IGeometry pGeometry = null;
            if (mapCtrl == null)
            {
                return null;
            }
            IRubberBand rb = new RubberPolygonClass();
            pGeometry = rb.TrackNew(mapCtrl.ActiveView.ScreenDisplay, null);
            return pGeometry as IPolygon;
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            Synchronize_axMapControl2();    // 主控件与鹰眼控件数据同步
        }

        private void Synchronize_axMapControl2()
        {
            if (axMapControl2.LayerCount > 0)
            {
                axMapControl2.ClearLayers();
            }

            // 鹰眼控件坐标系与主控件坐标系保持一致
            axMapControl2.SpatialReference = axMapControl1.SpatialReference;
            // 遍历主控件图层
            for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                ILayer pLayer = axMapControl1.get_Layer(i);

                // +
                if (pLayer == null || !pLayer.Visible)
                {
                    continue;
                }

                if (pLayer is IGroupLayer || pLayer is ICompositeLayer)
                {
                    ICompositeLayer pCompositeLayer = pLayer as ICompositeLayer;
                    for (int j = pCompositeLayer.Count - 1; j >= 0; j--)
                    {
                        ILayer pSubLayer = pCompositeLayer.get_Layer(j);

                        // +
                        if (pSubLayer == null || !pSubLayer.Visible)
                        {
                            continue;
                        }

                        // 支持栅格图层及过滤点图层
                        if (pSubLayer is IRasterLayer)
                        {
                            axMapControl2.AddLayer(pSubLayer);
                            continue;
                        }

                        IFeatureLayer pFeatureLayer = pSubLayer as IFeatureLayer;
                        // +
                        //if (pFeatureLayer.FeatureClass.ShapeType!=esriGeometryType.esriGeometryPoint&&pFeatureLayer.FeatureClass.ShapeType!=esriGeometryType.esriGeometryMultipoint)
                        //if (pFeatureLayer != null && pFeatureLayer.FeatureClass != null &&
                        //    pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint &&
                        //    pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                        if (pFeatureLayer != null && pFeatureLayer.FeatureClass != null)
                        {
                            axMapControl2.AddLayer(pSubLayer);
                        }
                    }
                }
                // 非图层组
                else
                {
                    // +
                    if (pLayer is IRasterLayer)
                    {
                        axMapControl2.AddLayer(pLayer);
                        continue;
                    }

                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    // repair
                    //if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                    //if (pFeatureLayer != null && pFeatureLayer.FeatureClass != null &&
                    //    pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint &&
                    //    pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                    if (pFeatureLayer != null && pFeatureLayer.FeatureClass != null)
                    {
                        axMapControl2.AddLayer(pLayer);
                    }
                }
            }
            axMapControl2.Extent = axMapControl1.FullExtent;
            pEnv = axMapControl1.Extent as IEnvelope;
            DrawRectangle(pEnv);
            axMapControl2.ActiveView.Refresh();
        }


        private void DrawRectangle(IEnvelope pEnv)
        {
            // 清除鹰眼控件的图形元素
            IGraphicsContainer pGraCon = axMapControl2.Map as IGraphicsContainer;
            pGraCon.DeleteAllElements();
            IActiveView pActiveView = pGraCon as IActiveView;
            // 得到当前视图范围
            IRectangleElement pRectEle = new RectangleElementClass();
            IElement pEle = pRectEle as IElement;
            pEle.Geometry = pEnv;
            // 设置矩形框
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine = new SimpleLineSymbolClass();
            pOutLine.Color = pColor;
            pOutLine.Width = 2;

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutLine;

            // 向鹰眼控件中添加矩形框
            IFillShapeElement pFillShpEle = pEle as IFillShapeElement;
            pFillShpEle.Symbol = pFillSymbol;
            pGraCon.AddElement((IElement)pFillShpEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IRgbColor GetRgbColor(int red, int green, int blue)
        {
            IRgbColor pColor = null;
            pColor = new RgbColor();
            pColor.Red = red;
            pColor.Green = green;
            pColor.Blue = blue;
            return pColor;
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            // 有图层数的情况
            if (axMapControl2.Map.LayerCount > 0)
            {
                // 判断指针落在鹰眼的矩形框中，标记可移动
                if (e.button == 1)
                {
                    if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
                    {
                        bCanDrag = true;
                    }
                    pMoveRectPoint = new PointClass();
                    pMoveRectPoint.PutCoords(e.mapX, e.mapY);    // 记录点击的第一个点的坐标
                }
                // 按下鼠标右键绘制矩形框
                if (e.button == 2)
                {
                    IEnvelope pEnvelope = axMapControl2.TrackRectangle();
                    if (pEnvelope == null || pEnvelope.IsEmpty)
                    {
                        return; // 空矩形不处理，避免空几何报错
                    }
                    IPoint pTempoint = new PointClass();

                    pTempoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 2, pEnvelope.YMin + pEnvelope.Height / 2);
                    axMapControl1.Extent = pEnvelope;
                    // 矩形框的高宽和数据视图的高宽不一定成正比，这里做一个中心调整
                    axMapControl1.CenterAt(pTempoint);
                }
            }

        }

        // 移动矩形框
        private void axMapControl2_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {

            // 改

            if (pEnv == null || pEnv.IsEmpty)
            {
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
                return;
            }

            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                // 如果鼠标移动到矩形框中，鼠标换成小手，表示可以拖动
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerHand;
                // 如果在内部按下鼠标右键，将鼠标演示设置为默认样式
                if (e.button == 2)
                {
                    axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else
            {
                // 在其他位置将鼠标设置为默认样式
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            if (bCanDrag)
            {
                double dx, dy;      // 记录鼠标移动位置
                dx = e.mapX - pMoveRectPoint.X;
                dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(dx, dy);        // 根据偏移量更改pEnv位置
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                axMapControl1.Extent = pEnv;
            }
        }

        private void axMapControl2_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && pMoveRectPoint != null)
            {
                if (e.mapX == pMoveRectPoint.X && e.mapY == pMoveRectPoint.Y)
                {
                    axMapControl1.CenterAt(pMoveRectPoint);
                }
                bCanDrag = false;
            }
        }

        // 绘制矩形框
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            // 得到当前视图范围
            pEnv = (IEnvelope)e.newEnvelope;
            DrawRectangle(pEnv);
        }
        private void CopyToPageLayout()
        {
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            object copiedMap = pObjectCopy.Copy(axMapControl1.Map);
            object copyToPage = axPageLayoutControl1.ActiveView.FocusMap;
            pObjectCopy.Overwrite(copiedMap, ref copyToPage);
            //axPageLayoutControl1.ActiveView.Refresh();
            // 仅局部刷新，避免触发全量重绘导致布局视图闪烁
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axPageLayoutControl1_OnAfterScreenDraw(object sender, IPageLayoutControlEvents_OnAfterScreenDrawEvent e)
        {
            //// 获取PageLayout的当前视图
            //IActiveView pActiveview = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            //// 显示转换
            //IDisplayTransformation pDTF = pActiveview.ScreenDisplay.DisplayTransformation;
            //// 设置范围
            //pDTF.VisibleBounds = axMapControl1.Extent;
            //axPageLayoutControl1.ActiveView.Refresh();
            //CopyToPageLayout();

            SyncPageLayout(false);
            //if (isPageLayoutSyncing)
            //{
            //    return;
            //}

            //try
            //{
            //    isPageLayoutSyncing = true;

            //    // 获取PageLayout的当前视图
            //    IActiveView pActiveview = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            //    // 显示转换
            //    IDisplayTransformation pDTF = pActiveview.ScreenDisplay.DisplayTransformation;
            //    // 设置范围
            //    pDTF.VisibleBounds = axMapControl1.Extent;

            //    CopyToPageLayout();

            //    // 避免再次触发全量刷新导致闪烁
            //    pActiveview.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //}
            //finally
            //{
            //    isPageLayoutSyncing = false;
            //}
        }

        private bool EnvelopesEqual(IEnvelope a, IEnvelope b, double tol = 1e-6)
        {
            if (a == null || b == null)
            {
                return false;
            }

            return Math.Abs(a.XMin - b.XMin) < tol &&
                   Math.Abs(a.YMin - b.YMin) < tol &&
                   Math.Abs(a.XMax - b.XMax) < tol &&
                   Math.Abs(a.YMax - b.YMax) < tol;
        }

        private void SyncPageLayout(bool force)
        {
            if (isPageLayoutSyncing)
            {
                return;
            }

            try
            {
                isPageLayoutSyncing = true;

                IActiveView pActiveview = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
                IDisplayTransformation pDTF = pActiveview.ScreenDisplay.DisplayTransformation;
                IEnvelope currentEnv = (IEnvelope)((IClone)axMapControl1.Extent).Clone();

                if (!force && lastPageLayoutEnv != null && EnvelopesEqual(lastPageLayoutEnv, currentEnv))
                {
                    return;
                }

                lastPageLayoutEnv = currentEnv;
                pDTF.VisibleBounds = currentEnv;

                CopyToPageLayout();

                pActiveview.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            finally
            {
                isPageLayoutSyncing = false;
            }
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            // +
            pMoveLayer = null;
            toIndex = -1;

            if (e.button == 1)
            {
                esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap pMap = null;
                object unk = null;
                object data = null;
                ILayer pLayer = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                if (pLayer == null)
                {
                    return;
                }
                // 改
                if (pMoveRectPoint == null)
                {
                    pMoveRectPoint = new PointClass();
                }
                pMoveRectPoint.PutCoords(e.x, e.y);
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (pLayer is IAnnotationSublayer)
                    {
                        return;
                    }
                    else
                    {
                        pMoveLayer = pLayer;
                    }
                }
            }
            if (e.button == 2)
            {
                esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap pMap = null;
                ILayer pLayer = null;
                object unk = null;
                object data = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                pTocLayer = pLayer;
                pTocFeatureLayer = pLayer as IFeatureLayer;
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer && pTocLayer != null)
                {
                    bool isFeature = pTocFeatureLayer != null;
                    // 
                    btnAttribute.Enabled = isFeature;

                    btnLayerSel.Enabled = isFeature && !pTocFeatureLayer.Selectable;
                    btnLayerUnSel.Enabled = isFeature && pTocFeatureLayer.Selectable;
                    if (ContextMenuStrip == null)
                    {
                        ContextMenuStrip = contextMenuStrip1 ?? new ContextMenuStrip();
                    }
                    // 改
                    //pTocFeatureLayer = pLayer as IFeatureLayer;
                    //// pTocFeatureLayer = (IFeatureLayer)pLayer;
                    //if (pItem==esriTOCControlItem.esriTOCControlItemLayer&&pTocFeatureLayer!=null)
                    //{
                    //    btnLayerSel.Enabled = !pTocFeatureLayer.Selectable;
                    //    btnLayerUnSel.Enabled = pTocFeatureLayer.Selectable;

                    //    if (ContextMenuStrip == null)
                    //    {
                    //        ContextMenuStrip = contextMenuStrip1 ?? new ContextMenuStrip();
                    //    }
                    // 弹出右键菜单
                    ContextMenuStrip.Show(Control.MousePosition);
                }
            }
        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            // +
            toIndex = -1;

            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pMap = null;
            object unk = null;
            object data = null;
            ILayer pLayer = null;
            axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
            IMap focusMap = axMapControl1.ActiveView.FocusMap;
            
            // +
            // 若只是点击勾选/取消可见性（无拖拽），立即同步 Map/Overview/PageLayout
            if (pLayer != null && e.button == 1)
            {
                bool isClickOnly = true;
                if (pMoveRectPoint != null)
                {
                    double dx = Math.Abs(e.x - pMoveRectPoint.X);
                    double dy = Math.Abs(e.y - pMoveRectPoint.Y);
                    isClickOnly = dx < 2 && dy < 2;
                }

                if (isClickOnly)
                {
                    // 延后一帧再刷新，确保 TOC 内部已更新可见性
                    BeginInvoke(new Action(() =>
                    {
                        axMapControl1.ActiveView.Refresh();
                        Synchronize_axMapControl2();
                        SyncPageLayout(true);
                    }));
                    pMoveLayer = null;
                    toIndex = -1;
                    return;
                }
            }


            if (pMoveLayer == null || focusMap == null || focusMap.LayerCount == 0)
            {
                // 处理仅勾选/取消可见性时的同步
                if (pLayer != null && pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    axMapControl1.ActiveView.Refresh();
                    Synchronize_axMapControl2();
                    SyncPageLayout(true);
                }
                // +
                pMoveLayer = null;
                return; // 避免 MoveLayer 抛“值不在预期范围内”
            }

            // +
            // 非图层节点（例如 Heading）直接跳过，避免 MoveLayer 抛异常
            if (pItem != esriTOCControlItem.esriTOCControlItemLayer &&
                pItem != esriTOCControlItem.esriTOCControlItemNone &&
                pItem != esriTOCControlItem.esriTOCControlItemMap)
            {
                pMoveLayer = null;
                return;
            }
            // +
            if (pItem == esriTOCControlItem.esriTOCControlItemLayer || pLayer != null)
            {
                if (pMoveLayer != pLayer)
                {
                    // repair
                    ILayer pTempLayer;
                    for (int i = 0; i < focusMap.LayerCount; i++)
                    {
                        pTempLayer = focusMap.get_Layer(i);
                        if (pTempLayer == pLayer)
                        {
                            toIndex = i;
                        }

                    }
                }
            }
            else if (pItem == esriTOCControlItem.esriTOCControlItemNone)
            {
                toIndex = focusMap.LayerCount - 1;
            }
            else if (pItem == esriTOCControlItem.esriTOCControlItemMap)
            {
                toIndex = 0;
            }

            // +
            // 防御：索引无效则不执行移动
            if (pMoveLayer == null || toIndex < 0 || toIndex >= focusMap.LayerCount)
            {
                if (pLayer != null && pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    axMapControl1.ActiveView.Refresh();
                    Synchronize_axMapControl2();
                    SyncPageLayout(true);
                }
                pMoveLayer = null;
                toIndex = -1;
                return;
            }
            // +
            focusMap.MoveLayer(pMoveLayer, toIndex);
            axMapControl1.ActiveView.Refresh();
            axTOCControl1.Update();

            // 图层可见性或顺序变化后同步鹰眼
            Synchronize_axMapControl2();

            // +
            // 同步 PageLayout 显示层次/可见性
            SyncPageLayout(true);
            //+
            pMoveLayer = null;
            toIndex = -1;
        }

        // 右键菜单：属性表
        private void btnAttribute_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null)
            {
                return;
            }

            if (frmAtrribute == null || frmAtrribute.IsDisposed)
            {
                frmAtrribute = new FormAttribute();
            }
            frmAtrribute.CurFeatureLayer = pTocFeatureLayer;
            frmAtrribute.InitUI();
            frmAtrribute.ShowDialog();
        }

        // 右键菜单：缩放至图层
        private void btnZoomToLayer_Click(object sender, EventArgs e)
        {
            //if (pTocFeatureLayer == null)
            if (pTocLayer == null)
            {
                return;
            }


            //(axMapControl1.Map as IActiveView).Extent = pTocFeatureLayer.AreaOfInterest;
            (axMapControl1.Map as IActiveView).Extent = pTocLayer.AreaOfInterest;
            (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        // 右键菜单：移除图层
        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {
            //if (pTocFeatureLayer == null)
            if (pTocLayer == null)
            {
                return;
            }

            //DialogResult result = MessageBox.Show("是否删除?" + pTocFeatureLayer.Name, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            DialogResult result = MessageBox.Show("是否删除?" + pTocLayer.Name, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                //axMapControl1.Map.DeleteLayer(pTocFeatureLayer);
                axMapControl1.Map.DeleteLayer(pTocLayer);
            }
            axMapControl1.ActiveView.Refresh();

            // +
            // 移除图层后同步鹰眼控件
            Synchronize_axMapControl2();

            // +
            // 强制同步 PageLayout 图层状态
            SyncPageLayout(true);
        }

        // 右键菜单：图层要素可选
        private void btnLayerSel_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null)
            {
                return;
            }
            pTocFeatureLayer.Selectable = true;
            btnLayerSel.Enabled = !btnLayerSel.Enabled;

        }

        // 右键菜单：图层要素不可选
        private void btnLayerUnSel_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null)
            {
                return;
            }
            pTocFeatureLayer.Selectable = false;
            btnLayerUnSel.Enabled = !btnLayerSel.Enabled;
        }

        private void chkCustomize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomize.Checked == false)
            {
                cd.CloseDialog();
            }
            else
            {
                cd.StartDialog(axToolbarControl1.hWnd);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            string SMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            barCooTxt.Text = string.Format("当前坐标为：X={0::#.###}{2},Y={1:#.###}{2}", e.mapX, e.mapY, SMapUnits);
            pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            #region 长度量算

            if (pMouseOperate == "MeasureLength")
            {
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.MoveTo(pMovePt);
                }
                double deltaX = 0;      // 两点之间X差值
                double deltaY = 0;      // 两点之间Y差值
                if ((pPointPt != null) && (pNewLineFeedback != null))
                {
                    deltaX = pMovePt.X - pPointPt.X;
                    deltaY = pMovePt.Y - pPointPt.Y;
                    dSegmentLength = Math.Round(Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY)), 3);
                    dToltalLength = dToltalLength + dSegmentLength;
                    if (frmMeasureResult != null)
                    {
                        frmMeasureResult.lblMeasureResult.Text = String.Format("当前线段长度：{0:.###}{1};\r\n总长度为：{2:.###}{1}", dSegmentLength, SMapUnits, dToltalLength);
                        dToltalLength = dToltalLength - dSegmentLength;    // 鼠标移动到新点重新开始计算
                    }
                    frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                }
            }
            #endregion
            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.MoveTo(pMovePt);
                }
                IPointCollection pPointCol = new Polygon();
                IPolygon pPolygon = new PolygonClass();
                IGeometry pGeo = null;
                ITopologicalOperator pTopo = null;
                for (int i = 0; i < pAreaPointCol.PointCount; i++)
                {
                    pPointCol.AddPoint(pAreaPointCol.get_Point(i), ref missing, ref missing);
                }
                pPointCol.AddPoint(pMovePt, ref missing, ref missing);
                if (pPointCol.PointCount < 3)
                {
                    return;
                }
                pPolygon = pPointCol as IPolygon;
                if (pPolygon != null)
                {
                    pPolygon.Close();
                    pGeo = pPolygon as IGeometry;
                    pTopo = pGeo as ITopologicalOperator;
                    // 使几何图形的拓扑关系正确
                    pTopo.Simplify();
                    pGeo.Project(axMapControl1.Map.SpatialReference);
                    IArea pArea = pGeo as IArea;
                    frmMeasureResult.lblMeasureResult.Text = String.Format("总面积为：{0:.###}平方{1};\r\n总长度为：{2:.###}{1}", pArea.Area, SMapUnits, pPolygon.Length);
                    pPolygon = null;
                }
            }
            #endregion
        }

        // 测量结果窗口关闭响应事件
        private void frmMeasureResult_frmClosed()
        {
            // 清空线对象
            if (pNewLineFeedback != null)
            {
                pNewLineFeedback.Stop();
                pNewLineFeedback = null;
            }
            // 清空面对象
            if (pNewPolygonFeedback != null)
            {
                pNewPolygonFeedback.Stop();
                pNewPolygonFeedback = null;
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);        // 清空集中所有点
            }
            // 清空量算画的线、面对象
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            // 结束量算功能
            pMouseOperate = string.Empty;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            string SMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            #region 长度量算
            if (pMouseOperate == "MeasureLength")
            {
                if (frmMeasureResult != null)
                {
                    frmMeasureResult.lblMeasureResult.Text = "线段总长度为：" + dToltalLength + SMapUnits;
                }
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.Stop();
                    pNewLineFeedback = null;
                    // 清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                dToltalLength = 0;
                dSegmentLength = 0;
            }
            #endregion
            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.Stop();
                    pNewPolygonFeedback = null;
                    // 清空所画的面对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);

                }
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);        // 清空点集中所有点
            }
            #endregion
        }

        private string GetMapUnit(esriUnits esriUnits)
        {
            string SMapUnits = string.Empty;
            #region 单位度量
            switch (esriUnits)
            {
                case esriUnits.esriCentimeters:
                    SMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    SMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    SMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    SMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    SMapUnits = "英尺";
                    break;
                case esriUnits.esriKilometers:
                    SMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    SMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    SMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    SMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    SMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    SMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    SMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    SMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    SMapUnits = "码";
                    break;
                default:
                    break;
            }
            #endregion
            return SMapUnits;
        }

        // 在GeoDatabase中创建要素类
        public IFeatureClass CreateFeatureClass2DB(string featureclassname, UID classExtensionUID, IFeatureWorkspace featureworkspace)
        {
            // 创建字段集合
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            // 添加object ID到字段集合中，创建要素类时必须包括该字段
            IField oidField = new FieldClass();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);
            // 为要素类创建几何定义和空间参考
            IGeometryDef geometryDef = new GeometryDefClass();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            geometryDefEdit.SpatialReference_2 = spatialReference;
            // 将几何字段添加到字段集合
            IField geometryField = new FieldClass();
            IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
            geometryFieldEdit.Name_2 = "Shape";
            geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            geometryFieldEdit.GeometryDef_2 = geometryDef;
            fieldsEdit.AddField(geometryField);
            // 创建字段“Name”
            IField nameField = new FieldClass();
            IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
            nameFieldEdit.Name_2 = "Name";
            nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            nameFieldEdit.Length_2 = 20;
            fieldsEdit.AddField(nameField);
            IFeatureClass featureClass = featureworkspace.CreateFeatureClass(featureclassname, fields, null, classExtensionUID, esriFeatureType.esriFTSimple, "Shape", "");
            return featureClass;

        }

        private void 局部导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            pMouseOperate = "ExportRegion";

        }

        private void 全局导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmExpMap == null || frmExpMap.IsDisposed)
            {
                frmExpMap = new FromExportMap(axMapControl1);
            }
            frmExpMap.IsRegion = false;
            frmExpMap.GetGeometry = axMapControl1.ActiveView.Extent;
            frmExpMap.Show();
            frmExpMap.Activate();
        }

        private void 距离量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "MeasureLength";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();
                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                frmMeasureResult.lblMeasureResult.Text = " ";
                frmMeasureResult.Text = "距离量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }

        private void 面积量测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "MeasureArea";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            if (frmMeasureResult == null || frmMeasureResult.IsDisposed)
            {
                frmMeasureResult = new FormMeasureResult();
                frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmClosed);
                frmMeasureResult.lblMeasureResult.Text = " ";
                frmMeasureResult.Text = "面积量测";
                frmMeasureResult.Show();
            }
            else
            {
                frmMeasureResult.Activate();
            }
        }

        private void 地图选择集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 实例化地图选择集窗体
            FormSelection formSelection = new FormSelection();
            // 将当前主窗体中的MapControl控件中的Map对象赋给FormSelection窗体的CurrentMap属性
            formSelection.CurrentMap = axMapControl1.Map;
            // 显示地图选择集窗体
            formSelection.Show();
        }

        private void 统计选择集ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新创建统计窗体 
            FormStatistics formStatistics = new FormStatistics();
            //将当前主窗体中MapControl控件中的Map对象赋值给FormStatistics窗体的CurrentMap属性 
            formStatistics.CurrentMap = axMapControl1.Map;
            //显示统计窗体 
            formStatistics.Show();
        }

        private void tsmStartEdit_Click(object sender, EventArgs e)
        {
            try
            {
                pMap = axMapControl1.Map;
                pActiveView = pMap as IActiveView;
                plstLayers = MapManager.GetLayers(pMap);
                if (plstLayers == null || plstLayers.Count == 0)
                {
                    MessageBox.Show("请加载编辑图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                pMap.ClearSelection();
                pActiveView.Refresh();
                InitComboBox(plstLayers);
                ChangeButtonState(true);
                // 如果编辑已经开始，则直接退出
                if (pEngineEditor.EditState != esriEngineEditState.esriEngineStateNotEditing)
                {
                    return;
                }
                if (pCurrentLyr == null)
                {
                    return;
                }
                // 获取当前编辑图层工作空间
                IDataset pDataSet = pCurrentLyr.FeatureClass as IDataset;
                IWorkspace pWs = pDataSet.Workspace;
                // 设置编辑模式，如果是ArcSDE采用版本模式
                if (pWs.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeVersioned;
                }
                else
                {
                    pEngineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeNonVersioned;
                }
                // 设置编辑任务
                pEngineEditTask = pEngineEditor.GetTaskByUniqueName("ControlToolsEditing_CreateNewFeatureTask");
                pEngineEditor.CurrentTask = pEngineEditTask;       // 设置编辑任务
                pEngineEditor.EnableUndoRedo(true);        //是否可以进行撤销、恢复操作
                pEngineEditor.StartEditing(pWs, pMap);         //开始编辑操作 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmbSelLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sLyrName = cmbSelLayer.SelectedItem.ToString();
                pCurrentLyr = MapManager.GetLayerByName(pMap, sLyrName) as IFeatureLayer;
                // 设置编辑目标图层
                pEngineEditLayers.SetTargetLayer(pCurrentLyr, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_saveEditCom = new SaveEditCommandClass();
                m_saveEditCom.OnCreate(axMapControl1.Object);
                m_saveEditCom.OnClick();
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;

                // +
                // 保存编辑后同步鹰眼控件
                Synchronize_axMapControl2();

                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {

            }
        }

        private void tsmEndEdit_Click(object sender, EventArgs e)
        {
            try
            {
                axMapControl1.CurrentTool = null;
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                ChangeButtonState(false);
                ICommand m_stopEditCom = new StopEditCommandClass();
                m_stopEditCom.OnCreate(axMapControl1.Object);
                m_stopEditCom.OnClick();

                // +
                // 结束编辑后同步鹰眼控件
                Synchronize_axMapControl2();
            }
            catch (Exception ex)
            {

            }
        }

        private void tsmAddFeature_Click(object sender, EventArgs e)
        {
            try
            {
                ICommand m_CreateFeatTool = new CreateFeatureToolClass();
                m_CreateFeatTool.OnCreate(axMapControl1.Object);
                m_CreateFeatTool.OnClick();
                axMapControl1.CurrentTool = m_CreateFeatTool as ITool;
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            }
            catch (Exception ex)
            {

            }
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
            if (e.button == 1)
            {
                if (itemType == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    // 取得图例 
                    ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);
                    // 创建符号选择器SymbolSelector实例 
                    frmSymbolSelector SymbolSelectorFrm = new frmSymbolSelector(pLegendClass, layer);
                    if (SymbolSelectorFrm.ShowDialog() == DialogResult.OK)
                    {
                        // 局部更新主Map控件 
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        pLegendClass.Symbol = SymbolSelectorFrm.pSymbol;        // 设置新的符号             
                        this.axMapControl1.ActiveView.Refresh();         // 更新主Map控件和图层控件 
                        this.axMapControl1.Refresh();
                    }
                }
            }
        }

        private void UniqueValueRenderer(IFeatureLayer pFeatLyr, string sFieldName)
        {
            IGeoFeatureLayer pGeoFeatLyr = pFeatLyr as IGeoFeatureLayer;
            ITable pTable = pFeatLyr as ITable;
            IUniqueValueRenderer pUniqueValueRender = new UniqueValueRendererClass();
            int intFieldNumber = pTable.FindField(sFieldName);
            // 设置唯一值符号化的关键字段为一个 
            pUniqueValueRender.FieldCount = 1;
            // 设置唯一值符号化的第一个关键字段 
            pUniqueValueRender.set_Field(0, sFieldName);
            IRandomColorRamp pRandColorRamp = new RandomColorRampClass();
            pRandColorRamp.StartHue = 0; pRandColorRamp.EndHue = 360;
            pRandColorRamp.MinValue = 0; pRandColorRamp.MaxValue = 100;
            pRandColorRamp.MinSaturation = 15; pRandColorRamp.MaxSaturation = 30;
            // 根据渲染字段的值的个数，设置一组随机颜色，如某一字段有5个值，则创建5个随机颜色与之匹配 
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pRandColorRamp.Size = pFeatLyr.FeatureClass.FeatureCount(pQueryFilter);
            bool bSuccess = false;
            pRandColorRamp.CreateRamp(out bSuccess);
            IEnumColors pEnumRamp = pRandColorRamp.Colors;//颜色集合 
            IColor pNextUniqueColor = null;
            // 查询字段的值 
            pQueryFilter = new QueryFilterClass(); pQueryFilter.AddField(sFieldName);
            ICursor pCursor = pTable.Search(pQueryFilter, true);
            IRow pNextRow = pCursor.NextRow(); //遍历每一行 
            object codeValue = null; IRowBuffer pNextRowBuffer = null;
            while (pNextRow != null)
            {
                pNextRowBuffer = pNextRow as IRowBuffer;
                codeValue = pNextRowBuffer.get_Value(intFieldNumber);//获取渲染字段的每一个值 
                pNextUniqueColor = pEnumRamp.Next();
                if (pNextUniqueColor == null)
                {
                    pEnumRamp.Reset(); pNextUniqueColor = pEnumRamp.Next();
                }
                IFillSymbol pFillSymbol = null;
                ILineSymbol pLineSymbol;
                IMarkerSymbol pMarkerSymbol;
                switch (pGeoFeatLyr.FeatureClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryAny:
                        break;
                    case esriGeometryType.esriGeometryBag:
                        break;
                    case esriGeometryType.esriGeometryBezier3Curve:
                        break;
                    case esriGeometryType.esriGeometryCircularArc:
                        break;
                    case esriGeometryType.esriGeometryEllipticArc:
                        break;
                    case esriGeometryType.esriGeometryEnvelope:
                        break;
                    case esriGeometryType.esriGeometryLine:
                        break;
                    case esriGeometryType.esriGeometryMultiPatch:
                        break;
                    case esriGeometryType.esriGeometryMultipoint:
                        break;
                    case esriGeometryType.esriGeometryNull:
                        break;
                    case esriGeometryType.esriGeometryPath:
                        break;
                    case esriGeometryType.esriGeometryPoint:
                        pMarkerSymbol = new SimpleMarkerSymbolClass();
                        pMarkerSymbol.Color = pNextUniqueColor;
                        // 添加渲染字段的值和渲染样式
                        pUniqueValueRender.AddValue(codeValue.ToString(), "", pMarkerSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        pFillSymbol = new SimpleFillSymbolClass();
                        pFillSymbol.Color = pNextUniqueColor;
                        // 添加渲染字段的值和渲染样式 
                        pUniqueValueRender.AddValue(codeValue.ToString(), "", pFillSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        break;
                    case esriGeometryType.esriGeometryPolyline:
                        pLineSymbol = new SimpleLineSymbolClass();
                        pLineSymbol.Color = pNextUniqueColor;
                        // 添加渲染字段的值和渲染样式
                        pUniqueValueRender.AddValue(codeValue.ToString(), "", pLineSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        break;
                    case esriGeometryType.esriGeometryRay:
                        break;
                    case esriGeometryType.esriGeometryRing:
                        break;
                    case esriGeometryType.esriGeometrySphere:
                        break;
                    case esriGeometryType.esriGeometryTriangleFan:
                        break;
                    case esriGeometryType.esriGeometryTriangleStrip:
                        break;
                    case esriGeometryType.esriGeometryTriangles:
                        break;
                    default:
                        break;
                }
            }
            pGeoFeatLyr.Renderer = pUniqueValueRender as IFeatureRenderer;
            axMapControl1.Refresh();
            axTOCControl1.Update();
        }

        private void axToolbarControl1_OnMouseDown(object sender, IToolbarControlEvents_OnMouseDownEvent e)
        {

        }

        private void 分级符号化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null || pTocFeatureLayer.FeatureClass == null)
            {
                MessageBox.Show("请在目录中选择一个要素图层。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            esriGeometryType geometryType = pTocFeatureLayer.FeatureClass.ShapeType;
            if (geometryType != esriGeometryType.esriGeometryPolygon && geometryType != esriGeometryType.esriGeometryPolyline)
            {
                MessageBox.Show("仅支持面或线要素图层的分级符号化。", "分级符号化", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormClassifiedRenderer frm = new FormClassifiedRenderer(axMapControl1, pTocFeatureLayer);
            frm.ShowDialog();
            axTOCControl1.Update();
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void 标题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInsertMode(InsertMode.Title);
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            if (_currentInsertMode == InsertMode.None)
            {
                return;
            }

            IPoint mapPoint = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            try
            {
                switch (_currentInsertMode)
                {
                    case InsertMode.Title:
                        InsertTitleAt(mapPoint);
                        break;
                    case InsertMode.Text:
                        InsertTextAt(mapPoint);
                        break;
                    case InsertMode.Legend:
                        InsertLegendAt(mapPoint);
                        break;
                    case InsertMode.ScaleBar:
                        InsertScaleBarAt(mapPoint);
                        break;
                    case InsertMode.NorthArrow:
                        InsertNorthArrowAt(mapPoint);
                        break;
                }
            }
            finally
            {
                ResetInsertMode();
            }
        }

        private void InsertTitleAt(IPoint anchor)
        {
            using (var dlg = new FormAddTitle())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(dlg.TitleText))
                {
                    MessageBox.Show("标题内容不能为空", "插入标题", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ITextElement textElement = new TextElementClass();
                textElement.Text = dlg.TitleText;

                ITextSymbol textSymbol = new TextSymbolClass
                {
                    Color = CreateRgbColor(0, 0, 0),
                    Size = dlg.FontSize,
                    Font = new StdFontClass
                    {
                        Name = "黑体",
                        Size = (decimal)dlg.FontSize
                    } as IFontDisp
                };

                textElement.Symbol = textSymbol;

                IElement element = textElement as IElement;
                element.Geometry = anchor;

                AddElementToLayout(element);
            }
        }

        private void InsertTextAt(IPoint anchor)
        {
            using (var dlg = new FormAddText())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(dlg.TextContent))
                {
                    MessageBox.Show("文本内容不能为空", "插入文本", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ITextElement textElement = new TextElementClass();
                textElement.Text = dlg.TextContent;

                ITextSymbol textSymbol = new TextSymbolClass
                {
                    Color = CreateRgbColor(0, 0, 0),
                    Size = dlg.FontSize,
                    Font = new StdFontClass
                    {
                        Name = dlg.FontName,
                        Size = (decimal)dlg.FontSize
                    } as IFontDisp
                };

                textElement.Symbol = textSymbol;

                IElement element = textElement as IElement;
                element.Geometry = anchor;

                AddElementToLayout(element);
            }
        }

        private void InsertLegendAt(IPoint anchor)
        {
            IMapFrame mapFrame = GetMapFrame();
            if (mapFrame == null)
            {
                MessageBox.Show("未找到地图框架，无法添加图例。", "插入图例", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dlg = new FormAddLegend(mapFrame.Map))
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (dlg.SelectedLayers == null || dlg.SelectedLayers.Count == 0)
                {
                    MessageBox.Show("请至少选择一个图层用于图例。", "插入图例", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                IMapSurroundFrame surroundFrame = mapFrame.CreateSurroundFrame(new UIDClass { Value = "esriCarto.Legend" }, null);

                ILegend legend = surroundFrame != null ? surroundFrame.MapSurround as ILegend : null;
                if (legend == null)
                {
                    MessageBox.Show("创建图例失败。", "插入图例", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 先自动填充图例以获取默认设置
                legend.AutoAdd = false;
                legend.AutoReorder = false;
                legend.AutoVisibility = true;
                // 清除默认的图例项
                legend.ClearItems();

                foreach (ILayer layer in dlg.SelectedLayers)
                {
                    // 创建新的图例项并关联图层
                    ILegendItem legendItem = new HorizontalLegendItemClass();
                    legendItem.Layer = layer;
                    legendItem.ShowDescriptions = false;
                    //legendItem.ShowHeadings = false;
                    legendItem.ShowLabels = true;
                    legendItem.ShowLayerName = true;

                    // 设置图例项的列数
                    legendItem.Columns = 1;
                    legendItem.KeepTogether = true;

                    legend.AddItem(legendItem);
                }

                surroundFrame.MapFrame = mapFrame;

                IElement element = (IElement)surroundFrame;
                IEnvelope env = new EnvelopeClass();
                double width = 2.5;
                double height = 3.0;
                env.PutCoords(anchor.X, anchor.Y, anchor.X + width, anchor.Y + height);
                element.Geometry = env;

                AddElementToLayout(element);
            }
        }

        private void InsertScaleBarAt(IPoint anchor)
        {
            IMapFrame mapFrame = GetMapFrame();
            if (mapFrame == null)
            {
                MessageBox.Show("未找到地图框架，无法添加比例尺。", "插入比例尺", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 使用ScaleLineClass创建比例线样式
            IScaleBar scaleBar = new ScaleLineClass();
            scaleBar.Units = esriUnits.esriKilometers;
            scaleBar.UnitLabel = "km";
            scaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            scaleBar.Division = 1;           // 主刻度数量为1（两个刻度点：0和终点）
            scaleBar.Subdivisions = 1;       // 不需要细分
            scaleBar.DivisionsBeforeZero = 0; // 零点之前无刻度
            scaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisions;

            IMapSurroundFrame surroundFrame = new MapSurroundFrameClass
            {
                MapSurround = scaleBar as IMapSurround,
                MapFrame = mapFrame
            };

            IElement element = (IElement)surroundFrame;
            IEnvelope env = new EnvelopeClass();
            double width = 2.5;
            double height = 0.5;
            env.PutCoords(anchor.X, anchor.Y, anchor.X + width, anchor.Y + height);
            element.Geometry = env;

            AddElementToLayout(element);
        }

        private void InsertNorthArrowAt(IPoint anchor)
        {
            IMapFrame mapFrame = GetMapFrame();
            if (mapFrame == null)
            {
                MessageBox.Show("未找到地图框架，无法添加指北针。", "插入指北针", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IMarkerNorthArrow northArrow = new MarkerNorthArrowClass();
            INorthArrow arrow = northArrow as INorthArrow;
            if (arrow != null)
            {
                arrow.Color = CreateRgbColor(0, 0, 0);
            }

            IMapSurroundFrame surroundFrame = new MapSurroundFrameClass
            {
                MapSurround = northArrow as IMapSurround,
                MapFrame = mapFrame
            };

            IElement element = (IElement)surroundFrame;
            IEnvelope env = new EnvelopeClass();
            double size = 1.0;
            env.PutCoords(anchor.X, anchor.Y, anchor.X + size, anchor.Y + size);
            element.Geometry = env;

            AddElementToLayout(element);
        }

        private IMapFrame GetMapFrame()
        {
            IGraphicsContainer container = axPageLayoutControl1.ActiveView.GraphicsContainer;
            return container.FindFrame(axPageLayoutControl1.ActiveView.FocusMap) as IMapFrame;
        }

        private void AddElementToLayout(IElement element)
        {
            IGraphicsContainer container = axPageLayoutControl1.ActiveView.GraphicsContainer;
            container.AddElement(element, 0);
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IColor CreateRgbColor(byte red, byte green, byte blue)
        {
            return new RgbColorClass
            {
                Red = red,
                Green = green,
                Blue = blue
            };
        }

        private void 文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInsertMode(InsertMode.Text);
        }

        private void 比例尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInsertMode(InsertMode.ScaleBar);
        }

        private void 图例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInsertMode(InsertMode.Legend);
        }

        private void 指北针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginInsertMode(InsertMode.NorthArrow);
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPrint formPrint = new FormPrint(axPageLayoutControl1);
            formPrint.ShowDialog();
        }

    }
}
