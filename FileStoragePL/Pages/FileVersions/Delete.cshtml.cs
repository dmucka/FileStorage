using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.FileVersions
{
    [RequireAdmin]
    public class DeleteModel : PageModel
    {
        private readonly FileVersionFacade _fileVersionFacade;

        public DeleteModel(FileVersionFacade fileVersionFacade)
        {
            _fileVersionFacade = fileVersionFacade;
        }

        [BindProperty]
        public FileVersion FileVersion { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FileVersion = await _fileVersionFacade.GetFileVersionByIdAsync(id.Value);

            if (FileVersion == null)
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

            FileVersion = await _fileVersionFacade.GetFileVersionByIdAsync(id.Value);

            if (FileVersion != null)
            {
                await _fileVersionFacade.DeleteFileVersionAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
