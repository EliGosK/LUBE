using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EAP.Framework.Windows.MultiLanguage
{
    public static class MessageManager
    {        
        #region Member
        

        private static DataTable m_dtbMessage = null;

        /// <summary>
        /// 
        /// </summary>
        private static string LanguagePath = "";

        #endregion

        #region Constructor

        static MessageManager()
        {           
        }

        #endregion

        #region Properties

        /// <summary>
        /// Current language
        /// </summary>
        public static string Language { get; private set; }

        #endregion


        #region Private Method       

        public static void SetMessage(DataTable dtbMessage)
        {
            m_dtbMessage = dtbMessage;
        }
        #endregion


        public static string GetMessageText(string Id)
        {
            return GetMessageText(Id, string.Empty);
        }
        public static string GetMessageText(string Id, string defaultText)
        {
            if (m_dtbMessage == null)
                return defaultText;

            string msgId = Id.StartsWith("MSG") ? Id.Insert(3, "-") : Id;
            DataRow[] drs = m_dtbMessage.Select(string.Format("MsgCode = '{0}'", msgId));
            if (drs.Length > 0)
                return drs[0]["MsgText"].ToString();
            
            if (String.IsNullOrEmpty(defaultText))
                return "Not found message: " + Id;

            return defaultText;
            
        }                       
    }
}
