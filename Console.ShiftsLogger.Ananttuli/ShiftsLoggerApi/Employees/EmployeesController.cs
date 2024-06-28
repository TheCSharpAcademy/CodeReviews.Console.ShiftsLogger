using Microsoft.AspNetCore.Mvc;
using ShiftsLoggerApi.Common;
using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : CustomController
    {
        private readonly EmployeesService EmployeesService;

        public EmployeesController(
            EmployeesService employeesService
        )
        {
            EmployeesService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var (employees, error) = await EmployeesService.GetEmployees();

            if (error == null && employees != null)
            {
                return Ok(employees);
            }

            return this.ErrorResponse(error);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var (employee, error) = await EmployeesService.GetEmployee(id);

            if (error == null && employee != null)
            {
                return Ok(employee);
            }

            return this.ErrorResponse(error);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> PutEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var (updatedEmployee, error) = await EmployeesService.UpdateEmployee(id, employeeUpdateDto);

            if (error == null && updatedEmployee != null)
            {
                return Ok(updatedEmployee);
            }

            return this.ErrorResponse(error);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var (createdEmployee, error) = await EmployeesService.CreateEmployee(employeeCreateDto);

            if (error == null && createdEmployee != null)
            {
                return CreatedAtAction("GetEmployee", new { id = createdEmployee.EmployeeId }, createdEmployee);
            }

            return this.ErrorResponse(error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var (deletedId, error) = await EmployeesService.DeleteEmployee(id);

            if (error == null && deletedId != null)
            {
                return Ok(deletedId);
            }

            return this.ErrorResponse(error);
        }
    }
}
