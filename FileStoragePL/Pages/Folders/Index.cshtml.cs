using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Folders
{
    [RequireAdmin]
    public class IndexModel : PageModel
    {
        private readonly FolderFacade _folderFacade;

        private readonly UserFacade _userFacade;

        public IndexModel(UserFacade userFacade, FolderFacade folderFacade)
        {
            _userFacade = userFacade;
            _folderFacade = folderFacade;
        }

        public User UserModel { get; set; }
        public List<Folder> FolderModel { get;set; }

        public async Task OnGetAsync()
        {
            UserModel = await _userFacade.GetUserByNameAsync(User.Identity.Name);
            FolderModel = await _folderFacade.GetAllFoldersAsync();
        }
    }
}
