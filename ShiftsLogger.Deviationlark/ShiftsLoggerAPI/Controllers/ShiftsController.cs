using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger;

namespace ShiftsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly Service _service;

        public ShiftsController(Service service)
        {
            _service = service;
        }

        // GET: api/Shifts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShifts()
        {
            var shifts = await _service.GetShiftsAsync();
            return Ok(shifts);
        }

        // GET: api/Shifts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(long id)
        {
            var shiftModel = await _service.GetShiftAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return Ok(shiftModel);
        }

        // PUT: api/Shifts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(long id, ShiftModel shiftModel)
        {
            if (!await _service.PutShiftAsync(id, shiftModel))
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Shifts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            var createdShift = await _service.CreateShiftAsync(shiftModel);

            return CreatedAtAction(nameof(PostShiftModel), new { id = createdShift.Id }, createdShift);
        }

        // DELETE: api/Shifts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(long id)
        {
            if (!await _service.DeleteShiftAsync(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
