using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Model;

namespace ShiftsLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftItemsController : ControllerBase
    {
        private readonly ShiftLoggerContext _context;

        public ShiftItemsController(ShiftLoggerContext context)
        {
            _context = context;
        }

        // GET: api/ShiftItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShiftItem>>> GetShiftItems()
        {
          if (_context.ShiftItems == null)
          {
              return NotFound();
          }
            return await _context.ShiftItems.ToListAsync();
        }

        // GET: api/ShiftItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftItem>> GetShiftItem(long id)
        {
          if (_context.ShiftItems == null)
          {
              return NotFound();
          }
            var shiftItem = await _context.ShiftItems.FindAsync(id);

            if (shiftItem == null)
            {
                return NotFound();
            }

            return shiftItem;
        }

        // PUT: api/ShiftItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShiftItem(long id, ShiftItem shiftItem)
        {
            if (id != shiftItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shiftItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShiftItemExists(id))
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

        // POST: api/ShiftItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShiftItem>> PostShiftItem(ShiftItem shiftItem)
        {
          if (_context.ShiftItems == null)
          {
              return Problem("Entity set 'ShiftLoggerContext.ShiftItems'  is null.");
          }
            _context.ShiftItems.Add(shiftItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShiftItem", new { id = shiftItem.Id }, shiftItem);
        }

        // DELETE: api/ShiftItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShiftItem(long id)
        {
            if (_context.ShiftItems == null)
            {
                return NotFound();
            }
            var shiftItem = await _context.ShiftItems.FindAsync(id);
            if (shiftItem == null)
            {
                return NotFound();
            }

            _context.ShiftItems.Remove(shiftItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShiftItemExists(long id)
        {
            return (_context.ShiftItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
