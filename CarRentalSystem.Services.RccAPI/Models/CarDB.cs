using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Services.RccAPI.Models
{
    public class CarDB
    {
        [Key]
        public int CardId { get; set; }
        [Required]
        public string? Brand { get; set; }        
        [Required]
        public string? Model { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? PricePerDay { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public virtual ICollection<RentalDB> Rentals { get; set; } = new List<RentalDB>();
    } 


}
