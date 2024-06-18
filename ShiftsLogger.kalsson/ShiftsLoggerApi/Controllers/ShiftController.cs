using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.DataAccess;
using ShiftsLoggerApi.Models;

namespace ShiftsLoggerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftContext _context;

        public ShiftController(ShiftContext context)
        {
            _context = context;
        }

        // GET: api/Shift
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftModel>>> GetShiftModels()
        {
            return await _context.ShiftModels.ToListAsync();
        }

        // GET: api/Shift/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftModel>> GetShiftModel(int id)
        {
            var shiftModel = await _context.ShiftModels.FindAsync(id);

            if (shiftModel == null)
            {
                return NotFound();
            }

            return shiftModel;
        }

        // PUT: api/Shift/5
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

        // POST: api/Shift
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftModel>> PostShiftModel(ShiftModel shiftModel)
        {
            _context.ShiftModels.Add(shiftModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftModel", new { id = shiftModel.Id }, shiftModel);
        }

        // DELETE: api/Shift/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftModel(int id)
        {
            var shiftModel = await _context.ShiftModels.FindAsync(id);
            if (shiftModel == null)
            {
                return NotFound();
            }

            _context.ShiftModels.Remove(shiftModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftModelExists(int id)
        {
            return _context.ShiftModels.Any(e => e.Id == id);
        }
    }
}
