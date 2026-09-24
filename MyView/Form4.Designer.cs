namespace MyView
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            panel2 = new Panel();
            paperlabel = new Label();
            groupBox1 = new GroupBox();
            label8 = new Label();
            setDirection = new ComboBox();
            setPaper = new ComboBox();
            label7 = new Label();
            setFontSize = new NumericUpDown();
            label6 = new Label();
            setFont = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            setDpi = new ComboBox();
            btnHaichi = new Button();
            label1 = new Label();
            yoko = new NumericUpDown();
            label2 = new Label();
            tate = new NumericUpDown();
            Pagelabel = new Label();
            PrinterNamelabel = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            previewControl = new PrintPreviewControl();
            splitContainer1 = new SplitContainer();
            toolTip1 = new ToolTip(components);
            toolStripContainer1 = new ToolStripContainer();
            toolStrip1 = new ToolStrip();
            btnPrint = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnPrintSet = new ToolStripButton();
            btnPageSet = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnPrev = new ToolStripButton();
            btnNext = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnClose = new ToolStripButton();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)setFontSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yoko).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tate).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(paperlabel);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(Pagelabel);
            panel2.Controls.Add(PrinterNamelabel);
            panel2.Location = new Point(41, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(313, 603);
            panel2.TabIndex = 8;
            // 
            // paperlabel
            // 
            paperlabel.AutoSize = true;
            paperlabel.Location = new Point(14, 64);
            paperlabel.Name = "paperlabel";
            paperlabel.Size = new Size(121, 21);
            paperlabel.TabIndex = 11;
            paperlabel.Text = "用紙サイズ、方向";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(setDirection);
            groupBox1.Controls.Add(setPaper);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(setFontSize);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(setFont);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(setDpi);
            groupBox1.Controls.Add(btnHaichi);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(yoko);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tate);
            groupBox1.Location = new Point(15, 98);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(283, 487);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "設定";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 185);
            label8.Name = "label8";
            label8.Size = new Size(99, 21);
            label8.TabIndex = 18;
            label8.Text = "用紙の向き：";
            // 
            // setDirection
            // 
            setDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            setDirection.FormattingEnabled = true;
            setDirection.Location = new Point(114, 182);
            setDirection.Name = "setDirection";
            setDirection.Size = new Size(73, 29);
            setDirection.TabIndex = 17;
            // 
            // setPaper
            // 
            setPaper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setPaper.DropDownStyle = ComboBoxStyle.DropDownList;
            setPaper.FormattingEnabled = true;
            setPaper.Location = new Point(73, 144);
            setPaper.Name = "setPaper";
            setPaper.Size = new Size(196, 29);
            setPaper.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 147);
            label7.Name = "label7";
            label7.Size = new Size(58, 21);
            label7.TabIndex = 15;
            label7.Text = "用紙：";
            // 
            // setFontSize
            // 
            setFontSize.Location = new Point(122, 261);
            setFontSize.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            setFontSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            setFontSize.Name = "setFontSize";
            setFontSize.Size = new Size(50, 29);
            setFontSize.TabIndex = 10;
            setFontSize.Tag = "フォントサイズを指定します。(1～16)";
            setFontSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            setFontSize.MouseClick += NumericUpDown_MouseClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 263);
            label6.Name = "label6";
            label6.Size = new Size(107, 21);
            label6.TabIndex = 14;
            label6.Text = "フォントサイズ：";
            // 
            // setFont
            // 
            setFont.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setFont.DropDownStyle = ComboBoxStyle.DropDownList;
            setFont.FormattingEnabled = true;
            setFont.Location = new Point(91, 224);
            setFont.Name = "setFont";
            setFont.Size = new Size(178, 29);
            setFont.TabIndex = 9;
            setFont.Tag = "フォントを指定します。";
            setFont.MouseEnter += Menu_MouseEnter;
            setFont.MouseLeave += Menu_MouseLeave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 227);
            label5.Name = "label5";
            label5.Size = new Size(71, 21);
            label5.TabIndex = 12;
            label5.Text = "フォント：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 109);
            label4.Name = "label4";
            label4.Size = new Size(74, 21);
            label4.TabIndex = 11;
            label4.Text = "解像度：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(175, 109);
            label3.Name = "label3";
            label3.Size = new Size(32, 21);
            label3.TabIndex = 10;
            label3.Text = "dpi";
            // 
            // setDpi
            // 
            setDpi.DropDownStyle = ComboBoxStyle.DropDownList;
            setDpi.FormattingEnabled = true;
            setDpi.Location = new Point(88, 106);
            setDpi.Name = "setDpi";
            setDpi.Size = new Size(81, 29);
            setDpi.TabIndex = 8;
            setDpi.Tag = "解像度を指定します。";
            setDpi.MouseEnter += Menu_MouseEnter;
            setDpi.MouseLeave += Menu_MouseLeave;
            // 
            // btnHaichi
            // 
            btnHaichi.Location = new Point(6, 447);
            btnHaichi.Name = "btnHaichi";
            btnHaichi.Size = new Size(120, 40);
            btnHaichi.TabIndex = 11;
            btnHaichi.Tag = "設定を反映します。";
            btnHaichi.Text = "設定反映";
            btnHaichi.UseVisualStyleBackColor = true;
            btnHaichi.Click += btnHaichi_Click;
            btnHaichi.MouseEnter += Menu_MouseEnter;
            btnHaichi.MouseLeave += Menu_MouseLeave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 29);
            label1.Name = "label1";
            label1.Size = new Size(42, 21);
            label1.TabIndex = 3;
            label1.Text = "縦：";
            // 
            // yoko
            // 
            yoko.Location = new Point(58, 66);
            yoko.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            yoko.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            yoko.Name = "yoko";
            yoko.Size = new Size(50, 29);
            yoko.TabIndex = 7;
            yoko.Tag = "横方向の配置数を指定します。(1～10)";
            yoko.Value = new decimal(new int[] { 2, 0, 0, 0 });
            yoko.MouseClick += NumericUpDown_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 68);
            label2.Name = "label2";
            label2.Size = new Size(42, 21);
            label2.TabIndex = 4;
            label2.Text = "横：";
            // 
            // tate
            // 
            tate.Location = new Point(57, 27);
            tate.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tate.Name = "tate";
            tate.Size = new Size(50, 29);
            tate.TabIndex = 6;
            tate.Tag = "縦方向の配置数を指定します。(1～10)";
            tate.Value = new decimal(new int[] { 4, 0, 0, 0 });
            tate.MouseClick += NumericUpDown_MouseClick;
            // 
            // Pagelabel
            // 
            Pagelabel.AutoSize = true;
            Pagelabel.Location = new Point(14, 36);
            Pagelabel.Name = "Pagelabel";
            Pagelabel.Size = new Size(46, 21);
            Pagelabel.TabIndex = 2;
            Pagelabel.Text = "ページ";
            // 
            // PrinterNamelabel
            // 
            PrinterNamelabel.AutoSize = true;
            PrinterNamelabel.Location = new Point(14, 10);
            PrinterNamelabel.Name = "PrinterNamelabel";
            PrinterNamelabel.Size = new Size(72, 21);
            PrinterNamelabel.TabIndex = 2;
            PrinterNamelabel.Text = "プリンタ名";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 730);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(776, 26);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(159, 21);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // previewControl
            // 
            previewControl.AutoZoom = false;
            previewControl.Location = new Point(77, 66);
            previewControl.Name = "previewControl";
            previewControl.Size = new Size(232, 93);
            previewControl.TabIndex = 10;
            previewControl.Zoom = 1D;
            previewControl.MouseDown += previewControl_MouseDown;
            previewControl.MouseMove += previewControl_MouseMove;
            previewControl.MouseUp += previewControl_MouseUp;
            // 
            // splitContainer1
            // 
            splitContainer1.Cursor = Cursors.SizeWE;
            splitContainer1.Location = new Point(41, 17);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Cursor = Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(previewControl);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(669, 642);
            splitContainer1.SplitterDistance = 367;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 11;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(splitContainer1);
            toolStripContainer1.ContentPanel.Size = new Size(745, 662);
            toolStripContainer1.Location = new Point(6, 12);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(745, 690);
            toolStripContainer1.TabIndex = 13;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnPrint, toolStripSeparator1, btnPrintSet, btnPageSet, toolStripSeparator2, btnPrev, btnNext, toolStripSeparator3, btnClose });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(462, 28);
            toolStrip1.TabIndex = 0;
            // 
            // btnPrint
            // 
            btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrint.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(55, 25);
            btnPrint.Tag = "印刷します。";
            btnPrint.Text = "印刷...";
            btnPrint.ToolTipText = "印刷します。";
            btnPrint.Click += btnPrint_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 28);
            // 
            // btnPrintSet
            // 
            btnPrintSet.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrintSet.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnPrintSet.Image = (Image)resources.GetObject("btnPrintSet.Image");
            btnPrintSet.ImageTransparentColor = Color.Magenta;
            btnPrintSet.Name = "btnPrintSet";
            btnPrintSet.Size = new Size(101, 25);
            btnPrintSet.Tag = "プリンタ設定を行います。";
            btnPrintSet.Text = "プリンタ設定...";
            btnPrintSet.ToolTipText = "プリンタ設定を行います。";
            btnPrintSet.Click += btnPrintSet_Click;
            // 
            // btnPageSet
            // 
            btnPageSet.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPageSet.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnPageSet.Image = (Image)resources.GetObject("btnPageSet.Image");
            btnPageSet.ImageTransparentColor = Color.Magenta;
            btnPageSet.Name = "btnPageSet";
            btnPageSet.Size = new Size(91, 25);
            btnPageSet.Text = "ページ設定...";
            btnPageSet.Click += btnPageSet_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 28);
            // 
            // btnPrev
            // 
            btnPrev.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrev.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnPrev.Image = (Image)resources.GetObject("btnPrev.Image");
            btnPrev.ImageTransparentColor = Color.Magenta;
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(66, 25);
            btnPrev.Tag = "前のページを表示します。";
            btnPrev.Text = "前ページ";
            btnPrev.ToolTipText = "前のページを表示します。";
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnNext.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnNext.Image = (Image)resources.GetObject("btnNext.Image");
            btnNext.ImageTransparentColor = Color.Magenta;
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(66, 25);
            btnNext.Tag = "次のページを表示します。";
            btnNext.Text = "次ページ";
            btnNext.ToolTipText = "次のページを表示します。";
            btnNext.Click += btnNext_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 28);
            // 
            // btnClose
            // 
            btnClose.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnClose.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnClose.Image = (Image)resources.GetObject("btnClose.Image");
            btnClose.ImageTransparentColor = Color.Magenta;
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(53, 25);
            btnClose.Tag = "一覧印刷を閉じます。";
            btnClose.Text = "閉じる";
            btnClose.ToolTipText = "一覧印刷を閉じます。";
            btnClose.Click += btnClose_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 756);
            Controls.Add(toolStripContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form4";
            StartPosition = FormStartPosition.CenterParent;
            Text = "一覧印刷";
            FormClosing += Form4_FormClosing;
            Load += Form4_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)setFontSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)yoko).EndInit();
            ((System.ComponentModel.ISupportInitialize)tate).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private Label Pagelabel;
        private Label PrinterNamelabel;
        private StatusStrip statusStrip1;
        private PrintPreviewControl previewControl;
        private NumericUpDown yoko;
        private NumericUpDown tate;
        private Label label2;
        private Label label1;
        private SplitContainer splitContainer1;
        private Button btnHaichi;
        private GroupBox groupBox1;
        private Label label3;
        private ComboBox setDpi;
        private Label label4;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolTip toolTip1;
        private Label paperlabel;
        private Label label5;
        private ComboBox setFont;
        private NumericUpDown setFontSize;
        private Label label6;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        private ToolStripButton btnPrint;
        private ToolStripButton btnPrintSet;
        private ToolStripButton btnPageSet;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnPrev;
        private ToolStripButton btnNext;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnClose;
        private Label label8;
        private ComboBox setDirection;
        private ComboBox setPaper;
        private Label label7;
    }
}