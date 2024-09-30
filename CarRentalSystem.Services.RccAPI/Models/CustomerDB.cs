using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Services.RccAPI.Models
{
    public class CustomerDB
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string? Name { get; set; }
        public int LoyaltyPoints { get; set; }

        public virtual ICollection<RentalDB> Rentals { get; set; } = new List<RentalDB>();
    }
}
