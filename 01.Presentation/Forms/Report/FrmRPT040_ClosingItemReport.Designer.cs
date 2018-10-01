namespace Presentation.Forms.Report
{
    partial class FrmRPT040_ClosingItemReport
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.itmCodeTo = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.itmCodeFrom = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.cbItmGrpTo = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.cbItmGrpFrom = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.cboMonth = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.txtYear = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.groupBox1);
            this.pnlContainer.Size = new System.Drawing.Size(747, 352);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.itmCodeTo);
            this.groupBox1.Controls.Add(this.itmCodeFrom);
            this.groupBox1.Controls.Add(this.cbItmGrpTo);
            this.groupBox1.Controls.Add(this.cbItmGrpFrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnPrintPreview);
            this.groupBox1.Controls.Add(this.cboMonth);
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 296);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // itmCodeTo
            // 
            this.itmCodeTo.FormattingEnabled = true;
            this.itmCodeTo.Location = new System.Drawing.Point(160, 199);
            this.itmCodeTo.Name = "itmCodeTo";
            this.itmCodeTo.Size = new System.Drawing.Size(254, 26);
            this.itmCodeTo.TabIndex = 15;
            // 
            // itmCodeFrom
            // 
            this.itmCodeFrom.AutoSearch = false;
            this.itmCodeFrom.FormattingEnabled = true;
            this.itmCodeFrom.Location = new System.Drawing.Point(160, 162);
            this.itmCodeFrom.Name = "itmCodeFrom";
            this.itmCodeFrom.Size = new System.Drawing.Size(254, 26);
            this.itmCodeFrom.TabIndex = 14;
            // 
            // cbItmGrpTo
            // 
            this.cbItmGrpTo.FormattingEnabled = true;
            this.cbItmGrpTo.Location = new System.Drawing.Point(160, 126);
            this.cbItmGrpTo.Name = "cbItmGrpTo";
            this.cbItmGrpTo.Size = new System.Drawing.Size(254, 26);
            this.cbItmGrpTo.TabIndex = 13;
            // 
            // cbItmGrpFrom
            // 
            this.cbItmGrpFrom.FormattingEnabled = true;
            this.cbItmGrpFrom.Location = new System.Drawing.Point(161, 91);
            this.cbItmGrpFrom.Name = "cbItmGrpFrom";
            this.cbItmGrpFrom.Size = new System.Drawing.Size(253, 26);
            this.cbItmGrpFrom.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Item Code From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(121, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Item Group From";
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Location = new System.Drawing.Point(450, 244);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(123, 32);
            this.btnPrintPreview.TabIndex = 7;
            this.btnPrintPreview.Text = "Print Preview";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(297, 56);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(117, 26);
            this.cboMonth.TabIndex = 2;
            // 
            // txtYear
            // 
            this.txtYear.AddDecimalAfterMaxWholeDigits = false;
            this.txtYear.AllowNegative = false;
            this.txtYear.DecimalPoint = '\0';
            this.txtYear.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtYear.DigitsInGroup = 0;
            this.txtYear.DoubleValue = 0D;
            this.txtYear.Flags = 65536;
            this.txtYear.GroupSeparator = ',';
            this.txtYear.IntValue = 0;
            this.txtYear.Location = new System.Drawing.Point(161, 56);
            this.txtYear.LongValue = ((long)(0));
            this.txtYear.MaxDecimalPlaces = 4;
            this.txtYear.MaxLength = 4;
            this.txtYear.MaxWholeDigits = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.NegativeSign = '-';
            this.txtYear.Prefix = "";
            this.txtYear.RangeMax = 9999D;
            this.txtYear.RangeMin = 1900D;
            this.txtYear.Size = new System.Drawing.Size(71, 24);
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year";
            // 
            // FrmRPT040_ClosingItemReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 399);
            this.Name = "FrmRPT040_ClosingItemReport";
            this.Text = "RPT040 : Closing Item Report";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private EAP.Framework.Windows.Controls.EAPComboBox cboMonth;
        private EAP.Framework.Windows.Controls.NumericTextBox txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private EAP.Framework.Windows.Controls.EAPComboBox cbItmGrpTo;
        private EAP.Framework.Windows.Controls.EAPComboBox cbItmGrpFrom;
        private EAP.Framework.Windows.Controls.EAPComboBox itmCodeTo;
        private EAP.Framework.Windows.Controls.EAPComboBox itmCodeFrom;
    }
}