using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Files
{
    [RequireAdmin]
    public class DetailsModel : PageModel
    {
        private readonly FileFacade _fileFacade;

        public DetailsModel(FileFacade fileFacade)
        {
            _fileFacade = fileFacade;
        }

        public new File File { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            File = await _fileFacade.GetFileByIdAsync(id.Value);

            if (File == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
