using Microsoft.AspNetCore.JsonPatch;
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
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee([FromQuery(Name = "name")] string employeeName)
    {
        var employee = await DBContext.Employees.Where(p => p.Name.StartsWith(employeeName)).ToListAsync();

        if (employee.Count <= 0)
            return NotFound("Employee name does not exists.");
        else
            return employee;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound("Employee ID does not exists");
        else
            return employee;
    }

    [HttpPut()]
    public async Task<ActionResult> PutEmployee(Employee employee)
    {
        DBContext.Employees.Add(new Employee{Name = employee.Name, Admin = employee.Admin});
        await DBContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost()]
    public async Task<ActionResult> PatchEmployee(Employee employeeToModify)
    {
        var employee = await DBContext.Employees
            .Where(p => p.EmployeeId == employeeToModify.EmployeeId).ToListAsync();
        if(employee.Count <= 0)
            return NotFound();

        employee.First().Name = employeeToModify.Name;
        employee.First().Admin = employeeToModify.Admin;
        await DBContext.SaveChangesAsync();
        return NoContent();
    }
}
