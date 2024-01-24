using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using NewtonJson = Newtonsoft.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Controllers;

public class ShiftsController
{
    public HttpClient Client;
    private static readonly Uri ShiftsBaseAdress = new("http://localhost:5039/api/Shifts/");

    public ShiftsController()
    {
        Client = new()
        {
            BaseAddress = ShiftsBaseAdress,
            // Timeout = new TimeSpan(0, 0, 10)
        };
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json-patch+json"));
    }

    public async Task<List<Shift>?> GetShifts(int? id)
    {
        var response = await Client.GetAsync($"{id}");
        if(response.IsSuccessStatusCode)
        {
            var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(response.Content.ReadAsStream());
            return shifts;
        }
        else
            throw new Exception(response.StatusCode.ToString());
    }

    public async Task<bool> PutShift(int? id, Shift shift)
    {
        var response = await Client.PutAsJsonAsync($"{id}", shift);
        if(response.StatusCode.Equals(HttpStatusCode.NoContent))
            return true;        // Needs more info on status codes, maybe return a error message string
        else
            return false;
    }

    public async Task<bool> PatchShift(int? id, DateTime endTime)
    {
        var patchEndTime = new JsonPatchDocument<Shift>();
        patchEndTime.Replace( p => p.ShiftEndTime, endTime);

        // var content = JsonSerializer.Serialize(endTime);
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{id}");
        request.Content = new StringContent(NewtonJson.JsonConvert.SerializeObject(patchEndTime), 
            System.Text.Encoding.UTF8, "application/json-patch+json");
        HttpResponseMessage response = await Client.SendAsync(request);
        if(response.StatusCode.Equals(HttpStatusCode.NoContent))
            return true;    // Needs more info on status codes, maybe return a error message string  improve patch methods
        else
            throw new Exception(response.StatusCode.ToString());
    }
}