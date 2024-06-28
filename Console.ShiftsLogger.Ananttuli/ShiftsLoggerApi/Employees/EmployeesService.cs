using Microsoft.EntityFrameworkCore;
using ShiftsLoggerApi.Employees.Models;
using ShiftsLoggerApi.Shifts.Models;
using ShiftsLoggerApi.Util;

namespace ShiftsLoggerApi.Employees;

public class EmployeesService
{
    private readonly ShiftsLoggerContext Db;

    public EmployeesService(ShiftsLoggerContext dbContext)
    {
        Db = dbContext;
    }

    public async Task<Result<EmployeeDto>> CreateEmployee(EmployeeCreateDto employeeCreateDto)
    {
        var (_, nameValidationError) = ValidateName(employeeCreateDto.Name);

        if (nameValidationError != null)
        {
            return Result<EmployeeDto>.Fail(nameValidationError);
        }

        var employeeToCreate = new Employee
        {
            Name = employeeCreateDto.Name
        };

        Db.Employees.Add(employeeToCreate);

        await Db.SaveChangesAsync();

        return await FindDtoById(employeeToCreate.EmployeeId);
    }

    private IQueryable<EmployeeDto> GetEmployeesQuery(int? id = null)
    {
        return Db.Employees
            .Where(e => id == null ? true : e.EmployeeId == id)
            .Select(e =>
                new EmployeeDto(
                    e.EmployeeId,
                    e.Name,
                    e.Shifts.Select(s =>
                        new ShiftCoreDto(
                            s.ShiftId,
                            s.StartTime,
                            s.EndTime
                        )
                    ).ToList()
                )
            );
    }

    public async Task<Result<List<EmployeeDto>>> GetEmployees()
    {
        List<EmployeeDto>? employees = await GetEmployeesQuery().ToListAsync();

        if (employees != null)
        {
            return Result<List<EmployeeDto>>.Success(employees);
        }

        return Result<List<EmployeeDto>>.Fail(
            new Error(ErrorType.DatabaseNotFound, "Could not get employees")
        );
    }

    public async Task<Result<EmployeeDto>> GetEmployee(int id)
    {
        var employeesQuery = GetEmployeesQuery(id);

        var employee = await employeesQuery
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            return Result<EmployeeDto>.Fail(
                new Error(
                    ErrorType.DatabaseNotFound,
                    "Employee not found"
                )
            );
        }

        return Result<EmployeeDto>.Success(employee);
    }

    public async Task<Result<EmployeeDto>> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        if (id != employeeUpdateDto.EmployeeId)
        {
            return Result<EmployeeDto>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Param ID does not match payload ID"
                )
            );
        }

        var (_, nameValidationError) = ValidateName(employeeUpdateDto.Name);

        if (nameValidationError != null)
        {
            return Result<EmployeeDto>.Fail(nameValidationError);
        }

        var (existingEmployee, employeeFetchError) =
            await FindById(employeeUpdateDto.EmployeeId);

        if (employeeFetchError != null || existingEmployee == null)
        {
            return Result<EmployeeDto>.Fail(employeeFetchError);
        }

        existingEmployee.Name = employeeUpdateDto.Name;
        Db.Entry(existingEmployee).State = EntityState.Modified;
        await Db.SaveChangesAsync();

        return await FindDtoById(id);
    }

    public async Task<Result<int?>> DeleteEmployee(int id)
    {
        var (employee, error) = await FindById(id);

        if (error != null || employee == null)
        {
            return Result<int?>.Fail(error);
        }

        Db.Employees.Remove(employee);

        await Db.SaveChangesAsync();

        return Result<int?>.Success(id);
    }

    private static Result<bool> ValidateName(string? Name)
    {
        var isValid = Name?.Trim().Length > 0;

        return isValid ? Result<bool>.Success(true) :
            Result<bool>.Fail(
                new Error(
                    ErrorType.BusinessRuleValidation,
                    "Employee name must not be empty"
                )
            );
    }

    private async Task<Result<Employee>> FindById(int id)
    {
        var employee = await Db.Employees.FindAsync(id);

        if (employee == null)
        {
            return Result<Employee>.Fail(new Error(
                ErrorType.DatabaseNotFound,
                "Could not find employee"
            ));
        }

        return Result<Employee>.Success(employee);
    }

    private async Task<Result<EmployeeDto>> FindDtoById(int id)
    {
        var (employee, error) = await FindById(id);

        if (employee == null)
        {
            return Result<EmployeeDto>.Fail(error);
        }

        return Result<EmployeeDto>.Success(EmployeeMapping.ToDto(employee));
    }
}