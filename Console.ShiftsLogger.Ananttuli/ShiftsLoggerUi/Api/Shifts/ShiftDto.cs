using System.Text.Json.Serialization;
using ShiftsLoggerUi.Api.Employees;

namespace ShiftsLoggerUi.Api.Shifts;

public record class ShiftDto(
    [property: JsonPropertyName("shiftId")] int ShiftId,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("endTime")] DateTime EndTime,
    [property: JsonPropertyName("employee")] EmployeeCoreDto Employee
)
{ }