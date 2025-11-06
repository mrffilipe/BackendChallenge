using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Application.UseCases
{
    public interface IDeliveryPersonQueryRepository
    {
        Task<DeliveryPerson> GetByExternalIdAsync(string externalId);
    }
}
