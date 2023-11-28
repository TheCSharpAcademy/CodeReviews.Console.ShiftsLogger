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
        bool onGoingShift = await ShiftService.OnGoingShift(shift.WorkerId);

        if (onGoingShift & (shift.CheckTypeField == CheckType.CheckIn))
            return BadRequest("Is a shift currently ongoing, you can't check in again");

        if (!onGoingShift & (shift.CheckTypeField == CheckType.CheckOut))
            return BadRequest("You can't check out [again]");

        shift.Check = DateTime.Now;
        await ShiftService.SaveShift(shift);
        return Ok(1);

    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteShift(int id)
    {
        await ShiftService.DeleteShift(id);
        return Ok();
    }

}
