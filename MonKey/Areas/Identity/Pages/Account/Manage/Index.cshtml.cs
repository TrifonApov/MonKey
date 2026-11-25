namespace MonKey.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<Data.Models.ApplicationUser> userManager;

        public IndexModel(UserManager<Data.Models.ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        [TempData]
        public string? StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user is null)
            {
                return this.NotFound("Unable to load your account.");
            }

            this.Input = new InputModel
            {
                Email = user.Email ?? string.Empty,
                Birthdate = user.Birthdate,
            };

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user is null)
            {
                return this.NotFound("Unable to load your account.");
            }

            this.Input.Email = user.Email ?? string.Empty;
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            if (this.Input.Birthdate!.Value > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                this.ModelState.AddModelError("Input.Birthdate", "Date of birth cannot be in the future.");
                return this.Page();
            }

            user.Birthdate = this.Input.Birthdate.Value;
            var result = await this.userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.Page();
            }

            this.StatusMessage = "Your profile has been updated.";
            return this.RedirectToPage();
        }

        public class InputModel
        {
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of birth")]
            public DateOnly? Birthdate { get; set; }
        }
    }
}
