﻿using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
