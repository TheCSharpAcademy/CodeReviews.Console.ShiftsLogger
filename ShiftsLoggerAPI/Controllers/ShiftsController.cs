using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Models;
using SharedLibrary.Validations;
using ShiftsLoggerAPI.Interfaces;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _service;

        public ShiftsController(IShiftService service)
        {
            _service = service;
        }

        // GET: api/Shifts
        [HttpGet]
        public ActionResult<IEnumerable<Shift>> GetAllShifts()
        {
            return _service.GetAllShifts();
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public ActionResult<Shift> GetShift(int id)
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
        public ActionResult UpdateShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.UpdateShift(shift);
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
        public ActionResult<Shift> CreateShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.CreateShift(shift);
                return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
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
