using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ShiftLogger.Data;
using ShiftLogger.Models;

namespace ShiftLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ShiftLoggerContext _context;

        public EmployeeController(ShiftLoggerContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
            => await _context.Employees.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByID(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var issueToDelete = await _context.Employees.FindAsync(id);
            if (issueToDelete == null) return NotFound();

            _context.Employees.Remove(issueToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
