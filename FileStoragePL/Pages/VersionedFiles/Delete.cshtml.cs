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

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireAdmin]
    public class DeleteModel : PageModel
    {
        private readonly VersionedFileFacade _versionedFilesFacade;

        public DeleteModel(VersionedFileFacade versionedFilesFacade)
        {
            _versionedFilesFacade = versionedFilesFacade;
        }

        [BindProperty]
        public VersionedFile VersionedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VersionedFile = await _versionedFilesFacade.GetVersionedFileByIdAsync(id.Value);

            if (VersionedFile == null)
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

            VersionedFile = await _versionedFilesFacade.GetVersionedFileByIdAsync(id.Value);

            if (VersionedFile != null)
            {
                await _versionedFilesFacade.DeleteVersionedFileAsync(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
