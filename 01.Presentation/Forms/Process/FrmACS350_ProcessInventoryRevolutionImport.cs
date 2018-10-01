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
using NECT_EDI.SAP;
using System.IO;
using OfficeOpenXml;

namespace Presentation.Forms.Process
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmACS350_ProcessInventoryRevolutionImport : LUBEFormDev
    {
        private ACS350_ProcessBIZ m_bizProcess = new ACS350_ProcessBIZ();
        string filePath = string.Empty;

        int countError = 0;

        ACS350_ImportData DataImport = new ACS350_ImportData();

        public FrmACS350_ProcessInventoryRevolutionImport()
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
        }

        private void BindPeriodCombobox()
        {
            DataTable dtbPeriod = m_bizProcess.GetPeriodCombobox();

            cboMonth.DataSource = dtbPeriod;
            cboMonth.ValueMember = "PeriodID";
            cboMonth.DisplayMember = "PeriodName";
        }

        private void SetControlDefaultValue()
        {
            txtYear.Text = (DateTime.Now.Year).ToString();
            cboMonth.SelectedValue = 1; // January
            txtPath.Text = string.Empty;
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

            if (Util.IsNullOrEmptyOrZero(cboMonth.SelectedValue))
            {
                errorProvider.SetError(cboMonth, "Please input: Month");
                bValid = false;
            }
            
            if (Util.IsNullOrEmpty(txtPath.Text))
            {
                errorProvider.SetError(txtPath, "Please select: file to import");
                bValid = false;
            }

            return bValid;
        }

        private void importData()
        {
            try
            {
                if (!ValidateRequire())
                    return;

                FileInfo fiACS350 = new FileInfo(filePath);
                if (fiACS350.Exists)
                {
                    using (ExcelPackage excel = new ExcelPackage(fiACS350))
                    {
                        var sht = excel.Workbook.Worksheets[1];

                        //Check Format File
                        for (int i = 1; i <= 35; i++)
                        {
                            if(Util.IsNullOrEmpty(sht.Cells[4, i].Value))
                            {
                                MessageDialog.ShowBusinessErrorMsg(this, "Wrong File Format.");
                                return;
                            }
                        }

                        //Check No Data
                        if (Util.IsNullOrEmpty(sht.Cells[5, 1].Value))
                        {
                            MessageDialog.ShowBusinessErrorMsg(this, "There is no data perform operation.");
                            return;
                        }

                        //Check Exist Transfer
                        //bool isExist = m_bizProcess.CheckExistTransfer(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));
                        //if(isExist)
                        //{
                        //    MessageDialog.ShowBusinessErrorMsg(this, "This month can't be processed.");
                        //    return;
                        //}

                        AppEnvironment.ShowWaitForm("Please Wait", "Importing data");
                        //Delete data in InventoryRevalution_Imp table.
                        m_bizProcess.DeleteInventoryRevalution_Imp();

                        //Import data.
                        int lastRows = sht.Dimension.End.Row - 2; //last row of data import
                        DataImport.Year = txtYear.IntValue;
                        DataImport.Period = Util.ConvertObjectToInteger(cboMonth.SelectedValue);
                        DataImport.ActCapaUsed = Util.ConvertObjectToDecimal(sht.Cells["AE2"].Value);
                        DataImport.EndingLiter = Util.ConvertObjectToDecimal(sht.Cells["AE3"].Value);
                        DataImport.UnSoldBudgetAll = Util.ConvertObjectToDecimal(sht.Cells["AG2"].Value);
                        DataImport.UnSoldCapaAll = Util.ConvertObjectToDecimal(sht.Cells["AH2"].Value);
                        DataImport.AdjInv = Util.ConvertObjectToDecimal(sht.Cells["AH3"].Value);
                        DataImport.Lot1 = null; // Modified by Pachara S.

                        for (int i = 5; i <= lastRows; i++)
                        {
                            //countError = i;
                            if (Util.IsNullOrEmpty(sht.Cells[i, 1].Value))
                                break;

                            DataImport.PostingDate = ConvertObjectToDateTime(sht.Cells[i, 1].Value);
                            DataImport.BatchNo = sht.Cells[i, 2].Text;
                            DataImport.ItemCode = sht.Cells[i, 3].Text;
                            DataImport.BOM3 = sht.Cells[i, 4].Text;
                            DataImport.DocumentNo = sht.Cells[i, 5].Text;
                            DataImport.Quantity = Util.ConvertObjectToDecimal(sht.Cells[i, 6].Value);
                            DataImport.QTYPPKG = Util.ConvertObjectToDecimal(sht.Cells[i, 7].Value);
                            DataImport.Liter = Util.ConvertObjectToDecimal(sht.Cells[i, 8].Value);
                            DataImport.DueDate = ConvertObjectToDateTime(sht.Cells[i, 9].Value);
                            //DataImport.Lot1 = sht.Cells[i, 10].Text; // Modified by Pachara S.
                            DataImport.DisAssemQty = Util.ConvertObjectToDecimal(sht.Cells[i, 10].Value); // Modified by Pachara S.
                            DataImport.Ref2 = sht.Cells[i, 11].Text;
                            DataImport.BaseRef = sht.Cells[i, 12].Text;
                            DataImport.RM_Cost_Cal = Util.ConvertObjectToDecimal(sht.Cells[i, 13].Value);
                            DataImport.RM_Cost_B1_Display = Util.ConvertObjectToDecimal(sht.Cells[i, 14].Value);
                            DataImport.StdOH = Util.ConvertObjectToDecimal(sht.Cells[i, 15].Value);
                            DataImport.Variance_BOM1 = Util.ConvertObjectToDecimal(sht.Cells[i, 16].Value);
                            DataImport.BOM1Cost = Util.ConvertObjectToDecimal(sht.Cells[i, 17].Value);
                            DataImport.ContainerCost = Util.ConvertObjectToDecimal(sht.Cells[i, 18].Value);
                            DataImport.LabelCost = Util.ConvertObjectToDecimal(sht.Cells[i, 19].Value);
                            DataImport.ActualCostFG = Util.ConvertObjectToDecimal(sht.Cells[i, 20].Value);
                            DataImport.Variance_BOM2 = Util.ConvertObjectToDecimal(sht.Cells[i, 21].Value);
                            DataImport.ReceiptQty = Util.ConvertObjectToDecimal(sht.Cells[i, 22].Value);
                            DataImport.CalPrice = Util.ConvertObjectToDecimal(sht.Cells[i, 23].Value);
                            DataImport.TransValue = Util.ConvertObjectToDecimal(sht.Cells[i, 24].Value);
                            DataImport.WhsCode = sht.Cells[i, 25].Text;
                            DataImport.SoldQuantity = Util.ConvertObjectToDecimal(sht.Cells[i, 26].Value);
                            DataImport.SoldLiter = Util.ConvertObjectToDecimal(sht.Cells[i, 27].Value);
                            DataImport.COGS = Util.ConvertObjectToDecimal(sht.Cells[i, 28].Value);
                            DataImport.EndingBalQuantity = Util.ConvertObjectToDecimal(sht.Cells[i, 29].Value);
                            DataImport.EndingBalLiter = Util.ConvertObjectToDecimal(sht.Cells[i, 30].Value);
                            DataImport.Uprice = Util.ConvertObjectToDecimal(sht.Cells[i, 31].Value);
                            DataImport.EndingBalAmount = Util.ConvertObjectToDecimal(sht.Cells[i, 32].Value);
                            DataImport.Effect = Util.ConvertObjectToDecimal(sht.Cells[i, 33].Value);
                            DataImport.NewAmount = Util.ConvertObjectToDecimal(sht.Cells[i, 34].Value);
                            DataImport.NewPrice = Util.ConvertObjectToDecimal(sht.Cells[i, 35].Value);

                            //Insert data in InventoryRevalution_Imp table.
                            m_bizProcess.InsertInventoryRevalution_Imp(DataImport);
                        }

                        //Check Posting Date not match
                        bool isNotMatch = m_bizProcess.CheckPostingDate(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));
                        if (isNotMatch)
                        {
                            AppEnvironment.CloseWaitForm();
                            MessageDialog.ShowBusinessErrorMsg(this, "Some Posting Date does not match with Selected Year/Month.");
                            return;
                        }

                        CalculateData();
                        AppEnvironment.CloseWaitForm();
                        MessageDialog.ShowInformationMsg("Import Completed.");
                    }
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        public void CalculateData()
        {
            try
            {
                //Process Calculate Data
                m_bizProcess.calculateData(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue), AppEnvironment.UserLogin);
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        public static DateTime ConvertObjectToDateTime(object obj)
        {
            if (Util.IsNullOrEmpty(obj)) return DateTime.MinValue;

            DateTime parsedDate = DateTime.Parse(obj.ToString());
            return DateTime.ParseExact(parsedDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = DiaBrowseFile.ShowDialog(this);
            if(result == DialogResult.OK)
            {
                filePath = DiaBrowseFile.FileName;
                txtPath.Text = filePath;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (MessageDialog.ShowConfirmationMsg(this, "Do you want to import data?") == DialogButton.Yes)
            {
                importData();
            } 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetControlDefaultValue();
        }
    }
}
