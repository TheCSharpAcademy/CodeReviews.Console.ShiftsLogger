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

    [HttpPut]
    public IActionResult UpdateShift(int id, Shift shift)
    {
        try
        {
            if (id != shift.Id)
            {
                return BadRequest("Id does not match.");
            }
            var updateShift = context.Shifts.Find(id);
            if (updateShift == null)
            {
                return NotFound();
            }
            updateShift.Start = shift.Start;
            updateShift.End = shift.End;
            updateShift.Id = shift.Id;
            updateShift.WorkerId = shift.WorkerId;
            updateShift.Worker = shift.Worker;
            context.SaveChanges();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
       
    }
    [HttpGet]
    public ActionResult<IEnumerable<Shift>> GetShifts()
    {
        try
        {
            var shifts = context.Shifts.Include(s => s.Worker).ToList();
            return Ok(shifts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }

    [HttpPost]
    public ActionResult<Shift> PostShift(Shift shift)
    {
        try
        {
            context.Shifts.Add(shift);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetShifts), new { id = shift.Id }, shift);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteShift(int id)
    {
        try
        {
            var shift = context.Shifts.Find(id);
            if (shift == null)
            {
                return NotFound();
            }

            context.Shifts.Remove(shift);
            context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

}
