using AspNet.Demo.Data;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Demo.Controllers;

[Route("")]
public sealed class HomeController(IReportService reportService,
    IWebReportInfoService webReportInfoService) : Controller
{
    [HttpGet("{reportName:regex(^.*\\.frx$)?}")]
    public IActionResult Index(string? reportName)
    {
        var webReport = new WebReport
        {
            Report = reportService.GetReport(reportName),
            Toolbar = _toolbarSettings,
            LocalizationFile = webReportInfoService.GetLocalizationFile(),
        };

        return View(webReport);
    }


    [HttpGet("LoadReport")]
    public IActionResult LoadReport(string reportName)
    {
        if (string.IsNullOrEmpty(reportName))
            return BadRequest("Report name cannot be null or empty");

        var webReport = new WebReport
        {
            Report = reportService.GetReport(reportName),
            Toolbar = _toolbarSettings,
            LocalizationFile = webReportInfoService.GetLocalizationFile(),
        };

        return PartialView("_ReportView", webReport);
    }


    private static readonly ToolbarSettings _toolbarSettings = new()
    {
        Exports = new ExportMenuSettings
        {
            ExportTypes = Exports.Default,
            EnableSettings = true,
            PinnedSettingsPosition = true
        }
    };
}