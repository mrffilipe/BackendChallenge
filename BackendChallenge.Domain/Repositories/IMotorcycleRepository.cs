using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Repositories
{
    public interface IMotorcycleRepository
    {
        Task AddAsync(Motorcycle motorcycle);
        void Remove(Motorcycle motorcycle);
    }
}
