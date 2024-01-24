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
            return NotFound();
        else
            return employee;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();
        else
            return employee.First();
    }

    [HttpPut()]
    public async Task<ActionResult> PutEmployee(EmployeeDto employee)
    {
        DBContext.Employees.Add(new Employee{Name = employee.Name});
        await DBContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult> PatchEmployee(int id,[FromBody] JsonPatchDocument<Employee> patchDoc)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if(employee.Count <= 0)
            return NotFound();

        if(patchDoc != null)
        {
            patchDoc.ApplyTo(employee.First(), ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);        
            DBContext.SaveChanges();
            return NoContent();
        }
        return BadRequest();
    }
}
