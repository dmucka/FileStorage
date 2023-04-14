using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Files
{
    [RequireAdmin]
    public class EditModel : PageModel
    {
        private readonly FileFacade _fileFacade;

        public EditModel(FileFacade fileFacade)
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

            if (File == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                await _fileFacade.UpdateFileAsync(new FileStorageBL.DTOs.FileDto
                { Id = File.Id, Link = File.Link, Size = File.Size, FileVersionId = File.FileVersionId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FileExists(File.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> FileExists(int id)
        {
            return await _fileFacade.GetFileByIdAsync(id) != null;
        }
    }
}
