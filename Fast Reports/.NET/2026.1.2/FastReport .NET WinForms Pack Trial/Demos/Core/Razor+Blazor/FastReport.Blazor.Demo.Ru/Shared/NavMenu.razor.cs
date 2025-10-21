using FastReport.Blazor.Demo.Data;
using FastReport.Blazor.Demo.Models;

namespace FastReport.Blazor.Demo.Shared
{
    public partial class NavMenu
    {
        private readonly IReadOnlyList<FolderStruct> Groups;

        public NavMenu()
        {
            Groups = new ReportFileProvider().Folders;
            Groups.FirstOrDefault().Hiden = false;


        }

        private void FolderClick(FolderStruct folder)
        {
            if (folder.Hiden)
                foreach (var folderOld in Groups)
                    folderOld.Hiden = true;

            folder.Hiden = !folder.Hiden;

        }

    }
}
