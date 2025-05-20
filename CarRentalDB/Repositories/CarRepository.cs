using CarRentalDB.Repositories.Interfaces;
using CarRentalDB.Services;
using CarRentalModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDB.Repositories;

public class CarRepository : ICarRepository
{
    private readonly CarRentalContext _context;
    public CarRepository(CarRentalContext context)
    {
        _context = context;
    }
    public async Task<(IEnumerable<Car>, PaginationMetaData)> GetAllAsync(string? fitlerQuery, string? searchQuery, int pageNumber, int pageSize)
    {
        var collection = _context.Cars as IQueryable<Car>;
        if (!string.IsNullOrEmpty(searchQuery))
        {
            collection = collection.Where(c => c.Model.Contains(searchQuery));
        }
        if (!string.IsNullOrEmpty(fitlerQuery))
        {
            collection = collection.Where(c => c.Color.Contains(fitlerQuery) || c.Model.Contains(fitlerQuery));
        }
        var totalItemAmount = await _context.Cars.CountAsync();
        var paginationMetaData = new PaginationMetaData(totalItemAmount, pageSize, pageSize);
        var collectionToReturn = await collection
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (collectionToReturn, paginationMetaData);
    }


    public async Task<Car?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task AddAsync(Car entity)
    {
        try
        {
            await _context.Cars.AddAsync(entity);
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
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
            if (car != null)
            {
                _context.Cars.Remove(car);
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
}