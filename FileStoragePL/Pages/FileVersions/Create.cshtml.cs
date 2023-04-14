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

namespace FileStoragePL.Pages.FileVersions
{
    [RequireAdmin]
    public class CreateModel : PageModel
    {
        private readonly FileVersionFacade _fileVersionFacade;
        private readonly VersionedFileFacade _versionedFileFacade;

        public CreateModel(FileVersionFacade fileVersionFacade, VersionedFileFacade versionedFileFacade)
        {
            _fileVersionFacade = fileVersionFacade;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["VersionedFileId"] = new SelectList(await _versionedFileFacade.GetAllVersionedFilesAsync(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public FileVersion FileVersion { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _fileVersionFacade.CreateFileVersionAsync(new FileStorageBL.DTOs.FileVersionDto { Id = FileVersion.Id, Changelog = FileVersion.Changelog, Number = FileVersion.Number, VersionedFileId = FileVersion.VersionedFileId });

            return RedirectToPage("./Index");
        }
    }
}
