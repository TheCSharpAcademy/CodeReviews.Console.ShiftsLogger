using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerModels;

namespace ShiftsLoggerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController(IShiftService shiftService) : ControllerBase
{
    private readonly IShiftService _shiftService = shiftService;

    [HttpGet]
    public ActionResult GetShifts()
    {
        return Ok(_shiftService.GetShifts());
    }

    [HttpGet("Employee/{id:int}")]
    public ActionResult GetShiftsByEmployeeId(int id)
    {
        return Ok(_shiftService.GetShiftsByEmployeeId(id));
    }

    [HttpGet("{id:int}")]
    public ActionResult GetShiftById(int id)
    {
        var shift = _shiftService.GetShiftById(id);
        
        if (shift is null) return NotFound();
        
        return Ok(shift);
    }

    [HttpPost]
    public ActionResult AddShift(Shift shift)
    {
        var dbShift = _shiftService.AddShift(shift);
        
        return Ok(dbShift);
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteShift(int id)
    {
        if (_shiftService.DeleteShift(id))
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut]
    public ActionResult UpdateShift(Shift shift)
    {
        var dbShift = _shiftService.UpdateShift(shift);
        if (dbShift is null) return NotFound();
        return Ok(dbShift);
    }
}