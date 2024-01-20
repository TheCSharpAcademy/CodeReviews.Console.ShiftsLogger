namespace ShiftsLoggerWebApi.Models;

public class ShiftDto(Shift shift)
{
    public DateTime ShiftStartTime {get; set;} = shift.ShiftStartTime;
    public DateTime? ShiftEndTime {get; set;} = shift.ShiftEndTime;
}