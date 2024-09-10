using System.Text.Json.Serialization;

namespace ShiftLoggerUI.Models;

public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("employeeName")]
    public string? EmployeeName { get; set; }
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    public DateTime? End { get; set; }
    [JsonPropertyName("duration")]
    public TimeSpan? Duration { get; set; }
}
