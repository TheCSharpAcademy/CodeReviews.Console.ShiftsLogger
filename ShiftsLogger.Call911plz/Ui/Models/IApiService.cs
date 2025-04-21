using RestSharp;
using Spectre.Console;
using System.Text.Json;

public class IApiService()
{
    internal RestClient _client;
    internal JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public void ConnectApi()
    {
        _client = new(
            new RestClientOptions("http://localhost:5295/api/")
        );
    }

    internal async Task<RestResponse?> ExecuteRestAsync(Func<Task<RestResponse>> requestFunc)
    {
        RestResponse response = await requestFunc();

        if (Errors.Codes.TryGetValue(response.StatusCode, out string? value))
        {
            AnsiConsole.MarkupLine(value);
            return null;
        }

        return response;
    }
}

