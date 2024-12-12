using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;

namespace DVD_RENTAL_API.Services
{
    public interface IRentalService
    {
        Task<RentalResponseDto> GetRentalById(int id);
        Task<List<RentalResponseDto>> GetAllRentalByCustomerId(int id);
        Task<List<Rental>> GetAllRentalsWithDetailsAsync();

        Task<List<RentalResponseDto>> GetAllRental();
        Task<int> GetAcceptedRentalsCount();
        Task<int> GetReturnedRentalsCount();
        Task<int> GetRejectedRentalsCount();
        Task<List<RentalResponseDto>> GetRentalsByCustomer(int customerId);
        Task<RentalResponseDto> AddRental(RentalRequestDto rentalRequestDto);
        Task<RentalResponseDto> RentalAccept(int id);


        Task<RentalResponseDto> RejectRental(int rentalId);
        Task<List<RentalResponseDto>> CheckAndUpdateOverdueRentals();
        Task<RentalResponseDto> ReturnRental(int rentalId);
    }
}
