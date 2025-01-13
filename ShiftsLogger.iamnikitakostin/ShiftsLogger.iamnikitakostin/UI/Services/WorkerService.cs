using Microsoft.Extensions.Configuration;
using RestSharp;
using UI.Controllers;
using UI.Interfaces;
using UI.Models;

namespace UI.Services;
internal class WorkerService : ConsoleController, IWorkerService
{
    private readonly RestClient client;

    public WorkerService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        var options = new RestClientOptions($"{connectionString}/api/worker/Workers");

        client = new RestClient(options);
    }

    public async Task<List<Worker>> GetAll()
    {
        var request = new RestRequest("", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<List<Worker>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                Console.WriteLine($"Error Message: {response.ErrorMessage}");
                Console.WriteLine($"Content: {response.Content}");
                throw new Exception($"Error fetching workers: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return null;
        }
    }

    public async Task<Worker> GetWorkerById(int id)
    {
        var request = new RestRequest(id.ToString(), Method.Get);

        try
        {
            var response = await client.ExecuteAsync<Worker>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }

            else
            {
                throw new Exception($"Error fetching a worker: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateWorker(Worker worker)
    {
        var request = new RestRequest("", Method.Post);
        request.AddJsonBody(worker);

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            else
            {
                throw new Exception($"Error creating a worker: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateWorker(Worker worker)
    {
        var request = new RestRequest("", Method.Put);
        request.AddJsonBody(worker);

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            else
            {
                throw new Exception($"Error updating a worker: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteWorker(int id)
    {
        var request = new RestRequest(id.ToString(), Method.Delete);

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            else
            {
                throw new Exception($"Error deleting a worker: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }

    public async Task<Dictionary<int, string>> GetAllAsDictionary()
    {
        var request = new RestRequest("", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<List<Worker>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                Dictionary<int, string> workers = response.Data.ToDictionary(w => w.Id, w => $"{w.Department} - {w.Name}");
                return workers;
            }
            else
            {
                throw new Exception($"Error fetching shifts: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return null;
        }
    }
}
