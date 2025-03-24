using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.weakiepedia.Models;
using ShiftsLogger.weakiepedia.Services;

namespace ShiftsLogger.weakiepedia.Controllers;

[ApiController]
[Route("shifts")]
public class ShiftController : ControllerBase
{
    private IShiftService _shiftService;
    
    public ShiftController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        var result = _shiftService.GetAllShifts();
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult GetShiftById(int id)
    {
        var result = _shiftService.GetShiftById(id);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public ActionResult CreateShift(Shift shift)
    {
        var result = _shiftService.CreateShift(shift);
        
        if (result == null)
            return BadRequest(new { message = "Something went wrong, are you sure there's an employee with this id?" });
        
        return Ok(result);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateShift(int id, Shift shift)
    {
        var result = _shiftService.UpdateShift(id, shift);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
}