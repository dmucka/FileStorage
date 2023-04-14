using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FileStoragePL.Pages.Folders
{
    [RequireAdmin]
    public class CreateModel : PageModel
    {
        private readonly FolderFacade _folderFacade;
        private readonly UserFacade _userFacade;

        [BindProperty]
        public int OwnerId { get; set; }
        public List<SelectListItem> AvailableOwners = new List<SelectListItem>(); // for dropdown menu

        [BindProperty]
        public Folder Folder { get; set; }

        public CreateModel(FolderFacade folderFacade, UserFacade userFacade)
        {
            _folderFacade = folderFacade;
            _userFacade = userFacade;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (this.IsUserAdmin())
            {
                List<User> users = await _userFacade.GetAllUsersAsync();
                foreach (var user in users)
                {
                    AvailableOwners.Add(new SelectListItem(user.Username, user.Id.ToString()));
                }
            }
            else
            {
                var currentUser = await _userFacade.GetUserByNameAsync(User.Identity.Name); 
                AvailableOwners.Add(new SelectListItem(currentUser.Username, currentUser.Id.ToString()));
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Folder.Owner = await _userFacade.GetUserByIdAsync(OwnerId);
            await _folderFacade.CreateFolderAsync(new FileStorageBL.DTOs.FolderCreateDto { Name = Folder.Name, Owner = Folder.Owner });

            return RedirectToPage("/Index");
        }
    }
}
