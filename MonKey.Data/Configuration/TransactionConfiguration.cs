namespace MonKey.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(t => t.TransactionType)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.HasIndex(t => t.TransferId);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Transaction_Transfer",
                    "([TransactionType] = 'Transfer' AND [TransferId] IS NOT NULL AND [CategoryId] IS NULL) " +
                    "OR ([TransactionType] <> 'Transfer' AND [TransferId] IS NULL AND [ExchangeRate] IS NULL)");

                t.HasCheckConstraint("CK_Transaction_Amount",
                    "([TransactionType] <> 'Income' OR [Amount] > 0) " +
                    "AND ([TransactionType] <> 'Outcome' OR [Amount] < 0) " +
                    "AND [Amount] <> 0");
            });
        }
    }
}
