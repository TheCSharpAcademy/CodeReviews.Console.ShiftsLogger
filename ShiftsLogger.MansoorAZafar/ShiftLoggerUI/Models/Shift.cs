namespace ShiftLoggerUI.Models;
internal class Shift
{
    internal static string[] headers = {"Id", "EmployeeName", "StartTime", "EndTime", "Duration", "Date"};

    public int? Id {get; set;}
    public string? EmployeeName {get; set;}
    public double? StartTime {get; set;}
    public double? EndTime {get; set;}
    public double? Duration {get; set;}
    public DateOnly? Date {get; set;}

    public Shift() {}

    public Shift(string employeeName, double startTime, double endTime, DateOnly date)
    {
        this.EmployeeName = employeeName;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Date = date;
    }

    public Shift(string employeeName, double startTime, double endTime, double duration, DateOnly date)
    {
        this.EmployeeName = employeeName;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Duration = duration;
        this.Date = date;
    }

    public Shift(int id, string employeeName, double startTime, double endTime, double duration, DateOnly date)
    {
        this.Id = id;   
        this.EmployeeName = employeeName;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Duration = duration;
        this.Date = date;
    }
}