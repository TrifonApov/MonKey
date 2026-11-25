namespace MonKey.Data.Models
{
    using MonKey.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;

    public class Currency
    {
        [Comment("Currency's GUID")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Currency's Name")]
        [Required]
        [MaxLength(EntityValidation.Currency.CurrencyNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Currency's ISO Code")]
        [Required]
        [MaxLength(EntityValidation.Currency.FixedCurrencyCodeLength)]
        public string Code { get; set; } = null!;
    }
}
