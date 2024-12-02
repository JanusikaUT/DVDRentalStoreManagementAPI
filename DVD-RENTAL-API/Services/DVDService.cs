using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVD_RENTAL_API.Services
{
    public class DVDService : IDVDService
    {
        private readonly IDVDRepository _repository;

        public DVDService(IDVDRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DvdDto>> GetAllAsync()
        {
            var dvds = await _repository.GetAllAsync();
            return dvds.Select(d => new DvdDto
            {
                Id = d.Id,
                Title = d.Title,
                Genre = d.Genre,
                Director = d.Director,
                ReleaseDate = d.ReleaseDate,
                AvailableCopies = d.AvailableCopies
            });
        }

        public async Task<DvdDto> GetByIdAsync(int id)
        {
            var dvd = await _repository.GetByIdAsync(id);
            return new DvdDto
            {
                Id = dvd.Id,
                Title = dvd.Title,
                Genre = dvd.Genre,
                Director = dvd.Director,
                ReleaseDate = dvd.ReleaseDate,
                AvailableCopies = dvd.AvailableCopies
            };
        }

        public async Task AddAsync(CreateDvdDto dto)
        {
            var dvd = new DVD
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Director = dto.Director,
                ReleaseDate = dto.ReleaseDate,
                AvailableCopies = dto.AvailableCopies
            };
            await _repository.AddAsync(dvd);
        }

        public async Task UpdateAsync(int id, UpdateDvdDto dto)
        {
            var existingDvd = await _repository.GetByIdAsync(id);
            existingDvd.Title = dto.Title;
            existingDvd.Genre = dto.Genre;
            existingDvd.Director = dto.Director;
            existingDvd.ReleaseDate = dto.ReleaseDate;
            existingDvd.AvailableCopies = dto.AvailableCopies;

            await _repository.UpdateAsync(existingDvd);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
