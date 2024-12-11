using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetAllAsync();
        Task<Rental> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(int id);
    }
}
