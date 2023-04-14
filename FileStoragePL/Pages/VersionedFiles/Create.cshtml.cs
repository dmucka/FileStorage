using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FileStorageDAL;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireAdmin]
    public class CreateModel : PageModel
    {
        private readonly VersionedFileFacade _versionedFilesFacade;

        public CreateModel(VersionedFileFacade versionedFilesFacade)
        {
            _versionedFilesFacade = versionedFilesFacade;
        }

        public IActionResult OnGet()
        {
            ViewData["NewestVersionId"] = new SelectList(new List<FileVersion>(), "Id", "Number");
            return Page();
        }

        [BindProperty]
        public VersionedFile VersionedFile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _versionedFilesFacade.CreateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileCreateDto { Name = VersionedFile.Name });

            return RedirectToPage("./Index");
        }
    }
}
