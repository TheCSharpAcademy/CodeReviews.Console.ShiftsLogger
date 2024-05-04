namespace ShiftLogger.Cactus.DataModel;

public class ShiftLogger
{
    public long Id { get; set; }
    public string EmployeeName { get; set; }
    public DateTime ShiftDate { get; set; }
    public TimeSpan ShiftStartTime { get; set; }
    public TimeSpan ShiftEndTime { get; set; }
    public double TotalHoursWorked { get; private set; }

    public ShiftLogger(string employeeName, DateTime shiftDate, TimeSpan shiftStartTime, TimeSpan shiftEndTime)
    {
        EmployeeName = employeeName;
        ShiftDate = shiftDate;
        ShiftStartTime = shiftStartTime;
        ShiftEndTime = shiftEndTime;
        CalculateTotalHoursWorked();
    }

    private void CalculateTotalHoursWorked()
    {
        TimeSpan totalShiftHours = ShiftEndTime - ShiftStartTime;
        TotalHoursWorked = totalShiftHours.TotalHours;
    }
}
