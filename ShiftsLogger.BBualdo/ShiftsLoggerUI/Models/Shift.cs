using System.Text.Json.Serialization;

namespace ShiftsLoggerUI.Models;

internal record class Shift(
                            [property: JsonPropertyName("shiftId")] int ShiftId,
                            [property: JsonPropertyName("employeeName")] string EmployeeName,
                            [property: JsonPropertyName("startDate")] DateTime StartDate,
                            [property: JsonPropertyName("endDate")] DateTime EndDate
                           );