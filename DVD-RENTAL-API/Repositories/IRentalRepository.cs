using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalById(int id);

        Task<List<Rental>> GetAllRentalByCustomerId(int id);
        Task<List<Rental>> GetAllRentalsWithDetailsAsync();
        Task<int> GetAcceptedRentalsCount();
        Task<int> GetReturnedRentalsCount();
        Task<int> GetRejectedRentalsCount();
        Task IncrementDVDQuantity(int dvdId);
        Task<List<Rental>> GetRentalsByCustomerId(int customerId);
        Task<Rental> AddRental(Rental rental);
        Task<Rental> RentalAccept(int rental);
        Task<Rental> RejectRental(Rental rental);
        Task<List<Rental>> CheckAndUpdateOverdueRentals();
        Task<Rental> UpdateRental(Rental rental);
    }
}
