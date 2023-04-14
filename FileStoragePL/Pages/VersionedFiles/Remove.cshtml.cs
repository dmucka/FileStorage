using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireBasic]
    public class RemoveModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly VersionedFileFacade _versionedFileFacade;

        public RemoveModel(UserFacade userFacade, VersionedFileFacade versionedFileFacade)
        {
            _userFacade = userFacade;
            _versionedFileFacade = versionedFileFacade;
        }

        public User UserModel { get; set; }
        public VersionedFile VersionedFileModel { get; set; }

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
            var versionedFile = await _versionedFileFacade.GetVersionedFileByIdAsync(id);

            if (versionedFile == null)
                return NotFound();

            var returnFolder = versionedFile.FolderId;

            // check that folder owner is correct
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            var folderOwner = await _versionedFileFacade.GetOwner(id);
            if (folderOwner.Id != owner.Id)
                return Unauthorized();

            await _versionedFileFacade.UpdateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileUpdateDto { Id = versionedFile.Id, Name = versionedFile.Name, NewestVersionId = null });
            await _versionedFileFacade.DeleteVersionedFileAsync(id);

            return RedirectToPage("/Folders/Open", new { id = returnFolder });
        }
    }
}
