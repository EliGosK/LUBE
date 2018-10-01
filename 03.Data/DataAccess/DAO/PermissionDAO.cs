using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Data;

namespace DataAccess
{
    public static class PermissionDAO
    {
        public static bool AllowPermission(Database db, string strUsername, string strScreenClass, string strPermissionName)
        {
            string sql = string.Empty;
            sql += " select top 1 ";
            sql += " 	sec.*, scr.ClassName  ";
            sql += " from   ";
            sql += " 	tbs_SecurityMatch sec inner join tbs_Screen scr on sec.ScreenID = scr.ScreenID   ";
            sql += " where  ";
            sql += " 	sec.Username = @Username ";
            sql += " 	and sec.Permission = @PermName ";
            sql += " 	and scr.ClassName = @ClassName ";
            sql += " union all ";
            sql += " select  top 1 ";
            sql += " 	sec.*, scr.ClassName   ";
            sql += " from  ";
            sql += " 	tbs_SecurityMatch sec   ";
            sql += " 	inner join tbs_GroupMapping grp on sec.GroupID = grp.GroupID   ";
            sql += " 	inner join tbs_User usr on usr.Username = grp.Username   ";
            sql += " 	inner join tbs_Screen scr on sec.ScreenID = scr.ScreenID   ";
            sql += " where  ";
            sql += " 	usr.Username = @Username ";
            sql += " 	and sec.Permission = @PermName  ";
            sql += " 	and scr.ClassName = @ClassName ";

            DataRequest req = new DataRequest(sql);
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
            req.Parameters.Add("@PermName", SqlDbType.VarChar, strPermissionName);
            req.Parameters.Add("@ClassName", SqlDbType.VarChar, strScreenClass);
            return db.ExecuteCommand(req).Rows.Count > 0;
        }
    }
}
