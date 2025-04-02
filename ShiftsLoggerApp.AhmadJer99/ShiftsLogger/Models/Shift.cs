using Newtonsoft.Json;

namespace ShiftsLoggerUI.Models;

public class Shift
{
    [JsonProperty("shiftId")]
    public int ShiftId { get; set; }
    [JsonProperty("empId")]
    public int EmpId { get; set; }
    [JsonProperty("startDateTime")]
    public DateTime? ShiftStartTime { get; set; }
    [JsonProperty("endDateTime")]
    public DateTime? ShiftEndTime { get; set; }
    [JsonProperty("shiftDurationHours")]
    public int ShiftDuration { get; set; }
}
