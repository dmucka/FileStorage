using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Users
{
    [RequireAdmin]
    public class CreateModel : PageModel
    {
        private readonly UserFacade _userFacade;
        
        public CreateModel(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public new User User { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userFacade.RegisterUserAsync(new FileStorageBL.DTOs.UserCreateDto { Email = User.Email, Password = User.Password, Username = User.Username });

            return RedirectToPage("./Index");
        }
    }
}
