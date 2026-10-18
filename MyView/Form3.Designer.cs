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
            printBtn2 = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            saveBtn2 = new ToolStripMenuItem();
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
            toolStrip1 = new ToolStrip();
            printBtn = new ToolStripButton();
            toolStripSeparator11 = new ToolStripSeparator();
            saveBtn = new ToolStripButton();
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
            closeBtn = new ToolStripButton();
            panel1 = new Panel();
            toolStripContainer1 = new ToolStripContainer();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            conTxtMenu1.SuspendLayout();
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
            conTxtMenu1.Items.AddRange(new ToolStripItem[] { printBtn2, toolStripSeparator12, saveBtn2, toolStripSeparator6, copyBtn2, toolStripSeparator7, pasteBtn2, toolStripSeparator8, zoomInBtn2, zoomOutBtn2, toolStripSeparator9, roolLeftBtn2, roolRightBtn2, toolStripSeparator10, closeBtn2 });
            conTxtMenu1.Name = "conTxtMenu1";
            conTxtMenu1.Size = new Size(207, 296);
            conTxtMenu1.Opening += conTxtMenu1_Opening;
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
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(203, 6);
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
            roolLeftBtn2.ToolTipText = "左へ90°回転(Ctrl+L)";
            roolLeftBtn2.Click += roolLeftBtn_Click;
            // 
            // roolRightBtn2
            // 
            roolRightBtn2.Image = (Image)resources.GetObject("roolRightBtn2.Image");
            roolRightBtn2.Name = "roolRightBtn2";
            roolRightBtn2.Size = new Size(206, 26);
            roolRightBtn2.Text = "右へ回転";
            roolRightBtn2.ToolTipText = "右へ90°回転(Ctrl+R)";
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
            statusStrip1.Size = new Size(921, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // viewStatusTxt
            // 
            viewStatusTxt.Name = "viewStatusTxt";
            viewStatusTxt.Size = new Size(906, 21);
            viewStatusTxt.Spring = true;
            viewStatusTxt.Text = "ここにフルパス";
            viewStatusTxt.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            toolStrip1.Items.AddRange(new ToolStripItem[] { printBtn, toolStripSeparator11, saveBtn, toolStripSeparator1, copyBtn, toolStripSeparator2, pasteBtn, toolStripSeparator3, zoomInBtn, zoomOutBtn, toolStripSeparator4, roolLeftBtn, roolRightBtn, toolStripSeparator5, closeBtn });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(906, 28);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // printBtn
            // 
            printBtn.Image = (Image)resources.GetObject("printBtn.Image");
            printBtn.ImageTransparentColor = Color.Magenta;
            printBtn.Name = "printBtn";
            printBtn.Size = new Size(90, 25);
            printBtn.Tag = "画像に印刷します。";
            printBtn.Text = "印刷(&P)...";
            printBtn.ToolTipText = "印刷プレビューを表示します。(Ctrl+P)";
            printBtn.Click += printBtn_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new Size(6, 28);
            // 
            // saveBtn
            // 
            saveBtn.Image = (Image)resources.GetObject("saveBtn.Image");
            saveBtn.ImageTransparentColor = Color.Magenta;
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(175, 25);
            saveBtn.Tag = "画像に名前を付けて保存します。";
            saveBtn.Text = "名前を付けて保存(&S)...";
            saveBtn.ToolTipText = "名前を付けて保存(Ctrl+S)";
            saveBtn.Click += saveBtn_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // copyBtn
            // 
            copyBtn.Image = (Image)resources.GetObject("copyBtn.Image");
            copyBtn.ImageTransparentColor = Color.Magenta;
            copyBtn.Name = "copyBtn";
            copyBtn.Size = new Size(82, 25);
            copyBtn.Tag = "画像をコピーします。";
            copyBtn.Text = "コピー(&C)";
            copyBtn.ToolTipText = "コピー(Ctrl+C)";
            copyBtn.Click += copyBtn_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // pasteBtn
            // 
            pasteBtn.Image = (Image)resources.GetObject("pasteBtn.Image");
            pasteBtn.ImageTransparentColor = Color.Magenta;
            pasteBtn.Name = "pasteBtn";
            pasteBtn.Size = new Size(106, 25);
            pasteBtn.Tag = "画像を貼り付けます。";
            pasteBtn.Text = "貼り付け(&V)";
            pasteBtn.ToolTipText = "貼り付け(Ctrl+V)";
            pasteBtn.Click += pasteBtn_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // zoomInBtn
            // 
            zoomInBtn.Image = (Image)resources.GetObject("zoomInBtn.Image");
            zoomInBtn.ImageTransparentColor = Color.Magenta;
            zoomInBtn.Name = "zoomInBtn";
            zoomInBtn.Size = new Size(62, 25);
            zoomInBtn.Tag = "画像を拡大します。";
            zoomInBtn.Text = "拡大";
            zoomInBtn.ToolTipText = "拡大(Ctrl+上スクロール)";
            zoomInBtn.Click += zoomInBtn_Click;
            // 
            // zoomOutBtn
            // 
            zoomOutBtn.Image = (Image)resources.GetObject("zoomOutBtn.Image");
            zoomOutBtn.ImageTransparentColor = Color.Magenta;
            zoomOutBtn.Name = "zoomOutBtn";
            zoomOutBtn.Size = new Size(62, 25);
            zoomOutBtn.Tag = "画像を縮小します。";
            zoomOutBtn.Text = "縮小";
            zoomOutBtn.ToolTipText = "縮小(Ctrl+下スクロール)";
            zoomOutBtn.Click += zoomOutBtn_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 28);
            // 
            // roolLeftBtn
            // 
            roolLeftBtn.Image = (Image)resources.GetObject("roolLeftBtn.Image");
            roolLeftBtn.ImageTransparentColor = Color.Magenta;
            roolLeftBtn.Name = "roolLeftBtn";
            roolLeftBtn.Size = new Size(109, 25);
            roolLeftBtn.Tag = "画像を左へ90°回転します。";
            roolLeftBtn.Text = "左へ回転(&L)";
            roolLeftBtn.ToolTipText = "左へ90°回転(Ctrl+L)";
            roolLeftBtn.Click += roolLeftBtn_Click;
            // 
            // roolRightBtn
            // 
            roolRightBtn.Image = (Image)resources.GetObject("roolRightBtn.Image");
            roolRightBtn.ImageTransparentColor = Color.Magenta;
            roolRightBtn.Name = "roolRightBtn";
            roolRightBtn.Size = new Size(111, 25);
            roolRightBtn.Tag = "画像を右へ90°回転します。";
            roolRightBtn.Text = "右へ回転(&R)";
            roolRightBtn.ToolTipText = "右へ90°回転(Ctrl+R)";
            roolRightBtn.Click += roolRightBtn_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 28);
            // 
            // closeBtn
            // 
            closeBtn.Image = (Image)resources.GetObject("closeBtn.Image");
            closeBtn.ImageTransparentColor = Color.Magenta;
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(88, 25);
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
            toolStripContainer1.ContentPanel.Size = new Size(909, 320);
            toolStripContainer1.Location = new Point(12, 52);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(909, 348);
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
            ClientSize = new Size(921, 496);
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
    }
}