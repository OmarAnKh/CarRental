using CarRentalModels.Models;

namespace CarRentalDB.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> ValidateCredentialsAsync(string? email, string? password);
    Task<User?> GetUserWithRentsAsync(int userId);
}