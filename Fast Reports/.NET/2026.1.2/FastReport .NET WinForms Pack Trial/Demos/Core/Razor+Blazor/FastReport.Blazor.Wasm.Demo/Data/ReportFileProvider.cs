using WASMUserApp.Models;
using FastReport.Utils;

namespace WASMUserApp.Data
{
    public interface IReportFileProvider
    {
        IReadOnlyList<FolderStruct> Folders { get; }

        string ReportsFolder { get; }

        string GetReportFilePath(string reportName);
    }

    public sealed class ReportFileProvider : IReportFileProvider
    {
        private readonly string reportsFolder;
        public IReadOnlyList<FolderStruct> Folders { get; }

        public string ReportsFolder => reportsFolder;
        public ReportFileProvider()
        {
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

        private List<FolderStruct> GetFolderList()
        {
            var folders = new List<FolderStruct>();
            try
            {
                var reports = new XmlDocument();
               
                reports.Load(ResourceHelper.GetReportResource("reports.xml"));
               
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
                        //if (reportItem.GetProp("WinDemo") == "false")
                        //    continue;

                        var file = folderItem[j].GetProp("File");
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        if (!ResourceHelper.Exists(file))
                            continue;

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
