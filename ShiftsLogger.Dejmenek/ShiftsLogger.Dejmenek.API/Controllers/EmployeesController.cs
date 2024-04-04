using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.Dejmenek.API.Data.Interfaces;
using ShiftsLogger.Dejmenek.API.Models;

namespace ShiftsLogger.Dejmenek.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : Controller
{
    private readonly IEmployeesRepository _employeesRepository;

    public EmployeesController(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    //GET: api/employees
    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var employees = await _employeesRepository.GetEmployeesAsync();

        return Ok(employees);
    }

    //GET: /api/employees/5/shifts
    [HttpGet]
    [Route("{employeeId}/shifts")]
    public async Task<IActionResult> GetEmployeeShiftsAsync(int employeeId)
    {
        if (!await _employeesRepository.EmployeeExists(employeeId))
        {
            return NotFound();
        }

        var employeeShifts = await _employeesRepository.GetEmployeeShiftsAsync(employeeId);

        if (employeeShifts is null)
        {
            return NotFound();
        }

        return Ok(employeeShifts);
    }

    //POST: /api/employees
    [HttpPost]
    public async Task<IActionResult> PostEmployeeAsync([FromBody] EmployeeCreateDTO employeeDto)
    {
        await _employeesRepository.AddEmployeeAsync(employeeDto);

        return NoContent();
    }

    //DELETE: /api/employees/5
    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int employeeId)
    {
        var result = await _employeesRepository.DeleteEmployeeAsync(employeeId);

        if (result == -1)
        {
            return NotFound();
        }

        return NoContent();
    }

    //PUT: /api/employees/5
    [HttpPut("{employeeId}")]
    public async Task<IActionResult> PutEmployeeAsync(int employeeId, [FromBody] EmployeeUpdateDTO employeeDto)
    {
        var result = await _employeesRepository.UpdateEmployeeAsync(employeeId, employeeDto);

        if (result == -1)
        {
            return NotFound();
        }

        return NoContent();
    }
}
