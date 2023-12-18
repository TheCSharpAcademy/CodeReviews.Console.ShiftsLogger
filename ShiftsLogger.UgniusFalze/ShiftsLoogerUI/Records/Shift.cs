using System.Text.Json.Serialization;

namespace ShiftsLoogerUI.Records;

public record Shift(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("shiftId")]
    int ShiftId,
    [property: JsonPropertyName("shiftStart")]
    DateTime ShiftStart,
    [property: JsonPropertyName("shiftEnd")]
    DateTime ShiftEnd,
    [property: JsonPropertyName("comment")]
    string? comment)
{
    public int Duration => (ShiftEnd - ShiftStart).Seconds;
}