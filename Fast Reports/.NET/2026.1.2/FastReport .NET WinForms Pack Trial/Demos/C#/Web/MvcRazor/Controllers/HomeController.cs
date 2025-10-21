using FastReport.Web;
using MvcRazor.Data;
using MvcRazor.Models;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MvcRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly static ReportFileProvider reportFileProvider = new ReportFileProvider();
        private readonly static ReportService reportService = new ReportService(reportFileProvider);

        public ActionResult Index(string reportName)
        { 
            if (string.IsNullOrEmpty(reportName))
                reportName = LocalizationConfig.DefaultReport;

            var webReport = new WebReport();
            SetReport(reportName, webReport);
            ViewBag.WebReport = webReport;
            ViewBag.Folders = reportFileProvider.Folders;
            return View();
        }

        public ActionResult LoadReport(string reportName)
        {
            if (string.IsNullOrEmpty(reportName))
                return new HttpNotFoundResult("Report name cannot be null or empty");

            var webReport = new WebReport();
            SetReport(reportName, webReport);
            return PartialView("_ReportView", webReport);
        }

        private void SetReport(string reportName, WebReport webReport)
        {
            webReport.Width = Unit.Percentage(100);
            webReport.Height = Unit.Percentage(100);
            webReport.ToolbarIconsStyle = ToolbarIconsStyle.Black;
            webReport.Report = reportService.GetReport(reportName);
            webReport.UseNewInterface = true;
            webReport.ShowOutline = true;
        }
    }
}
