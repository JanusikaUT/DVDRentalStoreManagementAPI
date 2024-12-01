using DVD_RENTAL_API.Models;
using DVD_RENTAL_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVD_RENTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DVDsController : ControllerBase
    {
        private readonly IDVDService _service;

        public DVDsController(IDVDService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dvds = await _service.GetAllAsync();
            return Ok(dvds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var dvd = await _service.GetByIdAsync(id);
                return Ok(dvd);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "DVD not found" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DVD dvd)
        {
            await _service.AddAsync(dvd);
            return CreatedAtAction(nameof(GetById), new { id = dvd.Id }, dvd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DVD dvd)
        {
            try
            {
                await _service.UpdateAsync(id, dvd);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "DVD not found" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "DVD not found" });
            }
        }
    }

}

