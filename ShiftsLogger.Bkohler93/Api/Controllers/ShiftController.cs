using Api.Data.Entities;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController: ControllerBase {
    private readonly AppDbContext db;
    public ShiftController(AppDbContext appDbContext)
    {
        db = appDbContext; 
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetShiftDto>>> GetShifts()
    {
        var shifts = await db.Shifts.ToListAsync();

        return shifts.Select(s => new GetShiftDto {
            Id = s.Id,
            Name = s.Name,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
        }).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetShiftDto>> GetShift(int id)
    {
        var shift = await db.Shifts.FindAsync(id);
        if (shift == null) {
            return NotFound(); 
        }

        return new GetShiftDto {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime
        };
    }

    [HttpPost]
    public async Task<ActionResult<GetShiftDto>> PostShift(PostShiftDto dto)
    {
        var shift = new Shift {
            Name = dto.Name,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            WorkerShifts = []
        };

        db.Shifts.Add(shift);
        await db.SaveChangesAsync();

        var returnDto = new GetShiftDto {
            Id = shift.Id,
            Name = shift.Name,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
        };

        return CreatedAtAction("GetShift", new { id = shift.Id }, returnDto); 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, PutShiftDto dto)
    {
        var shift = await db.Shifts.FindAsync(id);

        if (shift == null)
        {
            return NotFound();
        }

        shift.Name = dto.Name;
        shift.StartTime = dto.StartTime;
        shift.EndTime = dto.EndTime;

        db.Entry(shift).State = EntityState.Modified;
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        var shift = await db.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound();
        }

        db.Shifts.Remove(shift);
        await db.SaveChangesAsync();

        return NoContent();
    }
}