namespace MyView
{
    partial class Form6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("ぼかし");
            TreeNode treeNode2 = new TreeNode("シャープ");
            TreeNode treeNode3 = new TreeNode("基本補正", new TreeNode[] { treeNode1, treeNode2 });
            TreeNode treeNode4 = new TreeNode("セピア");
            TreeNode treeNode5 = new TreeNode("グレースケール");
            TreeNode treeNode6 = new TreeNode("ネガポジ反転(全チャンネル)");
            TreeNode treeNode7 = new TreeNode("ネガポジ反転(赤(R))");
            TreeNode treeNode8 = new TreeNode("ネガポジ反転(緑(G))");
            TreeNode treeNode9 = new TreeNode("ネガポジ反転(青(B))");
            TreeNode treeNode10 = new TreeNode("モノクローム");
            TreeNode treeNode11 = new TreeNode("色調加工", new TreeNode[] { treeNode4, treeNode5, treeNode6, treeNode7, treeNode8, treeNode9, treeNode10 });
            TreeNode treeNode12 = new TreeNode("エンボス");
            TreeNode treeNode13 = new TreeNode("ノイズ");
            TreeNode treeNode14 = new TreeNode("輪郭抽出");
            TreeNode treeNode15 = new TreeNode("輪郭・質感", new TreeNode[] { treeNode12, treeNode13, treeNode14 });
            TreeNode treeNode16 = new TreeNode("モザイク");
            TreeNode treeNode17 = new TreeNode("ピクセル化");
            TreeNode treeNode18 = new TreeNode("加工・変形", new TreeNode[] { treeNode16, treeNode17 });
            TreeNode treeNode19 = new TreeNode("周辺減光");
            TreeNode treeNode20 = new TreeNode("光・レンズ", new TreeNode[] { treeNode19 });
            TreeNode treeNode21 = new TreeNode("鉛筆画風");
            TreeNode treeNode22 = new TreeNode("水彩画風");
            TreeNode treeNode23 = new TreeNode("油絵風");
            TreeNode treeNode24 = new TreeNode("アート表現", new TreeNode[] { treeNode21, treeNode22, treeNode23 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel1 = new Panel();
            CancelBtn = new Button();
            ResetBtn = new Button();
            OkBtn = new Button();
            BrightnessTxt = new TextBox();
            BrightnessBar = new TrackBar();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ColorBalanceTxt_B = new TextBox();
            ColorBalanceBar_B = new TrackBar();
            ColorBalanceTxt_G = new TextBox();
            ColorBalanceBar_G = new TrackBar();
            ColorBalanceTxt_R = new TextBox();
            ColorBalanceBar_R = new TrackBar();
            contrastTxt = new TextBox();
            contrastBar = new TrackBar();
            GammaCorrectionTxt = new TextBox();
            GammaCorrectionBar = new TrackBar();
            SaturationTxt = new TextBox();
            SaturationBar = new TrackBar();
            label4 = new Label();
            label5 = new Label();
            toolTip1 = new ToolTip(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel2 = new Panel();
            label6 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            panel3 = new Panel();
            colorCorrectionResetBtn = new Button();
            tabPage2 = new TabPage();
            splitContainer2 = new SplitContainer();
            treeView1 = new TreeView();
            mosaicPanel = new Panel();
            mosaicGroup = new GroupBox();
            mosaicTxt = new TextBox();
            mosaicBar = new TrackBar();
            ContourExPanel = new Panel();
            ContourExGroup = new GroupBox();
            ContourExTxt = new TextBox();
            ContourExBar = new TrackBar();
            noisePanel = new Panel();
            noiseGroup = new GroupBox();
            noiseTxt = new TextBox();
            noiseBar = new TrackBar();
            embossPanel = new Panel();
            embossGroup = new GroupBox();
            embossCheck = new CheckBox();
            embossTxt = new TextBox();
            embossBar = new TrackBar();
            monochromePanel = new Panel();
            groupBox2 = new GroupBox();
            monochromeTxt = new TextBox();
            monochromeBar = new TrackBar();
            sharpPanel = new Panel();
            textBox2 = new TextBox();
            sharpGroup = new GroupBox();
            sharpTxt = new TextBox();
            sharpBar = new TrackBar();
            blurPanel = new Panel();
            textBox1 = new TextBox();
            blurGroup = new GroupBox();
            blurTxt = new TextBox();
            blurBar = new TrackBar();
            textBox3 = new TextBox();
            panel5 = new Panel();
            colorFilterResetBtn = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            splitContainer1 = new SplitContainer();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BrightnessBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_B).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_G).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_R).BeginInit();
            ((System.ComponentModel.ISupportInitialize)contrastBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GammaCorrectionBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SaturationBar).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            mosaicPanel.SuspendLayout();
            mosaicGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mosaicBar).BeginInit();
            ContourExPanel.SuspendLayout();
            ContourExGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ContourExBar).BeginInit();
            noisePanel.SuspendLayout();
            noiseGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)noiseBar).BeginInit();
            embossPanel.SuspendLayout();
            embossGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)embossBar).BeginInit();
            monochromePanel.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)monochromeBar).BeginInit();
            sharpPanel.SuspendLayout();
            sharpGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sharpBar).BeginInit();
            blurPanel.SuspendLayout();
            blurGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)blurBar).BeginInit();
            panel5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 724);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1251, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(100, 21);
            toolStripStatusLabel1.Text = "ここにステータス";
            // 
            // panel1
            // 
            panel1.Controls.Add(CancelBtn);
            panel1.Controls.Add(ResetBtn);
            panel1.Controls.Add(OkBtn);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 669);
            panel1.Name = "panel1";
            panel1.Size = new Size(1251, 55);
            panel1.TabIndex = 1;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(236, 13);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(100, 30);
            CancelBtn.TabIndex = 2;
            CancelBtn.Tag = "色彩補正をキャンセルし、ウィンドウを閉じます。";
            CancelBtn.Text = "キャンセル";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            CancelBtn.MouseEnter += Control_MouseEnter;
            CancelBtn.MouseLeave += Control_MouseLeave;
            // 
            // ResetBtn
            // 
            ResetBtn.Location = new Point(121, 13);
            ResetBtn.Name = "ResetBtn";
            ResetBtn.Size = new Size(100, 30);
            ResetBtn.TabIndex = 1;
            ResetBtn.Tag = "すべての補正を初期値に戻します。";
            ResetBtn.Text = "オールリセット";
            ResetBtn.UseVisualStyleBackColor = true;
            ResetBtn.Click += ResetBtn_Click;
            ResetBtn.MouseEnter += Control_MouseEnter;
            ResetBtn.MouseLeave += Control_MouseLeave;
            // 
            // OkBtn
            // 
            OkBtn.Location = new Point(12, 13);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(100, 30);
            OkBtn.TabIndex = 0;
            OkBtn.Tag = "ウィンドウを閉じて、色彩補正を適応します。";
            OkBtn.Text = "OK";
            OkBtn.UseVisualStyleBackColor = true;
            OkBtn.Click += OkBtn_Click;
            OkBtn.MouseEnter += Control_MouseEnter;
            OkBtn.MouseLeave += Control_MouseLeave;
            // 
            // BrightnessTxt
            // 
            BrightnessTxt.Location = new Point(122, 12);
            BrightnessTxt.Name = "BrightnessTxt";
            BrightnessTxt.Size = new Size(53, 29);
            BrightnessTxt.TabIndex = 1;
            BrightnessTxt.Tag = "明るさ(範囲：-255～255)";
            BrightnessTxt.TextAlign = HorizontalAlignment.Right;
            BrightnessTxt.Click += allSelect_Click;
            BrightnessTxt.Enter += allSelect_Enter;
            BrightnessTxt.KeyDown += NumericTextBox_KeyDown;
            BrightnessTxt.MouseEnter += Control_MouseEnter;
            BrightnessTxt.MouseLeave += Control_MouseLeave;
            // 
            // BrightnessBar
            // 
            BrightnessBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BrightnessBar.AutoSize = false;
            BrightnessBar.Location = new Point(181, 12);
            BrightnessBar.Maximum = 255;
            BrightnessBar.Minimum = -255;
            BrightnessBar.Name = "BrightnessBar";
            BrightnessBar.Size = new Size(870, 30);
            BrightnessBar.TabIndex = 2;
            BrightnessBar.Tag = "明るさ(範囲：-255～255)";
            BrightnessBar.TickStyle = TickStyle.None;
            BrightnessBar.Scroll += BrightnessBar_Scroll;
            BrightnessBar.MouseEnter += Control_MouseEnter;
            BrightnessBar.MouseLeave += Control_MouseLeave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 132);
            label3.Name = "label3";
            label3.Size = new Size(109, 21);
            label3.TabIndex = 9;
            label3.Text = "青(B)バランス：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 92);
            label2.Name = "label2";
            label2.Size = new Size(111, 21);
            label2.TabIndex = 6;
            label2.Text = "緑(G)バランス：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 52);
            label1.Name = "label1";
            label1.Size = new Size(110, 21);
            label1.TabIndex = 3;
            label1.Text = "赤(R)バランス：";
            // 
            // ColorBalanceTxt_B
            // 
            ColorBalanceTxt_B.Location = new Point(122, 132);
            ColorBalanceTxt_B.Name = "ColorBalanceTxt_B";
            ColorBalanceTxt_B.Size = new Size(53, 29);
            ColorBalanceTxt_B.TabIndex = 10;
            ColorBalanceTxt_B.Tag = "青(B)バランス(範囲：-255～255)";
            ColorBalanceTxt_B.TextAlign = HorizontalAlignment.Right;
            ColorBalanceTxt_B.Click += allSelect_Click;
            ColorBalanceTxt_B.Enter += allSelect_Enter;
            ColorBalanceTxt_B.KeyDown += NumericTextBox_KeyDown;
            ColorBalanceTxt_B.MouseEnter += Control_MouseEnter;
            ColorBalanceTxt_B.MouseLeave += Control_MouseLeave;
            // 
            // ColorBalanceBar_B
            // 
            ColorBalanceBar_B.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ColorBalanceBar_B.AutoSize = false;
            ColorBalanceBar_B.Location = new Point(181, 132);
            ColorBalanceBar_B.Maximum = 255;
            ColorBalanceBar_B.Minimum = -255;
            ColorBalanceBar_B.Name = "ColorBalanceBar_B";
            ColorBalanceBar_B.Size = new Size(870, 30);
            ColorBalanceBar_B.TabIndex = 11;
            ColorBalanceBar_B.Tag = "青(B)バランス(範囲：-255～255)";
            ColorBalanceBar_B.TickStyle = TickStyle.None;
            ColorBalanceBar_B.Scroll += ColorBalanceBar_B_Scroll;
            ColorBalanceBar_B.MouseEnter += Control_MouseEnter;
            ColorBalanceBar_B.MouseLeave += Control_MouseLeave;
            // 
            // ColorBalanceTxt_G
            // 
            ColorBalanceTxt_G.Location = new Point(122, 92);
            ColorBalanceTxt_G.Name = "ColorBalanceTxt_G";
            ColorBalanceTxt_G.Size = new Size(53, 29);
            ColorBalanceTxt_G.TabIndex = 7;
            ColorBalanceTxt_G.Tag = "緑(G)バランス(範囲：-255～255)";
            ColorBalanceTxt_G.TextAlign = HorizontalAlignment.Right;
            ColorBalanceTxt_G.Click += allSelect_Click;
            ColorBalanceTxt_G.Enter += allSelect_Enter;
            ColorBalanceTxt_G.KeyDown += NumericTextBox_KeyDown;
            ColorBalanceTxt_G.MouseEnter += Control_MouseEnter;
            ColorBalanceTxt_G.MouseLeave += Control_MouseLeave;
            // 
            // ColorBalanceBar_G
            // 
            ColorBalanceBar_G.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ColorBalanceBar_G.AutoSize = false;
            ColorBalanceBar_G.Location = new Point(181, 92);
            ColorBalanceBar_G.Maximum = 255;
            ColorBalanceBar_G.Minimum = -255;
            ColorBalanceBar_G.Name = "ColorBalanceBar_G";
            ColorBalanceBar_G.Size = new Size(870, 30);
            ColorBalanceBar_G.TabIndex = 8;
            ColorBalanceBar_G.Tag = "緑(G)バランス(範囲：-255～255)";
            ColorBalanceBar_G.TickStyle = TickStyle.None;
            ColorBalanceBar_G.Scroll += ColorBalanceBar_G_Scroll;
            ColorBalanceBar_G.MouseEnter += Control_MouseEnter;
            ColorBalanceBar_G.MouseLeave += Control_MouseLeave;
            // 
            // ColorBalanceTxt_R
            // 
            ColorBalanceTxt_R.Location = new Point(122, 52);
            ColorBalanceTxt_R.Name = "ColorBalanceTxt_R";
            ColorBalanceTxt_R.Size = new Size(53, 29);
            ColorBalanceTxt_R.TabIndex = 4;
            ColorBalanceTxt_R.Tag = "赤(R)バランス(範囲：-255～255)";
            ColorBalanceTxt_R.TextAlign = HorizontalAlignment.Right;
            ColorBalanceTxt_R.Click += allSelect_Click;
            ColorBalanceTxt_R.Enter += allSelect_Enter;
            ColorBalanceTxt_R.KeyDown += NumericTextBox_KeyDown;
            ColorBalanceTxt_R.MouseEnter += Control_MouseEnter;
            ColorBalanceTxt_R.MouseLeave += Control_MouseLeave;
            // 
            // ColorBalanceBar_R
            // 
            ColorBalanceBar_R.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ColorBalanceBar_R.AutoSize = false;
            ColorBalanceBar_R.Location = new Point(181, 52);
            ColorBalanceBar_R.Maximum = 255;
            ColorBalanceBar_R.Minimum = -255;
            ColorBalanceBar_R.Name = "ColorBalanceBar_R";
            ColorBalanceBar_R.Size = new Size(870, 30);
            ColorBalanceBar_R.TabIndex = 5;
            ColorBalanceBar_R.Tag = "赤(R)バランス(範囲：-255～255)";
            ColorBalanceBar_R.TickStyle = TickStyle.None;
            ColorBalanceBar_R.Scroll += ColorBalanceBar_R_Scroll;
            ColorBalanceBar_R.MouseEnter += Control_MouseEnter;
            ColorBalanceBar_R.MouseLeave += Control_MouseLeave;
            // 
            // contrastTxt
            // 
            contrastTxt.Location = new Point(122, 172);
            contrastTxt.Name = "contrastTxt";
            contrastTxt.Size = new Size(53, 29);
            contrastTxt.TabIndex = 13;
            contrastTxt.Tag = "コントラスト(範囲：-127～127)";
            contrastTxt.TextAlign = HorizontalAlignment.Right;
            contrastTxt.Click += allSelect_Click;
            contrastTxt.Enter += allSelect_Enter;
            contrastTxt.KeyDown += NumericTextBox_KeyDown;
            contrastTxt.MouseEnter += Control_MouseEnter;
            contrastTxt.MouseLeave += Control_MouseLeave;
            // 
            // contrastBar
            // 
            contrastBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            contrastBar.AutoSize = false;
            contrastBar.Location = new Point(181, 172);
            contrastBar.Maximum = 127;
            contrastBar.Minimum = -127;
            contrastBar.Name = "contrastBar";
            contrastBar.Size = new Size(870, 30);
            contrastBar.TabIndex = 14;
            contrastBar.Tag = "コントラスト(範囲：-127～127)";
            contrastBar.TickStyle = TickStyle.None;
            contrastBar.Scroll += contrastBar_Scroll;
            contrastBar.MouseEnter += Control_MouseEnter;
            contrastBar.MouseLeave += Control_MouseLeave;
            // 
            // GammaCorrectionTxt
            // 
            GammaCorrectionTxt.Location = new Point(122, 212);
            GammaCorrectionTxt.Name = "GammaCorrectionTxt";
            GammaCorrectionTxt.Size = new Size(53, 29);
            GammaCorrectionTxt.TabIndex = 16;
            GammaCorrectionTxt.Tag = "ガンマ補正(範囲：0.01～6.99)";
            GammaCorrectionTxt.TextAlign = HorizontalAlignment.Right;
            GammaCorrectionTxt.Click += allSelect_Click;
            GammaCorrectionTxt.Enter += allSelect_Enter;
            GammaCorrectionTxt.KeyDown += NumericTextBox_KeyDown;
            GammaCorrectionTxt.MouseEnter += Control_MouseEnter;
            GammaCorrectionTxt.MouseLeave += Control_MouseLeave;
            // 
            // GammaCorrectionBar
            // 
            GammaCorrectionBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GammaCorrectionBar.AutoSize = false;
            GammaCorrectionBar.Location = new Point(181, 212);
            GammaCorrectionBar.Maximum = 699;
            GammaCorrectionBar.Minimum = 1;
            GammaCorrectionBar.Name = "GammaCorrectionBar";
            GammaCorrectionBar.Size = new Size(870, 30);
            GammaCorrectionBar.TabIndex = 17;
            GammaCorrectionBar.Tag = "ガンマ補正(範囲：0.01～6.99)";
            GammaCorrectionBar.TickStyle = TickStyle.None;
            GammaCorrectionBar.Value = 100;
            GammaCorrectionBar.Scroll += GammaCorrectionBar_Scroll;
            GammaCorrectionBar.MouseEnter += Control_MouseEnter;
            GammaCorrectionBar.MouseLeave += Control_MouseLeave;
            // 
            // SaturationTxt
            // 
            SaturationTxt.Location = new Point(122, 252);
            SaturationTxt.Name = "SaturationTxt";
            SaturationTxt.Size = new Size(53, 29);
            SaturationTxt.TabIndex = 19;
            SaturationTxt.Tag = "彩度(範囲：-255～255)";
            SaturationTxt.TextAlign = HorizontalAlignment.Right;
            SaturationTxt.Click += allSelect_Click;
            SaturationTxt.Enter += allSelect_Enter;
            SaturationTxt.KeyDown += NumericTextBox_KeyDown;
            SaturationTxt.MouseEnter += Control_MouseEnter;
            SaturationTxt.MouseLeave += Control_MouseLeave;
            // 
            // SaturationBar
            // 
            SaturationBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SaturationBar.AutoSize = false;
            SaturationBar.Location = new Point(181, 252);
            SaturationBar.Maximum = 255;
            SaturationBar.Minimum = -255;
            SaturationBar.Name = "SaturationBar";
            SaturationBar.Size = new Size(870, 30);
            SaturationBar.TabIndex = 20;
            SaturationBar.Tag = "彩度(範囲：-255～255)";
            SaturationBar.TickStyle = TickStyle.None;
            SaturationBar.Scroll += SaturationBar_Scroll;
            SaturationBar.MouseEnter += Control_MouseEnter;
            SaturationBar.MouseLeave += Control_MouseLeave;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(381, 20);
            label4.TabIndex = 0;
            label4.Text = "オリジナル画像";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(390, 0);
            label5.Name = "label5";
            label5.Size = new Size(381, 20);
            label5.TabIndex = 1;
            label5.Text = "補正画像";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 15);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1077, 444);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(panel3);
            tabPage1.Location = new Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1069, 410);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "色彩補正";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(ColorBalanceBar_R);
            panel2.Controls.Add(SaturationTxt);
            panel2.Controls.Add(ColorBalanceTxt_R);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(ColorBalanceBar_G);
            panel2.Controls.Add(SaturationBar);
            panel2.Controls.Add(BrightnessBar);
            panel2.Controls.Add(GammaCorrectionTxt);
            panel2.Controls.Add(ColorBalanceTxt_G);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(GammaCorrectionBar);
            panel2.Controls.Add(BrightnessTxt);
            panel2.Controls.Add(contrastTxt);
            panel2.Controls.Add(ColorBalanceBar_B);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(contrastBar);
            panel2.Controls.Add(ColorBalanceTxt_B);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1063, 340);
            panel2.TabIndex = 22;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(51, 12);
            label6.Name = "label6";
            label6.Size = new Size(65, 21);
            label6.TabIndex = 0;
            label6.Text = "明るさ：";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(55, 252);
            label9.Name = "label9";
            label9.Size = new Size(58, 21);
            label9.TabIndex = 18;
            label9.Text = "彩度：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(19, 212);
            label8.Name = "label8";
            label8.Size = new Size(95, 21);
            label8.TabIndex = 15;
            label8.Text = "ガンマ補正：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(20, 172);
            label7.Name = "label7";
            label7.Size = new Size(94, 21);
            label7.TabIndex = 12;
            label7.Text = "コントラスト：";
            // 
            // panel3
            // 
            panel3.Controls.Add(colorCorrectionResetBtn);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(3, 362);
            panel3.Name = "panel3";
            panel3.Size = new Size(1063, 45);
            panel3.TabIndex = 23;
            // 
            // colorCorrectionResetBtn
            // 
            colorCorrectionResetBtn.Location = new Point(12, 7);
            colorCorrectionResetBtn.Name = "colorCorrectionResetBtn";
            colorCorrectionResetBtn.Size = new Size(154, 30);
            colorCorrectionResetBtn.TabIndex = 21;
            colorCorrectionResetBtn.Tag = "色彩補正を初期値に戻します。";
            colorCorrectionResetBtn.Text = "色彩補正リセット";
            colorCorrectionResetBtn.UseVisualStyleBackColor = true;
            colorCorrectionResetBtn.Click += colorCorrectionResetBtn_Click;
            colorCorrectionResetBtn.MouseEnter += Control_MouseEnter;
            colorCorrectionResetBtn.MouseLeave += Control_MouseLeave;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(splitContainer2);
            tabPage2.Controls.Add(panel5);
            tabPage2.Location = new Point(4, 30);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1069, 410);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "画像効果";
            // 
            // splitContainer2
            // 
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;
            splitContainer2.Cursor = Cursors.SizeWE;
            splitContainer2.Location = new Point(3, 12);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeView1);
            splitContainer2.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(mosaicPanel);
            splitContainer2.Panel2.Controls.Add(ContourExPanel);
            splitContainer2.Panel2.Controls.Add(noisePanel);
            splitContainer2.Panel2.Controls.Add(embossPanel);
            splitContainer2.Panel2.Controls.Add(monochromePanel);
            splitContainer2.Panel2.Controls.Add(sharpPanel);
            splitContainer2.Panel2.Controls.Add(blurPanel);
            splitContainer2.Panel2.Controls.Add(textBox3);
            splitContainer2.Panel2.Cursor = Cursors.Default;
            splitContainer2.Size = new Size(1043, 416);
            splitContainer2.SplitterDistance = 117;
            splitContainer2.SplitterWidth = 8;
            splitContainer2.TabIndex = 7;
            // 
            // treeView1
            // 
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.CheckBoxes = true;
            treeView1.Location = new Point(8, 17);
            treeView1.Name = "treeView1";
            treeNode1.Name = "ノード1";
            treeNode1.Text = "ぼかし";
            treeNode2.Name = "ノード3";
            treeNode2.Text = "シャープ";
            treeNode3.Name = "ノード0";
            treeNode3.Text = "基本補正";
            treeNode4.Name = "ノード4";
            treeNode4.Text = "セピア";
            treeNode5.Name = "ノード5";
            treeNode5.Text = "グレースケール";
            treeNode6.Name = "ノード6";
            treeNode6.Text = "ネガポジ反転(全チャンネル)";
            treeNode7.Name = "ノード7";
            treeNode7.Text = "ネガポジ反転(赤(R))";
            treeNode8.Name = "ノード8";
            treeNode8.Text = "ネガポジ反転(緑(G))";
            treeNode9.Name = "ノード9";
            treeNode9.Text = "ネガポジ反転(青(B))";
            treeNode10.Name = "ノード10";
            treeNode10.Text = "モノクローム";
            treeNode11.Name = "ノード2";
            treeNode11.Text = "色調加工";
            treeNode12.Name = "ノード12";
            treeNode12.Text = "エンボス";
            treeNode13.Name = "ノード13";
            treeNode13.Text = "ノイズ";
            treeNode14.Name = "ノード14";
            treeNode14.Text = "輪郭抽出";
            treeNode15.Name = "ノード11";
            treeNode15.Text = "輪郭・質感";
            treeNode16.Name = "ノード22";
            treeNode16.Text = "モザイク";
            treeNode17.Name = "ノード23";
            treeNode17.Text = "ピクセル化";
            treeNode18.Name = "ノード21";
            treeNode18.Text = "加工・変形";
            treeNode19.Name = "ノード16";
            treeNode19.Text = "周辺減光";
            treeNode20.Name = "ノード15";
            treeNode20.Text = "光・レンズ";
            treeNode21.Name = "ノード18";
            treeNode21.Text = "鉛筆画風";
            treeNode22.Name = "ノード19";
            treeNode22.Text = "水彩画風";
            treeNode23.Name = "ノード20";
            treeNode23.Text = "油絵風";
            treeNode24.Name = "ノード17";
            treeNode24.Text = "アート表現";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3, treeNode11, treeNode15, treeNode18, treeNode20, treeNode24 });
            treeView1.Size = new Size(104, 294);
            treeView1.TabIndex = 6;
            treeView1.BeforeCheck += treeView1_BeforeCheck;
            treeView1.AfterCheck += treeView1_AfterCheck;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // mosaicPanel
            // 
            mosaicPanel.Controls.Add(mosaicGroup);
            mosaicPanel.Location = new Point(464, 8);
            mosaicPanel.Name = "mosaicPanel";
            mosaicPanel.Size = new Size(200, 180);
            mosaicPanel.TabIndex = 9;
            mosaicPanel.Visible = false;
            // 
            // mosaicGroup
            // 
            mosaicGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mosaicGroup.Controls.Add(mosaicTxt);
            mosaicGroup.Controls.Add(mosaicBar);
            mosaicGroup.Location = new Point(16, 14);
            mosaicGroup.Name = "mosaicGroup";
            mosaicGroup.Size = new Size(160, 70);
            mosaicGroup.TabIndex = 1;
            mosaicGroup.TabStop = false;
            mosaicGroup.Text = "モザイク";
            // 
            // mosaicTxt
            // 
            mosaicTxt.Location = new Point(19, 28);
            mosaicTxt.Name = "mosaicTxt";
            mosaicTxt.Size = new Size(53, 29);
            mosaicTxt.TabIndex = 9;
            mosaicTxt.Tag = "モザイク(範囲：0～99)";
            mosaicTxt.TextAlign = HorizontalAlignment.Right;
            mosaicTxt.Click += allSelect_Click;
            mosaicTxt.Enter += allSelect_Enter;
            mosaicTxt.KeyDown += NumericTextBox_KeyDown;
            mosaicTxt.MouseEnter += Control_MouseEnter;
            mosaicTxt.MouseLeave += Control_MouseLeave;
            // 
            // mosaicBar
            // 
            mosaicBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mosaicBar.AutoSize = false;
            mosaicBar.Location = new Point(78, 28);
            mosaicBar.Maximum = 99;
            mosaicBar.Name = "mosaicBar";
            mosaicBar.Size = new Size(76, 30);
            mosaicBar.TabIndex = 8;
            mosaicBar.Tag = "モザイク(範囲：0～99)";
            mosaicBar.TickStyle = TickStyle.None;
            mosaicBar.Scroll += mosaicBar_Scroll;
            // 
            // ContourExPanel
            // 
            ContourExPanel.Controls.Add(ContourExGroup);
            ContourExPanel.Location = new Point(242, 211);
            ContourExPanel.Name = "ContourExPanel";
            ContourExPanel.Size = new Size(200, 180);
            ContourExPanel.TabIndex = 8;
            ContourExPanel.Visible = false;
            // 
            // ContourExGroup
            // 
            ContourExGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ContourExGroup.Controls.Add(ContourExTxt);
            ContourExGroup.Controls.Add(ContourExBar);
            ContourExGroup.Location = new Point(16, 15);
            ContourExGroup.Name = "ContourExGroup";
            ContourExGroup.Size = new Size(160, 70);
            ContourExGroup.TabIndex = 1;
            ContourExGroup.TabStop = false;
            ContourExGroup.Text = "輪郭抽出";
            // 
            // ContourExTxt
            // 
            ContourExTxt.Location = new Point(19, 28);
            ContourExTxt.Name = "ContourExTxt";
            ContourExTxt.Size = new Size(53, 29);
            ContourExTxt.TabIndex = 9;
            ContourExTxt.Tag = "輪郭抽出(範囲：0～99)";
            ContourExTxt.TextAlign = HorizontalAlignment.Right;
            ContourExTxt.Click += allSelect_Click;
            ContourExTxt.Enter += allSelect_Enter;
            ContourExTxt.KeyDown += NumericTextBox_KeyDown;
            ContourExTxt.MouseEnter += Control_MouseEnter;
            ContourExTxt.MouseLeave += Control_MouseLeave;
            // 
            // ContourExBar
            // 
            ContourExBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ContourExBar.AutoSize = false;
            ContourExBar.Location = new Point(78, 28);
            ContourExBar.Maximum = 99;
            ContourExBar.Name = "ContourExBar";
            ContourExBar.Size = new Size(76, 30);
            ContourExBar.TabIndex = 8;
            ContourExBar.Tag = "輪郭抽出(範囲：0～99)";
            ContourExBar.TickStyle = TickStyle.None;
            ContourExBar.Scroll += ContourExBar_Scroll;
            // 
            // noisePanel
            // 
            noisePanel.Controls.Add(noiseGroup);
            noisePanel.Location = new Point(242, 8);
            noisePanel.Name = "noisePanel";
            noisePanel.Size = new Size(200, 180);
            noisePanel.TabIndex = 7;
            noisePanel.Visible = false;
            // 
            // noiseGroup
            // 
            noiseGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            noiseGroup.Controls.Add(noiseTxt);
            noiseGroup.Controls.Add(noiseBar);
            noiseGroup.Location = new Point(16, 14);
            noiseGroup.Name = "noiseGroup";
            noiseGroup.Size = new Size(160, 70);
            noiseGroup.TabIndex = 0;
            noiseGroup.TabStop = false;
            noiseGroup.Text = "ノイズ";
            // 
            // noiseTxt
            // 
            noiseTxt.Location = new Point(19, 28);
            noiseTxt.Name = "noiseTxt";
            noiseTxt.Size = new Size(53, 29);
            noiseTxt.TabIndex = 9;
            noiseTxt.Tag = "ノイズ(範囲：0～99)";
            noiseTxt.TextAlign = HorizontalAlignment.Right;
            noiseTxt.Click += allSelect_Click;
            noiseTxt.Enter += allSelect_Enter;
            noiseTxt.KeyDown += NumericTextBox_KeyDown;
            noiseTxt.MouseEnter += Control_MouseEnter;
            noiseTxt.MouseLeave += Control_MouseLeave;
            // 
            // noiseBar
            // 
            noiseBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            noiseBar.AutoSize = false;
            noiseBar.Location = new Point(78, 28);
            noiseBar.Maximum = 99;
            noiseBar.Name = "noiseBar";
            noiseBar.Size = new Size(76, 30);
            noiseBar.TabIndex = 8;
            noiseBar.Tag = "ノイズ(範囲：0～99)";
            noiseBar.TickStyle = TickStyle.None;
            noiseBar.Scroll += noiseBar_Scroll;
            // 
            // embossPanel
            // 
            embossPanel.AutoScroll = true;
            embossPanel.Controls.Add(embossGroup);
            embossPanel.Location = new Point(111, 8);
            embossPanel.Name = "embossPanel";
            embossPanel.Size = new Size(200, 180);
            embossPanel.TabIndex = 3;
            embossPanel.Visible = false;
            // 
            // embossGroup
            // 
            embossGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            embossGroup.Controls.Add(embossCheck);
            embossGroup.Controls.Add(embossTxt);
            embossGroup.Controls.Add(embossBar);
            embossGroup.Location = new Point(16, 14);
            embossGroup.Name = "embossGroup";
            embossGroup.Size = new Size(170, 121);
            embossGroup.TabIndex = 9;
            embossGroup.TabStop = false;
            embossGroup.Text = "エンボス";
            // 
            // embossCheck
            // 
            embossCheck.AutoSize = true;
            embossCheck.Location = new Point(27, 76);
            embossCheck.Name = "embossCheck";
            embossCheck.Size = new Size(93, 25);
            embossCheck.TabIndex = 8;
            embossCheck.Text = "凹凸反転";
            embossCheck.UseVisualStyleBackColor = true;
            embossCheck.CheckedChanged += embossCheck_CheckedChanged;
            // 
            // embossTxt
            // 
            embossTxt.Location = new Point(18, 34);
            embossTxt.Name = "embossTxt";
            embossTxt.Size = new Size(53, 29);
            embossTxt.TabIndex = 7;
            embossTxt.Tag = "エンボス(範囲：0～99)";
            embossTxt.TextAlign = HorizontalAlignment.Right;
            embossTxt.Click += allSelect_Click;
            embossTxt.Enter += allSelect_Enter;
            embossTxt.KeyDown += NumericTextBox_KeyDown;
            embossTxt.MouseEnter += Control_MouseEnter;
            embossTxt.MouseLeave += Control_MouseLeave;
            // 
            // embossBar
            // 
            embossBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            embossBar.AutoSize = false;
            embossBar.Location = new Point(77, 34);
            embossBar.Maximum = 99;
            embossBar.Name = "embossBar";
            embossBar.Size = new Size(87, 30);
            embossBar.TabIndex = 5;
            embossBar.Tag = "エンボス(範囲：0～99)";
            embossBar.TickStyle = TickStyle.None;
            embossBar.Scroll += embossBar_Scroll;
            embossBar.MouseEnter += Control_MouseEnter;
            embossBar.MouseLeave += Control_MouseLeave;
            // 
            // monochromePanel
            // 
            monochromePanel.Controls.Add(groupBox2);
            monochromePanel.Location = new Point(6, 8);
            monochromePanel.Name = "monochromePanel";
            monochromePanel.Size = new Size(200, 180);
            monochromePanel.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(monochromeTxt);
            groupBox2.Controls.Add(monochromeBar);
            groupBox2.Location = new Point(16, 14);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(170, 70);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "モノクローム";
            // 
            // monochromeTxt
            // 
            monochromeTxt.Enabled = false;
            monochromeTxt.Location = new Point(19, 28);
            monochromeTxt.Name = "monochromeTxt";
            monochromeTxt.Size = new Size(53, 29);
            monochromeTxt.TabIndex = 1;
            monochromeTxt.Tag = "モノクローム(範囲：0～255)";
            monochromeTxt.TextAlign = HorizontalAlignment.Right;
            monochromeTxt.Click += allSelect_Click;
            monochromeTxt.Enter += allSelect_Enter;
            monochromeTxt.KeyDown += NumericTextBox_KeyDown;
            monochromeTxt.MouseEnter += Control_MouseEnter;
            monochromeTxt.MouseLeave += Control_MouseLeave;
            // 
            // monochromeBar
            // 
            monochromeBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            monochromeBar.AutoSize = false;
            monochromeBar.Enabled = false;
            monochromeBar.Location = new Point(78, 28);
            monochromeBar.Maximum = 255;
            monochromeBar.Name = "monochromeBar";
            monochromeBar.Size = new Size(86, 30);
            monochromeBar.TabIndex = 2;
            monochromeBar.Tag = "モノクローム(範囲：0～255)";
            monochromeBar.TickStyle = TickStyle.None;
            monochromeBar.Value = 128;
            monochromeBar.Scroll += monochromeBar_Scroll;
            monochromeBar.MouseEnter += Control_MouseEnter;
            monochromeBar.MouseLeave += Control_MouseLeave;
            // 
            // sharpPanel
            // 
            sharpPanel.Controls.Add(textBox2);
            sharpPanel.Controls.Add(sharpGroup);
            sharpPanel.Location = new Point(111, 211);
            sharpPanel.Name = "sharpPanel";
            sharpPanel.Size = new Size(200, 180);
            sharpPanel.TabIndex = 3;
            sharpPanel.Visible = false;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.BackColor = SystemColors.Control;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(16, 112);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(160, 63);
            textBox2.TabIndex = 5;
            textBox2.Text = "「ぼかし」を選択すると、「シャープ」設定は初期化されます。(「シャープ」と「ぼかし」は同時設定不可)";
            // 
            // sharpGroup
            // 
            sharpGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sharpGroup.Controls.Add(sharpTxt);
            sharpGroup.Controls.Add(sharpBar);
            sharpGroup.Location = new Point(16, 14);
            sharpGroup.Name = "sharpGroup";
            sharpGroup.Size = new Size(170, 92);
            sharpGroup.TabIndex = 2;
            sharpGroup.TabStop = false;
            sharpGroup.Text = "シャープ";
            // 
            // sharpTxt
            // 
            sharpTxt.Location = new Point(27, 34);
            sharpTxt.Name = "sharpTxt";
            sharpTxt.Size = new Size(53, 29);
            sharpTxt.TabIndex = 4;
            sharpTxt.Tag = "シャープ(範囲：0～99)";
            sharpTxt.TextAlign = HorizontalAlignment.Right;
            sharpTxt.Click += allSelect_Click;
            sharpTxt.Enter += allSelect_Enter;
            sharpTxt.KeyDown += NumericTextBox_KeyDown;
            sharpTxt.MouseEnter += Control_MouseEnter;
            sharpTxt.MouseLeave += Control_MouseLeave;
            // 
            // sharpBar
            // 
            sharpBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sharpBar.AutoSize = false;
            sharpBar.Location = new Point(86, 34);
            sharpBar.Maximum = 99;
            sharpBar.Name = "sharpBar";
            sharpBar.Size = new Size(78, 30);
            sharpBar.TabIndex = 5;
            sharpBar.Tag = "シャープ(範囲：0～99)";
            sharpBar.TickStyle = TickStyle.None;
            sharpBar.Scroll += sharpBar_Scroll;
            sharpBar.MouseEnter += Control_MouseEnter;
            sharpBar.MouseLeave += Control_MouseLeave;
            // 
            // blurPanel
            // 
            blurPanel.Controls.Add(textBox1);
            blurPanel.Controls.Add(blurGroup);
            blurPanel.Location = new Point(6, 212);
            blurPanel.Name = "blurPanel";
            blurPanel.Size = new Size(200, 180);
            blurPanel.TabIndex = 1;
            blurPanel.Visible = false;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.Control;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(18, 114);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(160, 63);
            textBox1.TabIndex = 4;
            textBox1.Text = "「シャープ」を選択すると、「ぼかし」設定は初期化されます。(「ぼかし」と「シャープ」は同時設定不可)";
            // 
            // blurGroup
            // 
            blurGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            blurGroup.Controls.Add(blurTxt);
            blurGroup.Controls.Add(blurBar);
            blurGroup.Location = new Point(16, 14);
            blurGroup.Name = "blurGroup";
            blurGroup.Size = new Size(170, 92);
            blurGroup.TabIndex = 3;
            blurGroup.TabStop = false;
            blurGroup.Text = "ぼかし";
            // 
            // blurTxt
            // 
            blurTxt.Location = new Point(19, 35);
            blurTxt.Name = "blurTxt";
            blurTxt.Size = new Size(53, 29);
            blurTxt.TabIndex = 1;
            blurTxt.Tag = "ぼかし(範囲：0～99)";
            blurTxt.TextAlign = HorizontalAlignment.Right;
            blurTxt.Click += allSelect_Click;
            blurTxt.Enter += allSelect_Enter;
            blurTxt.KeyDown += NumericTextBox_KeyDown;
            blurTxt.MouseEnter += Control_MouseEnter;
            blurTxt.MouseLeave += Control_MouseLeave;
            // 
            // blurBar
            // 
            blurBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            blurBar.AutoSize = false;
            blurBar.Location = new Point(78, 35);
            blurBar.Maximum = 99;
            blurBar.Name = "blurBar";
            blurBar.Size = new Size(86, 30);
            blurBar.TabIndex = 2;
            blurBar.Tag = "ぼかし(範囲：0～99)";
            blurBar.TickStyle = TickStyle.None;
            blurBar.Scroll += blurBar_Scroll;
            blurBar.MouseEnter += Control_MouseEnter;
            blurBar.MouseLeave += Control_MouseLeave;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.BackColor = SystemColors.Control;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(18, 20);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(771, 22);
            textBox3.TabIndex = 5;
            textBox3.Text = "設定項目はありません。";
            textBox3.WordWrap = false;
            // 
            // panel5
            // 
            panel5.Controls.Add(colorFilterResetBtn);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(3, 425);
            panel5.Name = "panel5";
            panel5.Size = new Size(1046, 45);
            panel5.TabIndex = 5;
            // 
            // colorFilterResetBtn
            // 
            colorFilterResetBtn.Location = new Point(13, 8);
            colorFilterResetBtn.Name = "colorFilterResetBtn";
            colorFilterResetBtn.Size = new Size(154, 30);
            colorFilterResetBtn.TabIndex = 4;
            colorFilterResetBtn.Tag = "画像効果を初期値に戻します。";
            colorFilterResetBtn.Text = "画像効果リセット";
            colorFilterResetBtn.UseVisualStyleBackColor = true;
            colorFilterResetBtn.Click += colorFilterResetBtn_Click;
            colorFilterResetBtn.MouseEnter += Control_MouseEnter;
            colorFilterResetBtn.MouseLeave += Control_MouseLeave;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Controls.Add(pictureBox2, 1, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 0);
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 1);
            tableLayoutPanel1.Location = new Point(24, 14);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(774, 92);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = SystemColors.WindowFrame;
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(390, 23);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(381, 66);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.WindowFrame;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(3, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(381, 66);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.Cursor = Cursors.SizeNS;
            splitContainer1.Location = new Point(0, 1);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            splitContainer1.Panel1.Cursor = Cursors.Arrow;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tabControl1);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(1121, 662);
            splitContainer1.SplitterDistance = 120;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 0;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1251, 750);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form6";
            StartPosition = FormStartPosition.CenterParent;
            Text = "色彩補正・画像効果";
            FormClosed += Form6_FormClosed;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BrightnessBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_B).EndInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_G).EndInit();
            ((System.ComponentModel.ISupportInitialize)ColorBalanceBar_R).EndInit();
            ((System.ComponentModel.ISupportInitialize)contrastBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)GammaCorrectionBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SaturationBar).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            mosaicPanel.ResumeLayout(false);
            mosaicGroup.ResumeLayout(false);
            mosaicGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mosaicBar).EndInit();
            ContourExPanel.ResumeLayout(false);
            ContourExGroup.ResumeLayout(false);
            ContourExGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ContourExBar).EndInit();
            noisePanel.ResumeLayout(false);
            noiseGroup.ResumeLayout(false);
            noiseGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)noiseBar).EndInit();
            embossPanel.ResumeLayout(false);
            embossGroup.ResumeLayout(false);
            embossGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)embossBar).EndInit();
            monochromePanel.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)monochromeBar).EndInit();
            sharpPanel.ResumeLayout(false);
            sharpPanel.PerformLayout();
            sharpGroup.ResumeLayout(false);
            sharpGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sharpBar).EndInit();
            blurPanel.ResumeLayout(false);
            blurPanel.PerformLayout();
            blurGroup.ResumeLayout(false);
            blurGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)blurBar).EndInit();
            panel5.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel1;
        private Button CancelBtn;
        private Button OkBtn;
        private TrackBar BrightnessBar;
        private TextBox BrightnessTxt;
        private Label label2;
        private Label label1;
        private TextBox ColorBalanceTxt_G;
        private TrackBar ColorBalanceBar_G;
        private TextBox ColorBalanceTxt_R;
        private TrackBar ColorBalanceBar_R;
        private Label label3;
        private TextBox ColorBalanceTxt_B;
        private TrackBar ColorBalanceBar_B;
        private TextBox contrastTxt;
        private TrackBar contrastBar;
        private TextBox GammaCorrectionTxt;
        private TrackBar GammaCorrectionBar;
        private TextBox SaturationTxt;
        private TrackBar SaturationBar;
        private Label label4;
        private Label label5;
        private Button ResetBtn;
        private ToolTip toolTip1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label6;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox monochromeTxt;
        private TrackBar monochromeBar;
        private Button colorCorrectionResetBtn;
        private Button colorFilterResetBtn;
        private TextBox blurTxt;
        private TrackBar blurBar;
        private GroupBox groupBox2;
        private TextBox sharpTxt;
        private TrackBar sharpBar;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private SplitContainer splitContainer1;
        private TextBox embossTxt;
        private TrackBar embossBar;
        private Panel panel2;
        private Panel panel3;
        private Panel embossPanel;
        private Panel panel5;
        private CheckBox embossCheck;
        private TreeView treeView1;
        private SplitContainer splitContainer2;
        private Panel blurPanel;
        private GroupBox blurGroup;
        private GroupBox sharpGroup;
        private Panel sharpPanel;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Panel monochromePanel;
        private GroupBox embossGroup;
        private Panel noisePanel;
        private GroupBox noiseGroup;
        private TextBox noiseTxt;
        private TrackBar noiseBar;
        private Panel ContourExPanel;
        private GroupBox ContourExGroup;
        private TextBox ContourExTxt;
        private TrackBar ContourExBar;
        private Panel mosaicPanel;
        private GroupBox mosaicGroup;
        private TextBox mosaicTxt;
        private TrackBar mosaicBar;
    }
}