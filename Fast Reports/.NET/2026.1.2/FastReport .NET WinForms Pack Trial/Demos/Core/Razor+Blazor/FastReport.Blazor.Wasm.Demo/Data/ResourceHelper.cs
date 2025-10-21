using System.IO;
using System.Reflection;

namespace WASMUserApp.Data
{
    internal static class ResourceHelper
    {
        private const string path = nameof(WASMUserApp) + ".Reports.";
        private static readonly Assembly _assembly = typeof(ReportService).Assembly;
        internal static MemoryStream GetReportResource(string name)
        {
            using Stream? stream = GetReportResourceFromAssembly(name);

            MemoryStream ms = new();
            stream.CopyTo(ms);
            ms.Position = 0;
            return ms;
        }

        internal static async Task<MemoryStream> GetReportResourceAsync(string name)
        {
            using Stream? stream = GetReportResourceFromAssembly(name);

            MemoryStream ms = new();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            return ms;
        }

        private static Stream GetReportResourceFromAssembly(string name)
        {

            Stream? stream = _assembly.GetManifestResourceStream(path + name);
            if (stream == null)
                throw new Exception("Report wasn't found");
            return stream;
        }

        public static bool Exists(string name)
        {
            return _assembly.GetManifestResourceNames().Contains(path + name);
        }

    }
}
