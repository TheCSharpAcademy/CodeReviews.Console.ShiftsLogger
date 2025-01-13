using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Shift
{
    [Key]
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? ShiftDuration { get; set; }
}
