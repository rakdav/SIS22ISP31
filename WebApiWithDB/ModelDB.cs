using Microsoft.EntityFrameworkCore;

namespace WebApiWithDB
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id=1,Name="Vasia",Age=23},
                new User { Id = 2, Name = "Boris", Age = 31 },
                new User { Id = 3, Name = "Egor", Age = 28 }
                );
        }
    }
}
