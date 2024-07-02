using Microsoft.AspNetCore.Mvc;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Dtos;

namespace ShiftLoggerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;

        public ShiftController(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<IActionResult> GetShiftsAsync()
        {
            var shifts = await _dataAccess.GetShiftsAsync();

            return Ok(shifts);
        }

        // GET: api/Shift/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetShiftByIdAsync(int id)
        {
            var shift = await _dataAccess.GetShiftByIdAsync(id);

            if (shift is null)
                return NotFound();

            return Ok(shift);
        }

        // POST: api/Shift
        [HttpPost]
        public async Task<IActionResult> PostShiftAsync([FromBody] ShiftWriteDto shiftDto)
        {
            var shiftReadDto = await _dataAccess.AddShiftAsync(shiftDto);

            return CreatedAtRoute("Get", new { shiftReadDto.Id }, shiftReadDto);
        }

        // PUT: api/Shift/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftAsync(int id, [FromBody] ShiftUpdateDto shift)
        {
            var result = await _dataAccess.UpdateShiftAsync(id, shift);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftAsync(int id)
        {
            var result = await _dataAccess.DeleteShiftAsync(id);
            if (result == -1)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}