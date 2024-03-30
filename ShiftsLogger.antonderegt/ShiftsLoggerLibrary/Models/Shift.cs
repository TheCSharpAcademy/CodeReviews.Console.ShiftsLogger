using System.Text.Json.Serialization;

namespace ShiftsLoggerLibrary.Models;

public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; set; } = null;

    [JsonPropertyName("endTime")]
    public DateTime? EndTime { get; set; } = null;

    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
}