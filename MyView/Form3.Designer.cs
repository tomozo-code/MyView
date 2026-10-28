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
            zoomInBtn2 = new ToolStripMenuItem();
            zoomOutBtn2 = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            roolLeftBtn2 = new ToolStripMenuItem();
            roolRightBtn2 = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
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
            zoomInBtn = new ToolStripButton();
            zoomOutBtn = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            roolLeftBtn = new ToolStripButton();
            roolRightBtn = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            flip_vBtn = new ToolStripButton();
            flip_hBtn = new ToolStripButton();
            toolStripSeparator16 = new ToolStripSeparator();
            closeBtn = new ToolStripButton();
            panel1 = new Panel();
            toolStripContainer1 = new ToolStripContainer();
            menuStrip1 = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            saveasMenu = new ToolStripMenuItem();
            printMenu = new ToolStripMenuItem();
            toolStripSeparator13 = new ToolStripSeparator();
            closeMenu = new ToolStripMenuItem();
            editMenu = new ToolStripMenuItem();
            copyMenu = new ToolStripMenuItem();
            pasteMenu = new ToolStripMenuItem();
            toolStripSeparator14 = new ToolStripSeparator();
            roolLeftMenu = new ToolStripMenuItem();
            roolRightMenu = new ToolStripMenuItem();
            toolStripSeparator15 = new ToolStripSeparator();
            verticalMenu = new ToolStripMenuItem();
            horizontalMenu = new ToolStripMenuItem();
            viewMenu = new ToolStripMenuItem();
            zoomInMenu = new ToolStripMenuItem();
            zoomOutMenu = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            conTxtMenu1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.WindowFrame;
            pictureBox1.ContextMenuStrip = conTxtMenu1;
            pictureBox1.Location = new Point(14, 12);
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
            conTxtMenu1.Items.AddRange(new ToolStripItem[] { saveBtn2, toolStripSeparator12, printBtn2, toolStripSeparator6, copyBtn2, toolStripSeparator7, pasteBtn2, toolStripSeparator8, zoomInBtn2, zoomOutBtn2, toolStripSeparator9, roolLeftBtn2, roolRightBtn2, toolStripSeparator10, closeBtn2 });
            conTxtMenu1.Name = "conTxtMenu1";
            conTxtMenu1.Size = new Size(207, 274);
            conTxtMenu1.Opening += conTxtMenu1_Opening;
            // 
            // saveBtn2
            // 
            saveBtn2.Image = (Image)resources.GetObject("saveBtn2.Image");
            saveBtn2.Name = "saveBtn2";
            saveBtn2.Size = new Size(206, 26);
            saveBtn2.Text = "名前を付けて保存...";
            saveBtn2.ToolTipText = "名前を付けて保存(Ctrl+S)";
            saveBtn2.Click += saveBtn_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(203, 6);
            // 
            // printBtn2
            // 
            printBtn2.Image = (Image)resources.GetObject("printBtn2.Image");
            printBtn2.Name = "printBtn2";
            printBtn2.Size = new Size(206, 26);
            printBtn2.Text = "印刷...";
            printBtn2.ToolTipText = "印刷(Ctrl+P)";
            printBtn2.Click += printBtn_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(203, 6);
            // 
            // copyBtn2
            // 
            copyBtn2.Image = (Image)resources.GetObject("copyBtn2.Image");
            copyBtn2.Name = "copyBtn2";
            copyBtn2.Size = new Size(206, 26);
            copyBtn2.Text = "コピー";
            copyBtn2.ToolTipText = "コピー(Ctrl+C)";
            copyBtn2.Click += copyBtn_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(203, 6);
            // 
            // pasteBtn2
            // 
            pasteBtn2.Image = (Image)resources.GetObject("pasteBtn2.Image");
            pasteBtn2.Name = "pasteBtn2";
            pasteBtn2.Size = new Size(206, 26);
            pasteBtn2.Text = "貼り付け";
            pasteBtn2.ToolTipText = "貼り付け(Ctrl+V)";
            pasteBtn2.Click += pasteBtn_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(203, 6);
            // 
            // zoomInBtn2
            // 
            zoomInBtn2.Image = (Image)resources.GetObject("zoomInBtn2.Image");
            zoomInBtn2.Name = "zoomInBtn2";
            zoomInBtn2.Size = new Size(206, 26);
            zoomInBtn2.Text = "拡大";
            zoomInBtn2.ToolTipText = "拡大(Ctrl+上スクロール)";
            zoomInBtn2.Click += zoomInBtn_Click;
            // 
            // zoomOutBtn2
            // 
            zoomOutBtn2.Image = (Image)resources.GetObject("zoomOutBtn2.Image");
            zoomOutBtn2.Name = "zoomOutBtn2";
            zoomOutBtn2.Size = new Size(206, 26);
            zoomOutBtn2.Text = "縮小";
            zoomOutBtn2.ToolTipText = "縮小(Ctrl+下スクロール)";
            zoomOutBtn2.Click += zoomOutBtn_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(203, 6);
            // 
            // roolLeftBtn2
            // 
            roolLeftBtn2.Image = (Image)resources.GetObject("roolLeftBtn2.Image");
            roolLeftBtn2.Name = "roolLeftBtn2";
            roolLeftBtn2.Size = new Size(206, 26);
            roolLeftBtn2.Text = "左へ回転";
            roolLeftBtn2.ToolTipText = "左へ90°回転(L)";
            roolLeftBtn2.Click += roolLeftBtn_Click;
            // 
            // roolRightBtn2
            // 
            roolRightBtn2.Image = (Image)resources.GetObject("roolRightBtn2.Image");
            roolRightBtn2.Name = "roolRightBtn2";
            roolRightBtn2.Size = new Size(206, 26);
            roolRightBtn2.Text = "右へ回転";
            roolRightBtn2.ToolTipText = "右へ90°回転(R)";
            roolRightBtn2.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(203, 6);
            // 
            // closeBtn2
            // 
            closeBtn2.Image = (Image)resources.GetObject("closeBtn2.Image");
            closeBtn2.Name = "closeBtn2";
            closeBtn2.Size = new Size(206, 26);
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
            statusStrip1.Size = new Size(703, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // viewStatusTxt
            // 
            viewStatusTxt.Name = "viewStatusTxt";
            viewStatusTxt.Size = new Size(688, 21);
            viewStatusTxt.Spring = true;
            viewStatusTxt.Text = "ここにフルパス";
            viewStatusTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            toolStrip1.Items.AddRange(new ToolStripItem[] { saveBtn, toolStripSeparator11, printBtn, toolStripSeparator1, copyBtn, toolStripSeparator2, pasteBtn, toolStripSeparator3, zoomInBtn, zoomOutBtn, toolStripSeparator4, roolLeftBtn, roolRightBtn, toolStripSeparator5, flip_vBtn, flip_hBtn, toolStripSeparator16, closeBtn });
            toolStrip1.Location = new Point(3, 29);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(481, 39);
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
            saveBtn.Text = "名前を付けて保存(&S)...";
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
            printBtn.ToolTipText = "印刷プレビューを表示します。(Ctrl+P)";
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
            zoomInBtn.ToolTipText = "拡大(Ctrl+上スクロール)";
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
            zoomOutBtn.ToolTipText = "縮小(Ctrl+下スクロール)";
            zoomOutBtn.Click += zoomOutBtn_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 39);
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
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(63, 60);
            panel1.Name = "panel1";
            panel1.Size = new Size(159, 123);
            panel1.TabIndex = 3;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(panel1);
            toolStripContainer1.ContentPanel.Size = new Size(638, 255);
            toolStripContainer1.Location = new Point(9, 64);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(638, 348);
            toolStripContainer1.TabIndex = 4;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(menuStrip1);
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, viewMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(638, 29);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { saveasMenu, printMenu, toolStripSeparator13, closeMenu });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(85, 25);
            fileMenu.Text = "ファイル(&F)";
            // 
            // saveasMenu
            // 
            saveasMenu.Image = (Image)resources.GetObject("saveasMenu.Image");
            saveasMenu.ImageScaling = ToolStripItemImageScaling.None;
            saveasMenu.Name = "saveasMenu";
            saveasMenu.Size = new Size(241, 38);
            saveasMenu.Text = "名前を付けて保存(&S)...";
            saveasMenu.Click += saveBtn_Click;
            // 
            // printMenu
            // 
            printMenu.Image = (Image)resources.GetObject("printMenu.Image");
            printMenu.ImageScaling = ToolStripItemImageScaling.None;
            printMenu.Name = "printMenu";
            printMenu.Size = new Size(241, 38);
            printMenu.Text = "印刷(&P)...";
            printMenu.Click += printBtn_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new Size(238, 6);
            // 
            // closeMenu
            // 
            closeMenu.Image = (Image)resources.GetObject("closeMenu.Image");
            closeMenu.ImageScaling = ToolStripItemImageScaling.None;
            closeMenu.Name = "closeMenu";
            closeMenu.Size = new Size(241, 38);
            closeMenu.Text = "閉じる(&X)";
            closeMenu.Click += closeBtn_Click;
            // 
            // editMenu
            // 
            editMenu.DropDownItems.AddRange(new ToolStripItem[] { copyMenu, pasteMenu, toolStripSeparator14, roolLeftMenu, roolRightMenu, toolStripSeparator15, verticalMenu, horizontalMenu });
            editMenu.Name = "editMenu";
            editMenu.Size = new Size(72, 25);
            editMenu.Text = "編集(&E)";
            // 
            // copyMenu
            // 
            copyMenu.Image = (Image)resources.GetObject("copyMenu.Image");
            copyMenu.ImageScaling = ToolStripItemImageScaling.None;
            copyMenu.Name = "copyMenu";
            copyMenu.Size = new Size(201, 38);
            copyMenu.Text = "コピー(&C)";
            copyMenu.Click += copyBtn_Click;
            // 
            // pasteMenu
            // 
            pasteMenu.Image = (Image)resources.GetObject("pasteMenu.Image");
            pasteMenu.ImageScaling = ToolStripItemImageScaling.None;
            pasteMenu.Name = "pasteMenu";
            pasteMenu.Size = new Size(201, 38);
            pasteMenu.Text = "貼り付け(&V)";
            pasteMenu.Click += pasteBtn_Click;
            // 
            // toolStripSeparator14
            // 
            toolStripSeparator14.Name = "toolStripSeparator14";
            toolStripSeparator14.Size = new Size(198, 6);
            // 
            // roolLeftMenu
            // 
            roolLeftMenu.Image = (Image)resources.GetObject("roolLeftMenu.Image");
            roolLeftMenu.ImageScaling = ToolStripItemImageScaling.None;
            roolLeftMenu.Name = "roolLeftMenu";
            roolLeftMenu.Size = new Size(201, 38);
            roolLeftMenu.Text = "左へ90°回転(&L)";
            roolLeftMenu.Click += roolLeftBtn_Click;
            // 
            // roolRightMenu
            // 
            roolRightMenu.Image = (Image)resources.GetObject("roolRightMenu.Image");
            roolRightMenu.ImageScaling = ToolStripItemImageScaling.None;
            roolRightMenu.Name = "roolRightMenu";
            roolRightMenu.Size = new Size(201, 38);
            roolRightMenu.Text = "右へ90°回転(&R)";
            roolRightMenu.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator15
            // 
            toolStripSeparator15.Name = "toolStripSeparator15";
            toolStripSeparator15.Size = new Size(198, 6);
            // 
            // verticalMenu
            // 
            verticalMenu.Image = (Image)resources.GetObject("verticalMenu.Image");
            verticalMenu.ImageScaling = ToolStripItemImageScaling.None;
            verticalMenu.Name = "verticalMenu";
            verticalMenu.Size = new Size(201, 38);
            verticalMenu.Text = "上下反転(&V)";
            verticalMenu.Click += flip_vBtn_Click;
            // 
            // horizontalMenu
            // 
            horizontalMenu.Image = (Image)resources.GetObject("horizontalMenu.Image");
            horizontalMenu.ImageScaling = ToolStripItemImageScaling.None;
            horizontalMenu.Name = "horizontalMenu";
            horizontalMenu.Size = new Size(201, 38);
            horizontalMenu.Text = "左右反転(&H)";
            horizontalMenu.Click += flip_hBtn_Click;
            // 
            // viewMenu
            // 
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { zoomInMenu, zoomOutMenu });
            viewMenu.Name = "viewMenu";
            viewMenu.Size = new Size(74, 25);
            viewMenu.Text = "表示(&V)";
            // 
            // zoomInMenu
            // 
            zoomInMenu.Image = (Image)resources.GetObject("zoomInMenu.Image");
            zoomInMenu.ImageScaling = ToolStripItemImageScaling.None;
            zoomInMenu.Name = "zoomInMenu";
            zoomInMenu.Size = new Size(128, 38);
            zoomInMenu.Text = "拡大";
            zoomInMenu.Click += zoomInBtn_Click;
            // 
            // zoomOutMenu
            // 
            zoomOutMenu.Image = (Image)resources.GetObject("zoomOutMenu.Image");
            zoomOutMenu.ImageScaling = ToolStripItemImageScaling.None;
            zoomOutMenu.Name = "zoomOutMenu";
            zoomOutMenu.Size = new Size(128, 38);
            zoomOutMenu.Text = "縮小";
            zoomOutMenu.Click += zoomOutBtn_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(703, 496);
            Controls.Add(toolStripContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
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
            panel1.ResumeLayout(false);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private Panel panel1;
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
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem saveasMenu;
        private ToolStripMenuItem printMenu;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem copyMenu;
        private ToolStripMenuItem pasteMenu;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem closeMenu;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem roolLeftMenu;
        private ToolStripMenuItem roolRightMenu;
        private ToolStripMenuItem viewMenu;
        private ToolStripMenuItem zoomInMenu;
        private ToolStripMenuItem zoomOutMenu;
        private ToolStripSeparator toolStripSeparator15;
        private ToolStripMenuItem verticalMenu;
        private ToolStripMenuItem horizontalMenu;
        private ToolStripButton flip_vBtn;
        private ToolStripButton flip_hBtn;
        private ToolStripSeparator toolStripSeparator16;
    }
}