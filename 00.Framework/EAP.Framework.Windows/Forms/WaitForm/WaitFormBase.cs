using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAP.Framework.Windows.Utils;

namespace EAP.Framework.Windows.Forms
{
    public partial class WaitFormBase : CustomTopFormBase, IWaitForm
    {

        /// <summary>
        /// To ensure splash screen is closed using the API and not by keyboard or any other things
        /// </summary>
        bool CloseSplashScreenFlag = false;


        #region Constructor

        public WaitFormBase()
        {
            InitializeComponent();
        }

        #endregion

        #region IWaitForm Implementation

        public virtual void ShowWaitForm()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new MethodInvoker(ShowWaitForm));
                return;
            }

            this.Show();
            Application.Run(this);
        }

        public virtual void CloseWaitForm()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new MethodInvoker(CloseWaitForm));
                return;
            }

            CloseSplashScreenFlag = true;
            this.Close();
        }

        public virtual void SetCaption(string text)
        {
            // Inherited class should be overriden method.
        }

        public virtual void SetDescription(string text)
        {
            // Inherited class should be overriden method.
        }

        public virtual void SetProgressValue(int percent)
        {
            // Inherited class should be overriden method.
        }
        #endregion

        #region Protected Method

        protected void SetCenterScreen()
        {
            Win32Util.CenterScreen(this);
        }

        #endregion
        private void WaitFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }
    }
}
