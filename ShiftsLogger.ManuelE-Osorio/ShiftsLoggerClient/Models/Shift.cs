using System.Text.Json.Serialization;

namespace ShiftsLoggerClient.Models;

public record Shift
(
    [property:JsonPropertyName("shiftStartTime")] 
    DateTime ShiftStartTime,
    
    [property:JsonPropertyName("shiftEndTime")] 
    DateTime? ShiftEndTime
);