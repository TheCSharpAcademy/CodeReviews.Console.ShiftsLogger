using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Backend.Data.Model;

public class Shift
{
    [Key]
    public long Id { get; set; }
    public string EmployeeName { get; set; }
    public string ShiftDescription { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime ShiftEnd { get; set; }
    public TimeSpan ShiftDuration { get; set; }
}