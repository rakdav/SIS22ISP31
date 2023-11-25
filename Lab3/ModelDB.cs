using Microsoft.EntityFrameworkCore;

namespace Lab3
{
    public class ModelDB:DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
           Database.EnsureCreated();
        }

        public DbSet<PriceList> PriceList { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PriceList VWGolf = new PriceList
            {
                Id = 7,
                Marka = "VW",
                Complect = "Golf",
                Price = 2100000
            };
            modelBuilder.Entity<PriceList>().HasData(
                new PriceList { Id = 1, Marka = "VW", Complect = "Passat", Price = 2700000 },
                new PriceList { Id = 2, Marka = "BMW", Complect = "Benz", Price = 3200000 },
                new PriceList { Id = 3, Marka = "Peugeot", Complect = "Base", Price = 7000000 },
                new PriceList { Id = 4, Marka = "Toyota", Complect = "LandCruiser", Price = 4200000 },
                new PriceList { Id = 5, Marka = "Nissan", Complect = "Navara", Price = 5600000 },
                new PriceList { Id = 6, Marka = "Kia", Complect = "Nova", Price = 1900000 }
                , VWGolf
                );
            modelBuilder.Entity<Sales>().HasData(
                new Sales
                {
                    Id=1,
                    DateSale = new DateTime(2023, 10, 4),
                    FIO = "Иванов Иван Иванович",
                    PriceList_Id=1,
                    Complect = "Super",
                    Discount = 10,
                    TotalPrice = 1950000
                }
                );
            modelBuilder.Entity<User>().HasData(
                new User{Id=1,Email="tom@gmail.com", Password="12345" },
                new User{Id=2,Email="bob@gmail.com", Password="55555" }
                );
        }
    }
}
