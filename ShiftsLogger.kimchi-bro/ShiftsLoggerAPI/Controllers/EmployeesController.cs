using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Produces("application/json")]
    public class EmployeesController(ShiftsLoggerDbContext dbContext) : ControllerBase
    {
        private readonly ShiftsLoggerDbContext _dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<object>> GetEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return Ok(new { employees });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            return Ok(employee);
        }

        [HttpGet("{id}/shifts")]
        public async Task<ActionResult<Shift>> GetShiftsForEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            var shifts = await _dbContext.Shifts.Where(s => s.EmployeeId == id).ToListAsync();

            return Ok(new { shifts });
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            if (employee is null) return BadRequest();

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updateEmployee)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            employee.Name = updateEmployee.Name;
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee is null) return NotFound();

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
