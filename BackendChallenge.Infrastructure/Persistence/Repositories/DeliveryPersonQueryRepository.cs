using BackendChallenge.Application.UseCases;
using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class DeliveryPersonQueryRepository : IDeliveryPersonQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryPerson> GetByExternalIdAsync(string externalId)
        {
            return await _context.DeliveryDrivers.FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }
    }
}
