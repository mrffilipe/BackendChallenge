using BackendChallenge.Application.UseCases;
using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleRentalQueryRepository : IMotorcycleRentalQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleRentalQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MotorcycleRental> GetByExternalIdAsync(string externalId)
        {
            return await _context.MotorcycleRentals.FirstOrDefaultAsync(x => externalId == externalId);
        }
    }
}
