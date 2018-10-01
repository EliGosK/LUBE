using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;

namespace BusinessService
{
    public class LoginBIZ
    {
        public bool CheckUserPassword(string userID, string password)
        {
            return StoreProcedure.sp_Common_CheckUserPassword(AppEnvironment.Database, userID, password);
        }

        /// <summary>
        /// Get Username from database
        /// </summary>
        /// <param name="userID">Username is ignore case</param>
        /// <returns></returns>
        public tb_User GetUserInfo(string userID)
        {
            return StoreProcedure.sp_Common_Get_User(AppEnvironment.Database, userID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void UpdateUserPassword(string username, string password)
        {
            UserDAO dao = new UserDAO(AppEnvironment.Database);
            dao.UpdatePassword(username, password);
        }
    }
}
