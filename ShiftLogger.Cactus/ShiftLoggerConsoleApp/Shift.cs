using System.Text.Json.Serialization;

namespace ShiftLoggerConsoleApp;
public record class Shift(
    [property: JsonPropertyName("Id")] int Id,
    [property: JsonPropertyName("EmployeeName")] string EmployeeName,
    [property: JsonPropertyName("ShiftDate")] DateTime ShiftDate,
    [property: JsonPropertyName("ShiftStartTime")] TimeSpan ShiftStartTime,
    [property: JsonPropertyName("ShiftEndTime")] TimeSpan ShiftEndTime,
    [property: JsonPropertyName("TotalHoursWorked")] double TotalHoursWorked)
{
}

