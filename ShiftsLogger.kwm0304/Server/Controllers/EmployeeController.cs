using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Server.Models;
using Spectre.Console;
namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    public EmployeeController(IEmployeeService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
      try
      {
        return Ok();
      }
      catch (Exception e)
      {
        AnsiConsole.WriteException(e);
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
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

    [HttpGet("{id}/shifts/{shift-id}")]
    public async Task<IActionResult> GetEmployeeShiftById(int id, int shiftId)
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
    public async Task<IActionResult> CreateEmployee([FromBody]Employee employee)
    {
      try
      {
        return Ok(employee);
      }
      catch (Exception e)
      {
        AnsiConsole.WriteException(e);
        BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeById(int id)
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

    public async Task<IActionResult> DeleteEmployeeById(int id)
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