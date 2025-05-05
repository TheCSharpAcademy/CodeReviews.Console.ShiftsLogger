using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ShiftsLogger.Ryanw84.Dtos;

public class ShiftApiRequestDto
{
    [Required]
    public string ShiftNumber { get; set; } = string.Empty;

    [Required]
    public string WorkerName { get; set; }

    [Required]
    public DateTime ShiftStartTime { get; set; }

    [Required]
    public DateTime ShiftEndTime { get; set; }

    [Required]
    public TimeSpan ShiftDuration { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [Required]
    public int WorkerId { get; set; }

    [Required]
    public int LocationId { get; set; }
}
