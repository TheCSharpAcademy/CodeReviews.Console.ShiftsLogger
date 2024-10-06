namespace ShiftsLoggerLibrary;

public class ShiftMapper
{
    public static ShiftDto MapToDto(Shift shift)
    {
        return new ShiftDto
        {
            Id = shift.ShiftId,
            Start = shift.Start,
            End = shift.End,
            DurationMinutes = shift.DurationMinutes,
            EmployeeName = shift.EmployeeName,
        };
    }
}