using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

// --------------------------------------------------------
// 画像に対して色調補正を行うフォーム
// --------------------------------------------------------

namespace MyView
{
    public partial class Form6 : Form
    {

        // ツールチップに表示するヒント文字列
        private string? toolHintTxt = null;

        // 色彩補正用画像
        // オリジナル画像
        private Bitmap _originalImage;
        // 補正プレビュー画像
        private Bitmap _previewImage;


        // 小数の最小値・最大値・ステップ(ガンマ補正用)
        private const double MinValue = 0.00;
        private const double MaxValue = 699.00;
        // 小数ステップ
        private const double Step = 0.01;

        // 補正後の画像を取得する
        public Bitmap? ResultImage { get; private set; }


        // 色調フィルターの種類
        private enum ColorFilter
        {
            None,
            Sepia,
            Grayscale,
            Monochrome,
            Negative
        }

        // 現在選択されているフィルター
        private ColorFilter _currentFilter = ColorFilter.None;

        // ネガポジ反転の対象チャンネル
        private enum NegativeChannel
        {
            All,
            Red,
            Green,
            Blue
        }

        // 現在の反転対象
        private NegativeChannel _negativeChannel = NegativeChannel.All;


        // 画像効果の種類
        private enum ImageEffect
        {
            None,
            Blur,
            Sharpen
        }

        // 現在選択されている画像効果
        private ImageEffect _currentImageEffect = ImageEffect.None;

        private bool _isUpdatingTreeView = false;

        // ============================================================
        // コンストラクタ
        // ============================================================
        public Form6(Image sourceImage)
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 750;
            this.MinimumSize = new Size(500, 500);

            splitContainer1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tabControl1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            embossPanel.Dock = DockStyle.Fill;

            splitContainer1.SplitterDistance = 250;
            splitContainer1.Panel1MinSize = 200;

            splitContainer2.SplitterDistance = 250;
            splitContainer2.Panel1MinSize = 100;

            treeView1.Dock = DockStyle.Fill;
            treeView1.ExpandAll();

            // ぼかしパネル
            blurPanel.Dock = DockStyle.Fill;
            // シャープパネル
            sharpPanel.Dock = DockStyle.Fill;
            // モノクロームパネル
            monochromePanel.Dock = DockStyle.Fill;
            // エンボスパネル
            embossPanel.Dock = DockStyle.Fill;
            // ノイズパネル
            noisePanel.Dock = DockStyle.Fill;
            // 輪郭抽出
            ContourExPanel.Dock = DockStyle.Fill;


            splitContainer2.Dock = DockStyle.Fill;

            // 背景色
            splitContainer1.BackColor = Color.Gainsboro;
            splitContainer1.Panel1.BackColor = this.BackColor;
            splitContainer1.Panel2.BackColor = this.BackColor;


            // オリジナル画像を複製
            _originalImage = new Bitmap(sourceImage);

            // プレビュー画像も複製
            _previewImage = new Bitmap(_originalImage);

            // 左側：オリジナル画像
            pictureBox1.Image = _originalImage;

            // 右側：プレビュー画像
            pictureBox2.Image = _previewImage;

            toolHintTxt = "画像補正を行います。"; ;

            // 初期設定
            SetupComponents();

            //  EscキーをCancelボタンに割り当て
            this.CancelButton = CancelBtn;
        }

        // ==============================
        // マウスONで説明(通常コントロール) Tagに書く 
        // ==============================
        private void Control_MouseEnter(object? sender, EventArgs e)
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


        // ==============================
        // マウス離脱(通常コントロール)
        // ==============================
        private void Control_MouseLeave(object? sender, EventArgs e)
        {
            // 戻す
            toolStripStatusLabel1.Text = toolHintTxt;
        }

        // ==============================
        // マウスONで説明の実行(通常コントロール) Tagに書いたもの 
        // ==============================
        private void SetTooltipAll(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Tag != null)
                {
                    // ツールバーにヒント
                    ctrl.MouseEnter += Control_MouseEnter;
                    ctrl.MouseLeave += Control_MouseLeave;
                    // ツールチップにもヒント
                    toolTip1.SetToolTip(ctrl, ctrl.Tag.ToString());
                }

                // 子コントロールも再帰
                if (ctrl.HasChildren)
                {
                    SetTooltipAll(ctrl);
                }
            }
        }

        // ============================================================
        // テキスト入力をクリックしたとき全選択
        // ============================================================
        private void allSelect_Click(object sender, EventArgs e)
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
        // テキスト入力にフォーカスが移動したとき全選択
        // ============================================================
        private void allSelect_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox nud)
            {
                BeginInvoke(new Action(() =>
                {
                    nud.SelectAll();
                }));
            }
        }

        // ============================================================
        // 初期設定
        // ============================================================
        private void SetupComponents()
        {
            // 色彩補正
            ColorCorrectionReset();

            // 色調加工
            ColorFilterReset();

            // 画像効果
            ImageEffectReset();

            // 設定パネルをすべて非表示にする
            UpdateEffectSettingsPanel(null);

            // TreeViewの選択も解除
            treeView1.SelectedNode = null;

            // ステータス表示を初期状態に戻す
            toolStripStatusLabel1.Text = toolHintTxt;
        }

        // ============================================================
        // TreeViewの指定した親ノード以下のチェックを解除
        // ============================================================
        private void ResetTreeViewChecks(string parentNodeText)
        {
            TreeNode? parentNode = treeView1.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == parentNodeText);

            if (parentNode == null)
                return;

            foreach (TreeNode node in parentNode.Nodes)
            {
                node.Checked = false;
            }
        }

        // ============================================================
        // 色彩補正リセットを押したとき
        // ============================================================
        private void colorCorrectionResetBtn_Click(object sender, EventArgs e)
        {
            // 色彩補正を初期化(リセット)
            ColorCorrectionReset();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色彩補正を初期化(リセット)
        // ============================================================
        private void ColorCorrectionReset()
        {
            // 明るさ
            BrightnessBar.Value = 0;
            BrightnessTxt.Text = "0";

            // 色バランス
            // R
            ColorBalanceBar_R.Value = 0;
            ColorBalanceTxt_R.Text = "0";
            // G
            ColorBalanceBar_G.Value = 0;
            ColorBalanceTxt_G.Text = "0";
            // B
            ColorBalanceBar_B.Value = 0;
            ColorBalanceTxt_B.Text = "0";

            // コントラスト
            contrastBar.Value = 0;
            contrastTxt.Text = "0";

            // ガンマ補正
            GammaCorrectionBar.Value = 100;
            // TrackBarの整数値を小数に変換
            double decimalValue = MinValue + GammaCorrectionBar.Value * Step;
            GammaCorrectionTxt.Text = $"{decimalValue:F2}";

            // 彩度
            SaturationBar.Value = 0;
            SaturationTxt.Text = "0";

            toolStripStatusLabel1.Text = toolHintTxt;

        }

        // ============================================================
        // 画像効果リセットを押したとき
        // ============================================================
        private void colorFilterResetBtn_Click(object sender, EventArgs e)
        {
            // セピア、グレースケール、モノクローム、ネガポジを初期化(リセット)
            ColorFilterReset();

            // ぼかし、シャープ、エンボスを初期化(リセット)
            ImageEffectReset();

            // 色調フィルターに応じてモノクローム設定を有効・無効にする
            UpdateFilterSettings();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // セピア、グレースケール、モノクローム、ネガポジを初期化(リセット)
        // ============================================================
        private void ColorFilterReset()
        {
            // 色調加工のTreeViewチェックを解除
            ResetTreeViewChecks("色調加工");

            // モノクロームのしきい値
            monochromeBar.Value = 128;
            monochromeTxt.Text = "128";

            // フィルターの選択状態
            _currentFilter = ColorFilter.None;

            // ネガポジ反転の対象
            _negativeChannel = NegativeChannel.All;

            // モノクローム設定を無効化
            UpdateFilterSettings();
        }

        // ============================================================
        // ぼかし、シャープ、エンボスを初期化(リセット)
        // ============================================================
        private void ImageEffectReset()
        {
            // 基本補正のTreeViewチェックを解除
            ResetTreeViewChecks("基本補正");

            // エンボスのTreeViewチェックも解除
            ResetTreeViewChecks("輪郭・質感");

            // ぼかし
            blurBar.Value = 0;
            blurTxt.Text = "0";

            // シャープ
            sharpBar.Value = 0;
            sharpTxt.Text = "0";

            // エンボス
            embossBar.Value = 0;
            embossTxt.Text = "0";
            embossCheck.Checked = false;

            // ノイズ
            noiseBar.Value = 0;
            noiseTxt.Text = "0";

            // 輪郭抽出
            ContourExBar.Value = 0;
            ContourExTxt.Text = "0";

            // 画像効果の選択状態
            _currentImageEffect = ImageEffect.None;
        }


        // ============================================================
        // Form6終了時 画像を解放する
        // ============================================================
        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PictureBoxから画像を切り離す
            pictureBox1.Image = null;
            pictureBox2.Image = null;

            // 画像を解放
            _originalImage.Dispose();
            _previewImage.Dispose();
        }

        // ============================================================
        // キャンセルを押したとき
        // ============================================================
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ============================================================
        // リセットボタンを押したとき
        // ============================================================
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            // 色彩補正とフィルターを初期状態に戻す
            SetupComponents();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // OKボタンをクリックしたとき(色彩補正をForm3の画像に反映させる)
        // ============================================================
        private void OkBtn_Click(object sender, EventArgs e)
        {
            // 現在のプレビュー画像をForm3へ返すために複製する
            ResultImage = new Bitmap(_previewImage);

            // OKとして閉じる
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ============================================================
        // 明るさのスライダーを移動させたとき
        // ============================================================
        private void BrightnessBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            BrightnessTxt.Text = BrightnessBar.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色バランスRのスライダーを移動させたとき
        // ============================================================
        private void ColorBalanceBar_R_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            ColorBalanceTxt_R.Text = ColorBalanceBar_R.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色バランスGのスライダーを移動させたとき
        // ============================================================
        private void ColorBalanceBar_G_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            ColorBalanceTxt_G.Text = ColorBalanceBar_G.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色バランスBのスライダーを移動させたとき
        // ============================================================
        private void ColorBalanceBar_B_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            ColorBalanceTxt_B.Text = ColorBalanceBar_B.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // コントラストのスライダーを移動させたとき
        // ============================================================
        private void contrastBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            contrastTxt.Text = contrastBar.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // ガンマ補正のスライダーを移動させたとき
        // ============================================================
        private void GammaCorrectionBar_Scroll(object sender, EventArgs e)
        {
            // TrackBarの整数値を小数に変換
            double decimalValue = MinValue + GammaCorrectionBar.Value * Step;
            GammaCorrectionTxt.Text = $"{decimalValue:F2}";
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 彩度のスライダーを移動させたとき
        // ============================================================
        private void SaturationBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            SaturationTxt.Text = SaturationBar.Value.ToString();
            // 色彩補正プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色彩補正、色調フィルター、画像効果のプレビューを更新
        // ============================================================
        private void UpdateCorrectionPreview()
        {
            if (_originalImage == null || _originalImage.Width <= 0)
                return;

            // ① 色彩補正値を取得
            int brightness = BrightnessBar.Value;

            int redBalance = ColorBalanceBar_R.Value;
            int greenBalance = ColorBalanceBar_G.Value;
            int blueBalance = ColorBalanceBar_B.Value;

            int contrast = contrastBar.Value;

            double gamma = GammaCorrectionBar.Value / 100.0;

            int saturation = SaturationBar.Value;

            // モノクロームのしきい値
            int monochromeThreshold = monochromeBar.Value;

            // ② 色彩補正用の係数を計算
            double contrastFactor =
                259.0 * (contrast + 255.0) /
                (255.0 * (259.0 - contrast));

            double saturationFactor = 1.0 + saturation / 255.0;

            // ガンマ補正用テーブル
            byte[] gammaTable = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                double normalized = i / 255.0;

                double corrected =
                    Math.Pow(normalized, 1.0 / gamma) * 255.0;

                gammaTable[i] = ClampByte(corrected);
            }

            // ③ 元画像を32bit形式に変換
            Bitmap source = new Bitmap(
                _originalImage.Width,
                _originalImage.Height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(
                    _originalImage,
                    0,
                    0,
                    _originalImage.Width,
                    _originalImage.Height);
            }

            Bitmap result = new Bitmap(
                source.Width,
                source.Height,
                PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(
                0, 0, source.Width, source.Height);

            BitmapData sourceData = source.LockBits(
                rect,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData resultData = result.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                int width = source.Width;
                int height = source.Height;

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                byte[] sourceBytes = new byte[Math.Abs(sourceStride)];
                byte[] resultBytes = new byte[Math.Abs(resultStride)];

                for (int y = 0; y < height; y++)
                {
                    IntPtr sourceRow =
                        IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    IntPtr resultRow =
                        IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(
                        sourceRow,
                        sourceBytes,
                        0,
                        sourceBytes.Length);

                    for (int x = 0; x < width; x++)
                    {
                        int index = x * 4;

                        // メモリ上の並びは BGRA
                        double b = sourceBytes[index];
                        double g = sourceBytes[index + 1];
                        double r = sourceBytes[index + 2];

                        byte alpha = sourceBytes[index + 3];

                        // A. 色彩補正
                        // ① 明るさ
                        r += brightness;
                        g += brightness;
                        b += brightness;

                        // ② RGB色バランス
                        r += redBalance;
                        g += greenBalance;
                        b += blueBalance;

                        // ③ コントラスト
                        r = (r - 128.0) * contrastFactor + 128.0;
                        g = (g - 128.0) * contrastFactor + 128.0;
                        b = (b - 128.0) * contrastFactor + 128.0;

                        r = Math.Clamp(r, 0.0, 255.0);
                        g = Math.Clamp(g, 0.0, 255.0);
                        b = Math.Clamp(b, 0.0, 255.0);

                        // ④ ガンマ補正
                        r = gammaTable[(int)r];
                        g = gammaTable[(int)g];
                        b = gammaTable[(int)b];

                        // ⑤ 彩度
                        double luminance =
                            0.299 * r +
                            0.587 * g +
                            0.114 * b;

                        r = luminance +
                            (r - luminance) * saturationFactor;

                        g = luminance +
                            (g - luminance) * saturationFactor;

                        b = luminance +
                            (b - luminance) * saturationFactor;

                        // B. 色調フィルター
                        switch (_currentFilter)
                        {
                            // フィルターなし
                            case ColorFilter.None:
                                break;

                            // セピア
                            case ColorFilter.Sepia:
                                {
                                    double sepiaR =
                                        0.393 * r + 0.769 * g + 0.189 * b;

                                    double sepiaG =
                                        0.349 * r + 0.686 * g + 0.168 * b;

                                    double sepiaB =
                                        0.272 * r + 0.534 * g + 0.131 * b;

                                    r = sepiaR;
                                    g = sepiaG;
                                    b = sepiaB;

                                    break;
                                }

                            // グレースケール
                            case ColorFilter.Grayscale:
                                {
                                    double gray =
                                        0.299 * r +
                                        0.587 * g +
                                        0.114 * b;

                                    r = gray;
                                    g = gray;
                                    b = gray;

                                    break;
                                }

                            // モノクローム
                            case ColorFilter.Monochrome:
                                {
                                    double gray =
                                        0.299 * r +
                                        0.587 * g +
                                        0.114 * b;

                                    byte mono =
                                        gray >= monochromeThreshold
                                            ? (byte)255
                                            : (byte)0;

                                    r = mono;
                                    g = mono;
                                    b = mono;

                                    break;
                                }

                            // ネガポジ反転
                            case ColorFilter.Negative:
                                {
                                    switch (_negativeChannel)
                                    {
                                        case NegativeChannel.All:
                                            r = 255.0 - r;
                                            g = 255.0 - g;
                                            b = 255.0 - b;
                                            break;

                                        case NegativeChannel.Red:
                                            r = 255.0 - r;
                                            break;

                                        case NegativeChannel.Green:
                                            g = 255.0 - g;
                                            break;

                                        case NegativeChannel.Blue:
                                            b = 255.0 - b;
                                            break;
                                    }

                                    break;
                                }
                        }

                        // C. 結果を保存
                        resultBytes[index] = ClampByte(b);
                        resultBytes[index + 1] = ClampByte(g);
                        resultBytes[index + 2] = ClampByte(r);
                        resultBytes[index + 3] = alpha;
                    }

                    Marshal.Copy(
                        resultBytes,
                        0,
                        resultRow,
                        resultBytes.Length);
                }
            }
            finally
            {
                source.UnlockBits(sourceData);
                result.UnlockBits(resultData);

                source.Dispose();
            }

            // D. 画像効果
            // 色彩補正・色調フィルター適用後の画像に効果を適用
            Bitmap effectResult = ApplyImageEffect(result);

            // 画像効果適用前の画像を解放
            result.Dispose();

            // E. プレビュー画像を更新
            Bitmap oldImage = _previewImage;

            _previewImage = effectResult;
            pictureBox2.Image = _previewImage;

            oldImage.Dispose();

        }

        // ============================================================
        // 0～255の範囲に制限
        // ============================================================
        private static byte ClampByte(double value)
        {
            return (byte)Math.Clamp(
                (int)Math.Round(value),
                0,
                255);
        }


        // ============================================================
        // 画像効果を適用する
        // ぼかし・シャープは排他、エンボスは重ねがけ可能
        // ============================================================
        private Bitmap ApplyImageEffect(Bitmap input)
        {
            // まず入力画像を複製
            Bitmap current = new Bitmap(
                input.Width,
                input.Height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(current))
            {
                g.DrawImage(input, 0, 0, input.Width, input.Height);
            }

            try
            {
                // 1. ぼかし、またはシャープを適用
                if (_currentImageEffect == ImageEffect.Blur &&
                    blurBar.Value > 0)
                {
                    double sigma =
                        0.1 + (blurBar.Value / 99.0) * 83.9;

                    Bitmap next = ApplyFastGaussianBlur(current, sigma);

                    current.Dispose();
                    current = next;
                }
                else if (_currentImageEffect == ImageEffect.Sharpen &&
                         sharpBar.Value > 0)
                {
                    Bitmap next = ApplySharpen(current, sharpBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 2. エンボスを適用
                if (embossBar.Value > 0)
                {
                    Bitmap next = ApplyEmboss(current, embossBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 3. ノイズを適用
                if (IsNoiseEnabled() && noiseBar.Value > 0)
                {
                    Bitmap next = ApplyNoise(current, noiseBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 4. 輪郭抽出
                if (IsContourExEnabled() && ContourExBar.Value > 0)
                {
                    Bitmap next = ApplyContourExtraction(
                        current,
                        ContourExBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 最終画像を返す
                Bitmap result = current;
                current = null!;

                return result;
            }
            finally
            {
                // 途中で例外が発生した場合も画像を解放
                current?.Dispose();
            }
        }


        // ============================================================
        // 高速Gaussian Blur（3回のボックスブラーで近似）
        // ============================================================
        private Bitmap ApplyFastGaussianBlur(Bitmap input, double sigma)
        {
            int width = input.Width;
            int height = input.Height;

            int stride = width * 4;
            int length = stride * height;

            // ボックスブラー3回でGaussian Blurを近似する。
            // 3回のボックスブラーの合成σが指定σに近くなるように半径を設定。
            int radius = Math.Max(
                1,
                (int)Math.Round(sigma));

            // 入力画像を32bit形式として読み込む
            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData sourceData = input.LockBits(
                rect,
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            byte[] sourceBytes = new byte[length];

            try
            {
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        sourceData.Scan0,
                        y * sourceData.Stride);

                    Marshal.Copy(
                        row,
                        sourceBytes,
                        y * stride,
                        stride);
                }
            }
            finally
            {
                input.UnlockBits(sourceData);
            }

            // 作業用バッファー
            byte[] currentBytes = sourceBytes;
            byte[] tempBytes = new byte[length];
            byte[] resultBytes = new byte[length];

            // ----------------------------------------
            // ボックスブラーを3回繰り返す
            // ----------------------------------------
            for (int pass = 0; pass < 3; pass++)
            {
                // 横方向
                BoxBlurHorizontal(
                    currentBytes,
                    tempBytes,
                    width,
                    height,
                    stride,
                    radius);

                // 縦方向
                BoxBlurVertical(
                    tempBytes,
                    resultBytes,
                    width,
                    height,
                    stride,
                    radius);

                // 次のパスの入力にする
                currentBytes = resultBytes;

                // 作業バッファーを入れ替える
                resultBytes = tempBytes;
            }

            // 結果画像を作成
            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            BitmapData resultData = result.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        resultData.Scan0,
                        y * resultData.Stride);

                    Marshal.Copy(
                        currentBytes,
                        y * stride,
                        row,
                        stride);
                }
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                result.UnlockBits(resultData);
            }

            return result;
        }


        // ============================================================
        // 横方向の高速ボックスブラー
        // ============================================================
        private void BoxBlurHorizontal(byte[] source, byte[] destination, int width, int height, int stride, int radius)
        {
            int windowSize = radius * 2 + 1;

            for (int y = 0; y < height; y++)
            {
                int rowOffset = y * stride;

                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                // 最初のウィンドウの合計を計算
                for (int k = -radius; k <= radius; k++)
                {
                    int px = Math.Clamp(k, 0, width - 1);
                    int index = rowOffset + px * 4;

                    sumB += source[index];
                    sumG += source[index + 1];
                    sumR += source[index + 2];
                }

                for (int x = 0; x < width; x++)
                {
                    int outputIndex = rowOffset + x * 4;

                    destination[outputIndex] = ClampByte(sumB / windowSize);

                    destination[outputIndex + 1] = ClampByte(sumG / windowSize);

                    destination[outputIndex + 2] = ClampByte(sumR / windowSize);

                    // アルファ値は元画像を維持
                    destination[outputIndex + 3] = source[outputIndex + 3];

                    // 次の画素へ移動するため、
                    // 左端の画素を合計から引く
                    int removeX = Math.Clamp(
                        x - radius,
                        0,
                        width - 1);

                    int removeIndex = rowOffset + removeX * 4;

                    sumB -= source[removeIndex];
                    sumG -= source[removeIndex + 1];
                    sumR -= source[removeIndex + 2];

                    // 右端の画素を合計に加える
                    int addX = Math.Clamp(
                        x + radius + 1,
                        0,
                        width - 1);

                    int addIndex = rowOffset + addX * 4;

                    sumB += source[addIndex];
                    sumG += source[addIndex + 1];
                    sumR += source[addIndex + 2];
                }
            }
        }


        // ============================================================
        // 縦方向の高速ボックスブラー
        // ============================================================
        private void BoxBlurVertical(byte[] source, byte[] destination, int width, int height, int stride, int radius)
        {
            int windowSize = radius * 2 + 1;

            for (int x = 0; x < width; x++)
            {
                double sumB = 0;
                double sumG = 0;
                double sumR = 0;

                // 最初のウィンドウの合計を計算
                for (int k = -radius; k <= radius; k++)
                {
                    int py = Math.Clamp(k, 0, height - 1);
                    int index = py * stride + x * 4;

                    sumB += source[index];
                    sumG += source[index + 1];
                    sumR += source[index + 2];
                }

                for (int y = 0; y < height; y++)
                {
                    int outputIndex = y * stride + x * 4;

                    destination[outputIndex] = ClampByte(sumB / windowSize);

                    destination[outputIndex + 1] = ClampByte(sumG / windowSize);

                    destination[outputIndex + 2] = ClampByte(sumR / windowSize);

                    // アルファ値は元画像を維持
                    destination[outputIndex + 3] = source[outputIndex + 3];

                    // 次の画素へ移動
                    int removeY = Math.Clamp(y - radius, 0, height - 1);

                    int removeIndex = removeY * stride + x * 4;

                    sumB -= source[removeIndex];
                    sumG -= source[removeIndex + 1];
                    sumR -= source[removeIndex + 2];

                    int addY = Math.Clamp(y + radius + 1, 0, height - 1);

                    int addIndex = addY * stride + x * 4;

                    sumB += source[addIndex];
                    sumG += source[addIndex + 1];
                    sumR += source[addIndex + 2];
                }
            }
        }


        // ============================================================
        // シャープ処理（高速化版：分離型カーネル）
        // ============================================================
        private Bitmap ApplySharpen(Bitmap input, int strength)
        {
            int width = input.Width;
            int height = input.Height;

            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = input.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                resultData = result.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                int sourceRowSize = Math.Abs(sourceStride);
                int resultRowSize = Math.Abs(resultStride);

                byte[] sourceBytes = new byte[sourceRowSize * height];
                byte[] horizontalBytes = new byte[sourceRowSize * height];
                byte[] resultBytes = new byte[resultRowSize * height];

                // 元画像を配列にコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        sourceData.Scan0,
                        y * sourceStride);

                    Marshal.Copy(
                        row,
                        sourceBytes,
                        y * sourceRowSize,
                        sourceRowSize);
                }

                // 横方向のぼかし
                // カーネル：1, 2, 1
                for (int y = 0; y < height; y++)
                {
                    int rowOffset = y * sourceRowSize;

                    for (int x = 0; x < width; x++)
                    {
                        int leftX = Math.Max(0, x - 1);
                        int rightX = Math.Min(width - 1, x + 1);

                        int leftIndex = rowOffset + leftX * 4;
                        int centerIndex = rowOffset + x * 4;
                        int rightIndex = rowOffset + rightX * 4;

                        int outputIndex = rowOffset + x * 4;

                        for (int c = 0; c < 3; c++)
                        {
                            int value =
                                sourceBytes[leftIndex + c] +
                                sourceBytes[centerIndex + c] * 2 +
                                sourceBytes[rightIndex + c];

                            horizontalBytes[outputIndex + c] =
                                (byte)((value + 2) / 4);
                        }

                        horizontalBytes[outputIndex + 3] =
                            sourceBytes[centerIndex + 3];
                    }
                }

                // 縦方向のぼかしとシャープ処理
                // カーネル：1, 2, 1
                double sharpAmount = strength / 50.0;

                for (int y = 0; y < height; y++)
                {
                    int topY = Math.Max(0, y - 1);
                    int bottomY = Math.Min(height - 1, y + 1);

                    int topOffset = topY * sourceRowSize;
                    int centerOffset = y * sourceRowSize;
                    int bottomOffset = bottomY * sourceRowSize;

                    int outputRowOffset = y * resultRowSize;

                    for (int x = 0; x < width; x++)
                    {
                        int index = x * 4;

                        int topIndex = topOffset + index;
                        int centerIndex = centerOffset + index;
                        int bottomIndex = bottomOffset + index;

                        int outputIndex = outputRowOffset + index;

                        for (int c = 0; c < 3; c++)
                        {
                            int blurredValue =
                                horizontalBytes[topIndex + c] +
                                horizontalBytes[centerIndex + c] * 2 +
                                horizontalBytes[bottomIndex + c];

                            double blurred = blurredValue / 4.0;
                            double original = sourceBytes[centerIndex + c];

                            double output =
                                original +
                                (original - blurred) * sharpAmount;

                            resultBytes[outputIndex + c] = ClampByte(output);
                        }

                        // Alpha値は元画像を維持
                        resultBytes[outputIndex + 3] =
                            sourceBytes[centerIndex + 3];
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        resultData.Scan0,
                        y * resultStride);

                    Marshal.Copy(
                        resultBytes,
                        y * resultRowSize,
                        row,
                        resultRowSize);
                }
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                if (sourceData != null)
                    input.UnlockBits(sourceData);

                if (resultData != null)
                    result.UnlockBits(resultData);
            }

            return result;
        }


        // ============================================================
        // エンボス（浮き彫り）
        // strength : 0～99
        // ============================================================
        private Bitmap ApplyEmboss(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 99);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap source = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                Rectangle rect = new Rectangle(0, 0, width, height);

                sourceData = source.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                resultData = result.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                byte[] sourceBytes =
                    new byte[Math.Abs(sourceStride) * height];

                byte[] resultBytes =
                    new byte[Math.Abs(resultStride) * height];

                Marshal.Copy(
                    sourceData.Scan0,
                    sourceBytes,
                    0,
                    sourceBytes.Length);

                // 凹凸の反転
                double direction = embossCheck.Checked ? -1.0 : 1.0;

                // エンボスの強さ
                // 0～99を、輪郭の明暗差に反映する
                double amount = strength / 20.0;

                // 輝度を取得する
                double GetLuminance(int x, int y)
                {
                    x = Math.Clamp(x, 0, width - 1);
                    y = Math.Clamp(y, 0, height - 1);

                    int index = y * sourceStride + x * 4;

                    double b = sourceBytes[index];
                    double g = sourceBytes[index + 1];
                    double r = sourceBytes[index + 2];

                    return 0.114 * b + 0.587 * g + 0.299 * r;
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // 斜め方向の明暗差から凹凸を作る
                        double topLeft = GetLuminance(x - 1, y - 1);
                        double top = GetLuminance(x, y - 1);
                        double left = GetLuminance(x - 1, y);

                        double right = GetLuminance(x + 1, y);
                        double bottom = GetLuminance(x, y + 1);
                        double bottomRight = GetLuminance(x + 1, y + 1);

                        double edge =
                            -topLeft - top - left
                            + right + bottom + bottomRight;

                        // 凹凸の反転
                        //double direction = embossCheck.Checked ? -1.0 : 1.0;

                        // 灰色（128）を基準に、輪郭を明暗で表現
                        double embossed = 128.0 + edge * amount * direction;

                        byte value = ClampByte(embossed);

                        int index = y * resultStride + x * 4;

                        resultBytes[index] = value;
                        resultBytes[index + 1] = value;
                        resultBytes[index + 2] = value;

                        // 元画像の透明度を維持
                        int sourceIndex = y * sourceStride + x * 4;
                        resultBytes[index + 3] = sourceBytes[sourceIndex + 3];
                    }
                }

                Marshal.Copy(
                    resultBytes,
                    0,
                    resultData.Scan0,
                    resultBytes.Length);
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                if (sourceData != null)
                    source.UnlockBits(sourceData);

                if (resultData != null)
                    result.UnlockBits(resultData);

                source.Dispose();
            }

            return result;
        }

        // ============================================================
        // TreeViewでノイズが有効になっているか確認
        // ============================================================
        private bool IsNoiseEnabled()
        {
            TreeNode? parentNode = treeView1.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == "輪郭・質感");

            if (parentNode == null)
                return false;

            TreeNode? noiseNode = parentNode.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == "ノイズ");

            return noiseNode?.Checked == true;
        }

        // ============================================================
        // ノイズ処理（白黒の粒状ノイズ）
        // strength : 0～99
        // ============================================================
        private Bitmap ApplyNoise(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 99);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = input.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                resultData = result.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                byte[] sourceRow = new byte[Math.Abs(sourceStride)];
                byte[] resultRow = new byte[Math.Abs(resultStride)];

                // 同じ設定なら同じノイズになるよう固定シードを使用
                Random random = new Random(12345);

                for (int y = 0; y < height; y++)
                {
                    IntPtr sourcePtr = IntPtr.Add(
                        sourceData.Scan0,
                        y * sourceStride);

                    IntPtr resultPtr = IntPtr.Add(
                        resultData.Scan0,
                        y * resultStride);

                    Marshal.Copy(
                        sourcePtr,
                        sourceRow,
                        0,
                        sourceRow.Length);

                    for (int x = 0; x < width; x++)
                    {
                        int index = x * 4;

                        // -strength ～ +strength の範囲でノイズを生成
                        int noise = random.Next(
                            -strength,
                            strength + 1);

                        // RGBに同じ値を加える（白黒ノイズ）
                        resultRow[index] = ClampByte(
                            sourceRow[index] + noise);

                        resultRow[index + 1] = ClampByte(
                            sourceRow[index + 1] + noise);

                        resultRow[index + 2] = ClampByte(
                            sourceRow[index + 2] + noise);

                        // 透明度は元画像を維持
                        resultRow[index + 3] = sourceRow[index + 3];
                    }

                    Marshal.Copy(
                        resultRow,
                        0,
                        resultPtr,
                        resultRow.Length);
                }
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                if (sourceData != null)
                    input.UnlockBits(sourceData);

                if (resultData != null)
                    result.UnlockBits(resultData);
            }

            return result;
        }

        // ============================================================
        // TreeViewで輪郭抽出が有効になっているか確認
        // ============================================================
        private bool IsContourExEnabled()
        {
            TreeNode? parentNode = treeView1.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == "輪郭・質感");

            if (parentNode == null)
                return false;

            TreeNode? contourNode = parentNode.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == "輪郭抽出");

            return contourNode?.Checked == true;
        }

        // ============================================================
        // 輪郭抽出（Sobelフィルター）
        // strength : 0～99
        // ============================================================
        private Bitmap ApplyContourExtraction(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 99);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap source = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = source.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                resultData = result.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                int sourceRowSize = Math.Abs(sourceStride);
                int resultRowSize = Math.Abs(resultStride);

                byte[] sourceBytes = new byte[sourceRowSize * height];
                byte[] resultBytes = new byte[resultRowSize * height];

                // 元画像を配列にコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        sourceData.Scan0,
                        y * sourceStride);

                    Marshal.Copy(
                        row,
                        sourceBytes,
                        y * sourceRowSize,
                        sourceRowSize);
                }

                // 輝度を取得する
                double GetLuminance(int x, int y)
                {
                    x = Math.Clamp(x, 0, width - 1);
                    y = Math.Clamp(y, 0, height - 1);

                    int index = y * sourceRowSize + x * 4;

                    double b = sourceBytes[index];
                    double g = sourceBytes[index + 1];
                    double r = sourceBytes[index + 2];

                    return 0.114 * b + 0.587 * g + 0.299 * r;
                }

                // 輪郭の強さ
                double amount = strength / 99.0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // 周囲8画素の輝度
                        double tl = GetLuminance(x - 1, y - 1);
                        double tc = GetLuminance(x, y - 1);
                        double tr = GetLuminance(x + 1, y - 1);

                        double ml = GetLuminance(x - 1, y);
                        double mr = GetLuminance(x + 1, y);

                        double bl = GetLuminance(x - 1, y + 1);
                        double bc = GetLuminance(x, y + 1);
                        double br = GetLuminance(x + 1, y + 1);

                        // Sobelフィルターによる横方向の変化
                        double gx =
                            -tl + tr
                            - 2.0 * ml + 2.0 * mr
                            - bl + br;

                        // Sobelフィルターによる縦方向の変化
                        double gy =
                            -tl - 2.0 * tc - tr
                            + bl + 2.0 * bc + br;

                        // 輪郭の強さを計算
                        double magnitude = Math.Sqrt(
                            gx * gx + gy * gy);

                        // 輪郭の強さを元画像に反映
                        double edgeAmount = magnitude * amount * 0.5;

                        int sourceIndex = y * sourceRowSize + x * 4;
                        int resultIndex = y * resultRowSize + x * 4;

                        // 輪郭部分を暗くする
                        resultBytes[resultIndex] = ClampByte(
                            sourceBytes[sourceIndex] - edgeAmount);

                        resultBytes[resultIndex + 1] = ClampByte(
                            sourceBytes[sourceIndex + 1] - edgeAmount);

                        resultBytes[resultIndex + 2] = ClampByte(
                            sourceBytes[sourceIndex + 2] - edgeAmount);

                        // 透明度は元画像を維持
                        resultBytes[resultIndex + 3] =
                            sourceBytes[sourceIndex + 3];
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(
                        resultData.Scan0,
                        y * resultStride);

                    Marshal.Copy(
                        resultBytes,
                        y * resultRowSize,
                        row,
                        resultRowSize);
                }
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                if (sourceData != null)
                    source.UnlockBits(sourceData);

                if (resultData != null)
                    result.UnlockBits(resultData);

                source.Dispose();
            }

            return result;
        }

        // ============================================================
        // 数値入力を検証してTrackBarに反映する共通処理
        // ============================================================
        private void ApplyIntegerInput(TextBox textBox, TrackBar trackBar, Action updatePreview)
        {
            if (!int.TryParse(textBox.Text, out int value))
            {
                // 数値でない場合は現在のスライダー値に戻す
                textBox.Text = trackBar.Value.ToString();
                return;
            }

            // TrackBarの範囲内に制限
            value = Math.Clamp(
                value,
                trackBar.Minimum,
                trackBar.Maximum);

            // スライダーに反映
            trackBar.Value = value;

            // テキストボックスも確定値に更新
            textBox.Text = value.ToString();

            // プレビューを更新
            updatePreview();
        }

        // ============================================================
        // Enterキーが押されたとき
        // ============================================================
        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;

            if (sender is not TextBox textBox)
                return;

            // 明るさ
            if (textBox == BrightnessTxt)
            {
                ApplyIntegerInput(
                    BrightnessTxt,
                    BrightnessBar,
                    UpdateCorrectionPreview);
            }
            // 色バランス[赤(R)]
            else if (textBox == ColorBalanceTxt_R)
            {
                ApplyIntegerInput(
                    ColorBalanceTxt_R,
                    ColorBalanceBar_R,
                    UpdateCorrectionPreview);
            }
            // 色バランス[緑(G)]
            else if (textBox == ColorBalanceTxt_G)
            {
                ApplyIntegerInput(
                    ColorBalanceTxt_G,
                    ColorBalanceBar_G,
                    UpdateCorrectionPreview);
            }
            // 色バランス[青(B)]
            else if (textBox == ColorBalanceTxt_B)
            {
                ApplyIntegerInput(
                    ColorBalanceTxt_B,
                    ColorBalanceBar_B,
                    UpdateCorrectionPreview);
            }
            // コントラスト
            else if (textBox == contrastTxt)
            {
                ApplyIntegerInput(
                    contrastTxt,
                    contrastBar,
                    UpdateCorrectionPreview);
            }
            // 彩度
            else if (textBox == SaturationTxt)
            {
                ApplyIntegerInput(
                    SaturationTxt,
                    SaturationBar,
                    UpdateCorrectionPreview);
            }
            // ガンマ補正
            else if (textBox == GammaCorrectionTxt)
            {
                ApplyGammaInput();
            }
            // モノクローム
            else if (textBox == monochromeTxt)
            {
                ApplyIntegerInput(
                    monochromeTxt,
                    monochromeBar,
                    UpdateCorrectionPreview);
            }
            // ぼかし
            else if (textBox == blurTxt)
            {
                ApplyIntegerInput(
                    blurTxt,
                    blurBar,
                    () =>
                    {
                        if (blurBar.Value > 0)
                        {
                            _currentImageEffect = ImageEffect.Blur;
                            sharpBar.Value = 0;
                            sharpTxt.Text = "0";
                        }
                        else
                        {
                            _currentImageEffect =
                            sharpBar.Value > 0
                            ? ImageEffect.Sharpen
                            : ImageEffect.None;
                        }
                        UpdateCorrectionPreview();
                    });

            }
            // シャープ
            else if (textBox == sharpTxt)
            {
                ApplyIntegerInput(
                    sharpTxt,
                    sharpBar,
                    () =>
                    {
                        if (sharpBar.Value > 0)
                        {
                            _currentImageEffect = ImageEffect.Sharpen;

                            blurBar.Value = 0;
                            blurTxt.Text = "0";
                        }
                        else
                        {
                            _currentImageEffect =
                                blurBar.Value > 0
                                    ? ImageEffect.Blur
                                    : ImageEffect.None;
                        }

                        UpdateCorrectionPreview();
                    });
            }
            // エンボス
            else if (textBox == embossTxt)
            {
                ApplyIntegerInput(
                    embossTxt,
                    embossBar,
                    UpdateCorrectionPreview);
            }
            // ノイズ
            else if (textBox == noiseTxt)
            {
                ApplyIntegerInput(
                    noiseTxt,
                    noiseBar,
                    UpdateCorrectionPreview);

            }
            // 輪郭中sy痛
            else if (textBox == ContourExTxt)
            {
                ApplyIntegerInput(
                    ContourExTxt,
                    ContourExBar,
                    UpdateCorrectionPreview);
            }

            // Enterキーで確定した後も、テキスト全体を選択状態にする
            BeginInvoke(new Action(() =>
            {
                textBox.SelectAll();
            }));

        }

        // ============================================================
        // ガンマ補正の数値入力を反映
        // ============================================================
        private void ApplyGammaInput()
        {
            if (!double.TryParse(GammaCorrectionTxt.Text, out double value))
            {
                double currentValue = MinValue + GammaCorrectionBar.Value * Step;

                GammaCorrectionTxt.Text = $"{currentValue:F2}";
                return;
            }

            // 最小値・最大値に制限
            value = Math.Clamp(value, MinValue, MaxValue);

            // 小数値をTrackBarの整数値に変換
            int trackBarValue = (int)Math.Round(
                (value - MinValue) / Step);

            trackBarValue = Math.Clamp(
                trackBarValue,
                GammaCorrectionBar.Minimum,
                GammaCorrectionBar.Maximum);

            GammaCorrectionBar.Value = trackBarValue;

            // スライダーの実際の値を表示
            double actualValue =
                MinValue + GammaCorrectionBar.Value * Step;

            GammaCorrectionTxt.Text = $"{actualValue:F2}";

            UpdateCorrectionPreview();
        }

        // ============================================================
        // フィルターを選択
        // ============================================================
        private void SetColorFilter(ColorFilter filter)
        {
            _currentFilter = filter;

            // 色調フィルターに応じてモノクローム設定を有効・無効にする
            UpdateFilterSettings();

            UpdateCorrectionPreview();
        }

        // ============================================================
        // 色調フィルターに応じてモノクローム設定を有効・無効にする
        // ============================================================
        private void UpdateFilterSettings()
        {
            bool isMonochrome = _currentFilter == ColorFilter.Monochrome;

            monochromeBar.Enabled = isMonochrome;
            monochromeTxt.Enabled = isMonochrome;
        }

        // ============================================================
        // モノクロームのしきい値スライダー
        // ============================================================
        private void monochromeBar_Scroll(object sender, EventArgs e)
        {
            monochromeTxt.Text = monochromeBar.Value.ToString();

            // モノクローム選択中だけプレビューを更新
            if (_currentFilter == ColorFilter.Monochrome)
            {
                UpdateCorrectionPreview();
            }
        }

        // ============================================================
        // ぼがしのスライダー
        // ============================================================
        private void blurBar_Scroll(object sender, EventArgs e)
        {
            int value = blurBar.Value;

            blurTxt.Text = value.ToString();

            if (value > 0)
            {
                // ぼかしを有効にする
                _currentImageEffect = ImageEffect.Blur;

                // シャープを解除
                sharpBar.Value = 0;
                sharpTxt.Text = "0";
            }
            else
            {
                _currentImageEffect =
                    sharpBar.Value > 0
                        ? ImageEffect.Sharpen
                        : ImageEffect.None;
            }

            UpdateCorrectionPreview();
        }

        // ============================================================
        // シャープのスライダー
        // ============================================================
        private void sharpBar_Scroll(object sender, EventArgs e)
        {
            int value = sharpBar.Value;

            sharpTxt.Text = value.ToString();

            if (value > 0)
            {
                // シャープを有効にする
                _currentImageEffect = ImageEffect.Sharpen;

                // ぼかしを解除
                blurBar.Value = 0;
                blurTxt.Text = "0";
            }
            else
            {
                _currentImageEffect =
                    blurBar.Value > 0
                        ? ImageEffect.Blur
                        : ImageEffect.None;
            }

            UpdateCorrectionPreview();
        }

        // ============================================================
        // エンボスのスライダー
        // ============================================================
        private void embossBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            embossTxt.Text = embossBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // エンボスの凹凸反転
        // ============================================================
        private void embossCheck_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCorrectionPreview();
        }

        // ============================================================
        // ルートノードはチェックできないようにしているが連打するとチェックが入ってしまう
        // ============================================================
        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // ルートノードはチェックできない
            if (e.Node?.Parent == null)
            {
                e.Cancel = true;
            }
        }

        // ============================================================
        // 色調加工の排他制御
        // ============================================================
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            if (_isUpdatingTreeView)
                return;

            if (e.Node?.Parent == null)
                return;

            try
            {
                _isUpdatingTreeView = true;

                // 色調加工の子ノードは相互排他
                if (e.Node.Parent.Text == "色調加工" && e.Node.Checked)
                {
                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (node != e.Node)
                            node.Checked = false;
                    }
                }

                // 基本補正の子ノードは相互排他
                else if (e.Node.Parent.Text == "基本補正" && e.Node.Checked)
                {
                    foreach (TreeNode node in e.Node.Parent.Nodes)
                    {
                        if (node != e.Node)
                            node.Checked = false;
                    }
                }
            }
            finally
            {
                _isUpdatingTreeView = false;
            }

            // チェックされたノードを基準にパネルを更新
            UpdateEffectSettingsPanel(e.Node);

            // 画像効果の切り替え
            if (e.Node.Parent.Text == "基本補正")
            {
                ChangeImageEffect(e.Node);
            }
            // 色調加工の切り替え
            else if (e.Node.Parent.Text == "色調加工")
            {
                ChangeColorFilter(e.Node);
            }
            // 輪郭・質感の切り替え
            else if (e.Node.Parent.Text == "輪郭・質感")
            {
                ChangeOutline(e.Node);
            }
            else
            {
                // 画像プレビューを更新
                UpdateCorrectionPreview();
            }

        }

        // ============================================================
        // ノード選択によってパネルを切り替える
        // ============================================================
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 選択したノードに応じて右側の設定画面を切り替える
            UpdateEffectSettingsPanel(e.Node);
        }

        // ============================================================
        // ノードの文字部分をクリックしたときチェックを切り替える
        // ============================================================
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            /*
            TreeViewHitTestInfo hit = treeView1.HitTest(e.Location);

            // ノードが存在しない場合は何もしない
            if (hit.Node == null)
                return;

            // ルートノードは対象外
            if (hit.Node.Parent == null)
                return;

            // チェックボックス部分はTreeView本来の動作に任せる
            if (hit.Location != TreeViewHitTestLocations.Label)
                return;

            // 文字部分をクリックしたらチェック状態を反転
            hit.Node.Checked = !hit.Node.Checked;
            */
        }

        // ============================================================
        // チェックされたTreeViewノードに応じて設定パネルを切り替える
        // ============================================================
        private void UpdateEffectSettingsPanel(TreeNode? selectedNode)
        {
            // いったんすべての設定パネルを非表示にする
            blurPanel.Visible = false;
            sharpPanel.Visible = false;
            monochromePanel.Visible = false;
            embossPanel.Visible = false;
            noisePanel.Visible = false;
            ContourExPanel.Visible = false;


            if (selectedNode == null)
                return;

            // ぼかしノードが選択され、チェックされている場合
            if (selectedNode.Text == "ぼかし" && selectedNode.Checked)
            {
                blurPanel.Visible = true;
            }
            // シャープノードが選択され、チェックされている場合
            else if (selectedNode.Text == "シャープ" && selectedNode.Checked)
            {
                sharpPanel.Visible = true;
            }
            // モノクロームが選択され、チェックされている場合
            else if (selectedNode.Text == "モノクローム" && selectedNode.Checked)
            {
                monochromePanel.Visible = true;
            }
            // エンボスが選択され、チェックされている場合
            else if (selectedNode.Text == "エンボス" && selectedNode.Checked)
            {
                embossPanel.Visible = true;
            }
            // ノイズが選択され、チェックされている場合
            else if (selectedNode.Text == "ノイズ" && selectedNode.Checked)
            {
                noisePanel.Visible = true;
            }
            // 輪郭抽出が選択され、チェックされている場合
            else if (selectedNode.Text == "輪郭抽出" && selectedNode.Checked)
            {
                ContourExPanel.Visible = true;
            }

        }

        // ============================================================
        // 画像効果を切り替える
        // ============================================================
        private void ChangeImageEffect(TreeNode selectedNode)
        {
            // ぼかし
            if (selectedNode.Text == "ぼかし")
            {
                if (selectedNode.Checked)
                {
                    _currentImageEffect = ImageEffect.Blur;

                    // シャープの設定を初期化
                    sharpBar.Value = 0;
                    sharpTxt.Text = "0";
                }
                else
                {
                    _currentImageEffect = ImageEffect.None;

                    // ぼかしの設定を初期化
                    blurBar.Value = 0;
                    blurTxt.Text = "0";
                }
            }

            // シャープ
            else if (selectedNode.Text == "シャープ")
            {
                if (selectedNode.Checked)
                {
                    _currentImageEffect = ImageEffect.Sharpen;

                    // ぼかしの設定を初期化
                    blurBar.Value = 0;
                    blurTxt.Text = "0";
                }
                else
                {
                    _currentImageEffect = ImageEffect.None;

                    // シャープの設定を初期化
                    sharpBar.Value = 0;
                    sharpTxt.Text = "0";
                }
            }


            UpdateCorrectionPreview();
        }

        // ============================================================
        // TreeViewから色調加工を切り替える
        // ============================================================
        private void ChangeColorFilter(TreeNode selectedNode)
        {
            if (!selectedNode.Checked)
            {
                // チェックOFFなら色調加工を解除
                SetColorFilter(ColorFilter.None);
                return;
            }

            switch (selectedNode.Text)
            {
                case "セピア":
                    SetColorFilter(ColorFilter.Sepia);
                    break;

                case "グレースケール":
                    SetColorFilter(ColorFilter.Grayscale);
                    break;

                case "ネガポジ反転(全チャンネル)":
                    _negativeChannel = NegativeChannel.All;
                    SetColorFilter(ColorFilter.Negative);
                    break;

                case "ネガポジ反転(赤(R))":
                    _negativeChannel = NegativeChannel.Red;
                    SetColorFilter(ColorFilter.Negative);
                    break;

                case "ネガポジ反転(緑(G))":
                    _negativeChannel = NegativeChannel.Green;
                    SetColorFilter(ColorFilter.Negative);
                    break;

                case "ネガポジ反転(青(B))":
                    _negativeChannel = NegativeChannel.Blue;
                    SetColorFilter(ColorFilter.Negative);
                    break;

                case "モノクローム":
                    SetColorFilter(ColorFilter.Monochrome);
                    break;
            }
        }

        // ============================================================
        // TreeViewから輪郭・質感を切り替える
        // ============================================================
        private void ChangeOutline(TreeNode selectedNode)
        {
            // エンボス
            if (selectedNode.Text == "エンボス")
            {
                if (!selectedNode.Checked)
                {
                    // エンボスOFF
                    embossBar.Value = 0;
                    embossTxt.Text = "0";
                    embossCheck.Checked = false;
                }
            }
            // ノイズ
            else if (selectedNode.Text == "ノイズ")
            {
                if (!selectedNode.Checked)
                {
                    // ノイズOFF
                    noiseBar.Value = 0;
                    noiseTxt.Text = "0";
                }
            }
            //輪郭抽出
            else if (selectedNode.Text == "輪郭抽出")
            {
                if (!selectedNode.Checked)
                {
                    // 輪郭抽出OFF
                    ContourExBar.Value = 0;
                    ContourExTxt.Text = "0";
                }
            }

            UpdateCorrectionPreview();

        }

        // ============================================================
        // ノイズのスライダー
        // ============================================================
        private void noiseBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            noiseTxt.Text = noiseBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        private void ContourExBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            ContourExTxt.Text = ContourExBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }
    }
}
