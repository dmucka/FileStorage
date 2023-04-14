using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireBasic]
    public class AddModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FolderFacade _folderFacade;
        private readonly VersionedFileFacade _versionedFileFacade;
        private readonly FileVersionFacade _fileVersionFacade;

        public AddModel(UserFacade userFacade, FolderFacade folderFacade, VersionedFileFacade versionedFileFacade, FileVersionFacade fileVersionFacade)
        {
            _userFacade = userFacade;
            _folderFacade = folderFacade;
            _versionedFileFacade = versionedFileFacade;
            _fileVersionFacade = fileVersionFacade;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Name { get; set; }
        }

        public User UserModel { get; set; }
        public Folder FolderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            FolderModel = await _folderFacade.GetFolderByIdAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var folder = await _folderFacade.GetFolderByIdAsync(id);
            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            // check that folder owner is correct
            if (folder.Owner.Id != owner.Id)
                return Unauthorized();

            var newVersionedFile = await _versionedFileFacade.CreateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileCreateDto { Name = Input.Name, FolderId = folder.Id });
            var defaultVersion = await _fileVersionFacade.CreateFileVersionAsync(new FileStorageBL.DTOs.FileVersionDto { Number = "1.0.0", Changelog = "Default version", VersionedFileId = newVersionedFile.Id });

           await _versionedFileFacade.UpdateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileUpdateDto { Id = newVersionedFile.Id, Name = newVersionedFile.Name, NewestVersionId = defaultVersion.Id });

            return RedirectToPage("/Folders/Open", new { id });
        }
    }
}
