using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkerShiftsAPI.DTOs;
using WorkerShiftsAPI.Models;

namespace WorkerShiftsAPI.Controllers;

/// <summary>
/// Provides a controller for managing worker shifts in the WorkerShiftsAPI application.
/// </summary>
[Route("api/shifts")]
[ApiController]
public class ShiftController : ControllerBase
{
    private readonly WorkerShiftContext _context;

    public ShiftController(WorkerShiftContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all shifts.
    /// </summary>
    /// <returns>A list of <see cref="ShiftDTO"/> objects representing the shifts.</returns>
    // GET: api/shifts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftDTO>>> GetShifts()
    {
        return await _context.Shifts
            .Include(s => s.Worker)
            .Select(s => ShiftToDTO(s))
            .ToListAsync();
    }

    /// <summary>
    /// Gets a specific shift by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift to retrieve.</param>
    /// <returns>A <see cref="ShiftDTO"/> object representing the requested shift, or a 404 NotFound response if the shift is not found.</returns>
    // GET: api/shifts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ShiftDTO>> GetShift(int id)
    {
        var shift = await _context.Shifts
            .Include(s => s.Worker)
            .Select(s => ShiftToDTO(s))
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        if (shift == null)
        {
            return NotFound();
        }

        return shift;
    }

    /// <summary>
    /// Updates an existing shift.
    /// </summary>
    /// <param name="id">The ID of the shift to update.</param>
    /// <param name="shiftDTO">The updated shift data.</param>
    /// <returns>A 204 NoContent response if the update is successful, or a 400 BadRequest response if the ID does not match the shift ID in the request body.</returns>
    // PUT: api/shifts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutShift(int id, ShiftDTO shiftDTO)
    {
        if (id != shiftDTO.ShiftId)
        {
            return BadRequest();
        }

        var shift = await _context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return NotFound();
        }

        shift.ShiftId = shiftDTO.ShiftId;
        shift.StartTime = shiftDTO.StartTime;
        shift.EndTime = shiftDTO.EndTime;
        shift.WorkerId = shiftDTO.WorkerId;

        _context.Entry(shift).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ShiftExists(id))
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

    /// <summary>
    /// Creates a new shift.
    /// </summary>
    /// <param name="shiftDTO">The data for the new shift.</param>
    /// <returns>A 201 Created response with the created shift data, or a 400 BadRequest response if the worker ID is invalid.</returns>
    // POST: api/shifts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
    {
        var worker = await _context.Workers.FindAsync(shiftDTO.WorkerId);
        if (worker == null)
        {
            return BadRequest("Invalid worker ID");
        }

        var shift = new Shift
        {
            StartTime = shiftDTO.StartTime,
            EndTime = shiftDTO.EndTime,
            WorkerId = shiftDTO.WorkerId
        };

        _context.Shifts.Add(shift);
        await _context.SaveChangesAsync();

        shiftDTO.ShiftId = shift.ShiftId;
        shiftDTO.WorkerName = worker.Name;

        return CreatedAtAction(nameof(GetShift), new { id = shiftDTO.ShiftId }, ShiftToDTO(shift));
    }

    /// <summary>
    /// Deletes a shift by its ID.
    /// </summary>
    /// <param name="id">The ID of the shift to delete.</param>
    /// <returns>A 204 NoContent response if the deletion is successful, or a 404 NotFound response if the shift is not found.</returns>
    // DELETE: api/shifts/5
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
        return _context.Shifts.Any(e => e.ShiftId == id);
    }

    public static ShiftDTO ShiftToDTO(Shift shift) =>
    new()
    {
        ShiftId = shift.ShiftId,
        StartTime = shift.StartTime,
        EndTime = shift.EndTime,
        WorkerId = shift.WorkerId,
        WorkerName = shift.Worker.Name
    };
}

