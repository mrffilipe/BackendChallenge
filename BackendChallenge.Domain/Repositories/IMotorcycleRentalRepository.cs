using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Repositories
{
    public interface IMotorcycleRentalRepository
    {
        Task AddAsync(MotorcycleRental rental);
    }
}
