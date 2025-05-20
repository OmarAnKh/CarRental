using CarRentalModels.Models;

namespace CarRentalDB.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> ValidateCredentialsAsync(string? email, string? password);
}