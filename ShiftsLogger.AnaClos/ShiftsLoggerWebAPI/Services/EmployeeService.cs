using Microsoft.EntityFrameworkCore;
using ShiftsLoggerWebAPI.Data;
using ShiftsLoggerWebAPI.DTOs;
using ShiftsLoggerWebAPI.Models;

namespace ShiftsLoggerWebAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ShiftsDbContext _dbContext;

    public EmployeeService(ShiftsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string? CreateEmployee(EmployeeDto employeeDto)
    {
        if (employeeDto == null)
        {
            return null;
        }
        try
        {
            Employee employee = Dto2Model(employeeDto);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return $"Successfully created employee with name {employee.Name}.";
        }
        catch (DbUpdateException ex)
        {
            return "Failed to create Employee, name is duplicate";
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string? DeleteEmployee(int id)
    {
        Employee savedEmployee = _dbContext.Employees.Find(id);

        if (savedEmployee == null)
        {
            return null;
        }

        _dbContext.Employees.Remove(savedEmployee);
        _dbContext.SaveChanges();

        return $"Successfully deleted employee with name: {savedEmployee.Name}";
    }

    public List<EmployeeDto>? GetAllEmployees()
    {
        var employees = _dbContext.Employees.ToList();
        if (employees == null)
        {
            return null;
        }
        var employeesDto = new List<EmployeeDto>();
        foreach (var employee in employees)
        {
            employeesDto.Add(Model2Dto(employee));
        }
        return employeesDto;
    }

    public EmployeeDto? GetEmployeeById(int id)
    {
        Employee savedEmployee = _dbContext.Employees.Find(id);
        if (savedEmployee == null)
        {
            return null;
        }
        return Model2Dto(savedEmployee);
    }

    public string? UpdateEmployee(EmployeeDto updatedEmployee)
    {
        Employee savedEmployee = _dbContext.Employees.Find(updatedEmployee.Id);
        if (savedEmployee == null)
        {
            return null;
        }
        try
        {
            _dbContext.Entry(savedEmployee).CurrentValues.SetValues(updatedEmployee);
            _dbContext.SaveChanges();
            return "Successfully updated employee.";
        }
        catch (DbUpdateException ex)
        {
            return "Failed to update employee, name is duplicate";
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static EmployeeDto? Model2Dto(Employee employee)
    {
        if (employee == null)
        {
            return null;
        }

        EmployeeDto employeeDto = new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name
        };
        return employeeDto;
    }

    public static Employee? Dto2Model(EmployeeDto employeeDto)
    {
        if (employeeDto == null)
        {
            return null;
        }

        Employee employee = new Employee
        {
            Id = employeeDto.Id,
            Name = employeeDto.Name
        };
        return employee;
    }
}