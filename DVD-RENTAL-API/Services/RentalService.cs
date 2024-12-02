using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;

namespace DVD_RENTAL_API.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _repository;

        public RentalService(IRentalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RentalDto>> GetAllAsync()
        {
            var rentals = await _repository.GetAllAsync();
            return rentals.Select(r => new RentalDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer.Name,
                DVDId = r.DVDId,
                DVDTitle = r.DVD.Title,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                IsOverdue = r.ReturnDate == null && r.RentalDate.AddDays(7) < DateTime.Now
            });
        }

        public async Task<RentalDto> GetByIdAsync(int id)
        {
            var rental = await _repository.GetByIdAsync(id);
            if (rental == null) throw new KeyNotFoundException("Rental not found.");

            return new RentalDto
            {
                Id = rental.Id,
                CustomerId = rental.CustomerId,
                CustomerName = rental.Customer.Name,
                DVDId = rental.DVDId,
                DVDTitle = rental.DVD.Title,
                RentalDate = rental.RentalDate,
                ReturnDate = rental.ReturnDate,
                IsOverdue = rental.ReturnDate == null && rental.RentalDate.AddDays(7) < DateTime.Now
            };
        }

        public async Task AddAsync(CreateRentalDto dto)
        {
            var rental = new Rental
            {
                CustomerId = dto.CustomerId,
                DVDId = dto.DVDId,
                RentalDate = dto.RentalDate,
                ReturnDate = dto.ReturnDate
            };

            await _repository.AddAsync(rental);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}

