using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;

namespace Symbology
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout, ArcScene/SceneControl
    /// or ArcGlobe/GlobeControl
    /// </summary>
    [Guid("b5b711c4-aef4-4f36-9ce8-416abf5a1d8d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Symbology.DotDensityRender")]
    public sealed class DotDensityRender : BaseCommand
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
            GMxCommands.Register(regKey);
            MxCommands.Register(regKey);
            SxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Unregister(regKey);
            MxCommands.Unregister(regKey);
            SxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper ;
        
        private IGlobeHookHelper m_globeHookHelper = null;
        private ISceneHookHelper m_sceneHookHelper = null;
         

        public DotDensityRender()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Symbol.Item"; //localizable text
            base.m_caption = "DotDenstiyRender";  //localizable text
            base.m_message = "DotDenstiyRender";
            base.m_toolTip = "点密度图";  //localizable text 
            base.m_name = "DotDenstiyRender";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            // Test the hook that calls this command and disable if nothing is valid
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }
            if (m_hookHelper == null)
            {
                //Can be scene or globe
                try
                {
                    m_sceneHookHelper = new SceneHookHelperClass();
                    m_sceneHookHelper.Hook = hook;
                    if (m_sceneHookHelper.ActiveViewer == null)
                    {
                        m_sceneHookHelper = null;
                    }
                }
                catch
                {
                    m_sceneHookHelper = null;
                }

                if (m_sceneHookHelper == null)
                {
                    //Can be globe
                    try
                    {
                        m_globeHookHelper = new GlobeHookHelperClass();
                        m_globeHookHelper.Hook = hook;
                        if (m_globeHookHelper.ActiveViewer == null)
                        {
                            m_globeHookHelper = null;
                        }
                    }
                    catch
                    {
                        m_globeHookHelper = null;
                    }
                }
            }

            if (m_globeHookHelper == null && m_sceneHookHelper == null && m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            //TODO: Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            try
            {
                // 确保 hook 与地图视图可用
                if (m_hookHelper == null || m_hookHelper.FocusMap == null || m_hookHelper.ActiveView == null)
                {
                    MessageBox.Show("当前没有可用的地图。");
                    return;
                }

                IActiveView pActiveView = m_hookHelper.ActiveView;      // 活动视图
                IMap pMap = m_hookHelper.FocusMap;                      // 地图

                // 确保有图层再取第一个
                if (pMap.LayerCount == 0)
                {
                    MessageBox.Show("当前地图没有图层。");
                    return;
                }

                MessageBox.Show(pMap.get_Layer(0).Name);
                IGeoFeatureLayer pGeoFeatureL = pMap.get_Layer(0) as IGeoFeatureLayer;      // 要素图层

                string strPopField = "drawValue";       // 字段
                IDotDensityRenderer pDotDensityRenderer = new DotDensityRendererClass();        // 渲染
                IRendererFields pRendererFields = (IRendererFields)pDotDensityRenderer;         // 渲染字段
                pRendererFields.AddField(strPopField, strPopField);
                IDotDensityFillSymbol pDotDensityFills = new DotDensityFillSymbolClass();
                pDotDensityFills.DotSize = 5;
                pDotDensityFills.Color = GetRGB(0, 0, 0);
                pDotDensityFills.BackgroundColor = GetRGB(239, 228, 190);
                ISymbolArray pSymbolArray = (ISymbolArray)pDotDensityFills;
                ISimpleMarkerSymbol pSimpleMarkerS = new SimpleMarkerSymbolClass();     // 点状 symbol
                pSimpleMarkerS.Style = esriSimpleMarkerStyle.esriSMSCircle;
                pSimpleMarkerS.Size = 5;
                pSimpleMarkerS.Color = GetRGB(128, 128, 255);
                pSymbolArray.AddSymbol((ISymbol)pSimpleMarkerS);
                pDotDensityRenderer.DotDensitySymbol = pDotDensityFills;
                pDotDensityRenderer.DotValue = 0.5;
                pDotDensityRenderer.CreateLegend();
                pGeoFeatureL.Renderer = (IFeatureRenderer)pDotDensityRenderer;
                pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        private IColor GetRGB(int Red, int Green, int Blue)
        {
            IRgbColor rgbcolor = new RgbColorClass();
            rgbcolor.Red = Red;
            rgbcolor.Green = Green;
            rgbcolor.Blue = Blue;
            return rgbcolor;
        }

        #endregion
    }
}
