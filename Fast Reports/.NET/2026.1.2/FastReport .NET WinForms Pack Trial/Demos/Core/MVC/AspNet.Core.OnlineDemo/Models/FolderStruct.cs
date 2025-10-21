namespace AspNet.Demo.Models;

public sealed class FolderStruct
{
    /// <summary>
    /// List of reports in folder
    /// </summary>
    public List<ReportStruct> Reports { get; } = new();

    public bool Hidden { get; set; } = true;

    /// <summary>
    /// Name of the folder
    /// </summary>
    public required string FolderName { get; init; }

    /// <summary>
    /// Description of the folder
    /// </summary>
    public required string Description { get; init; }
}