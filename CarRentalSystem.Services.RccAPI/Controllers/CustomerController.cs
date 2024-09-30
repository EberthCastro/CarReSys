using CarRentalSystem.Services.RccAPI.Models.Dtos;
using CarRentalSystem.Services.RccAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Services.RccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                List<CustomerDTO> result = await _customerService.GetAllAsync();
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
                CustomerDTO? result = await _customerService.GetOneAsync(id);

                if (result == null)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddNewCustomer")]
        public async Task<ActionResult> AddNewCustomer([FromBody] CustomerDTO customerDto)
        {
            try
            {
                CustomerDTO result = await _customerService.CreateAsync(customerDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CustomerDTO customerDto)
        {
            try
            {
                bool result = await _customerService.UpdateAsync(id, customerDto);
                if (!result)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }

                var customerUpdated = await _customerService.GetOneAsync(id);

                return Ok(customerUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var success = await _customerService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Customer with Id = {id} not found.");
            }
            return NoContent();
        }
    }
}
