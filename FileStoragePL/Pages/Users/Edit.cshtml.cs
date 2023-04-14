using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FileStorageDAL.Models;
using FileStorageBL.Facades;

namespace FileStoragePL.Pages.Users
{
    [RequireAdmin]
    public class EditModel : PageModel
    {
        private readonly UserFacade _userFacade;

        public EditModel(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _userFacade.UpdateUserAsync(new FileStorageBL.DTOs.UserUpdateDto { Id = User.Id, Email = User.Email, Password = User.Password, Username = User.Username });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> UserExists(int id)
        {
            return await _userFacade.GetUserByIdAsync(id) != null;
        }
    }
}
