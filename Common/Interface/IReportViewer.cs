using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Common
{
    public interface IReportViewer
    {
        void SetReportSource(ReportDocument report);
        DialogResult ShowReport(IWin32Window window, ReportDocument report);
    }
}
