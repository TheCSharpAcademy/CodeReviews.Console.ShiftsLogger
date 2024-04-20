using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Models;
using System.Data;

namespace ShiftsLoggerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShiftsController : ControllerBase
{
  private readonly ShiftsContext _context;

  public ShiftsController(ShiftsContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Shift>>> GetShifts()
  {
    if (_context.Shifts == null) return NotFound();

    return await _context.Shifts.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Shift>> GetShift(int id)
  {
    if (_context.Shifts == null) return NotFound();
    Shift? shift = await _context.Shifts.FindAsync(id);
    if (shift == null) return NotFound();

    return shift;
  }

  [HttpPost]
  public async Task<ActionResult<Shift>> AddShift(Shift shift)
  {
    _context.Shifts.Add(shift);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DBConcurrencyException ex)
    {
      return NotFound(ex);
    }

    return CreatedAtAction(nameof(AddShift), new { id = shift.ShiftId, emloyeeName = shift.EmployeeName, startDate = shift.StartDate, endDate = shift.EndDate });
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Shift>> UpdateShift(int id, Shift shift)
  {
    if (shift.ShiftId != id) return NotFound();

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

  [HttpDelete("{id}")]
  public async Task<ActionResult<Shift>> DeleteShift(int id)
  {
    if (_context.Shifts == null) return NotFound();

    Shift? shift = _context.Shifts.Where(shift => shift.ShiftId == id).FirstOrDefault();

    if (shift == null) return NotFound();

    _context.Shifts.Remove(shift);

    await _context.SaveChangesAsync();

    return NoContent();
  }
}