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
    public class ACS110_MasterBIZ
    {
        private Database m_db = null;

        public ACS110_MasterBIZ()
        {
            m_db = AppEnvironment.Database;
        }

        public DataTable GetItemGroupCombobox()
        {
            return StoreProcedure.sp_GetItemGroupCombo(AppEnvironment.Database, null);
        }

        public string CheckExistCostType(string CostTypeCode)
        {
            return StoreProcedure.sp_ACS110_CheckExistCostType(AppEnvironment.Database, CostTypeCode);
        }

        public string CheckUseCostType(string CostTypeCode)
        {
            return StoreProcedure.sp_ACS110_CheckUseCostType(AppEnvironment.Database, CostTypeCode);
        }

        public DataTable RetrieveData(string searchCostType, string searchDesc, string searchCostGrp)
        {
            return StoreProcedure.sp_ACS110_SearchCostType(AppEnvironment.Database, searchCostType, searchDesc, searchCostGrp);
        }

        public void InsertCostType(string CostTypeCode, string CostDesc, string CostGroup, int ProcessID, bool Status, string byUser)
        {
            StoreProcedure.sp_ACS110_InsertCostType(AppEnvironment.Database, CostTypeCode, CostDesc, CostGroup, ProcessID, Status, byUser);
        }

        public void UpdateCostType(string CostTypeCode, string CostDesc, string CostGroup, int ProcessID, bool Status, string byUser)
        {
            StoreProcedure.sp_ACS110_UpdateCostType(AppEnvironment.Database, CostTypeCode, CostDesc, CostGroup, ProcessID, Status, byUser);
        }

        public void DeleteCostType(string CostTypeCode)
        {
            StoreProcedure.sp_ACS110_DeleteCostType(AppEnvironment.Database, CostTypeCode);
        }
        
    }
}
