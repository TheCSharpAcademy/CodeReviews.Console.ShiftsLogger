
namespace ShiftsLogger.Domain;
public class Shift : Entity
{
    public Shift()
    {
    }

    public Shift(string employeeName, string shiftDescription, DateTime shiftStart, DateTime shiftEnd)
    {
        EmployeeName = employeeName;
        ShiftDescription = shiftDescription;
        ShiftStart = shiftStart;
        ShiftEnd = shiftEnd;
        ShiftDuration = shiftEnd - shiftStart;
    }



    public string EmployeeName { get; set; }
    public string ShiftDescription { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public TimeSpan ShiftDuration { get; init; }
}