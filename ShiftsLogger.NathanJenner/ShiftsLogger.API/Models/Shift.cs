using System.Text.Json.Serialization;

namespace ShiftsLogger.API.Models;

public class Shift
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    [JsonPropertyName("shiftLength")]
    public TimeSpan ShiftLength { get; set; }

    [JsonPropertyName("shiftInProgress")]
    public bool ShiftInProgress { get; set; } = false;
}