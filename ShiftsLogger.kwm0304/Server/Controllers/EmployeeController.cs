using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Shared;
using Shared.Enums;
using Spectre.Console;
namespace Server.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : Controller<Employee>
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IService<Employee> service, IEmployeeService employeeService) : base(service)
    {
        _employeeService = employeeService;
    }

    [HttpGet("classification/{classification}")]
    public async Task<IActionResult> GetEmployeesByShiftClassification(ShiftClassification classification)
    {
        try
        {
            var result = await _employeeService.GetEmployeesByShift(classification);
            return Ok(result);
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return BadRequest(e.Message);
        }
    }
}