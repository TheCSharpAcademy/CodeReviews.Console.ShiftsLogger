using System.Text.Json.Serialization;
using ShiftsLoggerUi.Api.Shifts;

namespace ShiftsLoggerUi.Api.Employees;

public record class EmployeeDto(
    [property: JsonPropertyName("employeeId")] int EmployeeId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("shifts")] List<ShiftCoreDto> Shifts
)
{ }