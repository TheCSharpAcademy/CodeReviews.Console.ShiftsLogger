using System.Text.Json.Serialization;

namespace ShiftsLogger.Dejmenek.UI.Models;
public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("employeeFirstName")]
    public string EmployeeFirstName { get; set; } = null!;
    [JsonPropertyName("employeeLastName")]
    public string EmployeeLastName { get; set; } = null!;
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }
    [JsonPropertyName("duration")]
    public string Duration { get; set; } = null!;
}

public class ShiftReadDTO
{
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}

public class ShiftAddDTO
{
    public int EmployeeID { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}

public class ShiftUpdateDTO
{
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}
