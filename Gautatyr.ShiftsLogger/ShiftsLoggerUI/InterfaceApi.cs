using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ShiftsLoggerUI.Models;

namespace ShiftsLoggerUI;

public static class InterfaceApi
{
    public static async Task<List<Shift>> GetShifts()
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var api_url = $"https://localhost:7060/api/Shifts";

        await using Stream stream = await client.GetStreamAsync(api_url);

        var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);

        return shifts;
    }

    public static async Task<Shift> GetShift(int id)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var api_url = $"https://localhost:7060/api/Shifts/{id}";

        await using Stream stream = await client.GetStreamAsync(api_url);

        var shift = await JsonSerializer.DeserializeAsync<Shift>(stream);

        return shift;
    }

    public static async Task<HttpResponseMessage> CreateShift(DateTime start, DateTime end)
    {
        var shift = new ShiftDTO
        {
            Start = start,
            End = end
        };

        var jsonString = JsonSerializer.Serialize(shift);
        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        using HttpClient client = new();
        
        var api_url = $"https://localhost:7060/api/Shifts";

        var response = await client.PostAsync(api_url, httpContent);
        return response;
    }

    public static async Task<HttpResponseMessage> UpdateShift(int id, DateTime start, DateTime end)
    {
        var shift = new ShiftDTO
        {
            Id = id,
            Start = start,
            End = end
        };

        var jsonString = JsonSerializer.Serialize(shift);
        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        using HttpClient client = new();

        var api_url = $"https://localhost:7060/api/Shifts/{id}";

        var response = await client.PutAsync(api_url, httpContent);
        return response;
    }


    public static async Task<HttpResponseMessage> DeleteShift(int id)
    {
        using HttpClient client = new();

        var api_url = $"https://localhost:7060/api/Shifts/{id}";

        var response = await client.DeleteAsync(api_url);
        return response;
    }
}
