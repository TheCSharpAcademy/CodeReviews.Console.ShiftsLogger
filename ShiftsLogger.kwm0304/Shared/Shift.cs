using Shared.Enums;

namespace Shared;

public class Shift
{
  public int ShiftId { get; set; }
  public DateTime StartTime { get;  set; }
  public DateTime EndTime { get;  set; }
  public ShiftClassification _classification;
  public List<EmployeeShift>? EmployeeShifts { get; set; }
  public ShiftClassification Classification
  {
    get => _classification;
    set
    {
      _classification = value;
      SetShiftTimes();
    }
  }
  private void SetShiftTimes()
  {
    DateTime today = DateTime.Today;
    DateTime tomorrow = today.AddDays(1);

    switch (_classification)
    {
      case ShiftClassification.First:
        StartTime = today.AddHours(9);
        EndTime = today.AddHours(17);
        break;
      case ShiftClassification.Second:
        StartTime = today.AddHours(17);
        EndTime = tomorrow.AddHours(1);
        break;
      case ShiftClassification.Third:
        StartTime = tomorrow;
        EndTime = tomorrow.AddHours(8);
        break;
    }
  }
}