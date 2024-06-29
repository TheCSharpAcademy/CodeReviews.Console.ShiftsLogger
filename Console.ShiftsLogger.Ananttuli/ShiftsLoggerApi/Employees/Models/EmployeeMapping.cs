using ShiftsLoggerApi.Shifts.Models;

namespace ShiftsLoggerApi.Employees.Models;

public class EmployeeMapping
{
    public static EmployeeCoreDto ToCoreDto(Employee employee)
    {
        return new EmployeeCoreDto(employee.EmployeeId, employee.Name);
    }

    public static EmployeeDto ToDto(Employee employee)
    {
        return new EmployeeDto(
            employee.EmployeeId,
            employee.Name,
            employee.Shifts
                .Select(ShiftMapping.ToCoreDto)
                .ToList()
        );
    }
}