namespace ShiftsLogger.frockett.API.DTOs;

public class ShiftCreateDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }
}
