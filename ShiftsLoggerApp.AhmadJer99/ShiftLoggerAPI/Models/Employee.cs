using Newtonsoft.Json;

namespace ShiftsLoggerAPI.Models;

public class Employee
{
    [JsonProperty("empId")]
    public int EmpId { get; set; }
    [JsonProperty("empName")]
    public string? EmpName { get; set; }
    [JsonProperty("empPhone")]
    public string? EmpPhone { get; set; }
    public ICollection<Shift>? EmpShifts { get; set; }
}