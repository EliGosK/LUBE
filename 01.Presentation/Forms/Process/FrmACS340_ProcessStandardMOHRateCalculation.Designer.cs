namespace Presentation.Forms.Process
{
    partial class FrmACS340_ProcessStandardMOHRateCalculation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpParameter = new System.Windows.Forms.GroupBox();
            this.grdView = new System.Windows.Forms.DataGridView();
            this.grcMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grcActualMOH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grcActualCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grcActualMOHRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRetrieveData = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtYear = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtActualMOHRate = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.txtActualCapacity = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtActualMOH = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
            this.grpOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpOutput);
            this.pnlContainer.Controls.Add(this.grpParameter);
            this.pnlContainer.Size = new System.Drawing.Size(864, 593);
            // 
            // grpParameter
            // 
            this.grpParameter.Controls.Add(this.grdView);
            this.grpParameter.Controls.Add(this.btnRetrieveData);
            this.grpParameter.Controls.Add(this.progressBar);
            this.grpParameter.Controls.Add(this.btnProcess);
            this.grpParameter.Controls.Add(this.btnClear);
            this.grpParameter.Controls.Add(this.txtYear);
            this.grpParameter.Controls.Add(this.label1);
            this.grpParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpParameter.Location = new System.Drawing.Point(12, 15);
            this.grpParameter.Name = "grpParameter";
            this.grpParameter.Size = new System.Drawing.Size(769, 438);
            this.grpParameter.TabIndex = 2;
            this.grpParameter.TabStop = false;
            this.grpParameter.Text = "Parameter";
            // 
            // grdView
            // 
            this.grdView.AllowUserToAddRows = false;
            this.grdView.AllowUserToDeleteRows = false;
            this.grdView.AllowUserToResizeColumns = false;
            this.grdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdView.ColumnHeadersHeight = 28;
            this.grdView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grcMonth,
            this.grcActualMOH,
            this.grcActualCapacity,
            this.grcActualMOHRate});
            this.grdView.Location = new System.Drawing.Point(15, 78);
            this.grdView.MultiSelect = false;
            this.grdView.Name = "grdView";
            this.grdView.ReadOnly = true;
            this.grdView.RowHeadersVisible = false;
            this.grdView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdView.Size = new System.Drawing.Size(737, 313);
            this.grdView.TabIndex = 10;
            // 
            // grcMonth
            // 
            this.grcMonth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grcMonth.DefaultCellStyle = dataGridViewCellStyle2;
            this.grcMonth.HeaderText = "Month";
            this.grcMonth.Name = "grcMonth";
            this.grcMonth.ReadOnly = true;
            this.grcMonth.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // grcActualMOH
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.grcActualMOH.DefaultCellStyle = dataGridViewCellStyle3;
            this.grcActualMOH.HeaderText = "Actual MOH (THB)";
            this.grcActualMOH.Name = "grcActualMOH";
            this.grcActualMOH.ReadOnly = true;
            this.grcActualMOH.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grcActualMOH.Width = 200;
            // 
            // grcActualCapacity
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.grcActualCapacity.DefaultCellStyle = dataGridViewCellStyle4;
            this.grcActualCapacity.HeaderText = "Actual Total FG Production (Liter)";
            this.grcActualCapacity.Name = "grcActualCapacity";
            this.grcActualCapacity.ReadOnly = true;
            this.grcActualCapacity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grcActualCapacity.Width = 250;
            // 
            // grcActualMOHRate
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.grcActualMOHRate.DefaultCellStyle = dataGridViewCellStyle5;
            this.grcActualMOHRate.HeaderText = "MOH Rate (THB/Liter)";
            this.grcActualMOHRate.Name = "grcActualMOHRate";
            this.grcActualMOHRate.ReadOnly = true;
            this.grcActualMOHRate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.grcActualMOHRate.Width = 170;
            // 
            // btnRetrieveData
            // 
            this.btnRetrieveData.Location = new System.Drawing.Point(620, 35);
            this.btnRetrieveData.Name = "btnRetrieveData";
            this.btnRetrieveData.Size = new System.Drawing.Size(132, 30);
            this.btnRetrieveData.TabIndex = 2;
            this.btnRetrieveData.Text = "Retrieve Data";
            this.btnRetrieveData.UseVisualStyleBackColor = true;
            this.btnRetrieveData.Click += new System.EventHandler(this.btnRetrieveData_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 403);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(618, 23);
            this.progressBar.TabIndex = 8;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(649, 399);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 30);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(511, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 30);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
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
            this.txtYear.Location = new System.Drawing.Point(75, 38);
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
            this.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year";
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.label9);
            this.grpOutput.Controls.Add(this.label8);
            this.grpOutput.Controls.Add(this.label7);
            this.grpOutput.Controls.Add(this.txtActualMOHRate);
            this.grpOutput.Controls.Add(this.txtActualCapacity);
            this.grpOutput.Controls.Add(this.label6);
            this.grpOutput.Controls.Add(this.label5);
            this.grpOutput.Controls.Add(this.txtActualMOH);
            this.grpOutput.Controls.Add(this.label4);
            this.grpOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpOutput.Location = new System.Drawing.Point(12, 454);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(769, 122);
            this.grpOutput.TabIndex = 3;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(676, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 18);
            this.label9.TabIndex = 12;
            this.label9.Text = "THB / Liter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(336, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "Liter";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "THB";
            // 
            // txtActualMOHRate
            // 
            this.txtActualMOHRate.AddDecimalAfterMaxWholeDigits = true;
            this.txtActualMOHRate.AllowNegative = false;
            this.txtActualMOHRate.DecimalPoint = '.';
            this.txtActualMOHRate.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtActualMOHRate.DigitsInGroup = 3;
            this.txtActualMOHRate.DoubleValue = 0D;
            this.txtActualMOHRate.Flags = 196608;
            this.txtActualMOHRate.GroupSeparator = ',';
            this.txtActualMOHRate.IntValue = 0;
            this.txtActualMOHRate.Location = new System.Drawing.Point(549, 68);
            this.txtActualMOHRate.LongValue = ((long)(0));
            this.txtActualMOHRate.MaxDecimalPlaces = 4;
            this.txtActualMOHRate.MaxLength = 20;
            this.txtActualMOHRate.MaxWholeDigits = 12;
            this.txtActualMOHRate.Name = "txtActualMOHRate";
            this.txtActualMOHRate.NegativeSign = '-';
            this.txtActualMOHRate.Prefix = "";
            this.txtActualMOHRate.RangeMax = 999999999.9999D;
            this.txtActualMOHRate.RangeMin = 0D;
            this.txtActualMOHRate.ReadOnly = true;
            this.txtActualMOHRate.Size = new System.Drawing.Size(117, 24);
            this.txtActualMOHRate.TabIndex = 9;
            this.txtActualMOHRate.Text = "0";
            this.txtActualMOHRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActualCapacity
            // 
            this.txtActualCapacity.AddDecimalAfterMaxWholeDigits = true;
            this.txtActualCapacity.AllowNegative = false;
            this.txtActualCapacity.DecimalPoint = '.';
            this.txtActualCapacity.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtActualCapacity.DigitsInGroup = 3;
            this.txtActualCapacity.DoubleValue = 0D;
            this.txtActualCapacity.Flags = 196608;
            this.txtActualCapacity.GroupSeparator = ',';
            this.txtActualCapacity.IntValue = 0;
            this.txtActualCapacity.Location = new System.Drawing.Point(209, 68);
            this.txtActualCapacity.LongValue = ((long)(0));
            this.txtActualCapacity.MaxDecimalPlaces = 2;
            this.txtActualCapacity.MaxLength = 20;
            this.txtActualCapacity.MaxWholeDigits = 12;
            this.txtActualCapacity.Name = "txtActualCapacity";
            this.txtActualCapacity.NegativeSign = '-';
            this.txtActualCapacity.Prefix = "";
            this.txtActualCapacity.RangeMax = 999999999.99D;
            this.txtActualCapacity.RangeMin = 0D;
            this.txtActualCapacity.ReadOnly = true;
            this.txtActualCapacity.Size = new System.Drawing.Size(117, 24);
            this.txtActualCapacity.TabIndex = 8;
            this.txtActualCapacity.Text = "0";
            this.txtActualCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(409, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Actual MOH Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total FG Production";
            // 
            // txtActualMOH
            // 
            this.txtActualMOH.AddDecimalAfterMaxWholeDigits = true;
            this.txtActualMOH.AllowNegative = false;
            this.txtActualMOH.DecimalPoint = '.';
            this.txtActualMOH.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtActualMOH.DigitsInGroup = 3;
            this.txtActualMOH.DoubleValue = 0D;
            this.txtActualMOH.Flags = 196608;
            this.txtActualMOH.GroupSeparator = ',';
            this.txtActualMOH.IntValue = 0;
            this.txtActualMOH.Location = new System.Drawing.Point(209, 38);
            this.txtActualMOH.LongValue = ((long)(0));
            this.txtActualMOH.MaxDecimalPlaces = 2;
            this.txtActualMOH.MaxLength = 20;
            this.txtActualMOH.MaxWholeDigits = 12;
            this.txtActualMOH.Name = "txtActualMOH";
            this.txtActualMOH.NegativeSign = '-';
            this.txtActualMOH.Prefix = "";
            this.txtActualMOH.RangeMax = 999999999.99D;
            this.txtActualMOH.RangeMin = 0D;
            this.txtActualMOH.ReadOnly = true;
            this.txtActualMOH.Size = new System.Drawing.Size(117, 24);
            this.txtActualMOH.TabIndex = 4;
            this.txtActualMOH.Text = "0";
            this.txtActualMOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Actual MOH This Year";
            // 
            // FrmACS340_ProcessStandardMOHRateCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 640);
            this.Name = "FrmACS340_ProcessStandardMOHRateCalculation";
            this.Text = "ACS340 : Process Standard MOH Rate Calculation (Yearly)";
            this.Load += new System.EventHandler(this.FrmACS340_ProcessStandardMOHRateCalculation_Load);
            this.Shown += new System.EventHandler(this.FrmACS340_ProcessStandardMOHRateCalculation_Shown);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpParameter.ResumeLayout(false);
            this.grpParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpParameter;
        private System.Windows.Forms.Button btnRetrieveData;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClear;
        private EAP.Framework.Windows.Controls.NumericTextBox txtYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualMOHRate;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualCapacity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualMOH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdView;
        private System.Windows.Forms.DataGridViewTextBoxColumn grcMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn grcActualMOH;
        private System.Windows.Forms.DataGridViewTextBoxColumn grcActualCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn grcActualMOHRate;
    }
}