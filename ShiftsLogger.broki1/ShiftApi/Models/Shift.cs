using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShiftApi.Models;

public class Shift
{
    [Key]
    [property: JsonPropertyName("shiftId")]
    public int ShiftId { get; set; }
    [property: JsonPropertyName("shiftStartTime")]
    public DateTime ShiftStartTime { get; set; }
    [property: JsonPropertyName("shiftEndTime")]
    public DateTime ShiftEndTime { get; set; }
    [property: JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
}
