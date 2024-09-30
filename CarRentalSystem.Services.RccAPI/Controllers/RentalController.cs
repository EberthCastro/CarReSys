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
        private readonly CustomerService _customerService;
        private readonly CarService _carService;

        public RentalController(RentalService rentalService, CustomerService customerService, CarService carService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _carService = carService;
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
        public async Task<IActionResult> RentalPrice([FromRoute] int rentalId)
        {
            try
            {
                // Obtener el alquiler desde la base de datos usando el RentalId
                RentalDTO? result = await _rentalService.GetOneAsync(rentalId); 
                if (result == null)
                {
                    return NotFound($"Rental with Id = {rentalId} not found.");
                }

                decimal Price = 0;
                int numberOfDays = (result.ReturnDate - result.RentalDate).Days;                

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
                Price = result.CarType switch
                {
                    "Premium" => CalculatePremiumPrice(numberOfDays, basePrice),
                    "SUV" => CalculateSuvPrice(numberOfDays, basePrice),
                    "Small" => CalculateSmallCarPrice(numberOfDays, basePrice),
                    _ => 0
                };

                // Update the rental record with the calculated price
                result.Price = Price.ToString(); 
                await _rentalService.UpdateAsync(rentalId, result); 

                return Ok(Price);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CalculateTotalPrice/{rentalId}/{ExtraDays}")]
        public async Task<IActionResult> CalculateTotalPrice([FromRoute] int rentalId, int ExtraDays)
        {
            try
            {
                // Obtener el alquiler desde la base de datos usando el RentalId
                RentalDTO? rental = await _rentalService.GetOneAsync(rentalId);
                if (rental == null)
                {
                    return NotFound($"Rental with Id = {rentalId} not found.");
                }

                // Obtener el RentalPrice desde la base de datos
                decimal rentalPrice = decimal.Parse(rental.Price ?? "0"); 

                // Calcular el TotalPrice considerando los días extra
                decimal extraCharges = CalculateExtraCharges(rental.CarType, ExtraDays.ToString());
                decimal totalPrice = rentalPrice + extraCharges;

                // Actualizar el TotalPrice en el objeto RentalDTO
                rental.TotalPrice = totalPrice.ToString("F2");
                rental.ExtraDays = ExtraDays.ToString();

                // Actualizar el alquiler en la base de datos
                await _rentalService.UpdateAsync(rentalId, rental);

                // Obtener el cliente asociado al Rental usando CustomerId
                CustomerDTO? customer = await _customerService.GetOneAsync(rental.CustomerId);
                if (customer == null)
                {
                    return NotFound($"Customer with Id = {rental.CustomerId} not found.");
                }

                // Actualizar los LoyaltyPoints
                int loyaltyPoints = CalculateLoyaltyPoints(rental.CarType);
                customer.LoyaltyPoints += loyaltyPoints;                
                await _customerService.UpdateAsync(rental.CustomerId, customer);

                // Obtener el coche asociado al alquiler usando CarId y cambiar su disponibilidad    **************checkaer elCarID en el DTO- re migrate
                CarDTO? car = await _carService.GetOneAsync(rental.CarId);                
                car.IsAvailable = true;
                bool carUpdated = await _carService.UpdateAsync(car.CarId, car);

                // Devolver el objeto de alquiler actualizado
                return Ok(rental);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        private decimal CalculatePremiumPrice(int numberOfDays, decimal premiumPrice)
        {
            decimal Price = premiumPrice * numberOfDays;
            if (numberOfDays > 1)
            {
                Price += premiumPrice * 0.20m * (numberOfDays - 1); 
            }
            return Price;
        }

        private decimal CalculateSuvPrice(int numberOfDays, decimal suvPrice)
        {
            decimal Price;

            if (numberOfDays <= 7)
            {
                Price = suvPrice * numberOfDays;
            }
            else if (numberOfDays <= 30)
            {
                Price = (suvPrice * 7) + (suvPrice * 0.80m * (numberOfDays - 7));
            }
            else
            {
                Price = (suvPrice * 7) + (suvPrice * 0.50m * (numberOfDays - 30));
            }

            return Price;
        }

        private decimal CalculateSmallCarPrice(int numberOfDays, decimal smallPrice)
        {
            decimal Price;

            if (numberOfDays <= 7)
            {
                Price = smallPrice * numberOfDays;
            }
            else
            {
                Price = (smallPrice * 7) + (smallPrice * 0.60m * (numberOfDays - 7));
            }

            return Price;
        }

        private decimal CalculateExtraCharges(string? carType, string? extraDays)
        {
            // Convertir extraDays a int
            int extraDaysCount = string.IsNullOrWhiteSpace(extraDays) ? 0 : int.Parse(extraDays);
            decimal extraCharges = 0;

            // Si no hay días extra, no hay cargos
            if (extraDaysCount <= 0) return extraCharges; 

            switch (carType)
            {
                case "Premium":
                    extraCharges += (300m + (300m * 0.20m)) * extraDaysCount; 
                    break;

                case "SUV":
                    extraCharges += (150m + (150m * 0.60m)) * extraDaysCount; 
                    break;

                case "Small":
                    extraCharges += (50m + (50m * 0.30m)) * extraDaysCount; 
                    break;

                default:
                    throw new ArgumentException("Invalid car type.");
            }

            return extraCharges;
        }

        private int CalculateLoyaltyPoints(string? carType)
        {
            return carType switch
            {
                "Premium" => 5,
                "SUV" => 3,
                "Small" => 1,
                _ => throw new ArgumentException("Invalid car type for loyalty points.")
            };
        }

    }
}
