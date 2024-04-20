namespace ShiftsLoggerUI.Models;

internal class ShiftUpdateRequest
{
  public int ShiftId { get; set; }
  public string? EmployeeName { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }

  public ShiftUpdateRequest(int shiftId, string? employeeName, DateTime? startDate, DateTime? endDate)
  {
    ShiftId = shiftId;
    EmployeeName = employeeName;
    StartDate = startDate;
    EndDate = endDate;
  }
}