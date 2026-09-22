using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

// --------------------------------------------------------
// 一覧印刷フォーム
// --------------------------------------------------------

// 印刷設定値は、「PrintSettings.txt」へ保存

namespace MyView
{
    public partial class Form4 : Form
    {
        private readonly PrintDocument _printDocument = new PrintDocument();
        private readonly List<string> _imageFiles = new List<string>();
        private int _currentPageIndex = 0;

        // 前回終了時(Form4を閉じたとき)の印刷設定保存
        // C:\Users\ユーザー名\AppData\Local\MyView\LastFolder.txt
        private string PrintSettingsFilePath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MyView",
                "PrintSettings.txt");


        // ============================================================
        // 右ドラッグによるプレビュー移動
        // ============================================================

        private bool _isPreviewDragging = false;
        private Point _previewDragStartPoint;
        private int _previewDragStartHorizontalValue;
        private int _previewDragStartVerticalValue;

        // ============================================================
        // コンストラクタ
        // ============================================================

        public Form4(List<string> imageFiles)
        {
            InitializeComponent();

            // フォームサイズ
            this.Width = 1000;
            this.Height = 700;
            this.MinimumSize = new Size(400, 400);
            this.WindowState = FormWindowState.Maximized;

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.SplitterDistance = 250;
            splitContainer1.Panel1MinSize = 200;

            previewControl.Dock = DockStyle.Fill;
            previewControl.BackColor = Color.DimGray;

            panel2.BackColor = Color.DimGray;
            panel2.Dock = DockStyle.Fill;

            setDpi.Items.AddRange(new string[] { "100", "200", "300", "400", "500", "600" });
            setDpi.SelectedIndex = 2;

            toolStripStatusLabel1.Text = "一覧印刷を行います。";

            // NumericUpDown の初期値・範囲ガード設定
            tate.Minimum = 1;
            tate.Maximum = 10;
            if (tate.Value < 1) tate.Value = 4;

            yoko.Minimum = 1;
            yoko.Maximum = 10;
            if (yoko.Value < 1) yoko.Value = 2;

            if (imageFiles != null)
            {
                _imageFiles = new List<string>(imageFiles);
            }

            // PrintPreviewControl に Document を割り当て
            previewControl.Document = _printDocument;

            // 印刷設定の初期値
            // A4(210mm×297mm)・幅・高さ
            _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4 (210x297mm)", 827, 1169);
            // 縦:false、横:ture
            _printDocument.DefaultPageSettings.Landscape = false;
            // 余白10mm 指定は1/100インチ
            int margin = (int)Math.Round(10 / 25.4 * 100);

            _printDocument.DefaultPageSettings.Margins =
                new Margins(
                    margin,
                    margin,
                    margin,
                    margin);

            paperlabel.Text = "用紙：A4 (210x297mm)・縦";

            // フォント一覧を取得して追加
            foreach (FontFamily font in FontFamily.Families.OrderBy(f => f.Name))
            {
                setFont.Items.Add(font.Name);
            }
            setFont.SelectedItem = "Yu Gothic UI";

            setFontSize.Minimum = 1;
            setFontSize.Maximum = 16;
            if (setFontSize.Value < 1) setFontSize.Value = 8;

            // NumericUpDown などのコントロールにイベントを一括紐付け
            tate.MouseEnter += Menu_MouseEnter;
            tate.MouseLeave += Menu_MouseLeave;

            yoko.MouseEnter += Menu_MouseEnter;
            yoko.MouseLeave += Menu_MouseLeave;

            // 印刷ページ描画イベントを登録
            _printDocument.PrintPage += PrintDocument_PrintPage;

            // マウスホイールイベント
            previewControl.MouseWheel += PreviewControl_MouseWheel;
            previewControl.Focus();
        }

        // ==============================
        // マウスONで説明 
        // ==============================
        private void Menu_MouseEnter(object? sender, EventArgs e)
        {
            string? message = null;

            // 通常のコントロール（Button, NumericUpDown, TextBox など）
            if (sender is Control control)
            {
                if (control.Tag != null)
                {
                    message = control.Tag.ToString();
                }

                // Tagが設定されていればToolTipをセット
                if (!string.IsNullOrEmpty(message))
                {
                    toolTip1.SetToolTip(control, message);
                }
            }
            // メニュー項目やツールバー項目（ToolStripMenuItem, ToolStripButton など）
            else if (sender is ToolStripItem item)
            {
                if (item.Tag != null)
                {
                    message = item.Tag.ToString();
                }

                if (!string.IsNullOrEmpty(message))
                {
                    item.ToolTipText = message;
                }
            }

            // ステータスバーへの表示更新
            if (!string.IsNullOrEmpty(message))
            {
                toolStripStatusLabel1.Text = message;
            }
        }

        // ==============================
        // マウス離脱 
        // ==============================
        private void Menu_MouseLeave(object? sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "一覧印刷を行います。";
        }

        // ============================================================
        // フォームを閉じるとき
        // ============================================================
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 印刷設定を保存
            SavePrintSettings();
        }

        // ============================================================
        // 閉じるを押したとき
        // ============================================================
        private void btnClose_Click(object sender, EventArgs e)
        {
            // 現在のフォームを閉じる
            this.Close();
        }

        // ============================================================
        // フォームを閉じるときに印刷設定を保存
        // ============================================================
        private void SavePrintSettings()
        {
            try
            {
                string folder = Path.GetDirectoryName(PrintSettingsFilePath)!;

                Directory.CreateDirectory(folder);

                var pageSettings = _printDocument.DefaultPageSettings;

                var paperSize = pageSettings.PaperSize;

                var lines = new List<string>
                {
                    // 縦配置数
                    $"tate={tate.Value}",
                    // 横配置数
                    $"yoko={yoko.Value}", 
                     // 解像度
                    $"dpi={setDpi.Text}",
                    // フォント
                    $"font={setFont.Text}",
                    // フォントサイズ
                    $"fontSize={setFontSize.Value}",
                    // 用紙
                    $"paperName={paperSize.PaperName}",
                    $"paperWidth={paperSize.Width}",
                    $"paperHeight={paperSize.Height}",

                    // 用紙の方向(縦・横)
                    $"landscape={_printDocument.DefaultPageSettings.Landscape}",
                    // 余白
                    $"marginLeft={_printDocument.DefaultPageSettings.Margins.Left}",
                    $"marginRight={_printDocument.DefaultPageSettings.Margins.Right}",
                    $"marginTop={_printDocument.DefaultPageSettings.Margins.Top}",
                    $"marginBottom={_printDocument.DefaultPageSettings.Margins.Bottom}"
                };

                File.WriteAllLines(PrintSettingsFilePath, lines, Encoding.UTF8);
            }
            catch
            {
                // 設定保存失敗はアプリの動作を止めない
            }
        }

        // ============================================================
        // マウスホイール操作
        //
        // Ctrl + ホイール
        //     → マウスカーソル位置を基準に拡大・縮小
        //
        // 通常のホイール
        //     → 上：前のページ
        //     → 下：次のページ
        // ============================================================
        private void PreviewControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            // ========================================================
            // Ctrl + ホイール：マウス位置を基準にズーム
            // ========================================================
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0)
                    previewControl.Zoom += 0.1;
                else
                    previewControl.Zoom -= 0.1;

                if (previewControl.Zoom < 0.2)
                    previewControl.Zoom = 0.2;

                if (previewControl.Zoom > 3.0)
                    previewControl.Zoom = 3.0;

                return;
            }

            // ========================================================
            // 通常のホイール：ページ移動
            // ========================================================

            if (e.Delta > 0)
            {
                // 上 → 前のページ
                GoToPreviousPage();
            }
            else if (e.Delta < 0)
            {
                // 下 → 次のページ
                GoToNextPage();
            }
        }

        // ============================================================
        // フォームを開いたとき
        // ============================================================
        private void Form4_Load(object sender, EventArgs e)
        {
            // 印刷設定を読み込む
            LoadPrintSettings();
            // プリンタ名ラベルの更新
            UpdatePrinterInfo();
            // プレビューの再描画更新
            RefreshPreview();
        }

        // ============================================================
        // フォームを開いたときに印刷設定を読み込む
        // ============================================================
        private void LoadPrintSettings()
        {
            try
            {
                if (!File.Exists(PrintSettingsFilePath))
                    return;

                string[] lines = File.ReadAllLines(PrintSettingsFilePath, Encoding.UTF8);
                // 用紙サイズ
                string paperName = "A4 (210x297mm)";
                int paperWidth = 827;
                int paperHeight = 1169;
                // 用紙方向
                bool landscape = false;
                // 余白
                int marginLeft = 39;
                int marginRight = 39;
                int marginTop = 39;
                int marginBottom = 39;
                // フォント
                string fontName = "Yu Gothic UI";
                //setFont.SelectedItem = fontName;
                //int fontSize = 8;

                foreach (string line in lines)
                {
                    string[] parts = line.Split('=', 2);

                    if (parts.Length != 2)
                        continue;

                    string key = parts[0];
                    string value = parts[1];

                    switch (key)
                    {
                        // 縦配置数
                        case "tate":
                            if (decimal.TryParse(value, out decimal tateValue))
                            {
                                tate.Value =
                                    Math.Max(
                                        tate.Minimum,
                                        Math.Min(
                                            tate.Maximum,
                                            tateValue));
                            }
                            break;
                        // 横配置数
                        case "yoko":
                            if (decimal.TryParse(value, out decimal yokoValue))
                            {
                                yoko.Value =
                                    Math.Max(
                                        yoko.Minimum,
                                        Math.Min(
                                            yoko.Maximum,
                                            yokoValue));
                            }
                            break;
                        // 解像度
                        case "dpi":
                            if (setDpi.Items.Contains(value))
                            {
                                setDpi.SelectedItem = value;
                            }
                            break;
                        // フォント
                        case "font":
                            fontName = value;
                            if (!string.IsNullOrEmpty(fontName) && setFont.Items.Contains(fontName))
                            {
                                setFont.SelectedItem = fontName;
                            }
                            break;
                        // フォントサイズ
                        case "fontSize":
                            if (decimal.TryParse(value, out decimal fontSizeValue))
                            {
                                setFontSize.Value =
                                    Math.Max(
                                        setFontSize.Minimum,
                                        Math.Min(
                                            setFontSize.Maximum,
                                            fontSizeValue));
                            }
                            break;
                        // 用紙名
                        case "paperName":
                            paperName = value;
                            break;
                        // 用紙の幅
                        case "paperWidth":
                            int.TryParse(value, out paperWidth);
                            break;
                        // 用紙の高さ
                        case "paperHeight":
                            int.TryParse(value, out paperHeight);
                            break;
                        // 用紙方向
                        case "landscape":
                            bool.TryParse(value, out landscape);
                            break;
                        // 余白
                        case "marginLeft":
                            int.TryParse(value, out marginLeft);
                            break;

                        case "marginRight":
                            int.TryParse(value, out marginRight);
                            break;

                        case "marginTop":
                            int.TryParse(value, out marginTop);
                            break;

                        case "marginBottom":
                            int.TryParse(value, out marginBottom);
                            break;
                    }
                }

                // 読み込んだページ設定を PrintDocument に反映
                // 用紙、用紙サイズ
                PaperSize? foundPaperSize =
                    _printDocument.PrinterSettings.PaperSizes
                        .Cast<PaperSize>()
                        .FirstOrDefault(p =>
                            string.Equals(
                                p.PaperName,
                                paperName,
                                StringComparison.OrdinalIgnoreCase));

                if (foundPaperSize != null)
                {
                    _printDocument.DefaultPageSettings.PaperSize = foundPaperSize;
                }
                else
                {
                    // プリンターに該当用紙がない場合は保存値を使用
                    _printDocument.DefaultPageSettings.PaperSize =
                        new PaperSize(
                            paperName,
                            paperWidth,
                            paperHeight);
                }

                // 用紙方向(縦、横)
                _printDocument.DefaultPageSettings.Landscape = landscape;
                // 余白
                _printDocument.DefaultPageSettings.Margins =
                    new Margins(
                        marginLeft,
                        marginRight,
                        marginTop,
                        marginBottom);

            }
            catch
            {
                // 読み込み失敗時は初期設定のまま
            }
        }

        // ============================================================
        // プレビューの再描画更新
        // ============================================================
        private void RefreshPreview()
        {
            _currentPageIndex = 0;
            previewControl.StartPage = 0;
            previewControl.InvalidatePreview();
            UpdatePageLabel();
        }

        // ============================================================
        // プリンタ名ラベルの更新
        // ============================================================
        private void UpdatePrinterInfo()
        {
            PrinterNamelabel.Text = $"プリンタ名：{_printDocument.PrinterSettings.PrinterName}";

            string landscapelabel = "縦";
            if (_printDocument.DefaultPageSettings.Landscape)
            {
                landscapelabel = "横";
            }
            paperlabel.Text = $"用紙：{_printDocument.DefaultPageSettings.PaperSize.PaperName}・{landscapelabel}";

        }

        // ============================================================
        // ページ数表示ラベルの更新
        // ============================================================
        private void UpdatePageLabel()
        {
            int rows = (int)tate.Value;
            int cols = (int)yoko.Value;
            int itemsPerPage = rows * cols;

            if (itemsPerPage <= 0 || _imageFiles.Count == 0)
            {
                Pagelabel.Text = "ページ：0 / 0";
                return;
            }

            int totalPages = (int)Math.Ceiling((double)_imageFiles.Count / itemsPerPage);
            int currentPage = previewControl.StartPage + 1;
            Pagelabel.Text = $"ページ：{currentPage} / {totalPages}";
        }

        // ============================================================
        // グリッドサイズ（縦・横）変更時
        // ============================================================
        private void GridSize_ValueChanged(object? sender, EventArgs e)
        {
            RefreshPreview();
        }

        // ============================================================
        // 印刷・プレビュー描画メインルーチン
        // ============================================================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics? g = e.Graphics;
            if (g == null)
                return;

            int rows = (int)tate.Value;
            int cols = (int)yoko.Value;

            int itemsPerPage = rows * cols;

            Rectangle marginBounds = e.MarginBounds;

            float cellWidth = (float)marginBounds.Width / cols;

            float cellHeight = (float)marginBounds.Height / rows;

            int startIndex = _currentPageIndex * itemsPerPage;

            using Font fileNameFont = new Font(setFont.Text, (float)setFontSize.Value);

            using StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };

            // このページで使用する画像を取得
            int imageCount =
                Math.Min(
                    itemsPerPage,
                    _imageFiles.Count - startIndex);

            string[] pageFilePaths = new string[imageCount];

            for (int i = 0; i < imageCount; i++)
            {
                pageFilePaths[i] = _imageFiles[startIndex + i];
            }

            // 画像をバックグラウンドで並列生成
            float printDpi = float.Parse(setDpi.Text);

            int targetWidth =
                Math.Max(
                    1,
                    (int)Math.Ceiling(
                        (cellWidth - 10) / 100f * printDpi));

            int targetHeight =
                Math.Max(
                    1,
                    (int)Math.Ceiling(
                        (cellHeight - 30) / 100f * printDpi));

            Image?[] pageImages =
                CreatePageImagesAsync(
                    pageFilePaths,
                    targetWidth,
                    targetHeight)
                .GetAwaiter()
                .GetResult();

            try
            {
                // ページ描画
                for (
                    int localIndex = 0;
                    localIndex < itemsPerPage;
                    localIndex++)
                {
                    int row = localIndex / cols;

                    int col = localIndex % cols;

                    float x = marginBounds.Left + (col * cellWidth);

                    float y = marginBounds.Top + (row * cellHeight);

                    RectangleF cellRect =
                        new RectangleF(
                            x,
                            y,
                            cellWidth,
                            cellHeight);

                    // セルの枠線
                    g.DrawRectangle(
                        Pens.LightGray,
                        cellRect.X,
                        cellRect.Y,
                        cellRect.Width,
                        cellRect.Height);

                    int imageIndex = startIndex + localIndex;

                    if (imageIndex >= _imageFiles.Count)
                    {
                        continue;
                    }

                    float fileNameHeight = 20;

                    RectangleF imageRect =
                        new RectangleF(
                            cellRect.X + 5,
                            cellRect.Y + 5,
                            cellRect.Width - 10,
                            cellRect.Height -
                                fileNameHeight - 10);

                    // 画像描画
                    int pageImageIndex = localIndex;

                    if (pageImageIndex < pageImages.Length)
                    {
                        Image? img =
                            pageImages[pageImageIndex];

                        if (img != null)
                        {
                            DrawImageKeepAspectRatio(g, img, imageRect);
                        }
                    }

                    // ファイル名
                    string fileName = Path.GetFileName(_imageFiles[imageIndex]);

                    RectangleF fileNameRect =
                        new RectangleF(
                            cellRect.X + 5,
                            cellRect.Bottom -
                                fileNameHeight - 5,
                            cellRect.Width - 10,
                            fileNameHeight);

                    g.DrawString(
                        fileName,
                        fileNameFont,
                        Brushes.Black,
                        fileNameRect,
                        stringFormat);
                }
            }
            finally
            {

                // 今回のページで作ったBitmapを破棄
                foreach (Image? image in pageImages)
                {
                    image?.Dispose();
                }
            }

            // 次のページ
            _currentPageIndex++;

            e.HasMorePages = (_currentPageIndex * itemsPerPage < _imageFiles.Count);

            if (!e.HasMorePages)
            {
                _currentPageIndex = 0;
            }

        }

        // ============================================================
        // 1ページ分の画像を最大8枚程度ずつ並列処理
        // ============================================================
        private Image CreatePrintImage(string filePath, int width, int height)
        {
            using var inputStream =
                new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

            using var outputStream = new MemoryStream();

            var settings = new ProcessImageSettings
            {
                Width = width,
                Height = height,
                ResizeMode = CropScaleMode.Max
            };

            MagicImageProcessor.ProcessImage(inputStream, outputStream, settings);

            outputStream.Position = 0;

            using var resizedImage = Image.FromStream(outputStream, false, false);

            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                float scale =
                    Math.Min(
                        (float)width / resizedImage.Width,
                        (float)height / resizedImage.Height);

                int drawWidth = (int)(resizedImage.Width * scale);

                int drawHeight = (int)(resizedImage.Height * scale);

                int x = (width - drawWidth) / 2;

                int y = (height - drawHeight) / 2;

                g.InterpolationMode = InterpolationMode.HighQualityBilinear;

                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawImage(
                    resizedImage,
                    new Rectangle(
                        x,
                        y,
                        drawWidth,
                        drawHeight));
            }

            return bitmap;
        }

        // ============================================================
        // 1ページ分の画像を最大8枚程度ずつ並列処理
        // ============================================================
        private async Task<Image?[]> CreatePageImagesAsync(string[] filePaths, int width, int height)
        {
            int maxParallel = Math.Min(Environment.ProcessorCount, 8);

            using var semaphore = new SemaphoreSlim(maxParallel);

            Image?[] images = new Image?[filePaths.Length];

            var tasks = filePaths.Select(async (filePath, index) =>
                {
                    if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    {
                        return;
                    }

                    await semaphore.WaitAsync().ConfigureAwait(false);

                    try
                    {
                        images[index] =
                            await Task.Run(() =>
                                CreatePrintImage(
                                    filePath,
                                    width,
                                    height))
                            .ConfigureAwait(false);
                    }
                    catch
                    {
                        images[index] = null;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return images;
        }

        // ============================================================
        // アスペクト比を維持した画像描画
        // ============================================================
        private void DrawImageKeepAspectRatio(Graphics g, Image img, RectangleF destRect)
        {
            float imgRatio = (float)img.Width / img.Height;
            float destRatio = destRect.Width / destRect.Height;

            float drawWidth = destRect.Width;
            float drawHeight = destRect.Height;
            float x = destRect.X;
            float y = destRect.Y;

            if (imgRatio > destRatio)
            {
                drawHeight = destRect.Width / imgRatio;
                y += (destRect.Height - drawHeight) / 2;
            }
            else
            {
                drawWidth = destRect.Height * imgRatio;
                x += (destRect.Width - drawWidth) / 2;
            }

            g.DrawImage(img, x, y, drawWidth, drawHeight);
        }

        // ============================================================
        // 印刷を押したとき
        // ============================================================
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                using (PrintDialog pd = new PrintDialog())
                {
                    pd.Document = _printDocument;
                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        _currentPageIndex = 0;
                        _printDocument.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("印刷エラー\n" + ex.Message);
            }

            /*
            try
            {
                _currentPageIndex = 0;
                _printDocument.Print();
                //this.Close();
            }
            catch (Exception ex)
            {
              
            */
        }

        // ============================================================
        // プリンタ設定を押したとき
        // ============================================================
        private void btnPrintSet_Click(object sender, EventArgs e)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                pd.Document = _printDocument;
                if (pd.ShowDialog() == DialogResult.OK)
                {
                    UpdatePrinterInfo();
                    RefreshPreview();
                }
            }
        }

        // ============================================================
        // ページ設定を押したとき
        // ============================================================
        private void btnPageSet_Click(object sender, EventArgs e)
        {
            using (PageSetupDialog psd = new PageSetupDialog())
            {
                // インチとミリメートルの変換が正常に行われるようにする
                // これがないと余白が0.3937倍され続ける
                psd.EnableMetric = true;

                psd.Document = _printDocument;

                if (psd.ShowDialog() == DialogResult.OK)
                {
                    UpdatePrinterInfo();
                    RefreshPreview();
                }
            }
        }

        // ============================================================
        // 前のページを押したとき
        // ============================================================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            GoToPreviousPage();
        }

        // ============================================================
        // 前のページへ移動
        // ============================================================
        private void GoToPreviousPage()
        {
            if (previewControl.StartPage > 0)
            {
                previewControl.StartPage--;
                UpdatePageLabel();
            }
        }

        // ============================================================
        // 次のページを押したとき
        // ============================================================
        private void btnNext_Click(object sender, EventArgs e)
        {
            GoToNextPage();
        }

        // ============================================================
        // 次のページへ移動
        // ============================================================
        private void GoToNextPage()
        {
            int rows = (int)tate.Value;
            int cols = (int)yoko.Value;
            int itemsPerPage = rows * cols;

            if (itemsPerPage <= 0)
                return;

            int totalPages = (int)Math.Ceiling((double)_imageFiles.Count / itemsPerPage);

            if (previewControl.StartPage < totalPages - 1)
            {
                previewControl.StartPage++;
                UpdatePageLabel();
            }
        }

        // ============================================================
        // 配置反映を押したとき
        // ============================================================
        private void btnHaichi_Click(object sender, EventArgs e)
        {
            // 1ページ目からプレビューを再描画
            RefreshPreview();
        }

        // ------------------------------------------------------------
        // 横スクロールバーを取得
        // ------------------------------------------------------------
        private HScrollBar? GetPreviewHorizontalScrollBar()
        {
            return previewControl.Controls
                .OfType<HScrollBar>()
                .FirstOrDefault();
        }

        // ------------------------------------------------------------
        // 縦スクロールバーを取得
        // ------------------------------------------------------------
        private VScrollBar? GetPreviewVerticalScrollBar()
        {
            return previewControl.Controls
                .OfType<VScrollBar>()
                .FirstOrDefault();
        }

        // ------------------------------------------------------------
        // 右ボタンを押した
        // ------------------------------------------------------------
        private void previewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            HScrollBar? hScrollBar = GetPreviewHorizontalScrollBar();

            VScrollBar? vScrollBar = GetPreviewVerticalScrollBar();

            if (hScrollBar == null && vScrollBar == null)
                return;

            _isPreviewDragging = true;

            _previewDragStartPoint = e.Location;

            _previewDragStartHorizontalValue = hScrollBar?.Value ?? 0;

            _previewDragStartVerticalValue = vScrollBar?.Value ?? 0;

            // カーソルを手の形にする
            previewControl.Cursor = Cursors.Hand;

        }

        // ------------------------------------------------------------
        // 右ボタンを押したまま移動
        // ------------------------------------------------------------
        private void previewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isPreviewDragging)
                return;

            int deltaX = e.X - _previewDragStartPoint.X;

            int deltaY = e.Y - _previewDragStartPoint.Y;

            HScrollBar? hScrollBar = GetPreviewHorizontalScrollBar();

            VScrollBar? vScrollBar = GetPreviewVerticalScrollBar();

            // --------------------------------------------------------
            // 横方向
            //
            // マウスを右へ動かす
            // → ページを右方向へ見る
            //
            // そのためスクロール値は減らす
            // --------------------------------------------------------
            if (hScrollBar != null)
            {
                int newValue = _previewDragStartHorizontalValue - deltaX;

                newValue =
                    Math.Max(
                        hScrollBar.Minimum,
                        Math.Min(
                            hScrollBar.Maximum - hScrollBar.LargeChange + 1,
                            newValue));

                hScrollBar.Value = newValue;
            }

            // --------------------------------------------------------
            // 縦方向
            //
            // マウスを下へ動かす
            // → ページを下方向へ見る
            // --------------------------------------------------------
            if (vScrollBar != null)
            {
                int newValue = _previewDragStartVerticalValue - deltaY;

                newValue =
                    Math.Max(
                        vScrollBar.Minimum,
                        Math.Min(
                            vScrollBar.Maximum - vScrollBar.LargeChange + 1,
                            newValue));

                vScrollBar.Value = newValue;
            }

        }

        // ------------------------------------------------------------
        // 右ボタンを離した
        // ------------------------------------------------------------
        private void previewControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            _isPreviewDragging = false;

            previewControl.Cursor = Cursors.Default;

        }
    }
}
