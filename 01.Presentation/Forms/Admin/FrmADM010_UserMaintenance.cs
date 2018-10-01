#region Using namespace
using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using BusinessService;
using Common;
using DataObject;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using EAP.Framework.Windows.Forms;
using Presentation.Forms.FindDialog;

#endregion

namespace Presentation.Forms.Admin
{

    [ScreenPermission(Permission.OpenScreen, Permission.Add, Permission.Edit, Permission.Delete)]
    public class FrmADM010_UserMaintenance : LUBEFormDev
    {
        #region Member
        private System.Windows.Forms.Label lblUserUpdate;
        private System.Windows.Forms.Label lblUpdateDate;
        private System.Windows.Forms.Label lblCreateUser;
        private System.Windows.Forms.Label lblCreateDate;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtCreateDate;
        private System.Windows.Forms.TextBox txtUpdateDate;
        private System.Windows.Forms.TextBox txtUpdateUser;
        private System.Windows.Forms.TextBox txtCreateUser;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
                
        private FindUserDialog m_dlgFindUser = null;
        private GroupBox grpUser;
        private Label label3;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private TextBox txtCompanyName;
        private TextBox txtDepartmentName;

        //private FindUserGroupDialog m_dlgFindUserGroup = null;

        UserBIZ m_bizUser;        

        #endregion

        #region Enum

        #endregion

        #region Controls

        #endregion
       
        #region Constructor
        public FrmADM010_UserMaintenance()
        {
            InitializeComponent();

            m_bizUser = new UserBIZ(AppEnvironment.Database);            

            ControlUtil.VisibleControl(false, m_toolBarExport, m_toolBarImport, m_toolBarPrint);
            ControlUtil.VisibleControl(false,m_toolBarPrint);
            ControlUtil.VisibleControl(true, m_toolBarExport);
            ControlUtil.EnabledControl(false, this.Controls);
        }
        #endregion

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblUserUpdate = new System.Windows.Forms.Label();
            this.lblUpdateDate = new System.Windows.Forms.Label();
            this.lblCreateUser = new System.Windows.Forms.Label();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtCreateDate = new System.Windows.Forms.TextBox();
            this.txtUpdateDate = new System.Windows.Forms.TextBox();
            this.txtUpdateUser = new System.Windows.Forms.TextBox();
            this.txtCreateUser = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDepartmentName = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpUser);
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContainer.Size = new System.Drawing.Size(905, 462);
            // 
            // lblUserUpdate
            // 
            this.lblUserUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblUserUpdate.Location = new System.Drawing.Point(408, 232);
            this.lblUserUpdate.Name = "lblUserUpdate";
            this.lblUserUpdate.Size = new System.Drawing.Size(100, 26);
            this.lblUserUpdate.TabIndex = 23;
            this.lblUserUpdate.Text = "Changed By";
            this.lblUserUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUpdateDate
            // 
            this.lblUpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblUpdateDate.Location = new System.Drawing.Point(18, 232);
            this.lblUpdateDate.Name = "lblUpdateDate";
            this.lblUpdateDate.Size = new System.Drawing.Size(100, 26);
            this.lblUpdateDate.TabIndex = 21;
            this.lblUpdateDate.Text = "Last Changed";
            this.lblUpdateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCreateUser.Location = new System.Drawing.Point(408, 203);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(100, 26);
            this.lblCreateUser.TabIndex = 19;
            this.lblCreateUser.Text = "Created By";
            this.lblCreateUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCreateDate.Location = new System.Drawing.Point(18, 203);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(100, 26);
            this.lblCreateDate.TabIndex = 17;
            this.lblCreateDate.Text = "Created Date";
            this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFirstName
            // 
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblFirstName.Location = new System.Drawing.Point(16, 52);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(100, 26);
            this.lblFirstName.TabIndex = 5;
            this.lblFirstName.Text = "First Name";
            this.lblFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserID
            // 
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblUserID.Location = new System.Drawing.Point(16, 20);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(100, 26);
            this.lblUserID.TabIndex = 0;
            this.lblUserID.Text = "User Name";
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastName
            // 
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblLastName.Location = new System.Drawing.Point(402, 52);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(100, 26);
            this.lblLastName.TabIndex = 7;
            this.lblLastName.Text = "Last Name";
            this.lblLastName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblPassword.Location = new System.Drawing.Point(18, 150);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 26);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUserID.Location = new System.Drawing.Point(122, 20);
            this.txtUserID.MaxLength = 15;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(140, 26);
            this.txtUserID.TabIndex = 0;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtFirstName.Location = new System.Drawing.Point(122, 52);
            this.txtFirstName.MaxLength = 100;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(224, 26);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtSurname
            // 
            this.txtSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtSurname.Location = new System.Drawing.Point(514, 52);
            this.txtSurname.MaxLength = 100;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(224, 26);
            this.txtSurname.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtPassword.Location = new System.Drawing.Point(122, 150);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(224, 26);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtCreateDate
            // 
            this.txtCreateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCreateDate.Location = new System.Drawing.Point(122, 203);
            this.txtCreateDate.Name = "txtCreateDate";
            this.txtCreateDate.ReadOnly = true;
            this.txtCreateDate.Size = new System.Drawing.Size(224, 26);
            this.txtCreateDate.TabIndex = 8;
            this.txtCreateDate.TabStop = false;
            this.txtCreateDate.Tag = "no control";
            // 
            // txtUpdateDate
            // 
            this.txtUpdateDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUpdateDate.Location = new System.Drawing.Point(122, 232);
            this.txtUpdateDate.Name = "txtUpdateDate";
            this.txtUpdateDate.ReadOnly = true;
            this.txtUpdateDate.Size = new System.Drawing.Size(224, 26);
            this.txtUpdateDate.TabIndex = 10;
            this.txtUpdateDate.TabStop = false;
            this.txtUpdateDate.Tag = "no control";
            // 
            // txtUpdateUser
            // 
            this.txtUpdateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtUpdateUser.Location = new System.Drawing.Point(514, 232);
            this.txtUpdateUser.Name = "txtUpdateUser";
            this.txtUpdateUser.ReadOnly = true;
            this.txtUpdateUser.Size = new System.Drawing.Size(224, 26);
            this.txtUpdateUser.TabIndex = 11;
            this.txtUpdateUser.TabStop = false;
            this.txtUpdateUser.Tag = "no control";
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCreateUser.Location = new System.Drawing.Point(514, 203);
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.Size = new System.Drawing.Size(224, 26);
            this.txtCreateUser.TabIndex = 9;
            this.txtCreateUser.TabStop = false;
            this.txtCreateUser.Tag = "no control";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtEmail.Location = new System.Drawing.Point(122, 84);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(224, 26);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblEmail.Location = new System.Drawing.Point(16, 84);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 26);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.txtCompanyName);
            this.grpUser.Controls.Add(this.txtDepartmentName);
            this.grpUser.Controls.Add(this.label3);
            this.grpUser.Controls.Add(this.textBox1);
            this.grpUser.Controls.Add(this.label2);
            this.grpUser.Controls.Add(this.label1);
            this.grpUser.Controls.Add(this.lblUserID);
            this.grpUser.Controls.Add(this.lblEmail);
            this.grpUser.Controls.Add(this.txtUpdateUser);
            this.grpUser.Controls.Add(this.lblFirstName);
            this.grpUser.Controls.Add(this.txtCreateUser);
            this.grpUser.Controls.Add(this.txtEmail);
            this.grpUser.Controls.Add(this.txtUpdateDate);
            this.grpUser.Controls.Add(this.lblLastName);
            this.grpUser.Controls.Add(this.txtCreateDate);
            this.grpUser.Controls.Add(this.lblPassword);
            this.grpUser.Controls.Add(this.lblUserUpdate);
            this.grpUser.Controls.Add(this.lblCreateUser);
            this.grpUser.Controls.Add(this.lblUpdateDate);
            this.grpUser.Controls.Add(this.lblCreateDate);
            this.grpUser.Controls.Add(this.txtUserID);
            this.grpUser.Controls.Add(this.txtFirstName);
            this.grpUser.Controls.Add(this.txtSurname);
            this.grpUser.Controls.Add(this.txtPassword);
            this.grpUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUser.Location = new System.Drawing.Point(3, 3);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(899, 456);
            this.grpUser.TabIndex = 0;
            this.grpUser.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(352, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 26);
            this.label3.TabIndex = 29;
            this.label3.Text = "Confirm Password";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.textBox1.Location = new System.Drawing.Point(514, 150);
            this.textBox1.MaxLength = 20;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(224, 26);
            this.textBox1.TabIndex = 7;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(410, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 26);
            this.label2.TabIndex = 27;
            this.label2.Text = "Company";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(18, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 25;
            this.label1.Text = "Department";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtDepartmentName.Location = new System.Drawing.Point(122, 116);
            this.txtDepartmentName.MaxLength = 100;
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(224, 26);
            this.txtDepartmentName.TabIndex = 30;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtCompanyName.Location = new System.Drawing.Point(514, 116);
            this.txtCompanyName.MaxLength = 100;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(224, 26);
            this.txtCompanyName.TabIndex = 31;
            // 
            // FrmADM010_UserMaintenance
            // 
            this.ClientSize = new System.Drawing.Size(905, 509);
            this.Name = "FrmADM010_UserMaintenance";
            this.Text = "ADM010 : User Maintenance";
            this.Load += new System.EventHandler(this.frmADM010_UserMaintenance_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmADM001_UserMaintenance_KeyPress);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Override Method        

        private bool DataLoading()
        {
            return DataLoading(LoadingParam);
        }

        public override bool DataLoading(object args)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ControlUtil.ClearControlData(this.Controls);                
                string strUserID = (string)this.LoadingParam;

                UserDTO data = m_bizUser.Search(strUserID);                

                if (data != null)
                {
                    
                    this.txtCreateDate.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", data.CreateDate);
                    this.txtCreateUser.Text = data.CreateBy;
                    this.txtEmail.Text = data.Email;
                    this.txtFirstName.Text = data.FirstName;
                    
                    this.txtPassword.Text = DataUtil.Decrypt(data.Password);
                    this.txtSurname.Text = data.LastName;

                    txtDepartmentName.Text = data.DepartmentName;
                    txtCompanyName.Text = data.CompanyName;
                    
                    this.txtUpdateDate.Text = string.Format("{0:dd/MM/yyyy HH:mm:ss}", data.UpdateDate);
                    this.txtUpdateUser.Text = data.UpdateBy;
                    this.txtUserID.Text = data.Username;
                    
                    
                    SetScreenMode(eScreenMode.View);
                    return true;
                }
                else
                {
                    this.LoadingParam = null;
                    SetScreenMode(eScreenMode.Idle);                    
                    
                    MessageDialog.ShowBusinessErrorMsg(this, Util.GetMessageText(eMsgId.COM0007));
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

        
        public override void SetScreenMode(eScreenMode eScreenMode)
        {
            base.SetScreenMode(eScreenMode);	// สำหรับใน Base class จะมี function สำหรับ Enabled & Disabled กับ Toolbar อยู่แล้ว เรียกใช้ได้เลย
                
            // m_dlgFindUserGroup = new FindUserGroupDialog(new FindDialogSqlDAO(AppEnvironment.Database), true, true);
            
            switch (eScreenMode)
            {
                case eScreenMode.Idle:
                    ControlUtil.ClearControlData(this.Controls);
                    ControlUtil.EnabledControl(false, this.Controls);
                    ControlUtil.EnabledControl(true, m_toolBarExport);
                    break;
                case eScreenMode.View:
                    ControlUtil.EnabledControl(false, this.Controls);
                    break;
                case eScreenMode.Edit:
                    ControlUtil.EnabledControl(true, this.Controls);
                    break;
                case eScreenMode.Add:
                    ControlUtil.ClearControlData(this.Controls);
                    ControlUtil.EnabledControl(true, this.Controls);
                    break;
            }
            ControlUtil.EnabledControl(false, txtCreateDate, txtCreateUser, txtUpdateDate, txtUpdateUser);
        }
        public override void PermissionControl()
        {                        
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Add, m_toolBarAdd);
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Edit, m_toolBarEdit);
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Delete, m_toolBarDelete);
            Util.UpdateToolbarButtonPermission(this.GetType(), Permission.Export, m_toolBarExport);

            UpdateToolbarSeparator();
        }
        public override void OnCommandClose()
        {
            // TODO : Add code for exit command.
            this.Close();
        }
        public override bool OnCommandAdd()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Add);
                txtUserID.Focus();
                txtCreateUser.Text = AppEnvironment.UserLogin;
                txtUpdateUser.Text = AppEnvironment.UserLogin;
                txtCreateDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
                txtUpdateDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
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
        public override bool OnCommandEdit()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SetScreenMode(eScreenMode.Edit);
                txtPassword.Focus();

                // TODO : Add code for edit data command.
                // Example bellow
                txtUpdateUser.Text = AppEnvironment.UserLogin;
                txtUpdateDate.Text = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
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

        public override bool OnCommandDelete()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0003)) != DialogButton.Yes)
                {
                    return false;
                }
                
                m_bizUser.Delete(txtUserID.Text);
                SetScreenMode(eScreenMode.Idle);
                this.LoadingParam = null;

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
        public override bool OnCommandFind()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                m_dlgFindUser = new FindUserDialog(new FindDialogSqlDAO(AppEnvironment.Database), false, false);

                if (m_dlgFindUser.ShowDialog(this) == DialogResult.OK)
                {
                    string strUserID = m_dlgFindUser.SelectedRecords[0][FindUserDialog.eColumn.Username.ToString()].ToString();
                    this.LoadingParam = strUserID;
                    this.DataLoading(strUserID);
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
        public override bool OnCommandSave()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0001)) != DialogButton.Yes)
                {
                    return false;
                }

                // Check data before save
                //m_bizUser.CheckDataBeforeSave(m_dtoUser);
                //if (CheckDataBeforeSave(txtUserID.Text.Trim()) == false)
                //{
                //    return false;
                //}

                // Prepare data to save
                UserDTO data = new UserDTO();                
                data.CreateBy = AppEnvironment.UserLogin;
                data.Email = this.txtEmail.Text;
                data.FirstName= this.txtFirstName.Text;
                data.LastName = this.txtSurname.Text;                
                data.Password = this.txtPassword.Text;
                data.DepartmentName = txtDepartmentName.Text;
                data.CompanyName = txtCompanyName.Text;                            
                data.UpdateBy = AppEnvironment.UserLogin;
                data.Username = txtUserID.Text;

                AppEnvironment.Database.BeginTrans();                

                switch (ScreenMode)
                {
                    case eScreenMode.Add:
                        m_bizUser.AddNew(data);
                        break;
                    case eScreenMode.Edit:
                        m_bizUser.Update(data);
                        break;
                }
                
                AppEnvironment.Database.CommitTrans();
                // End

                this.LoadingParam = txtUserID.Text.Trim();
                SetScreenMode(eScreenMode.View);
                return true;
            }
            catch (ApplicationException ex)
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
                AppEnvironment.Database.Close();
                this.Cursor = Cursors.Default;
            }
        }
        public override bool OnCommandCancel()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageDialog.ShowConfirmationMsg(this, Util.GetMessageText(eMsgId.COM0002)) != DialogButton.Yes)
                {
                    return false;
                }

                if (this.LoadingParam == null)
                {
                    SetScreenMode(eScreenMode.Idle);
                }
                else
                {
                    return this.DataLoading(LoadingParam);
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
        public override bool OnCommandExport()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                #warning ใส่ Code สำหรับ Export to Excel
                // TODO : Add code for export command.
                //Common.Export.exportExcel(this, new FindUserDialog(new FindDialogSqlDAO(MainApp.Database), false, false));
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
                // TODO : Add code for print command.
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
        public override bool OnCommandRefresh()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                return DataLoading();
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
        #endregion

        #region String Array
        public string StringArrayToString(string[] values)
        {
            string val = string.Empty;
            for (int i = 0; i < values.Length; ++i)
            {
                val += values[i];
                if (i < values.Length - 1)
                {
                    val += "\\n";
                }
            }
            return val;
        }
        public string[] StringToStringArray(string value)
        {
            if (value == null)
                return new string[0];
            ArrayList arLines = new ArrayList();
            string delim = "\\n";
            int index = value.IndexOf(delim);
            while (index >= 0)
            {
                arLines.Add(value.Substring(0, index));
                index += delim.Length;
                value = value.Substring(index, value.Length - index);
                index = value.IndexOf(delim);
            }
            arLines.Add(value);
            string[] lines = new string[arLines.Count];
            arLines.CopyTo(lines);
            return lines;
        }
        #endregion

        #region Evnet Handler       

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
            {
                if (e.KeyChar == '\r')
                {
                    OnCommandSave();
                }
            }
        }

       
        private void frmADM001_UserMaintenance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ScreenMode == eScreenMode.View || ScreenMode == eScreenMode.Add || ScreenMode == eScreenMode.Edit)
            {
                if (e.KeyChar == '\r')
                {
                    while (!this.SelectNextControl(this.ActiveControl, true, true, true, true))
                    {

                    }
                }
            }
        }
        #endregion

        private void frmADM010_UserMaintenance_Load(object sender, EventArgs e)
        {          
            SetScreenMode(eScreenMode.Idle);
        }
    }
}