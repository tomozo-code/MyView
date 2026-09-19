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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            panel2 = new Panel();
            groupBox1 = new GroupBox();
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
            previewControl = new PrintPreviewControl();
            splitContainer1 = new SplitContainer();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)yoko).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
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
            panel2.Size = new Size(375, 593);
            panel2.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnHaichi);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(yoko);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(tate);
            groupBox1.Location = new Point(15, 63);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(171, 147);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "配置";
            // 
            // btnHaichi
            // 
            btnHaichi.Location = new Point(20, 99);
            btnHaichi.Name = "btnHaichi";
            btnHaichi.Size = new Size(120, 40);
            btnHaichi.TabIndex = 8;
            btnHaichi.Text = "配置反映";
            btnHaichi.UseVisualStyleBackColor = true;
            btnHaichi.Click += btnHaichi_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 29);
            label1.Name = "label1";
            label1.Size = new Size(42, 21);
            label1.TabIndex = 3;
            label1.Text = "縦：";
            // 
            // yoko
            // 
            yoko.Location = new Point(68, 64);
            yoko.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            yoko.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            yoko.Name = "yoko";
            yoko.Size = new Size(70, 29);
            yoko.TabIndex = 6;
            yoko.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 66);
            label2.Name = "label2";
            label2.Size = new Size(42, 21);
            label2.TabIndex = 4;
            label2.Text = "横：";
            // 
            // tate
            // 
            tate.Location = new Point(68, 27);
            tate.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            tate.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tate.Name = "tate";
            tate.Size = new Size(70, 29);
            tate.TabIndex = 5;
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
            btnPageSet.Location = new Point(15, 310);
            btnPageSet.Name = "btnPageSet";
            btnPageSet.Size = new Size(120, 40);
            btnPageSet.TabIndex = 1;
            btnPageSet.Text = "ページ設定...";
            btnPageSet.UseVisualStyleBackColor = true;
            btnPageSet.Click += btnPageSet_Click;
            // 
            // btnPrintSet
            // 
            btnPrintSet.Location = new Point(15, 264);
            btnPrintSet.Name = "btnPrintSet";
            btnPrintSet.Size = new Size(120, 40);
            btnPrintSet.TabIndex = 1;
            btnPrintSet.Text = "プリンタ設定...";
            btnPrintSet.UseVisualStyleBackColor = true;
            btnPrintSet.Click += btnPrintSet_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(15, 403);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(120, 40);
            btnNext.TabIndex = 1;
            btnNext.Text = "次のページ";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(15, 356);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(120, 40);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "前のページ";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(15, 477);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 40);
            btnClose.TabIndex = 1;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(15, 218);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(120, 40);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "印刷";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            statusStrip1.Location = new Point(0, 689);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(895, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // previewControl
            // 
            previewControl.AutoZoom = false;
            previewControl.Location = new Point(77, 66);
            previewControl.Name = "previewControl";
            previewControl.Size = new Size(104, 93);
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
            splitContainer1.Size = new Size(821, 647);
            splitContainer1.SplitterDistance = 451;
            splitContainer1.SplitterWidth = 8;
            splitContainer1.TabIndex = 11;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(895, 711);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form4";
            StartPosition = FormStartPosition.CenterParent;
            Text = "インデックス印刷";
            Load += Form4_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)yoko).EndInit();
            ((System.ComponentModel.ISupportInitialize)tate).EndInit();
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
    }
}