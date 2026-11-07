namespace MyView
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            pictureBox1 = new PictureBox();
            conTxtMenu1 = new ContextMenuStrip(components);
            saveBtn2 = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            printBtn2 = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            copyBtn2 = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            pasteBtn2 = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            imageEnhancementBtn2 = new ToolStripMenuItem();
            toolStripSeparator19 = new ToolStripSeparator();
            zoomInBtn2 = new ToolStripMenuItem();
            zoomOutBtn2 = new ToolStripMenuItem();
            toolStripSeparator13 = new ToolStripSeparator();
            prevBtn2 = new ToolStripMenuItem();
            nextBtn2 = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            roolLeftBtn2 = new ToolStripMenuItem();
            roolRightBtn2 = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            flip_vBtn2 = new ToolStripMenuItem();
            flip_hBtn2 = new ToolStripMenuItem();
            toolStripSeparator21 = new ToolStripSeparator();
            closeBtn2 = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            viewStatusTxt = new ToolStripStatusLabel();
            toolStrip1 = new ClickThroughToolStrip();
            saveBtn = new ToolStripButton();
            toolStripSeparator11 = new ToolStripSeparator();
            printBtn = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            copyBtn = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            pasteBtn = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            imageEnhancementBtn = new ToolStripButton();
            toolStripSeparator18 = new ToolStripSeparator();
            zoomInBtn = new ToolStripButton();
            zoomOutBtn = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            prevBtn = new ToolStripButton();
            nextBtn = new ToolStripButton();
            toolStripSeparator22 = new ToolStripSeparator();
            roolLeftBtn = new ToolStripButton();
            roolRightBtn = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            flip_vBtn = new ToolStripButton();
            flip_hBtn = new ToolStripButton();
            toolStripSeparator16 = new ToolStripSeparator();
            closeBtn = new ToolStripButton();
            toolStripSeparator17 = new ToolStripSeparator();
            optionBtn = new ToolStripDropDownButton();
            iconLabelBtn = new ToolStripMenuItem();
            toolStripContainer1 = new ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            conTxtMenu1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.WindowFrame;
            pictureBox1.ContextMenuStrip = conTxtMenu1;
            pictureBox1.Location = new Point(290, 70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 91);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.Resize += PictureBox1_Resize;
            // 
            // conTxtMenu1
            // 
            conTxtMenu1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            conTxtMenu1.Items.AddRange(new ToolStripItem[] { saveBtn2, toolStripSeparator12, printBtn2, toolStripSeparator6, copyBtn2, toolStripSeparator7, pasteBtn2, toolStripSeparator8, imageEnhancementBtn2, toolStripSeparator19, zoomInBtn2, zoomOutBtn2, toolStripSeparator13, prevBtn2, nextBtn2, toolStripSeparator9, roolLeftBtn2, roolRightBtn2, toolStripSeparator10, flip_vBtn2, flip_hBtn2, toolStripSeparator21, closeBtn2 });
            conTxtMenu1.Name = "conTxtMenu1";
            conTxtMenu1.Size = new Size(223, 612);
            conTxtMenu1.Opening += conTxtMenu1_Opening;
            // 
            // saveBtn2
            // 
            saveBtn2.Image = (Image)resources.GetObject("saveBtn2.Image");
            saveBtn2.ImageScaling = ToolStripItemImageScaling.None;
            saveBtn2.Name = "saveBtn2";
            saveBtn2.Size = new Size(222, 38);
            saveBtn2.Text = "名前を付けて保存...";
            saveBtn2.ToolTipText = "名前を付けて保存(Ctrl+S)";
            saveBtn2.Click += saveBtn_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(219, 6);
            // 
            // printBtn2
            // 
            printBtn2.Image = (Image)resources.GetObject("printBtn2.Image");
            printBtn2.ImageScaling = ToolStripItemImageScaling.None;
            printBtn2.Name = "printBtn2";
            printBtn2.Size = new Size(222, 38);
            printBtn2.Text = "印刷...";
            printBtn2.ToolTipText = "印刷(Ctrl+P)";
            printBtn2.Click += printBtn_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(219, 6);
            // 
            // copyBtn2
            // 
            copyBtn2.Image = (Image)resources.GetObject("copyBtn2.Image");
            copyBtn2.ImageScaling = ToolStripItemImageScaling.None;
            copyBtn2.Name = "copyBtn2";
            copyBtn2.Size = new Size(222, 38);
            copyBtn2.Text = "コピー";
            copyBtn2.ToolTipText = "コピー(Ctrl+C)";
            copyBtn2.Click += copyBtn_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(219, 6);
            // 
            // pasteBtn2
            // 
            pasteBtn2.Image = (Image)resources.GetObject("pasteBtn2.Image");
            pasteBtn2.ImageScaling = ToolStripItemImageScaling.None;
            pasteBtn2.Name = "pasteBtn2";
            pasteBtn2.Size = new Size(222, 38);
            pasteBtn2.Text = "貼り付け";
            pasteBtn2.ToolTipText = "貼り付け(Ctrl+V)";
            pasteBtn2.Click += pasteBtn_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(219, 6);
            // 
            // imageEnhancementBtn2
            // 
            imageEnhancementBtn2.Image = (Image)resources.GetObject("imageEnhancementBtn2.Image");
            imageEnhancementBtn2.ImageScaling = ToolStripItemImageScaling.None;
            imageEnhancementBtn2.Name = "imageEnhancementBtn2";
            imageEnhancementBtn2.Size = new Size(222, 38);
            imageEnhancementBtn2.Text = "画像補正...";
            imageEnhancementBtn2.ToolTipText = "画像補正";
            imageEnhancementBtn2.Click += imageEnhancementBtn_Click;
            // 
            // toolStripSeparator19
            // 
            toolStripSeparator19.Name = "toolStripSeparator19";
            toolStripSeparator19.Size = new Size(219, 6);
            // 
            // zoomInBtn2
            // 
            zoomInBtn2.Image = (Image)resources.GetObject("zoomInBtn2.Image");
            zoomInBtn2.ImageScaling = ToolStripItemImageScaling.None;
            zoomInBtn2.Name = "zoomInBtn2";
            zoomInBtn2.Size = new Size(222, 38);
            zoomInBtn2.Text = "拡大";
            zoomInBtn2.ToolTipText = "拡大(Ctrl+ホイール上スクロール)";
            zoomInBtn2.Click += zoomInBtn_Click;
            // 
            // zoomOutBtn2
            // 
            zoomOutBtn2.Image = (Image)resources.GetObject("zoomOutBtn2.Image");
            zoomOutBtn2.ImageScaling = ToolStripItemImageScaling.None;
            zoomOutBtn2.Name = "zoomOutBtn2";
            zoomOutBtn2.Size = new Size(222, 38);
            zoomOutBtn2.Text = "縮小";
            zoomOutBtn2.ToolTipText = "縮小(Ctrl+ホイール下スクロール)";
            zoomOutBtn2.Click += zoomOutBtn_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new Size(219, 6);
            // 
            // prevBtn2
            // 
            prevBtn2.Image = (Image)resources.GetObject("prevBtn2.Image");
            prevBtn2.ImageScaling = ToolStripItemImageScaling.None;
            prevBtn2.Name = "prevBtn2";
            prevBtn2.Size = new Size(222, 38);
            prevBtn2.Text = "前画像";
            prevBtn2.ToolTipText = "前画像(ホイール上スクロール)";
            prevBtn2.Click += prevBtn_Click;
            // 
            // nextBtn2
            // 
            nextBtn2.Image = (Image)resources.GetObject("nextBtn2.Image");
            nextBtn2.ImageScaling = ToolStripItemImageScaling.None;
            nextBtn2.Name = "nextBtn2";
            nextBtn2.Size = new Size(222, 38);
            nextBtn2.Text = "次画像";
            nextBtn2.ToolTipText = "次画像(ホイール下スクロール)";
            nextBtn2.Click += nextBtn_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(219, 6);
            // 
            // roolLeftBtn2
            // 
            roolLeftBtn2.Image = (Image)resources.GetObject("roolLeftBtn2.Image");
            roolLeftBtn2.ImageScaling = ToolStripItemImageScaling.None;
            roolLeftBtn2.Name = "roolLeftBtn2";
            roolLeftBtn2.Size = new Size(222, 38);
            roolLeftBtn2.Text = "左へ回転";
            roolLeftBtn2.ToolTipText = "左へ90°回転(L)";
            roolLeftBtn2.Click += roolLeftBtn_Click;
            // 
            // roolRightBtn2
            // 
            roolRightBtn2.Image = (Image)resources.GetObject("roolRightBtn2.Image");
            roolRightBtn2.ImageScaling = ToolStripItemImageScaling.None;
            roolRightBtn2.Name = "roolRightBtn2";
            roolRightBtn2.Size = new Size(222, 38);
            roolRightBtn2.Text = "右へ回転";
            roolRightBtn2.ToolTipText = "右へ90°回転(R)";
            roolRightBtn2.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(219, 6);
            // 
            // flip_vBtn2
            // 
            flip_vBtn2.Image = (Image)resources.GetObject("flip_vBtn2.Image");
            flip_vBtn2.ImageScaling = ToolStripItemImageScaling.None;
            flip_vBtn2.Name = "flip_vBtn2";
            flip_vBtn2.Size = new Size(222, 38);
            flip_vBtn2.Text = "上下反転";
            flip_vBtn2.ToolTipText = "上下反転(V)";
            flip_vBtn2.Click += flip_vBtn_Click;
            // 
            // flip_hBtn2
            // 
            flip_hBtn2.Image = (Image)resources.GetObject("flip_hBtn2.Image");
            flip_hBtn2.ImageScaling = ToolStripItemImageScaling.None;
            flip_hBtn2.Name = "flip_hBtn2";
            flip_hBtn2.Size = new Size(222, 38);
            flip_hBtn2.Text = "左右反転";
            flip_hBtn2.ToolTipText = "左右反転(H)";
            flip_hBtn2.Click += flip_hBtn_Click;
            // 
            // toolStripSeparator21
            // 
            toolStripSeparator21.Name = "toolStripSeparator21";
            toolStripSeparator21.Size = new Size(219, 6);
            // 
            // closeBtn2
            // 
            closeBtn2.Image = (Image)resources.GetObject("closeBtn2.Image");
            closeBtn2.ImageScaling = ToolStripItemImageScaling.None;
            closeBtn2.Name = "closeBtn2";
            closeBtn2.Size = new Size(222, 38);
            closeBtn2.Text = "閉じる";
            closeBtn2.ToolTipText = "閉じる(Esc)";
            closeBtn2.Click += closeBtn_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { viewStatusTxt });
            statusStrip1.Location = new Point(0, 470);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(830, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // viewStatusTxt
            // 
            viewStatusTxt.Name = "viewStatusTxt";
            viewStatusTxt.Size = new Size(815, 21);
            viewStatusTxt.Spring = true;
            viewStatusTxt.Text = "ここにフルパス";
            viewStatusTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            toolStrip1.Items.AddRange(new ToolStripItem[] { saveBtn, toolStripSeparator11, printBtn, toolStripSeparator1, copyBtn, toolStripSeparator2, pasteBtn, toolStripSeparator3, imageEnhancementBtn, toolStripSeparator18, zoomInBtn, zoomOutBtn, toolStripSeparator4, prevBtn, nextBtn, toolStripSeparator22, roolLeftBtn, roolRightBtn, toolStripSeparator5, flip_vBtn, flip_hBtn, toolStripSeparator16, closeBtn, toolStripSeparator17, optionBtn });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(621, 39);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // saveBtn
            // 
            saveBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.ImageScaling = ToolStripItemImageScaling.None;
            saveBtn.ImageTransparentColor = Color.Magenta;
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(36, 36);
            saveBtn.Tag = "画像に名前を付けて保存します。";
            saveBtn.Text = "保存(&S)...";
            saveBtn.ToolTipText = "名前を付けて保存(Ctrl+S)";
            saveBtn.Click += saveBtn_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new Size(6, 39);
            // 
            // printBtn
            // 
            printBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printBtn.Image = (Image)resources.GetObject("printBtn.Image");
            printBtn.ImageScaling = ToolStripItemImageScaling.None;
            printBtn.ImageTransparentColor = Color.Magenta;
            printBtn.Name = "printBtn";
            printBtn.Size = new Size(36, 36);
            printBtn.Tag = "画像に印刷します。";
            printBtn.Text = "印刷(&P)...";
            printBtn.ToolTipText = "印刷プレビュー(Ctrl+P)";
            printBtn.Click += printBtn_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 39);
            // 
            // copyBtn
            // 
            copyBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyBtn.Image = (Image)resources.GetObject("copyBtn.Image");
            copyBtn.ImageScaling = ToolStripItemImageScaling.None;
            copyBtn.ImageTransparentColor = Color.Magenta;
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(36, 36);
            copyBtn.Tag = "画像をコピーします。";
            copyBtn.Text = "コピー(&C)";
            copyBtn.ToolTipText = "コピー(Ctrl+C)";
            copyBtn.Click += copyBtn_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 39);
            // 
            // pasteBtn
            // 
            pasteBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            pasteBtn.Image = (Image)resources.GetObject("pasteBtn.Image");
            pasteBtn.ImageScaling = ToolStripItemImageScaling.None;
            pasteBtn.ImageTransparentColor = Color.Magenta;
            pasteBtn.Name = "pasteBtn";
            pasteBtn.Size = new Size(36, 36);
            pasteBtn.Tag = "画像を貼り付けます。";
            pasteBtn.Text = "貼り付け(&V)";
            pasteBtn.ToolTipText = "貼り付け(Ctrl+V)";
            pasteBtn.Click += pasteBtn_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 39);
            // 
            // imageEnhancementBtn
            // 
            imageEnhancementBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            imageEnhancementBtn.Image = (Image)resources.GetObject("imageEnhancementBtn.Image");
            imageEnhancementBtn.ImageScaling = ToolStripItemImageScaling.None;
            imageEnhancementBtn.ImageTransparentColor = Color.Magenta;
            imageEnhancementBtn.Name = "imageEnhancementBtn";
            imageEnhancementBtn.Size = new Size(36, 36);
            imageEnhancementBtn.Tag = "色彩補正・色調フィルター・画像効果を設定します。";
            imageEnhancementBtn.Text = "画像補正";
            imageEnhancementBtn.ToolTipText = "画像補正(Crtl+I)";
            imageEnhancementBtn.Click += imageEnhancementBtn_Click;
            // 
            // toolStripSeparator18
            // 
            toolStripSeparator18.Name = "toolStripSeparator18";
            toolStripSeparator18.Size = new Size(6, 39);
            // 
            // zoomInBtn
            // 
            zoomInBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            zoomInBtn.Image = (Image)resources.GetObject("zoomInBtn.Image");
            zoomInBtn.ImageScaling = ToolStripItemImageScaling.None;
            zoomInBtn.ImageTransparentColor = Color.Magenta;
            zoomInBtn.Name = "zoomInBtn";
            zoomInBtn.Size = new Size(36, 36);
            zoomInBtn.Tag = "画像を拡大します。";
            zoomInBtn.Text = "拡大";
            zoomInBtn.ToolTipText = "拡大(Ctrl+ホイール上スクロール)";
            zoomInBtn.Click += zoomInBtn_Click;
            // 
            // zoomOutBtn
            // 
            zoomOutBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            zoomOutBtn.Image = (Image)resources.GetObject("zoomOutBtn.Image");
            zoomOutBtn.ImageScaling = ToolStripItemImageScaling.None;
            zoomOutBtn.ImageTransparentColor = Color.Magenta;
            zoomOutBtn.Name = "zoomOutBtn";
            zoomOutBtn.Size = new Size(36, 36);
            zoomOutBtn.Tag = "画像を縮小します。";
            zoomOutBtn.Text = "縮小";
            zoomOutBtn.ToolTipText = "縮小(Ctrl+ホイール下スクロール)";
            zoomOutBtn.Click += zoomOutBtn_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 39);
            // 
            // prevBtn
            // 
            prevBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            prevBtn.Image = (Image)resources.GetObject("prevBtn.Image");
            prevBtn.ImageScaling = ToolStripItemImageScaling.None;
            prevBtn.ImageTransparentColor = Color.Magenta;
            prevBtn.Name = "prevBtn";
            prevBtn.Size = new Size(36, 36);
            prevBtn.Tag = "前の画像を表示します。";
            prevBtn.Text = "前画像";
            prevBtn.ToolTipText = "前画像(ホイール上スクロール)";
            prevBtn.Click += prevBtn_Click;
            // 
            // nextBtn
            // 
            nextBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nextBtn.Image = (Image)resources.GetObject("nextBtn.Image");
            nextBtn.ImageScaling = ToolStripItemImageScaling.None;
            nextBtn.ImageTransparentColor = Color.Magenta;
            nextBtn.Name = "nextBtn";
            nextBtn.Size = new Size(36, 36);
            nextBtn.Tag = "次の画像を表示します。";
            nextBtn.Text = "次画像";
            nextBtn.ToolTipText = "次画像(ホイール下スクロール)";
            nextBtn.Click += nextBtn_Click;
            // 
            // toolStripSeparator22
            // 
            toolStripSeparator22.Name = "toolStripSeparator22";
            toolStripSeparator22.Size = new Size(6, 39);
            // 
            // roolLeftBtn
            // 
            roolLeftBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            roolLeftBtn.Image = (Image)resources.GetObject("roolLeftBtn.Image");
            roolLeftBtn.ImageScaling = ToolStripItemImageScaling.None;
            roolLeftBtn.ImageTransparentColor = Color.Magenta;
            roolLeftBtn.Name = "roolLeftBtn";
            roolLeftBtn.Size = new Size(36, 36);
            roolLeftBtn.Tag = "画像を左へ90°回転します。";
            roolLeftBtn.Text = "左へ回転(&L)";
            roolLeftBtn.ToolTipText = "左へ90°回転(L)";
            roolLeftBtn.Click += roolLeftBtn_Click;
            // 
            // roolRightBtn
            // 
            roolRightBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            roolRightBtn.Image = (Image)resources.GetObject("roolRightBtn.Image");
            roolRightBtn.ImageScaling = ToolStripItemImageScaling.None;
            roolRightBtn.ImageTransparentColor = Color.Magenta;
            roolRightBtn.Name = "roolRightBtn";
            roolRightBtn.Size = new Size(36, 36);
            roolRightBtn.Tag = "画像を右へ90°回転します。";
            roolRightBtn.Text = "右へ回転(&R)";
            roolRightBtn.ToolTipText = "右へ90°回転(R)";
            roolRightBtn.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 39);
            // 
            // flip_vBtn
            // 
            flip_vBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            flip_vBtn.Image = (Image)resources.GetObject("flip_vBtn.Image");
            flip_vBtn.ImageScaling = ToolStripItemImageScaling.None;
            flip_vBtn.ImageTransparentColor = Color.Magenta;
            flip_vBtn.Name = "flip_vBtn";
            flip_vBtn.Size = new Size(36, 36);
            flip_vBtn.Text = "上下反転(&V)";
            flip_vBtn.ToolTipText = "上下反転(V)";
            flip_vBtn.Click += flip_vBtn_Click;
            // 
            // flip_hBtn
            // 
            flip_hBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            flip_hBtn.Image = (Image)resources.GetObject("flip_hBtn.Image");
            flip_hBtn.ImageScaling = ToolStripItemImageScaling.None;
            flip_hBtn.ImageTransparentColor = Color.Magenta;
            flip_hBtn.Name = "flip_hBtn";
            flip_hBtn.Size = new Size(36, 36);
            flip_hBtn.Text = "左右反転(&H)";
            flip_hBtn.ToolTipText = "左右反転(H)";
            flip_hBtn.Click += flip_hBtn_Click;
            // 
            // toolStripSeparator16
            // 
            toolStripSeparator16.Name = "toolStripSeparator16";
            toolStripSeparator16.Size = new Size(6, 39);
            // 
            // closeBtn
            // 
            closeBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            closeBtn.Image = (Image)resources.GetObject("closeBtn.Image");
            closeBtn.ImageScaling = ToolStripItemImageScaling.None;
            closeBtn.ImageTransparentColor = Color.Magenta;
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(36, 36);
            closeBtn.Tag = "ウィンドウを閉じます。";
            closeBtn.Text = "閉じる(&X)";
            closeBtn.ToolTipText = "閉じる(Esc)";
            closeBtn.Click += closeBtn_Click;
            // 
            // toolStripSeparator17
            // 
            toolStripSeparator17.Name = "toolStripSeparator17";
            toolStripSeparator17.Size = new Size(6, 39);
            // 
            // optionBtn
            // 
            optionBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            optionBtn.DropDownItems.AddRange(new ToolStripItem[] { iconLabelBtn });
            optionBtn.Image = (Image)resources.GetObject("optionBtn.Image");
            optionBtn.ImageScaling = ToolStripItemImageScaling.None;
            optionBtn.ImageTransparentColor = Color.Magenta;
            optionBtn.Name = "optionBtn";
            optionBtn.Size = new Size(45, 36);
            optionBtn.Tag = "オプション";
            optionBtn.Text = "オプション";
            optionBtn.ToolTipText = "オプション";
            // 
            // iconLabelBtn
            // 
            iconLabelBtn.Name = "iconLabelBtn";
            iconLabelBtn.Size = new Size(232, 26);
            iconLabelBtn.Tag = "アイコンラベルの表示・非表示を切り替えます。";
            iconLabelBtn.Text = "アイコンラベルを表示する";
            iconLabelBtn.ToolTipText = "アイコンラベルの表示・非表示を切り替えます。";
            iconLabelBtn.Click += iconLabelBtn_Click;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(pictureBox1);
            toolStripContainer1.ContentPanel.Size = new Size(795, 309);
            toolStripContainer1.Location = new Point(9, 64);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(795, 348);
            toolStripContainer1.TabIndex = 4;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(830, 496);
            Controls.Add(toolStripContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterParent;
            Text = "画像ファイル名表示";
            Load += Form3_Load;
            KeyDown += Form3_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            conTxtMenu1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel viewStatusTxt;
        //private ToolStrip toolStrip1;
        private ClickThroughToolStrip toolStrip1;
        private ToolStripButton saveBtn;
        private ToolStripButton copyBtn;
        private ToolStripButton pasteBtn;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripContainer toolStripContainer1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton roolLeftBtn;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton roolRightBtn;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton closeBtn;
        private ToolStripButton zoomInBtn;
        private ToolStripButton zoomOutBtn;
        private ContextMenuStrip conTxtMenu1;
        private ToolStripMenuItem saveBtn2;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem copyBtn2;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem pasteBtn2;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem zoomInBtn2;
        private ToolStripMenuItem zoomOutBtn2;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem roolLeftBtn2;
        private ToolStripMenuItem roolRightBtn2;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem closeBtn2;
        private ToolStripButton printBtn;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem printBtn2;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripButton flip_vBtn;
        private ToolStripButton flip_hBtn;
        private ToolStripSeparator toolStripSeparator16;
        private ToolStripDropDownButton optionBtn;
        private ToolStripMenuItem iconLabelBtn;
        private ToolStripSeparator toolStripSeparator17;
        private ToolStripSeparator toolStripSeparator18;
        private ToolStripSeparator toolStripSeparator19;
        private ToolStripMenuItem flip_vBtn2;
        private ToolStripMenuItem flip_hBtn2;
        private ToolStripMenuItem imageEnhancementBtn2;
        private ToolStripButton imageEnhancementBtn;
        private ToolStripSeparator toolStripSeparator21;
        private ToolStripButton prevBtn;
        private ToolStripButton nextBtn;
        private ToolStripSeparator toolStripSeparator22;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem prevBtn2;
        private ToolStripMenuItem nextBtn2;
    }
}