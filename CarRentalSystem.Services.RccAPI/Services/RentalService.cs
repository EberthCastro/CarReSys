using CarRentalSystem.Services.RccAPI.Data;
using CarRentalSystem.Services.RccAPI.Models;
using CarRentalSystem.Services.RccAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Services.RccAPI.Services
{
    public class RentalService
    {
        private readonly AppDbContext _db;

        public RentalService(AppDbContext db)
        {
            _db = db;
        }

        // Get all rentals
        public async Task<List<RentalDTO>> GetAllAsync()
        {
            return await _db.Rentals.Select(r => new RentalDTO
            {
                CarId = r.CarId,
                CustomerId = r.CustomerId,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                Price = r.Price,
                ExtraDays = r.ExtraDays,
                CarType = r.CarType,
                TotalPrice = r.TotalPrice
            }).ToListAsync();
        }

        // Get a single rental by ID
        public async Task<RentalDTO?> GetOneAsync(int id)
        {
            var rental = await _db.Rentals.FindAsync(id);
            if (rental == null) return null;

            return new RentalDTO
            {
                CarId = rental.CarId,
                CustomerId = rental.CustomerId,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                Price = rental.Price,
                ExtraDays = rental.ExtraDays,
                CarType = rental.CarType,
                TotalPrice = rental.TotalPrice
            };
        }

        // Create a new rental and change the Isavlaible status to false
        public async Task<RentalDTO> CreateAsync(RentalDTO rentalDto)
        {
            // Retrieve the car by CarId
            var car = await _db.Cars.FindAsync(rentalDto.CarId);
            if (car == null)
            {
                throw new ArgumentException("Car not found.");
            }

            // Check if the car is available
            if (!car.IsAvailable)
            {
                throw new InvalidOperationException("The car is not available for rental.");
            }

            // Update the IsAvailable status of the car
            car.IsAvailable = false;

            // Create a new rental record
            var rental = new RentalDB
            {
                CarId = rentalDto.CarId,
                CustomerId = rentalDto.CustomerId,
                RentalDate = rentalDto.RentalDate,
                ReturnDate = rentalDto.ReturnDate,
                Price = rentalDto.Price,
                ExtraDays = rentalDto.ExtraDays,
                CarType = rentalDto.CarType,
                TotalPrice = rentalDto.TotalPrice
            };

            await _db.Rentals.AddAsync(rental);

            // Save changes to the database (both rental and car updates)
            await _db.SaveChangesAsync();

            return rentalDto;
        }


        // Update an existing rental
        public async Task<RentalDTO?> UpdateAsync(int id, RentalDTO rentalDto)
        {
            var rental = await _db.Rentals.FindAsync(id);
            if (rental == null) return null;

            rental.CarId = rentalDto.CarId;
            rental.CustomerId = rentalDto.CustomerId;
            rental.RentalDate = rentalDto.RentalDate;
            rental.ReturnDate = rentalDto.ReturnDate;
            rental.Price = rentalDto.Price;
            rental.ExtraDays = rentalDto.ExtraDays;
            rental.CarType = rentalDto.CarType;
            rental.TotalPrice = rentalDto.TotalPrice;

            await _db.SaveChangesAsync();

            return rentalDto;
        }

        // Delete a rental
        public async Task<bool> DeleteAsync(int id)
        {
            var rental = await _db.Rentals.FindAsync(id);
            if (rental == null) return false;

            _db.Rentals.Remove(rental);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
