using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.API.Models;

public class Shift
{
    public int Id { get; set; }

    [Required]
    public Employee Employee { get; set; }

    [Required]
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
}
