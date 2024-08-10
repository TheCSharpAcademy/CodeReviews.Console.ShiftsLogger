namespace Server.Models.Dtos;

public class EmployeeShiftDto
{
  public int EmployeeId { get; set; }
  public int ShiftId { get; set; }
  public DateTime ClockInTime { get; set; }
  public DateTime ClockOutTime { get; set; }
}
