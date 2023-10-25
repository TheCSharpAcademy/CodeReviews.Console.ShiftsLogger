using System.Text.Json.Serialization;

namespace ShiftsLogger.API.DTOs.Worker;
public class GetWorkerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;
}
