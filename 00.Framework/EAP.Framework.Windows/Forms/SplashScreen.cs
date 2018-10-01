using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP.Framework.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public class SplashScreen
    {
        #region Member

        private Type m_typeWaitForm = null;
        private IWaitForm m_waitFormInstance = null;
        private Thread m_thread = null;

        #endregion

        #region Constructor

        public SplashScreen(Type typeOfWaitForm)
        {
            m_typeWaitForm = typeOfWaitForm;

            
            
        }

        #endregion

        #region Properties

        public IWaitForm WaitForm
        {
            get { return m_waitFormInstance; }
        }

        public string Caption { get; set; }
        public string Description { get; set; }

        #endregion

        #region Public Method

        public bool IsVisibleSplashScreen
        {
            get { return (m_waitFormInstance != null && ((Form) m_waitFormInstance).Visible); }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowSplashScreen()
        {
            if (IsVisibleSplashScreen)
            {
                m_waitFormInstance.SetCaption(Caption);
                m_waitFormInstance.SetDescription(Description);

                return;
            }

            m_thread = new Thread(new ThreadStart(InternalShowSplashScreen));
            m_thread.Name = "SplashScreen";
            m_thread.IsBackground = true;
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            while (m_waitFormInstance == null || ((Form)m_waitFormInstance).IsHandleCreated == false)
            {
                Thread.Sleep(100);
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        public void CloseSplashScreen()
        {
            if (m_waitFormInstance != null && ((Form)m_waitFormInstance).IsDisposed == false)
            {
                m_waitFormInstance.CloseWaitForm();                
            }

            m_waitFormInstance = null;
            m_thread = null;
        }

        #endregion

        #region Internal Method

        private void InternalShowSplashScreen()
        {
            if (m_waitFormInstance == null)
            {

                m_waitFormInstance = (IWaitForm)Activator.CreateInstance(m_typeWaitForm);
                m_waitFormInstance.SetCaption(Caption);
                m_waitFormInstance.SetDescription(Description);

                m_waitFormInstance.ShowWaitForm();
            }
        }

        #endregion
    }
}
