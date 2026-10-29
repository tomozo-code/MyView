namespace MyView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            treeViewFolders = new TreeView();
            logTxt = new TextBox();
            listViewThumbnails = new ListView();
            conMenu1 = new ContextMenuStrip(components);
            conTxtMenu1 = new ToolStripMenuItem();
            conTxtMenu2 = new ToolStripMenuItem();
            conTxtMenu3 = new ToolStripMenuItem();
            conTxtMenu4 = new ToolStripMenuItem();
            conTxtMenu5 = new ToolStripMenuItem();
            conTxtMenu6 = new ToolStripMenuItem();
            conTxtMenu7 = new ToolStripMenuItem();
            conTxtMenu8 = new ToolStripMenuItem();
            conTxtMenu9 = new ToolStripMenuItem();
            panel1 = new Panel();
            numThumbnailSize = new NumericUpDown();
            menuStrip1 = new ClickThroughMenuStrip();
            fileMenu = new ToolStripMenuItem();
            printMenu = new ToolStripMenuItem();
            exitMenu = new ToolStripMenuItem();
            viewMenu = new ToolStripMenuItem();
            folderUpdate = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            foldertreeMenu = new ToolStripMenuItem();
            infoboxMenu = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            LeftRightLayout = new ToolStripMenuItem();
            TopBottomLayout = new ToolStripMenuItem();
            LayoutReset = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            useMenu = new ToolStripMenuItem();
            verMenu = new ToolStripMenuItem();
            SettingFolderMenu = new ToolStripMenuItem();
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            StatusLabel1 = new ToolStripStatusLabel();
            pathTxt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            conMenu1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThumbnailSize).BeginInit();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Cursor = Cursors.SizeWE;
            splitContainer1.Location = new Point(12, 166);
            splitContainer1.MinimumSize = new Size(500, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listViewThumbnails);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(800, 440);
            splitContainer1.SplitterDistance = 329;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Cursor = Cursors.SizeNS;
            splitContainer2.Location = new Point(42, 44);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(treeViewFolders);
            splitContainer2.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(logTxt);
            splitContainer2.Panel2.Cursor = Cursors.Arrow;
            splitContainer2.Size = new Size(255, 371);
            splitContainer2.SplitterDistance = 277;
            splitContainer2.SplitterWidth = 8;
            splitContainer2.TabIndex = 1;
            // 
            // treeViewFolders
            // 
            treeViewFolders.BackColor = SystemColors.Control;
            treeViewFolders.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            treeViewFolders.HideSelection = false;
            treeViewFolders.Location = new Point(36, 23);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(136, 107);
            treeViewFolders.TabIndex = 0;
            treeViewFolders.BeforeExpand += TreeViewFolders_BeforeExpand;
            treeViewFolders.AfterSelect += TreeViewFolders_AfterSelect;
            // 
            // logTxt
            // 
            logTxt.Location = new Point(72, 18);
            logTxt.Multiline = true;
            logTxt.Name = "logTxt";
            logTxt.ScrollBars = ScrollBars.Both;
            logTxt.Size = new Size(100, 50);
            logTxt.TabIndex = 3;
            // 
            // listViewThumbnails
            // 
            listViewThumbnails.AllowDrop = true;
            listViewThumbnails.BackColor = SystemColors.Control;
            listViewThumbnails.ContextMenuStrip = conMenu1;
            listViewThumbnails.Location = new Point(72, 44);
            listViewThumbnails.Name = "listViewThumbnails";
            listViewThumbnails.Size = new Size(121, 97);
            listViewThumbnails.TabIndex = 3;
            listViewThumbnails.UseCompatibleStateImageBehavior = false;
            listViewThumbnails.DragDrop += listViewThumbnails_DragDrop;
            listViewThumbnails.DragEnter += listViewThumbnails_DragEnter;
            listViewThumbnails.DoubleClick += ListViewThumbnails_DoubleClick;
            listViewThumbnails.KeyDown += ListViewThumbnails_KeyDown;
            listViewThumbnails.MouseDown += ListViewThumbnails_MouseDown;
            listViewThumbnails.MouseMove += ListViewThumbnails_MouseMove;
            // 
            // conMenu1
            // 
            conMenu1.Items.AddRange(new ToolStripItem[] { conTxtMenu1, conTxtMenu2, conTxtMenu3, conTxtMenu4, conTxtMenu5, conTxtMenu6, conTxtMenu7, conTxtMenu8, conTxtMenu9 });
            conMenu1.Name = "conMenu1";
            conMenu1.Size = new Size(169, 220);
            conMenu1.Opening += ConMenu1_Opening;
            // 
            // conTxtMenu1
            // 
            conTxtMenu1.Name = "conTxtMenu1";
            conTxtMenu1.Size = new Size(168, 24);
            conTxtMenu1.Text = "フルパス(&1)";
            conTxtMenu1.Click += ConTxtMenu1_Click;
            // 
            // conTxtMenu2
            // 
            conTxtMenu2.Name = "conTxtMenu2";
            conTxtMenu2.Size = new Size(168, 24);
            conTxtMenu2.Text = "ファイル名(&2)";
            conTxtMenu2.Click += ConTxtMenu2_Click;
            // 
            // conTxtMenu3
            // 
            conTxtMenu3.Name = "conTxtMenu3";
            conTxtMenu3.Size = new Size(168, 24);
            conTxtMenu3.Text = "フォルダーパス(&3)";
            conTxtMenu3.Click += ConTxtMenu3_Click;
            // 
            // conTxtMenu4
            // 
            conTxtMenu4.Name = "conTxtMenu4";
            conTxtMenu4.Size = new Size(168, 24);
            conTxtMenu4.Text = "画像(&4)";
            conTxtMenu4.Click += ConTxtMenu4_Click;
            // 
            // conTxtMenu5
            // 
            conTxtMenu5.Name = "conTxtMenu5";
            conTxtMenu5.Size = new Size(168, 24);
            conTxtMenu5.Text = "拡張子(&5)";
            conTxtMenu5.Click += ConTxtMenu5_Click;
            // 
            // conTxtMenu6
            // 
            conTxtMenu6.Name = "conTxtMenu6";
            conTxtMenu6.Size = new Size(168, 24);
            conTxtMenu6.Text = "ファイルサイズ(&6)";
            conTxtMenu6.Click += ConTxtMenu6_Click;
            // 
            // conTxtMenu7
            // 
            conTxtMenu7.Name = "conTxtMenu7";
            conTxtMenu7.Size = new Size(168, 24);
            conTxtMenu7.Text = "画像サイズ(&7)";
            conTxtMenu7.Click += ConTxtMenu7_Click;
            // 
            // conTxtMenu8
            // 
            conTxtMenu8.Name = "conTxtMenu8";
            conTxtMenu8.Size = new Size(168, 24);
            conTxtMenu8.Text = "更新日時(&8)";
            conTxtMenu8.Click += ConTxtMenu8_Click;
            // 
            // conTxtMenu9
            // 
            conTxtMenu9.Name = "conTxtMenu9";
            conTxtMenu9.Size = new Size(168, 24);
            conTxtMenu9.Text = "作成日時(&9)";
            conTxtMenu9.Click += ConTxtMenu9_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(numThumbnailSize);
            panel1.Controls.Add(menuStrip1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 38);
            panel1.TabIndex = 2;
            // 
            // numThumbnailSize
            // 
            numThumbnailSize.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            numThumbnailSize.Location = new Point(454, 7);
            numThumbnailSize.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numThumbnailSize.Minimum = new decimal(new int[] { 64, 0, 0, 0 });
            numThumbnailSize.Name = "numThumbnailSize";
            numThumbnailSize.ReadOnly = true;
            numThumbnailSize.Size = new Size(64, 29);
            numThumbnailSize.TabIndex = 3;
            numThumbnailSize.TextAlign = HorizontalAlignment.Right;
            numThumbnailSize.Value = new decimal(new int[] { 256, 0, 0, 0 });
            numThumbnailSize.ValueChanged += NumThumbnailSize_ValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenu, viewMenu, helpMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(265, 36);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { printMenu, exitMenu });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(85, 32);
            fileMenu.Text = "ファイル(&F)";
            // 
            // printMenu
            // 
            printMenu.Image = (Image)resources.GetObject("printMenu.Image");
            printMenu.ImageScaling = ToolStripItemImageScaling.None;
            printMenu.Name = "printMenu";
            printMenu.Size = new Size(188, 38);
            printMenu.Text = "一覧印刷(&P)...";
            printMenu.ToolTipText = "一覧印刷のプレビューを表示します。";
            printMenu.Click += printMenu_Click;
            // 
            // exitMenu
            // 
            exitMenu.Image = (Image)resources.GetObject("exitMenu.Image");
            exitMenu.ImageScaling = ToolStripItemImageScaling.None;
            exitMenu.Name = "exitMenu";
            exitMenu.Size = new Size(188, 38);
            exitMenu.Text = "終了(&X)";
            exitMenu.ToolTipText = "一覧帖を終了します。";
            exitMenu.Click += exitMenu_Click;
            // 
            // viewMenu
            // 
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { folderUpdate, toolStripSeparator1, foldertreeMenu, infoboxMenu, toolStripSeparator2, LeftRightLayout, TopBottomLayout, LayoutReset });
            viewMenu.Name = "viewMenu";
            viewMenu.Size = new Size(74, 32);
            viewMenu.Text = "表示(&V)";
            // 
            // folderUpdate
            // 
            folderUpdate.Image = (Image)resources.GetObject("folderUpdate.Image");
            folderUpdate.ImageScaling = ToolStripItemImageScaling.None;
            folderUpdate.Name = "folderUpdate";
            folderUpdate.Size = new Size(196, 38);
            folderUpdate.Text = "更新(&U)";
            folderUpdate.ToolTipText = "表示を更新(再読み込み)します。";
            folderUpdate.Click += folderUpdate_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(193, 6);
            // 
            // foldertreeMenu
            // 
            foldertreeMenu.Checked = true;
            foldertreeMenu.CheckState = CheckState.Checked;
            foldertreeMenu.Image = (Image)resources.GetObject("foldertreeMenu.Image");
            foldertreeMenu.ImageScaling = ToolStripItemImageScaling.None;
            foldertreeMenu.Name = "foldertreeMenu";
            foldertreeMenu.Size = new Size(196, 38);
            foldertreeMenu.Text = "フォルダツリー(&F)";
            foldertreeMenu.ToolTipText = "フォルダツリーの表示・非表示を切り替えます。";
            // 
            // infoboxMenu
            // 
            infoboxMenu.Checked = true;
            infoboxMenu.CheckState = CheckState.Checked;
            infoboxMenu.Image = (Image)resources.GetObject("infoboxMenu.Image");
            infoboxMenu.ImageScaling = ToolStripItemImageScaling.None;
            infoboxMenu.Name = "infoboxMenu";
            infoboxMenu.Size = new Size(196, 38);
            infoboxMenu.Text = "情報ボックス(&I)";
            infoboxMenu.ToolTipText = "情報ボックスの表示・非表示を切り替えます。";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(193, 6);
            // 
            // LeftRightLayout
            // 
            LeftRightLayout.Image = (Image)resources.GetObject("LeftRightLayout.Image");
            LeftRightLayout.ImageScaling = ToolStripItemImageScaling.None;
            LeftRightLayout.Name = "LeftRightLayout";
            LeftRightLayout.Size = new Size(196, 38);
            LeftRightLayout.Text = "左右配置";
            LeftRightLayout.ToolTipText = "サムネイルと画像ビューを左右に配置します。";
            LeftRightLayout.Click += LeftRightLayout_Click;
            // 
            // TopBottomLayout
            // 
            TopBottomLayout.Image = (Image)resources.GetObject("TopBottomLayout.Image");
            TopBottomLayout.ImageScaling = ToolStripItemImageScaling.None;
            TopBottomLayout.Name = "TopBottomLayout";
            TopBottomLayout.Size = new Size(196, 38);
            TopBottomLayout.Text = "上下配置";
            TopBottomLayout.ToolTipText = "サムネイルと画像ビューを上下に配置します。";
            TopBottomLayout.Click += TopBottomLayout_Click;
            // 
            // LayoutReset
            // 
            LayoutReset.Image = (Image)resources.GetObject("LayoutReset.Image");
            LayoutReset.ImageScaling = ToolStripItemImageScaling.None;
            LayoutReset.Name = "LayoutReset";
            LayoutReset.Size = new Size(196, 38);
            LayoutReset.Text = "配置解除";
            LayoutReset.ToolTipText = "サムネイルと画像ビューの配置を解除します。";
            LayoutReset.Click += LayoutReset_Click;
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { useMenu, verMenu, SettingFolderMenu });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(81, 32);
            helpMenu.Text = "ヘルプ(&H)";
            // 
            // useMenu
            // 
            useMenu.Image = (Image)resources.GetObject("useMenu.Image");
            useMenu.ImageScaling = ToolStripItemImageScaling.None;
            useMenu.Name = "useMenu";
            useMenu.Size = new Size(290, 38);
            useMenu.Text = "使い方(&U)...";
            useMenu.ToolTipText = "使い方.pdfを表示します。";
            useMenu.Click += useMenu_Click;
            // 
            // verMenu
            // 
            verMenu.Image = (Image)resources.GetObject("verMenu.Image");
            verMenu.ImageScaling = ToolStripItemImageScaling.None;
            verMenu.Name = "verMenu";
            verMenu.Size = new Size(290, 38);
            verMenu.Text = "バージョン情報(&A)...";
            verMenu.ToolTipText = "バージョン情報を表示します。";
            verMenu.Click += verMenu_Click;
            // 
            // SettingFolderMenu
            // 
            SettingFolderMenu.Image = (Image)resources.GetObject("SettingFolderMenu.Image");
            SettingFolderMenu.ImageScaling = ToolStripItemImageScaling.None;
            SettingFolderMenu.Name = "SettingFolderMenu";
            SettingFolderMenu.Size = new Size(290, 38);
            SettingFolderMenu.Text = "設定ファイルのフォルダを開く(&E)";
            SettingFolderMenu.ToolTipText = "終了時のパスの記録しているフォルダを開きます。";
            SettingFolderMenu.Click += SettingFolderMenu_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(268, 9);
            label1.Name = "label1";
            label1.Size = new Size(187, 21);
            label1.TabIndex = 2;
            label1.Text = "表示サイズ（64～256）：";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel1 });
            statusStrip1.Location = new Point(0, 609);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new Size(886, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            StatusLabel1.Name = "StatusLabel1";
            StatusLabel1.Size = new Size(74, 21);
            StatusLabel1.Text = "ここに文字";
            // 
            // pathTxt
            // 
            pathTxt.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            pathTxt.Location = new Point(54, 48);
            pathTxt.Name = "pathTxt";
            pathTxt.Size = new Size(100, 33);
            pathTxt.TabIndex = 6;
            pathTxt.Click += pathTxt_Click;
            pathTxt.KeyDown += PathTxt_KeyDown;
            pathTxt.Leave += pathTxt_Leave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(886, 635);
            Controls.Add(splitContainer1);
            Controls.Add(pathTxt);
            Controls.Add(statusStrip1);
            Controls.Add(panel1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MyView";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            conMenu1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThumbnailSize).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private TreeView treeViewFolders;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabel1;
        private Panel panel1;
        private Label label1;
        private ListView listViewThumbnails;
        private NumericUpDown numThumbnailSize;
        private TextBox logTxt;
        private ContextMenuStrip conMenu1;
        //private MenuStrip menuStrip1;
        private ClickThroughMenuStrip menuStrip1; 
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem exitMenu;
        private ToolStripMenuItem helpMenu;
        private TextBox pathTxt;
        private ToolStripMenuItem verMenu;
        private ToolStripMenuItem conTxtMenu1;
        private ToolStripMenuItem conTxtMenu2;
        private ToolStripMenuItem conTxtMenu3;
        private ToolStripMenuItem conTxtMenu4;
        private ToolStripMenuItem conTxtMenu5;
        private ToolStripMenuItem conTxtMenu6;
        private ToolStripMenuItem conTxtMenu7;
        private ToolStripMenuItem conTxtMenu8;
        private ToolStripMenuItem conTxtMenu9;
        private ToolStripMenuItem SettingFolderMenu;
        private ToolStripMenuItem viewMenu;
        private ToolStripMenuItem folderUpdate;
        private ToolStripMenuItem useMenu;
        private ToolStripMenuItem printMenu;
        private SplitContainer splitContainer2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem foldertreeMenu;
        private ToolStripMenuItem infoboxMenu;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem LeftRightLayout;
        private ToolStripMenuItem TopBottomLayout;
        private ToolStripMenuItem LayoutReset;
    }
}
