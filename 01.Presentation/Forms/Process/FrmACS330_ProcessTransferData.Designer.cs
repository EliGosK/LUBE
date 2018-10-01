namespace Presentation.Forms.Process
{
    partial class FrmACS330_ProcessTransferData
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
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
            this.pnlContainer.Size = new System.Drawing.Size(731, 367);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btnProcess);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cboMonth);
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 183);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(110, 90);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(448, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(455, 131);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 30);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(339, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(262, 52);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(138, 26);
            this.cboMonth.TabIndex = 3;
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
            this.txtYear.Location = new System.Drawing.Point(110, 52);
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
            this.txtYear.TabIndex = 2;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year";
            // 
            // FrmACS330_ProcessTransferData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 414);
            this.Name = "FrmACS330_ProcessTransferData";
            this.Text = "ACS330 : Process Transfer Data";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClear;
        private EAP.Framework.Windows.Controls.EAPComboBox cboMonth;
        private EAP.Framework.Windows.Controls.NumericTextBox txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}