namespace MonKey.Data.Repository.Contracts
{
    using Models;

    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllUserAccountsOrderByNameAsync(Guid userId);
    }
}
