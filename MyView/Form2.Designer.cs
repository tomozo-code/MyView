namespace MyView
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            TreeNode treeNode1 = new TreeNode("サムネイル");
            TreeNode treeNode2 = new TreeNode("画像ビュー");
            TreeNode treeNode3 = new TreeNode("印刷プレビュー");
            TreeNode treeNode4 = new TreeNode("簡単な使い方", new TreeNode[] { treeNode1, treeNode2, treeNode3 });
            TreeNode treeNode5 = new TreeNode("サードパーティライセンス");
            TitleTxt = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            UseTxtBox = new TextBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            OkBtn = new Button();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            toolTip1 = new ToolTip(components);
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // TitleTxt
            // 
            TitleTxt.AutoSize = true;
            TitleTxt.Location = new Point(149, 9);
            TitleTxt.Name = "TitleTxt";
            TitleTxt.Size = new Size(57, 21);
            TitleTxt.TabIndex = 0;
            TitleTxt.Text = "タイトル";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(149, 39);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(68, 21);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "バージョン";
            // 
            // labelCopyright
            // 
            labelCopyright.AutoSize = true;
            labelCopyright.Location = new Point(149, 69);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(58, 21);
            labelCopyright.TabIndex = 2;
            labelCopyright.Text = "作成者";
            // 
            // UseTxtBox
            // 
            UseTxtBox.Location = new Point(50, 75);
            UseTxtBox.Multiline = true;
            UseTxtBox.Name = "UseTxtBox";
            UseTxtBox.ScrollBars = ScrollBars.Both;
            UseTxtBox.Size = new Size(92, 120);
            UseTxtBox.TabIndex = 3;
            UseTxtBox.WordWrap = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(111, 111);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(OkBtn);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 529);
            panel1.Name = "panel1";
            panel1.Size = new Size(551, 50);
            panel1.TabIndex = 5;
            // 
            // OkBtn
            // 
            OkBtn.Location = new Point(12, 11);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(100, 30);
            OkBtn.TabIndex = 0;
            OkBtn.Text = "OK";
            OkBtn.UseVisualStyleBackColor = true;
            OkBtn.Click += OkBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(149, 99);
            label2.Name = "label2";
            label2.Size = new Size(62, 21);
            label2.TabIndex = 11;
            label2.Text = "GitHub:";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(217, 99);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(298, 21);
            linkLabel1.TabIndex = 12;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/tomozo-code/MyView";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Cursor = Cursors.SizeWE;
            splitContainer1.Location = new Point(12, 140);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(UseTxtBox);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(527, 383);
            splitContainer1.SplitterDistance = 175;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 14;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(18, 31);
            treeView1.Name = "treeView1";
            treeNode1.Name = "thumbnail";
            treeNode1.Text = "サムネイル";
            treeNode2.Name = "view";
            treeNode2.Text = "画像ビュー";
            treeNode3.Name = "preview";
            treeNode3.Text = "印刷プレビュー";
            treeNode4.Name = "use";
            treeNode4.Text = "簡単な使い方";
            treeNode5.Name = "licenses";
            treeNode5.Text = "サードパーティライセンス";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode4, treeNode5 });
            treeView1.Size = new Size(154, 268);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 579);
            Controls.Add(splitContainer1);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(labelCopyright);
            Controls.Add(labelVersion);
            Controls.Add(TitleTxt);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "バージョン情報";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TitleTxt;
        private Label labelVersion;
        private Label labelCopyright;
        private TextBox UseTxtBox;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Button OkBtn;
        private Label label2;
        private LinkLabel linkLabel1;
        private ToolTip toolTip1;
        private SplitContainer splitContainer1;
        private TreeView treeView1;
    }
}