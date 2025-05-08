using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ShiftsLogger.Ryanw84.Dtos;

public class ShiftApiRequestDto
{
    public string? WorkerName { get; set; }

    public DateTime ShiftStartTime { get; set; }

    public DateTime ShiftEndTime { get; set; }

    public TimeSpan ShiftDuration { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int WorkerId { get; set; }

    public int LocationId { get; set; }
}
