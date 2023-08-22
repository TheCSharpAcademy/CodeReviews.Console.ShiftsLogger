using Microsoft.AspNetCore.Mvc;

using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private IShiftRepository _repository;

        public ShiftController(IShiftRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Shift
        [HttpGet]
        public ActionResult<IEnumerable<Shift>> GetShifts()
        {
            var shifts = _repository.GetShifts();
            if (shifts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(shifts);
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public ActionResult<Shift> GetShift(int id)
        {
            var shift = _repository.GetShift(id);
            if (shift == null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        // PUT: api/Shift/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutShift(int id, Shift shift)
        {
            var isUpdated = _repository.UpdateShift(id, shift);
            if (!isUpdated)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Shift
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Shift> PostShift(Shift shift)
        {
            var isAdded = _repository.AddShift(shift);
            if (!isAdded)
            {
                return Problem("Entity set 'ShiftsContext.Shifts'  is null.");
            }

            return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShift(int id)
        {
            var isDeleted = _repository.DeleteShift(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
