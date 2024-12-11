using DVD_RENTAL_API.DTOs;
using DVD_RENTAL_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVD_RENTAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        // Add DVD
        [HttpPost("AddDVD")]
        public async Task<IActionResult> AddDVD([FromForm] ManagerRequestModel managerRequestModel)
        {
            try
            {
                var result = await _managerService.AddDVDAsync(managerRequestModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get DVD by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDVDById(int id)
        {
            try
            {
                var result = await _managerService.GetDVDByIdAsync(id);
                if (result == null)
                    return NotFound("DVD not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get All DVDs
        [HttpGet("GetAllDVDs")]
        public async Task<IActionResult> GetAllDVDs()
        {
            try
            {
                var result = await _managerService.GetAllDVDsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Partial Update DVD by Id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDVD(int id, [FromForm] ManagerRequestModel managerRequestModel)
        {
            try
            {
                var result = await _managerService.UpdateDVDAsync(id, managerRequestModel);
                if (result == null)
                    return NotFound("DVD not found or no changes were made.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete DVD
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDVD(int id)
        {
            try
            {
                var result = await _managerService.DeleteDVDAsync(id);
                if (result == null)
                    return NotFound("DVD not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

