using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DVDContext _context;

        public UserRepository(DVDContext context)
        {
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> RegisterAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
