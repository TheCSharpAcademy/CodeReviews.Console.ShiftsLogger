namespace Server.Models;

public class Shift
{
  public int ShiftId { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }
  public List<EmployeeShift>? EmployeeShifts { get; set; }
}
