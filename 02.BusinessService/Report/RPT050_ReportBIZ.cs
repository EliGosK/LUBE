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
    public class RPT050_ReportBIZ
    {
        private Database m_db = null;

        public RPT050_ReportBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetDetailBudgetCapaVarianceReport(int Year, int Period)
        {
            return StoreProcedure.sp_RPT050_DetailBudgetCapaVarianceReport(AppEnvironment.Database, Year, Period);
        }

    }
}
