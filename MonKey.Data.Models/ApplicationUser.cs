namespace MonKey.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUser : IdentityUser<Guid>
    {
        [Comment("User's Birthdate")]
        public DateOnly Birthdate { get; set; }

        public ICollection<Account> Accounts { get; set; }
            = new HashSet<Account>();
    }
}
