using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;
using System.Data;

namespace BusinessService
{
    public class RPT020_ReportBIZ
    {
        private Database m_db = null;

        public RPT020_ReportBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetCostReports(int Year, int Period, string ProdDateFrom, string ProdDateTo, string ProdOrderNoFrom, string ProdOrderNoTo,string ItemType)
        {
            return StoreProcedure.sp_RPT020_CostReports(AppEnvironment.Database, Year, Period, ProdDateFrom, ProdDateTo, ProdOrderNoFrom, ProdOrderNoTo, ItemType);
        }
    }
}
