using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

public class ShiftsWebService
{
    public HttpClient Client;
    private readonly Uri ShiftsBaseAdress;

    public ShiftsWebService(string appUrl)
    {
        ShiftsBaseAdress = new(appUrl + "/api/Shifts/");
        Client = new()
        {
            BaseAddress = ShiftsBaseAdress,
            Timeout = new TimeSpan(0, 0, 10)
        };
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json-patch+json"));
    }

    public async Task<HttpResponseMessage> GetShifts(int? id)
    {
        return await Client.GetAsync($"{id}");
    }

    public async Task<HttpResponseMessage> PutShift(int? id, ShiftJson shift)
    {
        return await Client.PutAsJsonAsync($"{id}", shift);
    }

    public async Task<HttpResponseMessage> PatchShift(int? id, string patchContent)
    {
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{id}")
        {
            Content = new StringContent(patchContent,
            System.Text.Encoding.UTF8, "application/json-patch+json")
        };
        return await Client.SendAsync(request);
    }
}