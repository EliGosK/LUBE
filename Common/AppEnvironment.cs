using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAP.Framework.Data;
using EAP.Framework.Windows.Forms;
using log4net;

namespace Common
{
    public static class AppEnvironment
    {
        #region Variables

        private static WaitForm m_waitForm = null;

        #endregion

        static AppEnvironment()
        {            
        }

        #region Main Login

        public static Icon AppIcon { get; set; }
        public static Form MainLoginForm { get; set; }
        public static string DatabaseName { get; set; }
        /// <summary>
        /// Get/Set current username login
        /// </summary>
        public static string UserLogin { get; set; }


        #endregion

        #region Database Instance

        /// <summary>
        /// Global Database Instance
        /// </summary>
        public static Database Database { get; set; }

        /// <summary>
        /// Create new instance database connection.
        /// </summary>
        /// <returns></returns>
        public static Database CreateDatabase()
        {
            return new SqlData(AppConfig.ConnectionString, AppConfig.SqlDefaultTimeout);
        }

        /// <summary>
        /// Create new instance database connection by specific own connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Database CreateDatabase(string connectionString)
        {
            return new SqlData(connectionString, AppConfig.SqlDefaultTimeout);
        }

        #endregion

        #region Log4Net

        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Waiting Form

        private static SplashScreen m_splashScreen = new SplashScreen(typeof(WaitForm));
       
        public static void ShowWaitForm()
        {
            //m_splashScreen.Caption = "Please Wait";
            //m_splashScreen.Description = "TORO";
            //m_splashScreen.ShowSplashScreen();
        }

        public static void ShowWaitForm(string caption, string description)
        {

            m_splashScreen.Caption = caption;
            m_splashScreen.Description = description;
            m_splashScreen.ShowSplashScreen();
        }

        public static void CloseWaitForm()
        {
            m_splashScreen.CloseSplashScreen();
        }

        #endregion

        #region Email

        public static string EmailSenderName { get; set; }
        public static SmtpInfo SmtpInfo { get; set; }

        #endregion

        #region Permission

        public static IPermission Permission { get; set; }

        #endregion
    }
}
