using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Infrastructure.Persistence
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }
    }
}
