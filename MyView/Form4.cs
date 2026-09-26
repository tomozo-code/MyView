using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private bool _isLoadingSettings = false;

        private bool _isRestoringPrinter = false;

        private bool _isSyncingPrinterSettings = false;

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

            // Form4でKeyDownを拾えるように
            this.KeyPreview = true;

            // フォームサイズ
            this.Width = 1000;
            this.Height = 700;
            this.MinimumSize = new Size(400, 400);
            this.WindowState = FormWindowState.Maximized;

            toolStripContainer1.Dock = DockStyle.Fill;

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.SplitterDistance = 100;
            splitContainer1.Panel1MinSize = 200;

            previewControl.Dock = DockStyle.Fill;
            previewControl.BackColor = Color.DimGray;

            panel2.BackColor = Color.DimGray;
            panel2.Dock = DockStyle.Fill;

            //panel1.BackColor = Color.DimGray;

            setDpi.Items.AddRange(new string[] { "100", "200", "300", "400", "500", "600" });
            setDpi.SelectedIndex = 2;

            setDirection.Items.AddRange(new string[] { "縦", "横" });
            setDirection.SelectedIndex = 0;

            setFileName.Items.AddRange(new string[] { "拡張子あり", "拡張子なし", "ファイル名なし" });
            setFileName.SelectedIndex = 0;

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

            setMarginLeft.Text = "10.00";
            setMarginRight.Text = "10.00";
            setMarginTop.Text = "10.00";
            setMarginBottom.Text = "10.00";

            // ページ設定は非表示にしておく
            btnPageSet.Visible = false;

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
            tate.MouseMove += NumericUpDown_MouseMove;
            yoko.MouseMove += NumericUpDown_MouseMove;
            setFontSize.MouseMove += NumericUpDown_MouseMove;

            // 印刷ページ描画イベントを登録
            _printDocument.PrintPage += PrintDocument_PrintPage;

            // マウスホイールイベント
            previewControl.MouseWheel += PreviewControl_MouseWheel;
            previewControl.Focus();
        }

        // ============================================================
        // マウスONで説明 
        // ============================================================
        private void Menu_MouseEnter(object? sender, EventArgs e)
        {
            string? message = null;

            // 通常のコントロール（Button, TextBox など）
            if (sender is Control control)
            {
                Control? target = control;

                // Tagが見つかるまで親をたどる
                while (target != null)
                {
                    if (target.Tag != null)
                    {
                        message = target.Tag.ToString();
                        break;
                    }

                    target = target.Parent;
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

        // ============================================================
        // マウス離脱 
        // ============================================================
        private void Menu_MouseLeave(object? sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "一覧印刷を行います。";
        }

        // ============================================================
        // NumericUpDown上で説明
        // ============================================================
        private void NumericUpDown_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown nud &&
                nud.Tag != null)
            {
                string message = nud.Tag.ToString()!;

                if (toolStripStatusLabel1.Text != message)
                {
                    toolStripStatusLabel1.Text = message;
                }

                toolTip1.SetToolTip(nud, message);
            }
        }

        // ============================================================
        // フォームを閉じるとき
        // ============================================================
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 余白チェック
            if (!ApplyMargins())
            {
                // エラーがある場合はフォームを閉じない
                e.Cancel = true;
                return;
            }

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

                string paperName = setPaper.SelectedItem?.ToString()!;

                PaperSize? paperSize =
                    _printDocument.PrinterSettings.PaperSizes
                        .Cast<PaperSize>()
                        .FirstOrDefault(p =>
                            string.Equals(
                                p.PaperName,
                                paperName,
                                StringComparison.OrdinalIgnoreCase));


                var lines = new List<string>
                {
                    // 縦配置数
                    $"tate={tate.Value}",
                    // 横配置数
                    $"yoko={yoko.Value}", 
                     // 解像度
                    $"dpi={setDpi.Text}",
                    // ファイル名の表示
                    $"fileName={setFileName.Text}",
                    // フォント
                    $"font={setFont.Text}",
                    // フォントサイズ
                    $"fontSize={setFontSize.Value}",
                    // プリンタ名
                    $"printerName={_printDocument.PrinterSettings.PrinterName}",
                    // 用紙
                    $"paperName={paperSize?.PaperName}",
                    $"paperWidth={paperSize?.Width}",
                    $"paperHeight={paperSize?.Height}",
                    // 用紙の方向(縦・横)
                    $"landscape={_printDocument.DefaultPageSettings.Landscape}",
                    // 余白(mmで保存)
                    $"marginLeft={setMarginLeft.Text}",
                    $"marginRight={setMarginRight.Text}",
                    $"marginTop={setMarginTop.Text}",
                    $"marginBottom={setMarginBottom.Text}"
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
            // Ctrl + ホイール：マウス位置を基準にズーム
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

            // 通常のホイール：ページ移動
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
            _isLoadingSettings = true;
            try
            {
                // 印刷設定を読み込む
                LoadPrintSettings();

                // プリンタ一覧を取得
                LoadPrinterName();

                // 選択しているプリンタの用紙取得
                LoadPaperSizes();

                // プリンタ名ラベルの更新
                UpdatePrinterInfo();
            }
            finally
            {
                _isLoadingSettings = false;
            }

            // 設定ファイルの余白をチェック
            // この時点では setPaper / setDirection が設定済み
            if (!ApplyMargins())
            {
                // 不正な余白だった場合は初期値に戻す
                setMarginLeft.Text = "10.00";
                setMarginRight.Text = "10.00";
                setMarginTop.Text = "10.00";
                setMarginBottom.Text = "10.00";

                _printDocument.DefaultPageSettings.Margins =
                    new Margins(
                        MmToHundredthsInch(10.00m),
                        MmToHundredthsInch(10.00m),
                        MmToHundredthsInch(10.00m),
                        MmToHundredthsInch(10.00m));
            }

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
                // プリンタ名(とりあえず空白に)
                string printerName = "";
                // 用紙サイズ
                string paperName = "A4 (210x297mm)";
                int paperWidth = 827;
                int paperHeight = 1169;
                // 用紙方向
                bool landscape = false;
                // 余白（mm）
                decimal marginLeftMm = 10.00m;
                decimal marginRightMm = 10.00m;
                decimal marginTopMm = 10.00m;
                decimal marginBottomMm = 10.00m;
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
                        // ファイル名
                        case "fileName":
                            if (setFileName.Items.Contains(value))
                            {
                                setFileName.SelectedItem = value;
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
                        // プリンタ名
                        case "printerName":
                            printerName = value;
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
                            decimal.TryParse(value, out marginLeftMm);
                            break;

                        case "marginRight":
                            decimal.TryParse(value, out marginRightMm);
                            break;

                        case "marginTop":
                            decimal.TryParse(value, out marginTopMm);
                            break;

                        case "marginBottom":
                            decimal.TryParse(value, out marginBottomMm);
                            break;
                    }
                }

                // 読み込んだページ設定を PrintDocument に反映
                // 保存されていたプリンタを反映
                if (!string.IsNullOrWhiteSpace(printerName) &&
                    PrinterSettings.InstalledPrinters
                        .Cast<string>()
                        .Any(p =>
                            string.Equals(
                                p,
                                printerName,
                                StringComparison.OrdinalIgnoreCase)))
                {
                    _printDocument.PrinterSettings.PrinterName = printerName;
                }

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
                        MmToHundredthsInch(marginLeftMm),
                        MmToHundredthsInch(marginRightMm),
                        MmToHundredthsInch(marginTopMm),
                        MmToHundredthsInch(marginBottomMm)
                        );

                // 読み込んだ余白をTextBoxに表示
                setMarginLeft.Text = marginLeftMm.ToString("F2");
                setMarginRight.Text = marginRightMm.ToString("F2");
                setMarginTop.Text = marginTopMm.ToString("F2");
                setMarginBottom.Text = marginBottomMm.ToString("F2");

            }
            catch
            {
                // 読み込み失敗時は初期設定のまま
            }
        }

        // ============================================================
        // プリンタ一覧を取得
        // ============================================================
        private void LoadPrinterName()
        {
            setPrinterName.Items.Clear();

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                setPrinterName.Items.Add(printerName);
            }

            // 現在のプリンタを選択
            string currentPrinter = _printDocument.PrinterSettings.PrinterName;

            int index = setPrinterName.Items.IndexOf(currentPrinter);

            if (index >= 0)
            {
                setPrinterName.SelectedIndex = index;
            }
            else if (setPrinterName.Items.Count > 0)
            {
                setPrinterName.SelectedIndex = 0;
            }

        }

        // ============================================================
        // 選択しているプリンタの用紙取得
        // ============================================================
        private void LoadPaperSizes()
        {
            setPaper.Items.Clear();

            foreach (PaperSize paperSize in
                _printDocument.PrinterSettings.PaperSizes)
            {
                setPaper.Items.Add(paperSize.PaperName);
            }

            // プリンタの既定用紙を選択
            int index = setPaper.Items.IndexOf(_printDocument.DefaultPageSettings.PaperSize.PaperName);

            if (index >= 0)
            {
                setPaper.SelectedIndex = index;
            }
            else if (setPaper.Items.Count > 0)
            {
                setPaper.SelectedIndex = 0;
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
            //string landscapelabel = "縦";
            if (_printDocument.DefaultPageSettings.Landscape)
            {
                //landscapelabel = "横";
                setDirection.SelectedIndex = 1;
            }

            // setPrinterName の選択状態を復元
            int printerIndex = setPrinterName.Items.IndexOf(_printDocument.PrinterSettings.PrinterName);

            if (printerIndex >= 0)
            {
                setPrinterName.SelectedIndex = printerIndex;
            }
            else if (setPrinterName.Items.Count > 0)
            {
                setPrinterName.SelectedIndex = 0;
            }
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

                    float fileNameHeight = setFileName.SelectedIndex == 2 ? 0 : 20;

                    RectangleF imageRect =
                        new RectangleF(
                            cellRect.X + 2,
                            cellRect.Y + 2,
                            cellRect.Width - 2,
                            cellRect.Height -
                                fileNameHeight - 6);

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
                    string fileName = "";
                    switch (setFileName.SelectedIndex)
                    {
                        case 0:
                            // ファイル名＋拡張子
                            fileName = Path.GetFileName(_imageFiles[imageIndex]);
                            break;

                        case 1:
                            // ファイル名のみ
                            fileName = Path.GetFileNameWithoutExtension(_imageFiles[imageIndex]);
                            break;

                        case 2:
                            // ファイル名なし
                            fileName = "";
                            break;
                    }

                    // ファイル名を表示
                    if (setFileName.SelectedIndex != 2)
                    {
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
                // ----------------------------------------------------
                // PrintDialogを開く前の設定を保存
                // ----------------------------------------------------
                string oldPrinterName =
                    _printDocument.PrinterSettings.PrinterName;

                PaperSize oldPaperSize =
                    _printDocument.DefaultPageSettings.PaperSize;

                string? oldPaperName =
                    setPaper.SelectedItem?.ToString();

                int oldPaperWidth = oldPaperSize.Width;
                int oldPaperHeight = oldPaperSize.Height;

                bool oldLandscape =
                    _printDocument.DefaultPageSettings.Landscape;

                Margins oldMargins =
                    _printDocument.DefaultPageSettings.Margins;

                using (PrintDialog pd = new PrintDialog())
                {
                    pd.Document = _printDocument;

                    if (pd.ShowDialog() != DialogResult.OK)
                        return;

                    // ------------------------------------------------
                    // プリンタ変更後の共通処理
                    // ------------------------------------------------
                    if (!ApplyPrinterChange(
                        oldPrinterName,
                        oldPaperName,
                        oldPaperWidth,
                        oldPaperHeight,
                        oldLandscape,
                        oldMargins))
                    {
                        return;
                    }

                    // ------------------------------------------------
                    // 問題なければ印刷
                    // ------------------------------------------------
                    _currentPageIndex = 0;
                    _printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "印刷エラー\n" + ex.Message);
            }
        }

        // ============================================================
        // プリンタ設定を押したとき
        // ============================================================
        private void btnPrintSet_Click(object sender, EventArgs e)
        {
            // --------------------------------------------------------
            // PrintDialogを開く前の設定を保存
            // --------------------------------------------------------
            string oldPrinterName =
                _printDocument.PrinterSettings.PrinterName;

            PaperSize oldPaperSize =
                _printDocument.DefaultPageSettings.PaperSize;

            string? oldPaperName =
                setPaper.SelectedItem?.ToString();

            int oldPaperWidth = oldPaperSize.Width;
            int oldPaperHeight = oldPaperSize.Height;

            bool oldLandscape =
                _printDocument.DefaultPageSettings.Landscape;

            Margins oldMargins =
                _printDocument.DefaultPageSettings.Margins;

            using (PrintDialog pd = new PrintDialog())
            {
                pd.Document = _printDocument;

                if (pd.ShowDialog() != DialogResult.OK)
                    return;

                // ----------------------------------------------------
                // PrintDialogで変更されたプリンタを共通処理
                // ----------------------------------------------------
                if (!ApplyPrinterChange(
                    oldPrinterName,
                    oldPaperName,
                    oldPaperWidth,
                    oldPaperHeight,
                    oldLandscape,
                    oldMargins))
                {
                    return;
                }

                // ----------------------------------------------------
                // 問題なければプレビュー更新
                // ----------------------------------------------------
                RefreshPreview();
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
                    // 選択しているプリンタの用紙取得
                    LoadPaperSizes();
                    // プリンタ名ラベルの更新
                    UpdatePrinterInfo();
                    // プレビューの再描画更新
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
        // プレビューを押したとき
        // ============================================================
        private void btnPreview_Click(object sender, EventArgs e)
        {
            // 用紙
            if (setPaper.SelectedItem == null)
                return;

            string paperName = setPaper.SelectedItem.ToString()!;

            PaperSize? paperSize =
                _printDocument.PrinterSettings.PaperSizes
                    .Cast<PaperSize>()
                    .FirstOrDefault(p =>
                        string.Equals(
                            p.PaperName,
                            paperName,
                            StringComparison.OrdinalIgnoreCase));

            if (paperSize == null)
                return;

            // まず、選択中の用紙を使って余白チェック
            if (!ApplyMargins())
                return;

            // 問題なければ、ここで初めて用紙を内部設定へ反映
            _printDocument.DefaultPageSettings.PaperSize = paperSize;

            // 用紙の向き
            _printDocument.DefaultPageSettings.Landscape =
                setDirection.SelectedIndex != 0;

            // 1ページ目からプレビューを再描画
            RefreshPreview();
        }

        // ============================================================
        // 横スクロールバーを取得
        // ============================================================
        private HScrollBar? GetPreviewHorizontalScrollBar()
        {
            return previewControl.Controls
                .OfType<HScrollBar>()
                .FirstOrDefault();
        }

        // ============================================================
        // 縦スクロールバーを取得
        // ============================================================
        private VScrollBar? GetPreviewVerticalScrollBar()
        {
            return previewControl.Controls
                .OfType<VScrollBar>()
                .FirstOrDefault();
        }

        // ============================================================
        // 右ボタンを押した
        // ============================================================
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

        // ============================================================
        // 右ボタンを押したまま移動
        // ============================================================
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

        // ============================================================
        // 右ボタンを離した
        // ============================================================
        private void previewControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            _isPreviewDragging = false;

            previewControl.Cursor = Cursors.Default;

        }

        // ============================================================
        // 数値入力共通ルーチン(クリックで全選択)
        // ============================================================
        private void NumericUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                BeginInvoke(new Action(() =>
                {
                    nud.Select(0, nud.Text.Length);
                }));
            }
        }

        // ============================================================
        // 数値入力共通ルーチン(クリック・Tabで全選択)
        // ============================================================
        private void NumericUpDown_Enter(object sender, EventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                BeginInvoke(new Action(() =>
                {
                    nud.Select(0, nud.Text.Length);
                }));
            }
        }

        // ============================================================
        // プリンタ変更後の設定を同期・余白チェック
        // ============================================================
        private bool ApplyPrinterChange(string oldPrinterName, string? oldPaperName, int oldPaperWidth, int oldPaperHeight, bool oldLandscape, Margins oldMargins)
        {
            // 新しいプリンタの用紙一覧を取得
            _isSyncingPrinterSettings = true;

            try
            {
                LoadPaperSizes();
                UpdatePrinterInfo();
            }
            finally
            {
                _isSyncingPrinterSettings = false;
            }

            // 新しいプリンタで余白チェック
            if (ApplyMargins())
            {
                return true;
            }

            // 余白NG → 元のプリンタへ戻す
            _isRestoringPrinter = true;

            try
            {
                _printDocument.PrinterSettings.PrinterName = oldPrinterName;

                // 元のプリンタの用紙一覧を取得
                LoadPaperSizes();

                // 元の用紙を選択
                if (!string.IsNullOrEmpty(oldPaperName))
                {
                    int oldPaperIndex = setPaper.Items.IndexOf(oldPaperName);

                    if (oldPaperIndex >= 0)
                    {
                        setPaper.SelectedIndex = oldPaperIndex;
                    }
                }

                // 元の用紙を内部設定へ戻す
                PaperSize? oldPaperSize =
                    _printDocument.PrinterSettings.PaperSizes
                        .Cast<PaperSize>()
                        .FirstOrDefault(p =>
                            string.Equals(
                                p.PaperName,
                                oldPaperName,
                                StringComparison.OrdinalIgnoreCase));

                if (oldPaperSize != null)
                {
                    _printDocument.DefaultPageSettings.PaperSize = oldPaperSize;
                }
                else
                {
                    _printDocument.DefaultPageSettings.PaperSize =
                        new PaperSize(
                            oldPaperName ?? "A4 (210x297mm)",
                            oldPaperWidth,
                            oldPaperHeight);
                }

                // 元の向き
                _printDocument.DefaultPageSettings.Landscape = oldLandscape;

                // 元の余白
                _printDocument.DefaultPageSettings.Margins = oldMargins;

                // プリンタ名・用紙・向きをUIへ反映
                UpdatePrinterInfo();

                if (!string.IsNullOrEmpty(oldPaperName))
                {
                    int oldPaperIndex = setPaper.Items.IndexOf(oldPaperName);

                    if (oldPaperIndex >= 0)
                    {
                        setPaper.SelectedIndex = oldPaperIndex;
                    }
                }
            }
            finally
            {
                _isRestoringPrinter = false;
            }

            return false;
        }

        // ============================================================
        // プリンタ名(setPrinterName)を変更したとき
        // ============================================================
        private void setPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setPrinterName.SelectedItem == null)
                return;

            if (_isRestoringPrinter || _isSyncingPrinterSettings || _isLoadingSettings)
                return;

            // 変更前の設定を保存
            string oldPrinterName = _printDocument.PrinterSettings.PrinterName;

            PaperSize oldPaperSize = _printDocument.DefaultPageSettings.PaperSize;

            string? oldPaperName = setPaper.SelectedItem?.ToString();

            int oldPaperWidth = oldPaperSize.Width;
            int oldPaperHeight = oldPaperSize.Height;

            bool oldLandscape = _printDocument.DefaultPageSettings.Landscape;

            Margins oldMargins = _printDocument.DefaultPageSettings.Margins;

            // 新しいプリンタへ変更
            string newPrinterName =
                setPrinterName.SelectedItem.ToString()!;

            _printDocument.PrinterSettings.PrinterName = newPrinterName;

            // プリンタ変更後の共通処理
            if (!ApplyPrinterChange(
                oldPrinterName,
                oldPaperName,
                oldPaperWidth,
                oldPaperHeight,
                oldLandscape,
                oldMargins))
            {
                return;
            }

            // 問題なければプレビュー更新
            RefreshPreview();
        }

        // ============================================================
        // 余白 入力チェック(数値と小数点のみ可)
        // ============================================================
        private void setMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.')
            {
                TextBox textBox = (TextBox)sender;

                // 小数点を2個以上入力させない
                if (!textBox.Text.Contains('.'))
                    return;
            }

            e.Handled = true;
        }

        // ============================================================
        // 余白 入力値をセット
        // ============================================================
        private void setMargin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            ApplyMargins();
        }

        // ============================================================
        // 余白入力欄からフォーカスが離れたとき
        // ============================================================
        private void setMargin_Validating(object sender, CancelEventArgs e)
        {
            if (!ApplyMargins())
            {
                // エラーならフォーカス移動をキャンセル
                e.Cancel = true;
            }
        }

        // ============================================================
        // 余白 入力をクリックしたとき全選択
        // ============================================================
        private void setMargin_Click(object sender, EventArgs e)
        {
            if (sender is TextBox nud)
            {
                BeginInvoke(new Action(() =>
                {
                    nud.Select(0, nud.Text.Length);
                }));
            }
        }

        // ============================================================
        // 余白 数値変換ルーチン(ミリ→インチ)
        // ============================================================
        private int MmToHundredthsInch(decimal mm)
        {
            return (int)Math.Round(mm / 25.4m * 100m);
        }

        // ============================================================
        // 余白 数値変換ルーチン(インチ→ミリ)
        // ============================================================
        private decimal InchToHundredthsMm(decimal Inch)
        {
            return Math.Round(Inch * 25.4m / 100m, 2);
        }

        // ============================================================
        // 余白設定をチェックして反映
        // ============================================================
        private bool ApplyMargins()
        {
            // 空白チェック
            if (string.IsNullOrWhiteSpace(setMarginLeft.Text) ||
                string.IsNullOrWhiteSpace(setMarginRight.Text) ||
                string.IsNullOrWhiteSpace(setMarginTop.Text) ||
                string.IsNullOrWhiteSpace(setMarginBottom.Text))
            {
                MessageBox.Show(
                    "余白の入力値が空白になっています。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // 数値チェック
            if (!decimal.TryParse(setMarginLeft.Text, out decimal left) ||
                !decimal.TryParse(setMarginRight.Text, out decimal right) ||
                !decimal.TryParse(setMarginTop.Text, out decimal top) ||
                !decimal.TryParse(setMarginBottom.Text, out decimal bottom))
            {
                MessageBox.Show(
                    "余白には数値を入力してください。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // 0未満チェック
            if (left < 0 || right < 0 || top < 0 || bottom < 0)
            {
                MessageBox.Show(
                    "余白には0以上の値を入力してください。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // チェックする用紙サイズ
            // setPaperで選択されている用紙を取得
            if (setPaper.SelectedItem == null)
            {
                MessageBox.Show(
                    "用紙が選択されていません。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            string paperName = setPaper.SelectedItem.ToString()!;

            PaperSize? paperSize =
                _printDocument.PrinterSettings.PaperSizes
                    .Cast<PaperSize>()
                    .FirstOrDefault(p =>
                        string.Equals(
                            p.PaperName,
                            paperName,
                            StringComparison.OrdinalIgnoreCase));

            if (paperSize == null)
            {
                MessageBox.Show(
                    "選択されている用紙サイズを取得できません。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            // 用紙サイズをmmに変換
            // PaperSize.Width / Height は 1/100インチ
            decimal paperWidthMm = paperSize.Width * 25.4m / 100m;

            decimal paperHeightMm = paperSize.Height * 25.4m / 100m;

            // 「横」が選択されている場合は、幅と高さを入れ替える
            if (setDirection.SelectedIndex == 1)
            {
                (paperWidthMm, paperHeightMm) = (paperHeightMm, paperWidthMm);
            }

            // 横方向チェック
            if (left + right >= paperWidthMm)
            {
                MessageBox.Show(
                    $"左右の余白が用紙幅を超えています。\n\n" +
                    $"用紙：{paperSize.PaperName}\n" +
                    $"用紙幅：{paperWidthMm:F2} mm\n" +
                    $"左：{left:F2} mm\n" +
                    $"右：{right:F2} mm",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // 初期値を格納する
                setMarginLeft.Text = "10.00";
                setMarginRight.Text = "10.00";

                return false;
            }

            // 縦方向チェック
            if (top + bottom >= paperHeightMm)
            {
                MessageBox.Show(
                    $"上下の余白が用紙高さを超えています。\n\n" +
                    $"用紙：{paperSize.PaperName}\n" +
                    $"用紙高さ：{paperHeightMm:F2} mm\n" +
                    $"上：{top:F2} mm\n" +
                    $"下：{bottom:F2} mm",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                // 初期値を格納する
                setMarginTop.Text = "10.00";
                setMarginBottom.Text = "10.00";

                return false;
            }

            // 問題なければ内部設定へ反映
            _printDocument.DefaultPageSettings.Margins =
                new Margins(
                    MmToHundredthsInch(left),
                    MmToHundredthsInch(right),
                    MmToHundredthsInch(top),
                    MmToHundredthsInch(bottom));

            return true;
        }

        // ============================================================
        // ショートカットキーの設定と実行
        // ============================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ctrl + P → 印刷
            if (keyData == (Keys.Control | Keys.P))
            {
                btnPrint.PerformClick();
                return true;
            }

            // Ctrl + R → プリンタ設定
            if (keyData == (Keys.Control | Keys.R))
            {
                btnPrintSet.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ============================================================
        // 用紙サイズを変更した時に余白チェック
        // ============================================================
        private void setPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingSettings || _isRestoringPrinter || _isSyncingPrinterSettings)
                return;

            if (setPaper.SelectedItem != null)
            {
                ApplyMargins();
            }
        }
    }
}
