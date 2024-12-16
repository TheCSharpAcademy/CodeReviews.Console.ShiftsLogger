using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Data;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly ShiftLoggerContext _context;

    public ShiftsController(ShiftLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Shifts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
    {
        return await _context.Shifts.ToListAsync();
    }

    // GET: api/Shifts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShift(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null)
        {
            return NotFound();
        }

        return shift;
    }

    // PUT: api/Shifts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutShift()
    {
        var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.End == null);
        if (shift == null)
        {
            return NotFound("No open shifts. Start a new one before ending.");
        }

        shift.End = DateTime.Now;
        shift.Duration = shift.End - shift.Start;

        _context.Entry(shift).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        { 
            throw;
        }

        return NoContent();
    }

    // POST: api/Shifts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(StartShiftDto startShift)
    {

        var openShift = await _context.Shifts.FirstOrDefaultAsync(s => s.End == null);
        if (openShift != null)
        {
            return BadRequest("There is already an open shift. End it before starting a new one.");
        }
        var shift = new Shift
        {
            EmployeeName = startShift.EmployeeName,
            Start = DateTime.Now
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
    }

    // DELETE: api/Shifts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound();
        }

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftExists(int id)
    {
        return _context.Shifts.Any(e => e.Id == id);
    }
}
