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
            btnPageSet = new Button();
            btnPrintSet = new Button();
            btnNext = new Button();
            btnPrev = new Button();
            btnClose = new Button();
            btnPrint = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            previewControl = new PrintPreviewControl();
            splitContainer1 = new SplitContainer();
            toolTip1 = new ToolTip(components);
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
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(paperlabel);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(Pagelabel);
            panel2.Controls.Add(PrinterNamelabel);
            panel2.Controls.Add(btnPageSet);
            panel2.Controls.Add(btnPrintSet);
            panel2.Controls.Add(btnNext);
            panel2.Controls.Add(btnPrev);
            panel2.Controls.Add(btnClose);
            panel2.Controls.Add(btnPrint);
            panel2.Location = new Point(41, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(313, 633);
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
            groupBox1.Size = new Size(283, 222);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "配置・解像度・フォント";
            // 
            // setFontSize
            // 
            setFontSize.Location = new Point(122, 141);
            setFontSize.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            setFontSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            setFontSize.Name = "setFontSize";
            setFontSize.Size = new Size(70, 29);
            setFontSize.TabIndex = 10;
            setFontSize.Tag = "フォントサイズを指定します。(1～16)";
            setFontSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 143);
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
            setFont.Location = new Point(91, 104);
            setFont.Name = "setFont";
            setFont.Size = new Size(178, 29);
            setFont.TabIndex = 9;
            setFont.Tag = "フォントを指定します。";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 107);
            label5.Name = "label5";
            label5.Size = new Size(71, 21);
            label5.TabIndex = 12;
            label5.Text = "フォント：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 68);
            label4.Name = "label4";
            label4.Size = new Size(74, 21);
            label4.TabIndex = 11;
            label4.Text = "解像度：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(178, 68);
            label3.Name = "label3";
            label3.Size = new Size(32, 21);
            label3.TabIndex = 10;
            label3.Text = "dpi";
            // 
            // setDpi
            // 
            setDpi.DropDownStyle = ComboBoxStyle.DropDownList;
            setDpi.FormattingEnabled = true;
            setDpi.Location = new Point(91, 65);
            setDpi.Name = "setDpi";
            setDpi.Size = new Size(81, 29);
            setDpi.TabIndex = 8;
            setDpi.Tag = "解像度を指定します。";
            setDpi.MouseEnter += Menu_MouseEnter;
            setDpi.MouseLeave += Menu_MouseLeave;
            // 
            // btnHaichi
            // 
            btnHaichi.Location = new Point(9, 176);
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
            yoko.Location = new Point(199, 27);
            yoko.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            yoko.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            yoko.Name = "yoko";
            yoko.Size = new Size(70, 29);
            yoko.TabIndex = 7;
            yoko.Tag = "横方向の配置数を指定します。(1～10)";
            yoko.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(150, 29);
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
            tate.Size = new Size(70, 29);
            tate.TabIndex = 6;
            tate.Tag = "縦方向の配置数を指定します。(1～10)";
            tate.Value = new decimal(new int[] { 4, 0, 0, 0 });
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
            // btnPageSet
            // 
            btnPageSet.Location = new Point(15, 418);
            btnPageSet.Name = "btnPageSet";
            btnPageSet.Size = new Size(120, 40);
            btnPageSet.TabIndex = 3;
            btnPageSet.Tag = "ページ設定を行います。";
            btnPageSet.Text = "ページ設定...";
            btnPageSet.UseVisualStyleBackColor = true;
            btnPageSet.Click += btnPageSet_Click;
            btnPageSet.MouseEnter += Menu_MouseEnter;
            btnPageSet.MouseLeave += Menu_MouseLeave;
            // 
            // btnPrintSet
            // 
            btnPrintSet.Location = new Point(15, 372);
            btnPrintSet.Name = "btnPrintSet";
            btnPrintSet.Size = new Size(120, 40);
            btnPrintSet.TabIndex = 2;
            btnPrintSet.Tag = "プリンタ設定を行います。";
            btnPrintSet.Text = "プリンタ設定...";
            btnPrintSet.UseVisualStyleBackColor = true;
            btnPrintSet.Click += btnPrintSet_Click;
            btnPrintSet.MouseEnter += Menu_MouseEnter;
            btnPrintSet.MouseLeave += Menu_MouseLeave;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(15, 511);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(120, 40);
            btnNext.TabIndex = 5;
            btnNext.Tag = "次のページを表示します。";
            btnNext.Text = "次のページ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            btnNext.MouseEnter += Menu_MouseEnter;
            btnNext.MouseLeave += Menu_MouseLeave;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(15, 464);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(120, 40);
            btnPrev.TabIndex = 4;
            btnPrev.Tag = "前のページを表示します。";
            btnPrev.Text = "前のページ";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            btnPrev.MouseEnter += Menu_MouseEnter;
            btnPrev.MouseLeave += Menu_MouseLeave;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(15, 585);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 40);
            btnClose.TabIndex = 12;
            btnClose.Tag = "一覧印刷画面を閉じます。";
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            btnClose.MouseEnter += Menu_MouseEnter;
            btnClose.MouseLeave += Menu_MouseLeave;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(15, 326);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(120, 40);
            btnPrint.TabIndex = 1;
            btnPrint.Tag = "印刷します。";
            btnPrint.Text = "印刷...";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            btnPrint.MouseLeave += Menu_MouseLeave;
            btnPrint.MouseHover += Menu_MouseEnter;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 706);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(895, 26);
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
            splitContainer1.Location = new Point(23, 12);
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
            splitContainer1.Size = new Size(821, 679);
            splitContainer1.SplitterDistance = 451;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 11;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(895, 732);
            Controls.Add(splitContainer1);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private Label Pagelabel;
        private Label PrinterNamelabel;
        private Button btnPageSet;
        private Button btnPrintSet;
        private Button btnNext;
        private Button btnPrev;
        private Button btnClose;
        private Button btnPrint;
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
    }
}