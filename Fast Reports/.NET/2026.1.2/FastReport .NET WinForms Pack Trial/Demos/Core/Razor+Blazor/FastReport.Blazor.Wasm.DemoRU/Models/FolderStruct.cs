
namespace WASMUserApp.Models;

public sealed class FolderStruct
{
    /// <summary>
    /// List of reports in folder
    /// </summary>
    public List<ReportStruct> Reports { get; } = new List<ReportStruct>();

    public bool Hiden { get; set; } = true;

    /// <summary>
    /// Name of the folder
    /// </summary>
    public string FolderName { get; init; }

    /// <summary>
    /// Description of the folder
    /// </summary>
    public string Description { get; init; }


    public FolderStruct()
    {
    }

}
