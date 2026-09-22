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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
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
            UseTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UseTxtBox.Location = new Point(12, 126);
            UseTxtBox.Multiline = true;
            UseTxtBox.Name = "UseTxtBox";
            UseTxtBox.Size = new Size(527, 303);
            UseTxtBox.TabIndex = 3;
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
            panel1.Location = new Point(0, 435);
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
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 485);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(UseTxtBox);
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
    }
}