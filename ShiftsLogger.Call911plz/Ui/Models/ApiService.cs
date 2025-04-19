using RestSharp;
using Spectre.Console;
using System.Text.Json;
using System.Threading.Tasks;

public class IApiService()
{
    internal RestClient _client;

    public void ConnectApi()
    {
        _client = new(
            new RestClientOptions("http://localhost:5295/api/Shifts/")
        );
    }
}

public class WorkerService() : IApiService()
{
    public async Task<List<Worker>> GetAllWorkersAsync()
    {
        var request = new RestRequest("");
        var response = await _client.GetAsync(request);

        if (response.Content == null)
        {
            AnsiConsole.MarkupLine("No content");
            return [];
        }

        var jsonOptions = new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        };
        var workers = JsonSerializer.Deserialize<List<Worker>>(response.Content, jsonOptions);

        return workers ?? [];
    }
}