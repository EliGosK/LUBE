using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;

namespace BusinessService
{
    public class ACS330_ProcessBIZ
    {

        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public DataTable GetConfig()
        {
            return StoreProcedure.sp_Common_Get_Config(AppEnvironment.Database);
        }

        public bool CheckExistTransfer(int year, int period)
        {
            string isExist = StoreProcedure.sp_ACS330_Check_ExistTransfer(AppEnvironment.Database, year, period);
            if (isExist == "T")
                return true;
            else
                return false;
        }

        public DataTable GetInventoryRevaluationData(int year, int period)
        {
            return StoreProcedure.sp_ACS330_Get_InventoryRevaluationData(AppEnvironment.Database, year, period);
        }

        public void UpdateProcessControl(int year, int period, string itemCode, string batchNo, string byUser)
        {
            try
            {
                StoreProcedure.sp_ACS330_Update_ProcessControl(AppEnvironment.Database, year, period, itemCode, batchNo, byUser);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public DataTable GetJournalEntryData(int year, int period)
        {
            return StoreProcedure.sp_ACS330_Get_JournalEntryData(AppEnvironment.Database, year, period);
        }

        public void UpdateProcessControl_JV(int year, int period, string byUser)
        {
            try
            {
                StoreProcedure.sp_ACS330_Update_ProcessControl_JV(AppEnvironment.Database, year, period, byUser);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
