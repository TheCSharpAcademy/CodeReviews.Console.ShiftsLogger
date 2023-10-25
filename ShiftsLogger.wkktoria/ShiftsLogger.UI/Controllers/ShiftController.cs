using Newtonsoft.Json;
using RestSharp;
using ShiftsLogger.UI.Exceptions;
using ShiftsLogger.UI.Models;
using ShiftsLogger.UI.Models.DTOs;

namespace ShiftsLogger.UI.Controllers;

public static class ShiftController
{
    private static readonly RestClient Client = new("http://localhost:5145/api");

    public static List<Shift> GetShifts()
    {
        try
        {
            var request = new RestRequest("/shifts")
            {
                Method = Method.Get
            };
            var response = Client.ExecuteAsync(request);

            if (!response.Result.IsSuccessful) throw new ApiException("Operation was not successful.");

            var rawResponse = response.Result.Content;

            if (rawResponse == null) throw new ApiException("Response content doesn't exist.");

            var shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

            if (shifts == null) throw new ApiException("Cannot deserialize response content.");

            return shifts;
        }
        catch (Exception)
        {
            throw new ApiException("Cannot connect to server. Is API server running?");
        }
    }

    public static Shift GetShiftById(long id)
    {
        try
        {
            var request = new RestRequest($"/shifts/{id}")
            {
                Method = Method.Get
            };
            var response = Client.ExecuteAsync(request);

            if (!response.Result.IsSuccessful) throw new ApiException("Operation was not successful.");

            var rawResponse = response.Result.Content;

            if (rawResponse == null) throw new ApiException("Response content doesn't exist.");

            var shift = JsonConvert.DeserializeObject<Shift>(rawResponse);

            if (shift == null) throw new ApiException("Cannot deserialize response content.");

            return shift;
        }
        catch (Exception)
        {
            throw new ApiException("Cannot connect to server. Is API server running?");
        }
    }

    public static void AddShift(ShiftDto shift)
    {
        try
        {
            var response = Client.PostJsonAsync("/shifts", shift);

            response.Wait();

            if (!response.IsCompletedSuccessfully) throw new ApiException("Operation was not successful.");
        }
        catch (Exception)
        {
            throw new ApiException("Cannot connect to server. Is API server running?");
        }
    }
}