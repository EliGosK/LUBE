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
    public class UserBIZ
    {

        #region Variables
        private UserDAO m_daoUser = null;
        //private UserGroupsSqlDAO m_daoUserGroup = null;
        #endregion


        #region Constructor
        public UserBIZ(Database db)
        {
            m_daoUser = new UserDAO(db);                        
        }

        #endregion

        #region Validate Data
        public void CheckDataBeforeSave(UserDTO data)
        {
            //User ID
            if (Util.IsNullOrEmpty(data.Username))
            {
                throw new ApplicationException("User ID cannot be empty. Please input UserID.");//User ID cannot be empty. Please input UserID.
            }
            if (m_daoUser.Exist(data.Username))
            {
                throw new ApplicationException("User ID exists already. Please input new value.");//User ID exists already. Please input new value.
            }
            // First Name
            if (Util.IsNullOrEmpty(data.FirstName))
            {
                throw new ApplicationException("First Name cannot be emply. Please input FirstName.");//FirstName cannot be emply. Please input FirstName.
            }
        }
        #endregion

        #region Operation

        public UserDTO Search(string userID)
        {
            return m_daoUser.Search(userID);
        }

        public void Delete(string userID)
        {
            m_daoUser.Delete(userID);
        }

        public void AddNew(UserDTO data)
        {
            this.CheckDataBeforeSave(data);
            m_daoUser.AddNew(data);
        }

        public void Update(UserDTO data)
        {
            m_daoUser.Update(data);
        }

        public void UpdateUserGroup(UserDTO data, int[] GroupID)
        {
            m_daoUser.UpdateUserGroup(data.Username, GroupID);
        }
        #endregion
    }
}
