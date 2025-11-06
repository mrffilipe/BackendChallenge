using BackendChallenge.Application.UseCases;
using BackendChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleQueryRepository : IMotorcycleQueryRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleQueryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> GetByExternalIdAsync(string externalId)
        {
            return await _context.Motorcycles.FirstOrDefaultAsync(x => x.ExternalId == externalId);
        }

        public async Task<Motorcycle> GetByPlateAsync(string plate)
        {
            return await _context.Motorcycles.FirstOrDefaultAsync(x => x.Plate == plate);
        }
    }
}
