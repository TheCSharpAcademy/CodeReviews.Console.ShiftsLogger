namespace ShiftsLogger.frockett.API.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Shift> Shifts { get; set; }
}
