using DVD_RENTAL_API.DTOs;

namespace DVD_RENTAL_API.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllAsync();
        Task<NotificationDto> GetByIdAsync(int id);
        Task AddAsync(CreateNotificationDto dto);
        Task DeleteAsync(int id);
    }
}
