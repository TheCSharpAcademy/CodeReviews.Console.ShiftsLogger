using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Dtos;
using Server.Services.Interfaces;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeShiftController : Controller<EmployeeShift>
{
    private readonly IEmployeeShiftService _employeeShiftService;

    public EmployeeShiftController(IService<EmployeeShift> service, IEmployeeShiftService employeeShiftService)
        : base(service)
    {
        _employeeShiftService = employeeShiftService;
    }

    [HttpGet("{employeeId}/{shiftId}")]
    public async Task<IActionResult> GetEmployeeShift(int employeeId, int shiftId)
    {
        try
        {
            var employeeShift = await _employeeShiftService.GetEmployeeShiftAsync(employeeId, shiftId);
            if (employeeShift == null)
            {
                return NotFound("EmployeeShift not found");
            }
            return Ok(employeeShift);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployeeShift([FromBody]EmployeeShiftDto employeeShiftDto)
    {
        try
        {
            var createdEmployeeShift = await _employeeShiftService.CreateEmployeeShiftAsync(employeeShiftDto);
            return CreatedAtAction(nameof(GetEmployeeShift),
                new { employeeId = createdEmployeeShift.EmployeeId, shiftId = createdEmployeeShift.ShiftId },
                createdEmployeeShift);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{employeeId}/{shiftId}")]
    public async Task<IActionResult> UpdateEmployeeShift(int employeeId, int shiftId, [FromBody]EmployeeShiftDto employeeShiftDto)
    {
        try
        {
            var updatedEmployeeShift = await _employeeShiftService.UpdateEmployeeShiftAsync(employeeId, shiftId, employeeShiftDto);
            return Ok(updatedEmployeeShift);
        }
        catch (NullReferenceException)
        {
            return NotFound("EmployeeShift not found");
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{employeeId}/{shiftId}")]
    public async Task<IActionResult> DeleteEmployeeShift(int employeeId, int shiftId)
    {
        try
        {
            await _employeeShiftService.DeleteEmployeeShiftAsync(employeeId, shiftId);
            return Ok("EmployeeShift deleted successfully");
        }
        catch (NullReferenceException)
        {
            return NotFound("EmployeeShift not found");
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }
}