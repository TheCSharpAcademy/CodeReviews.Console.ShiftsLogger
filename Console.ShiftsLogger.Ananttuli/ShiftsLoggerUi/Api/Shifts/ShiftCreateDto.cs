using System.Text.Json.Serialization;

namespace ShiftsLoggerUi.Api.Shifts;

public record class ShiftCreateDto(
    [property: JsonPropertyName("employeeId")] int EmployeeId,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("endTime")] DateTime EndTime
)
{ }