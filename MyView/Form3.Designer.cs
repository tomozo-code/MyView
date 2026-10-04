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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            pictureBox1 = new PictureBox();
            statusStrip1 = new StatusStrip();
            viewStatusTxt = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            saveBtn = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            copyBtn = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            pasteBtn = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            roolLeftBtn = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            roolRightBtn = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            closeBtn = new ToolStripButton();
            panel1 = new Panel();
            toolStripContainer1 = new ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.WindowFrame;
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
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { viewStatusTxt });
            statusStrip1.Location = new Point(0, 436);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(776, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // viewStatusTxt
            // 
            viewStatusTxt.Name = "viewStatusTxt";
            viewStatusTxt.Size = new Size(761, 21);
            viewStatusTxt.Spring = true;
            viewStatusTxt.Text = "ここにフルパス";
            viewStatusTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            toolStrip1.Items.AddRange(new ToolStripItem[] { saveBtn, toolStripSeparator1, copyBtn, toolStripSeparator2, pasteBtn, toolStripSeparator3, roolLeftBtn, toolStripSeparator4, roolRightBtn, toolStripSeparator5, closeBtn });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(715, 39);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // saveBtn
            // 
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.ImageScaling = ToolStripItemImageScaling.None;
            saveBtn.ImageTransparentColor = Color.Magenta;
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(163, 36);
            saveBtn.Tag = "画像に名前を付けて保存します";
            saveBtn.Text = "名前を付けて保存";
            saveBtn.ToolTipText = "名前を付けて保存(Ctrl+S)";
            saveBtn.Click += saveBtn_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 39);
            // 
            // copyBtn
            // 
            copyBtn.Image = (Image)resources.GetObject("copyBtn.Image");
            copyBtn.ImageScaling = ToolStripItemImageScaling.None;
            copyBtn.ImageTransparentColor = Color.Magenta;
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(78, 36);
            copyBtn.Tag = "画像をコピーします";
            copyBtn.Text = "コピー";
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
            pasteBtn.Image = (Image)resources.GetObject("pasteBtn.Image");
            pasteBtn.ImageScaling = ToolStripItemImageScaling.None;
            pasteBtn.ImageTransparentColor = Color.Magenta;
            pasteBtn.Name = "pasteBtn";
            pasteBtn.Size = new Size(102, 36);
            pasteBtn.Tag = "画像を貼り付けます";
            pasteBtn.Text = "貼り付け";
            pasteBtn.ToolTipText = "貼り付け(Ctrl+V)";
            pasteBtn.Click += pasteBtn_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 39);
            // 
            // roolLeftBtn
            // 
            roolLeftBtn.Image = (Image)resources.GetObject("roolLeftBtn.Image");
            roolLeftBtn.ImageScaling = ToolStripItemImageScaling.None;
            roolLeftBtn.ImageTransparentColor = Color.Magenta;
            roolLeftBtn.Name = "roolLeftBtn";
            roolLeftBtn.Size = new Size(107, 36);
            roolLeftBtn.Tag = "画像を左へ90°回転します";
            roolLeftBtn.Text = "左へ回転";
            roolLeftBtn.ToolTipText = "左へ90°回転(Ctrl+L)";
            roolLeftBtn.Click += roolLeftBtn_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 39);
            // 
            // roolRightBtn
            // 
            roolRightBtn.Image = (Image)resources.GetObject("roolRightBtn.Image");
            roolRightBtn.ImageScaling = ToolStripItemImageScaling.None;
            roolRightBtn.ImageTransparentColor = Color.Magenta;
            roolRightBtn.Name = "roolRightBtn";
            roolRightBtn.Size = new Size(107, 36);
            roolRightBtn.Tag = "画像を右へ90°回転します";
            roolRightBtn.Text = "右へ回転";
            roolRightBtn.ToolTipText = "右へ90°回転(Ctrl+R)";
            roolRightBtn.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 39);
            // 
            // closeBtn
            // 
            closeBtn.Image = (Image)resources.GetObject("closeBtn.Image");
            closeBtn.ImageScaling = ToolStripItemImageScaling.None;
            closeBtn.ImageTransparentColor = Color.Magenta;
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(85, 36);
            closeBtn.Tag = "ウィンドウを閉じます";
            closeBtn.Text = "閉じる";
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
            toolStripContainer1.ContentPanel.Size = new Size(739, 309);
            toolStripContainer1.Location = new Point(12, 52);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(739, 348);
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
            ClientSize = new Size(776, 462);
            Controls.Add(toolStripContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form3";
            Load += Form3_Load;
            KeyDown += Form3_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel viewStatusTxt;
        private ToolStrip toolStrip1;
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
    }
}