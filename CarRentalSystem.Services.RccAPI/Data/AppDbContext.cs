using CarRentalSystem.Services.RccAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Services.RccAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<CarDB> Cars { get; set; }
        public DbSet<CustomerDB> Customers { get; set; }
        public DbSet<RentalDB> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define one-to-many relationships
            modelBuilder.Entity<RentalDB>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalDB>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for Cars
            modelBuilder.Entity<CarDB>().HasData(
                new CarDB { CardId = 1, Brand = "Toyota", Model = "Corolla", Type = "Small", PricePerDay = "50", IsAvailable = true },
                new CarDB { CardId = 2, Brand = "Honda", Model = "Civic", Type = "Small", PricePerDay = "50", IsAvailable = true },
                new CarDB { CardId = 3, Brand = "Ford", Model = "Explorer", Type = "SUV", PricePerDay = "150", IsAvailable = true },
                new CarDB { CardId = 4, Brand = "Jeep", Model = "Grand Cherokee", Type = "SUV", PricePerDay = "150", IsAvailable = true },
                new CarDB { CardId = 5, Brand = "BMW", Model = "X5", Type = "Premium", PricePerDay = "300", IsAvailable = true },
                new CarDB { CardId = 6, Brand = "Audi", Model = "A8", Type = "Premium", PricePerDay = "300", IsAvailable = true },
                new CarDB { CardId = 7, Brand = "Mercedes", Model = "C-Class", Type = "Premium", PricePerDay = "300", IsAvailable = true },
                new CarDB { CardId = 8, Brand = "Hyundai", Model = "Elantra", Type = "Small", PricePerDay = "50", IsAvailable = true },
                new CarDB { CardId = 9, Brand = "Kia", Model = "Sorento", Type = "SUV", PricePerDay = "150", IsAvailable = true },
                new CarDB { CardId = 10, Brand = "Lexus", Model = "RX", Type = "Premium", PricePerDay = "300", IsAvailable = true }
            );

            // Seed data for Customers
            modelBuilder.Entity<CustomerDB>().HasData(
                new CustomerDB { CustomerId = 1, Name = "John Doe", LoyaltyPoints = 10 },
                new CustomerDB { CustomerId = 2, Name = "Jane Smith", LoyaltyPoints = 20 },
                new CustomerDB { CustomerId = 3, Name = "Robert Johnson", LoyaltyPoints = 15 },
                new CustomerDB { CustomerId = 4, Name = "Emily Davis", LoyaltyPoints = 25 },
                new CustomerDB { CustomerId = 5, Name = "Michael Williams", LoyaltyPoints = 30 },
                new CustomerDB { CustomerId = 6, Name = "Sarah Brown", LoyaltyPoints = 35 },
                new CustomerDB { CustomerId = 7, Name = "David Jones", LoyaltyPoints = 40 },
                new CustomerDB { CustomerId = 8, Name = "Linda Garcia", LoyaltyPoints = 50 },
                new CustomerDB { CustomerId = 9, Name = "James Miller", LoyaltyPoints = 60 },
                new CustomerDB { CustomerId = 10, Name = "Patricia Martinez", LoyaltyPoints = 70 }
            );
        }
    }
}
