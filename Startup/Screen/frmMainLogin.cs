using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using EAP.Framework.Windows;


namespace Startup.Screen
{
    public partial class frmMainLogin : Form
    {
        #region Properties
        public bool IsLogout { get; set; }
        #endregion

        #region Constructor

        public frmMainLogin()
        {
            InitializeComponent();
        }

        #endregion

        #region Generic Function

        /// <summary>
        /// แสดงหน้า Login
        /// </summary>
        public void PerformLogin()
        {
            frmLogin login = new frmLogin();
            login.Icon = AppEnvironment.AppIcon;

            if (login.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    // Show Working Form
                    InitialVariable();

                    frmMainMenu mainMenu = new frmMainMenu();
                    mainMenu.Icon = AppEnvironment.AppIcon;

                    mainMenu.MainLogin = this;
                    mainMenu.Show();


                    // Hide Background
                    this.Hide();

                }
                catch (Exception ex)
                {
                    AppEnvironment.CloseWaitForm();
                    ExceptionManager.ManageException(this, ex);                    

                    PerformLogin();
                }
            }
            else
            {
                // Exit Program
                this.Close();
            }
        }

        private void InitialVariable()
        {            
            //AppEnvironment.CompanyName = bizCommon.GetCompanyName();
        }
        
        #endregion

        private void frmMainLogin_Load(object sender, EventArgs e)
        {
            try
            {
                PerformLogin();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);                
            }
        }
    }
}
