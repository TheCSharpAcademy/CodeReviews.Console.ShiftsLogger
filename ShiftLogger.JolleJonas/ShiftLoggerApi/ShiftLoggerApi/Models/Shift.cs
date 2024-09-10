using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerApi.Models;

public class Shift
{
    public int Id { get; set; }
    public string? EmployeeName { get; set; }
    [Required]
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan? Duration { get; set; }
}
