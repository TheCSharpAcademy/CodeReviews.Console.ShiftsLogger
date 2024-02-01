using Newtonsoft.Json;

namespace ShiftsLoggerUI.Models;

public class Shift
{
    [JsonProperty("ShiftId")]
    public int ShiftId { get; set; }
    [JsonProperty("EmployeeId")]
    public int EmployeeId { get; set; }
    [JsonProperty("StartTime")]
    public  DateTime StartTime { get; set; }
    [JsonProperty("EndTime")]
    public  DateTime EndTime { get; set; }
    [JsonProperty("Comment")]
    public string Comment { get; set; } = "";
    public TimeSpan Duration { get; set; }
}