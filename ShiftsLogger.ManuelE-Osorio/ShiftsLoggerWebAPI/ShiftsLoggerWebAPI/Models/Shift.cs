namespace ShiftsLoggerWebApi.Models;

public class Shift
{
    public int ShiftId {get; set;}
    public int EmployeeId {get; set;}
    public DateTime ShiftStartTime {get; set;}
    public DateTime? ShiftEndTime {get; set;}
}