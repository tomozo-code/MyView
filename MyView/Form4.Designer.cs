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
            groupBox4 = new GroupBox();
            setMarginBottom = new TextBox();
            setMarginTop = new TextBox();
            setMarginRight = new TextBox();
            setMarginLeft = new TextBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            groupBox2 = new GroupBox();
            setPrinterName = new ComboBox();
            label3 = new Label();
            label8 = new Label();
            label4 = new Label();
            setDirection = new ComboBox();
            setDpi = new ComboBox();
            setPaper = new ComboBox();
            label7 = new Label();
            groupBox3 = new GroupBox();
            setFileName = new ComboBox();
            label5 = new Label();
            label9 = new Label();
            setFont = new ComboBox();
            label6 = new Label();
            setFontSize = new NumericUpDown();
            groupBox1 = new GroupBox();
            label1 = new Label();
            yoko = new NumericUpDown();
            label2 = new Label();
            tate = new NumericUpDown();
            btnHaichi = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            previewControl = new PrintPreviewControl();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            Pagelabel = new Label();
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
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)setFontSize).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yoko).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tate).BeginInit();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(groupBox4);
            panel2.Controls.Add(groupBox2);
            panel2.Controls.Add(groupBox3);
            panel2.Controls.Add(groupBox1);
            panel2.Controls.Add(btnHaichi);
            panel2.Location = new Point(41, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(313, 649);
            panel2.TabIndex = 8;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(setMarginBottom);
            groupBox4.Controls.Add(setMarginTop);
            groupBox4.Controls.Add(setMarginRight);
            groupBox4.Controls.Add(setMarginLeft);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label10);
            groupBox4.Location = new Point(10, 300);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(290, 95);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "余白(mm)";
            // 
            // setMarginBottom
            // 
            setMarginBottom.ImeMode = ImeMode.Disable;
            setMarginBottom.Location = new Point(185, 58);
            setMarginBottom.Name = "setMarginBottom";
            setMarginBottom.ShortcutsEnabled = false;
            setMarginBottom.Size = new Size(60, 29);
            setMarginBottom.TabIndex = 9;
            setMarginBottom.Tag = "下余白を設定します。";
            setMarginBottom.Click += setMargin_Click;
            setMarginBottom.KeyDown += setMargin_KeyDown;
            setMarginBottom.KeyPress += setMargin_KeyPress;
            setMarginBottom.MouseEnter += Menu_MouseEnter;
            setMarginBottom.MouseLeave += Menu_MouseLeave;
            setMarginBottom.Validating += setMargin_Validating;
            // 
            // setMarginTop
            // 
            setMarginTop.ImeMode = ImeMode.Disable;
            setMarginTop.Location = new Point(61, 58);
            setMarginTop.Name = "setMarginTop";
            setMarginTop.ShortcutsEnabled = false;
            setMarginTop.Size = new Size(60, 29);
            setMarginTop.TabIndex = 8;
            setMarginTop.Tag = "上余白を設定します。";
            setMarginTop.Click += setMargin_Click;
            setMarginTop.KeyDown += setMargin_KeyDown;
            setMarginTop.KeyPress += setMargin_KeyPress;
            setMarginTop.MouseEnter += Menu_MouseEnter;
            setMarginTop.MouseLeave += Menu_MouseLeave;
            setMarginTop.Validating += setMargin_Validating;
            // 
            // setMarginRight
            // 
            setMarginRight.ImeMode = ImeMode.Disable;
            setMarginRight.Location = new Point(185, 22);
            setMarginRight.Name = "setMarginRight";
            setMarginRight.ShortcutsEnabled = false;
            setMarginRight.Size = new Size(60, 29);
            setMarginRight.TabIndex = 7;
            setMarginRight.Tag = "右余白を設定します。";
            setMarginRight.Click += setMargin_Click;
            setMarginRight.KeyDown += setMargin_KeyDown;
            setMarginRight.KeyPress += setMargin_KeyPress;
            setMarginRight.MouseEnter += Menu_MouseEnter;
            setMarginRight.MouseLeave += Menu_MouseLeave;
            setMarginRight.Validating += setMargin_Validating;
            // 
            // setMarginLeft
            // 
            setMarginLeft.ImeMode = ImeMode.Disable;
            setMarginLeft.Location = new Point(61, 22);
            setMarginLeft.Name = "setMarginLeft";
            setMarginLeft.ShortcutsEnabled = false;
            setMarginLeft.Size = new Size(60, 29);
            setMarginLeft.TabIndex = 6;
            setMarginLeft.Tag = "左余白を設定します。";
            setMarginLeft.Click += setMargin_Click;
            setMarginLeft.KeyDown += setMargin_KeyDown;
            setMarginLeft.KeyPress += setMargin_KeyPress;
            setMarginLeft.MouseEnter += Menu_MouseEnter;
            setMarginLeft.MouseLeave += Menu_MouseLeave;
            setMarginLeft.Validating += setMargin_Validating;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(133, 61);
            label13.Name = "label13";
            label13.Size = new Size(42, 21);
            label13.TabIndex = 3;
            label13.Text = "下：";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(10, 61);
            label12.Name = "label12";
            label12.Size = new Size(42, 21);
            label12.TabIndex = 2;
            label12.Text = "上：";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(133, 25);
            label11.Name = "label11";
            label11.Size = new Size(42, 21);
            label11.TabIndex = 1;
            label11.Text = "右：";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(10, 25);
            label10.Name = "label10";
            label10.Size = new Size(42, 21);
            label10.TabIndex = 0;
            label10.Text = "左：";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(setPrinterName);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(setDirection);
            groupBox2.Controls.Add(setDpi);
            groupBox2.Controls.Add(setPaper);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(10, 126);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(290, 165);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "用紙";
            // 
            // setPrinterName
            // 
            setPrinterName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            setPrinterName.FormattingEnabled = true;
            setPrinterName.Location = new Point(125, 22);
            setPrinterName.Name = "setPrinterName";
            setPrinterName.Size = new Size(154, 29);
            setPrinterName.TabIndex = 2;
            setPrinterName.Tag = "プリンタを変更します。変更後プレビューが更新されます。";
            setPrinterName.SelectedIndexChanged += setPrinterName_SelectedIndexChanged;
            setPrinterName.MouseEnter += Menu_MouseEnter;
            setPrinterName.MouseLeave += Menu_MouseLeave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 25);
            label3.Name = "label3";
            label3.Size = new Size(88, 21);
            label3.TabIndex = 19;
            label3.Text = "プリンタ名：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(10, 133);
            label8.Name = "label8";
            label8.Size = new Size(99, 21);
            label8.TabIndex = 18;
            label8.Text = "用紙の向き：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 61);
            label4.Name = "label4";
            label4.Size = new Size(106, 21);
            label4.TabIndex = 11;
            label4.Text = "解像度(dpi)：";
            // 
            // setDirection
            // 
            setDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            setDirection.FormattingEnabled = true;
            setDirection.Location = new Point(125, 130);
            setDirection.Name = "setDirection";
            setDirection.Size = new Size(73, 29);
            setDirection.TabIndex = 5;
            setDirection.Tag = "用紙の向きを指定します。";
            setDirection.MouseEnter += Menu_MouseEnter;
            setDirection.MouseLeave += Menu_MouseLeave;
            // 
            // setDpi
            // 
            setDpi.DropDownStyle = ComboBoxStyle.DropDownList;
            setDpi.FormattingEnabled = true;
            setDpi.Location = new Point(125, 58);
            setDpi.Name = "setDpi";
            setDpi.Size = new Size(81, 29);
            setDpi.TabIndex = 3;
            setDpi.Tag = "解像度を指定します。";
            setDpi.MouseEnter += Menu_MouseEnter;
            setDpi.MouseLeave += Menu_MouseLeave;
            // 
            // setPaper
            // 
            setPaper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setPaper.DropDownStyle = ComboBoxStyle.DropDownList;
            setPaper.FormattingEnabled = true;
            setPaper.Location = new Point(125, 94);
            setPaper.Name = "setPaper";
            setPaper.Size = new Size(154, 29);
            setPaper.TabIndex = 4;
            setPaper.Tag = "用紙サイズを指定します。";
            setPaper.MouseEnter += Menu_MouseEnter;
            setPaper.MouseLeave += Menu_MouseLeave;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 97);
            label7.Name = "label7";
            label7.Size = new Size(94, 21);
            label7.TabIndex = 15;
            label7.Text = "用紙サイズ：";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(setFileName);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(setFont);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(setFontSize);
            groupBox3.Location = new Point(10, 403);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(290, 136);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "ファイル名";
            // 
            // setFileName
            // 
            setFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setFileName.DropDownStyle = ComboBoxStyle.DropDownList;
            setFileName.FormattingEnabled = true;
            setFileName.Location = new Point(125, 22);
            setFileName.Name = "setFileName";
            setFileName.Size = new Size(158, 29);
            setFileName.TabIndex = 10;
            setFileName.Tag = "ファイル名の表示を指定します。";
            setFileName.MouseEnter += Menu_MouseEnter;
            setFileName.MouseLeave += Menu_MouseLeave;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 61);
            label5.Name = "label5";
            label5.Size = new Size(71, 21);
            label5.TabIndex = 12;
            label5.Text = "フォント：";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(10, 25);
            label9.Name = "label9";
            label9.Size = new Size(87, 21);
            label9.TabIndex = 19;
            label9.Text = "ファイル名：";
            // 
            // setFont
            // 
            setFont.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setFont.DropDownStyle = ComboBoxStyle.DropDownList;
            setFont.FormattingEnabled = true;
            setFont.Location = new Point(125, 58);
            setFont.Name = "setFont";
            setFont.Size = new Size(158, 29);
            setFont.TabIndex = 11;
            setFont.Tag = "フォントを指定します。";
            setFont.MouseEnter += Menu_MouseEnter;
            setFont.MouseLeave += Menu_MouseLeave;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 96);
            label6.Name = "label6";
            label6.Size = new Size(107, 21);
            label6.TabIndex = 14;
            label6.Text = "フォントサイズ：";
            // 
            // setFontSize
            // 
            setFontSize.Location = new Point(125, 94);
            setFontSize.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            setFontSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            setFontSize.Name = "setFontSize";
            setFontSize.Size = new Size(50, 29);
            setFontSize.TabIndex = 12;
            setFontSize.Tag = "フォントサイズを指定します。(1～16)";
            setFontSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            setFontSize.Enter += NumericUpDown_Enter;
            setFontSize.MouseClick += NumericUpDown_MouseClick;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(yoko);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tate);
            groupBox1.Location = new Point(10, 58);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(290, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "画像配置";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 25);
            label1.Name = "label1";
            label1.Size = new Size(42, 21);
            label1.TabIndex = 3;
            label1.Text = "縦：";
            // 
            // yoko
            // 
            yoko.Location = new Point(185, 23);
            yoko.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            yoko.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            yoko.Name = "yoko";
            yoko.Size = new Size(50, 29);
            yoko.TabIndex = 1;
            yoko.Tag = "横方向の配置数を指定します。(1～10)";
            yoko.Value = new decimal(new int[] { 2, 0, 0, 0 });
            yoko.Enter += NumericUpDown_Enter;
            yoko.MouseClick += NumericUpDown_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(133, 25);
            label2.Name = "label2";
            label2.Size = new Size(42, 21);
            label2.TabIndex = 4;
            label2.Text = "横：";
            // 
            // tate
            // 
            tate.Location = new Point(61, 23);
            tate.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tate.Name = "tate";
            tate.Size = new Size(50, 29);
            tate.TabIndex = 0;
            tate.Tag = "縦方向の配置数を指定します。(1～10)";
            tate.Value = new decimal(new int[] { 4, 0, 0, 0 });
            tate.Enter += NumericUpDown_Enter;
            tate.MouseClick += NumericUpDown_MouseClick;
            // 
            // btnHaichi
            // 
            btnHaichi.Location = new Point(11, 12);
            btnHaichi.Name = "btnHaichi";
            btnHaichi.Size = new Size(120, 40);
            btnHaichi.TabIndex = 13;
            btnHaichi.Tag = "設定を反映し印刷プレビューを表示します。";
            btnHaichi.Text = "プレビュー";
            btnHaichi.UseVisualStyleBackColor = true;
            btnHaichi.Click += btnPreview_Click;
            btnHaichi.MouseEnter += Menu_MouseEnter;
            btnHaichi.MouseLeave += Menu_MouseLeave;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 756);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(837, 26);
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
            previewControl.Location = new Point(18, 228);
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
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Cursor = Cursors.Default;
            splitContainer1.Size = new Size(669, 678);
            splitContainer1.SplitterDistance = 367;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 11;
            // 
            // panel1
            // 
            panel1.Controls.Add(Pagelabel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(294, 30);
            panel1.TabIndex = 11;
            // 
            // Pagelabel
            // 
            Pagelabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Pagelabel.Location = new Point(3, 5);
            Pagelabel.Name = "Pagelabel";
            Pagelabel.Size = new Size(288, 21);
            Pagelabel.TabIndex = 2;
            Pagelabel.Text = "ページ";
            Pagelabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(splitContainer1);
            toolStripContainer1.ContentPanel.Size = new Size(745, 713);
            toolStripContainer1.Location = new Point(6, 12);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(745, 741);
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
            toolStrip1.Size = new Size(501, 28);
            toolStrip1.TabIndex = 0;
            // 
            // btnPrint
            // 
            btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnPrint.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(74, 25);
            btnPrint.Tag = "印刷します。";
            btnPrint.Text = "印刷(&P)...";
            btnPrint.ToolTipText = "印刷します。";
            btnPrint.Click += btnPrint_Click;
            btnPrint.MouseEnter += Menu_MouseEnter;
            btnPrint.MouseLeave += Menu_MouseLeave;
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
            btnPrintSet.Size = new Size(121, 25);
            btnPrintSet.Tag = "プリンタ設定を行います。";
            btnPrintSet.Text = "プリンタ設定(&R)...";
            btnPrintSet.ToolTipText = "プリンタ設定を行います。";
            btnPrintSet.Click += btnPrintSet_Click;
            btnPrintSet.MouseEnter += Menu_MouseEnter;
            btnPrintSet.MouseLeave += Menu_MouseLeave;
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
            btnPageSet.MouseEnter += Menu_MouseLeave;
            btnPageSet.MouseLeave += Menu_MouseLeave;
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
            btnPrev.MouseEnter += Menu_MouseEnter;
            btnPrev.MouseLeave += Menu_MouseLeave;
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
            btnNext.MouseEnter += Menu_MouseEnter;
            btnNext.MouseLeave += Menu_MouseLeave;
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
            btnClose.MouseEnter += Menu_MouseEnter;
            btnClose.MouseLeave += Menu_MouseLeave;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(837, 782);
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
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)setFontSize).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)yoko).EndInit();
            ((System.ComponentModel.ISupportInitialize)tate).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
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
        private StatusStrip statusStrip1;
        private PrintPreviewControl previewControl;
        private NumericUpDown yoko;
        private NumericUpDown tate;
        private Label label2;
        private Label label1;
        private SplitContainer splitContainer1;
        private Button btnHaichi;
        private GroupBox groupBox1;
        private ComboBox setDpi;
        private Label label4;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolTip toolTip1;
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
        private Label label9;
        private ComboBox setFileName;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ComboBox setPrinterName;
        private Label label3;
        private Label label10;
        private Label label13;
        private Label label12;
        private Label label11;
        private TextBox setMarginLeft;
        private TextBox setMarginTop;
        private TextBox setMarginRight;
        private TextBox setMarginBottom;
        private Panel panel1;
        private Label Pagelabel;
    }
}