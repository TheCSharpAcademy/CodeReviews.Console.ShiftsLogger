using System.Text.Json.Serialization;
using ShiftsLoggerUi.Api.Shifts;

namespace ShiftsLoggerUi.Api.Employees;

public record class EmployeeDto(
    [property: JsonPropertyName("employeeId")] string EmployeeId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("shifts")] List<ShiftDto> Shifts
)
{
    public override string ToString()
    {
        return $"{EmployeeId}\t{Name}";
    }
}