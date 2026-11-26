namespace MonKey.Data.Repository
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(MonkeyDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Account>> GetAllUserAccountsOrderByNameAsync(Guid userId)
        {
            IQueryable<Account> userAccounts = DbContext
                .Accounts
                .Where(a => a.OwnerId == userId && !a.IsDeleted)
                .Include(a=>a.Currency)
                .AsNoTracking()
                .OrderBy(a => a.Name);

            return await userAccounts.ToArrayAsync();
        }
    }
}
