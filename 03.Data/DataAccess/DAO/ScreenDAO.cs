using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataObject;
using EAP.Framework.Data;

namespace DataAccess
{
    public static class ScreenDAO
    {
        public static List<ScreenDTO> Load(Database db, int iScreenID)
        {            

            DataRequest req = new DataRequest("select * from tbs_Screen where ScreenID = @ScreenID");
            req.Parameters.Add("@ScreenID", SqlDbType.Int, iScreenID);

            return db.ExecuteList<ScreenDTO>(req);
        }

        public static List<ScreenDTO> LoadAllAsTreeView(Database db)
        {            
            DataRequest req = new DataRequest("SELECT * FROM tbs_Screen ORDER BY LEFT(ClassName, LEN(ClassName) - CHARINDEX('.', REVERSE(ClassName)))");
            return db.ExecuteList<ScreenDTO>(req);
        }
    }
}
