namespace ShiftsLogger.API.Models;
public class Worker
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Shift> Shifts { get; set; }
}

