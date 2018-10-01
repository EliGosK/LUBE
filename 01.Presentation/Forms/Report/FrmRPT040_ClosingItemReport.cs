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
    public partial class FrmRPT040_ClosingItemReport : LUBEFormDev
    {
        int? itmGrpFrom;
        int? itmGrpTo;
        string stritmCodeFrom;
        string stritmCodeTo;
        DataTable m_dtbItm;
        DataTable m_dtbItm2;

        #region Variables

        private RPT040_ReportBIZ m_bizReport = new RPT040_ReportBIZ();

        #endregion

        #region Constructor

        public FrmRPT040_ClosingItemReport()
        {
            InitializeComponent();
            InitialScreen();
        }

        #endregion

        private void InitialScreen()
        {
            BindPeriodCombobox();
            BindItemGroupComboBox();
            BindItemCombobox();
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

        private void BindItemGroupComboBox()
        {
            DataTable dtbItmGrp = m_bizReport.GetItemGroupComboBox(null);

            cbItmGrpFrom.DataSource = dtbItmGrp;
            cbItmGrpFrom.ValueMember = "ItmsGrpCod";
            cbItmGrpFrom.DisplayMember = "ItmsGrpNam";

            cbItmGrpTo.DataSource = dtbItmGrp.Copy();
            cbItmGrpTo.ValueMember = "ItmsGrpCod";
            cbItmGrpTo.DisplayMember = "ItmsGrpNam";
        }

        private void BindItemCombobox()
        {
            m_dtbItm = m_bizReport.GetItemCombobox(null, null);

            m_dtbItm.Columns.Add("colDisplay", typeof(String));

            foreach (DataRow row in m_dtbItm.Rows)
            {
                row["colDisplay"] = row["ItemCode"].ToString() + " : " + row["ItemName"].ToString();
            }

            m_dtbItm2 = m_dtbItm.Copy();

            itmCodeFrom.DataSource = m_dtbItm.Rows.Cast<System.Data.DataRow>().Take(20).CopyToDataTable();
            itmCodeFrom.ValueMember = "ItemCode";
            itmCodeFrom.DisplayMember = "colDisplay";
            itmCodeFrom.SelectedIndex = -1;
            itmCodeFrom.TextUpdate += new System.EventHandler((object sender, EventArgs e) =>
            {
                string filter = itmCodeFrom.Text;
                var tmp = m_dtbItm.Select(@"ItemCode like '%" + filter + "%'");
                if(tmp.Count() > 0)
                {
                    itmCodeFrom.DataSource = tmp.Cast<System.Data.DataRow>().Take(20).CopyToDataTable();
                    itmCodeFrom.ValueMember = "ItemCode";
                    itmCodeFrom.DisplayMember = "colDisplay";
                    itmCodeFrom.DroppedDown = true;
                }
                else
                {
                    itmCodeFrom.DroppedDown = false;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ItemCode", typeof(String));
                    dt.Columns.Add("colDisplay", typeof(String));

                    itmCodeFrom.DataSource = null;
                    itmCodeFrom.Items.Clear();
                }

                Cursor.Current = Cursors.Default;

                itmCodeFrom.IntegralHeight = true;
                itmCodeFrom.SelectedIndex = -1;

                itmCodeFrom.Text = filter;

                itmCodeFrom.SelectionStart = filter.Length;
                itmCodeFrom.SelectionLength = 0;
            });

            
            itmCodeTo.DataSource = m_dtbItm2.Rows.Cast<System.Data.DataRow>().Take(20).CopyToDataTable();
            itmCodeTo.ValueMember = "ItemCode";
            itmCodeTo.DisplayMember = "colDisplay";
            itmCodeTo.SelectedIndex = -1;
            itmCodeTo.TextUpdate += new System.EventHandler((object sender, EventArgs e) =>
            {
                string filter = itmCodeTo.Text;
                var tmp2 = m_dtbItm2.Select(@"ItemCode like '%" + filter + "%'");
                if (tmp2.Count() > 0)
                {
                    itmCodeTo.DataSource = tmp2.Cast<System.Data.DataRow>().Take(20).CopyToDataTable();
                    itmCodeTo.ValueMember = "ItemCode";
                    itmCodeTo.DisplayMember = "colDisplay";
                    itmCodeTo.DroppedDown = true;
                }
                else
                {
                    itmCodeTo.DroppedDown = false;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("ItemCode", typeof(String));
                    dt.Columns.Add("colDisplay", typeof(String));

                    itmCodeTo.DataSource = null;
                    itmCodeTo.Items.Clear();
                }

                Cursor.Current = Cursors.Default;

                itmCodeTo.IntegralHeight = true;
                itmCodeTo.SelectedIndex = -1;

                itmCodeTo.Text = filter;

                itmCodeTo.SelectionStart = filter.Length;
                itmCodeTo.SelectionLength = 0;
            });
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

            if (Util.IsNullOrEmptyOrZero(cbItmGrpFrom.SelectedValue))
                itmGrpFrom = null;
            else
                itmGrpFrom = Util.ConvertObjectToInteger(cbItmGrpFrom.SelectedValue);

            if (Util.IsNullOrEmptyOrZero(cbItmGrpTo.SelectedValue))
                itmGrpTo = null;
            else
                itmGrpTo = Util.ConvertObjectToInteger(cbItmGrpTo.SelectedValue);

            if (Util.IsNullOrEmpty(itmCodeFrom.SelectedValue))
                stritmCodeFrom = null;
            else
                stritmCodeFrom = itmCodeFrom.SelectedValue.ToString();

            if (Util.IsNullOrEmpty(itmCodeTo.SelectedValue))
                stritmCodeTo = null;
            else
                stritmCodeTo = itmCodeTo.SelectedValue.ToString();

            return bValid;
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateRequire())
                    return;

                DataTable dtbResult = m_bizReport.GetClosingItemReport(txtYear.IntValue, Convert.ToInt16(cboMonth.SelectedValue)
                                                                , itmGrpFrom, itmGrpTo, stritmCodeFrom, stritmCodeTo);

                if(!Util.IsNullOrEmptyOrZero(dtbResult.Rows.Count))
                {
                    AppEnvironment.ShowWaitForm("Please Wait", "Initializing Report.");
                    ReportDocument rpt = ReportUtil.LoadReport("RPT040_ClosingItemReporty.rpt");
                    rpt.SetDataSource(dtbResult);
                    string monthYear = cboMonth.Text + " " + txtYear.Text;
                    rpt.SetParameterValue("MonthYear", monthYear);

                    FrmPreviewReport frmPrint = new FrmPreviewReport();
                    AppEnvironment.CloseWaitForm();

                    ReportUtil.PrintPreviewReport(frmPrint, rpt);
                }
                else
                {
                    MessageBox.Show("Data not Found.", "RPT040", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                AppEnvironment.CloseWaitForm();
                ExceptionManager.ManageException(this, ex);
            }
        }

        #endregion

        private void itmCodeFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            //DataRow[] dr = m_dtbItm.Select(@"ItemCode like '" + e.KeyChar + "%'");
            //DataTable dt = m_dtbItm.Copy();
            //dt.Clear();
            //foreach (DataRow item in dr)
            //{
            //    dt.ImportRow(item);
            //}
            //itmCodeFrom.DataSource = dt;
        }
    }
}
