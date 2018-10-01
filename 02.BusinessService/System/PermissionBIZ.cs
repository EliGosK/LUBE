using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using EAP.Framework.Data;

namespace BusinessService
{
    public class PermissionBIZ : IPermission
    {
        public bool AllowPermission(string userID, string screenClass, string permission)
        {
            return PermissionDAO.AllowPermission(AppEnvironment.Database, userID, screenClass, permission);
        }
    }
}
