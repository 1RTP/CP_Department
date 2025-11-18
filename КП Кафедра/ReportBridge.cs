using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerializerLib;
using System.Data;

namespace КП_Кафедра.ReportsBridge
{
    public interface IReportExporter
    {
        void Export(DataTable table, string name);
        DataTable Import();
        void Open();
    }

    public class ExcelExporter : IReportExporter
    {
        private readonly RepExcel excel = new RepExcel();

        public void Export(DataTable table, string name) => excel.ExportToExcel(table, name);
        public DataTable Import() => excel.ImportFromExcel();
        public void Open() => excel.OpenExcelFile();
    }

    public class WordExporter : IReportExporter
    {
        private readonly RepWord word = new RepWord();

        public void Export(DataTable table, string name) => word.ExportToWord(table, name);
        public DataTable Import() => word.ImportFromWord();
        public void Open() => word.OpenWordFile();
    }

    public abstract class AbstractReport
    {
        protected IReportExporter exporter;

        public string ReportName { get; set; }

        protected AbstractReport(IReportExporter exporter) { this.exporter = exporter; }
        public void SetExporter(IReportExporter exporter) { this.exporter = exporter; }

        public abstract DataTable LoadData();

        public void Export()
        {
            var table = LoadData();
            exporter.Export(table, ReportName);
        }

        public DataTable Import() => exporter.Import();
        public void Open() => exporter.Open();
    }

    public class SqlReport : AbstractReport
    {
        private readonly string query;
        private readonly string dbPath;

        public SqlReport(string query, string dbPath, IReportExporter exporter) : base(exporter)
        {
            this.query = query;
            this.dbPath = dbPath;
        }

        public override DataTable LoadData()
        {
            try
            {
                using (var con = new Microsoft.Data.Sqlite.SqliteConnection($"Data Source={dbPath}"))
                {
                    con.Open();
                    using (var cmd = new Microsoft.Data.Sqlite.SqliteCommand(query, con))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"SQL помилка: {ex.Message}");
                return new DataTable();
            }
        }

        
    }
}