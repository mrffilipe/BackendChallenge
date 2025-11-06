using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
        }

        public void Remove(Motorcycle motorcycle)
        {
            _context.Motorcycles.Remove(motorcycle);
        }
    }
}
