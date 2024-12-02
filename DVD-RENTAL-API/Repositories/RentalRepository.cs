using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DVDContext _context;

        public RentalRepository(DVDContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .ToListAsync();
        }

        public async Task<Rental> GetByIdAsync(int id)
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rental = await GetByIdAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
                await _context.SaveChangesAsync();
            }
        }
    }
}

