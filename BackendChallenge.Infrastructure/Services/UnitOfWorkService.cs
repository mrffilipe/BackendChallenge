using BackendChallenge.Application.Services;
using BackendChallenge.Infrastructure.Persistence;

namespace BackendChallenge.Infrastructure.Services
{
    public class UnitOfWorkService : IUnityOfWorkService
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWorkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
