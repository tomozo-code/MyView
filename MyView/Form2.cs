using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

// --------------------------------------------------------
// バージョン情報フォーム
// --------------------------------------------------------

namespace MyView
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 600;
            this.MinimumSize = new Size(300, 300);

            // スパン
            splitContainer1.SplitterDistance = 200;
            splitContainer1.Panel1MinSize = 100;


            // 使い方表示用TextBox
            UseTxtBox.ReadOnly = true;
            UseTxtBox.BorderStyle = BorderStyle.FixedSingle;
            UseTxtBox.BackColor = this.BackColor;
            UseTxtBox.TabStop = false;
            UseTxtBox.Dock = DockStyle.Fill;

            // ツリービュー
            treeView1.Dock = DockStyle.Fill;
            treeView1.BackColor = this.BackColor;
            treeView1.ExpandAll();

            // EnterキーをOKボタンに割り当て
            this.AcceptButton = OkBtn;

            UseTxtBox.Text = "簡単な使い方、サードパーティライセンスの情報を表示します。" + Environment.NewLine +
                "・フォルダ内の画像がサムネイル表示されます" + Environment.NewLine +
                "・サムネイルをダブルクリックすると画像単体が画像ビューに表示されます" + Environment.NewLine +
                "・フォルダ内の画像を一覧印刷します";

            linkLabel1.Tag = "既定のブラウザで " + linkLabel1.Text + " を開きます";

        }

        // ============================================================
        // フォームをロードしたとき
        // ============================================================
        private void Form2_Load(object sender, EventArgs e)
        {
            // アセンブリ情報を取得して表示
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version?.ToString() ?? "不明";

            TitleTxt.Text = "ともさんの画像一覧帖";
            labelVersion.Text = $"Version: {version}";
            labelCopyright.Text = "Copyright(c) 2026 ともさん";

            // ツールチップ設定(通常コントロール用:Tagに表示させたい内容を書く)
            SetTooltipAll(this);

        }

        // ============================================================
        // OKを押したとき
        // ============================================================
        private void OkBtn_Click(object sender, EventArgs e)
        {
            // 現在のフォームを閉じる
            this.Close();
        }

        // ============================================================
        // ラベルをクリックしてGitHubを開く
        // ============================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // ブラウザを開くためのURLを指定
            string url = linkLabel1.Text;

            // 既定のブラウザで開く
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        // ============================================================
        // マウスONで説明の実行(通常コントロール) Tagに書いたもの
        // ============================================================
        private void SetTooltipAll(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Tag != null)
                {
                    // ツールチップにヒント
                    toolTip1.SetToolTip(ctrl, ctrl.Tag.ToString());
                }

                // 子コントロールも再帰
                if (ctrl.HasChildren)
                {
                    SetTooltipAll(ctrl);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            switch (e.Node?.Text)
            {
                case "簡単な使い方":
                    UseTxtBox.Text = "簡単な使い方、サードパーティライセンスの情報を表示します。" + Environment.NewLine +
                        "・フォルダ内の画像がサムネイル表示されます" + Environment.NewLine +
                        "・サムネイルをダブルクリックすると画像単体が画像ビューに表示されます" + Environment.NewLine +
                        "・フォルダ内の画像を一覧印刷します";
                    break;

                case "サムネイル":
                    UseTxtBox.Text = "画像を選択し数字キーを押すとクリップボードにコピーされます。" + Environment.NewLine +
                        "・１キー：フルパス（例：C:\\Pictures\\旅行\\IMG_001.jpg）" + Environment.NewLine +
                        "・２キー：ファイル名（例：IMG_001.jpg）" + Environment.NewLine +
                        "・３キー：フォルダーパス（例：C:\\Pictures\\旅行）" + Environment.NewLine +
                        "・４キー：画像そのもの" + Environment.NewLine +
                        "・５キー：拡張子（例：.jpg）" + Environment.NewLine +
                        "・６キー：ファイルサイズ（例：3.25 MB）" + Environment.NewLine +
                        "・７キー：画像サイズ（例：4032 × 3024）" + Environment.NewLine +
                        "・８キー：更新日時（例：2026 /09/09 15:32:10）" + Environment.NewLine +
                        "・９キー：作成日時（例：2026 /08/08 10:15:22）"; break;

                case "画像ビュー":
                    UseTxtBox.Text = "サムネイルをダブルクリックすると画像単体が画像ビューに表示されます。" + Environment.NewLine +
                        "・前の画像：上スクロール、↑キー、←キー、PageUpキー、backSpaceキー" + Environment.NewLine +
                        "・次の画像：下スクロール、↓キー、→キー、PageDownキー、Spaceキー" + Environment.NewLine +
                        "・拡大：Ctrl＋上スクロール、＋キー" + Environment.NewLine +
                        "・縮小：Ctrl＋下スクロールで縮小、－キー" + Environment.NewLine +
                        "・画像内移動：右ドラッグ、Ctrl＋矢印キー(1pxずつ)、Ctrl＋Shift＋矢印キー(10pxずつ)" + Environment.NewLine +
                        "・左ドラッグで枠描画：" + Environment.NewLine +
                        "　　枠内クリックで拡大" + Environment.NewLine +
                        "　　枠線の四隅と上下左右のハンドルをドラッグすると枠サイズ変更" + Environment.NewLine +
                        "　　コピーもしくは Ctrl＋Cで枠内画像をコピー" + Environment.NewLine +
                        "・画像に名前を付けて保存(Ctrl＋S)" + Environment.NewLine +
                        "・画像をコピー(Ctrl＋C)" + Environment.NewLine +
                        "・クリップボードの画像を貼り付け(Ctrl＋V)" + Environment.NewLine +
                        "・左へ90°回転(Ctrl＋L)" + Environment.NewLine +
                        "・右へ90°回転(Ctrl＋R)";
                    break;

                case "印刷プレビュー":
                    UseTxtBox.Text = "選択中のフォルダ内の画像を一覧印刷します。" + Environment.NewLine +
                        "・前ページ：上スクロール" + Environment.NewLine + 
                        "・次ページ：下スクロール" + Environment.NewLine +
                        "・拡大：Ctrl＋上スクロール" + Environment.NewLine + 
                        "・主将：Ctrl＋下スクロール" + Environment.NewLine +
                        "・印刷プレビュー内移動：右ドラッグ";
                    break;

                case "サードパーティライセンス":
                    UseTxtBox.Text = "本ソフトウェアは以下のオープンソースソフトウェアを使用しています：" + Environment.NewLine +
                        "・PhotoSauce.MagicScaler(MITライセンス)" + Environment.NewLine + Environment.NewLine +
                        "本ソフトウェアのメニューアイコンは以下サイトの画像を使用しています：" + Environment.NewLine +
                        "・ICOOON MONO(https://icooon-mono.com/)";
                    break;

                default:
                    UseTxtBox.Text = "簡単な使い方、サードパーティライセンスの情報を表示します。" + Environment.NewLine +
                        "・フォルダ内の画像がサムネイル表示されます" + Environment.NewLine +
                        "・サムネイルをダブルクリックすると画像単体が画像ビューに表示されます" + Environment.NewLine +
                        "・フォルダ内の画像を一覧印刷します";
                    break;
            }

        }
    }
}
