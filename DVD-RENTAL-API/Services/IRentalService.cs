using DVD_RENTAL_API.DTOs;

namespace DVD_RENTAL_API.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalDto>> GetAllAsync();
        Task<RentalDto> GetByIdAsync(int id);
        Task AddAsync(CreateRentalDto dto);
        Task DeleteAsync(int id);
    }
}
