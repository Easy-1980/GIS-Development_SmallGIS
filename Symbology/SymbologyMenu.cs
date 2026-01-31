using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.SystemUI;

namespace Symbology
{
    /// <summary>
    /// Summary description for SymbologyMenu.
    /// </summary>
    [Guid("261ee96f-b667-483e-970b-6dbdae47f12e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Symbology.SymbologyMenu")]
    public sealed class SymbologyMenu : BaseMenu
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
            ControlsMenus.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsMenus.Unregister(regKey);
        }

        #endregion
        #endregion
        // 构造函数
        public SymbologyMenu()
        {
            //
            // TODO: Define your menu here by adding items
            //            
            //AddItem("esriControls.ControlsMapZoomInFixedCommand");
            //BeginGroup(); //Separator
            //AddItem("{380FB31E-6C24-4F5C-B1DF-47F33586B885}"); //undo command
            //AddItem(new Guid("B0675372-0271-4680-9A2C-269B3F0C01E8")); //redo command
            AddItem("Symbology.SimpleRender");
            AddItem("Symbology.UniqueValueRender");
            AddItem("Symbology.ClassBreakRender");
            AddItem("Symbology.ProportionSymbol");
            AddItem("Symbology.DotDensityRender");
            AddItem("Symbology.BarChartRender");

        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "SymbologyMenu";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "SymbologyMenu";
            }
        }
        public int ItemCount
        {
            get { return 6; }
        }
        public void GetItemInfo(int pos, IItemDef itemDef)
        {
            switch (pos)
            {
                // 简单着色
                case 0: itemDef.ID = "Symbology.SimpleRender";
                    break;
                // 唯一值着色
                case 1: itemDef.ID = "Symbology.UniqueValueRender";
                    break;
                // 分级着色
                case 2: itemDef.ID = "Symbology.ClassBreakRender";
                    break;
                // 按比例着色
                case 3: itemDef.ID = "Symbology.ProportionSymbol";
                    break;
                // 点密度图
                case 4: itemDef.ID = "Symbology.DotDensityRender";
                    break;
                // 饼图
                case 5: itemDef.ID = "Symbology.BarChartRender";
                    break;
            }
        }
    }
}