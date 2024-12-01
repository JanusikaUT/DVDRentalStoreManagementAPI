using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;

namespace DVD_RENTAL_API.Services
{
    public class DVDService : IDVDService
    {
        private readonly IDVDRepository _repository;

        public DVDService(IDVDRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DVD>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DVD> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(DVD dvd)
        {
            await _repository.AddAsync(dvd);
        }

        public async Task UpdateAsync(int id, DVD dvd)
        {
            var existingDVD = await _repository.GetByIdAsync(id);
            if (existingDVD == null)
            {
                throw new KeyNotFoundException("DVD not found");
            }

            existingDVD.Title = dvd.Title;
            existingDVD.Genre = dvd.Genre;
            existingDVD.Director = dvd.Director;
            existingDVD.ReleaseDate = dvd.ReleaseDate;
            existingDVD.AvailableCopies = dvd.AvailableCopies;

            await _repository.UpdateAsync(existingDVD);
        }

        public async Task DeleteAsync(int id)
        {
            var dvd = await _repository.GetByIdAsync(id);
            if (dvd == null)
            {
                throw new KeyNotFoundException("DVD not found");
            }

            await _repository.DeleteAsync(id);
        }
    }
}

