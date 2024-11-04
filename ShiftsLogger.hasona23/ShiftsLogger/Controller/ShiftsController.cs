using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerAPI.Services;

namespace ShiftsLoggerAPI.Controller
{
    
    [ApiController,Route("api/[controller]")]
    public class ShiftsController:ControllerBase
    {
        private readonly IShiftsService _shiftsService;

        public ShiftsController(IShiftsService shiftsService)
        {
            _shiftsService = shiftsService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllShifts()
        {
            var shifts = await _shiftsService.GetAllShiftsAsync();
            return Ok(shifts);
        }
        [HttpPost]
        public async Task<IActionResult> AddShiftAsync([FromBody] ShiftCreate shift)
        {
            if (shift == null)
            {
                return BadRequest("Shift data is null.");
            }

           bool success = await _shiftsService.AddShiftAsync(shift);

            // Optionally return CreatedAtAction to return the created worker's URL
            return success?Created():BadRequest($"Worker id {shift.WorkerId} wasnt found");

        }
        [HttpGet("{workerId}")]
        public async Task<IActionResult> GetShiftsAsync(int workerId)
        {
            var shifts = await _shiftsService.GetShiftsAsync(workerId);
            return Ok(shifts);
        }
   
        [HttpPut]
        public async Task<IActionResult> UpdateShiftAsync([FromBody] ShiftUpdate newShift)
        {
            bool success = await _shiftsService.UpdateShiftAsync(newShift);
            return success ? NoContent() : NotFound($"Couldnt Find Shift of ID: {newShift.Id} to update");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            bool success = await _shiftsService.RemoveShiftAsync(id);
            return success ? NoContent() : NotFound($"Couldnt Find Shift of ID: {id} to update");
        }
    }
}
