using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryPersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(DeliveryPerson person)
        {
            await _context.DeliveryDrivers.AddAsync(person);
        }
    }
}
