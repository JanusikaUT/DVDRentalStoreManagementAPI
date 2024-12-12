using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DVD_RENTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Manager,Customer")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }



        [HttpGet("GetRentalById")]

        public async Task<IActionResult> GetRenatlById(int id)
        {
            try
            {
                var rent = await _rentalService.GetRentalById(id);

                if (rent == null)
                {
                    return NotFound("Rental not found");
                }

                return Ok(rent);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }

        }

        [HttpGet("GetAllRentalByCustomerId/{id}")]
        public async Task<IActionResult> GetAllRentalByCustomerId(int id)
        {
            try
            {
                var rent = await _rentalService.GetAllRentalByCustomerId(id);

                if (rent == null || !rent.Any())  // Check for null or empty list
                {
                    return NotFound("Customer ID not found or no rentals found.");
                }

                return Ok(rent);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("GetAllRental")]
        public async Task<IActionResult> GetAllRentals()
        {
            var rentals = await _rentalService.GetAllRentalsWithDetailsAsync();
            var result = rentals.Select(r => new
            {
                RentalId = r.Id,
                RentalDate = r.RentalDate,
                ReturnDate = r.ReturnDate,
                Status = r.status,
                Customer = new
                {
                    r.Customer.Id,
                    r.Customer.Name,
                    r.Customer.Email
                },
                DVD = new
                {
                    r.DVD.Id,
                    r.DVD.Title,
                    r.DVD.Price
                }
            });
            return Ok(result);
        }

        [HttpPost("AddRental")]
        public async Task<IActionResult> AddRental(RentalRequestDto rentalRequestDto)
        {
            try
            {
                var rent = await _rentalService.AddRental(rentalRequestDto);

                if (rent == null)
                {
                    return BadRequest("Error occurred while adding rental.");
                }


                return Ok(rent);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPut("RentalAccept/{id}")]
        public async Task<IActionResult> RentalUpdate(int id)
        {
            try
            {
                var rent = await _rentalService.RentalAccept(id);

                if (rent == null)
                {
                    return NotFound("RentalId not found or not in a pending state");
                }

                return Ok(rent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("RejectRental/{id}")]
        public async Task<IActionResult> RejectRental(int id)
        {
            try
            {
                var result = await _rentalService.RejectRental(id);

                if (result == null)
                {
                    return NotFound(new { message = "Rental ID not found or already processed." });
                }

                // Return a JSON response with a success message
                return Ok(new { message = "Rental rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }





        [HttpPut("CheckAndUpdateOverdueRentals")]
        public async Task<ActionResult<List<int>>> CheckAndUpdateOverdueRentals()
        {
            try
            {
                var rent = await _rentalService.CheckAndUpdateOverdueRentals();

                if (rent == null || !rent.Any())
                {
                    return NotFound("Rental details not Found");
                }

                return Ok(rent);  // Return the list of overdue rentals
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPut("ReturnRental/{rentalId}")]
        public async Task<IActionResult> ReturnRental(int rentalId)
        {
            try
            {
                var result = await _rentalService.ReturnRental(rentalId);

                if (result == null)
                {
                    return NotFound(new { message = "Rental not found or already returned." });
                }

                // Successful return logic
                return Ok(new { message = "DVD returned successfully.", rental = result });
            }
            catch (Exception ex)
            {
                // Capture and return a detailed error response
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }


        [HttpGet("CountAcceptedRentals")]
        public async Task<IActionResult> GetCountOfAcceptedRentals()
        {
            try
            {
                var count = await _rentalService.GetAcceptedRentalsCount();
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }


        [HttpGet("CountReturnedRentals")]
        public async Task<IActionResult> GetCountOfReturnedRentals()
        {
            try
            {
                var count = await _rentalService.GetReturnedRentalsCount();
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }

        [HttpGet("CountRejectedRentals")]
        public async Task<IActionResult> GetCountOfRejectedRentals()
        {
            try
            {
                var count = await _rentalService.GetRejectedRentalsCount();
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }

        [HttpGet("GetRentalsByCustomer/{customerId}")]
        public async Task<IActionResult> GetRentalsByCustomer(int customerId)
        {
            try
            {
                var rentals = await _rentalService.GetRentalsByCustomer(customerId);

                if (rentals == null || !rentals.Any())
                {
                    return NotFound("No rentals found for the provided Customer ID.");
                }

                return Ok(rentals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal Server Error: {ex.Message}" });
            }
        }

    }
}
