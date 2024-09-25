using System.Net.Http.Headers;
using System.Text.Json;

namespace ShiftsLoggerLibrary;

public sealed class ShiftsLoggerApiClient
{
    public const string BaseUrl = "https://localhost:7195/api";
    private static readonly Lazy<HttpClient> instance = new
        (() =>
            {
                var httpClient = new HttpClient() { BaseAddress = new Uri(BaseUrl) };
                httpClient.DefaultRequestHeaders.Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
        );

    private ShiftsLoggerApiClient() { }
    

    public static HttpClient Instance { get =>  instance.Value; }

    public async Task<T> GetAsyncDataFromShiftsLogger<T>(string endpoint) where T : class, new()
    {
        await using Stream stream = await Instance.GetStreamAsync(BaseUrl + endpoint);

        return await JsonSerializer.DeserializeAsync<T>(stream) ?? new();
    }
}
