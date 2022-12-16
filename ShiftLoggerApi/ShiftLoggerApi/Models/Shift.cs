using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerApi.Models;

public class Shift
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; } = null;
    public string? Duration { get; set; } = null;
}