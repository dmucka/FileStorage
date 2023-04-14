using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Files
{
    [RequireBasic]
    public class DeleteModel : PageModel
    {
        private readonly FileFacade _fileFacade;

        public DeleteModel(FileFacade fileFacade)
        {
            _fileFacade = fileFacade;
        }

        [BindProperty]
        public new File File { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            File = await _fileFacade.GetFileByIdAsync(id.Value);

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

            File = await _fileFacade.GetFileByIdAsync(id.Value);

            if (User != null)
            {
                await _fileFacade.DeleteFileAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
