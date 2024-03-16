
namespace ShiftsLogger.frockett.UI.Dtos;

public class ShiftDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
}
