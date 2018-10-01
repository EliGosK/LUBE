using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP.Framework.Data
{
    public interface IPermission
    {
        bool AllowPermission(string userID, string screenClass, string permission);
    }
}
