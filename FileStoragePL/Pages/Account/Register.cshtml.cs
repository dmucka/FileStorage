using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FileStorageBL.DTOs;
using FileStorageBL.Facades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileStoragePL.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserFacade _userFacade;

        public RegisterModel(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [StringLength(256, ErrorMessage = "The {0} must be at max {1} characters long.")]
            public string Email { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                await _userFacade.RegisterUserAsync(new UserCreateDto { Username = Input.Username, Email = Input.Email, Password = Input.Password });

                return Redirect(returnUrl);
            }

            return Page();
        }
    }
}
