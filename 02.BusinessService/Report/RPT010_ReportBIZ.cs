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
    public class RPT010_ReportBIZ
    {
        private Database m_db = null;

        public RPT010_ReportBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetMOHSummaryReport(int Period, int Year)
        {
            return StoreProcedure.sp_RPT010_MOHSummaryReport(AppEnvironment.Database, Period, Year);
        }
    }
}
