using AspNet.Demo.Models;
using FastReport;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Data;

namespace AspNet.Demo.Data;

public interface IReportService
{
    Report GetReport(string? reportName);
}

public sealed class ReportService : IReportService
{
    private static readonly ConcurrentDictionary<string, ReportDataCacheItem> _reportDataCache = new();

    private readonly string _directory;
    private readonly IStringLocalizer<ReportService> _localizer;
    private readonly IReportFileProvider _reportListProvider;


    public Report GetReport(string? reportName)
    {
        var report = Report.FromFile(
            _reportListProvider.GetReportFilePath(
                string.IsNullOrEmpty(reportName) ? _localizer["DefaultReport"] : reportName));

        // Registers the application dataset
        var reportDataCacheItem = _reportDataCache.GetOrAdd(_localizer["SubFolder"],
            _ => ReportDataCacheItem.Create(_directory));

        report.RegisterData(reportDataCacheItem.DataSet, _localizer["ReportDatabase"]);
        report.RegisterData(reportDataCacheItem.BusinessObjects, "Categories BusinessObject");

        return report;
    }


    public ReportService(IStringLocalizer<ReportService> localizer, IReportFileProvider reportListProvider)
    {
        _localizer = localizer;

        _directory = reportListProvider.ReportsFolder;
        _reportListProvider = reportListProvider;
    }


    private sealed class ReportDataCacheItem
    {
        public required Category[] BusinessObjects { get; init; }

        public required DataSet DataSet { get; init; }


        public static ReportDataCacheItem Create(string directory)
        {
            // BusinessObject
            var businessObjectsList = new List<Category>();

            var category = new Category("Beverages", "Soft drinks, coffees, teas, beers");
            category.Products.Add(new Product("Chai", 18m));
            category.Products.Add(new Product("Chang", 19m));
            category.Products.Add(new Product("Ipoh coffee", 46m));
            businessObjectsList.Add(category);

            category = new Category("Confections", "Desserts, candies, and sweet breads");
            category.Products.Add(new Product("Chocolade", 12.75m));
            category.Products.Add(new Product("Scottish Longbreads", 12.5m));
            category.Products.Add(new Product("Tarte au sucre", 49.3m));
            businessObjectsList.Add(category);

            category = new Category("Seafood", "Seaweed and fish");
            category.Products.Add(new Product("Boston Crab Meat", 18.4m));
            category.Products.Add(new Product("Red caviar", 15m));
            businessObjectsList.Add(category);

            var dataSet = new DataSet();
            dataSet.ReadXml(Path.Combine(directory, "..", "Reports", "nwind.xml"));

            return new ReportDataCacheItem()
            {
                BusinessObjects = businessObjectsList.ToArray(),
                DataSet = dataSet,
            };
        }
    }
}