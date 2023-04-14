using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Users
{
    [RequireAdmin]
    public class IndexModel : PageModel
    {
        private readonly UserFacade _userFacade;

        public IndexModel(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public new IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _userFacade.GetAllUsersAsync();
        }
    }
}
