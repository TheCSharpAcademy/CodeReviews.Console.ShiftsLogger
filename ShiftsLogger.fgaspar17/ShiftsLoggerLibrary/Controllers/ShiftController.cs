using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ShiftsLoggerLibrary;

public static class ShiftController
{
    private const string Endpoint = ShiftsLoggerApiClient.BaseUrl + "/shifts";
    public static async Task<List<Shift>> GetShiftsAsync()
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.GetAsync(Endpoint);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<List<Shift>>() ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }
    public static async Task<Shift> GetShiftByIdAsync(int shiftId)
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.GetAsync(Endpoint + $"/{shiftId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<Shift>() ?? new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    public static async Task<Shift> InsertShiftAsync(Shift shift)
    {
        try
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(shift), Encoding.UTF8, "application/json");
            using var responseMessage = await ShiftsLoggerApiClient.Instance.PostAsync(Endpoint, stringContent);

            return await responseMessage.Content.ReadFromJsonAsync<Shift>() ?? new();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return null;
    }

    public static async Task<bool> UpdateShiftAsync(Shift shift)
    {
        try
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(shift), Encoding.UTF8, "application/json");
            using var responseMessage = await ShiftsLoggerApiClient.Instance.PutAsync(Endpoint + $"/{shift.ShiftId}", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return false;
    }

    public static async Task<bool> DeleteShiftByIdAsync(int shiftId)
    {
        try
        {
            using var responseMessage = await ShiftsLoggerApiClient.Instance.DeleteAsync(Endpoint + $"/{shiftId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return false;
    }
}