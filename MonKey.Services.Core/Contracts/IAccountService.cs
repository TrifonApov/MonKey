namespace MonKey.Services.Core.Contracts
{
    using Data.Models;
    using MonKey.Services.Models.Account;

    public interface IAccountService
    {
        Task<IEnumerable<AllAccountDto>> GetAllUserAccountsOrderByNameAsync(Guid userId);
    }
}
