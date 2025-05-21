using CarRentalDB.Repositories.Interfaces;
using CarRentalDB.Services;
using CarRentalModels.Enums;
using CarRentalModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDB.Repositories;

public class RentRepository : IRentRepository
{
    private readonly CarRentalContext _context;
    public RentRepository(CarRentalContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Rent>, PaginationMetaData)> GetAllAsync(string? fitlerQuery, string? searchQuery, int pageNumber, int pageSize)
    {
        var collection = _context.Rents as IQueryable<Rent>;
        if (!string.IsNullOrEmpty(searchQuery))
        {
            collection = collection.Where(r => r.RentDate.ToString() == searchQuery);
        }
        var totalItemAmount = await collection.CountAsync();
        var paginationMetaData = new PaginationMetaData(totalItemAmount, pageNumber, pageSize);
        var collectionToReturn = await collection
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (collectionToReturn, paginationMetaData);
    }
    public async Task<Rent?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Rents.FirstOrDefaultAsync(r => r.RentId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<bool> AddAsync(Rent entity)
    {
        try
        {
            var carAvailable = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == entity.CarId);
            if (carAvailable == null || carAvailable?.Availability == CarAvailability.Unavailable)
            {
                return false;
            }
            await ChangeCarAvailabilityAsync(carAvailable!, CarAvailability.Unavailable);
            await _context.Rents.AddAsync(entity);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task DeleteAsync(int id)
    {
        try
        {
            var rentToDelete = await _context.Rents.FirstOrDefaultAsync(r => r.RentId == id);
            if (rentToDelete != null)
            {
                _context.Rents.Remove(rentToDelete);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task<bool> CancelRent(int rentId, int userId)
    {
        var rentToCancel = await _context.Rents.FirstOrDefaultAsync(r => r.RentId == rentId);
        if (rentToCancel == null)
        {
            return false;
        }
        if (rentToCancel.UserId != userId)
        {
            return false;
        }
        var carAvailable = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == rentToCancel.CarId);
        if (carAvailable == null)
        {
            return false;
        }
        await ChangeCarAvailabilityAsync(carAvailable!, CarAvailability.Available);
        return true;
    }

    private async Task ChangeCarAvailabilityAsync(Car car, CarAvailability availability)
    {
        car.Availability = availability;
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }
}