using System.Text.Json.Serialization;

namespace ShiftsLoggerClient.Models;

public record ShiftJson
(
    [property:JsonPropertyName("shiftStartTime")]
    DateTime ShiftStartTime,

    [property:JsonPropertyName("shiftEndTime")]
    DateTime? ShiftEndTime );