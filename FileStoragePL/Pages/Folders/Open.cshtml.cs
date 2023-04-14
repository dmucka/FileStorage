using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.Folders
{
    [RequireBasic]
    public class OpenModel : PageModel
    {
        private readonly VersionedFileFacade _versionedFileFacade;
        private readonly FolderFacade _folderFacade;

        public OpenModel(FolderFacade folderFacade, VersionedFileFacade versionedFileFacade)
        {
            _folderFacade = folderFacade;
            _versionedFileFacade = versionedFileFacade;
        }

        public List<VersionedFile> VersionedFilesModel { get; set; }
        public Folder FolderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            FolderModel = await _folderFacade.GetFolderByIdAsync(id);

            if (FolderModel == null)
                return NotFound();

            return Page();
        }
    }
}
