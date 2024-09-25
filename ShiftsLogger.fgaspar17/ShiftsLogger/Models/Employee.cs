using System.Text.Json.Serialization;

namespace ShiftsLogger;

public class Employee
{
    public int EmployeeId { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}