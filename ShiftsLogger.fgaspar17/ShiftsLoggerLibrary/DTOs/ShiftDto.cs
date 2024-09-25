namespace ShiftsLoggerLibrary;

public class ShiftDto
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double DurationMinutes { get; set; }
    public string EmployeeName { get; set; }
}