using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Models;

public class Shift
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime? ShiftEnd { get; set; }
    public TimeSpan? ShiftDuration { get; set; }
    public bool ShiftOpen { get; set; }
}
