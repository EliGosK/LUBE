using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Reflection;
using System.IO;


namespace EAP.Framework.Windows.Forms
{
    public partial class FindDialog : Form {
        #region Control Decalration
        private System.Windows.Forms.StatusBar statusBar1;
        protected System.Windows.Forms.GroupBox grpFindOptions;
        protected System.Windows.Forms.Panel panelFindOptions;
        protected System.Windows.Forms.Panel panelFindOptionsButton;
        protected System.Windows.Forms.Panel panelResultButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusBarPanel statusBarPanel1;
        private System.Windows.Forms.StatusBarPanel statusBarPanel2;
        protected System.Windows.Forms.Panel panelExtTop;
        #endregion

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDialog));
            this.grpFindOptions = new System.Windows.Forms.GroupBox();
            this.panelFindOptions = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panelFindOptionsButton = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this.panelResultButton = new System.Windows.Forms.Panel();
            this.btnUnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.pageNavigator = new EAP.Framework.Windows.Controls.PageNavigator();
            this.panelExtTop = new System.Windows.Forms.Panel();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.grpFindOptions.SuspendLayout();
            this.panelFindOptions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelFindOptionsButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
            this.panelResultButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFindOptions
            // 
            this.grpFindOptions.Controls.Add(this.panelFindOptions);
            this.grpFindOptions.Controls.Add(this.panelFindOptionsButton);
            this.grpFindOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFindOptions.Location = new System.Drawing.Point(0, 24);
            this.grpFindOptions.Name = "grpFindOptions";
            this.grpFindOptions.Size = new System.Drawing.Size(789, 157);
            this.grpFindOptions.TabIndex = 0;
            this.grpFindOptions.TabStop = false;
            this.grpFindOptions.Text = "Find Options...";
            this.grpFindOptions.Resize += new System.EventHandler(this.grpFindOptions_Resize);
            // 
            // panelFindOptions
            // 
            this.panelFindOptions.Controls.Add(this.panel1);
            this.panelFindOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFindOptions.Location = new System.Drawing.Point(3, 16);
            this.panelFindOptions.Name = "panelFindOptions";
            this.panelFindOptions.Size = new System.Drawing.Size(695, 138);
            this.panelFindOptions.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Location = new System.Drawing.Point(8, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 24);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(344, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "to";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(464, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(368, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "textBox2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(216, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "textBox1";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBox1.Location = new System.Drawing.Point(96, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "comboBox1";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(16, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox1
            // 
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(16, 24);
            this.checkBox1.TabIndex = 0;
            // 
            // panelFindOptionsButton
            // 
            this.panelFindOptionsButton.Controls.Add(this.btnClear);
            this.panelFindOptionsButton.Controls.Add(this.btnSearch);
            this.panelFindOptionsButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelFindOptionsButton.Location = new System.Drawing.Point(698, 16);
            this.panelFindOptionsButton.Name = "panelFindOptionsButton";
            this.panelFindOptionsButton.Size = new System.Drawing.Size(88, 138);
            this.panelFindOptionsButton.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(6, 29);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(6, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 488);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanel2});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(789, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "statusBar1";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Width = 200;
            // 
            // statusBarPanel2
            // 
            this.statusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel2.Name = "statusBarPanel2";
            this.statusBarPanel2.Width = 572;
            // 
            // panelResultButton
            // 
            this.panelResultButton.Controls.Add(this.btnUnSelectAll);
            this.panelResultButton.Controls.Add(this.btnSelectAll);
            this.panelResultButton.Controls.Add(this.btnClose);
            this.panelResultButton.Controls.Add(this.btnLoad);
            this.panelResultButton.Controls.Add(this.pageNavigator);
            this.panelResultButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelResultButton.Location = new System.Drawing.Point(0, 456);
            this.panelResultButton.Name = "panelResultButton";
            this.panelResultButton.Size = new System.Drawing.Size(789, 32);
            this.panelResultButton.TabIndex = 3;
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnSelectAll.Image")));
            this.btnUnSelectAll.Location = new System.Drawing.Point(106, 3);
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(104, 23);
            this.btnUnSelectAll.TabIndex = 8;
            this.btnUnSelectAll.Text = "Un Select All";
            this.btnUnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(2, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(100, 23);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(698, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnLoad.Image")));
            this.btnLoad.Location = new System.Drawing.Point(615, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pageNavigator
            // 
            this.pageNavigator.Alignment = System.Drawing.StringAlignment.Far;
            this.pageNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageNavigator.CurrentPage = 1;
            this.pageNavigator.Location = new System.Drawing.Point(0, 4);
            this.pageNavigator.MaxPage = 10;
            this.pageNavigator.Name = "pageNavigator";
            this.pageNavigator.PagePerScreen = 5;
            this.pageNavigator.PageText = "page";
            this.pageNavigator.Size = new System.Drawing.Size(609, 23);
            this.pageNavigator.TabIndex = 5;
            this.pageNavigator.Text = "pageNavigator1";
            this.pageNavigator.OnPageClick += new EAP.Framework.Windows.Controls.PageNavigator.PageClickHandler(this.pageNavigator_OnPageClick);
            // 
            // panelExtTop
            // 
            this.panelExtTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExtTop.Location = new System.Drawing.Point(0, 0);
            this.panelExtTop.Name = "panelExtTop";
            this.panelExtTop.Size = new System.Drawing.Size(789, 24);
            this.panelExtTop.TabIndex = 6;
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToAddRows = false;
            this.grdResult.AllowUserToDeleteRows = false;
            this.grdResult.AllowUserToResizeRows = false;
            this.grdResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdResult.Location = new System.Drawing.Point(0, 181);
            this.grdResult.MultiSelect = false;
            this.grdResult.Name = "grdResult";
            this.grdResult.ReadOnly = true;
            this.grdResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdResult.Size = new System.Drawing.Size(789, 275);
            this.grdResult.TabIndex = 8;
            // 
            // FindDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(789, 510);
            this.Controls.Add(this.grdResult);
            this.Controls.Add(this.panelResultButton);
            this.Controls.Add(this.grpFindOptions);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panelExtTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FindDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Dialog";
            this.Load += new System.EventHandler(this.FindDialog_Load);
            this.ResizeEnd += new System.EventHandler(this.FindDialog_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindDialog_KeyDown);
            this.grpFindOptions.ResumeLayout(false);
            this.panelFindOptions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelFindOptionsButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
            this.panelResultButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnSelectAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLoad;
        private DataGridView grdResult;
    }
}
