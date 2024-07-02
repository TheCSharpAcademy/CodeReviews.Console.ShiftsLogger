using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController(IShiftService service) : ControllerBase
    {
        private readonly IShiftService _service = service;

        // GET: api/Shifts
        [HttpGet]
        public ActionResult<IEnumerable<ShiftDto>> GetAllShifts()
        {
            return _service.GetAllShifts();
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public ActionResult<ShiftDto> GetShift(int id)
        {
            var shift = _service.GetShift(id);

            if (shift == null)
            {
                return NotFound();
            }

            return shift;
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult UpdateShift([FromBody] UpdateShiftDto shift, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.UpdateShift(shift, id);
                return NoContent();
            }
            catch (ShiftValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<CreateShiftDto> CreateShift([FromBody] CreateShiftDto shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.CreateShift(shift);
                return Ok(shift);
            }
            catch (ShiftValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public ActionResult DeleteShift(int id)
        {
            var shift = _service.GetShift(id);
            if (shift == null)
            {
                return NotFound();
            }

            _service.DeleteShift(shift);

            return NoContent();
        }
    }
}
