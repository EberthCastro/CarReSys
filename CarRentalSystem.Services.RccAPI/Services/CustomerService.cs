using CarRentalSystem.Services.RccAPI.Data;
using CarRentalSystem.Services.RccAPI.Models;
using CarRentalSystem.Services.RccAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Services.RccAPI.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _db;

        public CustomerService(AppDbContext db)
        {
            _db = db;
        }

        // Get all customers
        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            var customers = await _db.Customers.ToListAsync();
            return customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                LoyaltyPoints = c.LoyaltyPoints
            }).ToList();
        }

        // Get one customer by ID
        public async Task<CustomerDTO> GetOneAsync(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                LoyaltyPoints = customer.LoyaltyPoints
            };
        }

        // Create a new customer
        public async Task<CustomerDTO> CreateAsync(CustomerDTO customerDto)
        {
            var customer = new CustomerDB
            {
                Name = customerDto.Name,
                LoyaltyPoints = customerDto.LoyaltyPoints
            };

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();

            return new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                LoyaltyPoints = customer.LoyaltyPoints
            };
        }

        // Update an existing customer
        public async Task<bool> UpdateAsync(int id, CustomerDTO customerDto)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return false;

            customer.Name = customerDto.Name;
            customer.LoyaltyPoints = customerDto.LoyaltyPoints;

            await _db.SaveChangesAsync();

            return true;
        }

        // Delete a customer
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return false;

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
