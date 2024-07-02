
namespace ShiftsLogger.frockett.API.DTOs;

public class ShiftDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }  
}
