using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DVD_Rental_API.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DVDContext _context;

        public NotificationRepository(DVDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications.Include(n => n.Rental).ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.Rental)
                .FirstOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
