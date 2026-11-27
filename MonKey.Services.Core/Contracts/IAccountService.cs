namespace MonKey.Services.Core.Contracts
{
    using Data.Models;
    using Models.Currency;
    using MonKey.Services.Models.Account;

    public interface IAccountService
    {
        Task<IEnumerable<AllAccountDto>> GetAllUserAccountsOrderByNameAsync(Guid userId);

        Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync();

        Task CreateAccountAsync(CreateAccountDto accountDto);
    }
}
