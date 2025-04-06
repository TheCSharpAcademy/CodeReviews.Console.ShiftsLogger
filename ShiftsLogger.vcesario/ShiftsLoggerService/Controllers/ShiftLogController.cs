using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ShiftLogController : ControllerBase
{
    private readonly IShiftLogService m_ShiftLogService;

    public ShiftLogController(IShiftLogService service)
    {
        m_ShiftLogService = service;
    }

    [HttpGet]
    public ActionResult<List<Shift>> GetShifts()
    {
        return Ok(m_ShiftLogService.GetShifts());
    }
}