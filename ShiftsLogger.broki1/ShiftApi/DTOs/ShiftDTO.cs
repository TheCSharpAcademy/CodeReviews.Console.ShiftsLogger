using System.Text.Json.Serialization;

namespace ShiftApi.DTOs;

public class ShiftDTO
{
    [property: JsonPropertyName("shiftId")]
    public int ShiftId { get; set; }
    [property: JsonPropertyName("shiftStartTime")]
    public DateTime ShiftStartTime { get; set; }
    [property: JsonPropertyName("shiftEndTime")]
    public DateTime ShiftEndTime { get; set; }
}
