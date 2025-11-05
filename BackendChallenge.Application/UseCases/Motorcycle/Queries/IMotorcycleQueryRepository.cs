using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Application.UseCases
{
    public interface IMotorcycleQueryRepository
    {
        Task<Motorcycle> GetByExternalIdAsync(string externalId);
    }
}
