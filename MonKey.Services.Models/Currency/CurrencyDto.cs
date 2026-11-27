namespace MonKey.Services.Models.Currency
{
    using AutoMapping;
    using Data.Models;

    public class CurrencyDto : IMapFrom<Currency>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
