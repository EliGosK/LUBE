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
    public class RPT030_ReportBIZ
    {
        private Database m_db = null;

        public RPT030_ReportBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetCostOfGoodsManuReport_Header(int Year, int Period)
        {
            return StoreProcedure.RPT030_CostOfGoodsManuReport_Header(AppEnvironment.Database, Year, Period);
        }

        public DataTable CostOfGoodsManuReport_Detail(int Year, int Period)
        {
            return StoreProcedure.RPT030_CostOfGoodsManuReport_Detail(AppEnvironment.Database, Year, Period);
        }
    }
}
