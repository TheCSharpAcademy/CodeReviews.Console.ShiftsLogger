using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Data;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [ApiController]
    [Route("api/mock")]
    [Produces("application/json")]
    public class MockControllers(ShiftsLoggerDbContext dbContext) : ControllerBase
    {
        private readonly ShiftsLoggerDbContext _dbContext = dbContext;

        [HttpPost("add")]
        public IActionResult AddShifts([FromBody] List<Shift> shifts)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Shifts.AddRange(shifts);
                _dbContext.SaveChanges();
                transaction.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("empadd")]
        public IActionResult AddEmployees([FromBody] List<Employee> employees)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Employees.AddRange(employees);
                _dbContext.SaveChanges();
                transaction.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("del")]
        public async Task<IActionResult> DeleteShifts()
        {
            _dbContext.Shifts.RemoveRange(_dbContext.Shifts);
            await _dbContext.SaveChangesAsync();
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Shifts', RESEED, 0)");

            return NoContent();
        }

        [HttpDelete("empdel")]
        public async Task<IActionResult> DeleteEmployees()
        {
            _dbContext.Employees.RemoveRange(_dbContext.Employees);
            await _dbContext.SaveChangesAsync();
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Employees', RESEED, 0)");

            return NoContent();
        }
    }
}
