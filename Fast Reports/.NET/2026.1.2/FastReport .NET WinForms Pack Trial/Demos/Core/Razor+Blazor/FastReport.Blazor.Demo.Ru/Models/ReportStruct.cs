namespace FastReport.Blazor.Demo.Models
{
    public sealed class ReportStruct
    {
        /// <summary>
        /// Name of the report file with extension
        /// </summary>
        public string FilePath { get; init; }

        /// <summary>
        /// Name of the report without extension
        /// </summary>
        /// 
        public string FileName { get; init; }
    }
}
