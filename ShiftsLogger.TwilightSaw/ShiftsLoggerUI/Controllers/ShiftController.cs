using System.Text;
using System.Text.Json;
using ShiftsLoggerUI.Model;


namespace ShiftsLoggerUI.Controllers;

public class ShiftController(HttpClient client)
{
    public async Task<List<Shift>> GetShifts()
    {
        var url = $"https://localhost:7070/api/Shift";
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<ShiftDto>>(content);

        return result.Select(s => s.ToShift()).ToList();
    }

    public async Task CreateShift(Shift shift)
    {
        var url = $"https://localhost:7070/api/Shift";
        var json = JsonSerializer.Serialize(shift.ToDto());
        var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ресурс створено: {responseBody}");
        }
        else
        {
            Console.WriteLine($"Помилка: {response.StatusCode}");
        }
    }
}