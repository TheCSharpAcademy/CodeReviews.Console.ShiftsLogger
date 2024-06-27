using System.Text.Json.Serialization;
using ShiftsLoggerUi.Api.Shifts;

namespace ShiftsLoggerUi.Api.Employees;

public record class EmployeeCreateDto(
    [property: JsonPropertyName("name")] string Name
)
{ }