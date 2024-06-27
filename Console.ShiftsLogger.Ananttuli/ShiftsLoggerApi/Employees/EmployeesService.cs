using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Employees.Models;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Employees;

public class EmployeesService
{
    private readonly ShiftsLoggerContext Db;

    public EmployeesService(ShiftsLoggerContext dbContext)
    {
        Db = dbContext;
    }

    public async Task<Result<Employee>> CreateEmployee(EmployeeCreateDto employeeUpsertDto)
    {
        if (!IsValidName(employeeUpsertDto.Name))
        {
            return Result<Employee>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Employee name must not be empty"
                )
            );
        }

        var employeeToCreate = new Employee
        {
            Name = employeeUpsertDto.Name
        };

        Db.Employees.Add(employeeToCreate);

        await Db.SaveChangesAsync();

        return await FindById(employeeToCreate.EmployeeId);
    }

    public async Task<Result<Employee>> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
    {
        var (existingEmployee, employeeFetchError) = await FindById(employeeUpdateDto.EmployeeId);
        if (employeeFetchError != null || existingEmployee == null)
        {
            return Result<Employee>.Fail(employeeFetchError);
        }

        if (!IsValidName(employeeUpdateDto.Name))
        {
            return Result<Employee>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Employee name must not be empty"
                )
            );
        }

        existingEmployee.Name = employeeUpdateDto.Name;
        Db.Entry(existingEmployee).State = EntityState.Modified;
        await Db.SaveChangesAsync();

        return Result<Employee>.Success(existingEmployee);
    }

    public bool IsValidName(string? Name)
    {
        return Name?.Trim().Length > 0;
    }

    public async Task<Result<Employee>> FindById(int id)
    {
        var shift = await Db.Employees.FindAsync(id);

        if (shift == null)
        {
            return Result<Employee>.Fail(new Error(
                ErrorType.DatabaseNotFound,
                "Could not find shift"
            ));
        }

        return Result<Employee>.Success(shift);
    }
}