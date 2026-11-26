namespace MonKey.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Microsoft.EntityFrameworkCore;

    [Comment("The Category of the Transaction Performed")]
    public class Category
    {
        [Comment("Category's GUID")]
        [Key]
        public Guid Id { get; set; }

        [Comment("Category's Name")]
        [MaxLength(EntityValidation.Category.CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("URL to Category's Icon")]
        [MaxLength(EntityValidation.Category.IconUrlNameMaxLength)]
        public string IconUrl { get; set; } = null!;

        [Comment("All Category's Transactions")]
        public IEnumerable<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
