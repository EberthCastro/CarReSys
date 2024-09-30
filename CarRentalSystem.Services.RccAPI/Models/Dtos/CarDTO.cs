
namespace CarRentalSystem.Services.RccAPI.Models.Dtos
{
    public class CarDTO
    {
        
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
}
