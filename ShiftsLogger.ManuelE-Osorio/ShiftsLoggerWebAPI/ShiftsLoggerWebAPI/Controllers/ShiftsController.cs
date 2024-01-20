using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebApi.Models;

namespace ShiftsLoggerWebApi.Controllers;

[Route("api/Shifts")]
[ApiController]

public class ShiftsController : ControllerBase
{
    private static readonly int ShiftMaxGetQty = 5; 
    private readonly ShiftsLoggerContext DBContext;

    public ShiftsController(ShiftsLoggerContext dbContext)
    {
        DBContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ShiftDto>>> GetShifts(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();

        var shifts = employee.First().Shifts?.Select(p => ShiftDto.FromShift(p))
            .OrderBy( p => p.ShiftStartTime).ToList();
        
        if(shifts?.Count > ShiftMaxGetQty)
            return Ok(shifts.GetRange(shifts.Count-ShiftMaxGetQty, ShiftMaxGetQty));
        else
            return Ok(shifts);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutShift(int id, ShiftDto shift)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();

        employee.First().Shifts?.Add(Shift.FromShiftDto(shift));
        DBContext.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchShift(int id, DateTime shiftEndTime)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if(employee.Count <= 0)
            return NotFound();
        
        var shift = employee.First().Shifts?.OrderBy(p => p.ShiftStartTime).ToList();
        if (shift == null || shift.Count <= 0)
            return NotFound();

        shift.Last().ShiftEndTime = shiftEndTime;
        DBContext.SaveChanges();
        return NoContent();
    }
}
