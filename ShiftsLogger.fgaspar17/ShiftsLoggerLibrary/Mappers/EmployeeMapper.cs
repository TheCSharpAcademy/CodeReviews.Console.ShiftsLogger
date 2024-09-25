namespace ShiftsLoggerLibrary;

public static class EmployeeMapper
{
    public static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto { Id = employee.EmployeeId, Name = employee.Name };
    }
}