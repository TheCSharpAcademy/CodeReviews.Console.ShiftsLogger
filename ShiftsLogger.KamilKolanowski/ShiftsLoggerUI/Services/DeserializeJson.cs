using System.Text.Json;

namespace ShiftsLoggerUI.Services;

internal class DeserializeJson
{
    internal async Task<T> DeserializeAsync<T>(string json)
    {
        return await Task.FromResult(JsonSerializer.Deserialize<T>(json, Options));
    }
    
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
