using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using CrystalDecisions.CrystalReports.Engine;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using EAP.Framework.Windows.Forms;
using EAP.Framework.Windows.MultiLanguage;
using Startup.Screen;

namespace Startup
{
    static class Program
    {
        public static Process PriorProcess()
        {
            var curr = Process.GetCurrentProcess();
            var procs = Process.GetProcessesByName(curr.ProcessName);
            return procs.FirstOrDefault(p => (p.Id != curr.Id) && (p.MainModule.FileName == curr.MainModule.FileName));
        }

        private static bool RunAutoUpdate()
        {
            bool ForceUpdate = false;
            try
            {
                string filename = Path.Combine(Application.StartupPath, "AutoUpdate.exe");
                if (!File.Exists(filename))
                {
                    // Skip AutoUpdate
                    return false;
                }


                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = Path.Combine(Application.StartupPath, "AutoUpdate.exe");
                info.Arguments = "";

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                ForceUpdate = true;

            }
            catch (Exception ex)
            {
                //throw;
            }
            return ForceUpdate;
        }

        private static void InitialApplication()
        {

            Thread.CurrentThread.Name = "Main";

            if (File.Exists(Path.Combine(Application.StartupPath, "App.ico")))
            {
                AppEnvironment.AppIcon = new Icon("App.ico");
            }


            // Initialize log4net.
            log4net.Config.XmlConfigurator.Configure();
        }

        private static bool TestConnection()
        {        
            CommonBIZ biz = new CommonBIZ();                
            if (biz.TestDatabaseConnection())
            {
                AppEnvironment.Database = AppEnvironment.CreateDatabase();
                AppEnvironment.Database.KeepConnection = true;                

                return true;
            }

            return false;
        }

        private static void SilentLoadReport()
        {
            try
            {
                using (ReportDocument rpt = ReportUtil.LoadReport("Dummy.rpt"))
                {

                    DataTable dtb = new DataTable("WK_DUMMY_REPORT");
                    dtb.Columns.Add("ID_DUMMY", typeof(int));
                    dtb.Rows.Add(1);

                    rpt.SetDataSource(dtb);
                }
            }
            catch (Exception ex)
            {                
                AppEnvironment.Log.Error("Startup", ex);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            //####################
            //# Set Look and Feel 
            //####################
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //== Update config from Auto Update ===================
            //if (args.Length > 0)
            //{
            //    string[] arrParam = args[0].ToString().Split('|');
            //    string strParam1 = string.Empty;
            //    if (arrParam.Length > 0)
            //    {
            //        XmlConfiguration inc = new XmlConfiguration();
            //        strParam1 = arrParam[1];
            //        if (args.Length > 1)
            //        {
            //            for (int i = 1; i < args.Length; i++)
            //            {
            //                strParam1 = strParam1 + " " + args[i].ToString();
            //            }
            //        }

            //        inc.SetValue(arrParam[0], strParam1);
            //    }
            //    return;
            //}


            if (PriorProcess() != null)
            {
                MessageDialog.Show(null, "Exception", "Application has running already.", null, MessageBoxIcon.Stop, DialogButton.OK);                
                return;
            }


#if !DEBUG
            //####################
            //# Run AutoUpdate
            //####################
            if (args.Length == 0)
            {
                if (RunAutoUpdate())
                {
                    // Terminate Application.
                    return;
                }
            }
            else if (args.Length == 1)
            {
                //  argument muet be "SkipUpdate" if not it will dislay error 
                if (args[0].ToLower() != "SkipUpdate".ToLower())
                {
                    MessageDialog.ShowInformationMsg("Application's argument must be \"SkipUpdate\" or not input");
                    return;
                }
            }
#endif

            //####################
            //# Load configuration
            //####################
            InitialApplication();

            try
            {
#region Test SplashScreen

                //SplashScreen sp = new SplashScreen(typeof(WaitFormProgress));
                //sp.Caption = "Please Wait";
                //sp.Description = "Value: 0";                
                //sp.ShowSplashScreen();
               

                //if (sp.IsVisibleSplashScreen)
                //{
                //    WaitFormProgress wf = (WaitFormProgress)sp.WaitForm;
                //    wf.ResizeMode = eResizeMode.GrowOnly;

                //    int i = 0;
                //    while (i <= 100)
                //    {
                //        string lineDesc = "Value: " + i + Environment.NewLine + "Line 1";
                //        if (i % 2 == 0)
                //        {
                //            lineDesc += Environment.NewLine + "Line 2";
                //        }

                        

                //        wf.SetDescription(lineDesc);
                //        wf.SetProgressValue(i);
                //        Thread.Sleep(50);

                //        i++;
                //    }
                //}

                //sp.CloseSplashScreen();

#endregion



                AppEnvironment.ShowWaitForm("Please Wait", "Initializing Report.");                
                //####################
                //# Silent load report
                //####################
                SilentLoadReport();                
                
                //####################
                //# Test Database Connection
                //####################
                AppEnvironment.ShowWaitForm("Please Wait", "Connecting to database server.");                
                if (!TestConnection())
                {
                    //AppEnvironment.CloseWaitForm();
                    MessageDialog.ShowBusinessErrorMsg(null, "Cannot connect to database. Please contact Administrator.");
                    GC.Collect();
                    return;
                }

                //####################
                //# Load Global Message
                //####################                    
                /* MessageManager used by Util class */
                AppEnvironment.ShowWaitForm("Please Wait", "Preparing Message");


                CommonBIZ bizCommon = new CommonBIZ();
                DataTable dtbMessages = bizCommon.GetAllMessage();
                MessageManager.SetMessage(dtbMessages);

                //####################
                //# Permission
                //####################                    
                AppEnvironment.Permission = new PermissionBIZ();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(null, ex);
            }
            finally
            {
                AppEnvironment.CloseWaitForm();
            }

            frmMainLogin mainLogin = new frmMainLogin();
            mainLogin.Icon = AppEnvironment.AppIcon;

            AppEnvironment.MainLoginForm = mainLogin;
            Application.Run(mainLogin);          
        }
    }
}
