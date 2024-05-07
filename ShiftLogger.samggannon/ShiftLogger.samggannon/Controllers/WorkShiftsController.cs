using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLogger.samggannon.Models;

namespace ShiftLogger.samggannon.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkShiftsController : ControllerBase
{
    private readonly ShiftContext _context;

    public WorkShiftsController(ShiftContext context)
    {
        _context = context;
    }

    // GET: api/WorkShifts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkShift>>> GetShifts()
    {
        return await _context.Shifts.ToListAsync();
    }

    // GET: api/WorkShifts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkShift>> GetWorkShift(int id)
    {
        var workShift = await _context.Shifts.FindAsync(id);

        if (workShift == null)
        {
            return NotFound();
        }

        return workShift;
    }

    // PUT: api/WorkShifts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkShift(int id, WorkShift workShift)
    {
        if (id != workShift.Id)
        {
            return BadRequest();
        }

        _context.Entry(workShift).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkShiftExists(id))
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

    // POST: api/WorkShifts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkShift>> PostWorkShift(WorkShift workShift)
    {
        _context.Shifts.Add(workShift);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWorkShift", new { id = workShift.Id }, workShift);
    }

    // DELETE: api/WorkShifts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkShift(int id)
    {
        var workShift = await _context.Shifts.FindAsync(id);
        if (workShift == null)
        {
            return NotFound();
        }

        _context.Shifts.Remove(workShift);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkShiftExists(int id)
    {
        return _context.Shifts.Any(e => e.Id == id);
    }
}
