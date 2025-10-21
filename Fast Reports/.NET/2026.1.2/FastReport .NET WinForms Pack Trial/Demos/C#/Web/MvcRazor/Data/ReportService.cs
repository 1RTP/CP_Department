using FastReport;
using MvcRazor.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MvcRazor.Data
{
    public sealed class ReportService
    {
        private static readonly ConcurrentDictionary<string, ReportDataCacheItem> _reportDataCache = new ConcurrentDictionary<string, ReportDataCacheItem>();

        private readonly string _directory;
        private readonly ReportFileProvider _reportListProvider;


        public Report GetReport(string reportName)
        {
            var report = Report.FromFile(
                _reportListProvider.GetReportFilePath(
                    string.IsNullOrEmpty(reportName) ? LocalizationConfig.DefaultReport : reportName));

            // Registers the application dataset
            var reportDataCacheItem = _reportDataCache.GetOrAdd(LocalizationConfig.SubFolder,
                _ => ReportDataCacheItem.Create(_directory));

            report.RegisterData(reportDataCacheItem.DataSet, LocalizationConfig.ReportDatabase);
            report.RegisterData(reportDataCacheItem.BusinessObjects, "Categories BusinessObject");

            return report;
        }


        public ReportService(ReportFileProvider reportListProvider)
        {
            _directory = reportListProvider.ReportsFolder;
            _reportListProvider = reportListProvider;
        }


        private sealed class ReportDataCacheItem
        {
            public Category[] BusinessObjects { get; set; }

            public DataSet DataSet { get; set; }


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
}
