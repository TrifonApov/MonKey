namespace MonKey.Web.ViewModels.Currency
{
    using Data.Models;
    using Services.AutoMapping;
    using Services.Models.Currency;

    public class CurrencyViewModel : IMapFrom<CurrencyDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
