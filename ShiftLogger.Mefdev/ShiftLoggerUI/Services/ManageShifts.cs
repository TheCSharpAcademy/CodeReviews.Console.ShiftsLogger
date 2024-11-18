using RestSharp;
using Spectre.Console;
using ShiftLogger.Mefdev.ShiftLoggerUI.Dtos;

namespace ShiftLogger.Mefdev.ShiftLoggerUI.Services;

public class ManageShifts
{
    private readonly RestClient _client;

    public ManageShifts()
    {
        _client = new RestClient("https://localhost:7263/api/");
    }

    public async Task<List<WorkerShiftDto>> GetWorkerShifts(){
        try
        {
            var request = new RestRequest("workerShift", Method.Get);
            var shift = await _client.GetAsync<List<WorkerShiftDto>>(request);
            return shift ?? new List<WorkerShiftDto>();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            throw;
        }
    }

    public async Task<WorkerShiftDto?> GetWorkerShift(int id)
    {
        try
        {
            var request = new RestRequest($"workerShift/{id}", Method.Get);
            var shift = await _client.GetAsync<WorkerShiftDto>(request);
            return shift;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            throw;
        }
    }

    public async Task<WorkerShiftDto?> CreateWorkerShift(WorkerShiftDto workerShift)
    {
        try
        {
            var request = new RestRequest($"workerShift/", Method.Post);
            var payload = new
            {
                name = workerShift.Name,
                start = workerShift.Start,
                end = workerShift.End
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);

            var response = await _client.ExecuteAsync<WorkerShiftDto>(request);
            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                throw new Exception($"API Error: {response.StatusCode} - {response.Content}");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            AnsiConsole.Confirm("Press any key to continue...");
            throw;
        }
    }

    public async Task<WorkerShiftDto?> UpdateWorkerShift(int id, WorkerShiftDto workerShift)
    {
        try
        {
            var request = new RestRequest($"workerShift/{id}", Method.Put);
            var oldShift = await _client.GetAsync<WorkerShiftDto>(request);
            if(oldShift is null)
            {
                return null;
            }
            var newShift = new WorkerShiftDto(oldShift.Id, workerShift.Name, workerShift.Start, workerShift.End);
            request.AddJsonBody(newShift, contentType: ContentType.Json);
            var shift = await _client.PutAsync<WorkerShiftDto>(request);
            return shift;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            throw;
        }
    }

    public async Task<bool> DeleteWorkerShift(int id)
    {
        try
        {
            var request = new RestRequest($"workerShift/{id}", Method.Delete);
            var oldShift = await _client.GetAsync<WorkerShiftDto>(request);
            if (oldShift is null)
            {
                return false;
            }
            await _client.DeleteAsync<WorkerShiftDto>(request);
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
            throw;
        }
    }
}