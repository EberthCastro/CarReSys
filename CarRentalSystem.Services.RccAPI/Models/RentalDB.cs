using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Services.RccAPI.Models
{
    public class RentalDB
    {
        [Key]
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Price { get; set; }
        public string? ExtraDays { get; set; }
        public string? CarType { get; set; }
        public string? TotalPrice { get; set; }
    }
}
