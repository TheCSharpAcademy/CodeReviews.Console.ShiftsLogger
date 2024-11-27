namespace ShiftsLoggerUI.Model;

public class Shift(TimeOnly startTime, TimeOnly? endTime, TimeOnly? duration, DateOnly date)
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; } = startTime;
    public TimeOnly? EndTime { get; set; } = endTime;
    public TimeOnly? Duration { get; set; } = duration;
    public DateOnly Date { get; set; } = date;

    public ShiftDto ToDto()
    {
        var shitDto = new ShiftDto(StartTime.ToString(), EndTime.ToString(), Duration.ToString(), Date.ToString())
            {
                id = Id
            };
        return shitDto;
    }

    public void CalculateDuration()
    {
        
        var duration = (EndTime - StartTime);
        Duration = duration < TimeSpan.Zero ? TimeOnly.FromTimeSpan(duration.Value.Add(TimeSpan.FromHours(24))) : TimeOnly.FromTimeSpan(duration.Value);
    }
}