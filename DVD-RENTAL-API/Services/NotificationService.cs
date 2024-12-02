using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;
using DVD_RENTAL_API.Services;

namespace DVD_Rental_API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllAsync()
        {
            var notifications = await _repository.GetAllAsync();
            return notifications.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                RentalId = n.RentalId,
                Message = n.Message,
                Timestamp = n.Timestamp
            });
        }

        public async Task<NotificationDto> GetByIdAsync(int id)
        {
            var notification = await _repository.GetByIdAsync(id);
            if (notification == null)
                throw new KeyNotFoundException("Notification not found");

            return new NotificationDto
            {
                NotificationId = notification.NotificationId,
                RentalId = notification.RentalId,
                Message = notification.Message,
                Timestamp = notification.Timestamp
            };
        }

        public async Task AddAsync(CreateNotificationDto dto)
        {
            var notification = new Notification
            {
                RentalId = dto.RentalId,
                Message = dto.Message,
                Timestamp = DateTime.Now
            };

            await _repository.AddAsync(notification);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
