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

namespace Presentation.Forms.Report
{
    [ScreenPermission(Permission.OpenScreen)]
    public partial class FrmRPT999_ReportTest : LUBEFormDev
    {
        #region Constructor

        public FrmRPT999_ReportTest()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Events

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ReportDocument rpt = ReportUtil.LoadReport("RPT999_ReportTest.rpt");
            
            FrmPreviewReport frmPrint = new FrmPreviewReport();
            ReportUtil.PrintPreviewReport(frmPrint, rpt);
        }

        #endregion
    }
}
