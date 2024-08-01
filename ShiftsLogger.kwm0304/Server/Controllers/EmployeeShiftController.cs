using Microsoft.AspNetCore.Mvc;
using Server.Models.Dtos;
using Server.Services.Interfaces;
using Shared;
using Spectre.Console;

namespace Server.Controllers;

[ApiController]
[Route("api/employee-shift")]
public class EmployeeShiftController : Controller<EmployeeShift>
{
    private readonly IEmployeeShiftService _employeeShiftService;

    public EmployeeShiftController(IService<EmployeeShift> service, IEmployeeShiftService employeeShiftService)
        : base(service)
    {
        _employeeShiftService = employeeShiftService;
    }

    [HttpGet("{employeeId}/{shiftId}")]
    public async Task<IActionResult> GetEmployeeShift(int shiftId)
    {
        try
        {
            var result = await _employeeShiftService.GetLateEmployeesForShiftAsync(shiftId);
            return Ok(result);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("shift/{shiftId}")]
    public async Task<IActionResult> GetAllEmployeesOnShift(int shiftId)
    {
        try
        {
            var result = await _employeeShiftService.GetEmployeesForShiftAsync(shiftId);
            return Ok(result);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetAllShiftsForEmployee(int employeeId)
    {
        try
        {
            var result = await _employeeShiftService.GetShiftsForEmployeeAsync(employeeId);
            return Ok(result);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpGet("late/{shiftId}")]
    public async Task<IActionResult> GetLateEmployeesForShift(int shiftId)
    {
        try
        {
            var result = await _employeeShiftService.GetLateEmployeesForShiftAsync(shiftId);
            return Ok(result);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployeeShift([FromBody] EmployeeShiftDto employeeShiftDto)
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
    public async Task<IActionResult> UpdateEmployeeShift(int employeeId, int shiftId, [FromBody] EmployeeShiftDto employeeShiftDto)
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