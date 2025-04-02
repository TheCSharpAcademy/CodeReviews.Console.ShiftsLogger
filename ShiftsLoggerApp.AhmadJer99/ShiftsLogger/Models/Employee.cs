using Newtonsoft.Json;

namespace ShiftsLoggerUI.Models;

public class Employee
{
    [JsonProperty("empId")]
    public int EmployeeId { get; set; }
    [JsonProperty("empName")]
    public string? EmployeeName { get; set; }
    [JsonProperty("empPhone")]
    public string? EmployeePhone { get; set; }
}
