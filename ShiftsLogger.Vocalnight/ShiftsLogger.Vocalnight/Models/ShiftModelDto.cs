namespace ShiftLoggerConsole.Models;

public class ShiftModelDto
{
    public int day { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public string duration { get; set; }

    public int EmployeeId { get; set; }
}
