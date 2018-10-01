#region Using namespace
using System;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using Common;
using System.Data;
using BusinessService;
using DataObject;

using EAP;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using EAP.Framework.Windows.Forms;
using Presentation.Forms.FindDialog;

#endregion

namespace Presentation.Forms.Admin
{
    [ScreenPermission(Permission.OpenScreen, Permission.Add, Permission.Edit, Permission.Delete)]    
    public class FrmADM030_UserGroupMaintenance : LUBEFormDev
    {

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpAddEdit = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lstvMember = new System.Windows.Forms.ListView();
            this.clhMembers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gvResult = new System.Windows.Forms.DataGridView();
            this.grcGroupID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpAddEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitContainer1);
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContainer.Size = new System.Drawing.Size(1127, 634);
            // 
            // grpAddEdit
            // 
            this.grpAddEdit.Controls.Add(this.splitContainer2);
            this.grpAddEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAddEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.grpAddEdit.Location = new System.Drawing.Point(0, 0);
            this.grpAddEdit.Name = "grpAddEdit";
            this.grpAddEdit.Size = new System.Drawing.Size(1121, 374);
            this.grpAddEdit.TabIndex = 3;
            this.grpAddEdit.TabStop = false;
            this.grpAddEdit.Tag = "[I0V0E1A1]";
            this.grpAddEdit.Text = "Add/Edit";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblGroupName);
            this.splitContainer2.Panel1.Controls.Add(this.lblDescription);
            this.splitContainer2.Panel1.Controls.Add(this.txtGroupName);
            this.splitContainer2.Panel1.Controls.Add(this.txtDescription);
            this.splitContainer2.Panel1MinSize = 300;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstvMember);
            this.splitContainer2.Panel2.Controls.Add(this.btnRemove);
            this.splitContainer2.Panel2.Controls.Add(this.btnAdd);
            this.splitContainer2.Size = new System.Drawing.Size(1115, 355);
            this.splitContainer2.SplitterDistance = 300;
            this.splitContainer2.TabIndex = 4;
            // 
            // lblGroupName
            // 
            this.lblGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblGroupName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGroupName.Location = new System.Drawing.Point(3, 15);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(100, 20);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "Group Name";
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescription.Location = new System.Drawing.Point(3, 42);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 20);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtGroupName.Location = new System.Drawing.Point(109, 15);
            this.txtGroupName.MaxLength = 20;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(181, 24);
            this.txtGroupName.TabIndex = 1;
            this.txtGroupName.Tag = "";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDescription.Location = new System.Drawing.Point(109, 42);
            this.txtDescription.MaxLength = 50;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(181, 24);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Tag = "";
            this.txtDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescription_KeyPress);
            // 
            // lstvMember
            // 
            this.lstvMember.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhMembers,
            this.clhFirstName,
            this.clhLastName});
            this.lstvMember.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lstvMember.FullRowSelect = true;
            this.lstvMember.GridLines = true;
            this.lstvMember.Location = new System.Drawing.Point(3, 3);
            this.lstvMember.Name = "lstvMember";
            this.lstvMember.Size = new System.Drawing.Size(667, 349);
            this.lstvMember.TabIndex = 4;
            this.lstvMember.UseCompatibleStateImageBehavior = false;
            this.lstvMember.View = System.Windows.Forms.View.Details;
            // 
            // clhMembers
            // 
            this.clhMembers.Text = "Members";
            this.clhMembers.Width = 150;
            // 
            // clhFirstName
            // 
            this.clhFirstName.Text = "First Name";
            this.clhFirstName.Width = 180;
            // 
            // clhLastName
            // 
            this.clhLastName.Text = "Last Name";
            this.clhLastName.Width = 180;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnRemove.Image = global::Presentation.Properties.Resources.Delete;
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(688, 55);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(110, 43);
            this.btnRemove.TabIndex = 43;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Image = global::Presentation.Properties.Resources.Add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(688, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 43);
            this.btnAdd.TabIndex = 42;
            this.btnAdd.Text = "  Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gvResult
            // 
            this.gvResult.AllowUserToAddRows = false;
            this.gvResult.AllowUserToDeleteRows = false;
            this.gvResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvResult.ColumnHeadersHeight = 26;
            this.gvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.grcGroupID2,
            this.colGroupName,
            this.colDescription,
            this.colCreateDate,
            this.colCreateBy,
            this.colUpdateDate,
            this.colUpdateUser});
            this.gvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvResult.Location = new System.Drawing.Point(0, 0);
            this.gvResult.MultiSelect = false;
            this.gvResult.Name = "gvResult";
            this.gvResult.ReadOnly = true;
            this.gvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvResult.Size = new System.Drawing.Size(1121, 250);
            this.gvResult.TabIndex = 22;
            this.gvResult.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvResult_RowEnter);
            // 
            // grcGroupID2
            // 
            this.grcGroupID2.DataPropertyName = "GroupID";
            this.grcGroupID2.HeaderText = "GroupID";
            this.grcGroupID2.Name = "grcGroupID2";
            this.grcGroupID2.ReadOnly = true;
            this.grcGroupID2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.grcGroupID2.Visible = false;
            // 
            // colGroupName
            // 
            this.colGroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colGroupName.DataPropertyName = "GroupName";
            this.colGroupName.FillWeight = 50F;
            this.colGroupName.HeaderText = "Group Name";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.ReadOnly = true;
            this.colGroupName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.FillWeight = 50F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCreateDate
            // 
            this.colCreateDate.DataPropertyName = "CreateDate";
            this.colCreateDate.HeaderText = "Created Date";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.ReadOnly = true;
            this.colCreateDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCreateDate.Width = 120;
            // 
            // colCreateBy
            // 
            this.colCreateBy.DataPropertyName = "CreateUser";
            this.colCreateBy.HeaderText = "Created By";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.ReadOnly = true;
            this.colCreateBy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCreateBy.Width = 120;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.DataPropertyName = "UpdateDate";
            this.colUpdateDate.HeaderText = "Updated Date";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.ReadOnly = true;
            this.colUpdateDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUpdateDate.Width = 120;
            // 
            // colUpdateUser
            // 
            this.colUpdateUser.DataPropertyName = "UpdateUser";
            this.colUpdateUser.HeaderText = "Updated User";
            this.colUpdateUser.Name = "colUpdateUser";
            this.colUpdateUser.ReadOnly = true;
            this.colUpdateUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUpdateUser.Width = 120;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gvResult);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpAddEdit);
            this.splitContainer1.Size = new System.Drawing.Size(1121, 628);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 23;
            // 
            // FrmADM030_UserGroupMaintenance
            // 
            this.ClientSize = new System.Drawing.Size(1127, 681);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmADM030_UserGroupMaintenance";
            this.Text = "ADM030 : UserGroup Maintenance";
            this.Load += new System.EventHandler(this.frmADM003_UserGroupMaintenance_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmADM003_UserGroupMaintenance_KeyPress);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpAddEdit.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvResult)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Enumeration
        // TODO : ส่วนนี้จะสร้าง Enumeration ของ Column ที่ใช้ใน Sheet View ซึ่งก็ขึ้นอยู่กับว่าแต่ละ Sheet View มีอะไรบ้าง
        // นี่เป็นตัวอย่าง
        private enum eColDetail
        {
            GroupID,
            Name,
            Description,
            CreateDate,
            CreateUser,
            UpdateDate,            
            UpdateUser
        }
        #endregion

        #region Member
        /// <summary>
        /// ใช้สำหรับเก็บตำแหน่งแถวสุดท้ายของ Sheet View ก่อนที่จะมีการ Add แถวใหม่เข้าไป, 
        /// ซึ่งเอาไว้ใช้เพื่อให้ Sheet View แสดงตำแหน่งแถวที่ Active ได้ถูกต้อง ในกรณีที่กด Add แล้ว Cancel
        /// </summary>
        private int m_iOldRowIndex = -1;
        /// <summary>
        /// ใช้เพื่อเป็นตัวบอกว่า ณ. ขณะนี้ Mouse ได้ถูก Click ลงบน Spread หรือเปล่า (Mouse Down)
        /// </summary>
        private bool m_bIsMouseDown = false;
        private System.Windows.Forms.GroupBox grpAddEdit;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblGroupName;
        //private Cube.AFD.Windows.Controls.AFDListView lstvMember;
        private System.Windows.Forms.ListView lstvMember;
        private System.Windows.Forms.ColumnHeader clhMembers;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtGroupName;
        /// <summary>
        /// ใช้เพื่อบอกตำแหน่งของแถวบน Sheet View ที่ Mouse Click ลงไป
        /// </summary>
        private int m_iMouseDownRowIndex = -1;
        //private IUserGroupsDAO m_userGroupDao;
        private FindUserDialog m_dlgFindUser = null;
        private Button btnAdd;
        private Button btnRemove;
        private DataGridView gvResult;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private ColumnHeader clhFirstName;
        private ColumnHeader clhLastName;
        private DataGridViewTextBoxColumn grcGroupID2;
        private DataGridViewTextBoxColumn colGroupName;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colCreateDate;
        private DataGridViewTextBoxColumn colCreateBy;
        private DataGridViewTextBoxColumn colUpdateDate;
        private DataGridViewTextBoxColumn colUpdateUser;
        private UserGroupBIZ m_bizUserGroup;
        
        #endregion

        #region UserGroup
        public UserGroupBIZ BizUserGroup {
            get {
                if (m_bizUserGroup == null)
                    m_bizUserGroup = new UserGroupBIZ(AppEnvironment.Database);
                return m_bizUserGroup;
            }
        }
        #endregion

        #region Constructor
        public FrmADM030_UserGroupMaintenance()
        {
            InitializeComponent();
            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarPrint, m_toolBarImport, m_toolBarExport);
        }
        #endregion

        #region Override Member
        /// <summary>
        /// Function นี้จะถูกเรียอกเมื่อผู้ใช้ Click ที่ ToolbarAdd
        /// </summary>
        /// <returns>Bool</returns>
        public override bool OnCommandAdd()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Add);
                ControlUtil.ClearControlData(grpAddEdit.Controls);
                lstvMember.Items.Clear();
                ControlUtil.EnabledControl(true, grpAddEdit.Controls);

                // TODO : กำหนดค่าเริ่มต้นให้กับ Control ต่าง ๆ ที่อยู่ในกลุ่ม Add/Edit 
                // Example :
                txtGroupName.Text = string.Empty;
                txtDescription.Text = string.Empty;

                 // TODO : กำหนดค่าเริ่มต้นให้กับ Column ต่าง ๆ                
                gvResult.Enabled = false;

                gvResult.ClearSelection();
                
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

        /// <summary>
        /// Function นี้จะถูกเรียกเมื่อผู้ใช้ Click ที่ ToolbarCancel
        /// </summary>
        /// <returns></returns>
        public override bool OnCommandCancel()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // TODO : แสดงข้อความ เพื่อยืนยันการ Cancel
                // Example :
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0002)) == DialogButton.Yes)
                {                   
                    if (ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
                    {
                        OnCommandRefresh();
                    }                    
                    else
                    {
                        SetScreenMode(eScreenMode.Idle);
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
                gvResult.Enabled = true;
            }
        }

        public override bool OnCommandDelete()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvResult.RowCount <= 0)
                    return false;

                if (gvResult.CurrentRow == null)
                    return false;

                // TODO : แสดงข้อความเพื่อยืนยันการลบข้อมูล
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0003)) == DialogButton.Yes)
                {

                    
                    // TODO : delete selected data from data base.

                    //m_userGroupDao.Delete(Convert.ToInt32(shtDetail.Cells[iActiveRowIndex, (int)eColDetail.GroupID].Value));
                    BizUserGroup.Delete(Convert.ToInt32(gvResult.CurrentRow.Cells[(int)eColDetail.GroupID].Value));
                    ControlUtil.ClearControlData(grpAddEdit.Controls);
                    lstvMember.Items.Clear();
                    if (gvResult.RowCount > 0)
                    {                        
                        gvResult.Rows.Remove(gvResult.CurrentRow);
                        OnSelectedRowIndexChanged();
                    }
                    else
                    {
                        SetScreenMode(eScreenMode.Idle);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
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

        public override bool OnCommandEdit()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Edit);
                ControlUtil.EnabledControl(true, grpAddEdit.Controls);

                txtGroupName.Focus();
                txtGroupName.SelectAll();
                gvResult.Enabled = false;


                DataRow dr = ((DataTable) gvResult.DataSource).Rows[gvResult.CurrentRow.Index];
                dr[(int)eColDetail.UpdateDate] = DateTime.Now;
                dr[(int)eColDetail.UpdateUser] = AppEnvironment.UserLogin;
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

        public override void OnCommandClose()
        {
            this.Close();
        }

        public override bool OnCommandExport()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // TODO : สร้างคำสั่งสำหรับการ Export ข้อมูลของคุณ ถ้าไม่มีการ Export ก็ไม่ต้องเขียน code ในส่วนนี้
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

        public override bool OnCommandPrint()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // TODO : สร้างคำสั่งสำหรับการ Print ข้อมูลของคุณ ถ้าไม่มีการ Print ก็ไม่ต้องเขียน code ในส่วนนี้
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

        private string[] UserIDFromListView()
        {
            string[] users = new string[lstvMember.Items.Count];
            int iCount = 0;
            foreach (ListViewItem lv in lstvMember.Items)
            {
                users[iCount++] = lv.Text;
            }
            return users;
        }

        public override bool OnCommandSave()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // do you want to save?
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0001)) != DialogButton.Yes)
                {
                    return false;
                }
               
                AppEnvironment.Database.BeginTrans();
                // End

                // TODO : ถ้าเกิดตรวจสอบข้อมูลเรียบร้อยแล้ว ก็จะทำการ save โดยขึ้นอยู่กับ screen mode
                UserGroupDTO data = new UserGroupDTO();
                data.GroupName = txtGroupName.Text;
                data.Description = txtDescription.Text;
                data.CreateUser = AppEnvironment.UserLogin;
                data.UpdateUser = AppEnvironment.UserLogin;
                data.GroupID = -1;
                switch (ScreenMode)
                {
                    case eScreenMode.Add:
                        data.GroupID = BizUserGroup.AddNew(data);
                        break;
                    case eScreenMode.Edit:
                        data.GroupID = Convert.ToInt32(gvResult.CurrentRow.Cells[(int) eColDetail.GroupID].Value);
                        BizUserGroup.Update(data);
                        break;
                }
                // Update GroupMapping                
                BizUserGroup.UpdateGroupMapping(data.GroupID, UserIDFromListView());

                // Wirachai T. 2008 05 30
                AppEnvironment.Database.CommitTrans();
                // End


                // ปรับปรุง mode ของ screen ปัจจบัน
                OnCommandRefresh();
                SetScreenMode(eScreenMode.View);

                return true;
            }
            catch (BusinessException ex)
            {
                if (AppEnvironment.Database.HasTransaction)
                    AppEnvironment.Database.RollbackTrans();

                ExceptionManager.ManageException(this, ex);
                return false;
            }
            catch (Exception ex)
            {
                if (AppEnvironment.Database.HasTransaction)
                    AppEnvironment.Database.RollbackTrans();

                ExceptionManager.ManageException(this, ex);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                gvResult.Enabled = true;
            }
        }

        public override bool OnCommandRefresh()
        {
            return DataLoading();
        }

        public bool DataLoading()
        {
            return DataLoading(null);
        }

        public override bool DataLoading(object args)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                List<UserGroupDTO> datas = BizUserGroup.SearchData(null, null, null);

                DataTable dtb = DataUtil.ConvertListToDataTable(datas);
                gvResult.DataSource = dtb;
                
                lstvMember.Items.Clear();
                OnSelectedRowIndexChanged();

                if (gvResult.RowCount > 0)
                {
                    SetScreenMode(eScreenMode.View);
                }
                else
                {
                    SetScreenMode(eScreenMode.Idle);
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
      public override void SetScreenMode(eScreenMode eScreenMode)
        {
            base.SetScreenMode(eScreenMode);	// สำหรับใน Base class จะมี function สำหรับ Enabled & Disabled กับ Toolbar อยู่แล้ว เรียกใช้ได้เลย

            // TODO : Enabled & Disabled Control ต่าง ๆ ตาม mode ของ Screen
            m_dlgFindUser = new FindUserDialog(new FindDialogSqlDAO(AppEnvironment.Database), true, true);
            
            switch (ScreenMode)
            {
                case eScreenMode.Idle:
                    ControlUtil.EnabledControl(false, grpAddEdit);
                    ControlUtil.ClearControlData(grpAddEdit.Controls);
                    lstvMember.Items.Clear();
                    break;
                case eScreenMode.View:
                    ControlUtil.EnabledControl(false, grpAddEdit);
                    break;
                case eScreenMode.Add:
                    ControlUtil.EnabledControl(true, grpAddEdit);
                    lstvMember.Items.Clear();
                    break;
                case eScreenMode.Edit:
                    ControlUtil.EnabledControl(true, grpAddEdit);
                    break;
            }
        }
        public override void PermissionControl()
        {
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Add, m_toolBarAdd);
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Edit, m_toolBarEdit);
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Delete, m_toolBarDelete);
        }
        #endregion

        #region Generic Function
       

        private void OnSelectedRowIndexChanged()
        {
            if (gvResult.SelectedRows.Count <= 0)
                return;



            this.txtGroupName.Text = Convert.ToString(gvResult.SelectedRows[0].Cells[(int) eColDetail.Name].Value);
            this.txtDescription.Text = Convert.ToString(gvResult.SelectedRows[0].Cells[(int) eColDetail.Description].Value);
            LoadMember(Convert.ToInt32(gvResult.SelectedRows[0].Cells[(int)eColDetail.GroupID].Value));
        }

        private void LoadMember(int iGroupID)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                lstvMember.Items.Clear();
                //UserDTO data = m_userGroupDao.LoadMember(iGroupID);
                List<UserDTO> datas = BizUserGroup.LoadMember(iGroupID);
                
                foreach (UserDTO data in datas)
                {
                    ListViewItem lv = new ListViewItem(data.Username);
                    lv.SubItems.AddRange(new string[] { data.FirstName, data.LastName });

                    lstvMember.Items.Add(lv);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Event Handler Function               

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (m_dlgFindUser.ShowDialog(this) == DialogResult.OK)
                {                                       
                    SelectedRecordCollection records = m_dlgFindUser.SelectedRecords;
                    foreach (SelectedRecord line in records)
                    {
                        string strUsername = Convert.ToString(line[FindUserDialog.eColumn.Username.ToString()]);

                        bool bFoundItem = false;
                        foreach (ListViewItem item in lstvMember.Items)
                        {
                            if (item.Text == strUsername)
                            {
                                bFoundItem = true;
                                break;
                            }
                        }

                        if (!bFoundItem)
                        {
                            string firstName = Convert.ToString(line[FindUserDialog.eColumn.FirstName.ToString()]);
                            string lastName = Convert.ToString(line[FindUserDialog.eColumn.LastName.ToString()]);

                            ListViewItem lv = new ListViewItem(strUsername);
                            lv.SubItems.AddRange(new[] { firstName, lastName });
                            lstvMember.Items.Add(lv);
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem lv in lstvMember.SelectedItems)
            {
                lv.Remove();
            }
        }
        
        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
            {
                if (e.KeyChar == '\r')
                {
                    this.OnCommandSave();
                }
            }
        }

        private void gvResult_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (gvResult.RowCount > 0)
            {
                OnSelectedRowIndexChanged();
            }
        }

        #endregion

        #region Form Events

        private void frmADM003_UserGroupMaintenance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
            {
                if (e.KeyChar == '\r')
                {
                    while (!this.SelectNextControl(this.ActiveControl, true, true, true, true))
                    {

                    }
                }
            }
        }     

        private void frmADM003_UserGroupMaintenance_Load(object sender, EventArgs e)
        {
            gvResult.AutoGenerateColumns = false;

            colCreateDate.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            colUpdateDate.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";

            OnCommandRefresh();
        }

        #endregion

       
    }
}
