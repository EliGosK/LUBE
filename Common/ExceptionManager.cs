using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EAP.Framework.Windows;

namespace Common
{
    public static class ExceptionManager
    {
        public static string GetExceptionFullMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("[{0}] - {1}", ex.GetType().Name, ex.Message));
            sb.AppendLine("Stack Trace - ");
            sb.AppendLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                sb.AppendLine(" :: Inner Exception ::");
                sb.AppendLine(string.Format("[{0}] - {1}", inner.GetType().Name, inner.Message));
                sb.AppendLine("Stack Trace - ");
                sb.AppendLine(inner.StackTrace);

            }

            return sb.ToString();

        }

        public static void ManageException(IWin32Window owner, Exception ex)
        {            
            var applicationException = ex as ApplicationException;
            if (applicationException != null)
            {
                var exception = applicationException;
                MessageDialog.ShowBusinessErrorMsg(owner, exception);
            }
            else
            {
                MessageDialog.ShowSystemErrorMsg(owner, ex);

                AppEnvironment.Log.Error("Error", ex);

            }
        }
    }
}
