using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> RegisterAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}
