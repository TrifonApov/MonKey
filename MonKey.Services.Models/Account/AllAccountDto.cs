namespace MonKey.Services.Models.Account
{
    using AutoMapper;
    using AutoMapping;
    using MonKey.Data.Models;

    public class AllAccountDto : IMapFrom<Account>, IMapTo<Account>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;
    }
}
