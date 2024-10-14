using System.Text.Json.Serialization;

namespace ShiftsLoggerLibrary;

public class Shift
{
    [JsonPropertyName("shiftId")]
    public int ShiftId { get; set; }
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    public DateTime End { get; set; }
    [JsonPropertyName("durationMinutes")]
    public double DurationMinutes { get; set; }
    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [JsonPropertyName("employeeName")]
    public string EmployeeName { get; set; }
}