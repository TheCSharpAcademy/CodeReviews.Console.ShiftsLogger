using System.Text.Json.Serialization;

internal record class Shift(
    [property: JsonPropertyName("Id")] long id,
    [property: JsonPropertyName("EmployeeName")] string EmployeeName,
    [property: JsonPropertyName("StartOfShift")] DateTime StartOfShift,
    [property: JsonPropertyName("EndOfShift")] DateTime EndOfShift,
    [property: JsonPropertyName("Duration")] TimeSpan Duration);