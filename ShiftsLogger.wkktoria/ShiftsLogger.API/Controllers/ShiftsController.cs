using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLogger.API.Models;
using ShiftsLogger.API.Models.Contexts;
using ShiftsLogger.API.Models.DTOs;

namespace ShiftsLogger.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly ShiftContext _context;

    public ShiftsController(ShiftContext context)
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
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Shift>> GetShift(long id)
    {
        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null) return NotFound();

        return shift;
    }

    // PUT: api/Shifts/5
    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateShift(long id, ShiftDto shiftDto)
    {
        if (id != shiftDto.Id) return BadRequest();

        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return NotFound();

        shift.WorkerName = shiftDto.WorkerName;
        shift.StartedAt = shiftDto.StartedAt;
        shift.FinishedAt = shiftDto.FinishedAt;
        shift.Duration = (shiftDto.FinishedAt - shiftDto.StartedAt).Duration();

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ShiftExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Shifts
    [HttpPost]
    public async Task<ActionResult<Shift>> CreateShift(ShiftDto shiftDto)
    {
        var shift = new Shift
        {
            WorkerName = shiftDto.WorkerName,
            StartedAt = shiftDto.StartedAt,
            FinishedAt = shiftDto.FinishedAt,
            Duration = (shiftDto.FinishedAt - shiftDto.StartedAt).Duration()
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetShift),
            new { id = shift.Id },
            shift
        );
    }

    // DELETE: api/Shifts/5
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteShift(long id)
    {
        var shift = await _context.Shifts.FindAsync(id);

        if (shift == null) return NotFound();

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ShiftExists(long id)
    {
        return _context.Shifts.Any(shift => shift.Id == id);
    }
}