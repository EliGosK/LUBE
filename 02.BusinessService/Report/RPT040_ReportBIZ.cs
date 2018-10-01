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
    public class RPT040_ReportBIZ
    {
        private Database m_db = null;

        public RPT040_ReportBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetItemCombobox(string ItemCode, int? ItemGroup)
        {
            return StoreProcedure.sp_GetItemCombo(AppEnvironment.Database, ItemCode, ItemGroup);
        }

        public DataTable GetItemGroupComboBox(int? ItemGroupCode)
        {
            return StoreProcedure.sp_GetItemGroupCombo(AppEnvironment.Database, ItemGroupCode);
        }

        public DataTable GetClosingItemReport(int Year, int Period, int? ItemGroupFrom, int? ItemGroupTo, string ItemCodeFrom, string ItemCodeTo)
        {
            return StoreProcedure.sp_RPT040_ClosingItemReport(AppEnvironment.Database, Year, Period, ItemGroupFrom, ItemGroupTo, ItemCodeFrom, ItemCodeTo);
        }
    }
}
