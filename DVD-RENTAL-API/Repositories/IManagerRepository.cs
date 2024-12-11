using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface IManagerRepository
    {
        Task<DVD> AddDVDAsync(DVD dvd);
        Task<DVD> GetDVDByIdAsync(int id);
        Task<List<DVD>> GetAllDVDsAsync(); // Corrected missing method
        Task<DVD> UpdateDVDAsync(DVD dvd);
        Task<DVD> DeleteDVDAsync(int id);
    }
}
