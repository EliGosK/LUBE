using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;

namespace BusinessService
{
    public class UserGroupBIZ
    {

        #region Variables
        private UserGroupDAO m_daoUserGroup = null;
        private UserDAO m_daoUser = null;
        #endregion

        #region Constructor
        public UserGroupBIZ(Database db)
        {
            m_daoUserGroup = new UserGroupDAO(db);
            m_daoUser = new UserDAO(db);
        }

        #endregion

        #region Validate Data
        public void CheckDataBeforeSave(UserGroupDTO data)
        {
            if (Util.IsNullOrEmpty(data.GroupID))
            {
                throw new ApplicationException("GroupName cannot be empty. Please input data of GroupName.");//GroupName cannot be empty. Please input data of GroupName.
            }
        }
        #endregion

        #region Load Operation
        public List<UserGroupDTO> LoadGroup(string strUserID)
        {
            return m_daoUser.LoadGroup(strUserID);
        }
        public UserGroupDTO Load(int iGroupID)
        {
            return m_daoUserGroup.Load(iGroupID);
        }
        public List<UserDTO> LoadMember(int iGroupID)
        {
            List<UserDTO> dto = new List<UserDTO>();
            return m_daoUserGroup.LoadMember(iGroupID);
        }
        #endregion

        #region Operation
        public List<UserGroupDTO> SearchData(string strGroupName, string strDescription, string strMember)
        {
            return m_daoUserGroup.Search(strGroupName, strDescription, strMember);
        }
        public int AddNew(UserGroupDTO data)
        {
            this.CheckDataBeforeSave(data);
            return m_daoUserGroup.AddNew(data);
        }
        public void Update(UserGroupDTO data)
        {
            this.CheckDataBeforeSave(data);
            m_daoUserGroup.Update(data);
        }
        public void UpdateGroupMapping(decimal iGroupID, string[] UserID)
        {
            m_daoUserGroup.UpdateGroupMapping(iGroupID, UserID);
        }
        public void Delete(int iGroupID)
        {
            m_daoUserGroup.Delete(iGroupID);
        }
        #endregion
    }
}
