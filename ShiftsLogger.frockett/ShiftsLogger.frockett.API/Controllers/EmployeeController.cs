using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.frockett.API.Services;
using ShiftsLogger.frockett.API.DTOs;

namespace ShiftsLogger.frockett.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    // POST api/employees
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> AddEmployee([FromBody] EmployeeCreateDto employeeCreateDto)
    {
        var employeeDto = await employeeService.CreateEmployeeAsync(employeeCreateDto);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id });
    }

    // GET: api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
    {
        var employees = await employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    // GET: api/employees/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
    {
        var employee = await employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // PUT: api/employees/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeUpdateDto)
    {
        if (id != employeeUpdateDto.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            await employeeService.UpdateEmployeeAsync(employeeUpdateDto);
            return NoContent(); // 204 No Content is typically returned when an update operation is successful
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // DELETE: api/employees/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id, [FromBody] EmployeeDto employeeDto)
    {
        try
        {
            await employeeService.DeleteEmployeeAsync(employeeDto);
            return NoContent(); // 204 No Content is a common response for a successful DELETE operation
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
