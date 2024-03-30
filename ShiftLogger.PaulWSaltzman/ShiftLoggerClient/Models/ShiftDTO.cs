using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerClient.Models;

internal class ShiftDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ShiftStart { get; set; }
    public DateTime? ShiftEnd { get; set; }
    public TimeSpan? ShiftDuration { get; set; }
    public bool ShiftOpen { get; set; }
}