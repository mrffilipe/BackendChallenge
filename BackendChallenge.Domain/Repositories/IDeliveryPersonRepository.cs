using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Repositories
{
    public interface IDeliveryPersonRepository
    {
        Task AddAsync(DeliveryPerson person);
    }
}
