namespace ShiftsLogger;

public static class EmployeeMapper
{
    public static EmployeeDto MapToDto(Employee employee)
    {
        if (employee == null) return null;
        return new EmployeeDto
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            Shifts = employee.Shifts.Select(s => ShiftMapper.MapToDto(s)).ToList(),
        };
    }
}