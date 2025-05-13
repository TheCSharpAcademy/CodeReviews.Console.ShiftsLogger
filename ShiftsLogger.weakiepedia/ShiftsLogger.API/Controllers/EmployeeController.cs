using Microsoft.AspNetCore.Mvc;
using ShiftsLogger.weakiepedia.Models;
using ShiftsLogger.weakiepedia.Services;

namespace ShiftsLogger.weakiepedia.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private IEmployeeService _employeeService;
    
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public ActionResult<List<Employee>> GetAllEmployees()
    {
        var result = _employeeService.GetAllEmployees();

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpGet("{id}/shifts")]
    public ActionResult<List<Shift>> GetShiftsByEmployee(int id)
    {
        var result = _employeeService.GetShiftsByEmployeeId(id);

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public ActionResult<Employee> GetEmployeeById(int id)
    {
        var result = _employeeService.GetEmployeeById(id);

        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
    
    [HttpPost]
    public ActionResult<Employee> CreateEmployee(Employee employee)
    {
        var result = _employeeService.CreateEmployee(employee);
        
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public ActionResult<Employee> UpdateEmployee(int id, Employee employee)
    {
        var result = _employeeService.UpdateEmployee(id, employee);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<Employee> DeleteEmployee(int id)
    {
        var result = _employeeService.DeleteEmployee(id);
        
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
}

