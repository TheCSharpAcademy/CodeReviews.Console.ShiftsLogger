using System.Text.Json.Serialization;

namespace ShiftLoggerConsoleApp;
public record Shift(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("employeeName")] string EmployeeName,
    [property: JsonPropertyName("shiftDate")] DateTime ShiftDate,
    [property: JsonPropertyName("shiftStartTime")] TimeSpan ShiftStartTime,
    [property: JsonPropertyName("shiftEndTime")] TimeSpan ShiftEndTime,
    [property: JsonPropertyName("totalHoursWorked")] double TotalHoursWorked);
