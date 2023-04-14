using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.VersionedFiles
{
    [RequireBasic]
    public class RenameModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly VersionedFileFacade _versionedFileFacade;

        public RenameModel(UserFacade userFacade, VersionedFileFacade versionedFileFacade)
        {
            _userFacade = userFacade;
            _versionedFileFacade = versionedFileFacade;
        }

        public VersionedFile VersionedFileModel { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Name { get; set; }
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

            await _versionedFileFacade.UpdateVersionedFileAsync(new FileStorageBL.DTOs.VersionedFileUpdateDto { Id = id, Name = Input.Name, NewestVersionId = versionedFile.NewestVersionId });

            return RedirectToPage("/VersionedFiles/Open", new { id });
        }
    }
}
