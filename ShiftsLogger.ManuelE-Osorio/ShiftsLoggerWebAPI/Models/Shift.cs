namespace ShiftsLoggerWebApi.Models;

public class Shift
{
    public int ShiftId {get; set;}
    public int EmployeeId {get; set;}
    public DateTime ShiftStartTime {get; set;}
    public DateTime? ShiftEndTime {get; set;}

    public static Shift FromShiftDto(ShiftDto shiftDto)
    {
        var shift = new Shift
        {
            ShiftStartTime = shiftDto.ShiftStartTime,
            ShiftEndTime = shiftDto?.ShiftEndTime
        };
        return shift;
    }
}