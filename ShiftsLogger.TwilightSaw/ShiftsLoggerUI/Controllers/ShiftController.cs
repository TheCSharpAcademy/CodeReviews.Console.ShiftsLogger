using System.Text;
using System.Text.Json;
using ShiftsLoggerUI.Helpers;
using ShiftsLoggerUI.Model;


namespace ShiftsLoggerUI.Controllers;

public class ShiftController(HttpClient client)
{
    public async Task<List<Shift>> GetShifts()
    {
        var url = $"https://localhost:7070/api/Shift";
        try
        {
            Validation.Validate(() => client.GetAsync(url), false, out var validationResponse);
            var response = await validationResponse.Invoke();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Bad Request: {response.StatusCode}");
                return [];
            }
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<ShiftDto>>(content);

            if (result != null) return result.Select(s => s.ToShift()).ToList();
            Console.WriteLine("Deserialization error.");
            return [];
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return [];
        }
    }

    public async Task CreateShift(Shift shift)
    {
        var url = $"https://localhost:7070/api/Shift";
        var json = JsonSerializer.Serialize(shift.ToDto());
        Validation.Validate(() => client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")), false, out var validationResponse);

        try
        {
            var response = await validationResponse.Invoke();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Bad Request: {response.StatusCode}");
                return;
            }

            Console.WriteLine($"{(response.IsSuccessStatusCode ? "" : response.StatusCode)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }
        Validation.EndMessage("Your shift has successfully started!");
    }

    public async Task UpdateShift(Shift shift, int id)
    {
        var url = $"https://localhost:7070/api/Shift/{id}";

        var shiftDto = shift.ToDto();
        shiftDto.id = shift.Id;
        var json = JsonSerializer.Serialize(shiftDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        Validation.Validate(() => client.PutAsync(url, content), false, out var validationResponse);
        try
        {
            var response = await validationResponse.Invoke();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Bad Request: {response.StatusCode}");
                return;
            }

            Console.WriteLine(
                $"{(response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : response.StatusCode)}");
        }
        catch (Exception e)
        {
            return;
        }
        Validation.EndMessage("Your shift successfully ended! Good Job!");
    }

    public async Task Delete(int id)
    {
        var url = $"https://localhost:7070/api/Shift/{id}";
        Validation.Validate(() => client.DeleteAsync(url), false, out var validateResponse);
        try
        {
            var response = await validateResponse.Invoke();
            Console.WriteLine($"{(response.IsSuccessStatusCode ? "" : response.StatusCode)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }
        Validation.EndMessage("You successfully deleted this shift.");
    }
}