using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.API.Model;
using ShiftsLogger.API.Service;

namespace ShiftsLogger.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    private readonly ShiftService _shiftService;

    public ShiftController(ShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(Shift shift)
    {
        return Ok(_shiftService.CreateShift(shift));
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var result = _shiftService.GetShiftById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    public ActionResult<Shift> UpdateShift(Shift shift)
    {
        return Ok(_shiftService.UpdateShift(shift));
    }

    [HttpDelete("{id}")]
    public ActionResult<Shift> DeleteShift(int id)
    {
        var result = _shiftService.DeleteShift(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
