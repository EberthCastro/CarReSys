namespace CarRentalSystem.Services.RccAPI.Models
{
    public class CarDB
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public CarType Type { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
    }
    public enum CarType
    {
        Small,
        SUV,
        Premium
    }


}
