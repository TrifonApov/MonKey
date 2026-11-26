namespace MonKey.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Contracts;
    using Services.Models.Account;
    using Web.ViewModels.Account;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = this.GetUserGuid() 
                          ?? throw new InvalidOperationException("The authenticated user has no identifier.");
            
            IEnumerable<AllAccountDto> accounts = await this.accountService
                .GetAllUserAccountsOrderByNameAsync(userId);

            IEnumerable<AllAccountViewModel> allAccountViewModels = accounts
                .Select(a => new AllAccountViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = a.Amount,
                    CurrencyCode = a.CurrencyCode,
                    CurrencyName = a.CurrencyName,
                    Description = a.Description
                });

            return View(allAccountViewModels);
        }
    }
}
    