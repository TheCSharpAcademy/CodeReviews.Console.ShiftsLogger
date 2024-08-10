using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : Controller<Shift>
{
    private readonly IShiftService _shiftService;
    public ShiftsController(IService<Shift> service, IShiftService shiftService) : base(service)
    {
        _shiftService = shiftService;
    }

    [HttpGet("new")]
    public async Task<IActionResult> GetNewestShift()
    {
        try
        {
            var response = await _shiftService.GetNewestShift();
            return Ok(response);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }
}