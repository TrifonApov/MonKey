namespace MonKey.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Services.Core.Contracts;
    using Services.Models.Account;
    using Web.ViewModels.Account;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            Guid userId = this.GetUserGuid() 
                          ?? throw new InvalidOperationException("The authenticated user has no identifier.");
            
            IEnumerable<AllAccountDto> accounts = await this.accountService
                .GetAllUserAccountsOrderByNameAsync(userId);

            IEnumerable<AllAccountViewModel> allAccountViewModels = mapper
                .Map<IEnumerable<AllAccountViewModel>>(accounts);

            return View(allAccountViewModels);
        }
    }
}
    