namespace ShiftsLogger.Ryanw84.Models;

public class Location
    {
    public int LocationId { get; set; }
    public string Name { get; set; }
    public ICollection<Shift> Shifts { get; set; }
    }
