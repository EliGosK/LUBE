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
    public class ACS120_MasterBIZ
    {
        private Database m_db = null;

        public ACS120_MasterBIZ()
        {
            m_db = AppEnvironment.Database;
        }
        
        public DataTable GetCostTypeCombo(string CostType)
        {
            return StoreProcedure.sp_GetCostTypeCombo(AppEnvironment.Database, CostType);
        }

        public DataTable RetrievePrdData(string srchAccCode, string srchAccName)
        {
            return StoreProcedure.sp_ACS120_SearchAccount(AppEnvironment.Database, srchAccCode, srchAccName);
        }

        public DataTable RetrieveMapData(string mapAccCode)
        {
            return StoreProcedure.sp_ACS120_SearchMappingData(AppEnvironment.Database, mapAccCode);
        }

        public void InsertMappingData(string AccCode, string CostType, decimal Percent, string byUser)
        {
            StoreProcedure.sp_ACS120_InsertMappingData(AppEnvironment.Database, AccCode, CostType, Percent, byUser);
        }

        public void UpdateMappingData(string AccCode, string CostType, decimal Percent, string byUser)
        {
            StoreProcedure.sp_ACS120_UpdateMappingData(AppEnvironment.Database, AccCode, CostType, Percent, byUser);
        }

        public void DeleteMappingData(string AccCode, string CostType)
        {
            StoreProcedure.sp_ACS120_DeleteMappingData(AppEnvironment.Database, AccCode, CostType);
        }
    }
}
