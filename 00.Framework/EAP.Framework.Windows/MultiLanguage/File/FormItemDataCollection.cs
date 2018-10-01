using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Windows.MultiLanguage
{
    [Serializable]
    public class FormItemDataCollection : List<FormItemData>
    {
        public FormItemData FindByFullNamespace(string fullNamespace)
        {
            return this.Find(data => data.FullNamespace == fullNamespace);
        }
        
    }
}
