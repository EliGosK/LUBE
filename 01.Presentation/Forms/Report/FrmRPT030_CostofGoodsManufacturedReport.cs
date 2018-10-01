using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using CrystalDecisions.CrystalReports.Engine;
using EAP.Framework.Data;
using BusinessService;
using EAP.Framework.Windows;
using System.IO;
using OfficeOpenXml;
using EAP.Framework;

namespace Presentation.Forms.Report
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmRPT030_CostofGoodsManufacturedReport : LUBEFormDev
    {
        #region Variables

        private RPT030_ReportBIZ m_bizReport = new RPT030_ReportBIZ();

        #endregion

        #region Constructor

        public FrmRPT030_CostofGoodsManufacturedReport()
        {
            InitializeComponent();
            InitialScreen();
        }

        #endregion

        private void InitialScreen()
        {
            BindPeriodCombobox();
            txtYear.Text = (DateTime.Now.Year).ToString();
            ControlUtil.VisibleControl(false, m_toolBarFind, m_toolBarAdd, m_toolBarEdit, m_toolBarDelete, m_toolBarSave, m_toolBarCancel, m_toolBarRefresh, m_toolBarPrint, m_toolBarExport, m_toolBarImport);
            UpdateToolbarSeparator();
        }

        private void BindPeriodCombobox()
        {
            DataTable dtbPeriod = m_bizReport.GetPeriodCombobox();

            cboMonth.DataSource = dtbPeriod;
            cboMonth.ValueMember = "PeriodID";
            cboMonth.DisplayMember = "PeriodName";
        }

        #region Control Events

        private bool ValidateRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please input: Year");
                bValid = false;
            }

            return bValid;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateRequire())
                    return;

                DataTable dtbHeader = m_bizReport.GetCostOfGoodsManuReport_Header(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));
                DataTable dtbDetail = m_bizReport.CostOfGoodsManuReport_Detail(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));

                if (!Util.IsNullOrEmptyOrZero(dtbHeader.Rows.Count) && !Util.IsNullOrEmptyOrZero(dtbDetail.Rows.Count))
                {
                    String path = Path.Combine(Application.StartupPath, "Report/Template/RPT030_Template.xlsx");
                    FileInfo fiRPT050 = new FileInfo(path);

                    if (fiRPT050.Exists)
                    {
                        using (ExcelPackage excel = new ExcelPackage(fiRPT050))
                        {
                            var sht = excel.Workbook.Worksheets[1];

                            SaveFileDialog save = new SaveFileDialog();
                            string monthName = string.Empty;
                            if (cboMonth.SelectedValue.ToString().Length == 1)
                                monthName = "0" + cboMonth.SelectedValue.ToString();
                            else
                                monthName = cboMonth.SelectedValue.ToString();

                            save.FileName = "ACS_Costing_FG" + "_" + txtYear.Text + monthName;
                            save.Filter = "Execl files (*.xlsx)|*.xlsx";
                            if (save.ShowDialog() == DialogResult.OK)
                            {
                                //Header
                                foreach (DataRow hed in dtbHeader.Rows)
                                {
                                    sht.Cells["O2"].Value = hed["MOHRecoveryRate"]; //1
                                    sht.Cells["AE2"].Value = hed["ActCapaUsed"]; //2
                                    sht.Cells["AE3"].Value = hed["EndingLiter"]; //3
                                    sht.Cells["AG2"].Value = hed["UnSoldBudgetAll"]; //4
                                    sht.Cells["AH2"].Value = hed["UnSoldCapaAll"]; //5
                                    sht.Cells["AH3"].Value = hed["AdjInvForReva"]; //6

                                    if (Util.ConvertObjectToDecimal(hed["AdjInvForReva"]) > 0)
                                    {
                                        sht.Cells["AG3"].Value = "Increase Inventory";
                                    }
                                    else
                                    {
                                        sht.Cells["AG3"].Value = "Decrease Inventory";
                                    }
                                }
                                //End Header

                                //Detail
                                int rowIndex = 10;
                                int lastRowIndex = dtbDetail.Rows.Count - 1;
                                for (int i = 0; i < dtbDetail.Rows.Count; i++)
                                {
                                    if(i == lastRowIndex)
                                        sht.Cells[6, 1, 6, 35].Copy(sht.Cells[rowIndex, 1, rowIndex, 35]);
                                    else
                                        sht.Cells[5, 1, 5, 35].Copy(sht.Cells[rowIndex, 1, rowIndex, 35]);

                                    sht.Cells[rowIndex, 1].Value = dtbDetail.Rows[i]["PostingDate"]; //7
                                    sht.Cells[rowIndex, 2].Value = dtbDetail.Rows[i]["BatchNo"]; //8
                                    sht.Cells[rowIndex, 3].Value = dtbDetail.Rows[i]["ItemCode"]; //9
                                    sht.Cells[rowIndex, 4].Value = dtbDetail.Rows[i]["BOM3"]; //20180612 May Add
                                    sht.Cells[rowIndex, 5].Value = dtbDetail.Rows[i]["DocumentNo"]; //10
                                    sht.Cells[rowIndex, 6].Value = dtbDetail.Rows[i]["Quantity"]; //11
                                    sht.Cells[rowIndex, 7].Value = dtbDetail.Rows[i]["QTYPPKG"]; //12

                                    //sht.Cells[rowIndex, 8].Value = dtbDetail.Rows[i]["Liter"]; //13
                                    sht.Cells[rowIndex, 8].Formula = "= F" + rowIndex + "*G" + rowIndex;//13

                                    sht.Cells[rowIndex, 9].Value = dtbDetail.Rows[i]["DueDate"]; //14
                                    //sht.Cells[rowIndex, 10].Value = dtbDetail.Rows[i]["Lot1"];
                                    sht.Cells[rowIndex, 10].Value = dtbDetail.Rows[i]["DisAssemQty"]; //Modified by Pachara S.
                                    sht.Cells[rowIndex, 11].Value = dtbDetail.Rows[i]["Ref2_Manual"]; 
                                    sht.Cells[rowIndex, 12].Value = dtbDetail.Rows[i]["BaseRef"]; //15
                                    sht.Cells[rowIndex, 13].Value = dtbDetail.Rows[i]["RM_Cost_Cal"]; //16
                                    sht.Cells[rowIndex, 14].Value = dtbDetail.Rows[i]["RM_Cost_B1_Display"]; //17
                                    sht.Cells[rowIndex, 15].Value = dtbDetail.Rows[i]["StdOH"]; //18
                                    sht.Cells[rowIndex, 16].Value = dtbDetail.Rows[i]["Variance_BOM1"]; //19
                                    sht.Cells[rowIndex, 17].Value = dtbDetail.Rows[i]["BOM1Cost"]; //20
                                    sht.Cells[rowIndex, 18].Value = dtbDetail.Rows[i]["ContainerCost"]; //21
                                    sht.Cells[rowIndex, 19].Value = dtbDetail.Rows[i]["LabelCost"]; //22
                                    sht.Cells[rowIndex, 20].Value = dtbDetail.Rows[i]["ActualCostFG"]; //23
                                    sht.Cells[rowIndex, 21].Value = dtbDetail.Rows[i]["Variance_BOM2"]; //24
                                    sht.Cells[rowIndex, 22].Value = dtbDetail.Rows[i]["ReceiptQty"]; //25
                                    sht.Cells[rowIndex, 23].Value = dtbDetail.Rows[i]["CalPrice"]; //26
                                    sht.Cells[rowIndex, 24].Value = dtbDetail.Rows[i]["TransValue"]; //27
                                    sht.Cells[rowIndex, 25].Value = dtbDetail.Rows[i]["WhsCode"]; //28
                                    sht.Cells[rowIndex, 26].Value = dtbDetail.Rows[i]["SoldQuantity"]; //29

                                    //sht.Cells[rowIndex, 27].Value = dtbDetail.Rows[i]["SoldLiter"]; //30
                                    sht.Cells[rowIndex, 27].Formula = "= Z" + rowIndex + "*G" + rowIndex;//30

                                    sht.Cells[rowIndex, 28].Value = dtbDetail.Rows[i]["COGS"]; //31
                                    sht.Cells[rowIndex, 29].Value = dtbDetail.Rows[i]["EndingBalQuantity"]; //32
                                    sht.Cells[rowIndex, 30].Value = dtbDetail.Rows[i]["EndingBalLiter"]; //33

                                    sht.Cells[rowIndex, 31].Formula = "= IF(AC" + rowIndex + " <> 0, ROUND(AF" + rowIndex + "/ AC" + rowIndex + ", 4), 0)"; //34
                                    //sht.Cells[rowIndex, 31].Value = dtbDetail.Rows[i]["Uprice"]; //34

                                    sht.Cells[rowIndex, 32].Value = dtbDetail.Rows[i]["EndingBalAmount"]; //35

                                    sht.Cells[rowIndex, 33].Formula = "= ROUND(AD" + rowIndex + "*AH3/AE3,2)"; //36
                                    sht.Cells[rowIndex, 34].Formula = "= AF" + rowIndex + "+AG" + rowIndex; //37
                                    sht.Cells[rowIndex, 35].Formula = "= IF(AH" + rowIndex + " <> 0, ROUND(AH" + rowIndex + "/ AC" + rowIndex + ", 4), 0)"; //38
                                    //sht.Cells[rowIndex, 33].Value = dtbDetail.Rows[i]["Effect"]; //36
                                    //sht.Cells[rowIndex, 34].Value = dtbDetail.Rows[i]["NewAmount"]; //37
                                    //sht.Cells[rowIndex, 35].Value = dtbDetail.Rows[i]["NewPrice"]; //38

                                    rowIndex++;
                                }
                                //End Detail

                                //Footer
                                sht.Cells[7, 1, 7, 34].Copy(sht.Cells[rowIndex, 1, rowIndex, 34]);
                                string sumToRow = (rowIndex - 1).ToString();

                                sht.Cells[rowIndex, 6].Formula = "=SUM(F10:F" + sumToRow + ")"; //39
                                sht.Cells[rowIndex, 8].Formula = "=SUM(H10:H" + sumToRow + ")"; //40
                                sht.Cells[rowIndex, 10].Formula = "=SUM(J10:J" + sumToRow + ")"; // Modified by Pachara S.
                                sht.Cells[rowIndex, 13].Formula = "=SUM(M10:M" + sumToRow + ")"; //41
                                sht.Cells[rowIndex, 14].Formula = "=SUM(N10:N" + sumToRow + ")"; //42
                                sht.Cells[rowIndex, 15].Formula = "=SUM(O10:O" + sumToRow + ")"; //43
                                sht.Cells[rowIndex, 16].Formula = "=SUM(P10:P" + sumToRow + ")"; //44
                                sht.Cells[rowIndex, 17].Formula = "=SUM(Q10:Q" + sumToRow + ")"; //45
                                sht.Cells[rowIndex, 18].Formula = "=SUM(R10:R" + sumToRow + ")"; //46
                                sht.Cells[rowIndex, 19].Formula = "=SUM(S10:S" + sumToRow + ")"; //47
                                sht.Cells[rowIndex, 20].Formula = "=SUM(T10:T" + sumToRow + ")"; //48
                                sht.Cells[rowIndex, 21].Formula = "=SUM(U10:U" + sumToRow + ")"; //49
                                sht.Cells[rowIndex, 22].Formula = "=SUM(V10:V" + sumToRow + ")"; //50
                                sht.Cells[rowIndex, 24].Formula = "=SUM(X10:X" + sumToRow + ")"; //51
                                sht.Cells[rowIndex, 26].Formula = "=SUM(Z10:Z" + sumToRow + ")"; //52
                                sht.Cells[rowIndex, 27].Formula = "=SUM(AA10:AA" + sumToRow + ")"; //53
                                sht.Cells[rowIndex, 28].Formula = "=SUM(AB10:AB" + sumToRow + ")"; //54
                                sht.Cells[rowIndex, 29].Formula = "=SUM(AC10:AC" + sumToRow + ")"; //55
                                sht.Cells[rowIndex, 30].Formula = "=SUM(AD10:AD" + sumToRow + ")"; //56
                                sht.Cells[rowIndex, 32].Formula = "=SUM(AF10:AF" + sumToRow + ")"; //57
                                sht.Cells[rowIndex, 33].Formula = "=SUM(AG10:AG" + sumToRow + ")"; //58
                                sht.Cells[rowIndex, 34].Formula = "=SUM(AH10:AH" + sumToRow + ")"; //59

                                int spcFtIndex = rowIndex + 1;
                                sht.Cells[8, 5, 8, 17].Copy(sht.Cells[spcFtIndex, 5, spcFtIndex, 17]);
                                foreach (DataRow hed in dtbHeader.Rows)
                                {
                                    sht.Cells[spcFtIndex, 8].Value = Convert.ToDecimal(hed["SP_Liter"]) * -1; //60
                                    sht.Cells[spcFtIndex, 13].Value = hed["SP_RM_Cost_Cal"]; //61
                                    sht.Cells[spcFtIndex, 14].Value = hed["SP_RMCost_B1"]; //62
                                    sht.Cells[spcFtIndex, 15].Value = hed["SP_StdOH"]; //63
                                    sht.Cells[spcFtIndex, 17].Value = hed["SP_BOM1_Cost"]; //64
                                }
                                //End Footer

                                //Delete Footer
                                sht.DeleteRow(5, 5, true);

                                excel.SaveAs(new FileInfo(save.FileName));
                                DialogResult result = MessageBox.Show("Save Completed", "RPT030", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    System.Diagnostics.Process.Start(save.FileName);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error : Cannot find Report Template.", "RPT030", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data not Found.", "RPT030", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        #endregion
    }
}
