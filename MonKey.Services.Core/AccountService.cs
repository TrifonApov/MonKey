namespace MonKey.Services.Core
{
    using Contracts;
    using Data.Models;
    using Data.Repository.Contracts;
    using Models.Account;

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AllAccountDto>> GetAllUserAccountsOrderByNameAsync(Guid userId)
        {
            IEnumerable<Account> userAccounts = await accountRepository
                .GetAllUserAccountsOrderByNameAsync(userId);

            IEnumerable<AllAccountDto> allAccountDtos = userAccounts
                .Select(ua => new AllAccountDto()
                {
                    Id = ua.Id,
                    Name = ua.Name,
                    Amount = ua.Amount,
                    CurrencyCode = ua.Currency.Code,
                    CurrencyName = ua.Currency.Name,
                    Description = ua.Description
                })
                .OrderBy(dto=>dto.Name);

            return allAccountDtos;
        }
    }
}
