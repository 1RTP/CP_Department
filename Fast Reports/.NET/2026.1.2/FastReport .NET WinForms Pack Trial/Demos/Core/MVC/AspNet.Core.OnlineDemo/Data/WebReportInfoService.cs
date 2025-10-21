using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;

namespace AspNet.Demo.Data;

public interface IWebReportInfoService
{
    string GetLocalizationFile();

    string GetDesignerLocale();
}

internal sealed class WebReportInfoService : IWebReportInfoService
{
    private static readonly ConcurrentDictionary<string, WebReportInfoCacheItem> _webReportInfoCache = new();

    private readonly IStringLocalizer<ReportService> _localizer;


    public string GetLocalizationFile()
    {
        var webReportInfoCacheItem = GetInfo();
        return webReportInfoCacheItem.LocalizationFile;
    }

    public string GetDesignerLocale()
    {
        var webReportInfoCacheItem = GetInfo();
        return webReportInfoCacheItem.DesignerLocale;
    }

    private WebReportInfoCacheItem GetInfo()
    {
        return _webReportInfoCache.GetOrAdd(_localizer["SubFolder"],
            folderName => WebReportInfoCacheItem.Create(folderName, _localizer["LocalizationFile"], _localizer["DesignerLocale"]));
    }

    public WebReportInfoService(IStringLocalizer<ReportService> localizer)
    {
        _localizer = localizer;
    }


    private sealed class WebReportInfoCacheItem
    {
        public required string LocalizationFile { get; init; }

        public required string DesignerLocale { get; init; }


        public static WebReportInfoCacheItem Create(string baseDirectory, string localizationFile, string designerLocale)
        {
            var localizationFilePath = "";

            // en localization doen't have localizationFile
            if (!string.IsNullOrEmpty(localizationFile))
                localizationFilePath = Path.Combine(Environment.CurrentDirectory,
                    "Reports", baseDirectory, localizationFile);

            return new WebReportInfoCacheItem()
            {
                LocalizationFile = localizationFilePath,
                DesignerLocale = designerLocale,
            };
        }
    }
}