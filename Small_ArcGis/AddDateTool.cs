using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Small_ArcGis
{
    /// <summary>
    /// Summary description for AddDateTool.
    /// </summary>
    [Guid("2ac94441-9201-440c-83e7-761ebacb4762")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Small_ArcGis.AddDateTool")]
    public sealed class AddDateTool : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper;
        AxToolbarControl toolbar1;
        AxPageLayoutControl pagelayout;

        public AddDateTool(AxToolbarControl axtoolbar,AxPageLayoutControl axpagelayout)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Custom Command"; //localizable text 
            base.m_caption = "AddDateTool";  //localizable text 
            base.m_message = "AddDateTool";  //localizable text
            base.m_toolTip = "AddDateTool";  //localizable text
            base.m_name = "AddDateTool";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
            // 传入控件参数
            toolbar1 = axtoolbar;
            pagelayout = axpagelayout;
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();
            m_hookHelper.Hook = hook;

            // TODO:  Add AddDateTool.OnCreate implementation
            // 设置伙伴控件
            toolbar1.SetBuddyControl(pagelayout);
        }
        public override bool Enabled
        {
            get
            {
                #region Reapair1
                //if (m_hookHelper.ActiveView!=null)
                if (m_hookHelper == null)
                {
                    return false;
                } 
                // 仅在布局视图下启用
                if (m_hookHelper.ActiveView is IPageLayout)
                #endregion
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add AddDateTool.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDateTool.OnMouseDown implementation
            base.OnMouseDown(Button, Shift, X, Y);
            #region Repair2_Add
            // 确保在布局视图下
            if (m_hookHelper.ActiveView == null || !(m_hookHelper.ActiveView is IPageLayout))
            {
                return;
            }
            // 获取活动视图
            IActiveView activeView = m_hookHelper.ActiveView;
            IGraphicsContainer graphicsContainer = activeView.GraphicsContainer;

            // 1. 删除已存在的日期元素
            graphicsContainer.Reset();
            IElement pElement = graphicsContainer.Next();
            System.Collections.ArrayList elementsToDelete = new System.Collections.ArrayList();
            while (pElement != null)
            {
                if (pElement is IElementProperties)
                {
                    IElementProperties pElePro = pElement as IElementProperties;
                    if (pElePro.Name == "DateElement")
                    {
                        elementsToDelete.Add(pElement);
                    }
                }
                pElement = graphicsContainer.Next();
            }

            foreach (IElement ele in elementsToDelete)
            {
                graphicsContainer.DeleteElement(ele);
            }
            #endregion

            // 创建新的文本元素
            ITextElement textElement = new TextElementClass();
            // 创建文本符号
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Size = 25;
            // 设置文本元素属性
            textElement.Symbol = textSymbol;
            textElement.Text = System.DateTime.Now.ToShortDateString();
            // 对IElement QI
            IElement element = textElement as IElement;
            #region Reapair3
            // 设置元素名称，以便下次识别删除
            IElementProperties elementProps = element as IElementProperties;
            elementProps.Name = "DateElement";

            // 创建点
            //IPoint point = new PointClass();
            //point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            IPoint point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            
            #endregion
            // 设置元素属性
            element.Geometry = point;
            #region Repair4
            // 增加元素到图形绘制容器
            //activeView.GraphicsContainer.AddElement(element, 0);
            graphicsContainer.AddElement(element, 0);
            #endregion

            // 刷新图形
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDateTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDateTool.OnMouseUp implementation
        }
        #endregion
    }
}
