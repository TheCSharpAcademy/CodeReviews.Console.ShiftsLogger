using System.ComponentModel;
using System.Text.Json;
using Models;
using Models.Serialization;
using RestSharp;

namespace ShiftsLogger.Services;

public class ApiService {
    private static readonly string workerEndpoint = "/api/worker";
    private static readonly string shiftEndpoint = "/api/shift";
    private static readonly string workerShiftEndpoint = "/api/workerShift";


    private readonly RestClient _client;

    public ApiService(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl);
        _client = new(options); 
    }

    public List<GetWorkerDto>? GetAllWorkers()
    {
        var request = new RestRequest(workerEndpoint, Method.Get);
        
        try {
            var response = _client.Execute<List<GetWorkerDto>>(request);
            if (response.IsSuccessful) {
                return response.Data!;
            } else {
                return null;
            }
        } catch (Exception)
        {
            return null;
        }
    }

    public void UpdateWorker(PutWorkerDto worker, int id)
    {
        var request = new RestRequest(workerEndpoint + $"/{id}", Method.Put);
        request.AddJsonBody(worker);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                return;
            }
            throw new Exception("Error with the request... status code " + response.StatusCode);
        }catch(Exception) {
            throw;
        }
    }

    public void CreateWorker(PostWorkerDto worker)
    {
        var request = new RestRequest(workerEndpoint, Method.Post);
        request.AddJsonBody(worker);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                return;
            } 
            throw new Exception("Error with the request... status code "+ response.StatusCode);
        }catch(Exception) {
            throw;
        }
    }

    public void DeleteWorker(int id) {
        var request = new RestRequest(workerEndpoint + $"/{id}", Method.Delete);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                return;
            }
            throw new Exception("Error with the request... status code " + response.StatusCode);
        } catch(Exception) {
            throw;
        }
    }

    public List<GetShiftDto>? GetAllShifts()
    {
        var request = new RestRequest(shiftEndpoint, Method.Get);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new TimeOnlyJsonConverter());

                var shifts = JsonSerializer.Deserialize<List<GetShiftDto>>(response.Content!, options);
                return shifts;
            }
            
            throw new Exception("Error: status code " + response.StatusCode);
        }catch(Exception)
        {
            throw;
        }
    }

    public void CreateShift(PostShiftDto shift)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeOnlyJsonConverter());

        var jsonPayload = JsonSerializer.Serialize(shift, options);

        var request = new RestRequest(shiftEndpoint, Method.Post);
        request.AddStringBody(jsonPayload, DataFormat.Json);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful){
                return;
            }
            throw new Exception($"Error creating shift. Code {response.StatusCode}");
        } catch(Exception){
            throw;
        }
    }

    public void UpdateShift(int id, PutShiftDto shift)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeOnlyJsonConverter());

        var jsonPayload = JsonSerializer.Serialize(shift, options);

        var request = new RestRequest(shiftEndpoint + $"/{id}", Method.Put);
        request.AddStringBody(jsonPayload, DataFormat.Json);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful){
                return;
            }
            throw new Exception($"Error creating shift. Code {response.StatusCode}");
        } catch(Exception)
        {
            throw;
        }
    }

    public void DeleteShift(int id)
    {
        var request = new RestRequest(shiftEndpoint + $"/{id}", Method.Delete);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                return;
            }
            throw new Exception($"Error deleting shift. Code {response.StatusCode}");
        }catch (Exception)
        {
            throw;
        }
    }

    public void CreateWorkerShift(PostWorkerShiftDto workerShift)
    {
        var request = new RestRequest(workerShiftEndpoint, Method.Post);
        request.AddJsonBody(workerShift);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful)
            {
                return;
            }
            throw new Exception($"Error creating worker shift: Code {response.StatusCode}");
        } catch(Exception){
            throw;
        }
    } 

    public List<GetWorkerShiftDto>? GetAllWorkerShifts()
    {

        var request = new RestRequest(workerShiftEndpoint, Method.Get);

        try {
            var response = _client.Execute(request);
            
            if (response.IsSuccessful)
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new DateOnlyJsonConverter());
                options.Converters.Add(new TimeOnlyJsonConverter());

                var workerShifts = JsonSerializer.Deserialize<List<GetWorkerShiftDto>>(response.Content!, options);
                return workerShifts;
            }
            throw new Exception($"Error retrieving worker shifts: Code {response.StatusCode}");
        }catch(Exception)
        {
            throw;
        }
    } 

     public void UpdateWorkerShift(int id, PutWorkerShiftDto workerShift)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new TimeOnlyJsonConverter());
        options.Converters.Add(new DateOnlyJsonConverter());

        var jsonPayload = JsonSerializer.Serialize(workerShift, options);

        var request = new RestRequest(workerShiftEndpoint + $"/{id}", Method.Put);
        request.AddStringBody(jsonPayload, DataFormat.Json);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful){
                return;
            }
            throw new Exception($"Error updating worker shift. Code {response.StatusCode}");
        } catch(Exception)
        {
            throw;
        }
    }

    public void DeleteWorkerShift(int id) {
        var request = new RestRequest(workerShiftEndpoint + $"/{id}", Method.Delete);

        try {
            var response = _client.Execute(request);

            if (response.IsSuccessful) {
                return;
            }
            throw new Exception("Error with the request... status code " + response.StatusCode);
        } catch(Exception) {
            throw;
        }
    }
}