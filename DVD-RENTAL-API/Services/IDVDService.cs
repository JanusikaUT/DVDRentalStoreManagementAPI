using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Services
{
    public interface IDVDService
    {
        Task<IEnumerable<DVD>> GetAllAsync();
        Task<DVD> GetByIdAsync(int id);
        Task AddAsync(DVD dvd);
        Task UpdateAsync(int id, DVD dvd);
        Task DeleteAsync(int id);
    }
}
