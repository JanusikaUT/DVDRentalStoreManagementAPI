using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface IDVDRepository
    {
        Task<IEnumerable<DVD>> GetAllAsync();
        Task<DVD> GetByIdAsync(int id);
        Task AddAsync(DVD dvd);
        Task UpdateAsync(DVD dvd);
        Task DeleteAsync(int id);

    }
}
