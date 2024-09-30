using CarRentalSystem.Services.RccAPI.Models;
using CarRentalSystem.Services.RccAPI.Models.Dtos;
using CarRentalSystem.Services.RccAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Services.RccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<RentalDTO> result = await _rentalService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne([FromRoute] int id)
        {
            try
            {
                RentalDTO? result = await _rentalService.GetOneAsync(id);

                if (result == null)
                {
                    return NotFound($"Rental with Id = {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddNewRental")]
        public async Task<ActionResult> AddNewRental([FromBody] RentalDTO rentalDto)
        {
            try
            {
                RentalDTO result = await _rentalService.CreateAsync(rentalDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, [FromBody] RentalDTO rentalDto)
        {
            try
            {
                RentalDTO? result = await _rentalService.UpdateAsync(id, rentalDto);
                if (result == null)
                {
                    return NotFound($"Rental with Id = {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            var success = await _rentalService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Rental with Id = {id} not found.");
            }
            return NoContent();
        }

        //Method to calculate the price for rental after the Customer return the Car
        [HttpPost("RentalPrice/{rentalId}")]
        public async Task<IActionResult> RentalPrice([FromRoute] int rentalId, RentalDTO rentalDto)
        {
            try
            {
                // Obtener el alquiler desde la base de datos usando el RentalId
                RentalDTO? result = await _rentalService.GetOneAsync(rentalId); // Asegúrate de tener este método en tu servicio

                if (result == null)
                {
                    return NotFound($"Rental with Id = {rentalId} not found.");
                }

                decimal totalPrice = 0;
                int numberOfDays = (result.ReturnDate - result.RentalDate).Days;

                Console.WriteLine($"CarType received: '{result.RentalDate}'");
                Console.WriteLine($"CarType received: '{result.CarType}'");

                // Define prices
                const decimal premiumPrice = 300m;
                const decimal suvPrice = 150m;
                const decimal smallPrice = 50m;

                // Base price based on car type
                decimal basePrice = result.CarType switch
                {
                    "Premium" => premiumPrice,
                    "SUV" => suvPrice,
                    "Small" => smallPrice,
                    _ => throw new ArgumentException("Invalid car type.")
                };

                // Calculate the total price based on car type and rental duration
                totalPrice = result.CarType switch
                {
                    "Premium" => CalculatePremiumPrice(numberOfDays, basePrice),
                    "SUV" => CalculateSuvPrice(numberOfDays, basePrice),
                    "Small" => CalculateSmallCarPrice(numberOfDays, basePrice),
                    _ => 0
                };

                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private decimal CalculatePremiumPrice(int numberOfDays, decimal premiumPrice)
        {
            decimal totalPrice = premiumPrice * numberOfDays;
            if (numberOfDays > 1)
            {
                totalPrice += premiumPrice * 0.20m * (numberOfDays - 1); // Extra days
            }
            return totalPrice;
        }

        private decimal CalculateSuvPrice(int numberOfDays, decimal suvPrice)
        {
            decimal totalPrice;

            if (numberOfDays <= 7)
            {
                totalPrice = suvPrice * numberOfDays;
            }
            else if (numberOfDays <= 30)
            {
                totalPrice = (suvPrice * 7) + (suvPrice * 0.80m * (numberOfDays - 7));
            }
            else
            {
                totalPrice = (suvPrice * 7) + (suvPrice * 0.50m * (numberOfDays - 30));
            }

            return totalPrice;
        }

        private decimal CalculateSmallCarPrice(int numberOfDays, decimal smallPrice)
        {
            decimal totalPrice;

            if (numberOfDays <= 7)
            {
                totalPrice = smallPrice * numberOfDays;
            }
            else
            {
                totalPrice = (smallPrice * 7) + (smallPrice * 0.60m * (numberOfDays - 7));
            }

            return totalPrice;
        }

    }
}
