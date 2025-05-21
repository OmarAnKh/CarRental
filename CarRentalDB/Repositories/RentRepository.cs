using CarRentalDB.Repositories.Interfaces;
using CarRentalDB.Services;
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
        var totalItemAmount = await _context.Rents.CountAsync();
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
}