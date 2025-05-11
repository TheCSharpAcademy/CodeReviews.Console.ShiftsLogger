using Microsoft.AspNetCore.Mvc;

namespace ShiftsLoggerV2.RyanW84.Dtos;

public class ShiftFilterOptions
{
    [FromQuery(Name = "startTime")]
    public DateTimeOffset? StartTime { get; set; }

    [FromQuery(Name = "endTime")]
    public DateTimeOffset? EndTime { get; set; }

    [FromQuery(Name = "workerId")]
    public int? WorkerId { get; set; }

    [FromQuery(Name = "locationId")]
    public int? LocationId { get; set; }
}
