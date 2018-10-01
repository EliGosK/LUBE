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
using OfficeOpenXml;
using System.IO;
using System.Globalization;
using EAP.Framework;

namespace Presentation.Forms.Report
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmRPT050_VarianceReport : LUBEFormDev
    {
        #region Variables

        private RPT050_ReportBIZ m_bizReport = new RPT050_ReportBIZ();

        #endregion

        #region Constructor

        public FrmRPT050_VarianceReport()
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

                DataTable dtbResult = m_bizReport.GetDetailBudgetCapaVarianceReport(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue));

                if (!Util.IsNullOrEmptyOrZero(dtbResult.Rows.Count))
                {
                    //String path = @"D:\LUBE\01.Presentation\Report\Template\RPT050_Template.xlsx";
                    String path = Path.Combine(Application.StartupPath, "Report/Template/RPT050_Template.xlsx");
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

                            save.FileName = "ACS_Worksheet_Costing" + "_" + txtYear.Text + monthName;
                            save.Filter = "Execl files (*.xlsx)|*.xlsx";
                            if (save.ShowDialog() == DialogResult.OK)
                            {
                                foreach (DataRow row in dtbResult.Rows)
                                {
                                    int month = System.Convert.ToInt16(row["Period"]);
                                    string strMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).Substring(0, 3);
                                    sht.Cells["E1"].Value = strMonth + "-" + row["Year"].ToString().Substring(2, 2); //1
                                    sht.Cells["E2"].Value = row["MOHBudgetBase"]; //2
                                    sht.Cells["E3"].Value = row["MOHBudget"]; //3
                                    sht.Cells["E4"].Value = row["MOHAllocBase"]; //4
                                    sht.Cells["E5"].Value = row["NormalCapa"]; //5
                                    sht.Cells["E6"].Value = row["ActCapaUsed"]; //6
                                    sht.Cells["E7"].Value = row["ActMOH"]; //7
                                    sht.Cells["E8"].Value = row["MOHRecoveryRate"]; //8
                                    sht.Cells["E9"].Value = row["MOHAllocPlan"]; //9
                                    sht.Cells["E10"].Value = row["MOHAllocVariance"]; //10

                                    sht.Cells["E12"].Value = row["BudgetVariance"]; //11
                                    sht.Cells["G13"].Value = (System.Convert.ToDecimal(row["ActCapaUsed"]) - System.Convert.ToDecimal(row["NormalCapa"])); //12
                                    sht.Cells["G14"].Value = row["MOHRecoveryRate"]; //13
                                    sht.Cells["G15"].Value = row["CapaIdleVariance"]; //14
                                    sht.Cells["H16"].Value = row["Total"]; //15
                                    sht.Cells["H17"].Value = row["Diff"]; //16
                                    sht.Cells["D20"].Value = row["ActCapaUsed"]; //17
                                    sht.Cells["D21"].Value = row["SoldLiter"]; //18
                                    sht.Cells["D22"].Value = row["EndingLiter"]; //19
                                    sht.Cells["D25"].Value = row["SoldLiter"]; //20
                                    sht.Cells["D26"].Value = row["SoldLiterLT"]; //21
                                    sht.Cells["D27"].Value = row["SoldLiterOEM"]; //22

                                    bool IsBudgetAdvantage = System.Convert.ToInt32(row["IsBudgetAdvantage"]) == 1;
                                    bool IsCapaIdleAdvantage = System.Convert.ToInt32(row["IsCapaIdleAdvantage"]) == 1;

                                    if (IsBudgetAdvantage)
                                    {
                                        sht.Cells["E19"].Value = "Advantage"; //23
                                        sht.Cells["E20"].Value = "Decrease Value"; //24
                                        sht.Cells["E24"].Value = "Dr. MOH  (842002)"; //25
                                        sht.Cells["E25"].Value = "Dr. MOH - OEM  (842003)"; //25
                                        sht.Cells["E26"].Value = "Cr. COGS-BUDGET-LT  (501810)"; //25
                                        sht.Cells["E27"].Value = "Cr. COGS-BUDGET-OEM (501830)"; //25
                                        sht.Cells["F24"].Value = row["SoldBudgetMOH"]; //26
                                        sht.Cells["F25"].Value = row["SoldBudgetMOH_OEM"]; //27
                                        sht.Cells["F26"].Value = row["SoldBudgetCOGS_LT"]; //28
                                        sht.Cells["F27"].Value = row["SoldBudgetCOGS_OEM"]; //29
                                        sht.Cells["E31"].Value = "Dr. MOH (842002)"; //30
                                        sht.Cells["E32"].Value = "Dr. MOH-OEM (842003)"; //30
                                        sht.Cells["E33"].Value = "Cr. Ending Inventory(FG) (104800)"; //30
                                        sht.Cells["E34"].Value = "Cr. Ending Inventory(FG)-OEM (104801)"; //30
                                        //sht.Cells["E35"].Value = "(Produce in the month)"; //30
                                        sht.Cells["F31"].Value = row["UnSoldBudgetMOH"]; //31
                                        sht.Cells["F32"].Value = row["UnSoldBudgetMOH_OEM"]; //32
                                        sht.Cells["F33"].Value = row["UnSoldBudgetEndingInv"]; //33
                                        sht.Cells["F34"].Value = row["UnSoldBudgetEndingInv_OEM"]; //34
                                    }
                                    else
                                    {
                                        sht.Cells["E19"].Value = "Disadvantage"; //23
                                        sht.Cells["E20"].Value = "Increase Value"; //24
                                        sht.Cells["E24"].Value = "Dr. COGS-BUDGET-LT (501810)"; //25
                                        sht.Cells["E25"].Value = "Dr. COGS-BUDGET-OEM (501830)"; //25
                                        sht.Cells["E26"].Value = "Cr. MOH (842002)"; //25
                                        sht.Cells["E27"].Value = "Cr. MOH - OEM (842003)"; //25
                                        sht.Cells["F24"].Value = row["SoldBudgetCOGS_LT"]; //26
                                        sht.Cells["F25"].Value = row["SoldBudgetCOGS_OEM"]; //27
                                        sht.Cells["F26"].Value = row["SoldBudgetMOH"]; //28
                                        sht.Cells["F27"].Value = row["SoldBudgetMOH_OEM"]; //29
                                        sht.Cells["E31"].Value = "Dr. Ending Inventory(FG) (104800)"; //30
                                        sht.Cells["E32"].Value = "Dr. Ending Inventory(FG)-OEM (104801)"; //30
                                        //sht.Cells["E33"].Value = "(Produce in the month)"; //30
                                        sht.Cells["E33"].Value = "Cr. MOH (842002)"; //30
                                        sht.Cells["E34"].Value = "Cr. MOH-OEM (842003)"; //30
                                        sht.Cells["F31"].Value = row["UnSoldBudgetEndingInv"]; //31
                                        sht.Cells["F32"].Value = row["UnSoldBudgetEndingInv_OEM"]; //32
                                        sht.Cells["F33"].Value = row["UnSoldBudgetMOH"]; //33
                                        sht.Cells["F34"].Value = row["UnSoldBudgetMOH_OEM"]; //34
                                    }

                                    if (IsCapaIdleAdvantage)
                                    {
                                        sht.Cells["G19"].Value = "Advantage"; //35
                                        sht.Cells["G20"].Value = "Decrease Value"; //36
                                        sht.Cells["G24"].Value = "Dr. MOH (842002)"; //37
                                        sht.Cells["G25"].Value = "Dr. MOH-OEM (842003)"; //37
                                        sht.Cells["G26"].Value = "Cr. COGS-IDLE-LT (501820)"; //37
                                        sht.Cells["G27"].Value = "Cr. COGS-IDLE-OEM (501840)"; //37
                                        sht.Cells["H24"].Value = row["SoldCapaMOH"]; //38
                                        sht.Cells["H25"].Value = row["SoldCapaMOH_OEM"]; //39
                                        sht.Cells["H26"].Value = row["SoldCapaCOGS_LT"]; //40
                                        sht.Cells["H27"].Value = row["SoldCapaCOGS_OEM"]; //41
                                        sht.Cells["G31"].Value = "Dr. MOH (842002)"; //42
                                        sht.Cells["G32"].Value = "Dr. MOH-OEM (842003)"; //42
                                        sht.Cells["G33"].Value = "Cr. Ending Inventory(FG) (104800)"; //42
                                        sht.Cells["G34"].Value = "Cr. Ending Inventory(FG)-OEM (104801)"; //42
                                        //sht.Cells["G35"].Value = "(Produce in the month)"; //42
                                        sht.Cells["H31"].Value = row["UnSoldCapaMOH"]; //43
                                        sht.Cells["H32"].Value = row["UnSoldCapaMOH_OEM"]; //44
                                        sht.Cells["H33"].Value = row["UnSoldCapaEndingInv"]; //45
                                        sht.Cells["H34"].Value = row["UnSoldCapaEndingInv_OEM"]; //46
                                    }
                                    else
                                    {
                                        //20180601 May Modify
                                        sht.Cells["G19"].Value = "Disadvantage"; //35
                                        sht.Cells["G20"].Value = "Increase Special"; //36
                                        sht.Cells["G21"].Value = "Expense account under";
                                        sht.Cells["G22"].Value = "Sales & Admin Expense";
                                        sht.Cells["G24"].Value = "Dr. Idle (Lower Capacity) (613600)"; //37
                                        sht.Cells["G25"].Value = "Dr. Idle (Lower Capacity)-OEM (613600)"; //37
                                        sht.Cells["G26"].Value = "Cr. MOH (842002)"; //37
                                        sht.Cells["G27"].Value = "Cr. MOH-OEM (842003)"; //37
                                        sht.Cells["H24"].Value = row["SoldCapaCOGS_LT"]; //38
                                        sht.Cells["H25"].Value = row["SoldCapaCOGS_OEM"]; //39
                                        sht.Cells["H26"].Value = row["SoldCapaMOH"]; //40
                                        sht.Cells["H27"].Value = row["SoldCapaMOH_OEM"]; //41
                                        sht.Cells["G31"].Value = "Dr. Idle (Lower Capacity) (613600)"; //42
                                        sht.Cells["G32"].Value = "Dr. Idle (Lower Capacity)-OEM (613600)"; //42
                                        sht.Cells["G33"].Value = "Cr. MOH (842002)"; //42
                                        sht.Cells["G34"].Value = "Cr. MOH-OEM (842003)"; //42
                                        //sht.Cells["G35"].Value = "(Produce in the month)"; //42
                                        sht.Cells["H31"].Value = row["UnSoldCapaEndingInv"]; //43
                                        sht.Cells["H32"].Value = row["UnSoldCapaEndingInv_OEM"]; //44
                                        sht.Cells["H33"].Value = row["UnSoldCapaMOH"]; //45
                                        sht.Cells["H34"].Value = row["UnSoldCapaMOH_OEM"]; //46

                                        //sht.Cells["G19"].Value = "Disadvantage"; //35
                                        //sht.Cells["G20"].Value = "Increase"; //36
                                        //sht.Cells["G24"].Value = "Dr. COGS-LT"; //37
                                        //sht.Cells["G25"].Value = "Dr. COGS-OEM"; //37
                                        //sht.Cells["G26"].Value = "Cr. MOH"; //37
                                        //sht.Cells["G27"].Value = "Cr. MOH-OEM"; //37
                                        //sht.Cells["H24"].Value = row["SoldCapaCOGS_LT"]; //38
                                        //sht.Cells["H25"].Value = row["SoldCapaCOGS_OEM"]; //39
                                        //sht.Cells["H26"].Value = row["SoldCapaMOH"]; //40
                                        //sht.Cells["H27"].Value = row["SoldCapaMOH_OEM"]; //41
                                        //sht.Cells["G31"].Value = "Dr. Ending Inventory(FG)"; //42
                                        //sht.Cells["G32"].Value = "Dr. Ending Inventory(FG)-OEM"; //42
                                        //sht.Cells["G33"].Value = "(Produce in the month)"; //42
                                        //sht.Cells["G34"].Value = "Cr. MOH"; //42
                                        //sht.Cells["G35"].Value = "Cr. MOH-OEM"; //42
                                        //sht.Cells["H31"].Value = row["UnSoldCapaEndingInv"]; //43
                                        //sht.Cells["H32"].Value = row["UnSoldCapaEndingInv_OEM"]; //44
                                        //sht.Cells["H34"].Value = row["UnSoldCapaMOH"]; //45
                                        //sht.Cells["H35"].Value = row["UnSoldCapaMOH_OEM"]; //46
                                        
                                    }

                                    sht.Cells["D32"].Value = row["BudgetAfterAdjust"]; //47
                                    sht.Cells["D33"].Value = row["CapaIdleVariance"]; //48
                                    sht.Cells["D34"].Value = row["EndingLiter"]; //49
                                    sht.Cells["D35"].Value = row["EndingLiterLT"]; //50
                                    sht.Cells["D36"].Value = row["EndingLiterOEM"]; //51
                                }
                                excel.SaveAs(new FileInfo(save.FileName));
                                DialogResult result = MessageBox.Show("Save Completed", "RPT050", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    System.Diagnostics.Process.Start(save.FileName);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error : Cannot find Report Template.", "RPT050", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Data not Found.", "RPT050", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(this, ex);
            }
        }
        #endregion
    }
}
