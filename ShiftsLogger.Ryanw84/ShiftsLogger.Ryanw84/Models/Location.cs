namespace ShiftsLogger.Ryanw84.Models;

public class Location
{
    public int LocationId { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<Shift>? Shifts { get; set; }
}
