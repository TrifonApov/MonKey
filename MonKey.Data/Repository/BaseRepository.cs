namespace MonKey.Data.Repository
{
    public class BaseRepository
    {
        private bool isDisposed = false;
        private readonly MonkeyDbContext dbContext;

        protected BaseRepository(MonkeyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected MonkeyDbContext DbContext => dbContext;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            isDisposed = true;
        }
        protected async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
