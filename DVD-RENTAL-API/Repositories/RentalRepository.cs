using DVD_RENTAL_API.Data;
using DVD_RENTAL_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DVD_RENTAL_API.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DVDContext _dbContext;

        public RentalRepository(DVDContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Rental> GetRentalById(int id)
        {
            return await _dbContext.Rentals.Include(r => r.Customer).Include(r => r.DVD).FirstOrDefaultAsync(r => r.Id == id);


        }

        public async Task<List<Rental>> GetAllRentalByCustomerId(int id)
        {
            return await _dbContext.Rentals.Where(r => r.CustomerId == id).Include(r => r.Customer).Include(r => r.DVD).ToListAsync();
        }

        public async Task<List<Rental>> GetAllRentalsWithDetailsAsync()
        {
            return await _dbContext.Rentals
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .ToListAsync();
        }


        public async Task<Rental> AddRental(Rental rental)
        {

            await _dbContext.Rentals.AddAsync(rental);
            await _dbContext.SaveChangesAsync();
            return rental;
        }

        //public async Task<Rent> RentalAccept(int rentalid)
        //{
        //    var rental = await GetRentalById(rentalid);
        //    rental.Status = "Rent";
        //    rental.Isoverdue = false;
        //    await _dbContext.SaveChangesAsync();
        //    return rental;
        //}


        public async Task<Rental> RentalAccept(int rentalId)
        {
            // Fetch the rental record
            var rental = await GetRentalById(rentalId);
            if (rental == null) throw new Exception("Rental not found");

            // Update rental status
            rental.status = "Rent";
            rental.IsOverdue = false;

            // Fetch the related DVD and update copies available
            var dvd = await _dbContext.DVDs.FirstOrDefaultAsync(d => d.Id == rental.DVDId);
            if (dvd == null) throw new Exception("DVD not found");

            if (dvd.CopiesAvailable > 0)
            {
                dvd.CopiesAvailable--; // Decrement copies available
            }
            else
            {
                throw new Exception("No copies available for the selected DVD");
            }

            // Save changes to database
            await _dbContext.SaveChangesAsync();

            return rental;
        }

        // Update rental status and save changes
        public async Task<Rental> UpdateRental(Rental rental)
        {
            _dbContext.Rentals.Update(rental);
            await _dbContext.SaveChangesAsync();
            return rental;
        }
        public async Task<int> GetAcceptedRentalsCount()
        {
            return await _dbContext.Rentals.CountAsync(r => r.status == "Rent");
        }


        public async Task<Rental> RejectRental(Rental rental)
        {
            rental.status = "Rejected"; // Only update rental status
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return rental;
        }










        public async Task<List<Rental>> CheckAndUpdateOverdueRentals()
        {

            var overdue = await _dbContext.Rentals.Where(r => r.IsOverdue == true).ToListAsync();
            //foreach (var rent in overdue)
            //{
            //    rent.Isoverdue = true;

            //}

            //await _dbContext.SaveChangesAsync();
            return overdue;


        }

        public async Task<int> GetReturnedRentalsCount()
        {
            return await _dbContext.Rentals.CountAsync(r => r.status == "Returned");
        }

        public async Task<int> GetRejectedRentalsCount()
        {
            return await _dbContext.Rentals.CountAsync(r => r.status == "Rejected");
        }

        public async Task<List<Rental>> GetRentalsByCustomerId(int customerId)
        {
            return await _dbContext.Rentals
                .Include(r => r.DVD)
                .Where(r => r.CustomerId == customerId && r.status == "Rent")
                .ToListAsync();
        }

        public async Task IncrementDVDQuantity(int dvdId)
        {
            var dvd = await _dbContext.DVDs.FindAsync(dvdId);
            if (dvd != null)
            {
                dvd.CopiesAvailable += 1;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

