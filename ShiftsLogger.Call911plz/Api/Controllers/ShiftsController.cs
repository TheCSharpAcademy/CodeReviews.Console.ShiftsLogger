using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Workers/{workerId}/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftService _shiftService;    
    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet("all")]
    public ActionResult<List<Shift>> GetAllShifts()
    {
        return Ok(_shiftService.GetAllShifts());
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetShiftsByWorkerId(int workerId)
    {
        var result = _shiftService.GetShiftsByWorkerId(workerId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftByShiftId(int workerId, int id)
    {
        var result = _shiftService.GetShiftByShiftId(workerId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Shift> CreateShift(int workerId, ShiftDto shiftDto)
    {
        Shift shift = new() {
            WorkerId = workerId,
            StartDateTime = shiftDto.StartDateTime,
            EndDateTime = shiftDto.EndDateTime,
        };

        var result = _shiftService.CreateShift(shift);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{shiftId}")]
    public ActionResult<Shift> UpdateShift(int workerId, int shiftId, ShiftDto shiftDto)
    {
        Shift shift = new() {
            Id = shiftId,
            WorkerId = workerId,
            StartDateTime = shiftDto.StartDateTime,
            EndDateTime = shiftDto.EndDateTime,
        };

        var result = _shiftService.UpdateShift(shift);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteShift(int workerId, int id)
    {
        var result = _shiftService.DeleteShift(workerId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}