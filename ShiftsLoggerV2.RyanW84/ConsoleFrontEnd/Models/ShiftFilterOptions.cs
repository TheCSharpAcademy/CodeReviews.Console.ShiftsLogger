namespace ConsoleFrontEnd.Models;

internal class ShiftFilterOptions
{
    public int? WorkerId { get; set; }
    public int? LocationId { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
}
