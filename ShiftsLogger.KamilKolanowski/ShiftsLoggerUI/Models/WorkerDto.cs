using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftsLoggerUI.Models;

public class WorkerDto
{
    [JsonPropertyName("workerId")]
    public int WorkerId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = String.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = String.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = String.Empty;

    [JsonPropertyName("role")]
    public string Role { get; set; } = String.Empty;
}
