namespace Server.Models;

public class Employee
{
  public int EmployeeId { get; set; }
  public string? Name { get; set; }
  
  public int PayRate { get; set; }
  public List<EmployeeShift>? EmployeeShifts { get; set; }
}