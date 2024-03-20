using System.Text.Json.Serialization;

namespace ShiftApi.DTOs;

public class EmployeeDTO
{
    [property: JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [property: JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [property: JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [property: JsonPropertyName("shifts")]
    public ICollection<ShiftDTO> Shifts { get; set; }
}
