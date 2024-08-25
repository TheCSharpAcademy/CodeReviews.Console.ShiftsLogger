using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly ShiftDbContext context;

    public ShiftsController(ShiftDbContext _context)
    {
        context = _context;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, Shift infoShift)
    {
        Shift shift = await context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound("Shift not found.");
        }
        if (infoShift.End <= infoShift.Start)
        {
            return BadRequest("EndTime must be later than StartTime.");
        }
        shift.Start = infoShift.Start;
        shift.End = infoShift.End;
        context.Entry(shift).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftExists(id))
            {
                return NotFound("Shift not found.");
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    private bool ShiftExists(int id)
    {
        return context.Shifts.Any(e => e.Id == id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
    {
        try
        {
            return await context.Shifts.ToListAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Shift>> GetShift(int id)
    {
        var shift = await context.Shifts.FindAsync(id);

        if (shift == null)
        {
            return NotFound();
        }

        return shift;
    }


    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(Shift infoShift)
    {
        Shift shift = new()
        {
            Start = infoShift.Start,
            End = infoShift.End
        };
        context.Shifts.Add(shift);
        await context.SaveChangesAsync();
        return CreatedAtAction("GetShift", new { id = shift.Id }, shift);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shift = await context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound();
        }
        context.Shifts.Remove(shift);
        await context.SaveChangesAsync();
        return NoContent();
    }

}
