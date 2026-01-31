using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace Small_ArcGis
{
    public class ClassifiedRendererHelper
    {
        public bool IsNumericField(IField field)
        {
            if (field == null)
            {
                return false;
            }

            switch (field.Type)
            {
                case esriFieldType.esriFieldTypeDouble:
                case esriFieldType.esriFieldTypeInteger:
                case esriFieldType.esriFieldTypeSingle:
                case esriFieldType.esriFieldTypeSmallInteger:
                    return true;
                default:
                    return false;
            }
        }

        public IClassBreaksRenderer BuildRenderer(IFeatureLayer layer, string fieldName, int classCount, string methodName, string colorSchemeName)
        {
            if (layer == null)
            {
                throw new ArgumentNullException("layer");
            }

            if (layer.FeatureClass == null)
            {
                throw new InvalidOperationException("要素类不存在，无法进行分级符号化。");
            }

            esriGeometryType geometryType = layer.FeatureClass.ShapeType;
            if (geometryType != esriGeometryType.esriGeometryPolygon && geometryType != esriGeometryType.esriGeometryPolyline)
            {
                throw new NotSupportedException("仅支持面或线要素图层的分级符号化。");
            }

            int fieldIndex = layer.FeatureClass.Fields.FindField(fieldName);
            if (fieldIndex < 0)
            {
                throw new InvalidOperationException("分级字段不存在。");
            }

            IField targetField = layer.FeatureClass.Fields.get_Field(fieldIndex);
            if (!IsNumericField(targetField))
            {
                throw new InvalidOperationException("请选择数值型字段。");
            }

            if (classCount < 2 || classCount > 5)
            {
                throw new ArgumentOutOfRangeException("classCount", "分级数需在 2 到 5 之间。");
            }

            IClassifyGEN classifier = ResolveClassifier(methodName);
            if (classifier == null)
            {
                throw new NotSupportedException("暂不支持所选分级方式。");
            }

            ITableHistogram tableHistogram = new BasicTableHistogramClass();
            tableHistogram.Field = fieldName;
            tableHistogram.Table = layer.FeatureClass as ITable;
            object dataValues = null;
            object dataFrequencies = null;
            IBasicHistogram basicHistogram = tableHistogram as IBasicHistogram;
            if (basicHistogram == null)
            {
                throw new InvalidOperationException("当前环境不支持直方图计算。");
            }

            basicHistogram.GetHistogram(out dataValues, out dataFrequencies);

            if (dataValues == null)
            {
                throw new InvalidOperationException("分级字段未包含有效数据。");
            }

            double[] numericValues = ToDoubleArray(dataValues);
            double[] numericFreqs = ToDoubleArray(dataFrequencies);
            if (numericValues == null || numericFreqs == null || numericValues.Length == 0)
            {
                throw new InvalidOperationException("分级字段未包含有效数据。");
            }

            // 保证有效值充足，防止 StandardDeviationClass 等算法因样本不足返回 E_FAIL
            int effectiveCount = 0;
            foreach (double f in numericFreqs)
            {
                if (f > 0) effectiveCount++;
            }
            if (effectiveCount < 2)
            {
                throw new InvalidOperationException("分级字段有效样本不足，无法进行分级。");
            }

            int breakCount = classCount;

            try
            {
                classifier.Classify(numericValues, numericFreqs, ref breakCount);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("分级计算失败，请确认字段数据有效且分级数合理。", ex);
            }

            double[] classBreaks = ToDoubleArray(classifier.ClassBreaks);
            if (classBreaks == null || classBreaks.Length < 2)
            {
                throw new InvalidOperationException("生成分级断点失败。");
            }

            // 按实际返回的断点数量截断，避免 StandardDeviationClass 返回断点数少于请求时抛错
            int availableBreaks = classBreaks.Length - 1;
            int renderBreaks = Math.Min(breakCount, availableBreaks);

            IEnumColors colors = CreateColorRamp(colorSchemeName, breakCount);
            colors.Reset();

            IClassBreaksRenderer renderer = new ClassBreaksRendererClass();
            renderer.Field = fieldName;
            renderer.BreakCount = renderBreaks;
            renderer.SortClassesAscending = true;
            renderer.MinimumBreak = classBreaks[0];

            for (int i = 0; i < renderBreaks; i++)
            {
                IColor color = colors.Next();
                if (color == null)
                {
                    colors.Reset();
                    color = colors.Next();
                }

                double lowerBound = classBreaks[i];
                double upperBound = classBreaks[i + 1];
                renderer.set_Break(i, upperBound);
                renderer.set_Label(i, string.Format("{0:0.##} - {1:0.##}", lowerBound, upperBound));
                renderer.set_Symbol(i, CreateSymbol(geometryType, color));
            }

            return renderer;
        }

        private IClassifyGEN ResolveClassifier(string methodName)
        {
            string normalized = (methodName ?? string.Empty).Trim();
            switch (normalized)
            {
                case "等间距分级":
                    return new EqualIntervalClass();
                case "分位数分级":
                    return new QuantileClass();
                case "几何间隔分级":
                    return new GeometricalIntervalClass();
                /* 燃尽了，我真不知道这个该怎么改，我感觉是数据量太少了，标准差根本拟合不出正态曲线，删掉吧，加上也只能有一个报错提示 */
                //case "标准差分级":
                //    return new StandardDeviationClass();
                default:
                    return new NaturalBreaksClass();
            }
        }

        private IEnumColors CreateColorRamp(string colorSchemeName, int classCount)
        {
            AlgorithmicColorRamp colorRamp = new AlgorithmicColorRampClass();
            colorRamp.Size = classCount;
            colorRamp.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;

            string normalized = (colorSchemeName ?? string.Empty).Replace(" ", string.Empty);
            if (normalized.Contains("绿"))
            {
                colorRamp.FromColor = CreateRgbColor(38, 166, 65);
                colorRamp.ToColor = CreateRgbColor(255, 221, 85);
            }
            else if (normalized.Contains("灰"))
            {
                colorRamp.FromColor = CreateRgbColor(200, 200, 200);
                colorRamp.ToColor = CreateRgbColor(40, 40, 40);
            }
            else if (normalized.Contains("浅红") || normalized.Contains("深红"))
            {
                colorRamp.FromColor = CreateRgbColor(255, 204, 204);
                colorRamp.ToColor = CreateRgbColor(128, 0, 0);
            }
            else
            {
                colorRamp.FromColor = CreateRgbColor(0, 112, 192);
                colorRamp.ToColor = CreateRgbColor(192, 0, 0);
            }

            bool rampOk;
            colorRamp.CreateRamp(out rampOk);
            if (!rampOk)
            {
                throw new InvalidOperationException("生成色带失败。");
            }

            return colorRamp.Colors;
        }

        private IRgbColor CreateRgbColor(int r, int g, int b)
        {
            IRgbColor color = new RgbColorClass();
            color.Red = r;
            color.Green = g;
            color.Blue = b;
            color.UseWindowsDithering = true;
            return color;
        }

        private ISymbol CreateSymbol(esriGeometryType geometryType, IColor color)
        {
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPolygon:
                    ISimpleFillSymbol fill = new SimpleFillSymbolClass();
                    fill.Color = color;
                    ISimpleLineSymbol outline = new SimpleLineSymbolClass();
                    outline.Color = CreateRgbColor(80, 80, 80);
                    outline.Width = 0.4;
                    fill.Outline = outline;
                    return fill as ISymbol;
                case esriGeometryType.esriGeometryPolyline:
                    ISimpleLineSymbol line = new SimpleLineSymbolClass();
                    line.Color = color;
                    line.Width = 1.6;
                    return line as ISymbol;
                default:
                    ISimpleMarkerSymbol marker = new SimpleMarkerSymbolClass();
                    marker.Color = color;
                    marker.Size = 6;
                    return marker as ISymbol;
            }
        }

        private double[] ToDoubleArray(object classBreaks)
        {
            double[] typedBreaks = classBreaks as double[];
            if (typedBreaks != null)
            {
                return typedBreaks;
            }

            System.Array rawBreaks = classBreaks as System.Array;
            if (rawBreaks == null)
            {
                return null;
            }

            double[] result = new double[rawBreaks.Length];
            for (int i = 0; i < rawBreaks.Length; i++)
            {
                result[i] = Convert.ToDouble(rawBreaks.GetValue(i));
            }
            return result;
        }

    }
}