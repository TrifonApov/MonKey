namespace MonKey.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using MonKey.GCommon.Exceptions;
    using Services.Core.Contracts;
    using Services.Models.Account;
    using Services.Models.Currency;
    using Web.ViewModels.Account;
    using Web.ViewModels.Currency;
    using static GCommon.OutputMessages.Account;

    public class AccountController : BaseController
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Guid userId = GetUserGuid()
                          ?? throw new InvalidOperationException("The authenticated user has no identifier.");

            IEnumerable<AllAccountDto> accounts = await accountService
                .GetAllUserAccountsOrderByNameAsync(userId);

            IEnumerable<AllAccountViewModel> allAccountViewModels = mapper
                .Map<IEnumerable<AllAccountViewModel>>(accounts);

            return View(allAccountViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<CurrencyViewModel> currencies = await GetAllCurrenciesAsync();

            return Json(currencies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountFormViewModel model)
        {
            Guid userId = GetUserGuid()
                          ?? throw new InvalidOperationException("The authenticated user has no identifier.");

            if (model.CurrencyId == Guid.Empty)
            {
                ModelState.AddModelError(nameof(model.CurrencyId), "Select a currency.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = ModelState
                        .Where(entry => entry.Value?.Errors.Count > 0)
                        .ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value!.Errors.Select(error => error.ErrorMessage).ToArray()),
                });
            }

          
            try
            {
                CreateAccountDto accountDto = mapper
                    .Map<CreateAccountDto>(model);

                accountDto.OwnerId = userId;

                await accountService.CreateAccountAsync(accountDto);
            }
            catch (EntityPersistFailureException exception)
            {
                ModelState.AddModelError(string.Empty, CreateAccountFailureMessage);
                return RedirectToAction(nameof(Create));
            }
            //TODO: Catch if the Account name is not unique and give the propper message
            
            return Ok(new { redirectUrl = Url.Action(nameof(Index)) });
        }

        private async Task<IEnumerable<CurrencyViewModel>> GetAllCurrenciesAsync()
        {
            IEnumerable<CurrencyDto> allCurrencies = await accountService.GetAllCurrenciesAsync();

            IEnumerable<CurrencyViewModel> currencyViewModels = mapper
                .Map<IEnumerable<CurrencyViewModel>>(allCurrencies);

            return currencyViewModels;
        }
    }
}
