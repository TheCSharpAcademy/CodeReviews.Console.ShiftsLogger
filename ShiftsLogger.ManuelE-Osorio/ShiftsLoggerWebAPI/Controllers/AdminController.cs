using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebApi.Models;

namespace ShiftsLoggerWebApi.Controllers;

[Route("api/Admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ShiftsLoggerContext DBContext;

    public AdminController(ShiftsLoggerContext dbContext)
    {
        DBContext = dbContext;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<bool>> GetEmployeeAdmin(int id)
    {
        var employee = await DBContext.Employees.Where(p => p.EmployeeId == id).ToListAsync();
        if (employee.Count <= 0)
            return NotFound();
        else
            return employee.First().Admin;
    }
}
