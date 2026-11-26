namespace MonKey.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using MonKey.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Transaction Performed On an Account")]
    public class Transaction
    {
        [Comment("Transaction's GUID")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Transaction Type Enum")]
        public TransactionType TransactionType { get; set; }

        [Comment("The Amount of the Transaction")]
        [Column(TypeName = EntityValidation.Transaction.AmountPrecisionType)]
        public decimal Amount { get; set; }

        [Comment("Time When Transaction Is Created")]
        public DateTime DateCreated { get; set; }
        
        [Comment("Time When Transaction Is Modified For Last Time")]
        public DateTime DateModified { get; set; }

        [Comment("User ID of Creator of Transaction")]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [Comment("The Account In Which the Transaction Was Made")]
        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;
    }
}
