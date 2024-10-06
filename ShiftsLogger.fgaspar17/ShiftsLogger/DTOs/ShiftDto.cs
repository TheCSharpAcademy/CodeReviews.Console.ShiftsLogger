namespace ShiftsLogger;

public class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double DurationMinutes { get => (End - Start).TotalMinutes; }
    public string EmployeeName { get; set; }
}