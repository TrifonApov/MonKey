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

        [Comment("The amount in the account's currency; balance = SUM(Amount)")]
        [Column(TypeName = EntityValidation.Transaction.MoneyPrecisionType)]
        public decimal Amount { get; set; }

        [Comment("Time When Transaction Is Created")]
        public DateTime DateCreated { get; set; }
        
        [Comment("Time When Transaction Is Modified For Last Time")]
        public DateTime DateModified { get; set; }

        [Comment("The Account In Which the Transaction Was Made")]
        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;

        [Comment("Category (null for transfers)")]
        [ForeignKey(nameof(Category))]
        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        [Comment("Shared by both legs of a transfer; null otherwise")]
        public Guid? TransferId { get; set; }

        [Comment("Exchange rate used for a cross-currency transfer (source → destination); null otherwise")]
        [Column(TypeName = EntityValidation.Transaction.ExchangeRatePrecisionType)]
        public decimal? ExchangeRate { get; set; }
    }
}
