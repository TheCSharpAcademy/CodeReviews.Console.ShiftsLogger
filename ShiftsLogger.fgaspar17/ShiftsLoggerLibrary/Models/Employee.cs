using System.Text.Json.Serialization;

namespace ShiftsLoggerLibrary;

public class Employee
{
    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("shifts")]
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}