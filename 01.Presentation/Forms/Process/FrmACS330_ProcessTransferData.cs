using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessService;
using Common;
using EAP.Framework;
using EAP.Framework.Data;
using EAP.Framework.Windows;
using BusinessService;
using NECT_EDI.SAP;

namespace Presentation.Forms.Process
{
    [ScreenPermission(Permission.OpenScreen, "Process")]
    public partial class FrmACS330_ProcessTransferData : LUBEFormDev
    {
        NECT_EDI.SAP.DTO.SAPConnection connection = new NECT_EDI.SAP.DTO.SAPConnection();
        string errorMsg = "";
        private ACS330_ProcessBIZ m_bizProcess = new ACS330_ProcessBIZ();

        int year;
        int period;

        DateTime dtDocDate; DateTime dtTaxDate; String strRef2; int? iSeries; String strComment; String strJrnMemo;
        String strItemCode; String strWhsCode; String strRevalInc; String strRevalDec; int iSnbAbsEntry; double dNewCost;

        DateTime dtJVRefDate;DateTime dtJVTaxDate; DateTime dtJVDueDate; 
        String strBG_Dr_LT_AccCode; double dBG_Dr_LT_Value;  String strBG_Dr_OEM_AccCode; double dBG_Dr_OEM_Value;
        String strBG_Cr_LT_AccCode; double dBG_Cr_LT_Value; String strBG_Cr_OEM_AccCode; double dBG_Cr_OEM_Value; String strCP_Dr_LT_AccCode; double dCP_Dr_LT_Value;
        String strCP_Dr_OEM_AccCode; double dCP_Dr_OEM_Value; String strCP_Cr_LT_AccCode; double dCP_Cr_LT_Value; String strCP_Cr_OEM_AccCode; double dCP_Cr_OEM_Value;

        public FrmACS330_ProcessTransferData()
        {
            InitializeComponent();

            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarFind, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete,
                m_toolBarSave, m_toolBarCancel, m_toolBarRefresh,
                m_toolBarPrint, m_toolBarImport, m_toolBarExport);

            UpdateToolbarSeparator();
            InitialScreen();
        }

        private void InitialScreen()
        {
            BindPeriodCombobox();
            txtYear.Text = (DateTime.Now.Year).ToString();

            bool bProcess = AppEnvironment.Permission.AllowPermission(AppEnvironment.UserLogin, this.GetType().FullName, "Process");
            if (bProcess)
                btnProcess.Enabled = true;
            else
                btnProcess.Enabled = false;
        }

        private void BindPeriodCombobox()
        {
            DataTable dtbPeriod = m_bizProcess.GetPeriodCombobox();

            cboMonth.DataSource = dtbPeriod;
            cboMonth.ValueMember = "PeriodID";
            cboMonth.DisplayMember = "PeriodName";
        }

        private bool ValidateRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please input: Year");
                bValid = false;
            }
            else
                year = txtYear.IntValue;

            if (Util.IsNullOrEmptyOrZero(cboMonth.SelectedValue))
            {
                errorProvider.SetError(cboMonth, "Please input: Month");
                bValid = false;
            }
            else
                period = Convert.ToInt32(cboMonth.SelectedValue);

            return bValid;
        }

        private void GetConfig()
        {
            try
            {
                DataTable config = m_bizProcess.GetConfig();

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("SAP_DB_NAME", "DB_NAME");
                dic.Add("SAP_DB_PASSWORD", "DB_PASSWORD");
                dic.Add("SAP_DB_USERNAME", "DB_USERNAME");
                dic.Add("SAP_LICENSE_SERVER", "LICENSE_SERVER");
                dic.Add("SAP_PASSWORD", "PASSWORD");
                dic.Add("SAP_SERVER", "SERVER");
                dic.Add("SAP_USERNAME", "USERNAME");

                Type t = typeof(NECT_EDI.SAP.DTO.SAPConnection);
                foreach (DataRow item in config.Rows)
                {
                    string x = item["ConfigType"].ToString();
                    string y = item["SystemValue"].ToString();
                    if (dic.ContainsKey(x))
                        t.GetProperty(dic[x]).SetValue(connection, y);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void Process()
        {
            try
            {
                if (!ValidateRequire())
                    return;

                //bool isExist = m_bizProcess.CheckExistTransfer(year, period);
                //if(isExist)
                //{
                //    MessageDialog.ShowBusinessErrorMsg(this, "This month can't be processed");
                //    return;
                //}

                AppEnvironment.ShowWaitForm("Please Wait", "Connecting to SAP B1.");

                SAPCompany company = null;
                Console.WriteLine("Start Connect SAP B1");
                company = NECT_EDI.SAP.SAPCompany.InitializeCompany(connection, out errorMsg);
                if (company == null)
                {
                    AppEnvironment.CloseWaitForm();
                    MessageDialog.ShowBusinessErrorMsg(this, "Cannot connect to SAP B1.");
                    return;
                }

                AppEnvironment.ShowWaitForm("Please Wait", "Processing");

                //Journal Voucher
                DataTable dtJV = m_bizProcess.GetJournalEntryData(year, period);
                if (!Util.IsNullOrEmptyOrZero(dtJV.Rows.Count))
                {
                    foreach (DataRow row in dtJV.Rows)
                    {
                        dtJVRefDate = DateTime.Parse(row["RefDate"].ToString());
                        dtJVTaxDate = DateTime.Parse(row["TaxDate"].ToString());
                        dtJVDueDate = DateTime.Parse(row["DueDate"].ToString());
                        strBG_Dr_LT_AccCode = row["BG_Dr_LT_AccCode"].ToString();
                        dBG_Dr_LT_Value = Util.ConvertObjectToDouble(row["BG_Dr_LT_Value"].ToString());
                        strBG_Dr_OEM_AccCode = row["BG_Dr_OEM_AccCode"].ToString();
                        dBG_Dr_OEM_Value = Util.ConvertObjectToDouble(row["BG_Dr_OEM_Value"].ToString());
                        strBG_Cr_LT_AccCode = row["BG_Cr_LT_AccCode"].ToString();
                        dBG_Cr_LT_Value = Util.ConvertObjectToDouble(row["BG_Cr_LT_Value"].ToString());
                        strBG_Cr_OEM_AccCode = row["BG_Cr_OEM_AccCode"].ToString();
                        dBG_Cr_OEM_Value = Util.ConvertObjectToDouble(row["BG_Cr_OEM_Value"].ToString());
                        strCP_Dr_LT_AccCode = row["CP_Dr_LT_AccCode"].ToString();
                        dCP_Dr_LT_Value = Util.ConvertObjectToDouble(row["CP_Dr_LT_Value"].ToString());
                        strCP_Dr_OEM_AccCode = row["CP_Dr_OEM_AccCode"].ToString();
                        dCP_Dr_OEM_Value = Util.ConvertObjectToDouble(row["CP_Dr_OEM_Value"].ToString());
                        strCP_Cr_LT_AccCode = row["CP_Cr_LT_AccCode"].ToString();
                        dCP_Cr_LT_Value = Util.ConvertObjectToDouble(row["CP_Cr_LT_Value"].ToString());
                        strCP_Cr_OEM_AccCode = row["CP_Cr_OEM_AccCode"].ToString();
                        dCP_Cr_OEM_Value = Util.ConvertObjectToDouble(row["CP_Cr_OEM_Value"].ToString());

                        NECT_EDI.SAP.SAPCompany.JV(dtJVRefDate,dtJVTaxDate, dtJVDueDate, strBG_Dr_LT_AccCode, dBG_Dr_LT_Value, strBG_Dr_OEM_AccCode, dBG_Dr_OEM_Value,
                                                strBG_Cr_LT_AccCode, dBG_Cr_LT_Value, strBG_Cr_OEM_AccCode, dBG_Cr_OEM_Value, strCP_Dr_LT_AccCode, dCP_Dr_LT_Value,
                                                strCP_Dr_OEM_AccCode, dCP_Dr_OEM_Value, strCP_Cr_LT_AccCode, dCP_Cr_LT_Value, strCP_Cr_OEM_AccCode, dCP_Cr_OEM_Value, out errorMsg);

                        if (!Util.IsNullOrEmpty(errorMsg))
                        {
                            AppEnvironment.CloseWaitForm();
                            MessageDialog.ShowBusinessErrorMsg(this, errorMsg);
                            return;
                        }
                        else
                        {
                            m_bizProcess.UpdateProcessControl_JV(year, period, AppEnvironment.UserLogin);
                        }
                    }
                }

                //Inventory Revaluation
                DataTable dt = m_bizProcess.GetInventoryRevaluationData(year, period);

                if (!Util.IsNullOrEmptyOrZero(dt.Rows.Count))
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dtDocDate = DateTime.Parse(row["PostingDate"].ToString());
                        dtTaxDate = DateTime.Parse(row["TaxDate"].ToString());
                        strRef2 = row["BatchNo"].ToString();

                        if (Util.IsNullOrEmptyOrZero(row["Series"].ToString()))
                        {
                            iSeries = null;
                        }
                        else
                            iSeries = Convert.ToInt32(row["Series"].ToString());

                        strComment = row["Comment"].ToString();
                        strJrnMemo = row["JrnMemo"].ToString();
                        strItemCode = row["ItemCode"].ToString();
                        strWhsCode = row["WhsCode"].ToString();
                        strRevalInc = row["RevalInc"].ToString();
                        strRevalDec = row["RevalDec"].ToString();
                        iSnbAbsEntry = Convert.ToInt32(row["AbsEntry"].ToString());
                        dNewCost = Convert.ToDouble(row["NewPrice"].ToString());

                        NECT_EDI.SAP.SAPCompany.InventoryRevaluation(dtDocDate, dtTaxDate, strRef2, iSeries, strComment, strJrnMemo, strItemCode, strWhsCode,
                                                                    strRevalInc, strRevalDec, iSnbAbsEntry, dNewCost, out errorMsg);

                        if (!Util.IsNullOrEmpty(errorMsg))
                        {
                            AppEnvironment.CloseWaitForm();
                            MessageDialog.ShowBusinessErrorMsg(this, errorMsg);
                            return;
                        }
                        else
                        {
                            m_bizProcess.UpdateProcessControl(year, period, strItemCode, strRef2, AppEnvironment.UserLogin);
                        }
                    }

                    AppEnvironment.CloseWaitForm();
                    MessageDialog.ShowInformationMsg("Process completed 100%");
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                GetConfig();
                if (MessageDialog.ShowConfirmationMsg(this, "Do you want to process data?") == DialogButton.Yes)
                {
                    Process();
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }
    }
}
