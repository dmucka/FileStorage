using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.Folders
{
    [RequireBasic]
    public class AddModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FolderFacade _folderFacade;

        public AddModel(UserFacade userFacade, FolderFacade folderFacade)
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

        public User UserModel { get; set; }
        public Folder RootFolder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                RootFolder = await _folderFacade.GetFolderByIdAsync(id.Value);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
                return Page();

            var owner = await _userFacade.GetUserByNameAsync(User.Identity.Name);

            // check that folder owner is correct
            if (id != null)
            {
                var ownerFolder = await _folderFacade.GetFolderByIdAsync(id.Value);
                if (ownerFolder != null && ownerFolder.Owner != owner)
                    return Unauthorized();
            }

            await _folderFacade.CreateFolderAsync(new FileStorageBL.DTOs.FolderCreateDto { Name = Input.Name, Owner = owner, FolderId = id });

            if (id == null)
            {
                return LocalRedirect("/");
            }
            else
            {
                return RedirectToPage("/Folders/Open", new { id });
            }
        }
    }
}
