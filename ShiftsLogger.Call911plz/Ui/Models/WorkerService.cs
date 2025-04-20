using RestSharp;
using System.Text.Json;

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

    public async Task<Worker?> CreateWorkerAsync(WorkerDto worker)
    {
        RestResponse? response = await ExecuteRestAsync
        (
            async () => await _client.PostAsync
            (
                new RestRequest($"Workers")
                    .AddJsonBody(JsonSerializer.Serialize(worker))
            )
        );

        if (response == null)
            return null;
        return JsonSerializer.Deserialize<Worker>(response.Content, _jsonOptions);
    }

    public async Task<Worker?> UpdateWorkerAsync(WorkerDto worker)
    {
        RestResponse? response = await ExecuteRestAsync
        (
            async () => await _client.PutAsync
            (
                new RestRequest($"Workers")
                    .AddJsonBody(JsonSerializer.Serialize(worker))
            )
        );

        if (response == null)
            return null;
        return JsonSerializer.Deserialize<Worker>(response.Content, _jsonOptions);
    }

    public async Task<bool> DeleteWorkerAsync(WorkerDto worker)
    {
        RestResponse? response = await ExecuteRestAsync
        (
            async () => await _client.DeleteAsync
            (
                new RestRequest($"Workers/{worker.WorkerId}")
            )
        );

        if (response == null)
            return false;
        return true;
    }
}