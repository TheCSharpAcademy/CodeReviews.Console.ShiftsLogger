using Newtonsoft.Json;
using RestSharp;
using ShiftLoggerUi.DTOs;

namespace ShiftLoggerUi.Services;

internal class WorkerService
{
    private readonly RestClient _client;

    public WorkerService()
    {
        _client = new RestClient("https://localhost:7225/");
    }

    public List<WorkerDto> GetAllWorkers()
    {
        var request = new RestRequest("workers", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<List<WorkerDto>>(response.Content);
        }

        return new List<WorkerDto>();
    }

    public WorkerDto GetWorkerById(int workerId)
    {
        var request = new RestRequest($"workers/{workerId}", Method.Get);
        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<WorkerDto>(response.Content);
        }

        return null;
    }

    public WorkerDto CreateWorker(WorkerDto worker)
    {
        var request = new RestRequest("workers", Method.Post);
        request.AddJsonBody(worker);

        var response = _client.ExecuteAsync(request).Result;

        if (response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            return JsonConvert.DeserializeObject<WorkerDto>(response.Content);
        }

        return null;
    }

    public bool UpdateWorker(int workerId, WorkerDto worker)
    {
        var request = new RestRequest($"workers/{workerId}", Method.Put);
        request.AddJsonBody(worker);

        var response = _client.ExecuteAsync(request).Result;
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }

    public bool DeleteWorker(int workerId)
    {
        var request = new RestRequest($"workers/{workerId}", Method.Delete);
        var response = _client.ExecuteAsync(request).Result;

        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }
}
