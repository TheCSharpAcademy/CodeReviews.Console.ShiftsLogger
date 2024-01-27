using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebApi.Models;

namespace ShiftsLoggerWebApi.Controllers;

[Route("api/Shifts")]
[ApiController]

public class ShiftsController(ShiftsLoggerContext dbContext) : ControllerBase
{
    private static readonly int ShiftMaxGetQty = 5; 
    private readonly ShiftsLoggerContext DBContext = dbContext;

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShifts(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();

        var shifts = employee.First().Shifts?.Select(ShiftDto.FromShift)
            .OrderBy( p => p.ShiftStartTime).ToList();

        if(shifts?.Count > ShiftMaxGetQty)
            return shifts.GetRange(shifts.Count-ShiftMaxGetQty, ShiftMaxGetQty);
        else if(shifts != null)
            return shifts;
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutShift(int id, ShiftDto shift)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();
    
        if(employee.First().Shifts.Any(p => p.ShiftEndTime == null))
        {
            return BadRequest();
        }
        else
        {
            employee.First().Shifts.Add(Shift.FromShiftDto(shift));
            DBContext.SaveChanges();
            return NoContent();
        }
    }

    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult> PatchShift(int id,[FromBody] JsonPatchDocument<Shift> patchDoc)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if(employee.Count <= 0)
            return NotFound();

        var shift = employee.First().Shifts.OrderBy(p => p.ShiftStartTime).ToList();
        if (shift == null || shift.Count <= 0)
            return NotFound();

        if(patchDoc != null && shift.Last().ShiftEndTime == null)
        {
            patchDoc.ApplyTo(shift.Last(), ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);        
            DBContext.SaveChanges();
            return NoContent();
        }
        return BadRequest();
    }
}
