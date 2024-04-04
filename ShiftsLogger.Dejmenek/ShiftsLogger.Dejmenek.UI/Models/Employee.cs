using System.Text.Json.Serialization;

namespace ShiftsLogger.Dejmenek.UI.Models;
public class Employee
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = null!;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;
    [JsonPropertyName("shifts")]
    public List<Shift> Shifts { get; set; } = new List<Shift>();
}


public class EmployeeReadDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class EmployeeDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}