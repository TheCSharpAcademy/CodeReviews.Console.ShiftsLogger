using ShiftsLoggerApi.Employees.Models;

namespace ShiftsLoggerApi.Shifts.Models;

public class ShiftMapping
{
    public static ShiftCoreDto ToCoreDto(Shift shift)
    {
        return new ShiftCoreDto(
            shift.ShiftId,
            shift.StartTime,
            shift.EndTime
        );
    }

    public static ShiftDto ToDto(Shift shift)
    {
        if (shift.Employee == null)
        {
            throw new Exception("Employee must be included in a Shift");
        }

        return new ShiftDto(
            shift.ShiftId,
            shift.StartTime,
            shift.EndTime,
            EmployeeMapping.ToCoreDto(shift.Employee)
        );
    }
}