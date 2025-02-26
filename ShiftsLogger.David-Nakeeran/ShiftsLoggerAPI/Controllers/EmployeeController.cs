using Microsoft.AspNetCore.Mvc;
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

            if (employees.Message == "NotFound")
            {
                return NotFound(employees.Message);
            }

            if (!employees.Success)
            {
                return BadRequest(employees.Message);
            }

            var employeesDTO = employees?.Data?.Select(x => _employeeMapper.EmployeeToDTO(x)).ToList();
            return Ok(employeesDTO);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(long id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee.Data == null)
            {
                return NotFound(employee.Message);
            }

            if (!employee.Success)
            {
                return BadRequest(employee.Message);
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

            if (employee.Data == null)
            {
                return NotFound(employee.Message);
            }

            if (!employee.Success)
            {
                return BadRequest(employee.Message);
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

            if (createdEmployee.Data == null)
            {
                return NotFound(createdEmployee.Message);
            }

            if (!createdEmployee.Success)
            {
                return BadRequest(createdEmployee.Message);
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

            if (!employee.Data)
            {
                return NotFound(employee.Message);
            }

            if (!employee.Success)
            {
                return BadRequest(employee.Message);
            }

            return NoContent();
        }
    }
}
