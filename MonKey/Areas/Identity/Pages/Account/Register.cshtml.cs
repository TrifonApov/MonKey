namespace MonKey.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class RegisterModel : PageModel
    {
        private readonly UserManager<Data.Models.ApplicationUser> userManager;
        private readonly SignInManager<Data.Models.ApplicationUser> signInManager;

        public RegisterModel(
            UserManager<Data.Models.ApplicationUser> userManager,
            SignInManager<Data.Models.ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public IActionResult OnGet()
        {
            if (this.signInManager.IsSignedIn(this.User))
            {
                return this.RedirectToLocal();
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            if (this.Input.Birthdate!.Value > DateOnly.FromDateTime(DateTime.UtcNow))
            {
                this.ModelState.AddModelError("Input.Birthdate", "Date of birth cannot be in the future.");
                return this.Page();
            }

            var user = new Data.Models.ApplicationUser
            {
                UserName = this.Input.Email,
                Email = this.Input.Email,
                Birthdate = this.Input.Birthdate.Value,
            };

            var result = await this.userManager.CreateAsync(user, this.Input.Password);
            if (result.Succeeded)
            {
                var signInOptions = this.signInManager.Options.SignIn;
                if (signInOptions.RequireConfirmedAccount
                    || signInOptions.RequireConfirmedEmail
                    || signInOptions.RequireConfirmedPhoneNumber)
                {
                    return this.RedirectToPage("./RegisterConfirmation", new { email = this.Input.Email, returnUrl = this.ReturnUrl });
                }

                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.RedirectToLocal();
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }

        private IActionResult RedirectToLocal()
        {
            return this.Url.IsLocalUrl(this.ReturnUrl)
                ? this.LocalRedirect(this.ReturnUrl!)
                : this.LocalRedirect(this.Url.Content("~/"));
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of birth")]
            public DateOnly? Birthdate { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }
    }
}
