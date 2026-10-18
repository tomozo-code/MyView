using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

// --------------------------------------------------------
// 画像単体の印刷プレビューフォーム
// --------------------------------------------------------

// 印刷設定値は、「PrintSettings2.txt」へ保存

namespace MyView
{
    public partial class Form5 : Form
    {

        private readonly PrintDocument _printDocument = new PrintDocument();

        // 印刷対象の画像パス
        private readonly string _imagePath;

        private bool _isLoadingSettings = false;

        // 印刷対象の画像
        private Image? _printImage;

        // 実際の印刷中かどうか
        private bool _isActualPrinting = false;

        private bool _isRestoringPrinter = false;

        private bool _isSyncingPrinterSettings = false;


        // 前回終了時(Form5を閉じたとき)の印刷設定保存
        // C:\Users\ユーザー名\AppData\Local\MyView\LastFolder.txt
        private string PrintSettingsFilePath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MyView",
                "PrintSettings2.txt");

        // ============================================================
        // コンストラクタ
        // ============================================================

        // ファイルパスを受け取る
        public Form5(string imagePath) : this(imagePath, null)
        {

        }

        // 画像データを直接受け取る
        public Form5(Image image) : this(null, image)
        {

        }

        // フォームの初期化と画像の読み込みをまとめて行う
        private Form5(string? imagePath, Image? image)
        {
            InitializeComponent();

            // Form5でKeyDownを拾えるように
            this.KeyPreview = true;

            //_imagePath = imagePath;
            _imagePath = imagePath ?? string.Empty;

            // フォームサイズ
            this.Width = 1000;
            this.Height = 700;
            this.MinimumSize = new Size(400, 400);
            //this.WindowState = FormWindowState.Maximized;

            toolStripContainer1.Dock = DockStyle.Fill;

            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.SplitterDistance = 400;
            splitContainer1.Panel1MinSize = 200;

            panel2.Dock = DockStyle.Fill;
            panel2.BackColor = Color.DimGray;

            previewControl.Dock = DockStyle.Fill;
            previewControl.BackColor = Color.DimGray;

            setDirection.Items.AddRange(new string[] { "縦", "横" });
            setDirection.SelectedIndex = 1;

            // PrintPreviewControl に Document を割り当て
            previewControl.Document = _printDocument;

            // 印刷ページの描画イベント
            _printDocument.PrintPage += PrintDocument_PrintPage;

            // 画像を読み込む
            if (image != null)
            {
                // Form3の画像とは別の画像を作り、Form5側で管理する
                _printImage = new Bitmap(image);
            }
            else if (!string.IsNullOrEmpty(_imagePath) && File.Exists(_imagePath))
            {
                // 従来どおり、ファイルから画像を読み込む
                _printImage = Image.FromFile(_imagePath);
            }
            else
            {
                MessageBox.Show(
                    "印刷する画像を読み込めませんでした。",
                    "印刷プレビュー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            /*
            if (!string.IsNullOrEmpty(_imagePath) && File.Exists(_imagePath))
            {
                _printImage = Image.FromFile(_imagePath);
            }
            else
            {
                MessageBox.Show(
                    "印刷する画像を読み込めませんでした。",
                    "画像印刷",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            */

            // 印刷設定の初期値
            // A4(210mm×297mm)・幅・高さ
            _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4 (210x297mm)", 827, 1169);
            // 縦:false、横:ture
            _printDocument.DefaultPageSettings.Landscape = false;
            // 余白0mm 指定は1/100インチ
            int margin = (int)Math.Round(0 / 25.4 * 100);

            _printDocument.DefaultPageSettings.Margins =
                new Margins(
                    margin,
                    margin,
                    margin,
                    margin);

            setMarginLeft.Text = "0.00";
            setMarginRight.Text = "0.00";
            setMarginTop.Text = "0.00";
            setMarginBottom.Text = "0.00";



        }

        // ============================================================
        // フォームを開いたとき
        // ============================================================
        private void Form5_Load(object sender, EventArgs e)
        {
            _isLoadingSettings = true;

            try
            {
                // 保存されている印刷設定を読み込む
                LoadPrintSettings();

                // プリンター一覧を読み込む
                LoadPrinterName();

                // 用紙一覧を読み込む
                if (setPrinterName.SelectedItem != null)
                {
                    LoadPaperSizes();
                }

                // 向きの選択状態を同期する
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
                setMarginLeft.Text = "0.00";
                setMarginRight.Text = "0.00";
                setMarginTop.Text = "0.00";
                setMarginBottom.Text = "0.00";

                _printDocument.DefaultPageSettings.Margins =
                    new Margins(
                        MmToHundredthsInch(0.00m),
                        MmToHundredthsInch(0.00m),
                        MmToHundredthsInch(0.00m),
                        MmToHundredthsInch(0.00m));
            }

            // プレビュー更新
            UpdatePreview();
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
                decimal marginLeftMm = 0.00m;
                decimal marginRightMm = 0.00m;
                decimal marginTopMm = 0.00m;
                decimal marginBottomMm = 0.00m;

                foreach (string line in lines)
                {
                    string[] parts = line.Split('=', 2);

                    if (parts.Length != 2)
                        continue;

                    string key = parts[0];
                    string value = parts[1];

                    switch (key)
                    {
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
        // 閉じるを押したとき
        // ============================================================
        private void closeBtn_Click(object sender, EventArgs e)
        {
            // 現在のフォームを閉じる
            this.Close();
        }

        // ============================================================
        // フォーム閉じる時に画像を解放する
        // ============================================================
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _printDocument.PrintPage -= PrintDocument_PrintPage;

            _printImage?.Dispose();
            _printImage = null;

            _printDocument.Dispose();

            base.OnFormClosed(e);
        }

        // ============================================================
        // フォームを閉じるとき
        // ============================================================
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
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
        // 印刷ページの描画
        // ============================================================
        private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
        {
            if (_printImage == null)
            {
                e.HasMorePages = false;
                return;
            }

            Graphics? g = e.Graphics;

            if (g == null || _printImage == null)
            {
                e.HasMorePages = false;
                return;
            }

            // 実際の印刷時だけ、プリンターのハードマージンを補正
            // プレビューには適用しない
            if (_isActualPrinting)
            {
                g.TranslateTransform(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);
            }

            // 余白を除いた印刷可能領域
            Rectangle bounds = e.MarginBounds;

            // 画像全体が収まる倍率を計算する
            float scaleX = (float)bounds.Width / _printImage.Width;
            float scaleY = (float)bounds.Height / _printImage.Height;
            float scale = Math.Min(scaleX, scaleY);

            // 縦横比を維持した描画サイズ
            int width = (int)(_printImage.Width * scale);
            int height = (int)(_printImage.Height * scale);

            // 印刷可能領域の中央に配置する
            int x = bounds.Left + (bounds.Width - width) / 2;
            int y = bounds.Top + (bounds.Height - height) / 2;

            g.DrawImage(_printImage, new Rectangle(x, y, width, height));

            e.HasMorePages = false;
        }

        // ============================================================
        // プリンター一覧を読み込む
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
        // 選択したプリンターの用紙一覧
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
        // プリンター変更
        // ============================================================
        private void setPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_isLoadingSettings)
                //return;

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
            string newPrinterName = setPrinterName.SelectedItem.ToString()!;

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

            // 選択したプリンターに対応する用紙一覧を読み込む
            LoadPaperSizes();

            btnHaichi.PerformClick();

        }

        // ============================================================
        // プリンタ変更後の設定を同期・余白チェック
        // ============================================================
        private bool ApplyPrinterChange(string oldPrinterName, string? oldPaperName, int oldPaperWidth, int oldPaperHeight, bool oldLandscape, Margins oldMargins)
        {
            // 変更前の用紙情報を保存
            PaperSize oldPaperSize = _printDocument.DefaultPageSettings.PaperSize;

            // 新しいプリンタの用紙一覧を取得
            _isSyncingPrinterSettings = true;

            try
            {
                // プリンタ変更後は、PrintDocument内部の
                // DefaultPageSettings.PaperSize が新プリンタの
                // 既定用紙に変わることがある
                LoadPaperSizes();
                UpdatePrinterInfo();

                // まず「同じ用紙種類」を RawKind で探す
                PaperSize? newPaperSize = null;

                if (oldPaperSize.RawKind != 0)
                {
                    newPaperSize =
                        _printDocument.PrinterSettings.PaperSizes
                            .Cast<PaperSize>()
                            .FirstOrDefault(p =>
                                p.RawKind == oldPaperSize.RawKind);
                }

                // RawKindで見つからなければ、用紙名で探す
                if (newPaperSize == null &&
                    !string.IsNullOrEmpty(oldPaperName))
                {
                    newPaperSize =
                        _printDocument.PrinterSettings.PaperSizes
                            .Cast<PaperSize>()
                            .FirstOrDefault(p =>
                                string.Equals(
                                    p.PaperName,
                                    oldPaperName,
                                    StringComparison.OrdinalIgnoreCase));
                }

                // 同じ用紙が新プリンタに存在した
                if (newPaperSize != null)
                {
                    // UIを新しいプリンタ側の用紙名に合わせる
                    int newPaperIndex = setPaper.Items.IndexOf(newPaperSize.PaperName);

                    if (newPaperIndex >= 0)
                    {
                        setPaper.SelectedIndex = newPaperIndex;
                    }

                    // PrintDocument内部にも反映
                    _printDocument.DefaultPageSettings.PaperSize = newPaperSize;
                }
                else
                {
                    // 同じ用紙が存在しない場合
                    // LoadPaperSizes() が選択した
                    // 新プリンタの先頭用紙をそのまま使用する
                    if (setPaper.SelectedItem != null)
                    {
                        string paperName = setPaper.SelectedItem.ToString()!;

                        PaperSize? firstPaper =
                            _printDocument.PrinterSettings.PaperSizes
                                .Cast<PaperSize>()
                                .FirstOrDefault(p =>
                                    string.Equals(
                                        p.PaperName,
                                        paperName,
                                        StringComparison.OrdinalIgnoreCase));

                        if (firstPaper != null)
                        {
                            _printDocument.DefaultPageSettings.PaperSize = firstPaper;
                        }
                    }
                }
            }
            finally
            {
                _isSyncingPrinterSettings = false;
            }

            // 新しいプリンタ・用紙で余白チェック
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
                PaperSize? restoredPaperSize =
                    _printDocument.PrinterSettings.PaperSizes
                        .Cast<PaperSize>()
                        .FirstOrDefault(p =>
                            p.RawKind == oldPaperSize.RawKind);

                if (restoredPaperSize == null &&
                    !string.IsNullOrEmpty(oldPaperName))
                {
                    restoredPaperSize =
                        _printDocument.PrinterSettings.PaperSizes
                            .Cast<PaperSize>()
                            .FirstOrDefault(p =>
                                string.Equals(
                                    p.PaperName,
                                    oldPaperName,
                                    StringComparison.OrdinalIgnoreCase));
                }

                if (restoredPaperSize != null)
                {
                    _printDocument.DefaultPageSettings.PaperSize = restoredPaperSize;
                }
                else
                {
                    _printDocument.DefaultPageSettings.PaperSize =
                        new PaperSize(
                            oldPaperName ?? "A4 (210x297mm)",
                            oldPaperWidth,
                            oldPaperHeight);
                }

                // 元の向き・余白を復元
                _printDocument.DefaultPageSettings.Landscape = oldLandscape;

                _printDocument.DefaultPageSettings.Margins = oldMargins;

                // プリンタ名をUIへ反映
                UpdatePrinterInfo();

                // 元の用紙をUIへ反映
                if (restoredPaperSize != null)
                {
                    int restoredPaperIndex = setPaper.Items.IndexOf(restoredPaperSize.PaperName);

                    if (restoredPaperIndex >= 0)
                    {
                        setPaper.SelectedIndex = restoredPaperIndex;
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
        // プリンタ名ラベルの更新
        // ============================================================
        private void UpdatePrinterInfo()
        {
            // 用紙の向き 縦:1、横:0
            setDirection.SelectedIndex = _printDocument.DefaultPageSettings.Landscape ? 1 : 0;

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
        // プレビューボタンを押したとき
        // ============================================================
        private void btnHaichi_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ApplyPrintSettings())
                    return;

                UpdatePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"印刷設定を反映できませんでした。\n\n{ex.Message}",
                    "画像印刷",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 画面の設定をPrintDocumentに反映
        // ============================================================
        private bool ApplyPrintSettings()
        {
            // プリンターの確認
            string? printerName = setPrinterName.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(printerName))
            {
                MessageBox.Show(
                    "プリンターを選択してください。",
                    "印刷設定",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            _printDocument.PrinterSettings.PrinterName = printerName;

            if (!_printDocument.PrinterSettings.IsValid)
            {
                MessageBox.Show(
                    "選択したプリンターを使用できません。",
                    "印刷設定",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            // 用紙サイズの確認
            string? paperName = setPaper.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(paperName))
            {
                MessageBox.Show(
                    "用紙サイズを選択してください。",
                    "印刷設定",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            PaperSize? selectedPaper = null;

            foreach (PaperSize paper in _printDocument.PrinterSettings.PaperSizes)
            {
                if (string.Equals(
                    paper.PaperName,
                    paperName,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedPaper = paper;
                    break;
                }
            }

            if (selectedPaper == null)
            {
                MessageBox.Show(
                    "選択した用紙サイズがプリンターにありません。",
                    "印刷設定",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            // 印刷方向
            bool landscape = setDirection.SelectedItem?.ToString() == "横";

            // 余白を検証して反映
            if (!ApplyMargins())
            {
                // 入力エラーがあるため、設定の適用を中止する
                return false;
            }

            // 検証に成功した設定を反映
            _printDocument.DefaultPageSettings.PaperSize = selectedPaper;
            _printDocument.DefaultPageSettings.Landscape = landscape;

            return true;
        }


        // ============================================================
        // プレビュー更新
        // ============================================================
        private void UpdatePreview()
        {
            previewControl.InvalidatePreview();
            previewControl.Invalidate();
            previewControl.Update();
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
        // 印刷ボタンを押したとき
        // ============================================================
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // 印刷前に設定を検証する
                if (!ApplyPrintSettings())
                    return;

                using (PrintDialog pd = new PrintDialog())
                {
                    pd.Document = _printDocument;
                    pd.AllowSomePages = false;
                    pd.AllowSelection = false;

                    pd.PrinterSettings.PrintRange = PrintRange.AllPages;

                    if (pd.ShowDialog() != DialogResult.OK)
                        return;

                    // プレビュー描画と実印刷を区別
                    _isActualPrinting = true;

                    try
                    {
                        _printDocument.Print();
                    }
                    finally
                    {
                        _isActualPrinting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"印刷に失敗しました。\n\n{ex.Message}",
                    "画像印刷",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // 用紙の向きを変更したとき
        // ============================================================
        private void setDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingSettings)
                return;

            btnHaichi.PerformClick();
        }

        // ============================================================
        // 用紙サイズを変更したとき
        // ============================================================
        private void setPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingSettings)
                return;

            btnHaichi.PerformClick();
        }

        // ============================================================
        // キー操作
        // ============================================================
        private void Form5_KeyDown(object sender, KeyEventArgs e)
        {

            // Ctrl + P → 印刷
            if (e.Control && e.KeyCode == Keys.P)
            {
                // 印刷プレビュー表示
                btnPrint.PerformClick();

                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }

        // ============================================================
        // Escキーをフォーム全体で処理
        // ============================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}
