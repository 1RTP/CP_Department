using FastReport;

using System.Data;
using System.IO;
using System.Reflection;

using WASMUserApp.Models;

namespace WASMUserApp.Data;

public interface IReportService
{

    Report GetReport(string name);

    Task<Report> GetReportAsync(string name);

    Stream GetLocale(string? name = null);

    string[] ShowAvailable();

}

internal sealed class ReportService : IReportService
{
    private const string REPORTS_EXTENSION = ".frx";
    private const string path = nameof(WASMUserApp) + ".Reports.";
    private static readonly Assembly _assembly = typeof(ReportService).Assembly;
    private readonly DataSet _dataSet;
    private readonly List<Category> businessObjectsList;

    public ReportService()
    {
        using var stream = ResourceHelper.GetResource("nwind_ru.xml");
        _dataSet = new DataSet();
        _dataSet.ReadXml(stream);

        businessObjectsList = new List<Category>();

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
    }

    public Report GetReport(string name)
    {
        using var reportStream = ResourceHelper.GetResource(name + REPORTS_EXTENSION);
        var report = Report.FromStream(reportStream);
        RegisterData(report);
        return report;
    }

    public async Task<Report> GetReportAsync(string name)
    {
        using var reportStream = await ResourceHelper.GetResourceAsync(name + REPORTS_EXTENSION);
        var report = Report.FromStream(reportStream);
        RegisterData(report);
        return report;
    }

    private void RegisterData(Report report)
    {
        report.RegisterData(_dataSet, LocalizationConfig.ReportDatabase);
        report.RegisterData(businessObjectsList, "Categories BusinessObject");
    }

    public Stream GetLocale(string? name = null)
    {
        return ResourceHelper.GetLocale(name ?? LocalizationConfig.DefaultLocalization);
    }

    public string[] ShowAvailable()
    {
        var reports = _assembly.GetManifestResourceNames()
            .Select(resource => resource.Substring(path.Length))
            .Where(file => file.EndsWith(REPORTS_EXTENSION))
            .Select(file => Path.GetFileNameWithoutExtension(file))
            .ToArray();
        return reports;
    }
}
