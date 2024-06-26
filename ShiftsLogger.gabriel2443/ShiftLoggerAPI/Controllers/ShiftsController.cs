using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerAPI.Data;
using ShiftLoggerAPI.Models;

namespace ShiftLoggerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
    private readonly DataContext _context;

    public ShiftsController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Shift>> PostShift(Shift shift)
    {
        shift.Duration = shift.EndTime - shift.StartTime;
        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
        var shifts = await _context.Shifts.ToListAsync();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null) return NotFound("Shift not found");
        return Ok(shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShifts(int id, Shift updatedShift)
    {
        var shiftDb = await _context.Shifts.FindAsync(id);
        if (id != updatedShift.Id) return BadRequest("Shift id does not exist.");

        if (shiftDb == null) return NotFound("Shift not found");

        shiftDb.FullName = updatedShift.FullName;
        shiftDb.StartTime = updatedShift.StartTime;
        shiftDb.EndTime = updatedShift.EndTime;
        shiftDb.Duration = updatedShift.EndTime - updatedShift.StartTime;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shiftDb = await _context.Shifts.FindAsync(id);
        if (shiftDb == null) return NotFound("Shift not found");

        _context.Shifts.Remove(shiftDb);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}