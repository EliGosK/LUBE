using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using EAP.Framework.Data;

namespace BusinessService
{
    public class CommonBIZ
    {
        public bool TestDatabaseConnection()
        {
            try
            {
                Database db = AppEnvironment.CreateDatabase();
                return SystemDAO.TestConnection(db);                
            }
            catch
            {
                return false;
            }
        }

        #region Message

        public DataTable GetAllMessage()
        {
            DataTable dtbMsg = StoreProcedure.GetMessage(AppEnvironment.Database, null);
            return dtbMsg;
        }

        #endregion
    }
}
