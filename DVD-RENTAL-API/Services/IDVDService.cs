using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Services
{
    public interface IDVDService
    {
        Task<IEnumerable<DvdDto>> GetAllAsync();
        Task<DvdDto> GetByIdAsync(int id);
        Task AddAsync(CreateDvdDto dto);
        Task UpdateAsync(int id, UpdateDvdDto dto);
        Task DeleteAsync(int id);
    }
}
