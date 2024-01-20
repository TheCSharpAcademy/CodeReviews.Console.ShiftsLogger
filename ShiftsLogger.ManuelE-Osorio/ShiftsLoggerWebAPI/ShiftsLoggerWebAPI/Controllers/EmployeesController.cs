using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebApi.Models;

namespace ShiftsLoggerWebApi.Controllers;

[Route("api/Employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly ShiftsLoggerContext DBContext;

    public EmployeesController(ShiftsLoggerContext dbContext)
    {
        DBContext = dbContext;
    }
    
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee([FromQuery(Name = "name")] string employeeName)
    {
        var employee = await DBContext.Employees.Where(p => p.Name.StartsWith(employeeName)).ToListAsync();

        if (employee.Count <= 0)
            return NotFound();
        else
            return Ok(employee.Select(p => EmployeeDto.FromEmployee(p)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();
        else
            return Ok(EmployeeDto.FromEmployee(employee.First()));
    }

    [HttpPut()]
    public async Task<ActionResult> PutEmployee(EmployeeDto employee)
    {
        DBContext.Employees.Add(new Employee{Name = employee.Name});
        await DBContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchEmployee(int id, EmployeeDto employeeDto)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if(employee.Count <= 0)
            return NotFound();
        employee.First().Name = employeeDto.Name;
        await DBContext.SaveChangesAsync();
        return NoContent();
    }
}
