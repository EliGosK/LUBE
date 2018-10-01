using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Common
{
    public class ReportUtil
    {
        #region Constructor

        public ReportUtil()
        {
        }

        #endregion

        public static ReportDocument LoadReport(string filename)
        {

            string reportFolder;
            reportFolder = Path.Combine(Application.StartupPath, "Report");

            string reportPath = Path.Combine(reportFolder, filename);

            ReportDocument rpt = new ReportDocument();
            rpt.Load(reportPath);

            return rpt;
        }

        public static void PrintPreviewReport(IReportViewer viewer, string filename, DataTable dt)
        {
            ReportDocument rpt = LoadReport(filename);
            rpt.SetDataSource(dt);

            PrintPreviewReport(viewer, rpt);
        }
        public static void PrintPreviewReport(IReportViewer viewer, ReportDocument rpt)
        {
            viewer.ShowReport(null, rpt);
        }


        public static void PrintToPrinter(ReportDocument report, string printerName, int copy = 1, string paperSourceName = null)
        {
            // If change Printer Name after startup windows,
            // Sometime not found PrinterName.
            report.PrintOptions.PrinterName = printerName;

            PrinterSettings printerSettings = new PrinterSettings();
            PageSettings pageSettings = new PageSettings();

            // Copy PrinterSetting and PageSetting from Report out to variables
            // If report set NoPrinter, We will setting manual Printer Setup later.
            report.PrintOptions.CopyTo(printerSettings, pageSettings);

            printerSettings.PrinterName = printerName;
            pageSettings.PrinterSettings = printerSettings;

            printerSettings.Copies = (short)copy;
            printerSettings.Collate = false;
            printerSettings.FromPage = 0;
            printerSettings.ToPage = 9999;

            if (paperSourceName != null)
            {
                foreach (System.Drawing.Printing.PaperSource ps in printerSettings.PaperSources)
                {
                    if (ps.SourceName == paperSourceName)
                    {
                        printerSettings.DefaultPageSettings.PaperSource = ps;
                        break;
                    }
                }
            }

            report.PrintToPrinter(printerSettings, pageSettings, false);
        }
    }
}
