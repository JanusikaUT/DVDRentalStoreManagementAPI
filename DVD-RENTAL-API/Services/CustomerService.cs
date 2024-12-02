using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVD_RENTAL_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                NIC = c.NIC,
                Phone = c.Phone,
                Address = c.Address
            });
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                NIC = customer.NIC,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task AddAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                NIC = dto.NIC,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _repository.AddAsync(customer);
        }

        public async Task UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
