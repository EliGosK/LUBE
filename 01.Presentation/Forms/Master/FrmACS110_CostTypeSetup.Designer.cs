namespace Presentation.Forms.Master
{
    partial class FrmACS110_CostTypeSetup
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
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCostGroup = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCostTypeCode = new System.Windows.Forms.Label();
            this.cmbSearchCostGroup = new System.Windows.Forms.ComboBox();
            this.txtSearchDescription = new System.Windows.Forms.TextBox();
            this.txtSearchCostType = new System.Windows.Forms.TextBox();
            this.dgvCostType = new System.Windows.Forms.DataGridView();
            this.grdCostTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdCostTypeGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdUpdateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdUpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAddEdit = new System.Windows.Forms.GroupBox();
            this.radioBtnType2 = new System.Windows.Forms.RadioButton();
            this.radioBtnType1 = new System.Windows.Forms.RadioButton();
            this.lblAddProcess = new System.Windows.Forms.Label();
            this.cmbAddProcess = new System.Windows.Forms.ComboBox();
            this.lblAddCostGroup = new System.Windows.Forms.Label();
            this.lblAddDescription = new System.Windows.Forms.Label();
            this.lblAddCostType = new System.Windows.Forms.Label();
            this.cmbAddCostGroup = new System.Windows.Forms.ComboBox();
            this.txtAddDescription = new System.Windows.Forms.TextBox();
            this.txtAddCostType = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostType)).BeginInit();
            this.grpAddEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.btnCancel);
            this.pnlContainer.Controls.Add(this.btnSave);
            this.pnlContainer.Controls.Add(this.grpAddEdit);
            this.pnlContainer.Controls.Add(this.dgvCostType);
            this.pnlContainer.Controls.Add(this.grpSearch);
            this.pnlContainer.Size = new System.Drawing.Size(1003, 584);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnClear);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.lblCostGroup);
            this.grpSearch.Controls.Add(this.lblDescription);
            this.grpSearch.Controls.Add(this.lblCostTypeCode);
            this.grpSearch.Controls.Add(this.cmbSearchCostGroup);
            this.grpSearch.Controls.Add(this.txtSearchDescription);
            this.grpSearch.Controls.Add(this.txtSearchCostType);
            this.grpSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpSearch.Location = new System.Drawing.Point(27, 31);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(817, 120);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Criteria";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(713, 80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(618, 80);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCostGroup
            // 
            this.lblCostGroup.AutoSize = true;
            this.lblCostGroup.Location = new System.Drawing.Point(50, 80);
            this.lblCostGroup.Name = "lblCostGroup";
            this.lblCostGroup.Size = new System.Drawing.Size(86, 18);
            this.lblCostGroup.TabIndex = 4;
            this.lblCostGroup.Text = "Cost Group";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(359, 43);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(83, 18);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description";
            // 
            // lblCostTypeCode
            // 
            this.lblCostTypeCode.AutoSize = true;
            this.lblCostTypeCode.Location = new System.Drawing.Point(27, 43);
            this.lblCostTypeCode.Name = "lblCostTypeCode";
            this.lblCostTypeCode.Size = new System.Drawing.Size(116, 18);
            this.lblCostTypeCode.TabIndex = 3;
            this.lblCostTypeCode.Text = "Cost Type Code";
            // 
            // cmbSearchCostGroup
            // 
            this.cmbSearchCostGroup.FormattingEnabled = true;
            this.cmbSearchCostGroup.Location = new System.Drawing.Point(146, 80);
            this.cmbSearchCostGroup.Name = "cmbSearchCostGroup";
            this.cmbSearchCostGroup.Size = new System.Drawing.Size(194, 26);
            this.cmbSearchCostGroup.TabIndex = 2;
            // 
            // txtSearchDescription
            // 
            this.txtSearchDescription.Location = new System.Drawing.Point(447, 43);
            this.txtSearchDescription.Name = "txtSearchDescription";
            this.txtSearchDescription.Size = new System.Drawing.Size(341, 24);
            this.txtSearchDescription.TabIndex = 1;
            // 
            // txtSearchCostType
            // 
            this.txtSearchCostType.Location = new System.Drawing.Point(146, 43);
            this.txtSearchCostType.Name = "txtSearchCostType";
            this.txtSearchCostType.Size = new System.Drawing.Size(194, 24);
            this.txtSearchCostType.TabIndex = 0;
            // 
            // dgvCostType
            // 
            this.dgvCostType.AllowUserToAddRows = false;
            this.dgvCostType.AllowUserToResizeRows = false;
            this.dgvCostType.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvCostType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCostType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grdCostTypeCode,
            this.grdDescription,
            this.grdCostTypeGroup,
            this.grdProcess,
            this.grdActive,
            this.grdUpdateBy,
            this.grdUpdateDate});
            this.dgvCostType.Location = new System.Drawing.Point(30, 166);
            this.dgvCostType.Name = "dgvCostType";
            this.dgvCostType.ReadOnly = true;
            this.dgvCostType.RowHeadersVisible = false;
            this.dgvCostType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCostType.Size = new System.Drawing.Size(814, 203);
            this.dgvCostType.TabIndex = 1;
            // 
            // grdCostTypeCode
            // 
            this.grdCostTypeCode.DataPropertyName = "CostTypeCode";
            this.grdCostTypeCode.HeaderText = "Cost Type Code";
            this.grdCostTypeCode.Name = "grdCostTypeCode";
            this.grdCostTypeCode.ReadOnly = true;
            this.grdCostTypeCode.Width = 110;
            // 
            // grdDescription
            // 
            this.grdDescription.DataPropertyName = "Description";
            this.grdDescription.HeaderText = "Description";
            this.grdDescription.Name = "grdDescription";
            this.grdDescription.ReadOnly = true;
            this.grdDescription.Width = 236;
            // 
            // grdCostTypeGroup
            // 
            this.grdCostTypeGroup.DataPropertyName = "CostTypeGroup";
            this.grdCostTypeGroup.HeaderText = "Cost Group";
            this.grdCostTypeGroup.Name = "grdCostTypeGroup";
            this.grdCostTypeGroup.ReadOnly = true;
            this.grdCostTypeGroup.Width = 110;
            // 
            // grdProcess
            // 
            this.grdProcess.DataPropertyName = "Process";
            this.grdProcess.HeaderText = "Process";
            this.grdProcess.Name = "grdProcess";
            this.grdProcess.ReadOnly = true;
            // 
            // grdActive
            // 
            this.grdActive.DataPropertyName = "Active";
            this.grdActive.HeaderText = "Active";
            this.grdActive.Name = "grdActive";
            this.grdActive.ReadOnly = true;
            this.grdActive.Width = 55;
            // 
            // grdUpdateBy
            // 
            this.grdUpdateBy.DataPropertyName = "UpdateBy";
            this.grdUpdateBy.HeaderText = "Update By";
            this.grdUpdateBy.Name = "grdUpdateBy";
            this.grdUpdateBy.ReadOnly = true;
            this.grdUpdateBy.Width = 80;
            // 
            // grdUpdateDate
            // 
            this.grdUpdateDate.DataPropertyName = "UpdateDate";
            this.grdUpdateDate.HeaderText = "Update Date";
            this.grdUpdateDate.Name = "grdUpdateDate";
            this.grdUpdateDate.ReadOnly = true;
            this.grdUpdateDate.Width = 120;
            // 
            // grpAddEdit
            // 
            this.grpAddEdit.Controls.Add(this.radioBtnType2);
            this.grpAddEdit.Controls.Add(this.radioBtnType1);
            this.grpAddEdit.Controls.Add(this.lblAddProcess);
            this.grpAddEdit.Controls.Add(this.cmbAddProcess);
            this.grpAddEdit.Controls.Add(this.lblAddCostGroup);
            this.grpAddEdit.Controls.Add(this.lblAddDescription);
            this.grpAddEdit.Controls.Add(this.lblAddCostType);
            this.grpAddEdit.Controls.Add(this.cmbAddCostGroup);
            this.grpAddEdit.Controls.Add(this.txtAddDescription);
            this.grpAddEdit.Controls.Add(this.txtAddCostType);
            this.grpAddEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpAddEdit.Location = new System.Drawing.Point(32, 387);
            this.grpAddEdit.Name = "grpAddEdit";
            this.grpAddEdit.Size = new System.Drawing.Size(814, 120);
            this.grpAddEdit.TabIndex = 2;
            this.grpAddEdit.TabStop = false;
            this.grpAddEdit.Text = "Add / Edit";
            // 
            // radioBtnType2
            // 
            this.radioBtnType2.AutoSize = true;
            this.radioBtnType2.Location = new System.Drawing.Point(724, 84);
            this.radioBtnType2.Name = "radioBtnType2";
            this.radioBtnType2.Size = new System.Drawing.Size(75, 22);
            this.radioBtnType2.TabIndex = 19;
            this.radioBtnType2.TabStop = true;
            this.radioBtnType2.Text = "Inactive";
            this.radioBtnType2.UseVisualStyleBackColor = true;
            // 
            // radioBtnType1
            // 
            this.radioBtnType1.AutoSize = true;
            this.radioBtnType1.Location = new System.Drawing.Point(648, 84);
            this.radioBtnType1.Name = "radioBtnType1";
            this.radioBtnType1.Size = new System.Drawing.Size(65, 22);
            this.radioBtnType1.TabIndex = 18;
            this.radioBtnType1.TabStop = true;
            this.radioBtnType1.Text = "Active";
            this.radioBtnType1.UseVisualStyleBackColor = true;
            // 
            // lblAddProcess
            // 
            this.lblAddProcess.AutoSize = true;
            this.lblAddProcess.Location = new System.Drawing.Point(308, 80);
            this.lblAddProcess.Name = "lblAddProcess";
            this.lblAddProcess.Size = new System.Drawing.Size(64, 18);
            this.lblAddProcess.TabIndex = 6;
            this.lblAddProcess.Text = "Process";
            // 
            // cmbAddProcess
            // 
            this.cmbAddProcess.FormattingEnabled = true;
            this.cmbAddProcess.Location = new System.Drawing.Point(383, 80);
            this.cmbAddProcess.Name = "cmbAddProcess";
            this.cmbAddProcess.Size = new System.Drawing.Size(237, 26);
            this.cmbAddProcess.TabIndex = 5;
            // 
            // lblAddCostGroup
            // 
            this.lblAddCostGroup.AutoSize = true;
            this.lblAddCostGroup.Location = new System.Drawing.Point(50, 80);
            this.lblAddCostGroup.Name = "lblAddCostGroup";
            this.lblAddCostGroup.Size = new System.Drawing.Size(86, 18);
            this.lblAddCostGroup.TabIndex = 4;
            this.lblAddCostGroup.Text = "Cost Group";
            // 
            // lblAddDescription
            // 
            this.lblAddDescription.AutoSize = true;
            this.lblAddDescription.Location = new System.Drawing.Point(289, 43);
            this.lblAddDescription.Name = "lblAddDescription";
            this.lblAddDescription.Size = new System.Drawing.Size(83, 18);
            this.lblAddDescription.TabIndex = 4;
            this.lblAddDescription.Text = "Description";
            // 
            // lblAddCostType
            // 
            this.lblAddCostType.AutoSize = true;
            this.lblAddCostType.Location = new System.Drawing.Point(27, 43);
            this.lblAddCostType.Name = "lblAddCostType";
            this.lblAddCostType.Size = new System.Drawing.Size(116, 18);
            this.lblAddCostType.TabIndex = 3;
            this.lblAddCostType.Text = "Cost Type Code";
            // 
            // cmbAddCostGroup
            // 
            this.cmbAddCostGroup.FormattingEnabled = true;
            this.cmbAddCostGroup.Location = new System.Drawing.Point(146, 80);
            this.cmbAddCostGroup.Name = "cmbAddCostGroup";
            this.cmbAddCostGroup.Size = new System.Drawing.Size(133, 26);
            this.cmbAddCostGroup.TabIndex = 2;
            // 
            // txtAddDescription
            // 
            this.txtAddDescription.Location = new System.Drawing.Point(382, 43);
            this.txtAddDescription.MaxLength = 200;
            this.txtAddDescription.Name = "txtAddDescription";
            this.txtAddDescription.Size = new System.Drawing.Size(237, 24);
            this.txtAddDescription.TabIndex = 1;
            // 
            // txtAddCostType
            // 
            this.txtAddCostType.Location = new System.Drawing.Point(146, 43);
            this.txtAddCostType.MaxLength = 10;
            this.txtAddCostType.Name = "txtAddCostType";
            this.txtAddCostType.Size = new System.Drawing.Size(133, 24);
            this.txtAddCostType.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSave.Location = new System.Drawing.Point(634, 513);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 38);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnCancel.Location = new System.Drawing.Point(756, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 38);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmACS110_CostTypeSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 631);
            this.Name = "FrmACS110_CostTypeSetup";
            this.Text = "ACS110 : Cost Type Setup";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostType)).EndInit();
            this.grpAddEdit.ResumeLayout(false);
            this.grpAddEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpAddEdit;
        private System.Windows.Forms.Label lblAddProcess;
        private System.Windows.Forms.ComboBox cmbAddProcess;
        private System.Windows.Forms.Label lblAddCostGroup;
        private System.Windows.Forms.Label lblAddDescription;
        private System.Windows.Forms.Label lblAddCostType;
        private System.Windows.Forms.ComboBox cmbAddCostGroup;
        private System.Windows.Forms.TextBox txtAddDescription;
        private System.Windows.Forms.TextBox txtAddCostType;
        private System.Windows.Forms.DataGridView dgvCostType;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblCostGroup;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCostTypeCode;
        private System.Windows.Forms.ComboBox cmbSearchCostGroup;
        private System.Windows.Forms.TextBox txtSearchDescription;
        private System.Windows.Forms.TextBox txtSearchCostType;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton radioBtnType2;
        private System.Windows.Forms.RadioButton radioBtnType1;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCostTypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdCostTypeGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdUpdateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn grdUpdateDate;
    }
}