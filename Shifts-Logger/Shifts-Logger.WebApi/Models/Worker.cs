namespace ShiftsLogger.WebApi.Models;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Shift> Shifts { get; set; } = new List<Shift>();
}
