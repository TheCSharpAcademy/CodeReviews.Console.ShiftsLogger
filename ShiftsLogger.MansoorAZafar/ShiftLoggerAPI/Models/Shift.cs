namespace ShiftLoggerAPI.Models;

public class Shift
{
    public int? Id {get; set;}
    public string? EmployeeName {get; set;}
    public double? StartTime {get; set;}
    public double? EndTime {get; set;}
    public double? Duration {get; set;}
    public DateOnly? Date {get; set;}

    public Shift() {}

    public Shift(string employeeName, double startTime, double endTime, double duration, DateOnly date)
    {
        this.EmployeeName = employeeName;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Duration = duration;
        this.Date = date;
    }
}