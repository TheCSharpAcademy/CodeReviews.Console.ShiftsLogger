using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerAPI.Interface;
using ShiftsLoggerAPI.Models;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeMapper _employeeMapper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeMapper employeeMapper, IEmployeeService employeeService)
        {
            _employeeMapper = employeeMapper;
            _employeeService = employeeService;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            if (!string.IsNullOrEmpty(employees.Error))
            {
                return StatusCode(500, employees.Error);
            }

            if (employees.Data == null && !employees.Success)
            {
                return NotFound();
            }

            if (!employees.Success)
            {
                return BadRequest();
            }

            var employeesDTO = employees?.Data?.Select(x => _employeeMapper.EmployeeToDTO(x)).ToList();
            return Ok(employeesDTO);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(long id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (!string.IsNullOrEmpty(employee.Error))
            {
                return StatusCode(500, employee.Error);
            }

            if (employee.Data == null && !employee.Success)
            {
                return NotFound();
            }

            if (!employee.Success)
            {
                return BadRequest();
            }

            var employeeDTO = _employeeMapper.EmployeeToDTO(employee.Data);
            return Ok(employeeDTO);
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(long id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest("Employee ID in URL does not match response body");
            }
            var employee = await _employeeService.UpdateEmployee(id, employeeDTO);

            if (!string.IsNullOrEmpty(employee.Error))
            {
                return StatusCode(500, employee.Error);
            }

            if (employee.Data == null && !employee.Success)
            {
                return NotFound();
            }

            if (!employee.Success)
            {
                return BadRequest();
            }

            var updatedEmployeeDTO = _employeeMapper.EmployeeToDTO(employee.Data);
            return Ok(updatedEmployeeDTO);
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
        {

            var createdEmployee = await _employeeService.CreateEmployee(employeeDTO);

            if (!string.IsNullOrEmpty(createdEmployee.Error))
            {
                return StatusCode(500, createdEmployee.Error);
            }

            if (createdEmployee.Data == null && !createdEmployee.Success)
            {
                return NotFound();
            }

            if (!createdEmployee.Success)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = createdEmployee.Data.EmployeeId },
                _employeeMapper.EmployeeToDTO(createdEmployee.Data));

        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _employeeService.DeleteEmployee(id);

            if (!string.IsNullOrEmpty(employee.Error))
            {
                return StatusCode(500, employee.Error);
            }

            if (employee.Data == null && !employee.Success)
            {
                return NotFound();
            }

            if (!employee.Success)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
