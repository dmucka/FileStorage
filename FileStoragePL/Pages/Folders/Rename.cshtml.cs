using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.Folders
{
    [RequireBasic]
    public class RenameModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FolderFacade _folderFacade;

        public RenameModel(UserFacade userFacade, FolderFacade folderFacade)
        {
            _userFacade = userFacade;
            _folderFacade = folderFacade;
        }

        public Folder FolderModel { get; set; }

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
            FolderModel = await _folderFacade.GetFolderByIdAsync(id);

            if (FolderModel == null)
                return NotFound();

            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            // check that folder owner is correct
            if (owner.Id != FolderModel.Owner.Id)
                return Unauthorized();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var folder = await _folderFacade.GetFolderByIdAsync(id);

            if (folder == null)
                return NotFound();

            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            // check that folder owner is correct
            if (owner.Id != folder.Owner.Id)
                return Unauthorized();

            await _folderFacade.UpdateFolderAsync(new FileStorageBL.DTOs.FolderUpdateDto { Id = id, Name = Input.Name, Owner = owner, FolderId = folder.FolderId });

            return RedirectToPage("/Folders/Open", new { id });
        }
    }
}
