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
    }
}
