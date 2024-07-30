using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Dtos;
using Server.Services.Interfaces;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController(IShiftService service) : ControllerBase
{
    private readonly IShiftService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllShifts()
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        AnsiConsole.WriteException(e);
        BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShiftById(int id)
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        AnsiConsole.WriteException(e);
        BadRequest(e.Message);
      }
    }


    [HttpPost]
    public async Task<IActionResult> CreateShift([FromBody]EmployeeShiftDto dto)
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        AnsiConsole.WriteException(e);
        BadRequest(e.Message);
      }
    }
}
