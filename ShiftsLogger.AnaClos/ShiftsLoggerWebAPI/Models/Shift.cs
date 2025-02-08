namespace ShiftsLoggerWebAPI.Models;

public class Shift
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    public string CalculateDuration(DateTime start, DateTime end)
    {
        TimeSpan duration = end - start;
        string stringDuration = duration.ToString(@"dd hh\:mm\:ss");
        return $"Id: {Id} Start Time: {StartTime} End Time: {EndTime} Duration: {stringDuration}";
    }
    public TimeSpan CalculateDuration()
    {
        return this.EndTime - this.StartTime;
    }
}