using Microsoft.EntityFrameworkCore;

namespace PartyInvites.Models
{
    public class ApplicationContext: DbContext
    {
        private Func<object, object> value;

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationContext(Func<object, object> value)
        {
            this.value = value;
        }

        public DbSet<GuestResponse> GuestResponses { get; set; } = null!;
        
    }
}
