namespace MonKey.Services.Core
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repository.Contracts;
    using GCommon.Exceptions;
    using Models.Account;
    using Models.Currency;

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMapper mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AllAccountDto>> GetAllUserAccountsOrderByNameAsync(Guid userId)
        {
            IEnumerable<Account> userAccounts = await accountRepository
                .GetAllUserAccountsOrderByNameAsync(userId);


            IEnumerable<AllAccountDto> allAccountDtos = mapper
                .Map<IEnumerable<AllAccountDto>>(userAccounts)
                .OrderBy(a => a.Name)
                .ToArray();

            return allAccountDtos;
        }

        public async Task CreateAccountAsync(CreateAccountDto accountDto)
        {
            Account account = mapper.Map<Account>(accountDto);

            bool result = await accountRepository.CreateAccountAsync(account);

            if (!result)
            {
                throw new EntityPersistFailureException();
            }
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync()
        {
            IEnumerable<Currency> allCurrencies = await accountRepository
                .GetAllCurrenciesOrderByNameAsync();

            IEnumerable<CurrencyDto> currencyDtos = mapper
                .Map<IEnumerable<CurrencyDto>>(allCurrencies);

            return currencyDtos;
        }

    }
}
