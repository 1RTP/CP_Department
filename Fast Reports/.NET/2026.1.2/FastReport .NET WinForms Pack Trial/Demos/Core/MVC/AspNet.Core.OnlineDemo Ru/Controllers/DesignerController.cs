using AspNet.Demo.Data;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Demo.Controllers;

[Route("designer")]
public sealed class DesignerController(IReportService reportService,
    IWebReportInfoService webReportInfoService) : Controller
{
    [HttpGet("{reportName?}")]
    public IActionResult Index(string? reportName)
    {
        var webReport = new WebReport
        {
            Report = reportService.GetReport(reportName),
            Toolbar = _toolbarSettings,
            LocalizationFile = webReportInfoService.GetLocalizationFile(),
            Mode = WebReportMode.Designer,
            Width = "100%",
            Height = "100%",
            Designer = new DesignerSettings()
            {
                Locale = webReportInfoService.GetDesignerLocale(),
                ScriptCode = false,
                Path = "/net-core/WebReportDesigner/index.html",
            },
            Debug = false,
        };

        return View(webReport);
    }


    private static readonly ToolbarSettings _toolbarSettings = new()
    {
        Exports = new ExportMenuSettings
        {
            ExportTypes = Exports.Default,
            EnableSettings = true,
            PinnedSettingsPosition = true,
        }
    };
}