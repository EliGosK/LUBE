using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Extension;
using DataObject;
using EAP.Framework.Data;

namespace DataAccess
{
    public partial class StoreProcedure
    {
        #region ACS310 - Process Retrieve Data

        #region Get Data - Combobox

        public static DataTable sp_ACS310_Get_PeriodCombo(Database db)
        {
            DataRequest req = new DataRequest("sp_ACS310_Get_PeriodCombo",CommandType.StoredProcedure);
            return db.ExecuteCommand(req);
        }

        public static DataTable sp_ACS310_Get_SystemCombo(Database db)
        {
            DataRequest req = new DataRequest("sp_ACS310_Get_SystemCombo", CommandType.StoredProcedure);
            return db.ExecuteCommand(req);
        }

        public static DataTable sp_GetPeriodCombo(Database db)
        {
            DataRequest req = new DataRequest("sp_GetPeriodCombo", CommandType.StoredProcedure);
            return db.ExecuteCommand(req);
        }

        public static DataTable sp_GetItemCombo(Database db, string ItemCode, int? ItemGroup)
        {
            DataRequest req = new DataRequest("sp_GetItemCombo", CommandType.StoredProcedure);
            req.Parameters.Add("ItemCode", SqlDbType.NVarChar, ItemCode);
            req.Parameters.Add("ItemGroup", SqlDbType.Int, ItemGroup);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_GetItemGroupCombo(Database db, int? ItemGroupCode)
        {
            DataRequest req = new DataRequest("sp_GetItemGroupCombo", CommandType.StoredProcedure);
            req.Parameters.Add("ItemGroupCode", SqlDbType.Int, ItemGroupCode);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_GetCostTypeCombo(Database db, string CostType)
        {
            DataRequest req = new DataRequest("sp_GetCostTypeCombo", CommandType.StoredProcedure);
            req.Parameters.Add("CostType", SqlDbType.VarChar, CostType);

            return db.ExecuteCommand(req);
        }

        #endregion

        #region Master

        public static string sp_ACS110_CheckExistCostType(Database db, string CostTypeCode)
        {
            DataRequest req = new DataRequest("sp_ACS110_CheckExistCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS110_CheckUseCostType(Database db, string CostTypeCode)
        {
            DataRequest req = new DataRequest("sp_ACS110_CheckUseCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static DataTable sp_ACS110_SearchCostType(Database db, string CostTypeCode, string CostDesc, string CostGroup)
        {
            DataRequest req = new DataRequest("sp_ACS110_SearchCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);
            req.Parameters.Add("CostDesc", SqlDbType.VarChar, CostDesc);
            req.Parameters.Add("CostGroup", SqlDbType.VarChar, CostGroup);

            return db.ExecuteCommand(req);
        }

        public static void sp_ACS110_InsertCostType(Database db, string CostTypeCode, string CostDesc, string CostGroup, int ProcessID, bool Status, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS110_InsertCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);
            req.Parameters.Add("CostDesc", SqlDbType.VarChar, CostDesc);
            req.Parameters.Add("CostGroup", SqlDbType.VarChar, CostGroup);
            req.Parameters.Add("ProcessID", SqlDbType.Int, ProcessID);
            req.Parameters.Add("Status", SqlDbType.Bit, Status);
            req.Parameters.Add("byUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS110_UpdateCostType(Database db, string CostTypeCode, string CostDesc, string CostGroup, int ProcessID, bool Status, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS110_UpdateCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);
            req.Parameters.Add("CostDesc", SqlDbType.VarChar, CostDesc);
            req.Parameters.Add("CostGroup", SqlDbType.VarChar, CostGroup);
            req.Parameters.Add("ProcessID", SqlDbType.Int, ProcessID);
            req.Parameters.Add("Status", SqlDbType.Bit, Status);
            req.Parameters.Add("byUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS110_DeleteCostType(Database db, string CostTypeCode)
        {
            DataRequest req = new DataRequest("sp_ACS110_DeleteCostType", CommandType.StoredProcedure);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostTypeCode);

            db.ExecuteNonQuery(req);
        }

        public static DataTable sp_ACS120_SearchAccount(Database db, string srchAccCode, string srchAccName)
        {
            DataRequest req = new DataRequest("sp_ACS120_SearchAccount", CommandType.StoredProcedure);
            req.Parameters.Add("AccCode", SqlDbType.VarChar, srchAccCode);
            req.Parameters.Add("AccName", SqlDbType.VarChar, srchAccName);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_ACS120_SearchMappingData(Database db, string mapAccCode)
        {
            DataRequest req = new DataRequest("sp_ACS120_SearchMappingData", CommandType.StoredProcedure);
            req.Parameters.Add("AccCode", SqlDbType.VarChar, mapAccCode);

            return db.ExecuteCommand(req);
        }

        public static void sp_ACS120_InsertMappingData(Database db, string AccCode, string CostType, decimal Percent, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS120_InsertMappingData", CommandType.StoredProcedure);
            req.Parameters.Add("AccCode", SqlDbType.VarChar, AccCode);
            req.Parameters.Add("CostType", SqlDbType.VarChar, CostType);
            req.Parameters.Add("Percent", SqlDbType.Decimal, Percent);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS120_UpdateMappingData(Database db, string AccCode, string CostType, decimal Percent, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS120_UpdateMappingData", CommandType.StoredProcedure);
            req.Parameters.Add("AccCode", SqlDbType.VarChar, AccCode);
            req.Parameters.Add("CostType", SqlDbType.VarChar, CostType);
            req.Parameters.Add("Percent", SqlDbType.Decimal, Percent);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS120_DeleteMappingData(Database db, string AccCode,string CostType)
        {
            DataRequest req = new DataRequest("sp_ACS120_DeleteMappingData", CommandType.StoredProcedure);
            req.Parameters.Add("AccCode", SqlDbType.VarChar, AccCode);
            req.Parameters.Add("CostTypeCode", SqlDbType.VarChar, CostType);

            db.ExecuteNonQuery(req);
        }

        #endregion

        #region Checking

        public static DataTable sp_Common_Get_Revision(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_Common_Get_Revision", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.VarChar, year);
            req.Parameters.Add("Period", SqlDbType.VarChar, period);

            return db.ExecuteCommand(req);
        }

        public static string sp_ACS310_Check_ExistTransfer(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS310_Check_ExistTransfer", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS310_Check_RepeatProcess(Database db, int year, int period, int system)
        {
            DataRequest req = new DataRequest("sp_ACS310_Check_RepeatProcess", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);
            req.Parameters.Add("System", SqlDbType.Int, system);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        #endregion

        #region Update

        public static void sp_ACS310_Update_ProcessControl(Database db, int year, int period, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS310_Update_ProcessControl", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        #endregion

        #region Process - Retrieve


        public static void sp_ACS310_Process_RetrieveData(Database db, int year, int period, int system, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS310_Process_RetrieveData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);
            req.Parameters.Add("System", SqlDbType.Int, system);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static void sp_Test_Progress(Database db)
        {
            DataRequest req = new DataRequest("sp_Test_Progress", CommandType.StoredProcedure);            

            db.ExecuteNonQuery(req);
        }

        #endregion

        #endregion

        #region ACS320 - Actual Cost Calculation

        #region Checking

        public static string sp_ACS320_Check_ExistTransfer(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS320_Check_ExistTransfer", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS320_Check_Retrieve(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS320_Check_Retrieve", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS320_Check_RepeatProcess(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS320_Check_RepeatProcess", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        #endregion

        #region Get Data

        public static tb_Transfer sp_ACS320_Get_TransferData(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS320_Get_TransferData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            return db.ExecuteList<tb_Transfer>(req).FirstOrDefault();
        }

        #endregion


        #region Process - Calculate

        public static ACS320_Process sp_ACS320_Process_CalculateData(Database db, int year, int period, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS320_Process_CalculateData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            return db.ExecuteList<ACS320_Process>(req).FirstOrDefault();
        }

        public static ACS320_Process sp_ACS320_Process_ReCal(Database db, int Year, int Period, Decimal pActMOH, Decimal pActCapaUsed, decimal pSoldLiter, decimal pEndingLiter, decimal pSoldLiterOEM, decimal pEndingLiterOEM, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS320_Process_ReCal", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("pActMOH", SqlDbType.Decimal, pActMOH);
            req.Parameters.Add("pActCapaUsed", SqlDbType.Decimal, pActCapaUsed);
            req.Parameters.Add("pSoldLiter", SqlDbType.Decimal, pSoldLiter);
            req.Parameters.Add("pEndingLiter", SqlDbType.Decimal, pEndingLiter);
            req.Parameters.Add("pSoldLiterOEM", SqlDbType.Decimal, pSoldLiterOEM);
            req.Parameters.Add("pEndingLiterOEM", SqlDbType.Decimal, pEndingLiterOEM);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            return db.ExecuteList<ACS320_Process>(req).FirstOrDefault();
        }

        public static void sp_ACS320_Update_ProcessControl(Database db, int year, int period, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS320_Update_ProcessControl", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        #endregion

        #endregion

        #region ACS330
        public static DataTable sp_Common_Get_Config(Database db)
        {
            DataRequest req = new DataRequest("sp_Common_Get_Config", CommandType.StoredProcedure);

            return db.ExecuteCommand(req);
        }

        public static string sp_ACS330_Check_ExistTransfer(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS330_Check_ExistTransfer", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static DataTable sp_ACS330_Get_InventoryRevaluationData(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS330_Get_InventoryRevaluationData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            return db.ExecuteCommand(req);
        }

        public static void sp_ACS330_Update_ProcessControl(Database db, int Year, int Period, string ItemCode, string BatchNo, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS330_Update_ProcessControl", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("ItemCode", SqlDbType.VarChar, ItemCode);
            req.Parameters.Add("BatchNo", SqlDbType.VarChar, BatchNo);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static DataTable sp_ACS330_Get_JournalEntryData(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS330_Get_JournalEntryData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            return db.ExecuteCommand(req);
        }

        public static void sp_ACS330_Update_ProcessControl_JV(Database db, int Year, int Period, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS330_Update_ProcessControl_JV", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        #endregion

        #region ACS340 - Standard MOH Rate Calculation

        #region Checking

        public static string sp_ACS340_Check_ExistTransfer(Database db, int year)
        {
            DataRequest req = new DataRequest("sp_ACS340_Check_ExistTransfer", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);            

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS340_Check_ExistProcess(Database db, int year)
        {
            DataRequest req = new DataRequest("sp_ACS340_Check_ExistProcess", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        #endregion

        public static tb_NewStandardMOH sp_ACS340_Process_CalculateStandardMOH(Database db, int year, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS340_Process_CalculateStandardMOH", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            return db.ExecuteList<tb_NewStandardMOH>(req).FirstOrDefault();
        }

        public static void sp_ACS340_TransferData(Database db, int year, string byUser)
        {
            DataRequest req = new DataRequest("sp_ACS340_TransferData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, byUser);

            db.ExecuteNonQuery(req);
        }

        public static DataTable sp_ACS340_RetrieveData(Database db, int year)
        {
            DataRequest req = new DataRequest("sp_ACS340_RetrieveData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);

            return db.ExecuteCommand(req);
        }

        #endregion

        #region ACS350
        public static string sp_ACS350_Check_ExistTransfer(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS350_Check_ExistTransfer", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static string sp_ACS350_Check_PostingDate(Database db, int year, int period)
        {
            DataRequest req = new DataRequest("sp_ACS350_Check_PostingDate", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, year);
            req.Parameters.Add("Period", SqlDbType.Int, period);

            string strRet = db.ExecuteScalar(req).ToNullableString();
            return strRet;
        }

        public static void delete_tbt_InventoryRevaluation_Imp(Database db)
        {
            DataRequest req = new DataRequest("DELETE tbt_InventoryRevaluation_Imp", CommandType.Text);

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS350_Insert_InventoryRevaluation_Imp(Database db, ACS350_ImportData DataImport)
        {
            DataRequest req = new DataRequest("sp_ACS350_Insert_InventoryRevaluation_Imp", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, DataImport.Year);
            req.Parameters.Add("Period", SqlDbType.Int, DataImport.Period);
            req.Parameters.Add("ActCapaUsed", SqlDbType.Decimal, DataImport.ActCapaUsed);
            req.Parameters.Add("EndingLiter", SqlDbType.Decimal, DataImport.EndingLiter);
            req.Parameters.Add("UnSoldBudgetAll", SqlDbType.Decimal, DataImport.UnSoldBudgetAll);
            req.Parameters.Add("UnSoldCapaAll", SqlDbType.Decimal, DataImport.UnSoldCapaAll);
            req.Parameters.Add("AdjInv", SqlDbType.Decimal, DataImport.AdjInv);
            req.Parameters.Add("PostingDate", SqlDbType.DateTime, DataImport.PostingDate);
            req.Parameters.Add("BatchNo", SqlDbType.VarChar, DataImport.BatchNo);
            req.Parameters.Add("ItemCode", SqlDbType.VarChar, DataImport.ItemCode);
            req.Parameters.Add("BOM3", SqlDbType.Char, DataImport.BOM3);
            req.Parameters.Add("DocumentNo", SqlDbType.VarChar, DataImport.DocumentNo);
            req.Parameters.Add("BaseRef", SqlDbType.VarChar, DataImport.BaseRef);
            req.Parameters.Add("Quantity", SqlDbType.Decimal, DataImport.Quantity);
            req.Parameters.Add("QTYPPKG", SqlDbType.Decimal, DataImport.QTYPPKG);
            req.Parameters.Add("Liter", SqlDbType.Decimal, DataImport.Liter);
            req.Parameters.Add("DueDate", SqlDbType.DateTime, DataImport.DueDate);
            req.Parameters.Add("Lot1", SqlDbType.VarChar, DataImport.Lot1);
            req.Parameters.Add("Ref2", SqlDbType.VarChar, DataImport.Ref2);
            req.Parameters.Add("RM_Cost_B1_Display", SqlDbType.Decimal, DataImport.RM_Cost_B1_Display);
            req.Parameters.Add("StdOH", SqlDbType.Decimal, DataImport.StdOH);
            req.Parameters.Add("Variance_BOM1", SqlDbType.Decimal, DataImport.Variance_BOM1);
            req.Parameters.Add("BOM1Cost", SqlDbType.Decimal, DataImport.BOM1Cost);
            req.Parameters.Add("RM_Cost_Cal", SqlDbType.Decimal, DataImport.RM_Cost_Cal);
            req.Parameters.Add("ContainerCost", SqlDbType.Decimal, DataImport.ContainerCost);
            req.Parameters.Add("LabelCost", SqlDbType.Decimal, DataImport.LabelCost);
            req.Parameters.Add("ActualCostFG", SqlDbType.Decimal, DataImport.ActualCostFG);
            req.Parameters.Add("Variance_BOM2", SqlDbType.Decimal, DataImport.Variance_BOM2);
            req.Parameters.Add("ReceiptQty", SqlDbType.Decimal, DataImport.ReceiptQty);
            req.Parameters.Add("CalPrice", SqlDbType.Decimal, DataImport.CalPrice);
            req.Parameters.Add("TransValue", SqlDbType.Decimal, DataImport.TransValue);
            req.Parameters.Add("WhsCode", SqlDbType.VarChar, DataImport.WhsCode);
            req.Parameters.Add("SoldQuantity", SqlDbType.Decimal, DataImport.SoldQuantity);
            req.Parameters.Add("SoldLiter", SqlDbType.Decimal, DataImport.SoldLiter);
            req.Parameters.Add("EndingBalQuantity", SqlDbType.Decimal, DataImport.EndingBalQuantity);
            req.Parameters.Add("EndingBalLiter", SqlDbType.Decimal, DataImport.EndingBalLiter);
            req.Parameters.Add("Uprice", SqlDbType.Decimal, DataImport.Uprice);
            req.Parameters.Add("EndingBalAmount", SqlDbType.Decimal, DataImport.EndingBalAmount);
            req.Parameters.Add("COGS", SqlDbType.Decimal, DataImport.COGS);
            req.Parameters.Add("Effect", SqlDbType.Decimal, DataImport.Effect);
            req.Parameters.Add("NewAmount", SqlDbType.Decimal, DataImport.NewAmount);
            req.Parameters.Add("NewPrice", SqlDbType.Decimal, DataImport.NewPrice);
            req.Parameters.Add("DisAssemQty", SqlDbType.Decimal, DataImport.DisAssemQty); // Modified by Pachara S.

            db.ExecuteNonQuery(req);
        }

        public static void sp_ACS350_Process_CalculateData(Database db, int Year, int Period, string ByUser)
        {
            DataRequest req = new DataRequest("sp_ACS350_Process_CalculateData", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("ByUser", SqlDbType.VarChar, ByUser);

            db.ExecuteNonQuery(req);
        }

        #endregion

        #region Report

        public static DataTable sp_RPT010_MOHSummaryReport(Database db, int Period, int Year)
        {
            DataRequest req = new DataRequest("sp_RPT010_MOHSummaryReport", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_RPT020_CostReports(Database db, int Year, int Period, string ProdDateFrom, string ProdDateTo, string ProdOrderNoFrom, string ProdOrderNoTo,string ItemType)
        {
            DataRequest req = new DataRequest("sp_RPT020_CostReports", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("ProdDateFrom", SqlDbType.NVarChar, ProdDateFrom);
            req.Parameters.Add("ProdDateTo", SqlDbType.NVarChar, ProdDateTo);
            req.Parameters.Add("ProdOrderNoFrom", SqlDbType.NVarChar, ProdOrderNoFrom);
            req.Parameters.Add("ProdOrderNoTo", SqlDbType.NVarChar, ProdOrderNoTo);
            req.Parameters.Add("ItemType", SqlDbType.NVarChar, ItemType);

            return db.ExecuteCommand(req);
        }

        public static DataTable RPT030_CostOfGoodsManuReport_Header(Database db, int Year, int Period)
        {
            DataRequest req = new DataRequest("sp_RPT030_CostOfGoodsManuReport_Header", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);

            return db.ExecuteCommand(req);
        }

        public static DataTable RPT030_CostOfGoodsManuReport_Detail(Database db, int Year, int Period)
        {
            DataRequest req = new DataRequest("sp_RPT030_CostOfGoodsManuReport_Detail", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_RPT040_ClosingItemReport(Database db, int Year, int Period, int? ItemGroupFrom, int? ItemGroupTo, string ItemCodeFrom, string ItemCodeTo)
        {
            DataRequest req = new DataRequest("sp_RPT040_ClosingItemReport", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);
            req.Parameters.Add("ItemGroupFrom", SqlDbType.Int, ItemGroupFrom);
            req.Parameters.Add("ItemGroupTo", SqlDbType.Int, ItemGroupTo);
            req.Parameters.Add("ItemCodeFrom", SqlDbType.NVarChar, ItemCodeFrom);
            req.Parameters.Add("ItemCodeTo", SqlDbType.NVarChar, ItemCodeTo);

            return db.ExecuteCommand(req);
        }

        public static DataTable sp_RPT050_DetailBudgetCapaVarianceReport(Database db, int Year, int Period)
        {
            DataRequest req = new DataRequest("sp_RPT050_DetailBudgetCapaVarianceReport", CommandType.StoredProcedure);
            req.Parameters.Add("Year", SqlDbType.Int, Year);
            req.Parameters.Add("Period", SqlDbType.Int, Period);

            return db.ExecuteCommand(req);
        }

        #endregion
    }
}
