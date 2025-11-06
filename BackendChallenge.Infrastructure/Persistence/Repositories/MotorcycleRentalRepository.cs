using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class MotorcycleRentalRepository : IMotorcycleRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleRentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MotorcycleRental rental)
        {
            await _context.MotorcycleRentals.AddAsync(rental);
        }
    }
}
