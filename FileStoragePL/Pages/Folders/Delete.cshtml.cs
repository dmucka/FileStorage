using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Folders
{
    [RequireAdmin]
    public class DeleteModel : PageModel
    {
        private readonly FolderFacade _folderFacade;

        public DeleteModel(FolderFacade folderFacade)
        {
            _folderFacade = folderFacade;
        }

        [BindProperty]
        public Folder Folder { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Folder = await _folderFacade.GetFolderByIdAsync(id.Value);

            if (Folder != null)
            {
                await _folderFacade.DeleteFolderAsync(Folder.Id);
            }

            return RedirectToPage("../Index");
        }
    }
}
