namespace ShiftsLogger.Console.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public long DurationInSeconds { get; set; }

    public Shift(int employeeId, DateTime startTime, DateTime endTime)
    {
        EmployeeId = employeeId;
        StartTime = startTime;
        EndTime = endTime;
    }
}