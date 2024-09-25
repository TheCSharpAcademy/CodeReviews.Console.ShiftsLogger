using System.Text.Json.Serialization;

namespace ShiftsLogger;

public class Shift
{
    public int ShiftId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int EmployeeId { get; set; }
    [JsonIgnore]
    public Employee? Employee { get; set; }
}