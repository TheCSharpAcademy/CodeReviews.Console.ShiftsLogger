using System.Data.Common;
using Newtonsoft.Json;
using RestSharp;
using Shiftlogger.UI.DTOs;

namespace Shiftlogger.UI.Services;

internal class WorkerService
{
    internal string ApiBaseUrl = "https://localhost:7225/api/";

    internal async Task<List<WorkerRequestDto>>? GetWorkers()
    {
        if (!await IsApiRunning())
        {
            Console.WriteLine("API is not running.");
            return null;
        }
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("worker");
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                return JsonConvert.DeserializeObject<List<WorkerRequestDto>>(rawResponse);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                return null;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch workers.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch workers.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<WorkerRequestDto?> GetWorkerById(int id)
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("worker/{id}")
                .AddUrlSegment("id", id);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                var worker = JsonConvert.DeserializeObject<WorkerRequestDto>(rawResponse);
                return worker;
            }
            return null;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch workers.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch workers.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<WorkerRequestDto?> AddWorker(WorkerNewDto worker)
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("worker", Method.Post);
            request.AddJsonBody(worker);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                string rawResponse = response.Content;
                var returnedWorker = JsonConvert.DeserializeObject<WorkerRequestDto>(rawResponse);
                return returnedWorker;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                {
                    Console.WriteLine(response.Content);
                }
                return null;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch workers.");
            Console.WriteLine(httpEx.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch workers.");
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    internal async Task<bool> PutWorker(int id, WorkerDto worker)
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("worker/{id}", Method.Put);
            request.AddUrlSegment("id", id);
            request.AddJsonBody(worker);
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                    Console.WriteLine(response.Content);
                return false;
            }
            return true;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch workers.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch workers.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    internal async Task<bool> DeleteWorker(int id)
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("worker/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                Console.Clear();
                Console.WriteLine($"Error: {response.StatusCode}\nError Message: {response.ErrorMessage} {response.ErrorException}");
                if (response.Content != null)
                {
                    Console.WriteLine(response.Content);
                }
                return false;
            }
            return true;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to fetch workers.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to fetch workers.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> IsApiRunning()
    {
        try
        {
            var client = new RestClient(ApiBaseUrl);
            var request = new RestRequest("health");
            var response = await client.ExecuteAsync(request);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException httpEx)
        {
            Console.Clear();
            Console.WriteLine("Network error while trying to check API status.");
            Console.WriteLine(httpEx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("An error occurred while trying to check API status.");
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}