using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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

            this.Width = 500;
            this.Height = 450;
            this.MinimumSize = new Size(300, 300);

            // 使い方表示用TextBox
            UseTxtBox.ReadOnly = true;
            UseTxtBox.BorderStyle = BorderStyle.FixedSingle;
            UseTxtBox.BackColor = this.BackColor;
            UseTxtBox.TabStop = false;

            // EnterキーをOKボタンに割り当て
            this.AcceptButton = OkBtn;

            UseTxtBox.Text = "使い方：画像を選択し数字キーを押すとクリップボードにコピーされます" + Environment.NewLine +
    "・１キー：フルパス（例：C:\\Pictures\\旅行\\IMG_001.jpg）" + Environment.NewLine +
    "・２キー：ファイル名（例：IMG_001.jpg）" + Environment.NewLine +
    "・３キー：フォルダーパス（例：C:\\Pictures\\旅行）" + Environment.NewLine +
    "・４キー：画像そのもの" + Environment.NewLine +
    "・５キー：拡張子（例：.jpg）" + Environment.NewLine +
    "・６キー：ファイルサイズ（例：3.25 MB）" + Environment.NewLine +
    "・７キー：画像サイズ（例：4032 × 3024）" + Environment.NewLine +
    "・８キー：更新日時（例：2026 /09/09 15:32:10）" + Environment.NewLine +
    "・９キー：作成日時（例：2026 /08/08 10:15:22）";
        }

        // --------------------------------------------------------
        // フォームをロードしたとき
        // --------------------------------------------------------

        private void Form2_Load(object sender, EventArgs e)
        {
            // アセンブリ情報を取得して表示
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version?.ToString() ?? "不明";

            TitleTxt.Text = "ともさんの画像一覧帖";
            labelVersion.Text = $"Version: {version}";
            labelCopyright.Text = "Copyright(c) 2026 ともさん";
        }

        // --------------------------------------------------------
        // OKを押したとき
        // --------------------------------------------------------
        private void OkBtn_Click(object sender, EventArgs e)
        {
            // 現在のフォームを閉じる
            this.Close();
        }
    }
}
