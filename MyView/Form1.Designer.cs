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
            treeViewFolders = new TreeView();
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
            label1 = new Label();
            statusStrip1 = new StatusStrip();
            StatusLabel1 = new ToolStripStatusLabel();
            logTxt = new TextBox();
            menuStrip1 = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            printMenu = new ToolStripMenuItem();
            exitMenu = new ToolStripMenuItem();
            viewMenu = new ToolStripMenuItem();
            folderUpdate = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            useMenu = new ToolStripMenuItem();
            verMenu = new ToolStripMenuItem();
            SettingFolderMenu = new ToolStripMenuItem();
            pathTxt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            conMenu1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numThumbnailSize).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Cursor = Cursors.SizeWE;
            splitContainer1.Location = new Point(12, 84);
            splitContainer1.MinimumSize = new Size(500, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeViewFolders);
            splitContainer1.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listViewThumbnails);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(673, 173);
            splitContainer1.SplitterDistance = 238;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 0;
            // 
            // treeViewFolders
            // 
            treeViewFolders.BackColor = Color.White;
            treeViewFolders.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            treeViewFolders.HideSelection = false;
            treeViewFolders.Location = new Point(14, 34);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(136, 107);
            treeViewFolders.TabIndex = 0;
            treeViewFolders.BeforeExpand += TreeViewFolders_BeforeExpand;
            treeViewFolders.AfterSelect += TreeViewFolders_AfterSelect;
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
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(427, 38);
            panel1.TabIndex = 2;
            // 
            // numThumbnailSize
            // 
            numThumbnailSize.Increment = new decimal(new int[] { 32, 0, 0, 0 });
            numThumbnailSize.Location = new Point(202, 3);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 5);
            label1.Name = "label1";
            label1.Size = new Size(187, 21);
            label1.TabIndex = 2;
            label1.Text = "表示サイズ（64～256）：";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel1 });
            statusStrip1.Location = new Point(0, 343);
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
            // logTxt
            // 
            logTxt.Location = new Point(12, 272);
            logTxt.Multiline = true;
            logTxt.Name = "logTxt";
            logTxt.ScrollBars = ScrollBars.Both;
            logTxt.Size = new Size(100, 50);
            logTxt.TabIndex = 3;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenu, viewMenu, helpMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(886, 36);
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
            printMenu.Name = "printMenu";
            printMenu.Size = new Size(211, 26);
            printMenu.Text = "インデックス印刷(&P)...";
            printMenu.ToolTipText = "インデックス印刷します。";
            printMenu.Click += printMenu_Click;
            // 
            // exitMenu
            // 
            exitMenu.Name = "exitMenu";
            exitMenu.Size = new Size(211, 26);
            exitMenu.Text = "終了(&X)";
            exitMenu.ToolTipText = "一覧帖を終了します";
            exitMenu.Click += exitMenu_Click;
            // 
            // viewMenu
            // 
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { folderUpdate });
            viewMenu.Name = "viewMenu";
            viewMenu.Size = new Size(74, 32);
            viewMenu.Text = "表示(&V)";
            // 
            // folderUpdate
            // 
            folderUpdate.Name = "folderUpdate";
            folderUpdate.Size = new Size(133, 26);
            folderUpdate.Text = "更新(&U)";
            folderUpdate.ToolTipText = "表示を更新(再読み込み)します";
            folderUpdate.Click += folderUpdate_Click;
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
            useMenu.Name = "useMenu";
            useMenu.Size = new Size(274, 26);
            useMenu.Text = "使い方(&U)...";
            useMenu.ToolTipText = "使い方.pdfを表示します";
            useMenu.Click += useMenu_Click;
            // 
            // verMenu
            // 
            verMenu.Name = "verMenu";
            verMenu.Size = new Size(274, 26);
            verMenu.Text = "バージョン情報(&A)...";
            verMenu.ToolTipText = "バージョン情報を表示します";
            verMenu.Click += verMenu_Click;
            // 
            // SettingFolderMenu
            // 
            SettingFolderMenu.Name = "SettingFolderMenu";
            SettingFolderMenu.Size = new Size(274, 26);
            SettingFolderMenu.Text = "設定ファイルのフォルダを開く(&E)";
            SettingFolderMenu.ToolTipText = "終了時のパスの記録しているフォルダを開きます";
            SettingFolderMenu.Click += SettingFolderMenu_Click;
            // 
            // pathTxt
            // 
            pathTxt.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            pathTxt.Location = new Point(62, 39);
            pathTxt.Name = "pathTxt";
            pathTxt.Size = new Size(100, 33);
            pathTxt.TabIndex = 6;
            pathTxt.Click += pathTxt_Click;
            pathTxt.KeyDown += PathTxt_KeyDown;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(886, 369);
            Controls.Add(splitContainer1);
            Controls.Add(pathTxt);
            Controls.Add(logTxt);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MyView";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            conMenu1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numThumbnailSize).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private MenuStrip menuStrip1;
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
    }
}
