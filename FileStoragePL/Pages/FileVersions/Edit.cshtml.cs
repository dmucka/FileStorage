using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.FileVersions
{
    [RequireAdmin]
    public class EditModel : PageModel
    {
        private readonly FileVersionFacade _fileVersionFacade;
        private readonly VersionedFileFacade _versionedFileFacade;

        public EditModel(FileVersionFacade fileVersionFacade, VersionedFileFacade versionedFileFacade)
        {
            _fileVersionFacade = fileVersionFacade;
            _versionedFileFacade = versionedFileFacade;
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
           ViewData["VersionedFileId"] = new SelectList(await _versionedFileFacade.GetAllVersionedFilesAsync(), "Id", "Name");
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
                await _fileVersionFacade.UpdateFileVersionAsync(new FileStorageBL.DTOs.FileVersionDto { Id = FileVersion.Id, Changelog = FileVersion.Changelog, Number = FileVersion.Number, VersionedFileId = FileVersion.VersionedFileId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FileVersionExists(FileVersion.Id))
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

        private async Task<bool> FileVersionExists(int id)
        {
            return await _fileVersionFacade.GetFileVersionByIdAsync(id) != null;
        }
    }
}
