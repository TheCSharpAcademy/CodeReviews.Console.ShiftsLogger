namespace ShiftsLoggerAPI.Models;

public class EmployeeDTO
{
    public long EmployeeId { get; set; }

    public string? Name { get; set; }

    public List<long>? ShiftId { get; set; }
}