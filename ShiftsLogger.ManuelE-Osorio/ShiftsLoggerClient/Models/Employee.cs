using System.Text.Json.Serialization;

namespace ShiftsLoggerClient.Models;

public record Employee
(
    [property:JsonPropertyName("employeeId")] 
    int EmployeeId,
    
    [property:JsonPropertyName("name")] 
    string Name,

    [property: JsonPropertyName("admin")]
    bool Admin
);