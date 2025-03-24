using ShiftsLogger.weakiepedia.Data;
using ShiftsLogger.weakiepedia.Models;

namespace ShiftsLogger.weakiepedia.Services;

public interface IEmployeeService
{
    public List<Employee>? GetAllEmployees();
    public List<Shift>? GetShiftsByEmployeeId(int id);
    public Employee? GetEmployeeById(int id);
    public Employee CreateEmployee(Employee employee);
    public Employee? UpdateEmployee(int id, Employee employee);
    public string? DeleteEmployee(int id);
}

public class EmployeeService : IEmployeeService
{
    private ShiftsDbContext _db;

    public EmployeeService(ShiftsDbContext db)
    {
        _db = db;
    }
    
    public List<Employee>? GetAllEmployees()
    {
        var employees = _db.Employees.ToList();
        
        return employees.Any() ? employees : null;
    }
    
    public List<Shift>? GetShiftsByEmployeeId(int id)
    {
        if (_db.Employees.Find(id) == null)
            return null;
        
        var shifts = _db.Shifts.Where(s => s.EmployeeId == id).ToList();
        
        return shifts.Any() ? shifts : null;
    }
    
    public Employee? GetEmployeeById(int id)
    {
        return _db.Employees.Find(id);
    }
    
    public Employee CreateEmployee(Employee employee)
    {
        _db.Employees.Add(employee);
        _db.SaveChanges();
        
        return employee;
    }
    
    public Employee? UpdateEmployee(int id, Employee employee)
    {
        var savedEmployee = _db.Employees.Find(id);

        if (savedEmployee == null || savedEmployee.Id != id) return null;
        
        _db.Entry(savedEmployee).CurrentValues.SetValues(employee);
        _db.SaveChanges();
        
        return savedEmployee;
    }

    public string? DeleteEmployee(int id)
    {
        var employee = _db.Employees.Find(id);
        
        if (employee == null) return null;
        
        _db.Employees.Remove(employee);
        _db.SaveChanges();
        
        return "Successfully found and deleted the employee.";
    }
}