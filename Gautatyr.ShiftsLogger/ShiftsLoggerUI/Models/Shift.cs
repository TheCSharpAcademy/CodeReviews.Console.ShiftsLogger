using System.Text.Json.Serialization;

namespace ShiftsLoggerUI.Models;

public record Shift(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("start")] DateTime Start,
    [property: JsonPropertyName("end")] DateTime End);

public class ShiftDto
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class ShiftDtoDisplay
{
    public int Id { get; set; }
    public string? Start { get; set; }
    public string? End { get; set; }
    public string? Duration { get; set; }
}
