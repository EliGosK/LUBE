namespace Presentation.Forms.Master
{
    partial class FrmACS120_MappingAccountCodeAndCostType
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
            this.lblAccoutName = new System.Windows.Forms.Label();
            this.lblAccountCode = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.grpProductionCost = new System.Windows.Forms.GroupBox();
            this.dgvProductionCost = new System.Windows.Forms.DataGridView();
            this.AccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpMappingCost = new System.Windows.Forms.GroupBox();
            this.dgvMappingCost = new System.Windows.Forms.DataGridView();
            this.CostType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PercentAllocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpSearch.SuspendLayout();
            this.grpProductionCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionCost)).BeginInit();
            this.grpMappingCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappingCost)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpMappingCost);
            this.pnlContainer.Controls.Add(this.grpProductionCost);
            this.pnlContainer.Controls.Add(this.grpSearch);
            this.pnlContainer.Size = new System.Drawing.Size(1032, 584);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnClear);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.lblAccoutName);
            this.grpSearch.Controls.Add(this.lblAccountCode);
            this.grpSearch.Controls.Add(this.txtAccountName);
            this.grpSearch.Controls.Add(this.txtAccountCode);
            this.grpSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpSearch.Location = new System.Drawing.Point(25, 17);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(902, 67);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Criteria";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(811, 25);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 26);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(724, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblAccoutName
            // 
            this.lblAccoutName.AutoSize = true;
            this.lblAccoutName.Location = new System.Drawing.Point(287, 29);
            this.lblAccoutName.Name = "lblAccoutName";
            this.lblAccoutName.Size = new System.Drawing.Size(106, 18);
            this.lblAccoutName.TabIndex = 3;
            this.lblAccoutName.Text = "Account Name";
            // 
            // lblAccountCode
            // 
            this.lblAccountCode.AutoSize = true;
            this.lblAccountCode.Location = new System.Drawing.Point(40, 29);
            this.lblAccountCode.Name = "lblAccountCode";
            this.lblAccountCode.Size = new System.Drawing.Size(102, 18);
            this.lblAccountCode.TabIndex = 2;
            this.lblAccountCode.Text = "Account Code";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(399, 27);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(276, 24);
            this.txtAccountName.TabIndex = 1;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new System.Drawing.Point(148, 27);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(100, 24);
            this.txtAccountCode.TabIndex = 0;
            // 
            // grpProductionCost
            // 
            this.grpProductionCost.Controls.Add(this.dgvProductionCost);
            this.grpProductionCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpProductionCost.Location = new System.Drawing.Point(25, 90);
            this.grpProductionCost.Name = "grpProductionCost";
            this.grpProductionCost.Size = new System.Drawing.Size(442, 432);
            this.grpProductionCost.TabIndex = 1;
            this.grpProductionCost.TabStop = false;
            this.grpProductionCost.Text = "Account ";
            // 
            // dgvProductionCost
            // 
            this.dgvProductionCost.AllowUserToAddRows = false;
            this.dgvProductionCost.AllowUserToDeleteRows = false;
            this.dgvProductionCost.AllowUserToResizeRows = false;
            this.dgvProductionCost.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvProductionCost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductionCost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccountCode,
            this.AccountName});
            this.dgvProductionCost.Location = new System.Drawing.Point(21, 27);
            this.dgvProductionCost.MultiSelect = false;
            this.dgvProductionCost.Name = "dgvProductionCost";
            this.dgvProductionCost.ReadOnly = true;
            this.dgvProductionCost.RowHeadersVisible = false;
            this.dgvProductionCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductionCost.Size = new System.Drawing.Size(405, 382);
            this.dgvProductionCost.TabIndex = 0;
            this.dgvProductionCost.SelectionChanged += new System.EventHandler(this.dgvProductionCost_SelectionChanged);
            // 
            // AccountCode
            // 
            this.AccountCode.DataPropertyName = "AccountCode";
            this.AccountCode.HeaderText = "Acc. Code";
            this.AccountCode.Name = "AccountCode";
            this.AccountCode.ReadOnly = true;
            // 
            // AccountName
            // 
            this.AccountName.DataPropertyName = "AccountName";
            this.AccountName.HeaderText = "Account Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.ReadOnly = true;
            this.AccountName.Width = 310;
            // 
            // grpMappingCost
            // 
            this.grpMappingCost.Controls.Add(this.dgvMappingCost);
            this.grpMappingCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpMappingCost.Location = new System.Drawing.Point(473, 90);
            this.grpMappingCost.Name = "grpMappingCost";
            this.grpMappingCost.Size = new System.Drawing.Size(454, 432);
            this.grpMappingCost.TabIndex = 2;
            this.grpMappingCost.TabStop = false;
            this.grpMappingCost.Text = "Mapping Cost Type";
            // 
            // dgvMappingCost
            // 
            this.dgvMappingCost.AllowUserToAddRows = false;
            this.dgvMappingCost.AllowUserToDeleteRows = false;
            this.dgvMappingCost.AllowUserToResizeRows = false;
            this.dgvMappingCost.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvMappingCost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMappingCost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CostType,
            this.AccCode,
            this.AccName,
            this.Description,
            this.PercentAllocation});
            this.dgvMappingCost.Location = new System.Drawing.Point(15, 27);
            this.dgvMappingCost.MultiSelect = false;
            this.dgvMappingCost.Name = "dgvMappingCost";
            this.dgvMappingCost.ReadOnly = true;
            this.dgvMappingCost.RowHeadersVisible = false;
            this.dgvMappingCost.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvMappingCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMappingCost.Size = new System.Drawing.Size(423, 382);
            this.dgvMappingCost.TabIndex = 0;
            this.dgvMappingCost.SelectionChanged += new System.EventHandler(this.dgvMappingCost_SelectionChanged);
            // 
            // CostType
            // 
            this.CostType.DataPropertyName = "CostTypeCode";
            this.CostType.HeaderText = "Cost Type";
            this.CostType.Name = "CostType";
            this.CostType.ReadOnly = true;
            this.CostType.Width = 110;
            // 
            // AccCode
            // 
            this.AccCode.DataPropertyName = "AccountCode";
            this.AccCode.HeaderText = "Account Code";
            this.AccCode.Name = "AccCode";
            this.AccCode.ReadOnly = true;
            this.AccCode.Visible = false;
            // 
            // AccName
            // 
            this.AccName.DataPropertyName = "AccountName";
            this.AccName.HeaderText = "Account Name";
            this.AccName.Name = "AccName";
            this.AccName.ReadOnly = true;
            this.AccName.Visible = false;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 255;
            // 
            // PercentAllocation
            // 
            this.PercentAllocation.DataPropertyName = "PercentAllocation";
            this.PercentAllocation.HeaderText = "%";
            this.PercentAllocation.Name = "PercentAllocation";
            this.PercentAllocation.ReadOnly = true;
            this.PercentAllocation.Width = 55;
            // 
            // FrmACS120_MappingAccountCodeAndCostType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 631);
            this.Name = "FrmACS120_MappingAccountCodeAndCostType";
            this.Text = "ACS120 : Mapping Account Code and Cost Type";
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpProductionCost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductionCost)).EndInit();
            this.grpMappingCost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappingCost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblAccoutName;
        private System.Windows.Forms.Label lblAccountCode;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.GroupBox grpProductionCost;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvProductionCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.GroupBox grpMappingCost;
        private System.Windows.Forms.DataGridView dgvMappingCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostType;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn PercentAllocation;
    }
}