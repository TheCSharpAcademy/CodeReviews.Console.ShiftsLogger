using Microsoft.Extensions.Configuration;
using RestSharp;
using UI.Controllers;
using UI.Interfaces;
using UI.Models;

namespace UI.Services;
internal class ShiftService : ConsoleController, IShiftService
{
    private readonly RestClient client;

    public ShiftService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        client = new RestClient($"{connectionString}/api/shift/Shifts/");
    }

    public async Task<List<Shift>> GetAll()
    {
        var request = new RestRequest("", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<List<Shift>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
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

    public async Task<Dictionary<int, string>> GetAllAsDictionary()
    {
        var request = new RestRequest("", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<List<Shift>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                Dictionary<int, string> shifts = response.Data.ToDictionary(s => s.Id, s => $"{s.StartTime} - {s.EndTime}");
                return shifts;
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

    public async Task<Shift?> GetLatestShift()
    {
        var request = new RestRequest("latest", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<Shift>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else if (response.StatusDescription == "Not Found")
            {
                return null;
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

    public async Task<List<Shift>> GetAllByWorker(int workerId)
    {
        var request = new RestRequest($"all-worker-{workerId}", Method.Get);

        try
        {
            var response = await client.ExecuteAsync<List<Shift>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
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

    public async Task<Shift> GetShiftById(int id)
    {
        var request = new RestRequest(id.ToString(), Method.Get);

        try
        {
            var response = await client.ExecuteAsync<Shift>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
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

    public async Task<bool> CreateShift(Shift shift)
    {
        var request = new RestRequest("", Method.Post);
        request.AddJsonBody(shift);

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            else
            {
                throw new Exception($"Error creating a shift: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> UpdateShift (Shift shift)
    {
        var request = new RestRequest("", Method.Put);
        request.AddJsonBody(shift);

        try
        {
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return true;
            }

            else
            {
                throw new Exception($"Error updating a shift: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteShift (int id)
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
                throw new Exception($"Error deleting a shift: {response.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage($"An exception occured: {ex.Message}");
            return false;
        }
    }
}
