using System.Text.Json.Serialization;

namespace ShiftsLoggerUi.Api.Shifts;

public record class ShiftUpdateDto(
    [property: JsonPropertyName("shiftId")] int ShiftId,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("endTime")] DateTime EndTime
)
{ }