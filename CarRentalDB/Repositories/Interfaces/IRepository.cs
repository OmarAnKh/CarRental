using CarRentalDB.Services;

namespace CarRentalDB.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<(IEnumerable<T>, PaginationMetaData)> GetAllAsync(string? fitlerQuery, string? searchQuery, int pageNumber, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}