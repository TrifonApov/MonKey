namespace MonKey.Services.Core
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repository.Contracts;
    using Models.Account;

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
    }
}
