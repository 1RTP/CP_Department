namespace AspNet.Demo.Models;

public sealed class ReportStruct
{
    /// <summary>
    /// Name of the report file with extension
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// Name of the report without extension
    /// </summary>
    /// 
    public required string FileName { get; init; }
}