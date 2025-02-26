using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        private readonly IShiftMapper _shiftMapper;

        public ShiftsController(IShiftService shiftService, IShiftMapper shiftMapper)
        {
            _shiftService = shiftService;
            _shiftMapper = shiftMapper;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftDTO>>> GetShifts()
        {
            var shifts = await _shiftService.GetAllShiftsAsync();

            if (!shifts.Success)
            {
                return BadRequest(shifts.Message);
            }

            var shiftDTOs = shifts?.Data?.Select(s => _shiftMapper.ShiftToDTO(s)).ToList();

            if (shiftDTOs?.Count == 0)
            {
                return Ok(new List<ShiftDTO>());
            }
            return Ok(shiftDTOs);
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> GetShift(long id)
        {
            var shift = await _shiftService.GetShiftByIdAsync(id);

            if (shift.Data == null)
            {
                return NotFound(shift.Message);
            }

            if (!shift.Success)
            {
                return BadRequest(shift.Message);
            }

            var shiftDTO = _shiftMapper.ShiftToDTO(shift.Data);

            return Ok(shiftDTO);
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutShift(long id, ShiftDTO shiftDTO)
        {
            if (id != shiftDTO.ShiftId)
            {
                return BadRequest("Shift ID in URL does not match the request body");
            }
            var shift = await _shiftService.UpdateShift(id, shiftDTO);

            if (shift.Data == null)
            {
                return NotFound(shift.Message);
            }

            if (!shift.Success)
            {
                return BadRequest(shift.Message);
            }

            var updatedShiftDTO = _shiftMapper.ShiftToDTO(shift.Data);

            return Ok(updatedShiftDTO);
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdShift = await _shiftService.CreateShift(shiftDTO);

            if (createdShift.Message == "Employee does not exist")
            {
                return BadRequest(createdShift.Message);
            }

            if (createdShift.Data == null)
            {
                return NotFound(createdShift.Message);
            }

            if (!createdShift.Success)
            {
                return BadRequest(createdShift.Message);
            }

            return CreatedAtAction(
                nameof(GetShift),
                new { id = createdShift.Data.ShiftId },
                _shiftMapper.ShiftToDTO(createdShift.Data));
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(long id)
        {
            var shift = await _shiftService.DeleteShift(id);

            if (!shift.Data)
            {
                return NotFound(shift.Message);
            }

            if (!shift.Success)
            {
                return BadRequest(shift.Message);
            }

            return NoContent();
        }
    }
}
