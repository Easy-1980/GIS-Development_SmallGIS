using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;

namespace Small_ArcGis
{
    public partial class FormPrint : Form
    {
        private AxPageLayoutControl _mainPageLayoutControl;
        private Bitmap _previewBitmap = null;

        public FormPrint()
        {
            InitializeComponent();
            InitializeControls();
        }

        /// <summary>
        /// 构造函数，接收主窗体的PageLayoutControl
        /// </summary>
        /// <param name="mainPageLayoutControl">主窗体的AxPageLayoutControl</param>
        public FormPrint(AxPageLayoutControl mainPageLayoutControl)
        {
            InitializeComponent();
            _mainPageLayoutControl = mainPageLayoutControl;
            InitializeControls();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeControls()
        {
            // 初始化打印机列表
            InitPrinterList();

            // 初始化纸张大小
            if (cmbPaperSize.Items.Count > 0)
                cmbPaperSize.SelectedIndex = 0;

            // 初始化纸张方向
            radioButton1.Checked = true;

            // 初始化导出格式
            if (cmbExportFormat.Items.Count > 0)
                cmbExportFormat.SelectedIndex = 0;

            // 初始化分辨率
            if (cmbDPI.Items.Count > 0)
                cmbDPI.SelectedIndex = 1; // 默认300 DPI

            // 获取当前程序运行目录或文档目录作为默认前缀
            string baseDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
            string defaultFileName = "地图导出_" + DateTime.Now.ToString("yyMMdd");

            txtExPath1.Text = System.IO.Path.Combine(baseDir, defaultFileName + ".pdf");
            txtExPath2.Text = System.IO.Path.Combine(baseDir, defaultFileName); 

            // 绑定事件
            btnPrint.Click += BtnPrint_Click;
            btnCancel1.Click += BtnCancel_Click;
            btnExport.Click += BtnExport_Click;
            btnCancel2.Click += BtnCancel_Click;
            btnExPath1.Click += BtnExPath1_Click;
            btnExPath2.Click += BtnExPath2_Click;
            cmbExportFormat.SelectedIndexChanged += CmbExportFormat_SelectedIndexChanged;

            // 纸张设置改变时更新预览
            cmbPaperSize.SelectedIndexChanged += PaperSettings_Changed;
            radioButton1.CheckedChanged += PaperSettings_Changed;
            radioButton2.CheckedChanged += PaperSettings_Changed;

            this.Load += FormPrint_Load;
            this.FormClosing += FormPrint_FormClosing;
        }

        /// <summary>
        /// 初始化打印机列表
        /// </summary>
        private void InitPrinterList()
        {
            comboBox2.Items.Clear();

            // 添加系统安装的打印机
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBox2.Items.Add(printer);
            }

            // 设置默认打印机
            PrinterSettings settings = new PrinterSettings();
            string defaultPrinter = settings.PrinterName;

            int defaultIndex = comboBox2.Items.IndexOf(defaultPrinter);
            if (defaultIndex >= 0)
            {
                comboBox2.SelectedIndex = defaultIndex;
            }
            else if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void FormPrint_Load(object sender, EventArgs e)
        {
            // 生成初始预览
            UpdatePreview();
        }

        /// <summary>
        /// 窗体关闭事件，清理资源
        /// </summary>
        private void FormPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_previewBitmap != null)
            {
                _previewBitmap.Dispose();
                _previewBitmap = null;
            }
        }

        /// <summary>
        /// 纸张设置改变事件
        /// </summary>
        private void PaperSettings_Changed(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        /// <summary>
        /// 获取纸张尺寸（英寸）
        /// </summary>
        private void GetPaperSizeInInches(out double widthInches, out double heightInches)
        {
            // 默认 A4
            widthInches = 8.27;
            heightInches = 11.69;

            if (cmbPaperSize.SelectedItem != null)
            {
                switch (cmbPaperSize.SelectedItem.ToString())
                {
                    case "A3":
                        widthInches = 11.69;
                        heightInches = 16.54;
                        break;
                    case "A2":
                        widthInches = 16.54;
                        heightInches = 23.39;
                        break;
                    case "Letter":
                        widthInches = 8.5;
                        heightInches = 11;
                        break;
                }
            }

            // 根据纸张方向调整
            if (radioButton1.Checked) // 横向
            {
                double temp = widthInches;
                widthInches = heightInches;
                heightInches = temp;
            }
        }

        /// <summary>
        /// 更新打印预览
        /// </summary>
        private void UpdatePreview()
        {
            if (_mainPageLayoutControl == null || _mainPageLayoutControl.ActiveView == null)
            {
                picPreview.Image = null;
                return;
            }

            try
            {
                // 清理旧的预览图片
                if (_previewBitmap != null)
                {
                    picPreview.Image = null;
                    _previewBitmap.Dispose();
                    _previewBitmap = null;
                }

                // 获取纸张尺寸
                double widthInches, heightInches;
                GetPaperSizeInInches(out widthInches, out heightInches);

                // 使用较低的预览分辨率以提高性能
                int previewDpi = 96;
                int previewWidth = (int)(widthInches * previewDpi);
                int previewHeight = (int)(heightInches * previewDpi);

                // 创建预览位图
                _previewBitmap = new Bitmap(previewWidth, previewHeight);

                using (Graphics g = Graphics.FromImage(_previewBitmap))
                {
                    g.Clear(Color.White);

                    // 获取 HDC
                    IntPtr hdc = g.GetHdc();

                    try
                    {
                        // 设置输出矩形
                        tagRECT outputRect;
                        outputRect.left = 0;
                        outputRect.top = 0;
                        outputRect.right = previewWidth;
                        outputRect.bottom = previewHeight;

                        // 输出到 HDC
                        IActiveView activeView = _mainPageLayoutControl.ActiveView;
                        activeView.Output(hdc.ToInt32(), previewDpi, ref outputRect, null, null);
                    }
                    finally
                    {
                        g.ReleaseHdc(hdc);
                    }
                }

                // 显示预览
                picPreview.Image = _previewBitmap;
            }
            catch (Exception ex)
            {
                // 如果渲染失败，显示错误信息
                if (_previewBitmap != null)
                {
                    _previewBitmap.Dispose();
                    _previewBitmap = null;
                }

                _previewBitmap = new Bitmap(400, 300);
                using (Graphics g = Graphics.FromImage(_previewBitmap))
                {
                    g.Clear(Color.White);
                    g.DrawString("预览生成失败:\n" + ex.Message,
                        new Font("Microsoft YaHei", 10),
                        Brushes.Red,
                        new RectangleF(10, 10, 380, 280));
                }
                picPreview.Image = _previewBitmap;
            }
        }

        /// <summary>
        /// 打印按钮点击事件
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("请选择打印机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. 获取文本框路径
            string outputPath = txtExPath1.Text.Trim();

            // 2. 校验路径是否为空
            if (string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("请先设置保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 3. 直接调用导出方法，不再弹出 SaveFileDialog
                ExportToPDF(outputPath);

                MessageBox.Show("PDF导出成功！\n保存路径: " + outputPath, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 导出为PDF
        /// </summary>
        private void ExportToPDF(string outputPath)
        {
            // 始终使用主窗体的 PageLayoutControl 进行导出，避免 COM 对象复制问题
            if (_mainPageLayoutControl == null || _mainPageLayoutControl.ActiveView == null)
            {
                throw new Exception("无法获取活动视图");
            }

            IActiveView activeView = _mainPageLayoutControl.ActiveView;
            IExport pExport = null;

            try
            {
                // 创建PDF导出对象
                pExport = new ExportPDFClass();
                pExport.ExportFileName = outputPath;

                // 设置分辨率
                int dpi = 300;
                pExport.Resolution = dpi;

                // 获取导出范围 - 使用 PageLayout 的实际范围
                tagRECT exportRect = activeView.ExportFrame;

                // 根据纸张大小设置尺寸（像素）
                int width = (int)(8.27 * dpi);   // A4 默认宽度
                int height = (int)(11.69 * dpi); // A4 默认高度

                if (cmbPaperSize.SelectedItem != null)
                {
                    switch (cmbPaperSize.SelectedItem.ToString())
                    {
                        case "A3":
                            width = (int)(11.69 * dpi);
                            height = (int)(16.54 * dpi);
                            break;
                        case "A2":
                            width = (int)(16.54 * dpi);
                            height = (int)(23.39 * dpi);
                            break;
                        case "Letter":
                            width = (int)(8.5 * dpi);
                            height = (int)(11 * dpi);
                            break;
                    }
                }

                // 根据纸张方向调整
                if (radioButton1.Checked) // 横向
                {
                    int temp = width;
                    width = height;
                    height = temp;
                }

                exportRect.left = 0;
                exportRect.top = 0;
                exportRect.right = width;
                exportRect.bottom = height;

                // 设置导出像素范围
                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.PutCoords(exportRect.left, exportRect.top, exportRect.right, exportRect.bottom);
                pExport.PixelBounds = pEnvelope;

                // 开始导出
                int hDC = pExport.StartExporting();
                activeView.Output(hDC, dpi, ref exportRect, null, null);
                pExport.FinishExporting();
            }
            finally
            {
                // 确保清理资源
                if (pExport != null)
                {
                    pExport.Cleanup();
                }
            }
        }

        /// <summary>
        /// 导出按钮点击事件
        /// </summary>
        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (cmbExportFormat.SelectedItem == null)
            {
                MessageBox.Show("请选择导出格式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. 获取文本框路径
            string outputPath = txtExPath2.Text.Trim();

            // 2. 校验路径
            if (string.IsNullOrEmpty(outputPath))
            {
                MessageBox.Show("请先设置保存路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string format = cmbExportFormat.SelectedItem.ToString();

                // 确保扩展名正确（防止用户手动改了文本框但没改后缀）
                // 如果文本框里是 .jpg 但选的是 PNG格式，这里可以做个强制修正，或者信任用户
                // 这里保持简单，直接用文本框的路径

                // 获取分辨率
                int dpi = 300;
                if (cmbDPI.SelectedItem != null)
                {
                    int.TryParse(cmbDPI.SelectedItem.ToString(), out dpi);
                }

                // 3. 直接导出，不再弹出 SaveFileDialog
                ExportToImage(outputPath, format.Contains("PNG"), dpi);

                MessageBox.Show("图片导出成功！\n保存路径: " + outputPath, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 导出为图片
        /// </summary>
        private void ExportToImage(string outputPath, bool isPng, int dpi)
        {
            // 始终使用主窗体的 PageLayoutControl 进行导出，避免 COM 对象复制问题
            if (_mainPageLayoutControl == null || _mainPageLayoutControl.ActiveView == null)
            {
                throw new Exception("无法获取活动视图");
            }

            IActiveView activeView = _mainPageLayoutControl.ActiveView;
            IExport pExport = null;

            try
            {
                // 创建导出对象
                if (isPng)
                {
                    pExport = new ExportPNGClass();
                }
                else
                {
                    pExport = new ExportJPEGClass();
                }

                pExport.ExportFileName = outputPath;
                pExport.Resolution = dpi;

                // 获取导出范围 - 使用 PageLayout 的实际范围
                tagRECT exportRect = activeView.ExportFrame;

                // 计算导出尺寸（根据分辨率缩放）
                double scale = dpi / 96.0;
                int width = (int)(exportRect.right * scale);
                int height = (int)(exportRect.bottom * scale);

                exportRect.left = 0;
                exportRect.top = 0;
                exportRect.right = width;
                exportRect.bottom = height;

                // 设置导出像素范围
                IEnvelope pEnvelope = new EnvelopeClass();
                pEnvelope.PutCoords(exportRect.left, exportRect.top, exportRect.right, exportRect.bottom);
                pExport.PixelBounds = pEnvelope;

                // 开始导出
                int hDC = pExport.StartExporting();
                activeView.Output(hDC, dpi, ref exportRect, null, null);
                pExport.FinishExporting();
            }
            finally
            {
                // 确保清理资源
                if (pExport != null)
                {
                    pExport.Cleanup();
                }
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 浏览PDF保存路径 (改为 SaveFileDialog)
        /// </summary>
        private void BtnExPath1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "选择PDF保存路径";
                dialog.Filter = "PDF文件(*.pdf)|*.pdf";
                dialog.OverwritePrompt = false; //因为真正保存是在点"打印"时，这里设为false避免重复弹窗，或者设为true作为预警也可以

                // 尝试获取当前文本框的路径作为初始目录
                string currentPath = txtExPath1.Text.Trim();
                if (!string.IsNullOrEmpty(currentPath))
                {
                    try
                    {
                        string dir = System.IO.Path.GetDirectoryName(currentPath);
                        string fileName = System.IO.Path.GetFileName(currentPath);

                        if (System.IO.Directory.Exists(dir))
                        {
                            dialog.InitialDirectory = dir;
                        }
                        dialog.FileName = fileName;
                    }
                    catch { } // 忽略路径解析错误
                }
                else
                {
                    // 如果文本框为空，给个默认名
                    dialog.FileName = "地图导出_" + DateTime.Now.ToString("yyMMdd") + ".pdf";
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtExPath1.Text = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// 浏览图片保存路径 (改为 SaveFileDialog)
        /// </summary>
        private void BtnExPath2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "选择图片保存路径";

                // 1. 判断当前选中的格式
                bool isPng = false;
                if (cmbExportFormat.SelectedItem != null && cmbExportFormat.SelectedItem.ToString().Contains("PNG"))
                {
                    isPng = true;
                }

                // 2. 动态设置过滤器
                if (isPng)
                {
                    dialog.Filter = "PNG图片(*.png)|*.png";
                    dialog.DefaultExt = "png";
                }
                else
                {
                    dialog.Filter = "JPEG图片(*.jpg)|*.jpg";
                    dialog.DefaultExt = "jpg";
                }

                // 3. 尝试获取当前文本框的路径作为初始目录
                string currentPath = txtExPath2.Text.Trim();
                if (!string.IsNullOrEmpty(currentPath))
                {
                    try
                    {
                        string dir = System.IO.Path.GetDirectoryName(currentPath);
                        string fileName = System.IO.Path.GetFileName(currentPath);

                        // 如果当前文本框里的后缀和选中的格式不符，自动修正文件名后缀
                        string expectedExt = isPng ? ".png" : ".jpg";
                        if (!fileName.ToLower().EndsWith(expectedExt))
                        {
                            fileName = System.IO.Path.GetFileNameWithoutExtension(fileName) + expectedExt;
                        }

                        if (System.IO.Directory.Exists(dir))
                        {
                            dialog.InitialDirectory = dir;
                        }
                        dialog.FileName = fileName;
                    }
                    catch { }
                }
                else
                {
                    string ext = isPng ? ".png" : ".jpg";
                    dialog.FileName = "地图导出_" + DateTime.Now.ToString("yyMMdd") + ext;
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtExPath2.Text = dialog.FileName;
                }
            }
        }
        /// <summary>
        /// 导出格式改变事件
        /// </summary>
        private void CmbExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 更新文件扩展名
            if (cmbExportFormat.SelectedItem != null)
            {
                string baseName = "地图导出_" + DateTime.Now.ToString("yyMMdd");
                if (cmbExportFormat.SelectedItem.ToString().Contains("PNG"))
                {
                    if (!txtExPath2.Text.EndsWith(".png"))
                    {
                        txtExPath2.Text = baseName;
                    }
                }
                else
                {
                    if (!txtExPath2.Text.EndsWith(".jpg"))
                    {
                        txtExPath2.Text = baseName;
                    }
                }
            }
        }
    }
}

