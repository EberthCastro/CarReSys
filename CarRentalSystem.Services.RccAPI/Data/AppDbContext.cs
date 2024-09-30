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
    }
}
