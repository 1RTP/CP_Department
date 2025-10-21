using System.Collections.Generic;

namespace MvcRazor.Models
{
    public sealed class FolderStruct
    {
        /// <summary>
        /// List of reports in folder
        /// </summary>
        public List<ReportStruct> Reports { get; } = new List<ReportStruct>();

        public bool Hidden { get; set; } = true;

        /// <summary>
        /// Name of the folder
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Description of the folder
        /// </summary>
        public string Description { get; set; }
    }
}
