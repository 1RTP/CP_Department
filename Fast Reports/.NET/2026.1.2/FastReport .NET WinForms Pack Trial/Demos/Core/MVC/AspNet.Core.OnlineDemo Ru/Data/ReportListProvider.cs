using AspNet.Demo.Models;
using FastReport.Utils;
using System.Collections.Concurrent;

namespace AspNet.Demo.Data;

public interface IReportFileProvider
{
    IReadOnlyList<FolderStruct> Folders { get; }

    string ReportsFolder { get; }

    string GetReportFilePath(string reportName);
}

internal sealed class ReportFileProvider : IReportFileProvider
{
    private const string REPORTS_FOLDER_NAME = "Reports.New.Ru";
    private static readonly ConcurrentDictionary<string, ReportListCacheItem> _reportListCache = new();
    private readonly ReportListCacheItem _reportListCacheItem;

    public ReportFileProvider()
    {
        _reportListCacheItem = _reportListCache.GetOrAdd(LocalizationConfig.SubFolder,
            folderName =>
            {
                var reportFolder = FindReportsFolder(CurrentDirectory);
                return ReportListCacheItem.Create(reportFolder);
            });
    }

    private static string FindReportsFolder(string currentDirectory)
    {
        for (int i = 0; i < 7; i++)
        {
            var reportFolder = Path.Combine(currentDirectory, REPORTS_FOLDER_NAME);
            if (Directory.Exists(reportFolder))
                return reportFolder;

            currentDirectory = Path.Combine(currentDirectory, "..");
        }
        throw new Exception("Reports folder wasn't found");
    }

    public string GetReportFilePath(string reportName)
    {
        reportName = Path.GetFileNameWithoutExtension(reportName);
        foreach (var folder in Folders)
        {
            var report = folder.Reports.FirstOrDefault(report => report.FileName == reportName);
            if (report != null)
                return Path.Combine(ReportsFolder, report.FilePath);
        }
        throw new Exception("Report wasn't found.");
    }
    public IReadOnlyList<FolderStruct> Folders => _reportListCacheItem.Folders;
    public string ReportsFolder => _reportListCacheItem.ReportsFolder;

    private static string CurrentDirectory => Directory.GetCurrentDirectory();
    private sealed class ReportListCacheItem
    {
        public required IReadOnlyList<FolderStruct> Folders { get; init; }

        public required string ReportsFolder { get; init; }

        public static ReportListCacheItem Create(string reportsFolder)
        {
            return new ReportListCacheItem()
            {
                Folders = GetFolderList(reportsFolder),
                ReportsFolder = reportsFolder,
            };
        }

        private static List<FolderStruct> GetFolderList(string reportsFolder)
        {
            var folders = new List<FolderStruct>();
            try
            {
                var reports = new XmlDocument();
                reports.Load(Path.Combine(reportsFolder, "reports.xml"));

                for (var i = 0; i < reports.Root.Count; i++)
                {
                    var folderItem = reports.Root[i];
                    if (folderItem.GetProp("WinDemo") == "false")
                        continue;

                    var folderName = folderItem.GetProp("Name");

                    var folder = new FolderStruct
                    {
                        FolderName = folderName,
                        Description = folderItem.GetProp("Description")
                    };

                    for (var j = 0; j < folderItem.Count; j++)
                    {
                        var reportItem = folderItem[j];
                        //if (reportItem.GetProp("WinDemo") == "false")
                        //    continue;

                        var file = reportItem.GetProp("File");

                        if (!File.Exists(Path.Combine(reportsFolder, folderName, file)))
                            continue;

                        var fileName = Path.GetFileNameWithoutExtension(file);

                        ReportStruct report = new()
                        {
                            FileName = fileName,
                            FilePath = Path.Combine(folderName, file),
                        };

                        folder.Reports.Add(report);
                    }

                    folders.Add(folder);
                }
            }
            catch
            {
                // ignored
            }

            return folders;
        }
    }
}