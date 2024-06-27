using System.Text.Json.Serialization;
using ShiftsLoggerUi.Api.Employees;

namespace ShiftsLoggerUi.Api.Shifts;

public record class ShiftDto(
    [property: JsonPropertyName("shiftId")] int ShiftId,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("endTime")] DateTime EndTime,
    [property: JsonPropertyName("employee")] EmployeeDto Employee,
    [property: JsonPropertyName("duration")] TimeSpan Duration
)
{
    public override string ToString()
    {
        return $"{StartTime}\t{EndTime}\t{Duration}\t{Employee.Name}";
    }
}