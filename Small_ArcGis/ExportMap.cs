using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using ESRI.ArcGIS.Output;
using System.Windows.Forms;

namespace Small_ArcGis
{
    class ExportMap
    {
        public static void ExportView(IActiveView view, IGeometry pGeo, int OutputResolution, int Width, int Height, string ExpPath, bool bRegion)
        {
            IExport pExport = null;     // 导出对象
            tagRECT exportRect = new tagRECT();      // 导出的屏幕区域 设备像素
            IEnvelope pEnvelop = pGeo.Envelope;     // 地图的可见范围
            string sType = System.IO.Path.GetExtension(ExpPath);
            // 获取扩展名，初始化导出对象pExport
            switch (sType)
            {
                case".jpg":
                    pExport = new ExportJPEGClass();
                    break;
                case".bmp":
                    pExport = new ExportBMPClass();
                    break;
                case".gif":
                    pExport = new ExportGIFClass();
                    break;
                default:
                    MessageBox.Show("没有设定输出格式，默认.JPEG格式");
                    pExport = new ExportJPEGClass();
                    break;
            }
            pExport.ExportFileName = ExpPath;

            // 确定屏幕范围
            exportRect.left = 0;
            exportRect.top = 0;
            exportRect.right = Width;
            exportRect.bottom = Height;
            if (bRegion)
            {
                view.GraphicsContainer.DeleteAllElements();
                view.Refresh();
            }
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords((double)exportRect.left, (double)exportRect.top, (double)exportRect.right, (double)exportRect.bottom);
            pExport.PixelBounds = envelope;
            view.Output(pExport.StartExporting(), OutputResolution, ref exportRect, pEnvelop, null);
            pExport.FinishExporting();
            pExport.Cleanup();
        }

        private static IRgbColor GetRgbColor(int red, int green, int blue)
        {
            IRgbColor pColor = null;
            pColor = new RgbColor();
            pColor.Red = red;
            pColor.Green = green;
            pColor.Blue = blue;
            return pColor;
        }

        // 绘制几何图形
        public static void AddElement(IGeometry pGeometry, IActiveView activeView)
        {
            IRgbColor fillcolor = GetRgbColor(204, 175, 235);
            IRgbColor linecolor = GetRgbColor(255, 0, 0);
            IElement pEle = CreateElement(pGeometry, linecolor, fillcolor);
            IGraphicsContainer pGC = activeView.GraphicsContainer;
            if (pGC!=null)
            {
                pGC.AddElement(pEle, 0);
                activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, pEle, null);
            }
        }

        // 创建图形元素
        private static IElement CreateElement(IGeometry pGeometry, IRgbColor linecolor, IRgbColor fillcolor)
        {
            IElement pElement = null;
            if (pGeometry is IEnvelope)
            {
                pElement = new RectangleElementClass();
            }
            else if (pGeometry is IPolygon)
            {
                pElement = new PolygonElementClass();
            }
            else if (pGeometry is ICircularArc)
            {
                ISegment pSegment = pGeometry as ISegment;
                ISegmentCollection pSegCol = new PolygonClass();
                object o = Type.Missing;
                pSegCol.AddSegment(pSegment,ref o,ref o);
                IPolygon pPolygon = pSegCol as IPolygon;
                pGeometry = pPolygon as IGeometry;
                pElement = new CircleElementClass();
            }
            else if (pGeometry is IPolyline)
            {
                pElement = new LineElementClass();
            }
            pElement.Geometry = pGeometry;
            IFillShapeElement pFillEle = pElement as IFillShapeElement;
            ISimpleFillSymbol pSys = new SimpleFillSymbolClass();
            pSys.Color = fillcolor;
            pSys.Outline.Color = linecolor;
            pSys.Style = esriSimpleFillStyle.esriSFSCross;
            pFillEle.Symbol = pSys;
            return pElement;
        }
    }
}
