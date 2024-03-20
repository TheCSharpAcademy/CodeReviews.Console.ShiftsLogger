using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftApi.Models;

public class Employee
{
    [Key]
    [property: JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [property: JsonPropertyName("firstName")]
    public string FirstName { get; set; } = null!;
    [property: JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;
    [property: JsonPropertyName("shifts")]
    public ICollection<Shift> Shifts { get; set; }
}
