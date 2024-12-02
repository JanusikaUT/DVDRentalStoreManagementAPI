using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task AddAsync(CreateCustomerDto dto);
        Task UpdateAsync(int id, UpdateCustomerDto dto);
        Task DeleteAsync(int id);
    }
}
