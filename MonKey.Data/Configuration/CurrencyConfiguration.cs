namespace MonKey.Data.Configuration
{
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> entity)
        {
            entity
                .HasIndex(c => c.Code)
                .IsUnique();

            entity.Property(e => e.Code)
                .HasMaxLength(EntityValidation.Currency.FixedCurrencyCodeLength)
                .IsFixedLength()
                .IsRequired();

            entity.HasData(SeedCurrencies());
        }

        private IEnumerable<Currency> SeedCurrencies()
        {
            return new List<Currency>()
            {
                new()
                {
                    Id = new Guid("23991ac6-ae94-48a1-97f2-65c74938e578"),
                    Name = "Euro",
                    Code = "EUR"
                },
                new()
                {
                    Id = new Guid("7e6e976f-7a9e-485a-990d-cb2e21baa44f"),
                    Name = "US Dollar",
                    Code = "USD"
                },
                new()
                {
                    Id = new Guid("0f67c06c-e090-4898-8a1f-230d3ecd4074"),
                    Name = "Pound Sterling",
                    Code = "GBP"
                },
                new()
                {
                    Id = new Guid("c2003e10-42e9-46e5-a278-8cad7712ab12"),
                    Name = "Romanian Leu",
                    Code = "RON"
                },
            };
        }
    }
}