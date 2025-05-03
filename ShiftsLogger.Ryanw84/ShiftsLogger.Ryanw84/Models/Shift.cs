using System;

namespace ShiftsLogger.Ryanw84.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public string Name { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public ICollection<Worker> Workers { get; set; }
    public ICollection<Location> Locations { get; set; }
    public int WorkerId { get; set; }
    public Worker Worker { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
}
