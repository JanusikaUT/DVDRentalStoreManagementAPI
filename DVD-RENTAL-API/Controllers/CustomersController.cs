using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVD_RENTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Get all customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // Get customer by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found.");
            return Ok(customer);
        }

        // Add a new customer
        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // Update an existing customer
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null) return NotFound("Customer not found.");

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.NIC = updatedCustomer.NIC;
            existingCustomer.Phone = updatedCustomer.Phone;
            existingCustomer.Address = updatedCustomer.Address;

            await _customerService.UpdateCustomerAsync(existingCustomer);
            return NoContent();
        }

        // Delete a customer
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found.");

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}

