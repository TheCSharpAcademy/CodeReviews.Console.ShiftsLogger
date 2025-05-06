namespace ShiftsLogger.Ryanw84.Models;

public class Worker
    {
    internal int WorkerId { get; set; }
    internal string? Name { get; set; }
    internal virtual  ICollection<Shift>? Shifts { get; set; }
    internal virtual  ICollection<Location>? Locations { get; set; } 

	}