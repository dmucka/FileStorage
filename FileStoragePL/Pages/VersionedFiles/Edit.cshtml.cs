using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireAdmin]
    public class EditModel : PageModel
    {
        private readonly VersionedFileFacade _versionedFilesFacade;

        public EditModel(VersionedFileFacade versionedFilesFacade)
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
            ViewData["NewestVersionId"] = new SelectList(VersionedFile.FileVersions, "Id", "Number");
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
                await _versionedFilesFacade.UpdateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileUpdateDto { Id = VersionedFile.Id, Name = VersionedFile.Name, NewestVersionId = VersionedFile.NewestVersionId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VersionedFileExists(VersionedFile.Id))
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

        private async Task<bool> VersionedFileExists(int id)
        {
            return await _versionedFilesFacade.GetVersionedFileByIdAsync(id) != null;
        }
    }
}
