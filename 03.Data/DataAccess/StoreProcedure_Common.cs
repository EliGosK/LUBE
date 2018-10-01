using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Extension;
using DataObject;
using EAP.Framework.Data;

namespace DataAccess
{
    public partial class StoreProcedure
    {
        public static DataTable GetMessage(Database db, string MsgCode)
        {
            DataRequest req = new DataRequest("sp_Common_Get_Message", CommandType.StoredProcedure);
            req.Parameters.Add("MsgCode", SqlDbType.NVarChar, MsgCode);

            DataTable dtb = db.ExecuteCommand(req);
            return dtb;
        }

        public static bool sp_Common_CheckUserPassword(Database db, string username, string password)
        {
            string encPassword = DataUtil.Encrypt(password);

            DataRequest req = new DataRequest("sp_Common_CheckUserPassword", CommandType.StoredProcedure);
            req.Parameters.Add("Username", SqlDbType.VarChar, username);
            req.Parameters.Add("Password", SqlDbType.VarChar, encPassword);

            bool bCheck = Util.ConvertObjectToBoolean(db.ExecuteScalar(req));
            return bCheck;
        }

        public static tb_User sp_Common_Get_User(Database db, string username)
        {
            DataRequest req = new DataRequest("sp_Common_Get_USer", CommandType.StoredProcedure);
            req.Parameters.Add("Username", SqlDbType.VarChar, username);

            return db.ExecuteList<tb_User>(req).FirstOrDefault();            
        }
    }
}
