using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Windows.MultiLanguage
{
    [Serializable]
    public class FormItemData
    {
        public class ControlItemData
        {
            public string ControlName { get; set; }
            //public string PropertyText { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return string.Format("{0}-{1}", ControlName, Text);
            }
        }

        public class ControlItemDataCollection : List<ControlItemData>
        {
            public ControlItemData this[string controlName]
            {
                get { return FindControlData(controlName); }
            }

            public ControlItemData FindControlData(string controlName)
            {
                return this.Find(data => data.ControlName == controlName);
            }
        }

        public FormItemData()
        {
            ControlItems = new ControlItemDataCollection();
        }
        
        public string FullNamespace { get; set; }
        public string FormText { get; set; }
        public string MenuText { get; set; }
        public ControlItemDataCollection ControlItems { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="controlName"></param>
        ///// <returns></returns>
        //public string GetControlText(string controlName)
        //{
        //    return GetControlText(controlName, "");
        //}

        /// <summary>
        /// ดึงข้อความของ Control ที่ระบุ หากไม่พบข้อความจะแสดง DefaultText 
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public string GetControlText(string controlName, string defaultText)
        {
            ControlItemData item = ControlItems[controlName];
            if (item == null)
                return defaultText;

            return item.Text;
        }
    }
}
