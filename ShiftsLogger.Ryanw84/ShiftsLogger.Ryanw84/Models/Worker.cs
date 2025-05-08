namespace ShiftsLogger.Ryanw84.Models;

public class Worker
{
    public int WorkerId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Shift>? Shifts { get; set; }
    public virtual ICollection<Location>? Locations { get; set; }
}
