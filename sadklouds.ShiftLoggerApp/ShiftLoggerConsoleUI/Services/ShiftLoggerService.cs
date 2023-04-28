using ShiftLoggerConsoleUI.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ShiftLoggerConsoleUI.Services;

internal class ShiftLoggerService : IShiftLoggerService
{
    private static readonly HttpClient _client = new HttpClient();
    
    public async Task<List<DisplayShiftDto>> GetShifts()
    {
        List<DisplayShiftDto?> shifts = new();

        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using (HttpResponseMessage response = await _client.GetAsync("https://localhost:7272/api/shifts"))
        {
            if (response.IsSuccessStatusCode)
            {
                return shifts = response.Content.ReadFromJsonAsync<List<DisplayShiftDto>>().Result;
            }
            else
            {
                await Console.Out.WriteLineAsync($"Error getting shifts: {response.ReasonPhrase}");
                return shifts = null;
            }
        }
    }

    public async Task<DisplayShiftDto> GetShiftsById(int id)
    {
        DisplayShiftDto shift = new();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        using (HttpResponseMessage response = await _client.GetAsync($"https://localhost:7272/api/shifts/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return shift = response.Content.ReadFromJsonAsync<DisplayShiftDto>().Result;
            }
            else
            {
                await Console.Out.WriteLineAsync($"Error getting shift: {response.ReasonPhrase}");
                return shift = null;
            }
        }
    }

    public async Task<string> AddShift(DateTime shiftStart, DateTime shiftEnd) 
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var shift = new AddShiftDto
        { 
            ShiftStart = shiftStart,
            ShiftEnd = shiftEnd
        };
        var json = JsonSerializer.Serialize(shift);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (HttpResponseMessage response = await _client.PostAsync($"https://localhost:7272/api/shifts/", content ))
        {
            if (response.IsSuccessStatusCode)
            {
                return $"Shift successfully added";
            }
            else
            {
                return $"Error adding shift: {response.ReasonPhrase}";
            }
        }
    }

    public async Task<string> UpdateShift(int id , DateTime shiftStart, DateTime shiftEnd)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var updatedShift = new UpdateShiftDto
        {
            ShiftStart = shiftStart,
            ShiftEnd = shiftEnd
        };

        HttpResponseMessage response = await _client.PutAsJsonAsync($"https://localhost:7272/api/shifts/{id}", updatedShift);
        
        if (response.IsSuccessStatusCode)
        {
            return $"Shift successfully updated";
        }
        else
        {
            return $"Error updating shift: {response.ReasonPhrase}";
        }
    }

    public async Task<string> DeleteShift(int id)
    {
        DisplayShiftDto shift = new();
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await _client.DeleteAsync($"https://localhost:7272/api/shifts/{id}");
        {
            if (response.IsSuccessStatusCode)
            {
                return $"Shift sucessfully deleted";
            }
            else
            {
                return $"Error deleting shift: {response.ReasonPhrase}";
            }
        }
    }
}
