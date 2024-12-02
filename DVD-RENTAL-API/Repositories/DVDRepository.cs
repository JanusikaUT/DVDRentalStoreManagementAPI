using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var dvd = await _context.DVDs.FindAsync(id);
            if (dvd == null)
                throw new KeyNotFoundException($"DVD with ID {id} not found.");
            return dvd;
        }

        public async Task AddAsync(DVD dvd)
        {
            await _context.DVDs.AddAsync(dvd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DVD dvd)
        {
            var existingDvd = await _context.DVDs.AsNoTracking().FirstOrDefaultAsync(d => d.Id == dvd.Id);
            if (existingDvd == null)
                throw new KeyNotFoundException($"DVD with ID {dvd.Id} not found.");

            _context.DVDs.Update(dvd);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dvd = await GetByIdAsync(id);
            _context.DVDs.Remove(dvd);
            await _context.SaveChangesAsync();
        }
    }
}
