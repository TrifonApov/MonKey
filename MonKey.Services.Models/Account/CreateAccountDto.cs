namespace MonKey.Services.Models.Account
{
    using AutoMapper;
    using AutoMapping;
    using Data.Models;

    public class CreateAccountDto : IMapTo<Account>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Balance { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid OwnerId { get; set; }
    }
}
