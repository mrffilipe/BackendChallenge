using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Application.UseCases
{
    public interface IMotorcycleRentalQueryRepository
    {
        Task<MotorcycleRental> GetByExternalIdAsync(string externalId);
    }
}
