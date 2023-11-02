using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Lonchanick.Models;
using ShiftsLogger.Lonchanick.Services;

namespace ShiftsLogger.Lonchanick.Controllers;

[Route("api/[controller]")]
public class ShiftController : ControllerBase
{
    protected readonly IShiftService ShiftService;

    public ShiftController(IShiftService shiftService)
    {
        this.ShiftService = shiftService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(ShiftService.getShifts());
    }

    [HttpPost]
    public IActionResult NewShift([FromBody] Shift shift)
    {
        ShiftService.SaveShift(shift);
        return Ok(); 
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateShift(Guid id, [FromBody] Shift shift)
    {
        ShiftService.UpdateShift(id, shift);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult UpdateShift(Guid id)
    {
        ShiftService.DelteShift(id);
        return Ok();
    }

}
