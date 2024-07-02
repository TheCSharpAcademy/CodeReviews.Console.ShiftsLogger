using System.Net;
using Newtonsoft.Json;
using RestSharp;
using ShiftsLogger.UI.Exceptions;
using ShiftsLogger.UI.Models;
using ShiftsLogger.UI.Models.DTOs;

namespace ShiftsLogger.UI.Controllers;

public static class ShiftController
{
    private static readonly RestClient Client = new("http://localhost:5145/api");

    public static async Task<List<Shift>> GetShifts()
    {
        try
        {
            var request = new RestRequest("/shifts")
            {
                Method = Method.Get
            };

            var response = await Client.ExecuteAsync(request);
            if (!response.IsSuccessful) throw new ApiException("Operation was not successful.");

            var rawResponse = response.Content;
            if (rawResponse == null) throw new ApiException("Response content doesn't exist.");

            var shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);
            if (shifts == null) throw new ApiException("Cannot deserialize response content.");

            return shifts;
        }
        catch (Exception)
        {
            throw new ApiException("Problem with the server has occurred. Is API running?");
        }
    }

    public static async Task<Shift> GetShiftById(long id)
    {
        try
        {
            var request = new RestRequest($"/shifts/{id}")
            {
                Method = Method.Get
            };

            var response = await Client.ExecuteAsync(request);
            if (!response.IsSuccessful) throw new ApiException("Operation was not successful.");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ApiException("Cannot find requested resource.");

            var rawResponse = response.Content;
            if (rawResponse == null) throw new ApiException("Response content doesn't exist.");

            var shift = JsonConvert.DeserializeObject<Shift>(rawResponse);
            if (shift == null) throw new ApiException("Cannot deserialize response content.");

            return shift;
        }
        catch (Exception)
        {
            throw new ApiException("Problem with the server has occurred. Is API running?");
        }
    }

    public static async void AddShift(ShiftDto shift)
    {
        try
        {
            await Client.PostJsonAsync("/shifts", shift);
        }
        catch (Exception)
        {
            throw new ApiException("Problem with the server has occurred. Is API running?");
        }
    }

    public static async void DeleteShift(long id)
    {
        try
        {
            var request = new RestRequest($"/shifts/{id}")
            {
                Method = Method.Delete
            };

            var response = await Client.DeleteAsync(request);
            if (!response.IsSuccessful) throw new ApiException("Operation was not successful.");
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ApiException("Cannot find requested resource.");
        }
        catch (Exception)
        {
            throw new ApiException("Problem with the server has occurred. Is API server running?");
        }
    }

    public static async void UpdateShift(long id, ShiftToUpdateDto shift)
    {
        try
        {
            var response = await Client.PutJsonAsync($"/shifts/{id}", shift);

            if (response == HttpStatusCode.BadRequest) throw new ApiException("Bad request.");
            if (response == HttpStatusCode.NotFound) throw new ApiException("Cannot find requested resource");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new ApiException("Problem with the server has occurred. Is API running?");
        }
    }
}