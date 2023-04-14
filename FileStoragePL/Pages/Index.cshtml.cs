using FileStorageBL.Facades;
using FileStorageDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStoragePL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserFacade _userFacade;
        private readonly FolderFacade _folderFacade;

        public IndexModel(UserFacade userFacade, FolderFacade folderFacade)
        {
            _userFacade = userFacade;
            _folderFacade = folderFacade;
        }

        public User UserModel { get; set; }
        public List<Folder> FolderModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (this.IsUserLogged())
            {
                UserModel = await _userFacade.GetUserByNameAsync(User.Identity.Name);

                // invalid cookie
                if (UserModel == null)
                {
                    return RedirectToPage("/Account/Logout");
                }

                FolderModel = await _folderFacade.GetAllRootFoldersByUserId(UserModel.Id);
            }

            return Page();
        }
    }
}
