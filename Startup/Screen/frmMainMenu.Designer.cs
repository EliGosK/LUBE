namespace Startup.Screen
{
    partial class frmMainMenu
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mgMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS110 = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS120 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mgProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS310 = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS320 = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS330 = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS340 = new System.Windows.Forms.ToolStripMenuItem();
            this.miACS350 = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miADM010_UserMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.miADM030_UserGroupMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.miADM040_SecurityMapping = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miRPT010 = new System.Windows.Forms.ToolStripMenuItem();
            this.miRPT020 = new System.Windows.Forms.ToolStripMenuItem();
            this.miRPT030 = new System.Windows.Forms.ToolStripMenuItem();
            this.miRPT040 = new System.Windows.Forms.ToolStripMenuItem();
            this.miRPT050 = new System.Windows.Forms.ToolStripMenuItem();
            this.mgWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsScreenName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDatabaseName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabScreenList = new System.Windows.Forms.TabControl();
            this.menuMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mgMaster,
            this.mgProcess,
            this.securityToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mgWindows});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(956, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // mgMaster
            // 
            this.mgMaster.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miACS110,
            this.miACS120,
            this.toolStripSeparator3,
            this.miLogout});
            this.mgMaster.Name = "mgMaster";
            this.mgMaster.Size = new System.Drawing.Size(55, 20);
            this.mgMaster.Text = "&Master";
            // 
            // miACS110
            // 
            this.miACS110.Name = "miACS110";
            this.miACS110.Size = new System.Drawing.Size(330, 22);
            this.miACS110.Text = "ACS110 - Cost Type Setup";
            // 
            // miACS120
            // 
            this.miACS120.Name = "miACS120";
            this.miACS120.Size = new System.Drawing.Size(330, 22);
            this.miACS120.Text = "ACS120 - Mapping Account Code and Cost Type";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(327, 6);
            // 
            // miLogout
            // 
            this.miLogout.Name = "miLogout";
            this.miLogout.Size = new System.Drawing.Size(330, 22);
            this.miLogout.Text = "Logout";
            this.miLogout.Click += new System.EventHandler(this.miLogout_Click);
            // 
            // mgProcess
            // 
            this.mgProcess.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miACS310,
            this.miACS320,
            this.miACS330,
            this.miACS340,
            this.miACS350});
            this.mgProcess.Name = "mgProcess";
            this.mgProcess.Size = new System.Drawing.Size(59, 20);
            this.mgProcess.Text = "&Process";
            // 
            // miACS310
            // 
            this.miACS310.Name = "miACS310";
            this.miACS310.Size = new System.Drawing.Size(378, 22);
            this.miACS310.Text = "ACS310 - Process Retrieve Data";
            // 
            // miACS320
            // 
            this.miACS320.Name = "miACS320";
            this.miACS320.Size = new System.Drawing.Size(378, 22);
            this.miACS320.Text = "ACS320 - Process Actual Cost Calculation";
            // 
            // miACS330
            // 
            this.miACS330.Name = "miACS330";
            this.miACS330.Size = new System.Drawing.Size(378, 22);
            this.miACS330.Text = "ACS330 - Process Transfer Data (Monthly)";
            // 
            // miACS340
            // 
            this.miACS340.Name = "miACS340";
            this.miACS340.Size = new System.Drawing.Size(378, 22);
            this.miACS340.Text = "ACS340 - Process Standard MOH Rate Calculation (Yearly)";
            // 
            // miACS350
            // 
            this.miACS350.Name = "miACS350";
            this.miACS350.Size = new System.Drawing.Size(378, 22);
            this.miACS350.Text = "ACS350 - Process Inventory Revalution Import";
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miADM010_UserMaintenance,
            this.miADM030_UserGroupMaintenance,
            this.miADM040_SecurityMapping,
            this.toolStripSeparator1});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.securityToolStripMenuItem.Text = "&Security";
            // 
            // miADM010_UserMaintenance
            // 
            this.miADM010_UserMaintenance.Name = "miADM010_UserMaintenance";
            this.miADM010_UserMaintenance.Size = new System.Drawing.Size(261, 22);
            this.miADM010_UserMaintenance.Tag = "Presentation.Forms.Admin.frmADM010_UserMaintenance";
            this.miADM010_UserMaintenance.Text = "ADM010 - User Maintenance";
            // 
            // miADM030_UserGroupMaintenance
            // 
            this.miADM030_UserGroupMaintenance.Name = "miADM030_UserGroupMaintenance";
            this.miADM030_UserGroupMaintenance.Size = new System.Drawing.Size(261, 22);
            this.miADM030_UserGroupMaintenance.Tag = "Presentation.Forms.Admin.frmADM030_UserGroupMaintenance";
            this.miADM030_UserGroupMaintenance.Text = "ADM030 - User Group Maintenance";
            // 
            // miADM040_SecurityMapping
            // 
            this.miADM040_SecurityMapping.Name = "miADM040_SecurityMapping";
            this.miADM040_SecurityMapping.Size = new System.Drawing.Size(261, 22);
            this.miADM040_SecurityMapping.Tag = "Presentation.Forms.Admin.frmADM040_SecurityMapping";
            this.miADM040_SecurityMapping.Text = "ADM040 - Security Mapping";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.miRPT010,
            this.miRPT020,
            this.miRPT030,
            this.miRPT040,
            this.miRPT050});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItem1.Text = "&Reports";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(440, 6);
            // 
            // miRPT010
            // 
            this.miRPT010.Name = "miRPT010";
            this.miRPT010.Size = new System.Drawing.Size(443, 22);
            this.miRPT010.Tag = "Presentation.Forms.Report.FrmRPT010_ManufacturingOverheadSummaryReport";
            this.miRPT010.Text = "RPT010 - Overhead Report";
            // 
            // miRPT020
            // 
            this.miRPT020.Name = "miRPT020";
            this.miRPT020.Size = new System.Drawing.Size(443, 22);
            this.miRPT020.Text = "RPT020 - Cost Report";
            // 
            // miRPT030
            // 
            this.miRPT030.Name = "miRPT030";
            this.miRPT030.Size = new System.Drawing.Size(443, 22);
            this.miRPT030.Text = "RPT030 - Costing FG Report";
            // 
            // miRPT040
            // 
            this.miRPT040.Name = "miRPT040";
            this.miRPT040.Size = new System.Drawing.Size(443, 22);
            this.miRPT040.Text = "RPT040 - Closing Item Report (Summary)";
            // 
            // miRPT050
            // 
            this.miRPT050.Name = "miRPT050";
            this.miRPT050.Size = new System.Drawing.Size(291, 22);
            this.miRPT050.Text = "RPT050 - Worksheet Costing Report";
            // 
            // mgWindows
            // 
            this.mgWindows.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mgWindows.Name = "mgWindows";
            this.mgWindows.Size = new System.Drawing.Size(68, 20);
            this.mgWindows.Text = "&Windows";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsUser,
            this.toolStripStatusLabel1,
            this.stsScreenName,
            this.toolStripStatusLabel3,
            this.stsDatabaseName,
            this.toolStripStatusLabel2,
            this.stsVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 634);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(956, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsUser
            // 
            this.stsUser.Image = global::Startup.Properties.Resources.User_x16;
            this.stsUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stsUser.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.stsUser.Name = "stsUser";
            this.stsUser.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.stsUser.Size = new System.Drawing.Size(34, 17);
            this.stsUser.Text = "-";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 17);
            // 
            // stsScreenName
            // 
            this.stsScreenName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.stsScreenName.Image = global::Startup.Properties.Resources.form_x16;
            this.stsScreenName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stsScreenName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.stsScreenName.Name = "stsScreenName";
            this.stsScreenName.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.stsScreenName.Size = new System.Drawing.Size(749, 17);
            this.stsScreenName.Spring = true;
            this.stsScreenName.Text = "-";
            this.stsScreenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 17);
            // 
            // stsDatabaseName
            // 
            this.stsDatabaseName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.stsDatabaseName.Image = global::Startup.Properties.Resources.database_blue_x16;
            this.stsDatabaseName.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.stsDatabaseName.Name = "stsDatabaseName";
            this.stsDatabaseName.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.stsDatabaseName.Size = new System.Drawing.Size(112, 17);
            this.stsDatabaseName.Text = "Database Name";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 17);
            // 
            // stsVersion
            // 
            this.stsVersion.Image = global::Startup.Properties.Resources.version_x16;
            this.stsVersion.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.stsVersion.Name = "stsVersion";
            this.stsVersion.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.stsVersion.Size = new System.Drawing.Size(22, 17);
            // 
            // tabScreenList
            // 
            this.tabScreenList.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabScreenList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabScreenList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabScreenList.Location = new System.Drawing.Point(0, 608);
            this.tabScreenList.Name = "tabScreenList";
            this.tabScreenList.SelectedIndex = 0;
            this.tabScreenList.Size = new System.Drawing.Size(956, 26);
            this.tabScreenList.TabIndex = 5;
            this.tabScreenList.Visible = false;
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 656);
            this.Controls.Add(this.tabScreenList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMainMenu";
            this.Text = "Actual Costing System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainMenu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainMenu_FormClosed);
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmMainMenu_MdiChildActivate);
            this.Shown += new System.EventHandler(this.frmMainForm_Shown);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mgMaster;
        private System.Windows.Forms.ToolStripMenuItem miLogout;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miADM010_UserMaintenance;
        private System.Windows.Forms.ToolStripMenuItem miADM030_UserGroupMaintenance;
        private System.Windows.Forms.ToolStripMenuItem miADM040_SecurityMapping;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mgWindows;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsScreenName;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabControl tabScreenList;
        private System.Windows.Forms.ToolStripStatusLabel stsUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel stsDatabaseName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel stsVersion;
        private System.Windows.Forms.ToolStripMenuItem mgProcess;
        private System.Windows.Forms.ToolStripMenuItem miACS310;
        private System.Windows.Forms.ToolStripMenuItem miACS320;
        private System.Windows.Forms.ToolStripMenuItem miACS330;
        private System.Windows.Forms.ToolStripMenuItem miACS340;
        private System.Windows.Forms.ToolStripMenuItem miRPT020;
        private System.Windows.Forms.ToolStripMenuItem miRPT030;
        private System.Windows.Forms.ToolStripMenuItem miRPT040;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miACS110;
        private System.Windows.Forms.ToolStripMenuItem miACS120;
        private System.Windows.Forms.ToolStripMenuItem miACS120_addeditCost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem miRPT010;
        private System.Windows.Forms.ToolStripMenuItem miRPT050;
        private System.Windows.Forms.ToolStripMenuItem miACS350;
    }
}

