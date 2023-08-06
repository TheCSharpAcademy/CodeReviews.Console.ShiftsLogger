namespace ShiftLoggerConsole.Models;

public class ShiftModelDto
{
    public int Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; }

    public int EmployeeId { get; set; }
}
