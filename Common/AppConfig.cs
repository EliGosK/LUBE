using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAP.Framework.Data;

namespace Common
{
    public class AppConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public const int DB_EXECUTE_TIMEOUT = 120;

        #region Member
        private static XmlConfiguration m_config = null;
        #endregion

        #region Properties
        private static XmlConfiguration Instance
        {
            get
            {
                if (m_config == null)
                {
                    m_config = new XmlConfiguration();
                }
                return m_config;
            }
        }
        #endregion

        static AppConfig()
        {
            ReadConfiguration();
        }

        public static void ReadConfiguration()
        {
            DatabaseHost = Instance.GetValue(ConfigName.DB_HOST);
            DatabaseName = Instance.GetValue(ConfigName.DB_NAME);
            DatabaseUsername = Instance.GetValue(ConfigName.DB_USER);
            DatabasePassword = Instance.GetValue(ConfigName.DB_PASS);

            int iTimeout = Util.ConvertObjectToInteger(Instance.GetValue(ConfigName.SQL_DEFAULT_TIMEOUT));
            if (iTimeout == 0)
                iTimeout = DB_EXECUTE_TIMEOUT;
            SqlDefaultTimeout = iTimeout;
        }

        #region Config Name

        private class ConfigName
        {
            public const string DB_HOST = "DB_HOST";
            public const string DB_NAME = "DB_NAME";
            public const string DB_USER = "DB_USER";
            public const string DB_PASS = "DB_PASS";
            public const string SQL_DEFAULT_TIMEOUT = "SQL_DEFAULT_TIMEOUT";

        }
        #endregion

        #region Get/Set Data

        #region Database Configuration

        public static string DatabaseHost { get; private set; }
        public static string DatabaseName { get; private set; }
        public static string DatabaseUsername { get; private set; }
        public static string DatabasePassword { get; private set; }
        public static int SqlDefaultTimeout { get; private set; }

        //public static string DatabaseHost
        //{
        //    get
        //    {

        //        string strValue = Instance.GetValue(ConfigName.DB_HOST);
        //        return strValue;
        //    }
        //}

        //public static string DatabaseName
        //{
        //    get
        //    {
        //        string strValue = Instance.GetValue(ConfigName.DB_NAME);
        //        return strValue;
        //    }
        //}

        //public static string DatabaseUsername
        //{
        //    get
        //    {
        //        string strValue = Instance.GetValue(ConfigName.DB_USER);
        //        return strValue;
        //    }
        //}

        //public static string DatabasePassword
        //{
        //    get
        //    {
        //        string strValue = DataUtil.Decrypt(Instance.GetValue(ConfigName.DB_PASS));
        //        return strValue;
        //    }
        //}

        //public static int SqlDefaultTimeout
        //{
        //    get
        //    {
        //        int iTimeout = Util.ConvertObjectToInteger(Instance.GetValue(ConfigName.SQL_DEFAULT_TIMEOUT));
        //        if (iTimeout == 0)
        //            iTimeout = DB_EXECUTE_TIMEOUT;

        //        return iTimeout;
        //    }
        //}

        #endregion

        /// <summary>
        /// Get connection string for SQL Server
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string strConnection = string.Format("Data Source={0};User ID={1};password={2};Initial Catalog={3}",
                                DatabaseHost,
                                DatabaseUsername,
                                DatabasePassword,
                                DatabaseName);

                return strConnection;
            }
        }

        #endregion

    }
}
