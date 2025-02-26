using System.Text.Json.Serialization;

namespace ShiftsLoggerClient.Models;

public class ShiftDTO
{
    [JsonPropertyName("shiftId")]
    public long ShiftId { get; set; }

    [JsonPropertyName("employeeId")]
    public long EmployeeId { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("employeeName")]
    public string Name { get; set; }
}