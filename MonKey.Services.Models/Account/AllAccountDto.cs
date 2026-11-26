namespace MonKey.Services.Models.Account
{
    public class AllAccountDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyName { get; set; } = null!;
        
        public string CurrencyCode { get; set; } = null!;
    }
}
