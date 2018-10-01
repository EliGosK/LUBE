
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using DataObject;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;

namespace Startup.Screen
{
    public partial class frmLogin : Form
    {       

        #region Member

        private bool m_bInitial = true;        

        private LoginBIZ m_bizLogin = new LoginBIZ();

        #endregion

        #region Constructor

        public frmLogin()
        {
            InitializeComponent();            
        }

        #endregion

        #region Properties

        public tb_User CurrentUser { get; private set; }

        #endregion

        #region Method
      
        private void InitialScreen()
        {
            m_bInitial = true;

            try
            {
                // Initial Control Property     
                ControlUtil.ClearControlData(txtCompany); 

                ControlUtil.EnabledControl(false, btnChangePassword);

                lblVersion.Text = @"Version: " + Application.ProductVersion;
            }
            finally
            {
                m_bInitial = false;
            }

        }

        /// <summary>
        /// แสดงเวลาปัจจุบัน
        /// </summary>
        private void DateTimeNow()
        {
            lblTime.Text = DateTime.Now.ToString("ddd dd'/'MM'/'yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private tb_User LoadUser(string username)
        {
            tb_User userInfo = m_bizLogin.GetUserInfo(username);

            if (userInfo == null)
            {
                txtCompany.Text = "";
            }
            else
            {
                txtCompany.Text = userInfo.CompanyName;
            }

            return userInfo;
        }

        private void UpdateEnableChangePasswordButton()
        {
            ControlUtil.EnabledControl(CurrentUser != null, btnChangePassword);
        }

        #endregion

        #region Validate

        /// <summary>
        /// ตรวจสอบ Require Field
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {            
            Boolean bPass = true;

            errorProvider.Clear();

           
            if (Util.IsNullOrEmpty(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, Util.GetMessageText(eMsgId.COM0024, "Username"));
                bPass = false;
            }

            if (Util.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, Util.GetMessageText(eMsgId.COM0024, "Password"));
                bPass = false;
            }


            if (!bPass)
            {
                AppEnvironment.CloseWaitForm();
            }

            return bPass;
        }

        #endregion        

        #region Control Event

        private void btnOK_Click(object sender, EventArgs e)
        {            
            try
            {                
                if (!ValidateData())
                {
                    return;
                }

                AppEnvironment.ShowWaitForm();

                //==========================
                // ตรวจสอบ User + Pass บนฐานข้อมูล
                //==========================                                
                //bool bCheckUserPasswordValid = m_bizLogin.CheckUserPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                string encryptPassword = DataUtil.Encrypt(txtPassword.Text.Trim());
                bool bCheckUserPasswordValid = (CurrentUser != null && CurrentUser.Password == encryptPassword);

                if (!bCheckUserPasswordValid)
                {
                    tmCurrentDateTime.Enabled = false;  
                    MessageDialog.ShowBusinessErrorMsg(this, Util.GetMessageText(eMsgId.COM0021));
                    AppEnvironment.UserLogin = null;

                    txtUsername.Focus();
                    txtUsername.SelectAll();
                    tmCurrentDateTime.Enabled = true;                          

                    return;
                }


                //====================================
                //string dbUsername = m_bizLogin.GetUsername(txtUsername.Text.Trim());                
                AppEnvironment.UserLogin = CurrentUser.Username;

                AppEnvironment.DatabaseName = AppConfig.DatabaseName;
                tmCurrentDateTime.Enabled = false;
                tmCurrentDateTime.Stop();
                tmCurrentDateTime.Dispose();

                AppEnvironment.CloseWaitForm();

                //==========================        
                AppEnvironment.Log.Info("Login Success");

                this.DialogResult = DialogResult.OK;
            }            
            catch (Exception ex)
            {
                tmCurrentDateTime.Enabled = false;                
                ExceptionManager.ManageException(this, ex);
                tmCurrentDateTime.Enabled = true;
            }                      
        }        

        private void btnExit_Click(object sender, EventArgs e)
        {
            tmCurrentDateTime.Stop();
            tmCurrentDateTime.Dispose();

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       

        private void tmCurrentDateTime_Tick(object sender, EventArgs e)
        {
            DateTimeNow();
        }

        #endregion

        #region Form Event

        protected override void WndProc(ref Message message)
        {
            // ป้องกันการ Move Form
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOK.PerformClick();
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit.PerformClick();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InitialScreen();
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            txtUsername.Focus();

            DateTimeNow();
            tmCurrentDateTime.Start();

            this.BringToFront();
            this.Activate();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            tmCurrentDateTime.Stop();
            tmCurrentDateTime.Dispose();
        }

        #endregion


        private string m_strEditingUsername = null;

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            m_strEditingUsername = txtUsername.Text.Trim();
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

            /* If not changed */
            if (m_strEditingUsername == username)
                return;

            // Load user information
            CurrentUser = LoadUser(username);
            UpdateEnableChangePasswordButton();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(CurrentUser);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                // Refresh last updated password.
                CurrentUser.Password = frm.CurrentUser.Password;
            }
        }
    }
}
