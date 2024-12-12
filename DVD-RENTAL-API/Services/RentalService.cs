using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Repositories;

namespace DVD_RENTAL_API.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<RentalResponseDto> GetRentalById(int id)
        {
            var response = await _rentalRepository.GetRentalById(id);
            if (response == null)
            {
                return null; // or return a default response instead
            }
            var rent = new RentalResponseDto()
            {

                RentalId = response.Id,
                CustomerId = response.CustomerId,
                DVDId = response.DVDId,
                RentalDate = response.RentalDate,
                ReturnDate = response.ReturnDate,
                IsOverdue = response.IsOverdue,
                status = response.status



            };
            return rent;


        }


        public async Task<List<RentalResponseDto>> GetAllRentalByCustomerId(int id)
        {

            var rentlist = await _rentalRepository.GetAllRentalByCustomerId(id);

            var response = new List<RentalResponseDto>();

            foreach (var Data in rentlist)
            {

                var rent = new RentalResponseDto
                {
                    RentalId = Data.Id,
                    CustomerId = Data.CustomerId,
                    DVDId = Data.DVDId,
                    RentalDate = Data.RentalDate,
                    ReturnDate = Data.ReturnDate,
                    IsOverdue = Data.IsOverdue,
                    status = Data.status

                };


                response.Add(rent);


            }
            return response;

        }

        public async Task<List<Rental>> GetAllRentalsWithDetailsAsync()
        {
            return await _rentalRepository.GetAllRentalsWithDetailsAsync();
        }

        //public async Task<List<RentalResponseModel>> GetAllRental()
        //{

        //    var rentlist = await _rentalRepository.GetAllRental();


        //    var response = new List<RentalResponseModel>();



        //    foreach (var data in rentlist)
        //    {
        //        var rentalresponse = new RentalResponseModel()
        //        {

        //            RentalId = data.RentalId,
        //            DVDId = data.DVDId,
        //            CustomerId = data.CustomerId,
        //            RentalDate = data.RentalDate,
        //            ReturnDate = data.ReturnDate,
        //            Status = data.Status,
        //            Isoverdue = data.Isoverdue ?? false


        //        };

        //        response.Add(rentalresponse);


        //    }

        //    return response;


        //}

        public async Task<List<RentalResponseDto>> GetAllRental()
        {
            // Fetch data from repository
            var rentList = await _rentalRepository.GetAllRentalsWithDetailsAsync();

            // Return an empty list if no rentals exist
            if (rentList == null || !rentList.Any())
            {
                return new List<RentalResponseDto>();
            }

            // Use LINQ to map entities to response models
            var response = rentList.Select(data => new RentalResponseDto
            {
                RentalId = data.Id,
                DVDId = data.DVDId,
                CustomerId = data.CustomerId,
                RentalDate = data.RentalDate,
                ReturnDate = data.ReturnDate,
                status = data.status,
                IsOverdue= data.IsOverdue,
            }).ToList();


            return response;
        }



        public async Task<RentalResponseDto> AddRental(RentalRequestDto rentalRequestDto)
        {

            var rentalDate = DateTime.Now;

            var returnDate = rentalDate.AddDays(7);


            var request = new Rental()
            {
                CustomerId = rentalRequestDto.CustomerId,
                DVDId=rentalRequestDto.DVDId,
                RentalDate=rentalDate,
                ReturnDate = returnDate,
                
            };


            var responselist = await _rentalRepository.AddRental(request);


            var rent = new RentalResponseDto()
            {
                RentalId = responselist.Id,
                CustomerId = responselist.CustomerId,
                DVDId = responselist.DVDId,
                RentalDate = responselist.RentalDate,
                ReturnDate = responselist.ReturnDate,
                status = responselist.status,
                IsOverdue = responselist.IsOverdue
            };

            return rent;
        }




        public async Task<RentalResponseDto> RentalAccept(int id)
        {
            var rentData = await _rentalRepository.GetRentalById(id);

            if (rentData == null || rentData.status != "pending")
            {
                return null; // Either rental not found or not pending
            }

            // Accept the rental and update CopiesAvailable
            var updatedRental = await _rentalRepository.RentalAccept(rentData.Id);

            // Return the updated rental response
            var response = new RentalResponseDto
            {
                RentalId = updatedRental.Id,
                CustomerId = updatedRental.CustomerId,
                DVDId = updatedRental.DVDId,
                RentalDate = updatedRental.RentalDate,
                ReturnDate = updatedRental.ReturnDate,
                status = updatedRental.status,
                IsOverdue = updatedRental.IsOverdue
            };

            return response;
        }






        public async Task<RentalResponseDto> ReturnRental(int rentalId)
        {
            // Fetch the rental
            var rental = await _rentalRepository.GetRentalById(rentalId);
            if (rental == null || rental.status != "Rent")
            {
                return null; // Rental not found or already returned
            }

            // Update rental status
            rental.status = "Returned";
            await _rentalRepository.UpdateRental(rental);

            // Increment DVD quantity
            await _rentalRepository.IncrementDVDQuantity(rental.DVDId);

            // Map and return the response
            return new RentalResponseDto
            {
                RentalId = rental.Id,
                CustomerId = rental.CustomerId,
                DVDId = rental.DVDId,
                RentalDate = rental.RentalDate,
                ReturnDate = DateTime.Now,
                status = rental.status,
                IsOverdue = false // Assuming overdue is reset when returned
            };
        }











        public async Task<RentalResponseDto> RejectRental(int id)
        {
            // Retrieve the rental details
            var rental = await _rentalRepository.GetRentalById(id);

            if (rental == null || rental.status != "Pending")
                return null; // Rental does not exist or has already been processed

            // Reject the rental and update the status
            var updatedRental = await _rentalRepository.RejectRental(rental);

            // Map and return the response model
            return new RentalResponseDto
            {
                RentalId = updatedRental.Id,
                CustomerId = updatedRental.CustomerId,
                DVDId = updatedRental.DVDId,
                RentalDate = updatedRental.RentalDate,
                ReturnDate = updatedRental.ReturnDate,
                status = updatedRental.status,
                IsOverdue = updatedRental.IsOverdue
            };
        }










        public async Task<List<RentalResponseDto>> CheckAndUpdateOverdueRentals() //check this code 
        {

            var RentRes = await _rentalRepository.CheckAndUpdateOverdueRentals();


            var data = new List<RentalResponseDto>();

            foreach (var rentdata in RentRes)
            {
                var RentalMap = new RentalResponseDto
                {
                    RentalId = rentdata.Id,
                    CustomerId = rentdata.CustomerId,
                    DVDId = rentdata.DVDId,
                    RentalDate = rentdata.RentalDate,
                    ReturnDate = rentdata.ReturnDate,
                    status = rentdata.status,
                    IsOverdue = rentdata.IsOverdue
                };
                data.Add(RentalMap);
            }

            return data;
        }


        public async Task<int> GetAcceptedRentalsCount()
        {
            return await _rentalRepository.GetAcceptedRentalsCount();
        }

        public async Task<int> GetReturnedRentalsCount()
        {
            return await _rentalRepository.GetReturnedRentalsCount();
        }

        public async Task<int> GetRejectedRentalsCount()
        {
            return await _rentalRepository.GetRejectedRentalsCount();
        }


        public async Task<List<RentalResponseDto>> GetRentalsByCustomer(int customerId)
        {
            var rentals = await _rentalRepository.GetRentalsByCustomerId(customerId);

            if (rentals == null || !rentals.Any())
            {
                return null;
            }

            return rentals.Select(r => new RentalResponseDto
            {
                RentalId = r.Id,
                CustomerId = r.CustomerId,
                DVDId = r.DVDId,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                status = r.status,
                IsOverdue = r.IsOverdue
            }).ToList();
        }
    }
}

