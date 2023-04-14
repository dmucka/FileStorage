using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Users
{
    [RequireAdmin]
    public class DetailsModel : PageModel
    {
        private readonly UserFacade _userFacade;

        public DetailsModel(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _userFacade.GetUserByIdAsync(id.Value);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
