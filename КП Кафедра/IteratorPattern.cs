using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace КП_Кафедра.IteratorPattern
{
    public interface IReportIterator
    {
        bool HasNext();
        string Next();

        bool HasPrevious();
        string Previous();

        string Current();
    }

    public class ReportCollection
    {
        public List<string> Reports { get; }

        public ReportCollection(string folderPath)
        {
            if (Directory.Exists(folderPath)) Reports = Directory.GetFiles(folderPath, "*.frx").ToList();
            else Reports = new List<string>();
        }

        public IReportIterator CreateIterator() { return new ReportIterator(Reports); }
    }

    public class ReportIterator : IReportIterator
    {
        private readonly List<string> reports;
        private int index = 0;

        public ReportIterator(List<string> reports) { this.reports = reports; }

        public bool HasNext() => index < reports.Count - 1;
        public bool HasPrevious() => index > 0;

        public string Next()
        {
            if (HasNext()) index++;
            return reports[index];
        }

        public string Previous()
        {
            if (HasPrevious()) index--;
            return reports[index];
        }

        public string Current()
        {
            if (index < 0 || index >= reports.Count) return null;
            return reports[index];
        }
    }
}