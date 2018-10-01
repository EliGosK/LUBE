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
using EAP.Framework;

namespace Presentation.Forms.Report
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmRPT020_CostReport : LUBEFormDev
    {
        string strProdDateFrom;
        string strProdDateTo;
        string strProdOrderNoFrom;
        string strProdOrderNoTo;
        string strItemType;

        #region Variables

        private RPT020_ReportBIZ m_bizReport = new RPT020_ReportBIZ();

        #endregion

        #region Constructor

        public FrmRPT020_CostReport()
        {
            InitializeComponent();
            InitialScreen();
        }

        #endregion

        private void InitialScreen()
        {
            BindPeriodCombobox();
            txtYear.Text = (DateTime.Now.Year).ToString();
            //ProdDateFrom.Value = DateTime.Now.Date;
            //ProdDateTo.Value = DateTime.Now.Date;

            var year = Convert.ToInt16(txtYear.Text);
            var month = Convert.ToInt16(cboMonth.SelectedValue);
            ProdDateFrom.Value = new DateTime(year, month, 1);
            ProdDateTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            radioBtnType1.Checked = true;
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

        #region Validate

        private bool ValidateRequire()
        {
            errorProvider.Clear();

            bool bValid = true;

            if (Util.IsNullOrEmptyOrZero(txtYear.IntValue))
            {
                errorProvider.SetError(txtYear, "Please input: Year");
                bValid = false;
            }

            if (ProdDateFrom.Value > ProdDateTo.Value)
            {
                errorProvider.SetError(ProdDateTo, "Please input ProductionDate(To) more than ProductionDate(From).");
                bValid = false;
            }

            if (ProdDateFrom.Value == DateTime.MinValue)
            {
                errorProvider.SetError(ProdDateFrom, "Please input: Production Date (From)");
                bValid = false;
            }
            else
            {
                strProdDateFrom = ProdDateFrom.Value.ToString("yyyyMMdd");
            }
                
            if (ProdDateTo.Value == DateTime.MinValue)
            {
                errorProvider.SetError(ProdDateTo, "Please input: Production Date (To)");
                bValid = false;
            }
            else
            {
                strProdDateTo = ProdDateTo.Value.ToString("yyyyMMdd");
            }

            if (Util.IsNullOrEmpty(ProdOrderNoFrom.Text))
                strProdOrderNoFrom = null;
            else
                strProdOrderNoFrom = ProdOrderNoFrom.Text;

            if (Util.IsNullOrEmpty(ProdOrderNoTo.Text))
                strProdOrderNoTo = null;
            else
                strProdOrderNoTo = ProdOrderNoTo.Text;

            return bValid;

        }

        #endregion

        #region Control Events

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateRequire())
                    return;

                if (radioBtnType1.Checked)
                    strItemType = radioBtnType1.Text;
                else
                    strItemType = radioBtnType2.Text;

                DataTable dtbResult = m_bizReport.GetCostReports(txtYear.IntValue, Util.ConvertObjectToInteger(cboMonth.SelectedValue)
                                                                , strProdDateFrom, strProdDateTo, strProdOrderNoFrom, strProdOrderNoTo, strItemType);

                if (!Util.IsNullOrEmptyOrZero(dtbResult.Rows.Count))
                {
                    AppEnvironment.ShowWaitForm("Please Wait", "Initializing Report.");
                    ReportDocument rpt = ReportUtil.LoadReport("RPT020_CostReport.rpt");
                    rpt.SetDataSource(dtbResult);
                    string monthYear = cboMonth.Text + " " + txtYear.Text;

                    DateTime minDate = dtbResult.AsEnumerable()
                               .Select(cols => cols.Field<DateTime>("DocDate"))
                               .FirstOrDefault();

                    DateTime maxDate = dtbResult.AsEnumerable()
                              .Select(cols => cols.Field<DateTime>("DocDate"))
                              .OrderByDescending(p => p.Ticks)
                              .FirstOrDefault();

                    //string dateMinMax = minDate.ToString("dd/MM/yyyy") + " - " + maxDate.ToString("dd/MM/yyyy");
                    string dateMinMax = ProdDateFrom.Text + " - " + ProdDateTo.Text;
                    rpt.SetParameterValue("dateMinMax", dateMinMax);
                    rpt.SetParameterValue("MonthYear", monthYear);
                    FrmPreviewReport frmPrint = new FrmPreviewReport();

                    AppEnvironment.CloseWaitForm();

                    ReportUtil.PrintPreviewReport(frmPrint, rpt);
                }
                else
                {
                    MessageBox.Show("Data not Found.", "RPT020", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        private void cboMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var year = Convert.ToInt16(txtYear.Text);
            var month = Convert.ToInt16(cboMonth.SelectedValue);
            ProdDateFrom.Value = new DateTime(year, month, 1);
            ProdDateTo.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        #endregion

    }
}
