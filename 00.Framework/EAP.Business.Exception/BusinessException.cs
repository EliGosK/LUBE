using System;
using System.Collections.Generic;
using System.Text;

namespace EAP
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BusinessException : ApplicationException
    {
        #region Variables
        private string m_strMsgId;        
        private string[] m_strParams; 
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public string MsgId
        {
            get { return m_strMsgId; }
            set { m_strMsgId = value; }
        }
        
        /// <summary>
        /// Message Parameter List
        /// </summary>
        public string[] MsgParams {
            get { return m_strParams; }
            set { m_strParams = value; }
        }       

        #region Constructor

        /// <summary>
        /// จะต้องถูกนำไปใช้กับ Util.GetMessageText(BusinessException) เพื่อดึงค่า MsgDesc มาแสดง
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="strParams"></param>
        public BusinessException(string MsgId, params string[] strParams)
            : base(MsgId)
        {
            this.MsgId = MsgId;
            this.MsgParams = strParams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MsgId"></param>
        /// <param name="MsgDesc"></param>
        public BusinessException(string MsgId) 
            : base(MsgId)
        {
            this.MsgId = MsgId;
            this.m_strParams = null;
        }        

        #endregion
    }
}
