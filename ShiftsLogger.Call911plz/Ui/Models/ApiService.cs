using RestSharp;
using Spectre.Console;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

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

public class WorkerService() : IApiService()
{
    public async Task<List<Worker>> GetAllWorkersAsync()
    {
        RestResponse? response = await ExecuteRestAsync
        (
            async() => await _client.GetAsync
            (
                new RestRequest($"Workers")
            ) 
        );

        if (response == null)
            return [];

        List<Worker>? workers = JsonSerializer.Deserialize<List<Worker>>(response.Content, _jsonOptions);
        return workers ?? [];
    }

    public async Task<Worker?> GetWorkerByIdAsync(int id)
    {
        RestResponse? response = await ExecuteRestAsync
        (
            async () => await _client.GetAsync
            (
                new RestRequest($"Workers/{id}")
            )
        );

        if (response == null)
            return null;
        
        Worker? worker = JsonSerializer.Deserialize<Worker>(response.Content, _jsonOptions);
        return worker;
    }
}