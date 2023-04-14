using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FileStoragePL.Pages.FileVersions
{
    [RequireBasic]
    public class RemoveModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FileVersionFacade _fileVersionFacade;

        public RemoveModel(UserFacade userFacade, FileVersionFacade fileVersionFacade)
        {
            _userFacade = userFacade;
            _fileVersionFacade = fileVersionFacade;
        }

        public User UserModel { get; set; }

        public FileVersion FileVersionModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            FileVersionModel = await _fileVersionFacade.GetFileVersionByIdAsync(id);

            if (FileVersionModel == null)
                return NotFound();

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var folderOwner = await _fileVersionFacade.GetOwner(FileVersionModel.Id);
            if (folderOwner.Id != owner.Id)
                return Unauthorized();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var fileVersion = await _fileVersionFacade.GetFileVersionByIdAsync(id);

            if (fileVersion == null)
                return NotFound();

            var returnVersionedFileId = fileVersion.VersionedFileId;

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var folderOwner = await _fileVersionFacade.GetOwner(fileVersion.Id);
            if (folderOwner.Id != owner.Id)
                return Unauthorized();

            if (fileVersion.VersionedFile.FileVersions.Count > 1)
                await _fileVersionFacade.DeleteFileVersionAsync(id);

            var updated = await UpdateFileVersion(fileVersion.VersionedFile);
            if (!updated)
                return NotFound();

            return RedirectToPage("/VersionedFiles/Open", new { id = returnVersionedFileId });
        }

        private async Task<bool> UpdateFileVersion(VersionedFile versionedFile)
        {
            //lastVersionedFile
            var fileVersion = versionedFile.FileVersions.OrderByDescending(x => x.Id).FirstOrDefault();
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
