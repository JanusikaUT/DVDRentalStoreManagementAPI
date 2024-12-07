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
        private readonly IRentalService _service;

        public RentalsController(IRentalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRentalDto dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Rental created successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
