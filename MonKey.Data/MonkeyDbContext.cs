namespace MonKey.Data
{
    using System.Reflection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MonkeyDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public MonkeyDbContext(DbContextOptions<MonkeyDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Currency> Currencies { get; set; } = null!;
        
        public DbSet<Transaction> Transactions{ get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
