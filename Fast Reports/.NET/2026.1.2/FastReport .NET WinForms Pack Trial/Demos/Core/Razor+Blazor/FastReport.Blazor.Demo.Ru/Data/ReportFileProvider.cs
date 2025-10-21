using FastReport.Blazor.Demo.Models;
using FastReport.Utils;

namespace FastReport.Blazor.Demo.Data
{
    public interface IReportFileProvider
    {
        IReadOnlyList<FolderStruct> Folders { get; }

        string ReportsFolder { get; }

        string GetReportFilePath(string reportName);
    }

    public sealed class ReportFileProvider: IReportFileProvider
    {
        private const string REPORTS_FOLDER_NAME = "Reports.New.Ru";
        private readonly string reportsFolder;
        private static string CurrentDirectory => Directory.GetCurrentDirectory();
        public IReadOnlyList<FolderStruct> Folders { get; }

        public string ReportsFolder => reportsFolder;
        public ReportFileProvider()
        {
            reportsFolder = FindReportsFolder(CurrentDirectory);

            Folders = GetFolderList();
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

        private List<FolderStruct> GetFolderList()
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
