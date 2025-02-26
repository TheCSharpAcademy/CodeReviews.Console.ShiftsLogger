using System.Text.Json.Serialization;

namespace ShiftsLoggerClient.Models;

public class EmployeeDTO
{
    [JsonPropertyName("employeeId")]
    public long EmployeeId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("shiftId")]
    public List<long>? ShiftId { get; set; }
}