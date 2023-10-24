using System.Net;
using Newtonsoft.Json;
using RestSharp;
using ShiftsLogger.UI.Models;

namespace ShiftsLogger.UI.Controllers;

public static class ShiftController
{
    public static List<Shift>? GetShifts()
    {
        var client = new RestClient("http://localhost:5145/api");
        var request = new RestRequest("/shifts");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode != HttpStatusCode.OK) return null;

        var rawResponse = response.Result.Content;
        if (rawResponse == null) return null;

        var shifts = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

        return shifts;
    }
}