using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using DataObject;
using EAP;
using EAP.Framework.Data;
using EAP.Framework.Windows;

namespace Startup.Screen
{
    public partial class frmChangePassword : Form
    {
        #region Member

        private LoginBIZ m_bizLogin = new LoginBIZ();

        #endregion

        #region Constructor

        private frmChangePassword()
        {
            InitializeComponent();
        }

        public frmChangePassword(tb_User userInfo) : this()
        {
            CurrentUser = userInfo;
        }

        #endregion

        #region Properties

        public tb_User CurrentUser { get; private set; }

        #endregion

        #region Method

        private void ClearError()
        {
            errorProvider.Clear();
        }
        private void SetError(Control control, string text)
        {
            errorProvider.SetError(control, text);
        }


        #endregion

        #region Validation

        private bool ValidateBeforeChangePassword()
        {
            ClearError();

            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string newPasswordConfirm = txtConfirmNewPassword.Text.Trim();


            bool bValid = true;
            if (Util.IsNullOrEmpty(currentPassword))
            {
                SetError(txtCurrentPassword, Util.GetMessageText(eMsgId.COM0024, "Current Password"));
                bValid = false;
            }
            else
            {
                string encCurrentPassword = DataUtil.Encrypt(currentPassword);
                if (encCurrentPassword != CurrentUser.Password)
                {
                    SetError(txtCurrentPassword, "Current Password is incorrect.");
                    bValid = false;
                }
            }

            if (Util.IsNullOrEmpty(newPassword))
            {
                SetError(txtNewPassword, Util.GetMessageText(eMsgId.COM0024, "New Password"));
                bValid = false;
            }
            else
            {
                if (newPassword != newPasswordConfirm)
                {
                    SetError(txtConfirmNewPassword, "Confirm new password is invalid.");
                    bValid = false;
                }
            }

            return bValid;
        }

        #endregion

        #region Control Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeChangePassword())
            {
                MessageDialog.ShowBusinessErrorMsg(this, "Cannot change password. Please check input data is correct.");
                return;
            }

            string newPassword = txtNewPassword.Text.Trim();
            string encNewPassword = DataUtil.Encrypt(txtNewPassword.Text.Trim());

            // Update new password
            m_bizLogin.UpdateUserPassword(CurrentUser.Username, newPassword);

            // Refresh password            
            CurrentUser.Password = encNewPassword;


            MessageDialog.ShowInformationMsg("Password is updated complete");

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        private void frmChangePassword_Shown(object sender, EventArgs e)
        {            
        }
    }
}
