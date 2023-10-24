using Newtonsoft.Json;
using RestSharp;
using ShiftsLogger.UI.Exceptions;
using ShiftsLogger.UI.Models;
using Spectre.Console;

namespace ShiftsLogger.UI.Controllers;

public static class ShiftController
{
    private static readonly RestClient Client = new("http://localhost:5145/api");

    public static List<Shift>? GetShifts()
    {
        try
        {
            var request = new RestRequest("/shifts")
            {
                Method = Method.Get
            };
            var response = Client.ExecuteAsync(request);

            if (!response.Result.IsSuccessful) throw new ApiException("Request was not successful.");

            var rawResponse = response.Result.Content;

            if (rawResponse == null) throw new ApiException("No content.");

            var shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

            return shifts;
        }
        catch (ApiException ex)
        {
            var messageParts = ex.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }

        return null;
    }

    public static Shift? GetShiftById(long? id)
    {
        try
        {
            var request = new RestRequest($"/shifts/{id}")
            {
                Method = Method.Get
            };
            var response = Client.ExecuteAsync(request);

            if (!response.Result.IsSuccessful) throw new ApiException("Request was not successful.");

            var rawResponse = response.Result.Content;

            if (rawResponse == null) throw new ApiException("No content.");

            var shift = JsonConvert.DeserializeObject<Shift>(rawResponse);

            return shift;
        }
        catch (ApiException ex)
        {
            var messageParts = ex.Message.Split(":");
            AnsiConsole.MarkupLineInterpolated($"[red]{messageParts[0]}[/]{messageParts[1]}");
        }

        return null;
    }
}