using System.Text.Json.Serialization;

namespace ShiftsLoggerUi.Api.Employees;

public record class EmployeeCoreDto(
    [property: JsonPropertyName("employeeId")] int EmployeeId,
    [property: JsonPropertyName("name")] string Name
)
{ }