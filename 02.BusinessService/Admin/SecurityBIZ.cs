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
    public class SecurityBIZ
    {
        private Database m_db = null;

        public SecurityBIZ()
        {
            m_db = AppEnvironment.Database;
        }
             

        public List<string> LoadAllMenuByUser(string userID)
        {
            Database db = AppEnvironment.Database;

            List<SecurityMatchDTO> lstDTO = SecurityDAO.LoadSecurityOfUserForMenuMapping(db, userID);

            List<string> lstClass = new List<string>();
            lstDTO.ForEach(action =>
            {
                lstClass.Add(action.ClassName);
            });

            return lstClass;
        }

        #region Load Operation
        //User
        public List<UserDTO> LoadSecurityOfScreenForUserLevel(int iScreenID)
        {
            
            return SecurityDAO.LoadSecurityOfScreenForUserLevel(m_db, iScreenID);
        }
        public List<SecurityMatchDTO> LoadSecurityOfUser(string strUserID)
        {
            return SecurityDAO.LoadSecurityOfUser(m_db, strUserID);
        }
        public List<UserDTO> LoadAllUser()
        {
            UserDAO userDAO = new UserDAO(m_db);
            return userDAO.LoadAll();
        }
        public List<UserDTO> LoadMember(int iGroupID)
        {
            UserGroupDAO userGroupDAO = new UserGroupDAO(m_db);
            return userGroupDAO.LoadMember(iGroupID);
        }
        //User Group
        public List<UserGroupDTO> LoadSecurityOfScreenForUserGroupLevel(int iScreenID)
        {
            return SecurityDAO.LoadSecurityOfScreenForUserGroupLevel(m_db, iScreenID);
        }
        public List<SecurityMatchDTO> LoadSecurityOfUserGroup(int iGroupID)
        {
            return SecurityDAO.LoadSecurityOfUserGroup(m_db, iGroupID);
        }
        public List<UserGroupDTO> LoadAllUserGroup()
        {
            UserGroupDAO userGroupDAO = new UserGroupDAO(m_db);
            return userGroupDAO.LoadAll();
        }
        public List<UserGroupDTO> LoadGroup(string strUserID)
        {
            UserDAO userDAO = new UserDAO(m_db);
            return userDAO.LoadGroup(strUserID);
        }
        //Screen
        public List<ScreenDTO> LoadScreen(int iScreenID)
        {            
            return ScreenDAO.Load(m_db, iScreenID);
        }
        public List<ScreenDTO> LoadAllScreen()
        {
            return ScreenDAO.LoadAllAsTreeView(m_db);
        }
        public string Load(int iScreenID)
        {
            List<ScreenDTO> dtos = ScreenDAO.Load(m_db, iScreenID);
            if (dtos.Count > 0)
            {
                return dtos[0].ClassName;
            }
            else
            {
                return string.Empty;
            }
        }
        public List<SecurityMatchDTO> LoadSecurityOfUserForMenuMapping(string strUserID)
        {
            return SecurityDAO.LoadSecurityOfUserForMenuMapping(m_db, strUserID);
        }
        //Security
        public string[] LoadPermission(Type type)
        {            
            return SecurityDAO.LoadPermission(type);
        }

        public List<SecurityMatchDTO> LoadSecurityOfUserAssignByGroupLevel(string strUserID)
        {
            return SecurityDAO.LoadSecurityOfUserAssignByGroupLevel(m_db, strUserID);
        }
        #endregion

        #region Update Security Operation
        //User
        public void UpdateSecurityOfUser(string strUserID, SecurityMatchDTO[] data, string UserID)
        {
            SecurityDAO.UpdateSecurityOfUser(m_db, strUserID, data, UserID);
        }
        //User Group
        public void UpdateSecurityOfUserGroup(int iGroupID, SecurityMatchDTO[] data, string UserID)
        {
            SecurityDAO.UpdateSecurityOfUserGroup(m_db, iGroupID, data, UserID);
        }
        //Screen
        public void UpdateSecurityOfScreen(int iScreenID, SecurityMatchDTO[] secUsers, SecurityMatchDTO[] secGroups, string UserID)
        {
            SecurityDAO.UpdateSecurityOfScreen(m_db, iScreenID, secUsers, secGroups, UserID);
        }
        #endregion
    }
}
