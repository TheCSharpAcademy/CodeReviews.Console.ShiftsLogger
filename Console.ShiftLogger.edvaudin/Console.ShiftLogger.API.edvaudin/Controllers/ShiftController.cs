using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShiftLogger.API.Models;

namespace Console.ShiftLogger.API.edvaudin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ShiftController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await dataContext.Employees.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> Get(int id)
        {
            var employee = await dataContext.Employees.FindAsync(id);
            if (employee == null) { return BadRequest("We could not find any employees by that Id."); }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            dataContext.Employees.Add(employee);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Employees.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var employee = await dataContext.Employees.FindAsync(request.Id);
            if (employee == null) { return BadRequest("We could not find any employees by that Id."); }
            employee.Name = request.Name;
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Employees.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var employee = await dataContext.Employees.FindAsync(id);
            if (employee == null) { return BadRequest("We could not find any employees by that Id."); }
            dataContext.Employees.Remove(employee);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Employees.ToListAsync());
        }
    }
}
