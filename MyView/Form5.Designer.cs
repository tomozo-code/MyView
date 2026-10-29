namespace MyView
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            previewControl = new PrintPreviewControl();
            splitContainer1 = new SplitContainer();
            panel2 = new Panel();
            btnHaichi = new Button();
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
            setDirection = new ComboBox();
            setPaper = new ComboBox();
            label7 = new Label();
            toolStripContainer1 = new ToolStripContainer();
            toolStrip1 = new ToolStrip();
            btnPrint = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            closeBtn = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            optionBtn = new ToolStripDropDownButton();
            iconLabelBtn = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // previewControl
            // 
            previewControl.Location = new Point(15, 183);
            previewControl.Name = "previewControl";
            previewControl.Size = new Size(112, 111);
            previewControl.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Cursor = Cursors.SizeWE;
            splitContainer1.Location = new Point(27, 13);
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
            splitContainer1.Size = new Size(562, 407);
            splitContainer1.SplitterDistance = 406;
            splitContainer1.SplitterWidth = 9;
            splitContainer1.TabIndex = 1;
            splitContainer1.TabStop = false;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(btnHaichi);
            panel2.Controls.Add(groupBox4);
            panel2.Controls.Add(groupBox2);
            panel2.Location = new Point(18, 9);
            panel2.Name = "panel2";
            panel2.Size = new Size(375, 379);
            panel2.TabIndex = 9;
            // 
            // btnHaichi
            // 
            btnHaichi.Location = new Point(12, 17);
            btnHaichi.Name = "btnHaichi";
            btnHaichi.Size = new Size(135, 44);
            btnHaichi.TabIndex = 9;
            btnHaichi.Tag = "設定を反映し印刷プレビューを表示します。";
            btnHaichi.Text = "プレビュー";
            btnHaichi.UseVisualStyleBackColor = true;
            btnHaichi.Click += btnHaichi_Click;
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
            groupBox4.Location = new Point(11, 220);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(360, 105);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "余白(mm)";
            // 
            // setMarginBottom
            // 
            setMarginBottom.ImeMode = ImeMode.Disable;
            setMarginBottom.Location = new Point(198, 64);
            setMarginBottom.Name = "setMarginBottom";
            setMarginBottom.ShortcutsEnabled = false;
            setMarginBottom.Size = new Size(67, 29);
            setMarginBottom.TabIndex = 8;
            setMarginBottom.Tag = "下余白を設定します。";
            setMarginBottom.Click += setMargin_Click;
            setMarginBottom.Enter += setMargin_Enter;
            setMarginBottom.KeyDown += setMargin_KeyDown;
            setMarginBottom.KeyPress += setMargin_KeyPress;
            setMarginBottom.Validating += setMargin_Validating;
            // 
            // setMarginTop
            // 
            setMarginTop.ImeMode = ImeMode.Disable;
            setMarginTop.Location = new Point(59, 64);
            setMarginTop.Name = "setMarginTop";
            setMarginTop.ShortcutsEnabled = false;
            setMarginTop.Size = new Size(67, 29);
            setMarginTop.TabIndex = 7;
            setMarginTop.Tag = "上余白を設定します。";
            setMarginTop.Click += setMargin_Click;
            setMarginTop.Enter += setMargin_Enter;
            setMarginTop.KeyDown += setMargin_KeyDown;
            setMarginTop.KeyPress += setMargin_KeyPress;
            setMarginTop.Validating += setMargin_Validating;
            // 
            // setMarginRight
            // 
            setMarginRight.ImeMode = ImeMode.Disable;
            setMarginRight.Location = new Point(198, 24);
            setMarginRight.Name = "setMarginRight";
            setMarginRight.ShortcutsEnabled = false;
            setMarginRight.Size = new Size(67, 29);
            setMarginRight.TabIndex = 6;
            setMarginRight.Tag = "右余白を設定します。";
            setMarginRight.Click += setMargin_Click;
            setMarginRight.Enter += setMargin_Enter;
            setMarginRight.KeyDown += setMargin_KeyDown;
            setMarginRight.KeyPress += setMargin_KeyPress;
            setMarginRight.Validating += setMargin_Validating;
            // 
            // setMarginLeft
            // 
            setMarginLeft.ImeMode = ImeMode.Disable;
            setMarginLeft.Location = new Point(59, 24);
            setMarginLeft.Name = "setMarginLeft";
            setMarginLeft.ShortcutsEnabled = false;
            setMarginLeft.Size = new Size(67, 29);
            setMarginLeft.TabIndex = 5;
            setMarginLeft.Tag = "左余白を設定します。";
            setMarginLeft.Click += setMargin_Click;
            setMarginLeft.Enter += setMargin_Enter;
            setMarginLeft.KeyDown += setMargin_KeyDown;
            setMarginLeft.KeyPress += setMargin_KeyPress;
            setMarginLeft.Validating += setMargin_Validating;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(150, 67);
            label13.Name = "label13";
            label13.Size = new Size(42, 21);
            label13.TabIndex = 3;
            label13.Text = "下：";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 67);
            label12.Name = "label12";
            label12.Size = new Size(42, 21);
            label12.TabIndex = 2;
            label12.Text = "上：";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(150, 28);
            label11.Name = "label11";
            label11.Size = new Size(42, 21);
            label11.TabIndex = 1;
            label11.Text = "右：";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 28);
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
            groupBox2.Controls.Add(setDirection);
            groupBox2.Controls.Add(setPaper);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(11, 67);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(360, 143);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "用紙";
            // 
            // setPrinterName
            // 
            setPrinterName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            setPrinterName.FormattingEnabled = true;
            setPrinterName.Location = new Point(113, 24);
            setPrinterName.Name = "setPrinterName";
            setPrinterName.Size = new Size(241, 29);
            setPrinterName.TabIndex = 2;
            setPrinterName.Tag = "プリンタを変更します。変更後プレビューが更新されます。";
            setPrinterName.SelectedIndexChanged += setPrinterName_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 28);
            label3.Name = "label3";
            label3.Size = new Size(88, 21);
            label3.TabIndex = 19;
            label3.Text = "プリンタ名：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(11, 107);
            label8.Name = "label8";
            label8.Size = new Size(99, 21);
            label8.TabIndex = 18;
            label8.Text = "用紙の向き：";
            // 
            // setDirection
            // 
            setDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            setDirection.FormattingEnabled = true;
            setDirection.Location = new Point(113, 104);
            setDirection.Name = "setDirection";
            setDirection.Size = new Size(82, 29);
            setDirection.TabIndex = 4;
            setDirection.Tag = "用紙の向きを指定します。";
            setDirection.SelectedIndexChanged += setDirection_SelectedIndexChanged;
            // 
            // setPaper
            // 
            setPaper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setPaper.DropDownStyle = ComboBoxStyle.DropDownList;
            setPaper.FormattingEnabled = true;
            setPaper.Location = new Point(113, 64);
            setPaper.Name = "setPaper";
            setPaper.Size = new Size(241, 29);
            setPaper.TabIndex = 3;
            setPaper.Tag = "用紙サイズを指定します。";
            setPaper.SelectedIndexChanged += setPaper_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 67);
            label7.Name = "label7";
            label7.Size = new Size(94, 21);
            label7.TabIndex = 15;
            label7.Text = "用紙サイズ：";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(splitContainer1);
            toolStripContainer1.ContentPanel.Size = new Size(771, 596);
            toolStripContainer1.Location = new Point(12, 12);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(771, 635);
            toolStripContainer1.TabIndex = 2;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
            // 
            // toolStrip1
            // 
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnPrint, toolStripSeparator1, closeBtn, toolStripSeparator2, optionBtn });
            toolStrip1.Location = new Point(3, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(141, 39);
            toolStrip1.TabIndex = 0;
            // 
            // btnPrint
            // 
            btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnPrint.Image = (Image)resources.GetObject("btnPrint.Image");
            btnPrint.ImageScaling = ToolStripItemImageScaling.None;
            btnPrint.ImageTransparentColor = Color.Magenta;
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(36, 36);
            btnPrint.Text = "印刷(&P)...";
            btnPrint.ToolTipText = "印刷します。";
            btnPrint.Click += btnPrint_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 39);
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
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 39);
            // 
            // optionBtn
            // 
            optionBtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            optionBtn.DropDownItems.AddRange(new ToolStripItem[] { iconLabelBtn });
            optionBtn.Image = (Image)resources.GetObject("optionBtn.Image");
            optionBtn.ImageScaling = ToolStripItemImageScaling.None;
            optionBtn.ImageTransparentColor = Color.Magenta;
            optionBtn.Name = "optionBtn";
            optionBtn.Size = new Size(45, 36);
            optionBtn.Text = "オプション";
            optionBtn.ToolTipText = "オプション";
            // 
            // iconLabelBtn
            // 
            iconLabelBtn.Name = "iconLabelBtn";
            iconLabelBtn.Size = new Size(232, 26);
            iconLabelBtn.Text = "アイコンラベルを表示する";
            iconLabelBtn.ToolTipText = "アイコンラベルの表示・非表示を切り替えます。";
            iconLabelBtn.Click += iconLabelBtn_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 756);
            Controls.Add(toolStripContainer1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form5";
            StartPosition = FormStartPosition.CenterParent;
            Text = "画像印刷";
            FormClosing += Form5_FormClosing;
            Load += Form5_Load;
            KeyDown += Form5_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PrintPreviewControl previewControl;
        private SplitContainer splitContainer1;
        private Panel panel2;
        private GroupBox groupBox4;
        private TextBox setMarginBottom;
        private TextBox setMarginTop;
        private TextBox setMarginRight;
        private TextBox setMarginLeft;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private GroupBox groupBox2;
        private ComboBox setPrinterName;
        private Label label3;
        private Label label8;
        private ComboBox setDirection;
        private ComboBox setPaper;
        private Label label7;
        private Button btnHaichi;
        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        private ToolStripButton btnPrint;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton closeBtn;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripDropDownButton optionBtn;
        private ToolStripMenuItem iconLabelBtn;
    }
}