using System.Text.Json.Serialization;

namespace ShiftsLoogerUI.Records;

public class Shift(string name, int shiftId, DateTime shiftStart, DateTime shiftEnd, string? comment)
{
    [JsonPropertyName("name")] 
    public string Name { get; set; } = name;

    [JsonPropertyName("shiftId")]
    public int ShiftId { get; set; } = shiftId;

    [JsonPropertyName("shiftStart")]
    public DateTime ShiftStart { get; set; } = shiftStart;

    [JsonPropertyName("shiftEnd")]
    public DateTime ShiftEnd { get; set; } = shiftEnd;

    [JsonPropertyName("comment")]
    public string? Comment { get; set; } = comment;

    public double Duration => (ShiftEnd - ShiftStart).TotalHours;
}