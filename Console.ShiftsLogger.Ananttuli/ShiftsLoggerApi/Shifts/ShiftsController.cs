using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerApi.Common;
using ShiftsLoggerApi.Shifts.Models;

namespace ShiftsLoggerApi.Shifts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : CustomController
    {
        private readonly ShiftsService ShiftsService;

        public ShiftsController(ShiftsService shiftsService)
        {
            ShiftsService = shiftsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShifts()
        {
            var (shifts, error) = await ShiftsService.GetShifts();

            if (error == null && shifts != null)
            {
                return Ok(shifts);
            }

            return this.ErrorResponse(error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDto>> GetShift(int id)
        {
            var (shift, error) = await ShiftsService.GetShift(id);

            if (error == null && shift != null)
            {
                return Ok(shift);
            }

            return this.ErrorResponse(error);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShiftDto>> PutShift(int id, ShiftUpdateDto shiftUpdateDto)
        {
            var (updatedShift, error) = await ShiftsService.UpdateShift(id, shiftUpdateDto);

            if (error == null && updatedShift != null)
            {
                return Ok(updatedShift);
            }

            return this.ErrorResponse(error);

        }

        [HttpPost]
        public async Task<ActionResult<ShiftDto>> PostShift(ShiftCreateDto shiftCreateDto)
        {
            var (createdShift, error) = await ShiftsService.CreateShift(shiftCreateDto);

            if (error == null && createdShift != null)
            {
                return CreatedAtAction("GetShift", new { id = createdShift.ShiftId }, createdShift);
            }

            return this.ErrorResponse(error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            var (deletedId, error) = await ShiftsService.DeleteShift(id);

            if (error == null && deletedId != null)
            {
                return Ok(deletedId);
            }

            return this.ErrorResponse(error);
        }
    }
}
