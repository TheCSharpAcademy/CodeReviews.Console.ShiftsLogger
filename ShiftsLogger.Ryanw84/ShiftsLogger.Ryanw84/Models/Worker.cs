namespace ShiftsLogger.Ryanw84.Models;

public class Worker
    {
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public ICollection<Shift> Shifts { get; set; }
    public ICollection<Location> Locations { get; set; }
    }