using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerWebAPI.DTOs;
using ShiftsLoggerWebAPI.Services;

namespace ShiftsLoggerWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public ActionResult<List<EmployeeDto>> GetAllEmployees()
    {
        var result = _employeeService.GetAllEmployees();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<EmployeeDto> GetEmployeeById(int id)
    {
        var result = _employeeService.GetEmployeeById(id);
        if (result == null)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<string> CreateEmployee(EmployeeDto employeeDto)
    {
        var result = _employeeService.CreateEmployee(employeeDto);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut]
    public ActionResult<string> UpdateEmployee(EmployeeDto updatedEmployee)
    {
        var result = _employeeService.UpdateEmployee(updatedEmployee);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteEmployee(int id)
    {
        var result = _employeeService.DeleteEmployee(id);
        if (result == null)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}