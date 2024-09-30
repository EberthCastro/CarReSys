using CarRentalSystem.Services.RccAPI.Models.Dtos;
using CarRentalSystem.Services.RccAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Services.RccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<CarDTO> result = await _carService.GetAllAsync();

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
                CarDTO? result = await _carService.GetOneAsync(id);

                if (result == null)
                {
                    return NotFound($"Car with Id = {id} not found.");
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddNewCar")]
        public async Task<ActionResult> AddNewCar([FromBody] CarDTO carDto)
        {
            try
            {
            CarDTO result = await _carService.CreateAsync(carDto);
            return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar(int id, [FromBody] CarDTO carDto)
        {
            try
            {
                bool result = await _carService.UpdateAsync(id, carDto);
                if (!result)
                {
                    return NotFound($"Car with Id = {id} not found.");
                }

                var carUpdated = await _carService.GetOneAsync(id);

                return Ok(carUpdated);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var success = await _carService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Car with Id = {id} not found.");
            }
            return NoContent();
        }
    }
}
