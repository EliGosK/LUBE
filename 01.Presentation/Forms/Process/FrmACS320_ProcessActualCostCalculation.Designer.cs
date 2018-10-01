namespace Presentation.Forms.Process
{
    partial class FrmACS320_ProcessActualCostCalculation
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
            this.grpParameter = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cboMonth = new EAP.Framework.Windows.Controls.EAPComboBox();
            this.txtYear = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtEndLiterSum = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSoldLiterSum = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEndLiterOEM = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSoldLiterOEM = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEndLiter = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoldLiter = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRecal = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtActualMOHRate = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.txtActualCapacity = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtActualMOH = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bgWorkerProcess = new System.ComponentModel.BackgroundWorker();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpParameter.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpOutput);
            this.pnlContainer.Controls.Add(this.grpParameter);
            this.pnlContainer.Size = new System.Drawing.Size(840, 462);
            // 
            // grpParameter
            // 
            this.grpParameter.Controls.Add(this.progressBar);
            this.grpParameter.Controls.Add(this.btnProcess);
            this.grpParameter.Controls.Add(this.btnClear);
            this.grpParameter.Controls.Add(this.cboMonth);
            this.grpParameter.Controls.Add(this.txtYear);
            this.grpParameter.Controls.Add(this.label2);
            this.grpParameter.Controls.Add(this.label1);
            this.grpParameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpParameter.Location = new System.Drawing.Point(12, 15);
            this.grpParameter.Name = "grpParameter";
            this.grpParameter.Size = new System.Drawing.Size(803, 177);
            this.grpParameter.TabIndex = 1;
            this.grpParameter.TabStop = false;
            this.grpParameter.Text = "Parameter";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(110, 93);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(657, 23);
            this.progressBar.TabIndex = 8;
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(664, 131);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 30);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "&Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(548, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(93, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Location = new System.Drawing.Point(262, 52);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(138, 26);
            this.cboMonth.TabIndex = 2;
            // 
            // txtYear
            // 
            this.txtYear.AddDecimalAfterMaxWholeDigits = false;
            this.txtYear.AllowNegative = false;
            this.txtYear.DecimalPoint = '\0';
            this.txtYear.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtYear.DigitsInGroup = 0;
            this.txtYear.DoubleValue = 2017D;
            this.txtYear.Flags = 65536;
            this.txtYear.GroupSeparator = ',';
            this.txtYear.IntValue = 2017;
            this.txtYear.Location = new System.Drawing.Point(110, 52);
            this.txtYear.LongValue = ((long)(2017));
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
            this.txtYear.Text = "2017";
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
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.label20);
            this.grpOutput.Controls.Add(this.txtEndLiterSum);
            this.grpOutput.Controls.Add(this.label21);
            this.grpOutput.Controls.Add(this.label19);
            this.grpOutput.Controls.Add(this.txtSoldLiterSum);
            this.grpOutput.Controls.Add(this.label18);
            this.grpOutput.Controls.Add(this.label17);
            this.grpOutput.Controls.Add(this.label13);
            this.grpOutput.Controls.Add(this.txtEndLiterOEM);
            this.grpOutput.Controls.Add(this.label14);
            this.grpOutput.Controls.Add(this.label15);
            this.grpOutput.Controls.Add(this.txtSoldLiterOEM);
            this.grpOutput.Controls.Add(this.label16);
            this.grpOutput.Controls.Add(this.label11);
            this.grpOutput.Controls.Add(this.txtEndLiter);
            this.grpOutput.Controls.Add(this.label12);
            this.grpOutput.Controls.Add(this.label3);
            this.grpOutput.Controls.Add(this.txtSoldLiter);
            this.grpOutput.Controls.Add(this.label10);
            this.grpOutput.Controls.Add(this.btnRecal);
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
            this.grpOutput.Location = new System.Drawing.Point(12, 198);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(803, 243);
            this.grpOutput.TabIndex = 2;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(663, 164);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 18);
            this.label20.TabIndex = 31;
            this.label20.Text = "Liter";
            // 
            // txtEndLiterSum
            // 
            this.txtEndLiterSum.AddDecimalAfterMaxWholeDigits = true;
            this.txtEndLiterSum.AllowNegative = false;
            this.txtEndLiterSum.DecimalPoint = '.';
            this.txtEndLiterSum.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtEndLiterSum.DigitsInGroup = 3;
            this.txtEndLiterSum.DoubleValue = 2017D;
            this.txtEndLiterSum.Enabled = false;
            this.txtEndLiterSum.Flags = 196608;
            this.txtEndLiterSum.GroupSeparator = ',';
            this.txtEndLiterSum.IntValue = 2017;
            this.txtEndLiterSum.Location = new System.Drawing.Point(534, 161);
            this.txtEndLiterSum.LongValue = ((long)(2017));
            this.txtEndLiterSum.MaxDecimalPlaces = 4;
            this.txtEndLiterSum.MaxLength = 20;
            this.txtEndLiterSum.MaxWholeDigits = 12;
            this.txtEndLiterSum.Name = "txtEndLiterSum";
            this.txtEndLiterSum.NegativeSign = '-';
            this.txtEndLiterSum.Prefix = "";
            this.txtEndLiterSum.RangeMax = 999999999.9999D;
            this.txtEndLiterSum.RangeMin = 0D;
            this.txtEndLiterSum.ReadOnly = true;
            this.txtEndLiterSum.Size = new System.Drawing.Size(117, 24);
            this.txtEndLiterSum.TabIndex = 30;
            this.txtEndLiterSum.Text = "2,017";
            this.txtEndLiterSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(398, 164);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(122, 18);
            this.label21.TabIndex = 29;
            this.label21.Text = "Ending Liter Total";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(338, 164);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 18);
            this.label19.TabIndex = 28;
            this.label19.Text = "Liter";
            // 
            // txtSoldLiterSum
            // 
            this.txtSoldLiterSum.AddDecimalAfterMaxWholeDigits = true;
            this.txtSoldLiterSum.AllowNegative = false;
            this.txtSoldLiterSum.DecimalPoint = '.';
            this.txtSoldLiterSum.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtSoldLiterSum.DigitsInGroup = 3;
            this.txtSoldLiterSum.DoubleValue = 2017D;
            this.txtSoldLiterSum.Enabled = false;
            this.txtSoldLiterSum.Flags = 196608;
            this.txtSoldLiterSum.GroupSeparator = ',';
            this.txtSoldLiterSum.IntValue = 2017;
            this.txtSoldLiterSum.Location = new System.Drawing.Point(209, 161);
            this.txtSoldLiterSum.LongValue = ((long)(2017));
            this.txtSoldLiterSum.MaxDecimalPlaces = 4;
            this.txtSoldLiterSum.MaxLength = 20;
            this.txtSoldLiterSum.MaxWholeDigits = 12;
            this.txtSoldLiterSum.Name = "txtSoldLiterSum";
            this.txtSoldLiterSum.NegativeSign = '-';
            this.txtSoldLiterSum.Prefix = "";
            this.txtSoldLiterSum.RangeMax = 999999999.9999D;
            this.txtSoldLiterSum.RangeMin = 0D;
            this.txtSoldLiterSum.ReadOnly = true;
            this.txtSoldLiterSum.Size = new System.Drawing.Size(117, 24);
            this.txtSoldLiterSum.TabIndex = 27;
            this.txtSoldLiterSum.Text = "2,017";
            this.txtSoldLiterSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(87, 164);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 18);
            this.label18.TabIndex = 26;
            this.label18.Text = "Sold Liter Total";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(661, 101);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(36, 18);
            this.label17.TabIndex = 25;
            this.label17.Text = "Liter";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(661, 131);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 18);
            this.label13.TabIndex = 24;
            this.label13.Text = "Liter";
            // 
            // txtEndLiterOEM
            // 
            this.txtEndLiterOEM.AddDecimalAfterMaxWholeDigits = true;
            this.txtEndLiterOEM.AllowNegative = false;
            this.txtEndLiterOEM.DecimalPoint = '.';
            this.txtEndLiterOEM.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtEndLiterOEM.DigitsInGroup = 3;
            this.txtEndLiterOEM.DoubleValue = 2017D;
            this.txtEndLiterOEM.Flags = 196608;
            this.txtEndLiterOEM.GroupSeparator = ',';
            this.txtEndLiterOEM.IntValue = 2017;
            this.txtEndLiterOEM.Location = new System.Drawing.Point(534, 128);
            this.txtEndLiterOEM.LongValue = ((long)(2017));
            this.txtEndLiterOEM.MaxDecimalPlaces = 2;
            this.txtEndLiterOEM.MaxLength = 20;
            this.txtEndLiterOEM.MaxWholeDigits = 12;
            this.txtEndLiterOEM.Name = "txtEndLiterOEM";
            this.txtEndLiterOEM.NegativeSign = '-';
            this.txtEndLiterOEM.Prefix = "";
            this.txtEndLiterOEM.RangeMax = 999999999.99D;
            this.txtEndLiterOEM.RangeMin = 0D;
            this.txtEndLiterOEM.Size = new System.Drawing.Size(117, 24);
            this.txtEndLiterOEM.TabIndex = 23;
            this.txtEndLiterOEM.Text = "2,017";
            this.txtEndLiterOEM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndLiterOEM.TextChanged += new System.EventHandler(this.txtEndLiterOEM_TextChanged);
            this.txtEndLiterOEM.Leave += new System.EventHandler(this.txtEndLiterOEM_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(393, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 18);
            this.label14.TabIndex = 22;
            this.label14.Text = "Endling Liter OEM";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(336, 132);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 18);
            this.label15.TabIndex = 21;
            this.label15.Text = "Liter";
            // 
            // txtSoldLiterOEM
            // 
            this.txtSoldLiterOEM.AddDecimalAfterMaxWholeDigits = true;
            this.txtSoldLiterOEM.AllowNegative = false;
            this.txtSoldLiterOEM.DecimalPoint = '.';
            this.txtSoldLiterOEM.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtSoldLiterOEM.DigitsInGroup = 3;
            this.txtSoldLiterOEM.DoubleValue = 2017D;
            this.txtSoldLiterOEM.Flags = 196608;
            this.txtSoldLiterOEM.GroupSeparator = ',';
            this.txtSoldLiterOEM.IntValue = 2017;
            this.txtSoldLiterOEM.Location = new System.Drawing.Point(209, 129);
            this.txtSoldLiterOEM.LongValue = ((long)(2017));
            this.txtSoldLiterOEM.MaxDecimalPlaces = 2;
            this.txtSoldLiterOEM.MaxLength = 20;
            this.txtSoldLiterOEM.MaxWholeDigits = 12;
            this.txtSoldLiterOEM.Name = "txtSoldLiterOEM";
            this.txtSoldLiterOEM.NegativeSign = '-';
            this.txtSoldLiterOEM.Prefix = "";
            this.txtSoldLiterOEM.RangeMax = 999999999.99D;
            this.txtSoldLiterOEM.RangeMin = 0D;
            this.txtSoldLiterOEM.Size = new System.Drawing.Size(117, 24);
            this.txtSoldLiterOEM.TabIndex = 20;
            this.txtSoldLiterOEM.Text = "2,017";
            this.txtSoldLiterOEM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoldLiterOEM.TextChanged += new System.EventHandler(this.txtSoldLiterOEM_TextChanged);
            this.txtSoldLiterOEM.Leave += new System.EventHandler(this.txtSoldLiterOEM_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(85, 132);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 18);
            this.label16.TabIndex = 19;
            this.label16.Text = "Sold Liter OEM";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(336, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 18);
            this.label11.TabIndex = 18;
            this.label11.Text = "Liter";
            // 
            // txtEndLiter
            // 
            this.txtEndLiter.AddDecimalAfterMaxWholeDigits = true;
            this.txtEndLiter.AllowNegative = false;
            this.txtEndLiter.DecimalPoint = '.';
            this.txtEndLiter.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtEndLiter.DigitsInGroup = 3;
            this.txtEndLiter.DoubleValue = 2017D;
            this.txtEndLiter.Flags = 196608;
            this.txtEndLiter.GroupSeparator = ',';
            this.txtEndLiter.IntValue = 2017;
            this.txtEndLiter.Location = new System.Drawing.Point(534, 98);
            this.txtEndLiter.LongValue = ((long)(2017));
            this.txtEndLiter.MaxDecimalPlaces = 2;
            this.txtEndLiter.MaxLength = 20;
            this.txtEndLiter.MaxWholeDigits = 12;
            this.txtEndLiter.Name = "txtEndLiter";
            this.txtEndLiter.NegativeSign = '-';
            this.txtEndLiter.Prefix = "";
            this.txtEndLiter.RangeMax = 999999999.99D;
            this.txtEndLiter.RangeMin = 0D;
            this.txtEndLiter.Size = new System.Drawing.Size(117, 24);
            this.txtEndLiter.TabIndex = 17;
            this.txtEndLiter.Text = "2,017";
            this.txtEndLiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndLiter.TextChanged += new System.EventHandler(this.txtEndLiter_TextChanged);
            this.txtEndLiter.Leave += new System.EventHandler(this.txtEndLiter_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(409, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 18);
            this.label12.TabIndex = 16;
            this.label12.Text = "Endling Liter LT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Liter";
            // 
            // txtSoldLiter
            // 
            this.txtSoldLiter.AddDecimalAfterMaxWholeDigits = true;
            this.txtSoldLiter.AllowNegative = false;
            this.txtSoldLiter.DecimalPoint = '.';
            this.txtSoldLiter.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtSoldLiter.DigitsInGroup = 3;
            this.txtSoldLiter.DoubleValue = 2017D;
            this.txtSoldLiter.Flags = 196608;
            this.txtSoldLiter.GroupSeparator = ',';
            this.txtSoldLiter.IntValue = 2017;
            this.txtSoldLiter.Location = new System.Drawing.Point(209, 98);
            this.txtSoldLiter.LongValue = ((long)(2017));
            this.txtSoldLiter.MaxDecimalPlaces = 2;
            this.txtSoldLiter.MaxLength = 20;
            this.txtSoldLiter.MaxWholeDigits = 12;
            this.txtSoldLiter.Name = "txtSoldLiter";
            this.txtSoldLiter.NegativeSign = '-';
            this.txtSoldLiter.Prefix = "";
            this.txtSoldLiter.RangeMax = 999999999.99D;
            this.txtSoldLiter.RangeMin = 0D;
            this.txtSoldLiter.Size = new System.Drawing.Size(117, 24);
            this.txtSoldLiter.TabIndex = 14;
            this.txtSoldLiter.Text = "2,017";
            this.txtSoldLiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSoldLiter.TextChanged += new System.EventHandler(this.txtSoldLiter_TextChanged);
            this.txtSoldLiter.Leave += new System.EventHandler(this.txtSoldLiter_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(102, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Sold Liter LT";
            // 
            // btnRecal
            // 
            this.btnRecal.Location = new System.Drawing.Point(666, 199);
            this.btnRecal.Name = "btnRecal";
            this.btnRecal.Size = new System.Drawing.Size(103, 30);
            this.btnRecal.TabIndex = 9;
            this.btnRecal.Text = "Re-Calculate";
            this.btnRecal.UseVisualStyleBackColor = true;
            this.btnRecal.Click += new System.EventHandler(this.btnRecal_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(661, 41);
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
            2017,
            0,
            0,
            0});
            this.txtActualMOHRate.DigitsInGroup = 3;
            this.txtActualMOHRate.DoubleValue = 2017D;
            this.txtActualMOHRate.Enabled = false;
            this.txtActualMOHRate.Flags = 196608;
            this.txtActualMOHRate.GroupSeparator = ',';
            this.txtActualMOHRate.IntValue = 2017;
            this.txtActualMOHRate.Location = new System.Drawing.Point(534, 38);
            this.txtActualMOHRate.LongValue = ((long)(2017));
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
            this.txtActualMOHRate.Text = "2,017";
            this.txtActualMOHRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtActualCapacity
            // 
            this.txtActualCapacity.AddDecimalAfterMaxWholeDigits = true;
            this.txtActualCapacity.AllowNegative = false;
            this.txtActualCapacity.DecimalPoint = '.';
            this.txtActualCapacity.DecimalValue = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            this.txtActualCapacity.DigitsInGroup = 3;
            this.txtActualCapacity.DoubleValue = 2017D;
            this.txtActualCapacity.Flags = 196608;
            this.txtActualCapacity.GroupSeparator = ',';
            this.txtActualCapacity.IntValue = 2017;
            this.txtActualCapacity.Location = new System.Drawing.Point(209, 68);
            this.txtActualCapacity.LongValue = ((long)(2017));
            this.txtActualCapacity.MaxDecimalPlaces = 2;
            this.txtActualCapacity.MaxLength = 20;
            this.txtActualCapacity.MaxWholeDigits = 12;
            this.txtActualCapacity.Name = "txtActualCapacity";
            this.txtActualCapacity.NegativeSign = '-';
            this.txtActualCapacity.Prefix = "";
            this.txtActualCapacity.RangeMax = 999999999.99D;
            this.txtActualCapacity.RangeMin = 0D;
            this.txtActualCapacity.Size = new System.Drawing.Size(117, 24);
            this.txtActualCapacity.TabIndex = 8;
            this.txtActualCapacity.Text = "2,017";
            this.txtActualCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(394, 41);
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
            255000000,
            0,
            0,
            131072});
            this.txtActualMOH.DigitsInGroup = 3;
            this.txtActualMOH.DoubleValue = 2550000D;
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
            this.txtActualMOH.Size = new System.Drawing.Size(117, 24);
            this.txtActualMOH.TabIndex = 4;
            this.txtActualMOH.Text = "2,550,000.00";
            this.txtActualMOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Actual MOH This Month";
            // 
            // bgWorkerProcess
            // 
            this.bgWorkerProcess.WorkerReportsProgress = true;
            this.bgWorkerProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerProcess_DoWork);
            this.bgWorkerProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerProcess_ProgressChanged);
            this.bgWorkerProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerProcess_RunWorkerCompleted);
            // 
            // FrmACS320_ProcessActualCostCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 509);
            this.Name = "FrmACS320_ProcessActualCostCalculation";
            this.Text = "ACS320 : Process Actual Cost Calculation";
            this.Load += new System.EventHandler(this.FrmACS320_ProcessActualCostCalculation_Load);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpParameter.ResumeLayout(false);
            this.grpParameter.PerformLayout();
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualMOH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpParameter;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnClear;
        private EAP.Framework.Windows.Controls.EAPComboBox cboMonth;
        private EAP.Framework.Windows.Controls.NumericTextBox txtYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualMOHRate;
        private EAP.Framework.Windows.Controls.NumericTextBox txtActualCapacity;
        private System.ComponentModel.BackgroundWorker bgWorkerProcess;
        private System.Windows.Forms.Button btnRecal;
        private System.Windows.Forms.Label label11;
        private EAP.Framework.Windows.Controls.NumericTextBox txtEndLiter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private EAP.Framework.Windows.Controls.NumericTextBox txtSoldLiter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label20;
        private EAP.Framework.Windows.Controls.NumericTextBox txtEndLiterSum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private EAP.Framework.Windows.Controls.NumericTextBox txtSoldLiterSum;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private EAP.Framework.Windows.Controls.NumericTextBox txtEndLiterOEM;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private EAP.Framework.Windows.Controls.NumericTextBox txtSoldLiterOEM;
        private System.Windows.Forms.Label label16;
    }
}