using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
    }
}
