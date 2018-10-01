using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NECT_EDI.SAP
{
    public  class SAPCompany : IDisposable
    {
        public static SAPbobsCOM.Company Company { get; set; }

        public bool IsConnected
        {
            get
            {
                if (Company != null)
                    return Company.Connected;
                return false;
            }
        }
        public string SAPUserID
        {
            get
            {
                if (IsConnected)
                    return Company.UserName;
                return null;
            }
        }
        public void Disconnect()
        {
            if (IsConnected)
                Company.Disconnect();
        }
        public void StartTransaction()
        {
            if (IsConnected)
                Company.StartTransaction();
        }
        public void CommitTransaction()
        {
            if (IsConnected && Company.InTransaction)
                Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
        }
        public void RollbackTransaction()
        {
            if (IsConnected && Company.InTransaction)
                Company.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
        }

        public static SAPCompany InitializeCompany(DTO.SAPConnection connection, out string errorMessage)
        {
            try
            {
                errorMessage = "";

                SAPCompany company = new SAPCompany();

                // ' Step 1
                // ' Initialize the Company Object.
                // ' You must create this first to enable connection to the company database.
                Company = new SAPbobsCOM.Company();

                // ' Step 2
                // ' Set the mandatory connection properties.
                Company.Server = connection.SERVER;            // ' Name of the DB Server 
                Company.CompanyDB = connection.DB_NAME;      // ' Enter the name of your company
                Company.UserName = connection.USERNAME;        // ' Enter the B1 user name
                Company.Password = connection.PASSWORD;        // ' Enter the B1 password
                Company.language = SAPbobsCOM.BoSuppLangs.ln_English;
                Company.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;

                if (string.IsNullOrEmpty(connection.LICENSE_SERVER) == false)
                    Company.LicenseServer = connection.LICENSE_SERVER;

                // ' Step 3
                // ' Use Windows authentication for database server.
                // ' True for NT server authentication,
                // ' False for database server authentication.
                Company.UseTrusted = connection.UseTrusted;
                if (!connection.UseTrusted)
                {
                    Company.DbUserName = connection.DB_USERNAME;
                    Company.DbPassword = connection.DB_PASSWORD;
                }

                // ' Step 4
                // ' Log on SAP Business One database.
                // ' Check result code.
                // ' To continue, the result code must be 0. 
                // ' Otherwise, check the error code and its related error message.
                if (Company.Connect() != 0)
                {
                    int code = 0;
                    errorMessage = "";

                    Company.GetLastError(out code, out errorMessage);

                    errorMessage = "Unable to Log on to SAP Business One database " + Environment.NewLine +
                                        String.Format("Error Code : {0} ", code) + Environment.NewLine +
                                        String.Format("Error Message : {0} ", errorMessage);
                    return null;
                }

                return company;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsCurrencyExist(string currency)
        {
            if (string.IsNullOrEmpty(currency) == false)
            {
                SAPbobsCOM.Recordset rs = Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                rs.DoQuery("SELECT * FROM OCRN");

                SAPbobsCOM.Currencies currencies = Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCurrencyCodes);
                currencies.Browser.Recordset = rs;

                return currencies.GetByKey(currency);
            }

            return true;
        }

        public static void JV(DateTime dtRefDate, DateTime dtTaxDate, DateTime dtDueDate,
                              String strBG_Dr_LT_AccCode, double dBG_Dr_LT_Value,
                              String strBG_Dr_OEM_AccCode, double dBG_Dr_OEM_Value,
                              String strBG_Cr_LT_AccCode, double dBG_Cr_LT_Value,
                              String strBG_Cr_OEM_AccCode, double dBG_Cr_OEM_Value,
                              String strCP_Dr_LT_AccCode, double dCP_Dr_LT_Value,
                              String strCP_Dr_OEM_AccCode, double dCP_Dr_OEM_Value,
                              String strCP_Cr_LT_AccCode, double dCP_Cr_LT_Value,
                              String strCP_Cr_OEM_AccCode, double dCP_Cr_OEM_Value,
                              out string errorMessage)
        {
            try
            {
                errorMessage = "";

                SAPbobsCOM.JournalVouchers oJV = default(SAPbobsCOM.JournalVouchers);
                oJV = Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers);


                oJV.JournalEntries.ReferenceDate = dtRefDate;
                oJV.JournalEntries.TaxDate = dtTaxDate;
                oJV.JournalEntries.DueDate = dtDueDate;

                oJV.JournalEntries.Lines.SetCurrentLine(0);
                //Budget
                oJV.JournalEntries.Lines.AccountCode = strBG_Dr_LT_AccCode;
                oJV.JournalEntries.Lines.Debit = dBG_Dr_LT_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strBG_Dr_OEM_AccCode;
                oJV.JournalEntries.Lines.Debit = dBG_Dr_OEM_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strBG_Cr_LT_AccCode;
                oJV.JournalEntries.Lines.Credit = dBG_Cr_LT_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strBG_Cr_OEM_AccCode;
                oJV.JournalEntries.Lines.Credit = dBG_Cr_OEM_Value;
                oJV.JournalEntries.Lines.Add();

                //Capacity Idle Variance
                oJV.JournalEntries.Lines.AccountCode = strCP_Dr_LT_AccCode;
                oJV.JournalEntries.Lines.Debit = dCP_Dr_LT_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strCP_Dr_OEM_AccCode;
                oJV.JournalEntries.Lines.Debit = dCP_Dr_OEM_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strCP_Cr_LT_AccCode;
                oJV.JournalEntries.Lines.Credit = dCP_Cr_LT_Value;
                oJV.JournalEntries.Lines.Add();

                oJV.JournalEntries.Lines.AccountCode = strCP_Cr_OEM_AccCode;
                oJV.JournalEntries.Lines.Credit = dCP_Cr_OEM_Value;
                oJV.JournalEntries.Lines.Add();

                int RetVal = oJV.Add();
                if (RetVal != 0)
                {
                    Company.GetLastError(out RetVal, out errorMessage);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void InventoryRevaluation(DateTime dtDocDate, DateTime dtTaxDate, String strRef2, int? iSeries,
                                                String strComment, String strJrnMemo,
                                                String strItemCode, String strWhsCode, String strRevalInc, String strRevalDec,
                                                int iSnbAbsEntry, double dNewCost,
                                                out string errorMessage)
        {
            try
            {
                errorMessage = "";

                SAPbobsCOM.MaterialRevaluation oMaterialRevaluation = default(SAPbobsCOM.MaterialRevaluation);
                SAPbobsCOM.SNBLines oMaterialRevaluationSNBLines = default(SAPbobsCOM.SNBLines);
                oMaterialRevaluation = Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oMaterialRevaluation);
                oMaterialRevaluation.DocDate = dtDocDate;
                oMaterialRevaluation.TaxDate = dtTaxDate;
                oMaterialRevaluation.RevalType = "P";
                oMaterialRevaluation.Reference2 = strRef2;
                oMaterialRevaluation.Comments = strComment;
                oMaterialRevaluation.JournalMemo = strJrnMemo;

                oMaterialRevaluation.Lines.ItemCode = strItemCode;
                oMaterialRevaluation.Lines.WarehouseCode = strWhsCode;
                //oMaterialRevaluation.Lines.RevaluationDecrementAccount = strRevalInc;
                //oMaterialRevaluation.Lines.RevaluationIncrementAccount = strRevalDec;

                oMaterialRevaluationSNBLines = oMaterialRevaluation.Lines.SNBLines;
                oMaterialRevaluationSNBLines.SetCurrentLine(0);
                oMaterialRevaluationSNBLines.SnbAbsEntry = iSnbAbsEntry; //AbsEntry from OBTN Table
                oMaterialRevaluationSNBLines.NewCost = dNewCost;
                oMaterialRevaluationSNBLines.Add();
                
                int RetVal = oMaterialRevaluation.Add();
                if (RetVal != 0)
                {
                    Company.GetLastError(out RetVal, out errorMessage);  
                    
                }

                //SAPbobsCOM.MaterialRevaluation m_MaterialRev;
                //SAPbobsCOM.MaterialRevaluation_lines m_MaterialRevLines;
                //SAPbobsCOM.FIFOLayers m_FIFOLayers;

                //SAPbobsCOM.MaterialRevaluationFIFOService m_MRVFIFOService;
                //SAPbobsCOM.MaterialRevaluationFIFO m_MRVFIFO;
                //SAPbobsCOM.MaterialRevaluationFIFOParams m_MRVFIFOParam;

                //SAPbobsCOM.Items m_FIFOItems;
                //SAPbobsCOM.Documents m_APInvoice;
                //SAPbobsCOM.Document_Lines m_APInvoice_Line;

                //SAPbobsCOM.BusinessPartners m_Vendor;

                //SAPbobsCOM.CompanyService m_companyService;

                //m_MaterialRev = (SAPbobsCOM.MaterialRevaluation)Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oMaterialRevaluation);
                //m_MaterialRev.DocDate = DateTime.Now;
                //m_MaterialRev.RevalType = "P";

                //// Line sub object: 
                //m_MaterialRevLines = m_MaterialRev.Lines;
                //m_MaterialRevLines.SetCurrentLine(0);
                ////m_MaterialRevLines.ItemCode = m_FIFOItems.ItemCode;
                //m_MaterialRevLines.ItemCode = "OEM071PPL";

                //// Layer sub object 
                //m_FIFOLayers = m_MaterialRevLines.FIFOLayers;

                //// Get Company Service 
                //m_companyService = (SAPbobsCOM.CompanyService)Company.GetCompanyService();

                //// Get Material Revaluation FIFO Service 
                //m_MRVFIFOService = (SAPbobsCOM.MaterialRevaluationFIFOService)m_companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.MaterialRevaluationFIFOService);

                //// Create Material Revaluation FIFO Service parameters  
                //m_MRVFIFOParam = (SAPbobsCOM.MaterialRevaluationFIFOParams)m_MRVFIFOService.GetDataInterface(SAPbobsCOM.MaterialRevaluationFIFOServiceDataInterfaces.mrfifosMaterialRevaluationFIFOParams);
                ////m_MRVFIFOParam.ItemCode = m_FIFOItems.ItemCode;
                //m_MRVFIFOParam.ItemCode = "OEM071PPL";
                //m_MRVFIFOParam.LocationCode = "1";
                //m_MRVFIFOParam.LocationType = "64";
                //m_MRVFIFOParam.ShowIssuedLayers = SAPbobsCOM.BoYesNoEnum.tNO;

                //// Process FIFO layers 
                //m_MRVFIFO = m_MRVFIFOService.GetMaterialRevaluationFIFO(m_MRVFIFOParam);
                //// Process first layer 
                //m_FIFOLayers.LayerID = m_MRVFIFO.Layers.Item(0).LayerID;
                //m_FIFOLayers.TransactionSequenceNum = m_MRVFIFO.Layers.Item(0).TransactionSequenceNum;

                ////String strNewPrice = NewPriceTxt.Text;

                //m_FIFOLayers.Price = 500;
                //// Process other layers 
                //int LayerNum = m_MRVFIFO.Layers.Count;
                //for (int i = 1; i < LayerNum; ++i)
                //{
                //    m_FIFOLayers.Add();
                //    m_FIFOLayers.SetCurrentLine(i);
                //    m_FIFOLayers.LayerID = m_MRVFIFO.Layers.Item(i).LayerID;
                //    m_FIFOLayers.TransactionSequenceNum = m_MRVFIFO.Layers.Item(i).TransactionSequenceNum;
                //    m_FIFOLayers.Price = 500;
                //}
                //m_MaterialRev.Add();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Dispose()
        {
            Disconnect();

            if (Company != null)
                Company = null;
        }
    }
}
