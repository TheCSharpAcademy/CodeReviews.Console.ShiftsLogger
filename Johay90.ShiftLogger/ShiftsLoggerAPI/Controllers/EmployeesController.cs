using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;
using SharedLibrary.Validations;

namespace ShiftsLoggerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService service) : ControllerBase
    {
        private readonly IEmployeeService _service = service;

        // GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return _service.GetAllEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> GetEmployee(int id)
        {
            var employee = _service.GetEmployee(id);

            if (employee is null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee([FromBody] UpdateEmployeeDto employee, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.UpdateEmployee(employee, id);
            }
            catch (EmployeeValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }

            return Ok(employee);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<CreateEmployeeDto> CreateEmployee([FromBody] CreateEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.CreateEmployee(employee);
            }
            catch (EmployeeValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }

            return Ok(employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = _service.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            try
            {
                _service.DeleteEmployee(id);
            }
            catch (EmployeeValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }

            return Ok(id);
        }
    }
}
