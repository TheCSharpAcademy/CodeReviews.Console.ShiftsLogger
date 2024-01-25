namespace ShiftsLoggerClient.Models;

public class ShiftDto(ShiftJson shift)
{
    public DateTime ShiftStartTime {get; set;} = shift.ShiftStartTime.ToLocalTime();
    public DateTime? ShiftEndTime {get; set;} = shift.ShiftEndTime?.ToLocalTime();
    public string? Length {get; init;} = ((shift.ShiftEndTime ?? DateTime.Now) - shift.ShiftStartTime)
        .ToString(@"hh\:mm\:ss");
}