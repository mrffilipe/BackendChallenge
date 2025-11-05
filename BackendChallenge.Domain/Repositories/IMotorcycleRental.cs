using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Repositories
{
    public interface IMotorcycleRental
    {
        Task AddAsync(MotorcycleRental rental);
    }
}
