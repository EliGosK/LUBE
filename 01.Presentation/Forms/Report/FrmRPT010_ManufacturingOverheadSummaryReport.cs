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
using NECT_EDI.SAP;
using EAP.Framework;

namespace Presentation.Forms.Report
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmRPT010_ManufacturingOverheadSummaryReport : LUBEFormDev
    {
        #region Variables

        private RPT010_ReportBIZ m_bizReport = new RPT010_ReportBIZ();

        #endregion

        #region Constructor

        public FrmRPT010_ManufacturingOverheadSummaryReport()
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

                DataTable dtbResult = m_bizReport.GetMOHSummaryReport(Util.ConvertObjectToInteger(cboMonth.SelectedValue), txtYear.IntValue);

                if (!Util.IsNullOrEmptyOrZero(dtbResult.Rows.Count))
                {
                    AppEnvironment.ShowWaitForm("Please Wait", "Initializing Report.");
                    ReportDocument rpt = ReportUtil.LoadReport("RPT010_MOHSummaryReport.rpt");
                    rpt.SetDataSource(dtbResult);
                    string monthYear = cboMonth.Text + " " + txtYear.Text;
                    rpt.SetParameterValue("MonthYear", monthYear);
                    FrmPreviewReport frmPrint = new FrmPreviewReport();

                    AppEnvironment.CloseWaitForm();

                    ReportUtil.PrintPreviewReport(frmPrint, rpt);
                }
                else
                {
                    MessageBox.Show("Data not Found.", "RPT010", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } 
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }
        #endregion
    }
}
