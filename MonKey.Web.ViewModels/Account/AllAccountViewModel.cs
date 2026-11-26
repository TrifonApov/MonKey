namespace MonKey.Web.ViewModels.Account
{
    using Services.AutoMapping;
    using Services.Models.Account;

    public class AllAccountViewModel : IMapFrom<AllAccountDto>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Balance { get; set; }

        public string CurrencyName { get; set; } = null!;

        public string CurrencyCode { get; set; } = null!;
    }
}
