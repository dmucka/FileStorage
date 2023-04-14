using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileStoragePL.Pages.FileVersions
{
    [RequireBasic]
    public class AddModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FileVersionFacade _fileVersionFacade;
        private readonly VersionedFileFacade _versionedFileFacade;

        public AddModel(UserFacade userFacade, FileVersionFacade fileVersionFacade, VersionedFileFacade versionedFileFacade)
        {
            _userFacade = userFacade;
            _fileVersionFacade = fileVersionFacade;
            _versionedFileFacade = versionedFileFacade;
        }

        public VersionedFile VersionedFileModel { get; set; }
        public FileVersion FileVersionModel { get; set; }
        public User UserModel { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Number { get; set; }

            [StringLength(1024, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Changelog { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            VersionedFileModel = await _versionedFileFacade.GetVersionedFileByIdAsync(id);

            if (VersionedFileModel == null)
                return NotFound();

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var folderOwner = await _versionedFileFacade.GetOwner(id);
            if (folderOwner.Id != owner.Id)
                return Unauthorized();

            return Page();
        }

        

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var versionedFile = await _versionedFileFacade.GetVersionedFileByIdAsync(id);
            if (versionedFile == null)
                return NotFound();

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var folderOwner = await _versionedFileFacade.GetOwner(id);
            if (folderOwner.Id != owner.Id)
                return Unauthorized();

            var updated = await UpdateFileVersion(versionedFile, id);
            if (!updated)
                return NotFound();

            return RedirectToPage("/VersionedFiles/Open", new { id });
        }

        private async Task<bool> UpdateFileVersion(VersionedFile versionedFile, int id)
        {
            FileVersion fileVersion = await _fileVersionFacade.CreateFileVersionAsync(new FileStorageBL.DTOs.FileVersionDto { Number = Input.Number, Changelog = Input.Changelog, VersionedFileId = id });
            versionedFile.NewestVersion = fileVersion;
            versionedFile.NewestVersionId = fileVersion.Id;
            versionedFile.FileVersions.Add(fileVersion);

            try
            {
                await _fileVersionFacade.UpdateFileVersionAsync(new FileStorageBL.DTOs.FileVersionDto { Id = fileVersion.Id, Changelog = fileVersion.Changelog, Number = fileVersion.Number, VersionedFileId = fileVersion.VersionedFileId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await FileVersionExists(fileVersion.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private async Task<bool> FileVersionExists(int id)
        {
            return await _fileVersionFacade.GetFileVersionByIdAsync(id) != null;
        }
    }
}
