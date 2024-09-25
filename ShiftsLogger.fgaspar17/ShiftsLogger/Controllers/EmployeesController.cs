using Microsoft.AspNetCore.Mvc;

namespace ShiftsLogger;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeesController(ShiftContext context, EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Return a list of Employees
    /// </summary>
    /// <returns> A list of Employees </returns>
    /// <remarks>
    /// 
    /// Sample request
    /// GET: /api/employees
    /// 
    /// </remarks>
    /// <response code="200">Return a list of Employees</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        var employees = await _employeeService.GetEmployees();
        return new ActionResult<IEnumerable<EmployeeDto>>(employees);
    }

    /// <summary>
    /// Return an Employee
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be retrieved.</param>
    /// <returns> Employee </returns>
    /// <remarks>
    /// 
    /// Sample request
    /// GET: /api/employees/1
    /// 
    /// </remarks>
    /// <response code="200">Return an Employee</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return employee!;
    }

    /// <summary>
    /// Updates an Employee
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be updated.</param>
    /// <returns> No Content if it's successful </returns>
    /// <remarks>
    /// 
    /// Sample request
    /// PUT: /api/employees/1
    /// 
    /// </remarks>
    /// <response code="204">Return No Content if it's successful</response>
    [HttpPut("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> PutEmployee(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        try
        {
            await _employeeService.UpdateEmployee(employee);

            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return BadRequest();
        }
    }

    /// <summary>
    /// Creates an Employee
    /// </summary>
    /// <returns> Created Employee </returns>
    /// <remarks>
    /// 
    /// Sample request
    /// POST: /api/employees
    /// 
    /// </remarks>
    /// <response code="201">Return Created Employee</response>
    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        await _employeeService.InsertEmployee(employee);

        return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
    }

    /// <summary>
    /// Deletes an Employee
    /// </summary>
    /// <param name="id">The unique identifier of the employee to be deleted.</param>
    /// <returns> No Content if it's successful </returns>
    /// <remarks>
    /// 
    /// Sample request
    /// DELETE: /api/employees/1
    /// 
    /// </remarks>
    /// <response code="200">Return No Content is it's successful</response>
    [HttpDelete("{id}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _employeeService.DeleteEmployeeById(id);

        return NoContent();
    }
}
