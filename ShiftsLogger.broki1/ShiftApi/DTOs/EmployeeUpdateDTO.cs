using System.Text.Json.Serialization;

namespace ShiftApi.DTOs;

public class EmployeeUpdateDTO
{
    [property: JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }
    [property: JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [property: JsonPropertyName("lastName")]
    public string LastName { get; set; }
}