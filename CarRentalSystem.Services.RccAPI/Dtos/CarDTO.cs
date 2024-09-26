using CarRentalSystem.Services.RccAPI.Models;

namespace CarRentalSystem.Services.RccAPI.Dtos
{
    public class CarDTO
    {
        public int CardId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
