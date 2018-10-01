using System;
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
    public interface IUserGroupsDAO
    {
        int AddNew(UserGroupDTO data);
        void UpdateGroupMapping(decimal dcGroupID, string[] strUsernames);
        void Update(UserGroupDTO data);
        void Delete(decimal dcGroupID);
        bool Exist(decimal dcGroupID);
        List<UserDTO> LoadMember(decimal dcGroupID);
        List<UserGroupDTO> LoadAll();
        UserGroupDTO Load(decimal dcGroupID);
        List<UserGroupDTO> Search(string strGroupName, string strDescription, string strMemberName);
        List<UserGroupDTO> GetItems();
       // string[] GetItems(ItemMode mode, string delim);
    }

    public class UserGroupDAO
    {
        private Database m_db = null;

        public UserGroupDAO(Database db)
        {
            m_db = db;
        }

        public int AddNew(UserGroupDTO data)
        {
            DataRequest req = new DataRequest("sp_ADM030_AddNewUserGroups", CommandType.StoredProcedure);
            req.Parameters.Add("@GroupName", SqlDbType.VarChar, data.GroupName);
            req.Parameters.Add("@Description", SqlDbType.VarChar, data.Description);
            req.Parameters.Add("@CreateUser", SqlDbType.VarChar, data.CreateUser);
            req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, data.UpdateUser);

            DataRequest.Parameter param = new DataRequest.Parameter();
            param.Name = "@GroupID";
            param.ParameterType = SqlDbType.Int;
            param.Direction = ParameterDirection.Output;
            param.Size = 40;
            req.Parameters.Add(param);

            m_db.ExecuteNonQuery(req);

            return Util.ConvertTextToInteger(param.Value.ToString());
        }

        public void UpdateGroupMapping(decimal dcGroupID, string[] strUsernames)
        {
            DataRequest req;
            // Delete old data
            //req = new DataRequest("delete from tb_DPGroupMapping where GroupID = @GroupID");
            req = new DataRequest("sp_ADM030_DeleteForUpdate_GM", CommandType.StoredProcedure);
            req.Parameters.Add("@GroupID", SqlDbType.Decimal, dcGroupID);
            m_db.ExecuteNonQuery(req);

            // Add new data
            //req = new DataRequest("insert into tb_DPGroupMapping (GroupID, Username) values (@GroupID, @Username)");
            req = new DataRequest("sp_ADM030_AddNewGroupMapping", CommandType.StoredProcedure);
            foreach (string Username in strUsernames)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@GroupID", SqlDbType.Decimal, dcGroupID);
                req.Parameters.Add("@Username", SqlDbType.VarChar, Username);
                m_db.ExecuteNonQuery(req);
            }
        }

        public void Update(UserGroupDTO data)
        {
            DataRequest req = new DataRequest("sp_ADM030_UpdateUserGroups", CommandType.StoredProcedure);
            req.Parameters.Add("@GroupName", SqlDbType.VarChar, data.GroupName);
            req.Parameters.Add("@Description", SqlDbType.VarChar, data.Description);
            req.Parameters.Add("@UpdateUser", SqlDbType.VarChar, data.UpdateUser);
            req.Parameters.Add("@GroupID", SqlDbType.Decimal, data.GroupID);
            m_db.ExecuteNonQuery(req);
        }

        public void Delete(decimal dcGroupID)
        {
            DataRequest req = new DataRequest("sp_ADM030_DeleteUserGroups", CommandType.StoredProcedure);
            req.Parameters.Add("@GroupID", SqlDbType.Decimal, dcGroupID);
            m_db.ExecuteNonQuery(req);
        }

        public bool Exist(decimal dcGroupID)
        {
            DataRequest req = new DataRequest("select top 1 * from tbs_UserGroup where GroupID = @GroupID");
            req.Parameters.Add("@GroupID", SqlDbType.Decimal, dcGroupID);
            return m_db.ExecuteCommand(req).Rows.Count > 0;
        }

        public List<UserDTO> LoadMember(decimal dcGroupID)
        {
            string sql = string.Empty;
            sql += " select u.* from ";
            sql += " tbs_UserGroup ug ";
            sql += " inner join tbs_GroupMapping gm on ug.GroupID = gm.GroupID ";
            sql += " inner join tbs_User u on gm.Username = u.Username ";
            sql += " where ug.GroupID = @GroupID ";
            DataRequest req = new DataRequest(sql);
            req.Parameters.Add("@GroupID", SqlDbType.Int, dcGroupID);
            return m_db.ExecuteList<UserDTO>(req);
        }

        public List<UserGroupDTO> LoadAll()
        {
            DataRequest req = new DataRequest("select * from tbs_UserGroup order by GroupName");
            return m_db.ExecuteList<UserGroupDTO>(req);
        }

        public UserGroupDTO Load(decimal dcGroupID)
        {
            DataRequest req = new DataRequest("select * from tbs_UserGroup where GroupID = @GroupID");
            req.Parameters.Add("@GroupID", SqlDbType.Decimal, dcGroupID);
            List<UserGroupDTO> datas = m_db.ExecuteList<UserGroupDTO>(req);
            if (datas.Count > 0)
            {
                return datas[0];
            }
            else
            {
                return null;
            }
        }

        public List<UserGroupDTO> Search(string strGroupName, string strDescription, string strMemberName)
        {
            DataRequest req = new DataRequest("select distinct a.* from tbs_UserGroup a left join tbs_GroupMapping b on a.GroupID = b.GroupID left join tbs_User c on b.Username = c.Username");
            string con = " where ";
            if (strGroupName != null)
            {
                req.CommandText += con + "a.GroupName like @GroupName";
                req.Parameters.Add("@GroupName", SqlDbType.VarChar, strGroupName);
                con = " and ";
            }
            if (strDescription != null)
            {
                req.CommandText += con + "a.Description like @Description";
                req.Parameters.Add("@Description", SqlDbType.VarChar, strDescription);
                con = " and ";
            }
            if (strMemberName != null)
            {
                req.CommandText += con + "(c.Username like @Username or c.FirstName like @FirstName or c.LastName like @LastName)";
                req.Parameters.Add("@Username", SqlDbType.VarChar, strMemberName);
                req.Parameters.Add("@FirstName", SqlDbType.VarChar, strMemberName);
                req.Parameters.Add("@LastName", SqlDbType.VarChar, strMemberName);
                con = " and ";
            }

            req.CommandText += " order by a.groupname ";

            return m_db.ExecuteList<UserGroupDTO>(req);
        }
    }
}
