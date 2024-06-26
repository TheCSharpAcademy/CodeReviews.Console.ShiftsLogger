using ShiftsLoggerUI.Models;
using System.Text;
using System.Text.Json;

namespace ShiftsLoggerUI;

public static class ShiftHttp
{
    private static readonly HttpClient client = new HttpClient();

    internal static async Task<List<Shift>> GetShifts()

    {
        string url = "https://localhost:7189/api/Shifts";
        try
        {
            HttpResponseMessage res = await client.GetAsync(url);

            res.EnsureSuccessStatusCode();
            string resBody = await res.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Shift>>(resBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            throw;
        }
    }

    internal static async Task AddShift(Shift shift)
    {
        try
        {
            string json = JsonSerializer.Serialize(shift);

            StringContent httpContent = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PostAsync("https://localhost:7189/api/Shifts", httpContent);

            res.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    internal static async Task UpdateShift(Shift shift)
    {
        string url = $"https://localhost:7189/api/Shifts/{shift.Id}";

        try
        {
            string json = JsonSerializer.Serialize(shift);

            StringContent httpContent = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await client.PutAsync(url, httpContent);
            res.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    internal static async Task DeleteShift(int id)
    {
        try
        {
            string url = $"https://localhost:7189/api/Shifts/{id}";
            HttpResponseMessage res = await client.DeleteAsync(url);
            res.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");

        }
    }
}