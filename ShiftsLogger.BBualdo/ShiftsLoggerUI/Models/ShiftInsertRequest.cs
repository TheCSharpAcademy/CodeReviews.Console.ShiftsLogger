namespace ShiftsLoggerUI.Models;

internal class ShiftInsertRequest
{
  public string? EmployeeName { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }

  public ShiftInsertRequest(string? employeeName, DateTime? startDate, DateTime? endDate)
  {
    EmployeeName = employeeName;
    StartDate = startDate;
    EndDate = endDate;
  }
}