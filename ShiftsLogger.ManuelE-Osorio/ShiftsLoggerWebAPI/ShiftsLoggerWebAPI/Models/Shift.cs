namespace ShiftsLoggerWebApi.Models;

public class Shift
{
    public int ShiftId {get; set;}
    public int EmployeeId {get; set;}
    public DateTime ShiftStartTime {get; set;}
    public DateTime? ShiftEndTime {get; set;}

    public static Shift FromShiftDto(ShiftDto shiftDto)
    {
        var shift = new Shift();
        shift.ShiftStartTime = shiftDto.ShiftStartTime;
        shift.ShiftEndTime = shiftDto?.ShiftEndTime;
        return shift;
    }
}