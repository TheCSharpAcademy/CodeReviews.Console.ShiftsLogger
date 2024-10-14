namespace ShiftsLogger;

public static class ShiftMapper
{
    public static ShiftDto MapToDto(Shift shift)
    {
        if (shift == null) return null;
        return new ShiftDto
        {
            ShiftId = shift.ShiftId,
            Start = shift.Start,
            End = shift.End,
            EmployeeName = shift.Employee.Name ?? string.Empty,
        };
    }
}