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

namespace Presentation.Forms.Report
{
    public partial class FrmPreviewReport : Form, IReportViewer
    {
        #region Member

        private ReportDocument _report = null;

        #endregion

        #region Constructor

        public FrmPreviewReport()
        {
            InitializeComponent();
        }

        #endregion

        #region Public method

        public void SetReportName(string reportName)
        {
            string strTitle = string.Format("Report Preview: {0}", reportName);
            this.Text = strTitle;
        }
        public void SetReportSource(ReportDocument report)
        {
            _report = report;            
        }

        public DialogResult ShowReport(IWin32Window window, ReportDocument report)
        {
            _report = report;            
            return this.ShowDialog(window);
        }
        #endregion

        #region Form Events

        private void FrmPreviewReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 2016-08-20 Teerayut S. : When use for long time may occuring Memory Leak.
            // Release memory resource.
            ReportDocument rpt = rptViewer.ReportSource as ReportDocument;
            if (rpt != null)
            {
                rpt.Close();
                rpt.Dispose();
                rpt = null;
            }
            
            rptViewer.ReportSource = null;
            rptViewer.Dispose();
        }

        private void FrmPreviewReport_Shown(object sender, EventArgs e)
        {
            rptViewer.ReportSource = _report;
            rptViewer.Zoom(100);     
        }

        #endregion
    }
}
