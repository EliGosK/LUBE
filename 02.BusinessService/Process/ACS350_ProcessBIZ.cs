using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataObject;
using EAP.Framework.Data;

namespace BusinessService
{
    public class ACS350_ProcessBIZ
    {
        public DataTable GetPeriodCombobox()
        {
            return StoreProcedure.sp_GetPeriodCombo(AppEnvironment.Database);
        }

        public bool CheckExistTransfer(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS350_Check_ExistTransfer(AppEnvironment.Database, year, period);
            if (strRet == "T")
                return true;
            else
                return false;
        }

        public bool CheckPostingDate(int year, int period)
        {
            string strRet = StoreProcedure.sp_ACS350_Check_PostingDate(AppEnvironment.Database, year, period);
            if (strRet == "T")
                return true;
            else
                return false;
        }

        public void DeleteInventoryRevalution_Imp()
        {
            StoreProcedure.delete_tbt_InventoryRevaluation_Imp(AppEnvironment.Database);
        }

        public void InsertInventoryRevalution_Imp(ACS350_ImportData DataImport)
        {
            StoreProcedure.sp_ACS350_Insert_InventoryRevaluation_Imp(AppEnvironment.Database, DataImport);
        }

        public void calculateData(int Year, int Period, string ByUser)
        {
            StoreProcedure.sp_ACS350_Process_CalculateData(AppEnvironment.Database, Year, Period, ByUser);
        }

    }
}
