using CarRentalModels.Models;

namespace CarRentalDB.Repositories.Interfaces;

public interface IRentRepository : IRepository<Rent>
{
    Task<bool> CancelRent(int rentId, int userId);
}