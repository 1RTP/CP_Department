using FastReport.Blazor.Demo.Models;
using System.Collections.Concurrent;
using System.Data;

namespace FastReport.Blazor.Demo.Data
{
    public interface IReportService
    {
        Report GetReport(string reportName);
    }

    internal sealed class ReportService : IReportService
    {
        private readonly string directory;
        private readonly IReportFileProvider _reportListProvider;
        private static readonly ConcurrentDictionary<string, ReportDataCacheItem> _reportDataCache = new();

        readonly string DEFAULT_REPORT = LocalizationConfig.DefaultReport;


        public Report GetReport(string reportName)
        {
            var report = Report.FromFile(
                _reportListProvider.GetReportFilePath(
                    string.IsNullOrEmpty(reportName) ? DEFAULT_REPORT : reportName));

            // Registers the application dataset
            var reportDataCacheItem = _reportDataCache.GetOrAdd(LocalizationConfig.SubFolder,
                _ => ReportDataCacheItem.Create(directory));

            report.RegisterData(reportDataCacheItem.DataSet, LocalizationConfig.ReportDatabase);
            report.RegisterData(reportDataCacheItem.BusinessObjects, "Categories BusinessObject");

            return report;
        }

        public ReportService(IReportFileProvider reportListProvider)
        {

            directory = reportListProvider.ReportsFolder;
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
                dataSet.ReadXml(Path.Combine(directory, "nwind_ru.xml"));

                return new ReportDataCacheItem()
                {
                    BusinessObjects = businessObjectsList.ToArray(),
                    DataSet = dataSet,
                };
            }
        }
    }
}
