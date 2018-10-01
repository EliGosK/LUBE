using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ScreenPermissionAttribute : Attribute
    {
        private string[] m_permissions = null;
        public ScreenPermissionAttribute(params string[] perms)
        {
            m_permissions = perms;
        }
        /// <summary>
        /// ใช้ Constructor นี้สำหรับ VB เพราะว่า VB ไม่สามารถ pass Parameter ของ Attribute ที่เป็น Param Array ได้
        /// </summary>
        /// <param name="strPerm"></param>
        public ScreenPermissionAttribute(string strPerm)
        {
            m_permissions = new string[] { strPerm };
        }
        public string[] PermissionItems
        {
            get
            {
                return m_permissions;
            }
        }

        public static string[] GetScreenPermission(Type formType)
        {
            if (formType != null)
            {
                object[] att = formType.GetCustomAttributes(typeof(ScreenPermissionAttribute), true);
                List<string> lstPerm = new List<string>();
                foreach (object o in att)
                {
                    if (o is ScreenPermissionAttribute)
                    {
                        lstPerm.AddRange(((ScreenPermissionAttribute)o).PermissionItems);
                    }
                }
                return lstPerm.ToArray();
            }
            else
            {
                return new string[0];
            }
        }
    }
}
