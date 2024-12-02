using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(int id);
        Task AddAsync(Notification notification);
        Task DeleteAsync(int id);
    }
}
