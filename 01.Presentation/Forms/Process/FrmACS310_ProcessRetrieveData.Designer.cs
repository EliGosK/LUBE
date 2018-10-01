namespace Presentation.Forms.Process
{
    partial class FrmACS310_ProcessRetrieveData
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
            this.txtYear = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericTextBox2 = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.numericTextBox1 = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cboSystem = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMonth = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.groupBox1);
            this.pnlContainer.Size = new System.Drawing.Size(777, 317);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYear);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.btnProcess);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cboSystem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboMonth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(597, 290);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameter";
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
            this.txtYear.TabIndex = 1;
            this.txtYear.Text = "0";
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericTextBox2);
            this.groupBox2.Controls.Add(this.numericTextBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(110, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 77);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.AddDecimalAfterMaxWholeDigits = false;
            this.numericTextBox2.AllowNegative = false;
            this.numericTextBox2.DecimalPoint = '\0';
            this.numericTextBox2.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericTextBox2.DigitsInGroup = 0;
            this.numericTextBox2.DoubleValue = 0D;
            this.numericTextBox2.Flags = 65536;
            this.numericTextBox2.GroupSeparator = ',';
            this.numericTextBox2.IntValue = 0;
            this.numericTextBox2.Location = new System.Drawing.Point(110, 46);
            this.numericTextBox2.LongValue = ((long)(0));
            this.numericTextBox2.MaxDecimalPlaces = 4;
            this.numericTextBox2.MaxLength = 4;
            this.numericTextBox2.MaxWholeDigits = 4;
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.NegativeSign = '-';
            this.numericTextBox2.Prefix = "";
            this.numericTextBox2.RangeMax = 9999D;
            this.numericTextBox2.RangeMin = 1900D;
            this.numericTextBox2.ReadOnly = true;
            this.numericTextBox2.Size = new System.Drawing.Size(71, 24);
            this.numericTextBox2.TabIndex = 14;
            this.numericTextBox2.Text = "0";
            this.numericTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.AddDecimalAfterMaxWholeDigits = false;
            this.numericTextBox1.AllowNegative = false;
            this.numericTextBox1.DecimalPoint = '\0';
            this.numericTextBox1.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericTextBox1.DigitsInGroup = 0;
            this.numericTextBox1.DoubleValue = 0D;
            this.numericTextBox1.Flags = 65536;
            this.numericTextBox1.GroupSeparator = ',';
            this.numericTextBox1.IntValue = 0;
            this.numericTextBox1.Location = new System.Drawing.Point(110, 15);
            this.numericTextBox1.LongValue = ((long)(0));
            this.numericTextBox1.MaxDecimalPlaces = 4;
            this.numericTextBox1.MaxLength = 4;
            this.numericTextBox1.MaxWholeDigits = 4;
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.NegativeSign = '-';
            this.numericTextBox1.Prefix = "";
            this.numericTextBox1.RangeMax = 9999D;
            this.numericTextBox1.RangeMin = 1900D;
            this.numericTextBox1.ReadOnly = true;
            this.numericTextBox1.Size = new System.Drawing.Size(71, 24);
            this.numericTextBox1.TabIndex = 13;
            this.numericTextBox1.Text = "0";
            this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Financial";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Production";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Revision";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(110, 211);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(448, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 8;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(455, 246);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 30);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(339, 246);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboSystem
            // 
            this.cboSystem.FormattingEnabled = true;
            this.cboSystem.Location = new System.Drawing.Point(110, 89);
            this.cboSystem.Name = "cboSystem";
            this.cboSystem.Size = new System.Drawing.Size(290, 26);
            this.cboSystem.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "System";
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(262, 52);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(138, 26);
            this.cboMonth.TabIndex = 2;
            this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
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
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // FrmACS310_ProcessRetrieveData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 364);
            this.Name = "FrmACS310_ProcessRetrieveData";
            this.Text = "ACS310 : Process Retrieve Data";
            this.Load += new System.EventHandler(this.FrmACS310_ProcessRetrieveData_Load);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private EAP.Framework.Windows.Controls.EAPComboBox cboMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClear;
        private EAP.Framework.Windows.Controls.EAPComboBox cboSystem;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private EAP.Framework.Windows.Controls.NumericTextBox numericTextBox2;
        private EAP.Framework.Windows.Controls.NumericTextBox numericTextBox1;
        private EAP.Framework.Windows.Controls.NumericTextBox txtYear;
    }
}