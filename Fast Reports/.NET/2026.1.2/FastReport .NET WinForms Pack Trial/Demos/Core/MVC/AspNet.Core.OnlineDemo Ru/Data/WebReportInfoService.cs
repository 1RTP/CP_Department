using AspNet.Demo.Models;
using System.Collections.Concurrent;

namespace AspNet.Demo.Data;

public interface IWebReportInfoService
{
    string GetLocalizationFile();

    string GetDesignerLocale();
}

internal sealed class WebReportInfoService : IWebReportInfoService
{
    private readonly ConcurrentDictionary<string, WebReportInfoCacheItem> _webReportInfoCache = new();


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
        return _webReportInfoCache.GetOrAdd(LocalizationConfig.SubFolder,
            folderName => WebReportInfoCacheItem.Create(LocalizationConfig.WebReportLocalization, LocalizationConfig.DesignerLocale));
    }

    public WebReportInfoService()
    {
    }


    private sealed class WebReportInfoCacheItem
    {
        public required string LocalizationFile { get; init; }

        public required string DesignerLocale { get; init; }


        public static WebReportInfoCacheItem Create(string localizationFile, string designerLocale)
        {
            return new WebReportInfoCacheItem()
            {
                LocalizationFile = localizationFile,
                DesignerLocale = designerLocale,
            };
        }
    }
}