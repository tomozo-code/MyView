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

        // 現在選択されている画像効果(ぼかし、シャープ)
        private ImageEffect _currentImageEffect = ImageEffect.None;

        private bool _isUpdatingTreeView = false;

        // セピア強度（0～99）
        private int _sepiaStrength = 99;

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

            splitContainer2.SplitterDistance = 350;
            splitContainer2.Panel1MinSize = 200;

            treeView1.Dock = DockStyle.Fill;
            treeView1.ExpandAll();

            // ぼかしパネル
            blurPanel.Dock = DockStyle.Fill;
            blurGroup.Dock = DockStyle.Top;
            blurHintTxt.Dock = DockStyle.Fill;
            // シャープパネル
            sharpPanel.Dock = DockStyle.Fill;
            sharpGroup.Dock = DockStyle.Top;
            sharpHintTxt.Dock = DockStyle.Top;
            // セピア
            sepiaPanel.Dock = DockStyle.Fill;
            sepiaGroup.Dock = DockStyle.Top;
            sepiaHintTxt.Dock = DockStyle.Fill;
            // グレースケール
            grayscalePanel.Dock = DockStyle.Fill;
            grayscaleGroup.Dock = DockStyle.Top;
            grayscaleHintTxt.Dock = DockStyle.Fill;
            // ネガポジ反転(全チャンネル)
            negaPosiAllPanel.Dock = DockStyle.Fill;
            negaPosiAllGroup.Dock = DockStyle.Top;
            negaPosiAllHintTxt.Dock = DockStyle.Fill;
            // ネガポジ反転(赤(R))
            negaPosiRedPanel.Dock = DockStyle.Fill;
            negaPosiRedGroup.Dock = DockStyle.Top;
            negaPosiRedHintTxt.Dock = DockStyle.Fill;
            // ネガポジ反転(緑(G))
            negaPosiGreenPanel.Dock = DockStyle.Fill;
            negaPosiGreenGroup.Dock = DockStyle.Top;
            negaPosiGreenHintTxt.Dock = DockStyle.Fill;
            // ネガポジ反転(青(B))
            negaPosiBluePanel.Dock = DockStyle.Fill;
            negaPosiBlueGroup.Dock = DockStyle.Top;
            negaPosiBlueHintTxt.Dock = DockStyle.Fill;
            // モノクロームパネル
            monochromePanel.Dock = DockStyle.Fill;
            monochromeGroup.Dock = DockStyle.Top;
            monochromeHintTxt.Dock = DockStyle.Fill;
            // エンボスパネル
            embossPanel.Dock = DockStyle.Fill;
            embossGroup.Dock = DockStyle.Top;
            embossHintTxt.Dock = DockStyle.Fill;
            // ノイズパネル
            noisePanel.Dock = DockStyle.Fill;
            noiseGroup.Dock = DockStyle.Top;
            noiseHintTxt.Dock = DockStyle.Fill;
            // 輪郭抽出
            ContourExPanel.Dock = DockStyle.Fill;
            ContourExGroup.Dock = DockStyle.Top;
            ContourExHintTxt.Dock = DockStyle.Fill;
            // モザイク
            mosaicPanel.Dock = DockStyle.Fill;
            mosaicGroup.Dock = DockStyle.Top;
            mosaicHintTxt.Dock = DockStyle.Fill;
            // ピクセル化
            pixelPanel.Dock = DockStyle.Fill;
            pixelGroup.Dock = DockStyle.Top;
            pixelHintTxt.Dock = DockStyle.Fill;
            // 周辺減光
            dimmingPanel.Dock = DockStyle.Fill;
            dimmingGroup.Dock = DockStyle.Top;
            dimmingHintTxt.Dock = DockStyle.Fill;
            // 鉛筆画風
            pencilPanel.Dock = DockStyle.Fill;
            pencilGroup.Dock = DockStyle.Top;
            pencilHintTxt.Dock = DockStyle.Fill;
            // 水彩画風
            watercolorPanel.Dock = DockStyle.Fill;
            watercolorGroup.Dock = DockStyle.Top;
            watercolorHintTxt.Dock = DockStyle.Fill;
            // 油絵風
            oilpaintingPanel.Dock = DockStyle.Fill;
            oilpaintingGroup.Dock = DockStyle.Top;
            oilpaintingHintTxt.Dock = DockStyle.Fill;


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

            // 画像効果
            ResetAllEffects();

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
            // 画像効果を初期化（リセット）
            ResetAllEffects();

            // 色調フィルターに応じてモノクローム設定を有効・無効にする
            UpdateFilterSettings();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 画像効果を初期化（リセット）
        // ============================================================
        private void ResetAllEffects()
        {
            // 色調加工
            // 色調加工のTreeViewチェックを解除
            ResetTreeViewChecks("色調加工");

            // セピア
            sepiaBar.Value = 99;
            sepiaTxt.Text = "99";

            // モノクロームのしきい値
            monochromeBar.Value = 128;
            monochromeTxt.Text = "128";

            // フィルターの選択状態
            _currentFilter = ColorFilter.None;

            // ネガポジ反転の対象
            _negativeChannel = NegativeChannel.All;

            // 画像効果
            // 基本補正のTreeViewチェックを解除
            ResetTreeViewChecks("基本補正");

            // ぼかし
            blurBar.Value = 10;
            blurTxt.Text = "10";

            // シャープ
            sharpBar.Value = 99;
            sharpTxt.Text = "99";

            // 輪郭・質感のTreeViewチェックを解除
            ResetTreeViewChecks("輪郭・質感");

            // エンボス
            embossBar.Value = 30;
            embossTxt.Text = "30";
            //embossCheck.Checked = false;

            // ノイズ
            noiseBar.Value = 99;
            noiseTxt.Text = "99";

            // 輪郭抽出
            ContourExBar.Value =50;
            ContourExTxt.Text = "50";

            // 加工・変形のTreeViewチェックを解除
            ResetTreeViewChecks("加工・変形");

            // モザイク
            mosaicBar.Value = 10;
            mosaicTxt.Text = "10";

            // ピクセル化
            pixelBar.Value = 10;
            pixelTxt.Text = "10";

            // 光・レンズのTreeViewチェックを解除
            ResetTreeViewChecks("光・レンズ");

            // 周辺減光
            dimmingBar.Value = 100;
            dimmingTxt.Text = "100";

            // アート表現のTreeViewチェックを解除
            ResetTreeViewChecks("アート表現");

            // 鉛筆画風
            pencilBar.Value = 99;
            pencilTxt.Text = "99";

            // 水彩画風
            watercolorBar.Value = 50;
            watercolorTxt.Text = "50";

            // 油絵風
            oilpaintingBar.Value = 50;
            oilpaintingTxt.Text = "50";

            // 画像効果の選択状態
            _currentImageEffect = ImageEffect.None;

            // 設定パネルを更新
            UpdateFilterSettings();

            // プレビューを更新
            UpdateCorrectionPreview();
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
        // オールリセットボタンを押したとき
        // ============================================================
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            // 色彩補正と画像効果を初期状態に戻す
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
                    IntPtr sourceRow = IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    IntPtr resultRow = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(sourceRow, sourceBytes, 0, sourceBytes.Length);

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

                        r = luminance + (r - luminance) * saturationFactor;

                        g = luminance + (g - luminance) * saturationFactor;

                        b = luminance + (b - luminance) * saturationFactor;

                        // B. 色調フィルター
                        switch (_currentFilter)
                        {
                            // フィルターなし
                            case ColorFilter.None:
                                break;

                            // セピア
                            case ColorFilter.Sepia:
                                {
                                    // セピア変換前のRGBを保存
                                    double originalR = r;
                                    double originalG = g;
                                    double originalB = b;

                                    // セピア色に変換
                                    double sepiaR =
                                        0.393 * originalR +
                                        0.769 * originalG +
                                        0.189 * originalB;

                                    double sepiaG =
                                        0.349 * originalR +
                                        0.686 * originalG +
                                        0.168 * originalB;

                                    double sepiaB =
                                        0.272 * originalR +
                                        0.534 * originalG +
                                        0.131 * originalB;

                                    // 強度に応じて元画像とセピア画像を合成
                                    double ratio = Math.Clamp(_sepiaStrength, 0, 99) / 99.0;

                                    r = originalR * (1.0 - ratio) + sepiaR * ratio;
                                    g = originalG * (1.0 - ratio) + sepiaG * ratio;
                                    b = originalB * (1.0 - ratio) + sepiaB * ratio;

                                    break;
                                }
                            /*
                            case ColorFilter.Sepia:
                                {
                                    double sepiaR = 0.393 * r + 0.769 * g + 0.189 * b;

                                    double sepiaG = 0.349 * r + 0.686 * g + 0.168 * b;

                                    double sepiaB = 0.272 * r + 0.534 * g + 0.131 * b;

                                    r = sepiaR;
                                    g = sepiaG;
                                    b = sepiaB;

                                    break;
                                }
                            */

                            // グレースケール(RGBから輝度を計算 256階調の灰色)
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
                                    // グレースケール(RGBから輝度を計算 256階調の灰色)に変換して
                                    double gray =
                                        0.299 * r +
                                        0.587 * g +
                                        0.114 * b;
                                    // 白黒の2階調に変換
                                    // 輝度がしきい値以上なら白（255）、輝度がしきい値未満なら黒（0）
                                    // つまり、途中の灰色はすべて白か黒に振り分けられる
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

                    Marshal.Copy(resultBytes, 0, resultRow, resultBytes.Length);
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
            return (byte)Math.Clamp((int)Math.Round(value), 0, 255);
        }


        // ============================================================
        // 画像効果を適用する
        // ぼかし・シャープは排他、エンボスは重ねがけ可能
        // ============================================================
        private Bitmap ApplyImageEffect(Bitmap input)
        {
            // まず入力画像を複製
            Bitmap current = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(current))
            {
                g.DrawImage(input, 0, 0, input.Width, input.Height);
            }

            try
            {
                // 1. ぼかし、またはシャープを適用
                if (_currentImageEffect == ImageEffect.Blur && blurBar.Value > 0)
                {
                    double sigma = 0.1 + (blurBar.Value / 255.0) * 83.9;

                    Bitmap next = ApplyFastGaussianBlur(current, sigma);

                    current.Dispose();
                    current = next;
                }
                else if (_currentImageEffect == ImageEffect.Sharpen && sharpBar.Value > 0)
                {
                    Bitmap next = ApplySharpen(current, sharpBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 2. エンボス
                if (IsEffectEnabled("輪郭・質感", "エンボス") && embossBar.Value > 0)
                {
                    Bitmap next = ApplyEmboss(current, embossBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 3. ノイズ
                if (IsEffectEnabled("輪郭・質感", "ノイズ") && noiseBar.Value > 0)
                {
                    Bitmap next = ApplyNoise(current, noiseBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 4. 輪郭抽出
                if (IsEffectEnabled("輪郭・質感", "輪郭抽出") && ContourExBar.Value > 0)
                {
                    Bitmap next = ApplyContourExtraction(
                        current,
                        ContourExBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 5.モザイク
                if (IsEffectEnabled("加工・変形", "モザイク") && mosaicBar.Value > 0)
                {
                    Bitmap next = ApplyMosaic(current, mosaicBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 6. ピクセル化
                if (IsEffectEnabled("加工・変形", "ピクセル化") && pixelBar.Value > 0)
                {
                    Bitmap next = ApplyPixelation(current, pixelBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 7.周辺減光
                if (IsEffectEnabled("光・レンズ", "周辺減光") && dimmingBar.Value > 0)
                {
                    Bitmap next = ApplyVignette(current, dimmingBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 8. 鉛筆画風
                if (IsEffectEnabled("アート表現", "鉛筆画風") && pencilBar.Value > 0)
                {
                    Bitmap next = ApplyPencilSketch(current, pencilBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 9. 水彩画風
                if (IsEffectEnabled("アート表現", "水彩画風") && watercolorBar.Value > 0)
                {
                    Bitmap next = ApplyWatercolor(current, watercolorBar.Value);

                    current.Dispose();
                    current = next;
                }

                // 10. 油絵風
                if (IsEffectEnabled("アート表現", "油絵風") && oilpaintingBar.Value > 0)
                {
                    Bitmap next = Applyoilpainting(current, oilpaintingBar.Value);

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
                    IntPtr row = IntPtr.Add(sourceData.Scan0, y * sourceData.Stride);

                    Marshal.Copy(row, sourceBytes, y * stride, stride);
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

            // ボックスブラーを3回繰り返す
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
            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData resultData = result.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(resultData.Scan0, y * resultData.Stride);

                    Marshal.Copy(currentBytes, y * stride, row, stride);
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
                    int removeX = Math.Clamp(x - radius, 0, width - 1);

                    int removeIndex = rowOffset + removeX * 4;

                    sumB -= source[removeIndex];
                    sumG -= source[removeIndex + 1];
                    sumR -= source[removeIndex + 2];

                    // 右端の画素を合計に加える
                    int addX = Math.Clamp(x + radius + 1, 0, width - 1);

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

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

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
                    IntPtr row = IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    Marshal.Copy(row, sourceBytes, y * sourceRowSize, sourceRowSize);
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

                            horizontalBytes[outputIndex + c] = (byte)((value + 2) / 4);
                        }

                        horizontalBytes[outputIndex + 3] = sourceBytes[centerIndex + 3];
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

                            double output = original + (original - blurred) * sharpAmount;

                            resultBytes[outputIndex + c] = ClampByte(output);
                        }

                        // Alpha値は元画像を維持
                        resultBytes[outputIndex + 3] = sourceBytes[centerIndex + 3];
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(resultBytes, y * resultRowSize, row, resultRowSize);
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
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyEmboss(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap source = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                Rectangle rect = new Rectangle(0, 0, width, height);

                sourceData = source.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                byte[] sourceBytes = new byte[Math.Abs(sourceStride) * height];

                byte[] resultBytes = new byte[Math.Abs(resultStride) * height];

                Marshal.Copy(sourceData.Scan0, sourceBytes, 0, sourceBytes.Length);

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

                        double edge = -topLeft - top - left + right + bottom + bottomRight;

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

                Marshal.Copy(resultBytes, 0, resultData.Scan0, resultBytes.Length);
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
        // TreeViewの指定した効果が有効になっているか確認
        // ============================================================
        private bool IsEffectEnabled(string parentText, string effectText)
        {
            TreeNode? parentNode = treeView1.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == parentText);

            if (parentNode == null)
                return false;

            TreeNode? effectNode = parentNode.Nodes
                .Cast<TreeNode>()
                .FirstOrDefault(node => node.Text == effectText);

            return effectNode?.Checked == true;
        }

        // ============================================================
        // モザイク処理
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyMosaic(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            // スライダー値からブロックサイズを計算
            // 1～99 → 2～50ピクセル
            int blockSize = 2 + (int)Math.Round(strength * 48.0 / 99.0);

            // 元画像を32bit形式に変換
            Bitmap source = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = source.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                int sourceRowSize = Math.Abs(sourceStride);
                int resultRowSize = Math.Abs(resultStride);

                byte[] sourceBytes = new byte[sourceRowSize * height];

                byte[] resultBytes = new byte[resultRowSize * height];

                // 元画像を配列にコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    Marshal.Copy(row, sourceBytes, y * sourceRowSize, sourceRowSize);
                }

                // ブロック単位で処理
                for (int blockY = 0; blockY < height; blockY += blockSize)
                {
                    for (int blockX = 0; blockX < width; blockX += blockSize)
                    {
                        // ブロックの終端
                        int endX = Math.Min(blockX + blockSize, width);
                        int endY = Math.Min(blockY + blockSize, height);

                        long sumB = 0;
                        long sumG = 0;
                        long sumR = 0;

                        int pixelCount = 0;

                        // ① ブロック内のRGB合計を計算
                        for (int y = blockY; y < endY; y++)
                        {
                            int rowOffset = y * sourceRowSize;

                            for (int x = blockX; x < endX; x++)
                            {
                                int index = rowOffset + x * 4;

                                // BGRA形式
                                sumB += sourceBytes[index];
                                sumG += sourceBytes[index + 1];
                                sumR += sourceBytes[index + 2];

                                pixelCount++;
                            }
                        }

                        // ブロック内の平均色
                        byte averageB = (byte)(sumB / pixelCount);
                        byte averageG = (byte)(sumG / pixelCount);
                        byte averageR = (byte)(sumR / pixelCount);

                        // ② ブロック全体を平均色で塗りつぶす
                        for (int y = blockY; y < endY; y++)
                        {
                            int sourceRowOffset = y * sourceRowSize;
                            int resultRowOffset = y * resultRowSize;

                            for (int x = blockX; x < endX; x++)
                            {
                                int sourceIndex =
                                    sourceRowOffset + x * 4;

                                int resultIndex =
                                    resultRowOffset + x * 4;

                                resultBytes[resultIndex] = averageB;
                                resultBytes[resultIndex + 1] = averageG;
                                resultBytes[resultIndex + 2] = averageR;

                                // 透明度は元画像を維持
                                resultBytes[resultIndex + 3] =
                                    sourceBytes[sourceIndex + 3];
                            }
                        }
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(resultBytes, y * resultRowSize, row, resultRowSize);
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
        // ピクセル化
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyPixelation(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            // 強度から縮小率を決定
            // 強度が大きいほど縮小画像が小さくなる
            int blockSize = 1 + (int)Math.Round(strength * 49.0 / 99.0);

            int smallWidth = Math.Max(1, (int)Math.Ceiling(width / (double)blockSize));

            int smallHeight = Math.Max(1, (int)Math.Ceiling(height / (double)blockSize));

            Bitmap small = new Bitmap(smallWidth, smallHeight, PixelFormat.Format32bppArgb);

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            try
            {
                // ① 元画像を縮小する
                using (Graphics g = Graphics.FromImage(small))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                    g.DrawImage(
                        input,
                        new Rectangle(0, 0, smallWidth, smallHeight),
                        new Rectangle(0, 0, width, height),
                        GraphicsUnit.Pixel);
                }

                // ② 縮小画像を元の大きさに拡大する
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                    g.DrawImage(
                        small,
                        new Rectangle(0, 0, width, height),
                        new Rectangle(0, 0, smallWidth, smallHeight),
                        GraphicsUnit.Pixel);
                }

                return result;
            }
            catch
            {
                result.Dispose();
                throw;
            }
            finally
            {
                small.Dispose();
            }
        }

        // ============================================================
        // ノイズ処理（白黒の粒状ノイズ）
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyNoise(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                byte[] sourceRow = new byte[Math.Abs(sourceStride)];
                byte[] resultRow = new byte[Math.Abs(resultStride)];

                // 同じ設定なら同じノイズになるよう固定シードを使用
                Random random = new Random(12345);

                for (int y = 0; y < height; y++)
                {
                    IntPtr sourcePtr = IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    IntPtr resultPtr = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(
                        sourcePtr,
                        sourceRow,
                        0,
                        sourceRow.Length);

                    for (int x = 0; x < width; x++)
                    {
                        int index = x * 4;

                        // -strength ～ +strength の範囲でノイズを生成
                        int noise = random.Next(-strength, strength + 1);

                        // RGBに同じ値を加える（白黒ノイズ）
                        resultRow[index] = ClampByte(sourceRow[index] + noise);

                        resultRow[index + 1] = ClampByte(sourceRow[index + 1] + noise);

                        resultRow[index + 2] = ClampByte(sourceRow[index + 2] + noise);

                        // 透明度は元画像を維持
                        resultRow[index + 3] = sourceRow[index + 3];
                    }

                    Marshal.Copy(resultRow, 0, resultPtr, resultRow.Length);
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
        // 周辺減光
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyVignette(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap source = new Bitmap(width, height, PixelFormat.Format32bppArgb);

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

                // 画像中心
                double centerX = (width - 1) / 2.0;
                double centerY = (height - 1) / 2.0;

                // 中心から四隅までの最大距離
                double maxDistance = Math.Sqrt(
                    centerX * centerX +
                    centerY * centerY);

                // 周辺減光の強さ
                double amount = strength / 99.0 * 0.85;

                for (int y = 0; y < height; y++)
                {
                    int sourceRowOffset = y * sourceRowSize;
                    int resultRowOffset = y * resultRowSize;

                    for (int x = 0; x < width; x++)
                    {
                        int sourceIndex = sourceRowOffset + x * 4;
                        int resultIndex = resultRowOffset + x * 4;

                        // 中心からの距離を0～1に正規化
                        double dx = (x - centerX) / maxDistance;
                        double dy = (y - centerY) / maxDistance;

                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        // 中心から離れるほど暗くする
                        double vignette = distance * distance;

                        double factor = 1.0 - amount * vignette;

                        factor = Math.Clamp(factor, 0.0, 1.0);

                        // BGRA形式
                        resultBytes[resultIndex] =
                            ClampByte(sourceBytes[sourceIndex] * factor);

                        resultBytes[resultIndex + 1] =
                            ClampByte(sourceBytes[sourceIndex + 1] * factor);

                        resultBytes[resultIndex + 2] =
                            ClampByte(sourceBytes[sourceIndex + 2] * factor);

                        // 透明度は元画像を維持
                        resultBytes[resultIndex + 3] =
                            sourceBytes[sourceIndex + 3];
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(resultBytes, y * resultRowSize, row, resultRowSize);
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
        // 輪郭抽出（Sobelフィルター）
        // strength : 0～255
        // ============================================================
        private Bitmap ApplyContourExtraction(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            Bitmap source = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap result = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? resultData = null;

            try
            {
                sourceData = source.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                resultData = result.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                int sourceStride = sourceData.Stride;
                int resultStride = resultData.Stride;

                int sourceRowSize = Math.Abs(sourceStride);
                int resultRowSize = Math.Abs(resultStride);

                byte[] sourceBytes = new byte[sourceRowSize * height];
                byte[] resultBytes = new byte[resultRowSize * height];

                // 元画像を配列にコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(sourceData.Scan0, y * sourceStride);

                    Marshal.Copy(row, sourceBytes, y * sourceRowSize, sourceRowSize);
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
                        double magnitude = Math.Sqrt(gx * gx + gy * gy);

                        // 輪郭の強さを元画像に反映
                        double edgeAmount = magnitude * amount * 0.5;

                        int sourceIndex = y * sourceRowSize + x * 4;
                        int resultIndex = y * resultRowSize + x * 4;

                        // 輪郭部分を暗くする
                        resultBytes[resultIndex] = ClampByte(sourceBytes[sourceIndex] - edgeAmount);

                        resultBytes[resultIndex + 1] = ClampByte(sourceBytes[sourceIndex + 1] - edgeAmount);

                        resultBytes[resultIndex + 2] = ClampByte(sourceBytes[sourceIndex + 2] - edgeAmount);

                        // 透明度は元画像を維持
                        resultBytes[resultIndex + 3] = sourceBytes[sourceIndex + 3];
                    }
                }

                // 結果をBitmapへコピー
                for (int y = 0; y < height; y++)
                {
                    IntPtr row = IntPtr.Add(resultData.Scan0, y * resultStride);

                    Marshal.Copy(resultBytes, y * resultRowSize, row, resultRowSize);
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
        // 鉛筆画風の画像を作成する
        // strength : 0～99
        // ============================================================
        private Bitmap ApplyPencilSketch(Bitmap input, int strength)
        {
            // 効果の強さを0～99に制限
            strength = Math.Clamp(strength, 0, 99);

            // 効果が0なら元画像のコピーを返す
            if (strength == 0)
            {
                return new Bitmap(input);
            }

            int width = input.Width;
            int height = input.Height;

            // 画像を32bit ARGB形式に統一する
            Bitmap source = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            Bitmap gray = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Bitmap inverted = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            try
            {
                // 1. グレースケール化(色を取り除き、明暗情報にする)
                Rectangle rect = new Rectangle(0, 0, width, height);

                BitmapData sourceData = source.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData grayData = gray.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    int sourceStride = sourceData.Stride;
                    int grayStride = grayData.Stride;

                    byte[] sourceBuffer = new byte[Math.Abs(sourceStride) * height];
                    byte[] grayBuffer = new byte[Math.Abs(grayStride) * height];

                    Marshal.Copy(
                        sourceData.Scan0,
                        sourceBuffer,
                        0,
                        sourceBuffer.Length);

                    for (int y = 0; y < height; y++)
                    {
                        int sourceRow = y * Math.Abs(sourceStride);
                        int grayRow = y * Math.Abs(grayStride);

                        for (int x = 0; x < width; x++)
                        {
                            int si = sourceRow + x * 4;
                            int gi = grayRow + x * 4;

                            byte b = sourceBuffer[si];
                            byte gr = sourceBuffer[si + 1];
                            byte r = sourceBuffer[si + 2];

                            // 人間の視覚に合わせた輝度計算
                            byte luminance = ClampByte(
                                r * 0.299 +
                                gr * 0.587 +
                                b * 0.114);

                            grayBuffer[gi] = luminance;
                            grayBuffer[gi + 1] = luminance;
                            grayBuffer[gi + 2] = luminance;
                            grayBuffer[gi + 3] = sourceBuffer[si + 3];
                        }
                    }

                    Marshal.Copy(
                        grayBuffer,
                        0,
                        grayData.Scan0,
                        grayBuffer.Length);
                }
                finally
                {
                    source.UnlockBits(sourceData);
                    gray.UnlockBits(grayData);
                }

                // 2. グレースケール画像を反転(明るい部分を暗く、暗い部分を明るくする)
                BitmapData grayReadData = gray.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData invertedData = inverted.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    int grayStride = grayReadData.Stride;
                    int invertedStride = invertedData.Stride;

                    byte[] grayBuffer = new byte[Math.Abs(grayStride) * height];
                    byte[] invertedBuffer = new byte[Math.Abs(invertedStride) * height];

                    Marshal.Copy(
                        grayReadData.Scan0,
                        grayBuffer,
                        0,
                        grayBuffer.Length);

                    for (int y = 0; y < height; y++)
                    {
                        int grayRow = y * Math.Abs(grayStride);
                        int invertedRow = y * Math.Abs(invertedStride);

                        for (int x = 0; x < width; x++)
                        {
                            int gi = grayRow + x * 4;
                            int ii = invertedRow + x * 4;

                            byte value = (byte)(255 - grayBuffer[gi]);

                            invertedBuffer[ii] = value;
                            invertedBuffer[ii + 1] = value;
                            invertedBuffer[ii + 2] = value;
                            invertedBuffer[ii + 3] = grayBuffer[gi + 3];
                        }
                    }

                    Marshal.Copy(
                        invertedBuffer,
                        0,
                        invertedData.Scan0,
                        invertedBuffer.Length);
                }
                finally
                {
                    gray.UnlockBits(grayReadData);
                    inverted.UnlockBits(invertedData);
                }

                // 3. 反転画像をガウスぼかし(輪郭や陰影を広げる)
                // sigmaを大きくすると、鉛筆画の濃淡が広がる
                double sigma = 8.0;

                using Bitmap blurred = ApplyFastGaussianBlur(inverted, sigma);

                // 4. カラードッジ合成
                Bitmap sketch = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                BitmapData graySketchData = gray.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData blurredData = blurred.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData sketchData = sketch.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    int grayStride = graySketchData.Stride;
                    int blurredStride = blurredData.Stride;
                    int sketchStride = sketchData.Stride;

                    byte[] grayBuffer = new byte[Math.Abs(grayStride) * height];
                    byte[] blurredBuffer = new byte[Math.Abs(blurredStride) * height];
                    byte[] sketchBuffer = new byte[Math.Abs(sketchStride) * height];

                    Marshal.Copy(graySketchData.Scan0, grayBuffer, 0, grayBuffer.Length);
                    Marshal.Copy(blurredData.Scan0, blurredBuffer, 0, blurredBuffer.Length);

                    for (int y = 0; y < height; y++)
                    {
                        int grayRow = y * Math.Abs(grayStride);
                        int blurredRow = y * Math.Abs(blurredStride);
                        int sketchRow = y * Math.Abs(sketchStride);

                        for (int x = 0; x < width; x++)
                        {
                            int gi = grayRow + x * 4;
                            int bi = blurredRow + x * 4;
                            int si = sketchRow + x * 4;

                            int baseValue = grayBuffer[gi];
                            int blendValue = blurredBuffer[bi];

                            // カラードッジ(鉛筆で描いたような線と濃淡を作る)
                            int result;

                            if (blendValue >= 255)
                            {
                                result = 255;
                            }
                            else
                            {
                                result = Math.Min(
                                    255,
                                    (baseValue * 255) / (255 - blendValue));
                            }

                            byte value = (byte)result;

                            sketchBuffer[si] = value;
                            sketchBuffer[si + 1] = value;
                            sketchBuffer[si + 2] = value;
                            sketchBuffer[si + 3] = grayBuffer[gi + 3];
                        }
                    }

                    Marshal.Copy(
                        sketchBuffer,
                        0,
                        sketchData.Scan0,
                        sketchBuffer.Length);
                }
                finally
                {
                    gray.UnlockBits(graySketchData);
                    blurred.UnlockBits(blurredData);
                    sketch.UnlockBits(sketchData);
                }

                // 5. 元画像と鉛筆画を合成(スライダーで効果の強さを調整する)
                // strength 0～99を0.0～1.0に変換
                double ratio = strength / 99.0;

                Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                using (Graphics g = Graphics.FromImage(resultImage))
                {
                    g.DrawImage(source, 0, 0);
                }

                BitmapData originalData = source.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData sketchReadData = sketch.LockBits(
                    rect,
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                BitmapData resultData = resultImage.LockBits(
                    rect,
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                try
                {
                    int originalStride = originalData.Stride;
                    int sketchStride = sketchReadData.Stride;
                    int resultStride = resultData.Stride;

                    byte[] originalBuffer = new byte[Math.Abs(originalStride) * height];
                    byte[] sketchBuffer = new byte[Math.Abs(sketchStride) * height];
                    byte[] resultBuffer = new byte[Math.Abs(resultStride) * height];

                    Marshal.Copy(originalData.Scan0, originalBuffer, 0, originalBuffer.Length);
                    Marshal.Copy(sketchReadData.Scan0, sketchBuffer, 0, sketchBuffer.Length);

                    for (int y = 0; y < height; y++)
                    {
                        int originalRow = y * Math.Abs(originalStride);
                        int sketchRow = y * Math.Abs(sketchStride);
                        int resultRow = y * Math.Abs(resultStride);

                        for (int x = 0; x < width; x++)
                        {
                            int oi = originalRow + x * 4;
                            int si = sketchRow + x * 4;
                            int ri = resultRow + x * 4;

                            for (int c = 0; c < 3; c++)
                            {
                                double originalValue = originalBuffer[oi + c];
                                double sketchValue = sketchBuffer[si + c];

                                resultBuffer[ri + c] = ClampByte(
                                    originalValue * (1.0 - ratio) +
                                    sketchValue * ratio);
                            }

                            resultBuffer[ri + 3] = originalBuffer[oi + 3];
                        }
                    }

                    Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
                }
                finally
                {
                    source.UnlockBits(originalData);
                    sketch.UnlockBits(sketchReadData);
                    resultImage.UnlockBits(resultData);
                }

                sketch.Dispose();

                return resultImage;
            }
            finally
            {
                source.Dispose();
                gray.Dispose();
                inverted.Dispose();
            }
        }

        // ============================================================
        // 水彩画風の画像を作成する
        // strength : 0～99
        // ============================================================
        private Bitmap ApplyWatercolor(Bitmap input, int strength)
        {
            strength = Math.Clamp(strength, 0, 255);

            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            // --------------------------------------------------------
            // 画像を32bit ARGB形式に統一する
            // --------------------------------------------------------
            using Bitmap source = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            double ratio = strength / 99.0;

            // --------------------------------------------------------
            // 基本パラメーター
            // --------------------------------------------------------

            // 基本のぼかし
            double sigma = 0.5 + ratio * 2.5;

            // 色の整理具合
            int colorStep = 3 + (int)Math.Round(ratio * 21.0);

            // にじみのぼかし量
            double bleedSigma = 0.35 + ratio * 1.15;

            // にじみの混合率
            double bleedMix = 0.05 + ratio * 0.25;

            // --------------------------------------------------------
            // 基本のぼかし画像を作成
            // --------------------------------------------------------
            using Bitmap softened = ApplyFastGaussianBlur(source, sigma);

            // --------------------------------------------------------
            // にじみ画像を作成
            // --------------------------------------------------------
            using Bitmap bleed = ApplyFastGaussianBlur(
                softened,
                bleedSigma);

            Bitmap result = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, width, height);

            BitmapData? sourceData = null;
            BitmapData? softenedData = null;
            BitmapData? bleedData = null;
            BitmapData? resultData = null;

            try
            {
                try
                {
                    sourceData = source.LockBits(
                        rect,
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format32bppArgb);

                    softenedData = softened.LockBits(
                        rect,
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format32bppArgb);

                    bleedData = bleed.LockBits(
                        rect,
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format32bppArgb);

                    resultData = result.LockBits(
                        rect,
                        ImageLockMode.WriteOnly,
                        PixelFormat.Format32bppArgb);

                    int sourceStride = sourceData.Stride;
                    int softenedStride = softenedData.Stride;
                    int bleedStride = bleedData.Stride;
                    int resultStride = resultData.Stride;

                    byte[] sourceRow = new byte[width * 4];
                    byte[] softenedRow = new byte[width * 4];
                    byte[] bleedRow = new byte[width * 4];
                    byte[] resultRow = new byte[width * 4];

                    byte[] previousRow = new byte[width * 4];
                    byte[] currentRow = new byte[width * 4];
                    byte[] nextRow = new byte[width * 4];

                    // ------------------------------------------------
                    // 輝度を取得
                    // ------------------------------------------------
                    double GetLuminance(byte[] row, int x)
                    {
                        int index = x * 4;

                        double b = row[index];
                        double g = row[index + 1];
                        double r = row[index + 2];

                        return 0.114 * b + 0.587 * g + 0.299 * r;
                    }

                    // ------------------------------------------------
                    // 色を整理する
                    // ------------------------------------------------
                    byte QuantizeColor(double value)
                    {
                        int quantized =
                            (int)Math.Round(value / colorStep) * colorStep;

                        return (byte)Math.Clamp(quantized, 0, 255);
                    }

                    // ------------------------------------------------
                    // 紙の粒状感を作るためのノイズ
                    // 座標から計算するため毎回同じ模様になる
                    // ------------------------------------------------
                    double GetPaperGrain(int x, int y)
                    {
                        uint n = unchecked(
                            (uint)(x * 374761393 + y * 668265263));

                        n = unchecked(
                            (n ^ (n >> 13)) * 1274126177);

                        n ^= n >> 16;

                        return (n & 0xFFFF) / 65535.0;
                    }

                    // ------------------------------------------------
                    // 紙の緩やかな濃淡を事前計算
                    // ------------------------------------------------
                    double[] paperX = new double[width];

                    for (int x = 0; x < width; x++)
                    {
                        paperX[x] =
                            Math.Sin(x * 0.071) +
                            0.35 * Math.Sin(x * 0.019 + 1.7);
                    }

                    // ------------------------------------------------
                    // 画像処理
                    // ------------------------------------------------
                    for (int y = 0; y < height; y++)
                    {
                        // 元画像
                        Marshal.Copy(
                            IntPtr.Add(
                                sourceData.Scan0,
                                y * sourceStride),
                            sourceRow,
                            0,
                            sourceRow.Length);

                        // 基本のぼかし画像
                        Marshal.Copy(
                            IntPtr.Add(
                                softenedData.Scan0,
                                y * softenedStride),
                            softenedRow,
                            0,
                            softenedRow.Length);

                        // にじみ画像
                        Marshal.Copy(
                            IntPtr.Add(
                                bleedData.Scan0,
                                y * bleedStride),
                            bleedRow,
                            0,
                            bleedRow.Length);

                        // 輪郭検出用の上下の行
                        int previousY = Math.Max(0, y - 1);
                        int nextY = Math.Min(height - 1, y + 1);

                        Marshal.Copy(
                            IntPtr.Add(
                                bleedData.Scan0,
                                previousY * bleedStride),
                            previousRow,
                            0,
                            previousRow.Length);

                        Marshal.Copy(
                            IntPtr.Add(
                                bleedData.Scan0,
                                nextY * bleedStride),
                            nextRow,
                            0,
                            nextRow.Length);

                        Array.Copy(
                            bleedRow,
                            currentRow,
                            bleedRow.Length);

                        // 紙の緩やかな濃淡
                        double paperY =
                            Math.Sin(y * 0.083 + 0.6) +
                            0.35 * Math.Sin(y * 0.023 + 2.1);

                        // --------------------------------------------
                        // ピクセル単位の処理
                        // --------------------------------------------
                        for (int x = 0; x < width; x++)
                        {
                            int index = x * 4;

                            // 基本のぼかし画像の色
                            double b = softenedRow[index];
                            double g = softenedRow[index + 1];
                            double r = softenedRow[index + 2];

                            // にじみ画像の色
                            double bleedB = bleedRow[index];
                            double bleedG = bleedRow[index + 1];
                            double bleedR = bleedRow[index + 2];

                            // ----------------------------------------
                            // 1. 水彩のにじみ
                            // ----------------------------------------
                            double mixedB =
                                b * (1.0 - bleedMix) +
                                bleedB * bleedMix;

                            double mixedG =
                                g * (1.0 - bleedMix) +
                                bleedG * bleedMix;

                            double mixedR =
                                r * (1.0 - bleedMix) +
                                bleedR * bleedMix;

                            // ----------------------------------------
                            // 2. 色の整理
                            // ----------------------------------------
                            byte watercolorB = QuantizeColor(mixedB);
                            byte watercolorG = QuantizeColor(mixedG);
                            byte watercolorR = QuantizeColor(mixedR);

                            // ----------------------------------------
                            // 3. 輪郭の検出
                            // ----------------------------------------
                            int leftX = Math.Max(0, x - 1);
                            int rightX = Math.Min(width - 1, x + 1);

                            double left = GetLuminance(currentRow, leftX);
                            double right = GetLuminance(currentRow, rightX);

                            double top = GetLuminance(previousRow, x);
                            double bottom = GetLuminance(nextRow, x);

                            double gx = right - left;
                            double gy = bottom - top;

                            double edge = Math.Sqrt(gx * gx + gy * gy);

                            // 輪郭部分を少し暗くする
                            double edgeFactor =
                                1.0 -
                                Math.Min(1.0, edge / 180.0)
                                * 0.18 * ratio;

                            // ----------------------------------------
                            // 4. 紙の質感
                            // ----------------------------------------

                            // 細かな粒状感
                            double grain =
                                (GetPaperGrain(x, y) - 0.5)
                                * (1.5 + 3.5 * ratio)
                                * ratio;

                            // 紙の緩やかな濃淡
                            double paperWave =
                                (paperX[x] + paperY) * 0.5;

                            double paperOffset =
                                paperWave *
                                (0.5 + ratio) *
                                ratio;

                            // 粒状感と濃淡を合成
                            double paperTexture =
                                grain + paperOffset;

                            // ----------------------------------------
                            // 5. 元画像と水彩画風画像を合成
                            // ----------------------------------------

                            double processedB =
                                watercolorB * edgeFactor +
                                paperTexture;

                            double processedG =
                                watercolorG * edgeFactor +
                                paperTexture;

                            double processedR =
                                watercolorR * edgeFactor +
                                paperTexture;

                            double originalB = sourceRow[index];
                            double originalG = sourceRow[index + 1];
                            double originalR = sourceRow[index + 2];

                            resultRow[index] = ClampByte(
                                originalB * (1.0 - ratio) +
                                processedB * ratio);

                            resultRow[index + 1] = ClampByte(
                                originalG * (1.0 - ratio) +
                                processedG * ratio);

                            resultRow[index + 2] = ClampByte(
                                originalR * (1.0 - ratio) +
                                processedR * ratio);

                            // 透明度は元画像を維持
                            resultRow[index + 3] = sourceRow[index + 3];
                        }

                        // 処理結果を書き込む
                        Marshal.Copy(
                            resultRow,
                            0,
                            IntPtr.Add(
                                resultData.Scan0,
                                y * resultStride),
                            resultRow.Length);
                    }
                }
                finally
                {
                    // LockBitsを必ず解除する
                    if (sourceData != null)
                        source.UnlockBits(sourceData);

                    if (softenedData != null)
                        softened.UnlockBits(softenedData);

                    if (bleedData != null)
                        bleed.UnlockBits(bleedData);

                    if (resultData != null)
                        result.UnlockBits(resultData);
                }
            }
            catch
            {
                result.Dispose();
                throw;
            }

            return result;
        }


        // ============================================================
        // 油絵風の画像を作成する
        // strength : 0～255
        // ============================================================
        private Bitmap Applyoilpainting(Bitmap input, int strength)
        {
            // 効果の強さを0～99に制限
            strength = Math.Clamp(strength, 0, 255);

            // 効果が0なら元画像のコピーを返す
            if (strength == 0)
                return new Bitmap(input);

            int width = input.Width;
            int height = input.Height;

            // --------------------------------------------------------
            // 画像を32bit ARGB形式に統一する
            // --------------------------------------------------------
            using Bitmap source = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(source))
            {
                g.DrawImage(input, 0, 0, width, height);
            }

            // --------------------------------------------------------
            // 強さに応じたパラメーター
            // --------------------------------------------------------

            double ratio = strength / 99.0;

            // 周囲を調べる間隔
            // 強くするほど大きな範囲から色を取得する
            int radius = 1 + (int)Math.Round(ratio * 3.0);

            // 明るさの分類数
            // 少なくするほど色のまとまりが大きくなる
            int binCount = 16 - (int)Math.Round(ratio * 8.0);

            binCount = Math.Clamp(binCount, 8, 16);

            // --------------------------------------------------------
            // 出力画像
            // --------------------------------------------------------
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

                byte[] sourceBytes =
                    new byte[sourceRowSize * height];

                byte[] resultBytes =
                    new byte[resultRowSize * height];

                // ----------------------------------------------------
                // 元画像を配列にコピー
                // ----------------------------------------------------
                for (int y = 0; y < height; y++)
                {
                    Marshal.Copy(
                        IntPtr.Add(sourceData.Scan0, y * sourceStride),
                        sourceBytes,
                        y * sourceRowSize,
                        sourceRowSize);
                }

                // ----------------------------------------------------
                // 明るさの分類ごとに集計する配列
                // ----------------------------------------------------
                int[] histogram = new int[16];

                long[] sumB = new long[16];
                long[] sumG = new long[16];
                long[] sumR = new long[16];

                // ----------------------------------------------------
                // 画像処理
                // ----------------------------------------------------
                for (int y = 0; y < height; y++)
                {
                    int sourceRow = y * sourceRowSize;
                    int resultRow = y * resultRowSize;

                    for (int x = 0; x < width; x++)
                    {
                        // 分類用の集計配列を初期化
                        Array.Clear(histogram);
                        Array.Clear(sumB);
                        Array.Clear(sumG);
                        Array.Clear(sumR);

                        // --------------------------------------------
                        // 1. 周囲5×5画素の色を調べる
                        // --------------------------------------------
                        for (int ky = -2; ky <= 2; ky++)
                        {
                            int sampleY = Math.Clamp(
                                y + ky * radius,
                                0,
                                height - 1);

                            int sampleRow = sampleY * sourceRowSize;

                            for (int kx = -2; kx <= 2; kx++)
                            {
                                int sampleX = Math.Clamp(
                                    x + kx * radius,
                                    0,
                                    width - 1);

                                int index = sampleRow + sampleX * 4;

                                byte b = sourceBytes[index];
                                byte g = sourceBytes[index + 1];
                                byte r = sourceBytes[index + 2];

                                // 輝度を計算
                                double luminance =
                                    0.114 * b +
                                    0.587 * g +
                                    0.299 * r;

                                // 輝度を分類
                                int bin = (int)(
                                    luminance * binCount / 256.0);

                                bin = Math.Clamp(bin, 0, binCount - 1);

                                // 分類ごとに画素数と色を集計
                                histogram[bin]++;

                                sumB[bin] += b;
                                sumG[bin] += g;
                                sumR[bin] += r;
                            }
                        }

                        // --------------------------------------------
                        // 2. 最も多く集まった明るさのグループを探す
                        // --------------------------------------------
                        int dominantBin = 0;
                        int maxCount = 0;

                        for (int i = 0; i < binCount; i++)
                        {
                            if (histogram[i] > maxCount)
                            {
                                maxCount = histogram[i];
                                dominantBin = i;
                            }
                        }

                        // --------------------------------------------
                        // 3. 選ばれたグループの平均色を計算
                        // --------------------------------------------
                        double oilB = sumB[dominantBin] /
                                      (double)maxCount;

                        double oilG = sumG[dominantBin] /
                                      (double)maxCount;

                        double oilR = sumR[dominantBin] /
                                      (double)maxCount;

                        // --------------------------------------------
                        // 4. 元画像と油絵風画像を合成
                        // --------------------------------------------
                        int sourceIndex = sourceRow + x * 4;
                        int resultIndex = resultRow + x * 4;

                        double originalB = sourceBytes[sourceIndex];
                        double originalG = sourceBytes[sourceIndex + 1];
                        double originalR = sourceBytes[sourceIndex + 2];

                        resultBytes[resultIndex] = ClampByte(
                            originalB * (1.0 - ratio) +
                            oilB * ratio);

                        resultBytes[resultIndex + 1] = ClampByte(
                            originalG * (1.0 - ratio) +
                            oilG * ratio);

                        resultBytes[resultIndex + 2] = ClampByte(
                            originalR * (1.0 - ratio) +
                            oilR * ratio);

                        // 透明度は元画像を維持
                        resultBytes[resultIndex + 3] =
                            sourceBytes[sourceIndex + 3];
                    }
                }

                // ----------------------------------------------------
                // 結果をBitmapへコピー
                // ----------------------------------------------------
                for (int y = 0; y < height; y++)
                {
                    Marshal.Copy(
                        resultBytes,
                        y * resultRowSize,
                        IntPtr.Add(resultData.Scan0, y * resultStride),
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
            value = Math.Clamp(value, trackBar.Minimum, trackBar.Maximum);

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
            // 輪郭抽出
            else if (textBox == ContourExTxt)
            {
                ApplyIntegerInput(
                    ContourExTxt,
                    ContourExBar,
                    UpdateCorrectionPreview);
            }
            // モザイク
            else if (textBox == mosaicTxt)
            {
                ApplyIntegerInput(
                    mosaicTxt,
                    mosaicBar,
                    UpdateCorrectionPreview);
            }
            // ピクセル化
            else if (textBox == pixelTxt)
            {
                ApplyIntegerInput(
                    pixelTxt,
                    pixelBar,
                    UpdateCorrectionPreview);
            }
            // 周辺減光
            else if (textBox == dimmingTxt)
            {
                ApplyIntegerInput(
                    dimmingTxt,
                    dimmingBar,
                    UpdateCorrectionPreview);
            }
            // 鉛筆画風
            else if (textBox == pencilTxt)
            {
                ApplyIntegerInput(
                    pencilTxt,
                    pencilBar,
                    UpdateCorrectionPreview);
            }
            // 水彩画風
            else if (textBox == watercolorTxt)
            {
                ApplyIntegerInput(
                    watercolorTxt,
                    watercolorBar,
                    UpdateCorrectionPreview);
            }
            // 油絵風
            else if (textBox == oilpaintingTxt)
            {
                ApplyIntegerInput(
                    oilpaintingTxt,
                    oilpaintingBar,
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
                // アート表現の子ノードは相互排他
                else if (e.Node.Parent.Text == "アート表現" && e.Node.Checked)
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
            else if (e.Node.Parent.Text == "加工・変形")
            {
                ChangeProcessing(e.Node);
            }
            else if (e.Node.Parent.Text == "光・レンズ")
            {
                ChangeLighting(e.Node);
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
            sepiaPanel.Visible = false;
            grayscalePanel.Visible = false;
            negaPosiAllPanel.Visible = false;
            negaPosiRedPanel.Visible = false;
            negaPosiGreenPanel.Visible = false;
            negaPosiBluePanel.Visible = false;
            monochromePanel.Visible = false;
            embossPanel.Visible = false;
            noisePanel.Visible = false;
            ContourExPanel.Visible = false;
            mosaicPanel.Visible = false;
            pixelPanel.Visible = false;
            dimmingPanel.Visible = false;
            pencilPanel.Visible = false;
            watercolorPanel.Visible = false;
            oilpaintingPanel.Visible = false;


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
            // セピア
            else if (selectedNode.Text == "セピア" && selectedNode.Checked)
            {
                sepiaPanel.Visible = true;
            }
            // グレースケール
            else if (selectedNode.Text == "グレースケール" && selectedNode.Checked)
            {
                grayscalePanel.Visible = true;
            }
            // ネガポジ反転(全チャンネル)
            else if (selectedNode.Text == "ネガポジ反転(全チャンネル)" && selectedNode.Checked)
            {
                negaPosiAllPanel.Visible = true;
            }
            // ネガポジ反転(赤(R))
            else if (selectedNode.Text == "ネガポジ反転(赤(R))" && selectedNode.Checked)
            {
                negaPosiRedPanel.Visible = true;
            }
            // ネガポジ反転(緑(G))
            else if (selectedNode.Text == "ネガポジ反転(緑(G))" && selectedNode.Checked)
            {
                negaPosiGreenPanel.Visible = true;
            }
            // ネガポジ反転(青(B))
            else if (selectedNode.Text == "ネガポジ反転(青(B))" && selectedNode.Checked)
            {
                negaPosiBluePanel.Visible = true;
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
            // モザイクが選択され、チェックされている場合
            else if (selectedNode.Text == "モザイク" && selectedNode.Checked)
            {
                mosaicPanel.Visible = true;
            }
            // ピクセル化が選択され、チェックされている場合
            else if (selectedNode.Text == "ピクセル化" && selectedNode.Checked)
            {
                pixelPanel.Visible = true;
            }
            // 周辺減光が選択され、チェックされている場合
            else if (selectedNode.Text == "周辺減光" && selectedNode.Checked)
            {
                dimmingPanel.Visible = true;
            }
            //鉛筆画風が選択され、チェックされている場合
            else if (selectedNode.Text == "鉛筆画風" && selectedNode.Checked)
            {
                pencilPanel.Visible = true;
            }
            // 水彩画風が選択され、チェックされている場合
            else if (selectedNode.Text == "水彩画風" && selectedNode.Checked)
            {
                watercolorPanel.Visible = true;
            }
            // 油絵風が選択され、チェックされている場合
            else if (selectedNode.Text == "油絵風" && selectedNode.Checked)
            {
                oilpaintingPanel.Visible = true;
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
                    sharpBar.Value = 99;
                    sharpTxt.Text = "99";
                }
                else
                {
                    _currentImageEffect = ImageEffect.None;

                    // ぼかしの設定を初期化
                    blurBar.Value = 10;
                    blurTxt.Text = "10";
                }
            }

            // シャープ
            else if (selectedNode.Text == "シャープ")
            {
                if (selectedNode.Checked)
                {
                    _currentImageEffect = ImageEffect.Sharpen;

                    // ぼかしの設定を初期化
                    blurBar.Value = 10;
                    blurTxt.Text = "10";
                }
                else
                {
                    _currentImageEffect = ImageEffect.None;

                    // シャープの設定を初期化
                    sharpBar.Value = 99;
                    sharpTxt.Text = "99";
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
            // チェック状態だけを切り替える
            // スライダーの値は保持する
            UpdateCorrectionPreview();

        }

        // ============================================================
        // TreeViewから加工・変形を切り替える
        // ============================================================
        private void ChangeProcessing(TreeNode selectedNode)
        {
            // チェック状態だけを切り替える
            // スライダーの値は保持する
            UpdateCorrectionPreview();

        }

        // ============================================================
        // TreeViewから光・レンズを切り替える
        // ============================================================
        private void ChangeLighting(TreeNode selectedNode)
        {
            // チェック状態だけを切り替える
            // スライダーの値は保持する
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

        // ============================================================
        // 輪郭抽出のスライダー
        // ============================================================
        private void ContourExBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            ContourExTxt.Text = ContourExBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // モザイクのスライダー
        // ============================================================
        private void mosaicBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            mosaicTxt.Text = mosaicBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // ピクセル化のスライダー
        // ============================================================
        private void pixelBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            pixelTxt.Text = pixelBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 周辺減光のスライダー
        // ============================================================
        private void dimmingBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            dimmingTxt.Text = dimmingBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 鉛筆画風のスライダー
        // ============================================================
        private void pencilBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            pencilTxt.Text = pencilBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 水彩画風のスライダー
        // ============================================================
        private void watercolorBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            watercolorTxt.Text = watercolorBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // 油絵風のスライダー
        // ============================================================
        private void oilpaintingBar_Scroll(object sender, EventArgs e)
        {
            // 数値表示を更新
            oilpaintingTxt.Text = oilpaintingBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }

        // ============================================================
        // セピアのスライダー
        // ============================================================
        private void sepiaBar_Scroll(object sender, EventArgs e)
        {
            _sepiaStrength = sepiaBar.Value;

            // 数値表示を更新
            sepiaTxt.Text = sepiaBar.Value.ToString();

            // プレビューを更新
            UpdateCorrectionPreview();
        }
    }
}
