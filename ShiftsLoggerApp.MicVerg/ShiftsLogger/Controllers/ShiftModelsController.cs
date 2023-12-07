using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Models;

namespace ShiftsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftModelsController : ControllerBase
    {
        private readonly ShiftContext _context;

        public ShiftModelsController(ShiftContext context)
        {
            _context = context;
        }

        // GET: api/ShiftModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShifts()
        {
            return await _context.Shifts.ToListAsync();
        }

        // GET: api/ShiftModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
        {
            var shiftModel = await _context.Shifts.FindAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return shiftModel;
        }

        // PUT: api/ShiftModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftModel(int id, ShiftModel shiftModel)
        {
            if (id != shiftModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShiftModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            _context.Shifts.Add(shiftModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShiftModel), new { id = shiftModel.Id }, shiftModel);
        }

        // DELETE: api/ShiftModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(int id)
        {
            var shiftModel = await _context.Shifts.FindAsync(id);
            if (shiftModel == null)
            {
                return NotFound();
            }

            _context.Shifts.Remove(shiftModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftModelExists(int id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }
    }
}
