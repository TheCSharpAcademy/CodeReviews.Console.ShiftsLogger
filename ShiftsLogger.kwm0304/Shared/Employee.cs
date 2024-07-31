using Shared.Enums;

namespace Shared;

public class Employee
{
  public int EmployeeId { get; set; }
  public string? Name { get; set; }

  public ShiftClassification ShiftAssignment { get; set; }
  public double PayRate { get; set; }
  public List<EmployeeShift>? EmployeeShifts { get; set; }
}