namespace ShiftsLoggerWebApi.Models;

public class ShiftDto()
{
    public DateTime ShiftStartTime {get; set;}
    public DateTime? ShiftEndTime {get; set;}

    public static ShiftDto FromShift(Shift shift)
    {
        var shiftDto = new ShiftDto
        {
            ShiftStartTime = shift.ShiftStartTime,
            ShiftEndTime = shift.ShiftEndTime
        };
        return shiftDto;
    }
}