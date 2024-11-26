using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Model;

namespace ShiftsLoggerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shifts = await context.Shifts.ToListAsync();
        return Ok(shifts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var shift = await context.Shifts.FindAsync(id);

        return shift == null ? NotFound() : Ok(shift);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Shift shift)
    {
        context.Shifts.AddAsync(shift);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = shift.Id }, shift);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Shift shift)
    {
        if (id != shift.Id) return BadRequest();
        context.Entry(shift).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            if (!context.Shifts.Any(s => s.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shift = await context.Shifts.FindAsync(id);
        if (shift == null) return NotFound();
        context.Shifts.Remove(shift);
        await context.SaveChangesAsync();
        return NoContent();
    }
}