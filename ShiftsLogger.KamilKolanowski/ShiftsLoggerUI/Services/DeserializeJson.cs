using System.Text.Json;

namespace ShiftsLogger.KamilKolanowski.Services;

internal class DeserializeJson
{
    internal async Task<T> DeserializeAsync<T>(string json)
    {
        return await Task.FromResult(JsonSerializer.Deserialize<T>(json));
    }
}
