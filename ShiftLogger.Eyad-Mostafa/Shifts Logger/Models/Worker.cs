namespace Shifts_Logger.Models;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Shift> Shifts { get; set; } = new();
}
