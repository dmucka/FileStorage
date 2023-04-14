using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Folders
{
    [RequireAdmin]
    public class DetailsModel : PageModel
    {
        private readonly FolderFacade _folderFacade;

        public DetailsModel(FolderFacade folderFacade)
        {
            _folderFacade = folderFacade;
        }

        public new Folder Folder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Folder = await _folderFacade.GetFolderByIdAsync(id.Value);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
