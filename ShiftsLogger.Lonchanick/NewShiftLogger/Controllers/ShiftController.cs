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
    public async Task<IActionResult> Get()
    {
        return Ok(await ShiftService.getShifts());
    }

    [HttpPost]
    public async Task<IActionResult> NewShift([FromBody] Shift shift)
    {
        shift.Check = DateTime.Now;
        await ShiftService.SaveShift(shift);
        return Ok(); 
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateShift(int id, [FromBody] Shift shift)
    {
        await ShiftService.UpdateShift(id, shift);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        await ShiftService.DeleteShift(id);
        return Ok();
    }

}
