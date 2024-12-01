using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Repositories
{
    public class DVDRepository : IDVDRepository
    {
        private readonly DVDContext _context;

        public DVDRepository(DVDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DVD>> GetAllAsync()
        {
            return await _context.DVDs.ToListAsync();
        }

        public async Task<DVD> GetByIdAsync(int id)
        {
            return await _context.DVDs.FindAsync(id);
        }

        public async Task AddAsync(DVD dvd)
        {
            await _context.DVDs.AddAsync(dvd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DVD dvd)
        {
            _context.DVDs.Update(dvd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dvd = await GetByIdAsync(id);
            if (dvd != null)
            {
                _context.DVDs.Remove(dvd);
                await _context.SaveChangesAsync();
            }
        }
    }
}

