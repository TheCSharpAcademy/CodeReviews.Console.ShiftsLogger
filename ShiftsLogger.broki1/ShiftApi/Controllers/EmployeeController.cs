using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftApi.DTOs;
using ShiftApi.Models;
using ShiftApi.Service;

namespace ShiftApi.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{

    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult> PostEmployee(Employee employee)
    {
        await _employeeService.PostEmployee(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.EmployeeId }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeUpdateDTO employeeUpdateDTO)
    {
        var employee = await _employeeService.FindAsync(id);

        if (id != employeeUpdateDTO.EmployeeId)
        {
            return BadRequest();
        }

        try
        {
            await _employeeService.UpdateEmployee(id, employeeUpdateDTO);
        }
        catch (Exception ex)
        {
            if (!_employeeService.EmployeeExists(employee))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var shift = await _employeeService.FindAsync(id);

        if (shift is null)
            return NotFound();

        await _employeeService.Delete(shift);

        return NoContent();
    }
}
