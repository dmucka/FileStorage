using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireBasic]
    public class OpenModel : PageModel
    {
        private readonly VersionedFileFacade _versionedFileFacade;

        public OpenModel(VersionedFileFacade versionedFileFacade)
        {
            _versionedFileFacade = versionedFileFacade;
        }

        public VersionedFile VersionedFileModel { get; set; }
        public Folder FolderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            VersionedFileModel = await _versionedFileFacade.GetVersionedFileByIdAsync(id);

            if (VersionedFileModel == null)
                return NotFound();

            return Page();
        }
    }
}
