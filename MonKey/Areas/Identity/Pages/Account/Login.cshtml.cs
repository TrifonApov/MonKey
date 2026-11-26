namespace MonKey.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class LoginModel : PageModel
    {
        private readonly SignInManager<Data.Models.ApplicationUser> signInManager;

        public LoginModel(SignInManager<Data.Models.ApplicationUser> signInManager)
        {
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

            var result = await this.signInManager.PasswordSignInAsync(
                this.Input.Email,
                this.Input.Password,
                this.Input.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return this.RedirectToLocal();
            }

            if (result.RequiresTwoFactor)
            {
                return this.RedirectToPage("./LoginWith2fa", new { ReturnUrl = this.ReturnUrl, RememberMe = this.Input.RememberMe });
            }

            if (result.IsLockedOut)
            {
                return this.RedirectToPage("./Lockout");
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
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
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }
    }
}
