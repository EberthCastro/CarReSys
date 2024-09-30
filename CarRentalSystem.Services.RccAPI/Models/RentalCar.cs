using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Services.RccAPI.Models
{
    public class RentalCar
    {
        [Key]
        public int RentalCarId { get; set; }

        // Car details
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Type { get; set; }  // Car Type: "Small", "SUV", "Premium"
        [Required]
        public string? PricePerDay { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        // Customer details
        
        public string? CustomerName { get; set; }
        public int LoyaltyPoints { get; set; }

        // Rental details
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? TotalPrice { get; set; }
    }
}
