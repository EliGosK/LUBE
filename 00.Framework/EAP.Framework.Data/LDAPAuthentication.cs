using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Data
{
    public interface ILDAPAuthen
    {
        LDAPUserObjectLink GetUserObjectLinkByName(string accountName, string userName, string password);
    }

    public class LDAPAuthenFactory
    {
        public static ILDAPAuthen CreateInstance(string ldapPath)
        {
            return new LDAPAuthentication(ldapPath);
        }
    }

    /*
     * Parameters for Factory
     * <param name="ldapPath" value="LDAP://IP:Port/DC=DOMAIN;DC=COM"/>
     */
    public class LDAPAuthentication : ILDAPAuthen
    {
        private const string LDAP_FILTER_USER = "(&(SAMAccountName={0})(!(SAMAccountName=*$))(objectClass=user))";
        private const string DEFAULT_PORT = "389";
        private string m_ldappath = null;
        public string LDAPpath
        {
            get { return m_ldappath; }
        }

        public LDAPAuthentication(IDictionary param)
        {
            if (param.Contains("ldapPath"))
                m_ldappath = param["ldapPath"].ToString();
        }

        public LDAPAuthentication(string ldapPath)
        {
            m_ldappath = ldapPath;
        }

        public LDAPAuthentication(string domainName, string ldapIP)
        {
            SetLDAPPath(domainName, ldapIP, DEFAULT_PORT);
        }

        public LDAPAuthentication(string domainName, string ldapIP, string ldapPort)
        {
            SetLDAPPath(domainName, ldapIP, ldapPort);
        }

        private void SetLDAPPath(string domainName, string ldapIP, string ldapPort)
        {
            //string[] arrDC = domainName.ToUpper().Split('.');
            //string DCName = string.Empty;
            //foreach (string DC in arrDC)
            //{
            //    if (DC != "COM")
            //    {
            //        DCName += string.Format("DC={0};", DC);
            //    }
            //}
            //m_ldappath = string.Format("LDAP://{0}:{1}/{2}DC=COM", ldapIP, ldapPort, DCName);

            string[] arrDC = domainName.ToUpper().Split('.');
            string DCName = string.Empty;

            for (int i = 0; i < arrDC.Length; i++)
            {
                if (i != (arrDC.Length - 1))
                {
                    DCName += string.Format("DC={0};", arrDC[i].ToString());
                }
                else
                {
                    DCName += string.Format("DC={0}", arrDC[i].ToString());
                }
            }

            m_ldappath = string.Format("LDAP://{0}:{1}/{2}", ldapIP, ldapPort, DCName);

        }

        public bool GetAuthenResult(string userName, string password)
        {
            bool result = false;
            DirectoryEntry ldapConnector = null;
            DirectorySearcher ldapSearcher = null;

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(m_ldappath))
                {
                    //result = AuthenResult.INVALID_INPUT;
                    result = false;
                }
                else
                {
                    ldapConnector = new DirectoryEntry(m_ldappath, userName, password);
                    ldapSearcher = new DirectorySearcher(ldapConnector);


                    SearchResult searchResult = ldapSearcher.FindOne();

                    //result = AuthenResult.SUCCEED;
                    result = true;
                }
            }
            catch (DirectoryServicesCOMException dex)
            {
                //result = AuthenResult.FAILED;
                throw dex;

            }
            catch (Exception ex)
            {
                //result = AuthenResult.UNEXPECTED_ERROR;
                throw ex;
            }
            finally
            {
                if (null != ldapSearcher)
                {
                    ldapSearcher.Dispose();
                }
                if (null != ldapConnector)
                {
                    ldapConnector.Dispose();
                }
            }

            return result;
        }

        public LDAPUserObjectLink GetUserObjectLinkByName(string accountName, string userName = "", string password = "")
        {
            LDAPUserObjectLink userObjeLink = new LDAPUserObjectLink();
            DirectoryEntry ldapConnector = null;
            DirectorySearcher ldapSearcher = null;

            //userName = AppConfig.DomainUsername;
            //password = AppConfig.DomainPassword;

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(m_ldappath))
                {
                    //property = null;
                }
                else
                {
                    ldapConnector = new DirectoryEntry(m_ldappath, userName, password);
                    ldapSearcher = new DirectorySearcher(ldapConnector);

                    ldapSearcher.Filter = String.Format(LDAP_FILTER_USER, accountName);
                    ldapSearcher.PropertiesToLoad.Add("samaccountname");
                    ldapSearcher.PropertiesToLoad.Add("company");
                    ldapSearcher.PropertiesToLoad.Add("mail");
                    ldapSearcher.PropertiesToLoad.Add("department");
                    ldapSearcher.PropertiesToLoad.Add("cn");
                    ldapSearcher.PropertiesToLoad.Add("domain");
                    ldapSearcher.PropertiesToLoad.Add("givenname"); //First Name
                    ldapSearcher.PropertiesToLoad.Add("sn"); //Last Name

                    SearchResultCollection colResult = ldapSearcher.FindAll();

                    foreach (SearchResult result in colResult)
                    {
                        ResultPropertyCollection props = result.Properties;
                        foreach (string propName in props.PropertyNames)
                        {
                            //Loop properties and pick out company,department
                            string tmp = (string)props[propName][0];
                        }
                    }

                    //SearchResult searchResult = ldapSearcher.FindOne();
                    foreach (SearchResult result in colResult)
                    {
                        if (result.Properties["samaccountname"][0].ToString().Trim().Length > 0)
                        {
                            List<string> lstOU = null;
                            lstOU = ExtractUserOU(result.Path);
                            string strAccName = null;
                            string strFirstName = null;
                            string strLastName = null;
                            string strDomain = null;
                            string strDepart = null;
                            string strDisp = null;
                            string strMail = null;
                            string strComp = null;

                            if (result.Properties.Contains("sAMAccountName"))
                            {
                                strAccName = result.Properties["sAMAccountName"][0].ToString();
                            }
                            if (result.Properties.Contains("givenname"))
                            {
                                strFirstName = result.Properties["givenname"][0].ToString();
                            }
                            if (result.Properties.Contains("sn"))
                            {
                                strLastName = result.Properties["sn"][0].ToString();
                            }
                            if (result.Properties.Contains("domain"))
                            {
                                strDomain = result.Properties["domain"][0].ToString();
                            }
                            else if (ldapConnector.Name != null && ldapConnector.Name.Length > 3)
                            {
                                strDomain = ldapConnector.Name.Substring(3);
                            }
                            if (result.Properties.Contains("department"))
                            {
                                strDepart = result.Properties["department"][0].ToString();
                            }
                            if (result.Properties.Contains("cn"))
                            {
                                strDisp = result.Properties["cn"][0].ToString();
                            }
                            if (result.Properties.Contains("mail"))
                            {
                                strMail = result.Properties["mail"][0].ToString();
                            }
                            if (result.Properties.Contains("company"))
                            {
                                strComp = result.Properties["company"][0].ToString();
                            }
                            LDAPUserObject objUser = new LDAPUserProperties(
                            strAccName,
                            strFirstName,
                            strLastName,
                            strDomain,
                            strDepart,
                            strDisp,
                            strMail,
                            strComp,
                            lstOU);
                            userObjeLink.AddLast(objUser);
                        }
                    }
                    colResult.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != ldapSearcher)
                    ldapSearcher.Dispose();
                if (null != ldapConnector)
                    ldapConnector.Dispose();
            }
            return userObjeLink;
        }

        private List<string> ExtractUserOU(string searchingPath)
        {
            string[] strOU = searchingPath.Split(',');
            List<string> lstrOU = new List<string>();
            foreach (string OU in strOU)
            {
                if (OU.Contains("OU="))
                {
                    lstrOU.Add(OU.Substring(3));
                }
            }

            return lstrOU;
        }

        private class LDAPUserProperties : LDAPUserObject
        {
            public LDAPUserProperties(string strUsername, string strFirstName, string strLastName, string strDomain, string strDepart, string strDisplay, string strEMail, string strCompany, List<string> lstOU)
                : base(strUsername, strFirstName, strLastName, strDomain, strDepart, strDisplay, strEMail, strCompany, lstOU)
            {
            }
        }
    }

    public abstract class LDAPUserObject
    {
        private string m_strUsername;
        private string m_strMail;
        private string m_strDepartment;
        private string m_strDisplayName;
        private string m_strDomainName;
        private List<string> m_lstOU;
        private string m_strCompany;
        private string m_strFirstName;
        private string m_strLastName;

        public string UserName { get { return m_strUsername; } }
        public string FirstName { get { return m_strFirstName; } }
        public string LastName { get { return m_strLastName; } }
        public string EMail { get { return m_strMail; } }
        public string Department { get { return m_strDepartment; } }
        public string FullName { get { return m_strDisplayName; } }
        public string DomainName { get { return m_strDomainName; } }
        public List<string> OU { get { return m_lstOU; } }
        public string Company { get { return m_strCompany; } set { m_strCompany = value; } }

        protected LDAPUserObject(string strUsername, string strFirstName, string strLastName, string strDomain, string strDepart, string strDisplay, string strEMail, string strCompany, List<string> lstOU)
        {
            m_strUsername = strUsername;
            m_strFirstName = strFirstName;
            m_strLastName = strLastName;
            m_strDomainName = strDomain;
            m_strDepartment = strDepart;
            m_strDisplayName = strDisplay;
            m_strMail = strEMail;
            m_lstOU = lstOU;
            m_strCompany = strCompany;
        }
    }

    public class LDAPUserObjectLink : LinkedList<LDAPUserObject> { }
}
