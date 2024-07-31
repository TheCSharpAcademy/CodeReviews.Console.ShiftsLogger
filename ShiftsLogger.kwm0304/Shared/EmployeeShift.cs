namespace Shared;

public class EmployeeShift
{
  public int EmployeeId { get; set; }
  public Employee? Employee { get; set; }

  public int ShiftId { get; set; }
  public Shift? Shift { get; set; }

  public DateTime ClockInTime { get; set; }
  public DateTime ClockOutTime { get; set; }
}