using CarRentalDB.Repositories.Interfaces;
using CarRentalDB.Services;
using CarRentalModels.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDB.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CarRentalContext _carRentalContext;

    public UserRepository(CarRentalContext carRentalContext)
    {
        _carRentalContext = carRentalContext;
    }
    public async Task<(IEnumerable<User>, PaginationMetaData)> GetAllAsync(string? email, string? searchQuery, int pageNumber, int pageSize)
    {
        try
        {
            IQueryable<User> collection = _carRentalContext.Users;

            if (!string.IsNullOrEmpty(email))
            {
                collection = collection.Where(u => u.Email == email);
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                collection = collection.Where(u => u.FirstName.Contains(searchQuery) || u.LastName.Contains(searchQuery)
                                                                                     || u.Email.Contains(searchQuery)
                                                                                     || u.PhoneNumber.Contains(searchQuery));
            }
            int totalItemCount = await collection.CountAsync();
            PaginationMetaData paginationMetaData = new PaginationMetaData(totalItemCount, pageNumber, pageSize);
            List<User> collectionToReturn = await collection.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (collectionToReturn, paginationMetaData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        try
        {
            return await _carRentalContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task AddAsync(User entity)
    {
        try
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            await _carRentalContext.Users.AddAsync(entity);
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
            User? user = await _carRentalContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
            {
                _carRentalContext.Users.Remove(user);
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
        await _carRentalContext.SaveChangesAsync();
    }

    public async Task<User?> ValidateCredentialsAsync(string? email, string? password)
    {
        var user = await _carRentalContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);
        return passwordMatch ? user : null;
    }
}