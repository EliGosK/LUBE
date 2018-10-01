using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;
using EAP.Framework.Data;

namespace DataAccess
{
    public interface IUserDAO
    {
        void AddNew(UserDTO data);
        void UpdateUserGroup(string strUsername, int[] iGroupIDs);
        void Update(UserDTO data);
        void Delete(string strUsername);
        void UpdatePassword(string strUsername, string strNewPassword);
        string CheckPassword(string strUsername, string strPassword);
        bool Exist(string strUsername);
        List<UserGroupDTO> LoadGroup(string strUsername);
        List<UserDTO> LoadAll();
        UserDTO Search(string strUsername);

        //List<UserDTO> GetItems();
        //string[] GetItems(ItemMode mode, string delim);
    }

    public class UserDAO : IUserDAO
    {
        private Database m_db = null;

        public UserDAO(Database db)
        {
            m_db = db;
        }

        public void AddNew(UserDTO data)
        {
            DataRequest req = new DataRequest("sp_ADM010_AddNewUser", CommandType.StoredProcedure);
            req.Parameters.Add("@Username", SqlDbType.VarChar, data.Username);
            req.Parameters.Add("@FirstName", SqlDbType.VarChar, data.FirstName);
            req.Parameters.Add("@LastName", SqlDbType.VarChar, data.LastName);            
            req.Parameters.Add("@Password", SqlDbType.VarChar, DataUtil.Encrypt(data.Password));
            req.Parameters.Add("@Email", SqlDbType.VarChar, data.Email);
            req.Parameters.Add("@CompanyName", SqlDbType.VarChar, data.CompanyName);
            req.Parameters.Add("@DepartmentName", SqlDbType.VarChar, data.DepartmentName);
            req.Parameters.Add("@CreateBy", SqlDbType.VarChar, data.CreateBy);            
            m_db.ExecuteNonQuery(req);
        }

        public void UpdateUserGroup(string strUsername, int[] iGroupIDs)
        {
            DataRequest req;
            // Delete old data
            req = new DataRequest("sp_ADM030_DeleteGroupMapping", CommandType.StoredProcedure);
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
            m_db.ExecuteNonQuery(req);

            // Add New
            req = new DataRequest("sp_ADM030_AddNewGroupMapping", CommandType.StoredProcedure);
            foreach (int id in iGroupIDs)
            {
                req.Parameters.Clear();
                req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
                req.Parameters.Add("@GroupID", SqlDbType.Int, id);
                m_db.ExecuteNonQuery(req);
            }
        }

        public void Update(UserDTO data)
        {
            DataRequest req = new DataRequest("sp_ADM010_UpdateUser", CommandType.StoredProcedure);
            req.Parameters.Add("@Username", SqlDbType.VarChar, data.Username);
            req.Parameters.Add("@FirstName", SqlDbType.VarChar, data.FirstName);
            req.Parameters.Add("@LastName", SqlDbType.VarChar, data.LastName);            
            req.Parameters.Add("@Password", SqlDbType.VarChar, DataUtil.Encrypt(data.Password));
            req.Parameters.Add("@Email", SqlDbType.VarChar, data.Email);
            req.Parameters.Add("@CompanyName", SqlDbType.VarChar, data.CompanyName);
            req.Parameters.Add("@DepartmentName", SqlDbType.VarChar, data.DepartmentName);
            req.Parameters.Add("@UpdateBy", SqlDbType.VarChar, data.UpdateBy);
            
            m_db.ExecuteNonQuery(req);
        }

        public void Delete(string strUsername)
        {
            DataRequest req = new DataRequest("sp_ADM010_DeleteUser", CommandType.StoredProcedure);

            req.Parameters.Add("@UserID", SqlDbType.VarChar, strUsername);
            m_db.ExecuteNonQuery(req);
        }

        public void UpdatePassword(string strUsername, string strNewPassword)
        {
            DataRequest req = new DataRequest("sp_ADM010_UpdateUserPassword", CommandType.StoredProcedure);

            req.Parameters.Add("@Password", DataUtil.Encrypt(strNewPassword));
            req.Parameters.Add("@Username", strUsername);
            m_db.ExecuteNonQuery(req);
        }

        public string CheckPassword(string strUsername, string strPassword)
        {
            string strPasswordEncrypt = DataUtil.Encrypt(strPassword);

            DataRequest req = new DataRequest("select * from tbs_User where Username = @Username and Password = @Password");
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
            req.Parameters.Add("@Password", SqlDbType.VarChar, strPasswordEncrypt);
            List<UserDTO> datas = m_db.ExecuteList<UserDTO>(req);
            if (datas.Count > 0)
            {
                return datas[0].Username;
            }
            else
            {
                return string.Empty;
            }
        }

        public bool Exist(string strUsername)
        {
            DataRequest req = new DataRequest("select top 1 * from tbs_User where Username = @Username");
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
            return m_db.ExecuteCommand(req).Rows.Count > 0;
        }

        public List<UserGroupDTO> LoadGroup(string strUsername)
        {
            string sql = string.Empty;
            sql += " select distinct c.*  ";
            sql += " from tbs_User a  ";
            sql += " inner join tbs_GroupMapping b on a.Username = b.Username ";
            sql += " inner join tbs_UserGroup c on b.GroupID = c.GroupID ";
            sql += " where a.Username = @Username order by c.GroupName ";
            DataRequest req = new DataRequest(sql);
            req.Parameters.Add("@Username", SqlDbType.VarChar, strUsername);
            return m_db.ExecuteList<UserGroupDTO>(req);
        }

        public List<UserDTO> LoadAll()
        {
            DataRequest req = new DataRequest("select * from tbs_User order by Username");
            return m_db.ExecuteList<UserDTO>(req);
        }

        public UserDTO Search(string strUsername)
        {
            DataRequest req = new DataRequest("select * from tbs_User where Username = @Username");
            req.Parameters.Add("@Username", strUsername);
            return m_db.ExecuteList<UserDTO>(req).FirstOrDefault();
        }
    }
}
