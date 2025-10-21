using FastReport.Blazor.Demo.Data;
using FastReport.Blazor.Demo.Models;
using FastReport.Web;
using Microsoft.AspNetCore.Components;

namespace FastReport.Blazor.Demo.Pages
{
    public partial class Index
    {
        public WebReport UserWebReport { get; set; }

        [Inject]
        protected IReportService ReportService { get; set; }


        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            var report = ReportService.GetReport(ReportName);

            UserWebReport = new WebReport
            {
                Report = report,
                Toolbar = toolbarSettings,
                LocalizationFile = LocalizationConfig.WebReportLocalization
            };
        }


        private static readonly ToolbarSettings toolbarSettings = new()
        {
            Exports = new ExportMenuSettings
            {
                ExportTypes = Exports.Default,
                EnableSettings = true,
                PinnedSettingsPosition = true
            }
        };
    }
}
