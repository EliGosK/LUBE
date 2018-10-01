#region Using namespace

using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using BusinessService;
using Common;
using DataObject;
using EAP;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using EAP.Framework.Windows.Forms;
using Presentation.Forms.FindDialog;

#endregion

namespace Presentation.Forms.Admin {

    [ScreenPermission(Permission.OpenScreen, Permission.Edit)]    
    public class FrmADM040_SecurityMapping : LUBEFormDev {

        #region Control member
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageUser;
        private System.Windows.Forms.TabPage tabPageUserGroup;
        private System.Windows.Forms.TabPage tabPageScreen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ColumnHeader clhUserName;
        private System.Windows.Forms.ListView lstvUser;
        private System.Windows.Forms.ListView lstvUserGroup;
        private System.Windows.Forms.ColumnHeader clhGroupName;
        private System.Windows.Forms.TreeView trevScreen;
        private System.Windows.Forms.ImageList imglIcon;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenu cmnuSecurity;
        private System.Windows.Forms.MenuItem miAddScreen;
        private System.Windows.Forms.MenuItem miAddUser;
        private System.Windows.Forms.MenuItem miAddUserGroup;

        private System.Windows.Forms.MenuItem miSelectAll;
        private System.Windows.Forms.MenuItem miUnselectAll;

        private System.Windows.Forms.TreeView trevSecurity;
        #endregion

        #region Enumeration
        private enum eImgIndex {
            NameSpace,
            Screen,
            UserGroup,
            User,
            UnChecked,
            Checked,
            Report
        }

        private enum NodeDeepUserView {
            LevelType,
            Screen,
            Permission
        }
        private enum NodeDeepUserGroupView {
            Screen,
            Permission
        }
        private enum NodeDeepScreenView {
            LevelType,
            Name,
            Permission
        }

        #endregion

        #region Members
        private FindScreenDialog m_dlgFindScreen = null;
        private FindUserDialog m_dlgFindUser = null;
        private FindUserGroupDialog m_dlgFindUserGroup = null;

        private Color m_clDisable = Color.Gray;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListView lstvBelongGroup;
        private System.Windows.Forms.ListView lstvMemberOfGroup;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.GroupBox grpSecurityMapping;
        private System.Windows.Forms.GroupBox grpBelongtoGroup;
        private System.Windows.Forms.GroupBox grpMember;
        private MenuItem menuItem1;
        private SecurityBIZ m_bizSecurity;
        
        #endregion

        #region Property 
        public SecurityBIZ BizSecurity {
            get {
                if (m_bizSecurity == null)
                    m_bizSecurity = new SecurityBIZ();
                return m_bizSecurity;
            }
        }
        #endregion

        #region Constructor
        public FrmADM040_SecurityMapping() {
            InitializeComponent();
            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarAdd, m_toolBarDelete, m_toolBarPrint, m_toolBarExport, m_toolBarImport);
        }
        #endregion

        #region Windows Form Designer generated code
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmADM040_SecurityMapping));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageUser = new System.Windows.Forms.TabPage();
            this.lstvUser = new System.Windows.Forms.ListView();
            this.clhUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imglIcon = new System.Windows.Forms.ImageList(this.components);
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.grpBelongtoGroup = new System.Windows.Forms.GroupBox();
            this.lstvBelongGroup = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageUserGroup = new System.Windows.Forms.TabPage();
            this.lstvUserGroup = new System.Windows.Forms.ListView();
            this.clhGroupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.grpMember = new System.Windows.Forms.GroupBox();
            this.lstvMemberOfGroup = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageScreen = new System.Windows.Forms.TabPage();
            this.trevScreen = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpSecurityMapping = new System.Windows.Forms.GroupBox();
            this.trevSecurity = new System.Windows.Forms.TreeView();
            this.cmnuSecurity = new System.Windows.Forms.ContextMenu();
            this.miAddScreen = new System.Windows.Forms.MenuItem();
            this.miAddUser = new System.Windows.Forms.MenuItem();
            this.miAddUserGroup = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miSelectAll = new System.Windows.Forms.MenuItem();
            this.miUnselectAll = new System.Windows.Forms.MenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageUser.SuspendLayout();
            this.grpBelongtoGroup.SuspendLayout();
            this.tabPageUserGroup.SuspendLayout();
            this.grpMember.SuspendLayout();
            this.tabPageScreen.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpSecurityMapping.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.panel1);
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContainer.Size = new System.Drawing.Size(984, 617);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageUser);
            this.tabControl1.Controls.Add(this.tabPageUserGroup);
            this.tabControl1.Controls.Add(this.tabPageScreen);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.ImageList = this.imglIcon;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 611);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.Tag = "";
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageUser
            // 
            this.tabPageUser.Controls.Add(this.lstvUser);
            this.tabPageUser.Controls.Add(this.splitter2);
            this.tabPageUser.Controls.Add(this.grpBelongtoGroup);
            this.tabPageUser.ImageIndex = 3;
            this.tabPageUser.Location = new System.Drawing.Point(4, 23);
            this.tabPageUser.Name = "tabPageUser";
            this.tabPageUser.Size = new System.Drawing.Size(336, 584);
            this.tabPageUser.TabIndex = 0;
            this.tabPageUser.Text = "User";
            // 
            // lstvUser
            // 
            this.lstvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhUserName});
            this.lstvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvUser.HideSelection = false;
            this.lstvUser.Location = new System.Drawing.Point(0, 0);
            this.lstvUser.MultiSelect = false;
            this.lstvUser.Name = "lstvUser";
            this.lstvUser.Size = new System.Drawing.Size(336, 426);
            this.lstvUser.SmallImageList = this.imglIcon;
            this.lstvUser.TabIndex = 0;
            this.lstvUser.UseCompatibleStateImageBehavior = false;
            this.lstvUser.View = System.Windows.Forms.View.Details;
            this.lstvUser.SelectedIndexChanged += new System.EventHandler(this.lstvUser_SelectedIndexChanged);
            // 
            // clhUserName
            // 
            this.clhUserName.Text = "Name";
            this.clhUserName.Width = 300;
            // 
            // imglIcon
            // 
            this.imglIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglIcon.ImageStream")));
            this.imglIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imglIcon.Images.SetKeyName(0, "");
            this.imglIcon.Images.SetKeyName(1, "");
            this.imglIcon.Images.SetKeyName(2, "");
            this.imglIcon.Images.SetKeyName(3, "");
            this.imglIcon.Images.SetKeyName(4, "");
            this.imglIcon.Images.SetKeyName(5, "");
            this.imglIcon.Images.SetKeyName(6, "report.png");
            this.imglIcon.Images.SetKeyName(7, "screen.png");
            this.imglIcon.Images.SetKeyName(8, "users.png");
            this.imglIcon.Images.SetKeyName(9, "user.png");
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 426);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(336, 3);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // grpBelongtoGroup
            // 
            this.grpBelongtoGroup.Controls.Add(this.lstvBelongGroup);
            this.grpBelongtoGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpBelongtoGroup.Location = new System.Drawing.Point(0, 429);
            this.grpBelongtoGroup.Name = "grpBelongtoGroup";
            this.grpBelongtoGroup.Size = new System.Drawing.Size(336, 155);
            this.grpBelongtoGroup.TabIndex = 1;
            this.grpBelongtoGroup.TabStop = false;
            this.grpBelongtoGroup.Text = "Belong to group";
            // 
            // lstvBelongGroup
            // 
            this.lstvBelongGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstvBelongGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvBelongGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstvBelongGroup.HideSelection = false;
            this.lstvBelongGroup.Location = new System.Drawing.Point(3, 16);
            this.lstvBelongGroup.Name = "lstvBelongGroup";
            this.lstvBelongGroup.Size = new System.Drawing.Size(330, 136);
            this.lstvBelongGroup.SmallImageList = this.imglIcon;
            this.lstvBelongGroup.TabIndex = 2;
            this.lstvBelongGroup.UseCompatibleStateImageBehavior = false;
            this.lstvBelongGroup.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 300;
            // 
            // tabPageUserGroup
            // 
            this.tabPageUserGroup.Controls.Add(this.lstvUserGroup);
            this.tabPageUserGroup.Controls.Add(this.splitter3);
            this.tabPageUserGroup.Controls.Add(this.grpMember);
            this.tabPageUserGroup.ImageIndex = 2;
            this.tabPageUserGroup.Location = new System.Drawing.Point(4, 23);
            this.tabPageUserGroup.Name = "tabPageUserGroup";
            this.tabPageUserGroup.Size = new System.Drawing.Size(336, 584);
            this.tabPageUserGroup.TabIndex = 1;
            this.tabPageUserGroup.Text = "User Group";
            // 
            // lstvUserGroup
            // 
            this.lstvUserGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhGroupName});
            this.lstvUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvUserGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstvUserGroup.HideSelection = false;
            this.lstvUserGroup.Location = new System.Drawing.Point(0, 0);
            this.lstvUserGroup.MultiSelect = false;
            this.lstvUserGroup.Name = "lstvUserGroup";
            this.lstvUserGroup.Size = new System.Drawing.Size(336, 426);
            this.lstvUserGroup.SmallImageList = this.imglIcon;
            this.lstvUserGroup.TabIndex = 1;
            this.lstvUserGroup.UseCompatibleStateImageBehavior = false;
            this.lstvUserGroup.View = System.Windows.Forms.View.Details;
            this.lstvUserGroup.SelectedIndexChanged += new System.EventHandler(this.lstvUserGroup_SelectedIndexChanged);
            // 
            // clhGroupName
            // 
            this.clhGroupName.Text = "Name";
            this.clhGroupName.Width = 300;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 426);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(336, 3);
            this.splitter3.TabIndex = 3;
            this.splitter3.TabStop = false;
            // 
            // grpMember
            // 
            this.grpMember.Controls.Add(this.lstvMemberOfGroup);
            this.grpMember.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpMember.Location = new System.Drawing.Point(0, 429);
            this.grpMember.Name = "grpMember";
            this.grpMember.Size = new System.Drawing.Size(336, 155);
            this.grpMember.TabIndex = 2;
            this.grpMember.TabStop = false;
            this.grpMember.Text = "Member(s)";
            // 
            // lstvMemberOfGroup
            // 
            this.lstvMemberOfGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lstvMemberOfGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvMemberOfGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstvMemberOfGroup.HideSelection = false;
            this.lstvMemberOfGroup.Location = new System.Drawing.Point(3, 16);
            this.lstvMemberOfGroup.Name = "lstvMemberOfGroup";
            this.lstvMemberOfGroup.Size = new System.Drawing.Size(330, 136);
            this.lstvMemberOfGroup.SmallImageList = this.imglIcon;
            this.lstvMemberOfGroup.TabIndex = 2;
            this.lstvMemberOfGroup.UseCompatibleStateImageBehavior = false;
            this.lstvMemberOfGroup.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 300;
            // 
            // tabPageScreen
            // 
            this.tabPageScreen.Controls.Add(this.trevScreen);
            this.tabPageScreen.ImageIndex = 1;
            this.tabPageScreen.Location = new System.Drawing.Point(4, 23);
            this.tabPageScreen.Name = "tabPageScreen";
            this.tabPageScreen.Size = new System.Drawing.Size(336, 584);
            this.tabPageScreen.TabIndex = 2;
            this.tabPageScreen.Text = "Screen";
            // 
            // trevScreen
            // 
            this.trevScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trevScreen.HideSelection = false;
            this.trevScreen.ImageIndex = 0;
            this.trevScreen.ImageList = this.imglIcon;
            this.trevScreen.Location = new System.Drawing.Point(0, 0);
            this.trevScreen.Name = "trevScreen";
            this.trevScreen.SelectedImageIndex = 0;
            this.trevScreen.Size = new System.Drawing.Size(336, 584);
            this.trevScreen.TabIndex = 1;
            this.trevScreen.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trevScreen_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grpSecurityMapping);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 611);
            this.panel1.TabIndex = 3;
            // 
            // grpSecurityMapping
            // 
            this.grpSecurityMapping.Controls.Add(this.trevSecurity);
            this.grpSecurityMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSecurityMapping.Location = new System.Drawing.Point(347, 0);
            this.grpSecurityMapping.Name = "grpSecurityMapping";
            this.grpSecurityMapping.Size = new System.Drawing.Size(631, 611);
            this.grpSecurityMapping.TabIndex = 4;
            this.grpSecurityMapping.TabStop = false;
            this.grpSecurityMapping.Tag = "";
            this.grpSecurityMapping.Text = "Security Mapping";
            // 
            // trevSecurity
            // 
            this.trevSecurity.ContextMenu = this.cmnuSecurity;
            this.trevSecurity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trevSecurity.HideSelection = false;
            this.trevSecurity.ImageIndex = 0;
            this.trevSecurity.ImageList = this.imglIcon;
            this.trevSecurity.Location = new System.Drawing.Point(3, 16);
            this.trevSecurity.Name = "trevSecurity";
            this.trevSecurity.SelectedImageIndex = 0;
            this.trevSecurity.Size = new System.Drawing.Size(625, 592);
            this.trevSecurity.TabIndex = 0;
            this.trevSecurity.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trevSecurity_NodeMouseClick);
            this.trevSecurity.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trevSecurity_MouseDown);
            // 
            // cmnuSecurity
            // 
            this.cmnuSecurity.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miAddScreen,
            this.miAddUser,
            this.miAddUserGroup,
            this.menuItem1,
            this.miSelectAll,
            this.miUnselectAll});
            this.cmnuSecurity.Popup += new System.EventHandler(this.cmnuSecurity_Popup);
            // 
            // miAddScreen
            // 
            this.miAddScreen.Index = 0;
            this.miAddScreen.Text = "Add Screen...";
            this.miAddScreen.Click += new System.EventHandler(this.miAddScreen_Click);
            // 
            // miAddUser
            // 
            this.miAddUser.Index = 1;
            this.miAddUser.Text = "Add User...";
            this.miAddUser.Click += new System.EventHandler(this.miAddUser_Click);
            // 
            // miAddUserGroup
            // 
            this.miAddUserGroup.Index = 2;
            this.miAddUserGroup.Text = "Add User Group...";
            this.miAddUserGroup.Click += new System.EventHandler(this.miAddUserGroup_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 3;
            this.menuItem1.Text = "-";
            // 
            // miSelectAll
            // 
            this.miSelectAll.Index = 4;
            this.miSelectAll.Text = "Select All";
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);
            // 
            // miUnselectAll
            // 
            this.miUnselectAll.Index = 5;
            this.miUnselectAll.Text = "Unselect All";
            this.miUnselectAll.Click += new System.EventHandler(this.miUnselectAll_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(344, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 611);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // frmADM040_SecurityMapping
            // 
            this.ClientSize = new System.Drawing.Size(984, 664);
            this.Name = "frmADM040_SecurityMapping";
            this.Text = "ADM040 : Security Mapping";
            this.Load += new System.EventHandler(this.frmADM040_SecurityMapping_Load);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageUser.ResumeLayout(false);
            this.grpBelongtoGroup.ResumeLayout(false);
            this.tabPageUserGroup.ResumeLayout(false);
            this.grpMember.ResumeLayout(false);
            this.tabPageScreen.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpSecurityMapping.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Override Method

        public bool DataLoading()
        {
            return DataLoading(null);
        }

        public override bool DataLoading(object args)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.View);
                ControlUtil.EnabledControl(false, m_toolBarEdit);
                LoadAllUser();
                LoadAllUserGroup();
                LoadAllScreen();
                if (tabControl1.SelectedTab == tabPageUser)
                {
                    if (lstvUser.Items.Count > 0)
                    {
                        lstvUser.Items[0].Selected = true;
                    }
                }
                else if (tabControl1.SelectedTab == tabPageUserGroup)
                {
                    if (lstvUserGroup.Items.Count > 0)
                    {
                        lstvUserGroup.Items[0].Selected = true;
                    }
                }
                else if (tabControl1.SelectedTab == tabPageScreen)
                {
                    if (trevScreen.SelectedNode != null)
                    {
                        if (trevScreen.SelectedNode.Tag != null)
                        {
                            if (trevScreen.SelectedNode.ImageIndex == (int) eImgIndex.Screen)
                            {
                                LoadSecurityOfScreen(Convert.ToInt32(trevScreen.SelectedNode.Tag), trevScreen.SelectedNode.Text);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public override void SetScreenMode(eScreenMode eScreenMode) {

            base.SetScreenMode(eScreenMode);	// สำหรับใน Base class จะมี function สำหรับ Enabled & Disabled กับ Toolbar อยู่แล้ว เรียกใช้ได้เลย
            switch (eScreenMode) {
                case eScreenMode.Idle:
                ControlUtil.ClearControlData(this.Controls);
                ControlUtil.EnabledControl(false, this.Controls);
                tabControl1.Enabled = true;
                trevSecurity.ContextMenu = null;
                break;
                case eScreenMode.View:
                ControlUtil.EnabledControl(false, this.Controls);
                tabControl1.Enabled = true;
                trevSecurity.ContextMenu = null;
                break;
                case eScreenMode.Edit:
                ControlUtil.EnabledControl(true, this.Controls);
                tabControl1.Enabled = false;
                trevSecurity.ContextMenu = cmnuSecurity;
                break;
                case eScreenMode.Add:
                ControlUtil.EnabledControl(true, this.Controls);
                tabControl1.Enabled = false;
                trevSecurity.ContextMenu = cmnuSecurity;
                break;
            }
        }
        public override void PermissionControl() {
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Edit, m_toolBarEdit);
        }
        public override void OnCommandClose() {
            // TODO : Add code for exit command.
            this.Close();
        }
        public override bool OnCommandAdd() {
            try {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Add);
               
                return true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandEdit() {
            try {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Edit);

                m_dlgFindScreen = new FindScreenDialog(new FindDialogSqlDAO(AppEnvironment.Database), true, false);
                m_dlgFindUser = new FindUserDialog(new FindDialogSqlDAO(AppEnvironment.Database), true, false);
                m_dlgFindUserGroup = new FindUserGroupDialog(new FindDialogSqlDAO(AppEnvironment.Database), true, false);
                
                return true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        public override bool OnCommandDelete() {
            try {
                this.Cursor = Cursors.WaitCursor;
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0003)) != DialogButton.Yes)
                {
                    return false;
                }
                SetScreenMode(eScreenMode.Idle);
                // TODO : Add code for delete command.
                return true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandSave() {
            try {
                this.Cursor = Cursors.WaitCursor;

                if (tabControl1.SelectedTab == tabPageUser) {
                    UpdateSecuritOfUser();
                    LoadAllUserGroup();
                    LoadAllScreen();
                }
                else if (tabControl1.SelectedTab == tabPageUserGroup) {
                    UpdateSecuritOfUserGroup();
                    LoadAllUser();
                    LoadAllScreen();
                }
                else if (tabControl1.SelectedTab == tabPageScreen) {
                    UpdateSecurityOfScreen();
                    LoadAllUser();
                    LoadAllUserGroup();
                }

                SetScreenMode(eScreenMode.View);
                return true;
            }
            catch (BusinessException ex) {
                if (AppEnvironment.Database.HasTransaction)
                    AppEnvironment.Database.RollbackTrans();

                ExceptionManager.ManageException(this, ex);
                return false;
            }
            catch (Exception ex) {

                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        public override bool OnCommandCancel() {
            try {
                this.Cursor = Cursors.WaitCursor;
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0002)) != DialogButton.Yes)
                {
                    return false;
                }
                return this.DataLoading();
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandExport() {
            try {
                this.Cursor = Cursors.WaitCursor;
                // TODO : Add code for export command.
                return true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandPrint() {
            try {
                this.Cursor = Cursors.WaitCursor;
                // TODO : Add code for print command.
                return true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandRefresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                return DataLoading();
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Evnet Handler

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e) {
            grpSecurityMapping.Text = string.Empty;
            ControlUtil.EnabledControl(false, m_toolBarEdit);
            trevSecurity.BeginUpdate();
            trevSecurity.Nodes.Clear();
            trevSecurity.EndUpdate();
            this.Cursor = Cursors.WaitCursor;
            try {
                if (tabControl1.SelectedTab == tabPageUser) {
                    if (lstvUser.SelectedItems.Count == 1) {
                        LoadSecurityOfUser(Convert.ToString(lstvUser.SelectedItems[0].Tag), lstvUser.SelectedItems[0].Text);
                    }
                    else {
                        if (lstvUser.Items.Count > 0) {
                            lstvUser.Items[0].Selected = true;
                        }
                    }
                }
                else if (tabControl1.SelectedTab == tabPageUserGroup) {
                    if (lstvUserGroup.SelectedItems.Count == 1) {
                        LoadSecurityOfUserGroup(Convert.ToInt32(lstvUserGroup.SelectedItems[0].Tag), lstvUserGroup.SelectedItems[0].Text);
                    }
                    else {
                        if (lstvUserGroup.Items.Count > 0) {
                            lstvUserGroup.Items[0].Selected = true;
                        }
                    }
                }
                else if (tabControl1.SelectedTab == tabPageScreen) {
                    if (trevScreen.SelectedNode != null) {
                        if (trevScreen.SelectedNode.ImageIndex == (int)eImgIndex.Screen) {
                            LoadSecurityOfScreen(Convert.ToInt32(trevScreen.SelectedNode.Tag), trevScreen.SelectedNode.Text);
                        }
                    }
                    else {
                        if (trevScreen.Nodes.Count > 0) {
                            trevScreen.SelectedNode = trevScreen.Nodes[0];
                        }
                    }
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void miAddUser_Click(object sender, System.EventArgs e) {
            AddUserForScreen();
        }

        private void trevSecurity_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            
        }

        private void trevSecurity_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button != MouseButtons.Left || ScreenMode == eScreenMode.Idle || ScreenMode == eScreenMode.View)
                return;
            if (e.X < e.Node.Bounds.Left - 30 || e.X > e.Node.Bounds.Right) {
                return;
            }
            try {
                TreeNode selNode = e.Node;
                if (selNode == null)
                    return;
                if (selNode.ForeColor == m_clDisable) {
                    return;
                }
                SetPermissionState(selNode);
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void miAddUserGroup_Click(object sender, System.EventArgs e) {
            AddUserGroupForScreen();
        }

        private void lstvUser_SelectedIndexChanged(object sender, System.EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                grpSecurityMapping.Text = string.Empty;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();
                trevSecurity.EndUpdate();
                bool bEnable = (lstvUser.SelectedItems.Count == 1);
                ControlUtil.EnabledControl(bEnable, m_toolBarEdit);
                if (lstvUser.SelectedItems.Count == 1) {
                    LoadSecurityOfUser(Convert.ToString(lstvUser.SelectedItems[0].Tag), lstvUser.SelectedItems[0].Text);
                    LoadBelongGroup(Convert.ToString(lstvUser.SelectedItems[0].Tag));
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void lstvUserGroup_SelectedIndexChanged(object sender, System.EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                grpSecurityMapping.Text = string.Empty;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();
                trevSecurity.EndUpdate();
                //m_toolBarEdit.Enabled = (lstvUserGroup.SelectedItems.Count == 1);
                bool bEnable = (lstvUser.SelectedItems.Count == 1);
                ControlUtil.EnabledControl(bEnable, m_toolBarEdit);
                if (lstvUserGroup.SelectedItems.Count == 1) {
                    LoadSecurityOfUserGroup(Convert.ToInt32(lstvUserGroup.SelectedItems[0].Tag), lstvUserGroup.SelectedItems[0].Text);
                    LoadMemberOfGroup(Convert.ToInt32(lstvUserGroup.SelectedItems[0].Tag));
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void trevScreen_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                grpSecurityMapping.Text = string.Empty;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();
                trevSecurity.EndUpdate();
                if (e.Node.ImageIndex == (int)eImgIndex.Screen) {
                    LoadSecurityOfScreen(Convert.ToInt32(e.Node.Tag), e.Node.Text);
                    ControlUtil.EnabledControl(true, m_toolBarEdit);
                }
                else {
                    trevSecurity.Nodes.Clear();
                    ControlUtil.EnabledControl(false, m_toolBarEdit);
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void miAddScreen_Click(object sender, System.EventArgs e) {
            if (tabControl1.SelectedTab == tabPageUser) {
                AddScreenForUser();
            }
            else if (tabControl1.SelectedTab == tabPageUserGroup) {
                AddScreenForUserGroup();
            }
        }

        private void miSelectAll_Click(object sender, System.EventArgs e) {
            bool bCheck = true;
            AddSelectAllorUnseleteAll(bCheck, trevSecurity.SelectedNode);
        }

        private void miUnselectAll_Click(object sender, System.EventArgs e) {
            bool bCheck = false;
            AddSelectAllorUnseleteAll(bCheck, trevSecurity.SelectedNode);
        }

        private void cmnuSecurity_Popup(object sender, System.EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                TreeNode selNode = trevSecurity.GetNodeAt(trevSecurity.PointToClient(MousePosition));
                trevSecurity.SelectedNode = selNode;

                foreach (MenuItem m in cmnuSecurity.MenuItems) {
                    m.Enabled = false;
                }
                miAddScreen.Enabled = (tabControl1.SelectedTab == tabPageUser || tabControl1.SelectedTab == tabPageUserGroup);
                miAddUser.Enabled = (tabControl1.SelectedTab == tabPageScreen);
                miAddUserGroup.Enabled = miAddUser.Enabled;

                miSelectAll.Enabled = true;
                miUnselectAll.Enabled = true;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }
       
        #endregion

        #region LoadAllData
        private void LoadAllUser() {
            try {
                lstvUser.BeginUpdate();
                lstvUser.Items.Clear();
                //UserDTO data = m_userDao.LoadAll();
                List<UserDTO> datas = BizSecurity.LoadAllUser();
                foreach (UserDTO data in datas) {
                    ListViewItem lv = new ListViewItem(data.Username + "  [" + data.LastName + " " + data.FirstName + "]");
                    lv.ImageIndex = (int)eImgIndex.User;
                    lv.Tag = data.Username;
                    lstvUser.Items.Add(lv);
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                lstvUser.EndUpdate();
            }
        }

        private void LoadAllUserGroup() {
            try {
                lstvUserGroup.BeginUpdate();
                lstvUserGroup.Items.Clear();
                //UserGroupsDTO data = m_userGroupDAO.LoadAll();
                List<UserGroupDTO> datas = BizSecurity.LoadAllUserGroup();
                foreach (UserGroupDTO data in datas) {
                    ListViewItem lv = new ListViewItem(data.GroupName);
                    lv.ImageIndex = (int)eImgIndex.UserGroup;
                    lv.Tag = data.GroupID;
                    lstvUserGroup.Items.Add(lv);
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                lstvUserGroup.EndUpdate();
            }
        }

        private void LoadAllScreen() {
            try {
                trevScreen.BeginUpdate();
                trevScreen.Nodes.Clear();
                ArrayList arSingleNode = new ArrayList();
                List<ScreenDTO> datas = BizSecurity.LoadAllScreen();
                TreeNode nodeLast = null;
                string strOldNameSpace = string.Empty;
                foreach (ScreenDTO data in datas) {
                    string[] secs = data.ClassName.Split('.');
                    if (secs.Length == 1) {
                        arSingleNode.Add(new TreeNode(data.ClassName, (int)eImgIndex.NameSpace, (int)eImgIndex.NameSpace));
                    }
                    else {
                        int index = data.ClassName.LastIndexOf(".");
                        string nameSpace = data.ClassName.Substring(0, index);
                        string className = string.Format("{0}  [ {1} ]", data.DisplayName, data.ClassName);
                        if (strOldNameSpace != nameSpace) {
                            secs = nameSpace.Split('.');
                            if (nodeLast == null) {
                                nodeLast = trevScreen.Nodes.Add(secs[0]);
                                nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                for (int i = 1; i < secs.Length; ++i) {
                                    nodeLast = nodeLast.Nodes.Add(secs[i]);
                                    nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                    nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                }
                            }
                            else {
                                if (secs.Length > strOldNameSpace.Split('.').Length && nameSpace.IndexOf(strOldNameSpace) >= 0) {
                                    for (int i = strOldNameSpace.Split('.').Length; i < secs.Length; ++i) {
                                        nodeLast = nodeLast.Nodes.Add(secs[i]);
                                        nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                        nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                    }
                                }
                                else {
                                    while (true) {
                                        nodeLast = nodeLast.Parent;
                                        if (nodeLast == null) {
                                            nodeLast = trevScreen.Nodes.Add(secs[0]);
                                            nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                            nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                            for (int i = 1; i < secs.Length; ++i) {
                                                nodeLast = nodeLast.Nodes.Add(secs[i]);
                                                nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                                nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                            }
                                            break;
                                        }
                                        else {
                                            if (nodeLast.Text == secs[secs.Length - 2]) {
                                                nodeLast = nodeLast.Nodes.Add(secs[secs.Length - 1]);
                                                nodeLast.ImageIndex = (int)eImgIndex.NameSpace;
                                                nodeLast.SelectedImageIndex = (int)eImgIndex.NameSpace;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            strOldNameSpace = nameSpace;
                        }

                        if (nodeLast != null) {
                            TreeNode nodeNew = nodeLast.Nodes.Add(className);
                            nodeNew.ImageIndex = (int)eImgIndex.Screen;
                            nodeNew.SelectedImageIndex = (int)eImgIndex.Screen;
                            nodeNew.Tag = data.ScreenID;
                        }
                    }
                }// while (data.MoveNext())
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                trevScreen.ExpandAll();
                trevScreen.EndUpdate();
            }
        }

        #endregion

        #region Add screen to user and usergroup, add user and usergroup to screen
        private void AddUserGroupForScreen() {
            this.Cursor = Cursors.WaitCursor;
            try {
                if (m_dlgFindUserGroup.ShowDialog(this) == DialogResult.OK) {
                    int iScreenID = Convert.ToInt32(trevScreen.SelectedNode.Tag);
                    string strClassName = BizSecurity.Load(iScreenID);
                    string[] perms = LoadPermission(strClassName);
                    ArrayList arUserGroupIDs = new ArrayList();
                    // Load current user it and keep in array
                    TreeNode nodeUserGroupLevel = null;
                    foreach (TreeNode nodeLevel in trevSecurity.Nodes) {
                        if (nodeLevel.ImageIndex == (int)eImgIndex.UserGroup) {
                            nodeUserGroupLevel = nodeLevel;
                            foreach (TreeNode nodeUserGroup in nodeUserGroupLevel.Nodes) {
                                arUserGroupIDs.Add(Convert.ToInt32(nodeUserGroup.Tag));
                            }
                        }
                    }// foeach
                    // Load data from find dailog and add to tree view
                    if (nodeUserGroupLevel != null) {
                        SelectedRecordCollection records = m_dlgFindUserGroup.SelectedRecords;
                        foreach (SelectedRecord rec in records) {
                            int iGroupID = Convert.ToInt32(rec[FindUserGroupDialog.eColumns.GroupID.ToString()]);
                            string strGroupName = Convert.ToString(rec[FindUserGroupDialog.eColumns.GroupName.ToString()]);
                            if (arUserGroupIDs.Contains(iGroupID) == false) {
                                TreeNode nodeUserGroup = new TreeNode(strGroupName, (int)eImgIndex.UserGroup, (int)eImgIndex.UserGroup);
                                nodeUserGroup.Tag = iGroupID;
                                foreach (string p in perms) {
                                    nodeUserGroup.Nodes.Add(new TreeNode(p, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                                }
                                nodeUserGroupLevel.Nodes.Add(nodeUserGroup);
                                arUserGroupIDs.Add(iGroupID);
                            }
                        }// end foeach
                    }// end if
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }
        private void AddUserForScreen() {
            this.Cursor = Cursors.WaitCursor;
            try {
                if (m_dlgFindUser.ShowDialog(this) == DialogResult.OK) {
                    int iScreenID = Convert.ToInt32(trevScreen.SelectedNode.Tag);
                    string strClassName = BizSecurity.Load(iScreenID);
                    string[] perms = LoadPermission(strClassName);
                    ArrayList arUserIDs = new ArrayList();
                    // Load current user it and keep in array
                    TreeNode nodeUserLevel = null;
                    foreach (TreeNode nodeLevel in trevSecurity.Nodes) {
                        if (nodeLevel.ImageIndex == (int)eImgIndex.User) {
                            nodeUserLevel = nodeLevel;
                            foreach (TreeNode nodeUser in nodeLevel.Nodes) {
                                arUserIDs.Add(Convert.ToString(nodeUser.Tag));
                            }
                        }
                    }// foeach
                    // Load data from find dailog and add to tree view
                    if (nodeUserLevel != null) {
                        SelectedRecordCollection records = m_dlgFindUser.SelectedRecords;
                        foreach (SelectedRecord rec in records) {
                            string strUserID = Convert.ToString(rec[FindUserDialog.eColumn.Username.ToString()]);
                            if (arUserIDs.Contains(strUserID) == false) {
                                TreeNode nodeUser = new TreeNode(strUserID, (int)eImgIndex.User, (int)eImgIndex.User);
                                nodeUser.Tag = strUserID;
                                foreach (string p in perms) {
                                    nodeUser.Nodes.Add(new TreeNode(p, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                                }
                                nodeUserLevel.Nodes.Add(nodeUser);
                                arUserIDs.Add(strUserID);
                            }
                        }// end foeach
                    }// end if
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void AddScreenForUser() {
            this.Cursor = Cursors.WaitCursor;
            try {
                if (m_dlgFindScreen.ShowDialog(this) == DialogResult.OK) {
                    ArrayList arScreenIDs = new ArrayList();
                    // Load current screen id andd keep in arrray
                    TreeNode nodeUserLevel = null;
                    foreach (TreeNode nodeGroup in trevSecurity.Nodes) {
                        if (nodeGroup.ForeColor != m_clDisable) {
                            foreach (TreeNode nodeScreen in nodeGroup.Nodes) {
                                arScreenIDs.Add(Convert.ToInt32(nodeScreen.Tag));
                            }
                            nodeUserLevel = nodeGroup;
                            break;
                        }
                    }
                    // Load data from find dialog and add to tree view
                    if (nodeUserLevel != null) {
                        SelectedRecordCollection records = m_dlgFindScreen.SelectedRecords;
                        foreach (SelectedRecord rec in records) {
                            int iScreenID = Convert.ToInt32(rec[FindScreenDialog.eColumns.ScreenID.ToString()]);
                            if (arScreenIDs.Contains(iScreenID) == false) {
                                arScreenIDs.Add(iScreenID);
                                TreeNode nodeScreen = nodeUserLevel.Nodes.Add(rec[FindScreenDialog.eColumns.ClassName.ToString()].ToString());
                                nodeScreen.Tag = iScreenID;

                                if (!nodeScreen.Text.ToUpper().Contains(".REPORT")) {
                                    nodeScreen.ImageIndex = (int)eImgIndex.Screen;
                                    nodeScreen.SelectedImageIndex = (int)eImgIndex.Screen;
                                }
                                else {
                                    nodeScreen.ImageIndex = (int)eImgIndex.Report;
                                    nodeScreen.SelectedImageIndex = (int)eImgIndex.Report;
                                }
                                try {
                                    //Type t = Type.GetType(nodeScreen.Text);
                                    string[] perms = LoadPermission(nodeScreen.Text);
                                    //Loop Add Screen
                                    
                                    foreach (string p in perms) {
                                        nodeScreen.Nodes.Add(new TreeNode(p, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                                    }
                                    
                                }
                                catch (Exception ex) {
                                    ExceptionManager.ManageException(this, ex);
                                }

                                nodeScreen.Expand();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void AddScreenForUserGroup() {
            this.Cursor = Cursors.WaitCursor;
            try {
                if (m_dlgFindScreen.ShowDialog(this) == DialogResult.OK) {
                    ArrayList arScreenIDs = new ArrayList();
                    // Load current screen id andd keep in arrray
                    foreach (TreeNode nodeScreen in trevSecurity.Nodes) {
                        arScreenIDs.Add(Convert.ToInt32(nodeScreen.Tag));
                    }
                    // Load data from find dialog and add to tree view
                    SelectedRecordCollection records = m_dlgFindScreen.SelectedRecords;
                    foreach (SelectedRecord rec in records) {
                        int iScreenID = Convert.ToInt32(rec[FindScreenDialog.eColumns.ScreenID.ToString()]);
                        if (arScreenIDs.Contains(iScreenID) == false) {
                            arScreenIDs.Add(iScreenID);
                            TreeNode nodeScreen = trevSecurity.Nodes.Add(rec[FindScreenDialog.eColumns.ClassName.ToString()].ToString());
                            nodeScreen.Tag = iScreenID;
                            nodeScreen.ImageIndex = (int)eImgIndex.Screen;
                            nodeScreen.SelectedImageIndex = (int)eImgIndex.Screen;
                            try {
                                //Type t = Type.GetType(nodeScreen.Text);
                                string[] perms = LoadPermission(nodeScreen.Text);
                                foreach (string p in perms) {
                                    nodeScreen.Nodes.Add(new TreeNode(p, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                                }
                            }
                            catch (Exception ex) {
                                ExceptionManager.ManageException(this, ex);
                            }

                            nodeScreen.Expand();
                        }
                    }
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }


        private void AddSelectAllorUnseleteAll(bool bCheck, TreeNode node) {
            try {
                bool bSetPermission = false;
                int iNodeDeep = node.Level;

                // check leaf node
                if (tabControl1.SelectedTab == tabPageUser) {
                    bSetPermission = iNodeDeep == (int)NodeDeepUserView.Permission;
                }
                else if (tabControl1.SelectedTab == tabPageUserGroup) {
                    bSetPermission = iNodeDeep == (int)NodeDeepUserGroupView.Permission;
                }
                else if (tabControl1.SelectedTab == tabPageScreen) {
                    bSetPermission = iNodeDeep == (int)NodeDeepScreenView.Permission;
                }

                // this is leaf node, set 
                if (bSetPermission) {
                    node.ImageIndex = bCheck ? (int)eImgIndex.Checked : (int)eImgIndex.UnChecked;
                    node.SelectedImageIndex = bCheck ? (int)eImgIndex.Checked : (int)eImgIndex.UnChecked;
                }

                // search for leaf node in child node
                for (int i = 0; i < node.Nodes.Count; i++) {
                    AddSelectAllorUnseleteAll(bCheck, node.Nodes[i]);
                    node.Expand();
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
        }
        #endregion

        #region Load Security
        private void LoadSecurityOfScreen(int iScreenID, string strScreen) {
            try {

                grpSecurityMapping.Text = @"Security mapping for screen " + strScreen;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();

                List<ScreenDTO> sec = BizSecurity.LoadScreen(iScreenID);
                if (sec == null || sec.Count == 0) {
                    return;
                }
                else {
                }
                // Load Permission of screen
                string[] perms = LoadPermission(sec[0].ClassName);
                TreeNode[] nodePerm = new TreeNode[perms.Length];
                for (int i = 0; i < perms.Length; ++i) {
                    nodePerm[i] = new TreeNode(perms[i], (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked);
                }
                // Load security for user level
                TreeNode nodeUserLevel = new TreeNode("User Level", (int)eImgIndex.User, (int)eImgIndex.User);
                TreeNode nodeUser = null;

                List<UserDTO> users = BizSecurity.LoadSecurityOfScreenForUserLevel(iScreenID);
                string strOldUserID = string.Empty;
                foreach (UserDTO user in users) {
                    if (strOldUserID != user.Username) {
                        nodeUser = nodeUserLevel.Nodes.Add(user.Username);
                        nodeUser.ImageIndex = (int)eImgIndex.User;
                        nodeUser.SelectedImageIndex = nodeUser.ImageIndex;
                        foreach (TreeNode n in nodePerm) {
                            nodeUser.Nodes.Add((TreeNode)n.Clone());
                        }
                        nodeUser.Tag = user.Username;
                        strOldUserID = user.Username;
                    }
                    if (nodeUser != null) {
                        foreach (TreeNode n in nodeUser.Nodes) {
                            if (n.Text == user.Permission) {
                                n.ImageIndex = (int)eImgIndex.Checked;
                                n.SelectedImageIndex = n.ImageIndex;
                                break;
                            }
                        }
                    }
                }	// end while
                // Load security for user group level
                TreeNode nodeUserGroupLevel = new TreeNode("User Group Level", (int)eImgIndex.UserGroup, (int)eImgIndex.UserGroup);
                TreeNode nodeGroup = null;

                List<UserGroupDTO> grps = BizSecurity.LoadSecurityOfScreenForUserGroupLevel(iScreenID);
                int iOldGroupID = -1;
                foreach (UserGroupDTO grp in grps) {
                    if (iOldGroupID != grp.GroupID) {
                        nodeGroup = nodeUserGroupLevel.Nodes.Add(grp.GroupName);
                        nodeGroup.ImageIndex = (int)eImgIndex.UserGroup;
                        nodeGroup.SelectedImageIndex = nodeGroup.ImageIndex;
                        foreach (TreeNode n in nodePerm) {
                            nodeGroup.Nodes.Add((TreeNode)n.Clone());
                        }
                        nodeGroup.Tag = grp.GroupID;
                        iOldGroupID = (int)grp.GroupID;
                    }
                    if (nodeGroup != null) {
                        foreach (TreeNode n in nodeGroup.Nodes) {
                            if (n.Text == grp.Permission) {
                                n.ImageIndex = (int)eImgIndex.Checked;
                                n.SelectedImageIndex = n.ImageIndex;
                                break;
                            }
                        }
                    }
                }	// end while
                trevSecurity.Nodes.Add(nodeUserLevel);
                trevSecurity.Nodes.Add(nodeUserGroupLevel);

                if (trevSecurity.TopNode != null)
                    trevSecurity.SelectedNode = trevSecurity.TopNode;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                //trevSecurity.ExpandAll();
                trevSecurity.Nodes[0].ExpandAll();
                trevSecurity.EndUpdate();
            }
        }

        private void LoadSecurityOfUserGroup(int iGroupID, string strGroupName) {
            try {
                ControlUtil.EnabledControl(true, m_toolBarEdit);
                grpSecurityMapping.Text = @"Security mapping for user group " + strGroupName;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();
                // Load security for user level
                List<SecurityMatchDTO> secs = BizSecurity.LoadSecurityOfUserGroup(iGroupID);
                int iOldScreenID = -1;
                TreeNode nodeScreen = null;
                foreach (SecurityMatchDTO sec in secs) {
                    if (iOldScreenID != sec.ScreenID) {
                        iOldScreenID = (int)sec.ScreenID;
                        string strDisplayName = string.Format("{0}  [ {1} ]", sec.DisplayName, sec.ClassName);
                        nodeScreen = new TreeNode(strDisplayName, (int)eImgIndex.Screen, (int)eImgIndex.Screen);
                        nodeScreen.ForeColor = Color.Blue;
                        nodeScreen.Tag = sec.ScreenID;

                        string[] perms = LoadPermission(sec.ClassName);
                        foreach (string perm in perms) {
                            nodeScreen.Nodes.Add(new TreeNode(perm, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                        }
                        trevSecurity.Nodes.Add(nodeScreen);
                    }
                    if (nodeScreen != null) {
                        foreach (TreeNode nodePerm in nodeScreen.Nodes) {
                            if (nodePerm.Text == sec.Permission) {
                                nodePerm.ImageIndex = (int)eImgIndex.Checked;
                                nodePerm.SelectedImageIndex = nodePerm.ImageIndex;
                            }
                        }
                    }
                }// while

                if (trevSecurity.TopNode != null)
                    trevSecurity.SelectedNode = trevSecurity.TopNode;
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                trevSecurity.ExpandAll();
                trevSecurity.EndUpdate();
            }
        }


        private void LoadSecurityOfUser(string strUserID, string strName) {
            try {
                ControlUtil.EnabledControl(true, m_toolBarEdit);

                grpSecurityMapping.Text = @"Security mapping for user " + strName;
                trevSecurity.BeginUpdate();
                trevSecurity.Nodes.Clear();
                TreeNode nodeUserLevel = new TreeNode("User Level", (int)eImgIndex.User, (int)eImgIndex.User);
                TreeNode nodeGroupLevel = new TreeNode("Group Level", (int)eImgIndex.UserGroup, (int)eImgIndex.UserGroup);
                trevSecurity.Nodes.Add(nodeUserLevel);
                trevSecurity.Nodes.Add(nodeGroupLevel);

                // Load security for user level
                List<SecurityMatchDTO> secs = BizSecurity.LoadSecurityOfUser(strUserID);
                int iOldScreenID = -1;
                TreeNode nodeScreen = null;
                foreach (SecurityMatchDTO sec in secs) {
                    if (iOldScreenID != sec.ScreenID) {
                        iOldScreenID = (int)sec.ScreenID;
                        
                        string strDisplayName = string.Format("{0}  [ {1} ]", sec.DisplayName, sec.ClassName);
                        if (!sec.ClassName.ToUpper().Contains(".REPORT")) {
                            nodeScreen = new TreeNode(strDisplayName, (int)eImgIndex.Screen, (int)eImgIndex.Screen);
                        }
                        else {
                            nodeScreen = new TreeNode(strDisplayName, (int)eImgIndex.Report, (int)eImgIndex.Report);
                        }
                        nodeScreen.ForeColor = Color.Blue;
                        nodeScreen.Tag = sec.ScreenID;

                        string[] perms = LoadPermission(sec.ClassName);
                        foreach (string perm in perms) {
                            nodeScreen.Nodes.Add(new TreeNode(perm, (int)eImgIndex.UnChecked, (int)eImgIndex.UnChecked));
                        }
                        nodeUserLevel.Nodes.Add(nodeScreen);
                    }
                    if (nodeScreen != null) {
                        foreach (TreeNode nodePerm in nodeScreen.Nodes) {
                            if (nodePerm.Text == sec.Permission) {
                                nodePerm.ImageIndex = (int)eImgIndex.Checked;
                                nodePerm.SelectedImageIndex = nodePerm.ImageIndex;
                            }
                        }
                    }
                }// while

                // Load security for user group level
                secs = BizSecurity.LoadSecurityOfUserAssignByGroupLevel(strUserID);
                iOldScreenID = -1;
                nodeScreen = null;
                foreach (SecurityMatchDTO sec in secs) {
                    if (iOldScreenID != sec.ScreenID) {
                        iOldScreenID = (int)sec.ScreenID;
                        nodeScreen = new TreeNode(sec.ClassName, (int)eImgIndex.Screen, (int)eImgIndex.Screen);
                        nodeScreen.Tag = sec.ScreenID;
                        nodeScreen.ForeColor = m_clDisable;

                        string[] perms = BizSecurity.LoadPermission(Type.GetType(sec.ClassName));
                        foreach (string perm in perms) {
                            TreeNode n = nodeScreen.Nodes.Add(perm);
                            n.ImageIndex = (int)eImgIndex.UnChecked;
                            n.SelectedImageIndex = n.ImageIndex;
                            n.ForeColor = m_clDisable;
                        }
                        nodeGroupLevel.Nodes.Add(nodeScreen);
                    }
                    if (nodeScreen != null) {
                        foreach (TreeNode nodePerm in nodeScreen.Nodes) {
                            if (nodePerm.Text == sec.Permission) {
                                nodePerm.ImageIndex = (int)eImgIndex.Checked;
                                nodePerm.SelectedImageIndex = nodePerm.ImageIndex;
                            }
                        }
                    }
                }// while

            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                trevSecurity.Nodes[0].ExpandAll();
                trevSecurity.EndUpdate();
            }
        }

        private void LoadMemberOfGroup(int iGroupID) {
            try {
                lstvMemberOfGroup.BeginUpdate();
                lstvMemberOfGroup.Items.Clear();

                List<UserDTO> datas = BizSecurity.LoadMember(iGroupID);
                foreach (UserDTO data in datas)
                {
                    string user = data.Username + " (" + data.FirstName + "  " + data.LastName + ")";
                    lstvMemberOfGroup.Items.Add(user, (int)eImgIndex.User);
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                lstvMemberOfGroup.EndUpdate();
            }
        }

        private void LoadBelongGroup(string strUserID) {
            try {
                lstvBelongGroup.BeginUpdate();
                lstvBelongGroup.Items.Clear();
                
                List<UserGroupDTO> datas = BizSecurity.LoadGroup(strUserID);
                foreach (UserGroupDTO data in datas) {
                    lstvBelongGroup.Items.Add(data.GroupName, (int)eImgIndex.UserGroup);
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
            finally {
                lstvBelongGroup.EndUpdate();
            }
        }
        
        private string[] LoadPermission(string strClassName) {
                        
            string strAsmFilePath = string.Empty;
            if (strClassName.StartsWith("Presentation.")) {
                strAsmFilePath = Path.Combine(Application.StartupPath, "Presentation.dll");
            }

            Assembly asm = null;
            string[] perms = null;
            try {
                asm = Assembly.LoadFile(strAsmFilePath);
                perms = BizSecurity.LoadPermission(asm.GetType(strClassName));                
            }
            catch
            {
                // ignored
            }
                        
            return perms;
        }
        #endregion

        #region Generic Function
        private int GetNodeDeep(TreeNode node) {
            int iCount = 0;
            TreeNode tmpNode = node.Parent;
            while (tmpNode != null) {
                tmpNode = tmpNode.Parent;
                iCount++;
            }
            return iCount;
        }

        private void SetPermissionState(TreeNode selNode) {
            try {
                if (selNode == null)
                    return;
                int iNodeDeep = GetNodeDeep(selNode);
                bool bSetPermission = false;
                if (tabControl1.SelectedTab == tabPageUser) {
                    bSetPermission = iNodeDeep == (int)NodeDeepUserView.Permission;
                }
                else if (tabControl1.SelectedTab == tabPageUserGroup) {
                    bSetPermission = iNodeDeep == (int)NodeDeepUserGroupView.Permission;
                }
                else if (tabControl1.SelectedTab == tabPageScreen) {
                    bSetPermission = iNodeDeep == (int)NodeDeepScreenView.Permission;
                }

                if (bSetPermission) {
                    bool bAllowPermission = !(selNode.ImageIndex == (int)eImgIndex.Checked);
                    int iImageIndex = bAllowPermission ? (int)eImgIndex.Checked : (int)eImgIndex.UnChecked;
                    selNode.ImageIndex = iImageIndex;
                    selNode.SelectedImageIndex = selNode.ImageIndex;
                }
            }
            catch (Exception ex) {
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion

        #region Update Security
        //Modify by Wirachai T. 2007/07/10 : Add BeginTrans, Commit, Rollback
        private void UpdateSecurityOfScreen() {
            try {
                AppEnvironment.Database.BeginTrans();

                int iScreenID = Convert.ToInt32(trevScreen.SelectedNode.Tag);
                // get permission for User level & UserGroup
                ArrayList arSecUser = new ArrayList();
                ArrayList arSecUserGroup = new ArrayList();
                foreach (TreeNode nodeLevel in trevSecurity.Nodes) {
                    eImgIndex index = (eImgIndex)nodeLevel.ImageIndex;
                    foreach (TreeNode nodeItem in nodeLevel.Nodes) {
                        foreach (TreeNode nodePerm in nodeItem.Nodes) {
                            if (nodePerm.ImageIndex == (int)eImgIndex.Checked) {
                                SecurityMatchDTO sec = null;
                                switch (index) {
                                    case eImgIndex.User:
                                    sec = new SecurityMatchDTO();
                                    sec.Username = Convert.ToString(nodeItem.Tag);
                                    sec.Permission = nodePerm.Text;
                                    arSecUser.Add(sec);
                                    break;
                                    case eImgIndex.UserGroup:
                                    sec = new SecurityMatchDTO();
                                    sec.GroupID = Convert.ToInt32(nodeItem.Tag);
                                    sec.Permission = nodePerm.Text;
                                    arSecUserGroup.Add(sec);
                                    break;
                                }
                            }// end if
                        }// end foreach
                    }// end foreach
                }// end foreach
                // start to saving data
                SecurityMatchDTO[] secUsers = new SecurityMatchDTO[arSecUser.Count];
                SecurityMatchDTO[] secGroups = new SecurityMatchDTO[arSecUserGroup.Count];
                arSecUser.CopyTo(secUsers);
                arSecUserGroup.CopyTo(secGroups);
                //m_secDao.UpdateSecurityOfScreen(iScreenID, secUsers, secGroups, AppEnvironment.UserID);
                BizSecurity.UpdateSecurityOfScreen(iScreenID, secUsers, secGroups, AppEnvironment.UserLogin);

                AppEnvironment.Database.CommitTrans();
            }
            catch (Exception ex) {
                AppEnvironment.Database.RollbackTrans();
                throw ex;
            }
        }

        //Modify by Wirachai T. 2007/07/10 : Add BeginTrans, Commit, Rollback
        private void UpdateSecuritOfUserGroup() {
            try {
                AppEnvironment.Database.BeginTrans();

                int iGroupID = Convert.ToInt32(lstvUserGroup.SelectedItems[0].Tag);
                ArrayList arSecurity = new ArrayList();
                foreach (TreeNode nodeScreen in trevSecurity.Nodes) {
                    int iScreenID = Convert.ToInt32(nodeScreen.Tag);
                    foreach (TreeNode nodePerm in nodeScreen.Nodes) {
                        if (nodePerm.ImageIndex == (int)eImgIndex.Checked) {
                            SecurityMatchDTO d = new SecurityMatchDTO();
                            d.ScreenID = iScreenID;
                            d.Permission = nodePerm.Text;
                            arSecurity.Add(d);
                        }
                    }
                }
                SecurityMatchDTO[] data = new SecurityMatchDTO[arSecurity.Count];
                arSecurity.CopyTo(data);
                //m_secDao.UpdateSecurityOfUserGroup(iGroupID, data, AppEnvironment.UserID);
                BizSecurity.UpdateSecurityOfUserGroup(iGroupID, data, AppEnvironment.UserLogin);
                AppEnvironment.Database.CommitTrans();
            }
            catch (Exception ex) {
                AppEnvironment.Database.RollbackTrans();
                throw ex;
            }
        }

        //Modify by Wirachai T. 2007/07/10 : Add BeginTrans, Commit, Rollback
        private void UpdateSecuritOfUser() {
            try {
                AppEnvironment.Database.BeginTrans();

                string strUserID = Convert.ToString(lstvUser.SelectedItems[0].Tag);
                ArrayList arSecurity = new ArrayList();
                foreach (TreeNode nodeLevel in trevSecurity.Nodes) {
                    if (nodeLevel.ForeColor != m_clDisable) {
                        foreach (TreeNode nodeScreen in nodeLevel.Nodes) {
                            int iScreenID = Convert.ToInt32(nodeScreen.Tag);
                            foreach (TreeNode nodePerm in nodeScreen.Nodes) {
                                if (nodePerm.ImageIndex == (int)eImgIndex.Checked) {
                                    SecurityMatchDTO d = new SecurityMatchDTO();
                                    d.ScreenID = iScreenID;
                                    d.Permission = nodePerm.Text;
                                    arSecurity.Add(d);
                                }
                            }
                        }
                        break;
                    }
                }
                SecurityMatchDTO[] data = new SecurityMatchDTO[arSecurity.Count];
                arSecurity.CopyTo(data);
                //m_secDao.UpdateSecurityOfUser(strUserID, data, AppEnvironment.UserID);
                BizSecurity.UpdateSecurityOfUser(strUserID, data, AppEnvironment.UserLogin);
                AppEnvironment.Database.CommitTrans();
            }
            catch (Exception ex) {
                AppEnvironment.Database.RollbackTrans();
                throw ex;
            }
        }

        #endregion

        private void frmADM040_SecurityMapping_Load(object sender, System.EventArgs e) {
            DataLoading();
        }
    }
}
