using CarRentalSystem.Services.RccAPI.Data;
using CarRentalSystem.Services.RccAPI.Models;
using CarRentalSystem.Services.RccAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Services.RccAPI.Services
{
    public class CarService
    {
        private readonly AppDbContext _db;

        public CarService(AppDbContext db)
        {
            _db = db;
        }

        // Método para obtener todos los autos
        public async Task<List<CarDTO>> GetAllAsync()
        {
            var cars = await _db.Cars.ToListAsync();
            return cars.Select(car => new CarDTO
            {
                Brand = car.Brand,
                Model = car.Model,
                Type = car.Type,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable
            }).ToList();
        }

        // Método para obtener un solo auto por Id
        public async Task<CarDTO?> GetOneAsync(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return null;

            return new CarDTO
            {
                Brand = car.Brand,
                Model = car.Model,
                Type = car.Type,
                PricePerDay = car.PricePerDay,
                IsAvailable = car.IsAvailable
            };
        }

        // Método para crear un nuevo auto
        public async Task<CarDTO> CreateAsync(CarDTO carDto)
        {
            var newCar = new CarDB
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Type = carDto.Type,
                PricePerDay = carDto.PricePerDay,
                IsAvailable = carDto.IsAvailable
            };

            _db.Cars.Add(newCar);
            await _db.SaveChangesAsync();

            return new CarDTO
            {
                Brand = newCar.Brand,
                Model = newCar.Model,
                Type = newCar.Type,
                PricePerDay = newCar.PricePerDay,
                IsAvailable = newCar.IsAvailable
            };
        }

        // Método para actualizar un auto existente
        public async Task<bool> UpdateAsync(int id, CarDTO carDto)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return false;

            car.Brand = carDto.Brand;
            car.Model = carDto.Model;
            car.Type = carDto.Type;
            car.PricePerDay = carDto.PricePerDay;
            car.IsAvailable = carDto.IsAvailable;

            await _db.SaveChangesAsync();
            return true;
        }

        // Método para eliminar un auto
        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return false;

            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
