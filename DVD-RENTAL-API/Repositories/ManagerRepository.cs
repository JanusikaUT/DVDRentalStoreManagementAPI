using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly DVDContext _dbContext;

        public ManagerRepository(DVDContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Add a new DVD
        public async Task<DVD> AddDVDAsync(DVD dvd)
        {
            await _dbContext.DVDs.AddAsync(dvd);
            await _dbContext.SaveChangesAsync();
            return dvd;
        }

        // Get DVD by Id
        public async Task<DVD> GetDVDByIdAsync(int id)
        {
            return await _dbContext.DVDs.FindAsync(id);
        }

        // Get all DVDs
        public async Task<List<DVD>> GetAllDVDsAsync()
        {
            return await _dbContext.DVDs.ToListAsync(); // Corrected method
        }

        // Update DVD
        public async Task<DVD> UpdateDVDAsync(DVD dvd)
        {
            _dbContext.Entry(dvd).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return dvd;
        }

        // Delete DVD
        public async Task<DVD> DeleteDVDAsync(int id)
        {
            var dvd = await _dbContext.DVDs.FindAsync(id);
            if (dvd != null)
            {
                _dbContext.DVDs.Remove(dvd);
                await _dbContext.SaveChangesAsync();
            }
            return dvd;
        }
    }
}

