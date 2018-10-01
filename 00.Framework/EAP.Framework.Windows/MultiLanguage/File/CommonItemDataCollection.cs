using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Windows.MultiLanguage
{
    public class CommonItemDataCollection : List<CommonItemData>
    {
        public CommonItemData FindById(string Id)
        {
            return this.Find(data => data.Id == Id);
        }
    }
}
