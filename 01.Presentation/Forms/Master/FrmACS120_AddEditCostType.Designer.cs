namespace Presentation.Forms.Master
{
    partial class FrmACS120_AddEditCostType
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
            this.lblAccountCode = new System.Windows.Forms.Label();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.lblCostType = new System.Windows.Forms.Label();
            this.lblAllocation = new System.Windows.Forms.Label();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbCostType = new System.Windows.Forms.ComboBox();
            this.txtAllocation = new EAP.Framework.Windows.Controls.NumericTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAccountCode
            // 
            this.lblAccountCode.AutoSize = true;
            this.lblAccountCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAccountCode.Location = new System.Drawing.Point(37, 49);
            this.lblAccountCode.Name = "lblAccountCode";
            this.lblAccountCode.Size = new System.Drawing.Size(102, 18);
            this.lblAccountCode.TabIndex = 0;
            this.lblAccountCode.Text = "Account Code";
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAccountName.Location = new System.Drawing.Point(37, 89);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(106, 18);
            this.lblAccountName.TabIndex = 1;
            this.lblAccountName.Text = "Account Name";
            // 
            // lblCostType
            // 
            this.lblCostType.AutoSize = true;
            this.lblCostType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCostType.Location = new System.Drawing.Point(37, 127);
            this.lblCostType.Name = "lblCostType";
            this.lblCostType.Size = new System.Drawing.Size(76, 18);
            this.lblCostType.TabIndex = 2;
            this.lblCostType.Text = "Cost Type";
            // 
            // lblAllocation
            // 
            this.lblAllocation.AutoSize = true;
            this.lblAllocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblAllocation.Location = new System.Drawing.Point(37, 166);
            this.lblAllocation.Name = "lblAllocation";
            this.lblAllocation.Size = new System.Drawing.Size(89, 18);
            this.lblAllocation.TabIndex = 3;
            this.lblAllocation.Text = "% Allocation";
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAccountCode.Location = new System.Drawing.Point(157, 46);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.ReadOnly = true;
            this.txtAccountCode.Size = new System.Drawing.Size(125, 24);
            this.txtAccountCode.TabIndex = 1;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtAccountName.Location = new System.Drawing.Point(157, 86);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.ReadOnly = true;
            this.txtAccountName.Size = new System.Drawing.Size(217, 24);
            this.txtAccountName.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Location = new System.Drawing.Point(261, 204);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 32);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbCostType
            // 
            this.cmbCostType.FormattingEnabled = true;
            this.cmbCostType.Location = new System.Drawing.Point(157, 128);
            this.cmbCostType.Name = "cmbCostType";
            this.cmbCostType.Size = new System.Drawing.Size(217, 21);
            this.cmbCostType.TabIndex = 3;
            // 
            // txtAllocation
            // 
            this.txtAllocation.AddDecimalAfterMaxWholeDigits = true;
            this.txtAllocation.AllowNegative = false;
            this.txtAllocation.DecimalPoint = '.';
            this.txtAllocation.DecimalValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAllocation.DigitsInGroup = 3;
            this.txtAllocation.DoubleValue = 0D;
            this.txtAllocation.Flags = 197120;
            this.txtAllocation.GroupSeparator = ',';
            this.txtAllocation.IntValue = 0;
            this.txtAllocation.Location = new System.Drawing.Point(157, 166);
            this.txtAllocation.LongValue = ((long)(0));
            this.txtAllocation.MaxDecimalPlaces = 2;
            this.txtAllocation.MaxLength = 10;
            this.txtAllocation.MaxWholeDigits = 12;
            this.txtAllocation.Name = "txtAllocation";
            this.txtAllocation.NegativeSign = '-';
            this.txtAllocation.Prefix = "";
            this.txtAllocation.RangeMax = 100D;
            this.txtAllocation.RangeMin = 0D;
            this.txtAllocation.Size = new System.Drawing.Size(61, 20);
            this.txtAllocation.TabIndex = 4;
            this.txtAllocation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAllocation.TextChanged += new System.EventHandler(this.txtAllocation_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(354, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 32);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmACS120_AddEditCostType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 257);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtAllocation);
            this.Controls.Add(this.cmbCostType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.txtAccountCode);
            this.Controls.Add(this.lblAllocation);
            this.Controls.Add(this.lblCostType);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.lblAccountCode);
            this.Name = "FrmACS120_AddEditCostType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Cost Type";
            this.Load += new System.EventHandler(this.FrmACS120_AddEditCostType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAccountCode;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label lblCostType;
        private System.Windows.Forms.Label lblAllocation;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbCostType;
        private EAP.Framework.Windows.Controls.NumericTextBox txtAllocation;
        private System.Windows.Forms.Button btnCancel;
    }
}