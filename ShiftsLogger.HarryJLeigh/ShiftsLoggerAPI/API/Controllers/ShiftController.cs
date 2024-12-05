using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using ShiftsLogger.API.Services;

namespace ShiftsLogger.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ShiftController(IShiftService shiftService) : ControllerBase
{
    private readonly IShiftService _shiftService = shiftService;
    
    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(_shiftService.CreateShift(shift));
    }

    [HttpPut("{id}")]
    public ActionResult<Shift> UpdateShift(Shift shift)
    {
        var result = _shiftService.UpdateShift(shift);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);
        if (result == null) return NotFound();
        return Ok(result);
    }
}