using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Employees.Models;
using ShiftsLoggerApi.Shifts.Models;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ShiftsLoggerContext Db;
        private readonly EmployeesService EmployeesService;

        public EmployeesController(
            ShiftsLoggerContext dbContext,
            EmployeesService employeesService
        )
        {
            Db = dbContext;
            EmployeesService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await Db.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var employee = await Db.Employees
                .Where(e => e.EmployeeId == id)
                .Select(e =>
                    new EmployeeDto(
                        e.EmployeeId,
                        e.Name,
                        e.Shifts.Select(s =>
                            new ShiftDto(
                                s.ShiftId,
                                s.StartTime,
                                s.EndTime,
                                null
                            )
                        ).ToList()
                    )
                )
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> PutEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            if (id != employeeUpdateDto.EmployeeId)
            {
                return BadRequest(
                    new Error(
                        ErrorType.BusinessRuleValidation,
                        "Param ID does not match payload ID"
                    )
                );
            }

            var (updatedEmployee, error) = await EmployeesService.UpdateEmployee(employeeUpdateDto);

            if (error == null && updatedEmployee != null)
            {
                return updatedEmployee;
            }

            return error?.Type switch
            {
                Util.ErrorType.BusinessRuleValidation => BadRequest(error),
                Util.ErrorType.DatabaseNotFound => NotFound(),
                _ => StatusCode(StatusCodes.Status500InternalServerError, error)
            };

        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var (CreatedEmployee, Error) = await EmployeesService.CreateEmployee(employeeCreateDto);

            if (Error == null && CreatedEmployee != null)
            {
                return CreatedAtAction("GetEmployee", new { id = CreatedEmployee.EmployeeId }, CreatedEmployee);
            }

            if (Error?.Type == Util.ErrorType.BusinessRuleValidation)
            {
                return BadRequest(Error);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await Db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            Db.Employees.Remove(employee);
            await Db.SaveChangesAsync();

            return NoContent();
        }
    }
}
