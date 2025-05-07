namespace ShiftsLogger.Ryanw84.Models;

public class Worker
	{
	public int WorkerId { get; set; }
	public string? Name { get; set; }

	public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
	}
