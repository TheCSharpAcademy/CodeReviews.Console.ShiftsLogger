using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Services;

namespace ShiftsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftContext _context;
        public readonly ShiftService _shiftService;

        public ShiftsController(ShiftContext context, ShiftService shiftService)
        {
            _context = context;
            _shiftService = shiftService;
        }

        /// <summary>
        /// Return a list of Shifts
        /// </summary>
        /// <returns> A list of Shifts </returns>
        /// <remarks>
        /// 
        /// Sample request
        /// GET: /api/shift
        /// 
        /// </remarks>
        /// <response code="200">Return a list of Shifts</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShifts()
        {
            var shifts = await _shiftService.GetShifts();
            return new ActionResult<IEnumerable<ShiftDto>>(shifts);
        }

        /// <summary>
        /// Return a Shift
        /// </summary>
        /// <param name="id">The unique identifier of the shift to be retrieved.</param>
        /// <returns> Shift </returns>
        /// <remarks>
        /// 
        /// Sample request
        /// GET: api/Shifts/5
        /// 
        /// </remarks>
        /// <response code="200">Return a Shift</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ShiftDto>> GetShift(int id)
        {
            var shift = await _shiftService.GetShiftById(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        /// <summary>
        /// Updates a Shift
        /// </summary>
        /// <param name="id">The unique identifier of the shift to be updated.</param>
        /// <returns> No Content if it's successful </returns>
        /// <remarks>
        /// 
        /// Sample request
        /// PUT: api/Shifts/5
        /// 
        /// </remarks>
        /// <response code="204">Return No Content if it's successful</response>
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutShift(int id, Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return BadRequest();
            }

            try
            {
                await _shiftService.UpdateShift(shift);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a Shift
        /// </summary>
        /// <returns> Created Shift </returns>
        /// <remarks>
        /// 
        /// Sample request
        /// POST: api/Shifts
        /// 
        /// </remarks>
        /// <response code="201">Return Created Shift</response>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Shift>> PostShift(Shift shift)
        {
            await _shiftService.InsertShift(shift);

            return CreatedAtAction(nameof(GetShift), new { id = shift.ShiftId }, shift);
        }

        /// <summary>
        /// Deletes a Shift
        /// </summary>
        /// <param name="id">The unique identifier of the shift to be deleted.</param>
        /// <returns> No Content if it's successful </returns>
        /// <remarks>
        /// 
        /// Sample request
        /// DELETE: api/Shifts/5
        /// 
        /// </remarks>
        /// <response code="200">Return No Content is it's successful</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShift(int id)
        {
            await _shiftService.DeleteShiftById(id);

            return NoContent();
        }
    }
}
