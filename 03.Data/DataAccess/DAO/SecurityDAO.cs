using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataObject;
using EAP.Framework.Data;

namespace DataAccess
{
    public static class SecurityDAO
    {
        public static List<SecurityMatchDTO> LoadSecurityOfUserGroup(Database db, int groupID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT sec.SecurityID, sec.Username, sec.GroupID, sec.ScreenID, sec.Permission");
            sb.AppendLine("     , sec.CreateDate, sec.UpdateDate, sec.CreateUser, sec.UpdateUser");
            sb.AppendLine("     , scr.ClassName, scr.DisplayName");
            sb.AppendLine(" FROM tbs_SecurityMatch sec JOIN tbs_Screen scr ON sec.ScreenID = scr.ScreenID ");
            sb.AppendLine(" WHERE sec.GroupID = @GroupID");
            sb.AppendLine(" ORDER BY scr.ClassName, sec.SecurityID");



            DataRequest req = new DataRequest();
            req.CommandText = sb.ToString();

            req.Parameters.Add("@GroupID", SqlDbType.Int, groupID);
            return db.ExecuteList<SecurityMatchDTO>(req);
        }

        public static List<SecurityMatchDTO> LoadSecurityOfUser(Database db, string Username)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT sec.SecurityID, sec.Username, sec.GroupID, sec.ScreenID, sec.Permission");
            sb.AppendLine("     , sec.CreateDate, sec.UpdateDate, sec.CreateUser, sec.UpdateUser");
            sb.AppendLine("     , scr.ClassName, scr.DisplayName");
            sb.AppendLine(" FROM tbs_SecurityMatch sec INNER JOIN tbs_Screen scr ON sec.ScreenID = scr.ScreenID");
            sb.AppendLine(" WHERE sec.Username = @Username");
            sb.AppendLine(" ORDER BY scr.ClassName, sec.SecurityID");

            DataRequest req = new DataRequest();
            req.CommandText = sb.ToString();
            req.Parameters.Add("@Username", SqlDbType.VarChar, Username);

            return db.ExecuteList<SecurityMatchDTO>(req);
        }

        public static List<SecurityMatchDTO> LoadSecurityOfUserForMenuMapping(Database db, string Username)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine(" SELECT DISTINCT scr.ClassName ");
            sb.AppendLine("  FROM tbs_SecurityMatch sec JOIN tbs_Screen scr on sec.ScreenID = scr.ScreenID  ");
            sb.AppendLine("  WHERE sec.Username = @Username AND sec.Permission = @OpenScreen ");
            sb.AppendLine(" UNION ");
            sb.AppendLine(" SELECT DISTINCT scr.ClassName ");
            sb.AppendLine("  FROM tbs_SecurityMatch sec  ");
            sb.AppendLine("   JOIN tbs_GroupMapping grp on sec.GroupID = grp.GroupID  ");
            sb.AppendLine("   JOIN tbs_User usr on usr.Username = grp.Username  ");
            sb.AppendLine("   JOIN tbs_Screen scr on sec.ScreenID = scr.ScreenID  ");
            sb.AppendLine(" where usr.Username = @Username and sec.Permission = @OpenScreen ");

            DataRequest req = new DataRequest(sb.ToString());
            req.Parameters.Add("@Username", SqlDbType.VarChar, Username);
            req.Parameters.Add("@OpenScreen", SqlDbType.VarChar, Permission.OpenScreen);

            return db.ExecuteList<SecurityMatchDTO>(req);
        }

        //public static List<SecurityMatchDTO> LoadSecurityOfUserForReportMapping(Database db, string Username)
        //{
        //    StringBuilder sb = new StringBuilder();

            
        //    sb.AppendLine(" SELECT scr.ClassName ");
        //    sb.AppendLine("   FROM tb_SecurityMatch sec inner join tb_Screen scr on sec.ScreenID = scr.ScreenID  ");
        //    sb.AppendLine("  WHERE sec.Username = @Username and sec.Permission = @OpenScreen ");
        //    sb.AppendLine(" UNION ");
        //    sb.AppendLine(" SELECT DISTINCT scr.ClassName  ");
        //    sb.AppendLine("   FROM tb_SecurityMatch sec  ");
        //    sb.AppendLine("    JOIN tb_GroupMapping grp on sec.GroupID = grp.GroupID  ");
        //    sb.AppendLine("    JOIN tb_User usr on usr.Username = grp.Username  ");
        //    sb.AppendLine("    JOIN tb_Screen scr on sec.ScreenID = scr.ScreenID  ");
        //    sb.AppendLine("  WHERE usr.Username = @Username and sec.Permission = @OpenScreen ");

        //    DataRequest req = new DataRequest(sb.ToString());
        //    req.Parameters.Add("@Username", SqlDbType.VarChar, Username);
        //    req.Parameters.Add("@OpenScreen", SqlDbType.VarChar, Permission.OpenReport);

        //    return db.ExecuteList<SecurityMatchDTO>(req);            
        //}

        public static List<SecurityMatchDTO> LoadSecurityOfUserAssignByGroupLevel(Database db, string Username)
        {
            StringBuilder sb = new StringBuilder();                        
            sb.AppendLine(" SELECT DISTINCT  ");
            sb.AppendLine("       sec.SecurityID, sec.Username, sec.GroupID, sec.ScreenID, sec.Permission");
            sb.AppendLine("     , sec.CreateDate, sec.UpdateDate, sec.CreateUser, sec.UpdateUser");
            sb.AppendLine("     , scr.ClassName");
            sb.AppendLine("   FROM tbs_SecurityMatch sec ");
            sb.AppendLine("   JOIN tbs_GroupMapping grp on sec.GroupID = grp.GroupID ");
            sb.AppendLine("   JOIN tbs_User usr on usr.Username = grp.Username ");
            sb.AppendLine("   JOIN tbs_Screen scr on sec.ScreenID = scr.ScreenID ");
            sb.AppendLine(" WHERE usr.Username = @Username ");
            sb.AppendLine(" ORDER BY scr.ClassName, sec.SecurityID ");

            DataRequest req = new DataRequest(sb.ToString());
            req.Parameters.Add("@Username", SqlDbType.VarChar, Username);
            return db.ExecuteList<SecurityMatchDTO>(req);
        }

        public static List<UserDTO> LoadSecurityOfScreenForUserLevel(Database db, int screenID)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT ");
            sb.AppendLine("     usr.Username ");
            sb.AppendLine("   , usr.FirstName ");
            sb.AppendLine("   , usr.LastName ");
            sb.AppendLine("   , usr.Password ");            
            sb.AppendLine("   , usr.Email ");            
            sb.AppendLine("   , usr.CreateDate ");
            sb.AppendLine("   , usr.UpdateDate ");
            sb.AppendLine("   , usr.CreateBy ");
            sb.AppendLine("   , usr.UpdateBy ");
            sb.AppendLine("   , usr.PassUpdateDate ");
            sb.AppendLine("   , sec.Permission");
            sb.AppendLine("   FROM tbs_SecurityMatch sec inner join tbs_User usr on sec.Username = usr.Username ");
            sb.AppendLine("  WHERE sec.ScreenID = @ScreenID and sec.Username is not null ");
            sb.AppendLine("  ORDER BY sec.Username, sec.SecurityID ");


            DataRequest req = new DataRequest(sb.ToString());            
            req.Parameters.Add("@ScreenID", SqlDbType.Int, screenID);
            return db.ExecuteList<UserDTO>(req);
        }

        public static List<UserGroupDTO> LoadSecurityOfScreenForUserGroupLevel(Database db, int screenID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT ");
            sb.AppendLine("    grp.GroupID ");
            sb.AppendLine("  , grp.GroupName ");
            sb.AppendLine("  , grp.Description ");
            sb.AppendLine("  , grp.CreateDate ");
            sb.AppendLine("  , grp.UpdateDate ");
            sb.AppendLine("  , grp.CreateUser ");
            sb.AppendLine("  , grp.UpdateUser ");
            sb.AppendLine("  , sec.Permission ");
            sb.AppendLine("   FROM tbs_SecurityMatch sec inner join tbs_UserGroup grp on sec.GroupID = grp.GroupID ");
            sb.AppendLine("  WHERE sec.ScreenID = @ScreenID and sec.GroupID is not null ");
            sb.AppendLine("  ORDER BY grp.GroupName, sec.SecurityID ");

            DataRequest req = new DataRequest(sb.ToString());
            
            req.Parameters.Add("@ScreenID", SqlDbType.Int, screenID);
            return db.ExecuteList<UserGroupDTO>(req);
        }

        public static void UpdateSecurityOfUserGroup(Database db, int groupID, SecurityMatchDTO[] secs, string updateUser)
        {
            DataRequest req = null;
            // Delete security of user
            req = new DataRequest("DELETE FROM tbs_SecurityMatch where GroupID = @GroupID");
            req.Parameters.Add("@GroupID", SqlDbType.Int, groupID);
            db.ExecuteNonQuery(req);


            // Insert new permission
            req = new DataRequest("sp_ADM040_AddNewSecurity", CommandType.StoredProcedure);
            foreach (SecurityMatchDTO sec in secs)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@Username", SqlDbType.VarChar, DBNull.Value);
                req.Parameters.Add("@GroupID", SqlDbType.Int, groupID);
                req.Parameters.Add("@ScreenID", SqlDbType.Int, sec.ScreenID);
                req.Parameters.Add("@Permission", SqlDbType.VarChar, sec.Permission);
                req.Parameters.Add("@CreateUser", SqlDbType.VarChar, updateUser);
                req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, updateUser);
                db.ExecuteNonQuery(req);
            }
        }

        public static void UpdateSecurityOfScreen(Database db, int screenID, SecurityMatchDTO[] userSecs, SecurityMatchDTO[] groupSecs, string updateUser)
        {
            DataRequest req = null;
            // Delete old data
            req = new DataRequest("DELETE FROM tbs_SecurityMatch where ScreenID = @ScreenID");
            req.Parameters.Add("@ScreenID", SqlDbType.Int, screenID);
            db.ExecuteNonQuery(req);

            // Insert new permission [user level]
            req = new DataRequest("sp_ADM040_AddNewSecurity", CommandType.StoredProcedure);
            foreach (SecurityMatchDTO sec in userSecs)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@Username", SqlDbType.VarChar, sec.Username);
                req.Parameters.Add("@GroupID", SqlDbType.Int, DBNull.Value);
                req.Parameters.Add("@ScreenID", SqlDbType.Int, screenID);
                req.Parameters.Add("@Permission", SqlDbType.VarChar, sec.Permission);
                req.Parameters.Add("@CreateUser", SqlDbType.VarChar, updateUser);
                req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, updateUser);
                db.ExecuteNonQuery(req);
            }

            // Insert new permission [user group level]
            foreach (SecurityMatchDTO sec in groupSecs)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@Username", SqlDbType.VarChar, DBNull.Value);
                req.Parameters.Add("@GroupID", SqlDbType.Int, sec.GroupID);
                req.Parameters.Add("@ScreenID", SqlDbType.Int, screenID);
                req.Parameters.Add("@Permission", SqlDbType.VarChar, sec.Permission);
                req.Parameters.Add("@CreateUser", SqlDbType.VarChar, updateUser);
                req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, updateUser);
                db.ExecuteNonQuery(req);
            }
        }

        public static void UpdateSecurityOfUser(Database db, string strUserID, SecurityMatchDTO[] secs, string strUpdateUser)
        {
            DataRequest req = null;
            // Delete security of user
            req = new DataRequest("delete from tbs_SecurityMatch where Username = @Username");
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUserID);
            db.ExecuteNonQuery(req);

            // Insert new permission
            req = new DataRequest("sp_ADM040_AddNewSecurity", CommandType.StoredProcedure);
            foreach (SecurityMatchDTO sec in secs)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@Username", SqlDbType.VarChar, strUserID);
                req.Parameters.Add("@GroupID", SqlDbType.Int, DBNull.Value);
                req.Parameters.Add("@ScreenID", SqlDbType.Int, sec.ScreenID);
                req.Parameters.Add("@Permission", SqlDbType.VarChar, sec.Permission);
                req.Parameters.Add("@CreateUser", SqlDbType.VarChar, strUpdateUser);
                req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, strUpdateUser);
                db.ExecuteNonQuery(req);
            }
        }

        public static string[] LoadPermission(Type formType)
        {
            if (formType != null)
            {
                object[] att = formType.GetCustomAttributes(typeof(ScreenPermissionAttribute), false);
                ArrayList arItems = new ArrayList();
                foreach (object o in att)
                {
                    if (o is ScreenPermissionAttribute)
                    {
                        arItems.AddRange(((ScreenPermissionAttribute)o).PermissionItems);
                    }
                }
                string[] result = new string[arItems.Count];
                arItems.CopyTo(result);
                return result;
            }
            else
            {
                return new string[0];
            }
        }
    }
}
