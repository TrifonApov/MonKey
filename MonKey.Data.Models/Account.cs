namespace MonKey.Data.Models
{
    using MonKey.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    [Index(nameof(OwnerId), nameof(Name), IsUnique = true)]
    public class Account
    {
        [Comment("Account's GUID")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Account's Name")]
        [Required]
        [MaxLength(EntityValidation.Account.AccountNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Account's Description (optional)")]
        [MaxLength(EntityValidation.Account.DescriptionNameMaxLength)]
        public string? Description { get; set; }

        [Comment("The Balance of the Account")]
        [Column(TypeName = EntityValidation.Transaction.MoneyPrecisionType)]
        public decimal Balance { get; set; }

        [Comment("Is Account Deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Account Currency's ID")]
        [ForeignKey(nameof(Currency))]
        public Guid CurrencyId { get; set; }
        
        public Currency Currency { get; set; } = null!;

        [Comment("Account Owner's ID")]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        public ApplicationUser Owner { get; set; } = null!;

        [Comment("All account's transaction ")]
        public ICollection<Transaction> Transactions { get; set; } 
            = new List<Transaction>();
    }
}
