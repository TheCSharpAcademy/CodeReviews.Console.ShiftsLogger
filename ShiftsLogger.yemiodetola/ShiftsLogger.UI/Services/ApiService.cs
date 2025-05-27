using Newtonsoft.Json;
using ShiftsLogger.UI.Models;
using System.Text;


namespace ShiftsLogger.UI.Services;

public class ApiService
{
  private readonly HttpClient _httpClient;
  private readonly string _baseUrl;

  public ApiService()
  {
    _httpClient = new HttpClient();
    _baseUrl = "http://localhost:5251/api";
  }


  public async Task<List<Shift>> GetAllShiftsAsync()
  {
    try
    {
      var response = await _httpClient.GetAsync($"{_baseUrl}/shifts");

      if (response.IsSuccessStatusCode)
      {
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<Shift>>(json) ?? new List<Shift>();
      }
      else
      {
        Console.WriteLine($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
        return new List<Shift>();
      }
    }
    catch (HttpRequestException ex)
    {
      Console.WriteLine($"Network error: {ex.Message}");
      return new List<Shift>();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Unexpected error: {ex.Message}");
      return new List<Shift>();
    }
  }


  public async Task<Shift?> GetShiftByIdAsync(int id)
  {
    try
    {
      var response = await _httpClient.GetAsync($"{_baseUrl}/shifts/{id}");

      if (response.IsSuccessStatusCode)
      {
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Shift>(json);
      }
      else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        return null;
      }
      else
      {
        Console.WriteLine($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
        return null;
      }
    }
    catch (HttpRequestException ex)
    {
      Console.WriteLine($"Network error: {ex.Message}");
      return null;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Unexpected error: {ex.Message}");
      return null;
    }
  }


  public async Task<bool> CreateShiftAsync(Shift shift)
  {
    try
    {
      var json = JsonConvert.SerializeObject(shift);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      var response = await _httpClient.PostAsync($"{_baseUrl}/shifts", content);

      if (response.IsSuccessStatusCode)
      {
        return true;
      }
      else
      {
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Error: {response.StatusCode} - {errorContent}");
        return false;
      }
    }
    catch (HttpRequestException ex)
    {
      Console.WriteLine($"Network error: {ex.Message}");
      return false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Unexpected error: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> UpdateShiftAsync(int id, Shift shift)
  {
    try
    {
      var json = JsonConvert.SerializeObject(shift);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      var response = await _httpClient.PutAsync($"{_baseUrl}/shifts/{id}", content);

      if (response.IsSuccessStatusCode)
      {
        return true;
      }
      else
      {
        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"API Error: {response.StatusCode} - {errorContent}");
        return false;
      }
    }
    catch (HttpRequestException ex)
    {
      Console.WriteLine($"Network error: {ex.Message}");
      return false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Unexpected error: {ex.Message}");
      return false;
    }
  }


  public async Task<bool> DeleteShiftAsync(int id)
  {
    try
    {
      var response = await _httpClient.DeleteAsync($"{_baseUrl}/shifts/{id}");

      if (response.IsSuccessStatusCode)
      {
        return true;
      }
      else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        Console.WriteLine("Shift not found.");
        return false;
      }
      else
      {
        Console.WriteLine($"API Error: {response.StatusCode} - {response.ReasonPhrase}");
        return false;
      }
    }
    catch (HttpRequestException ex)
    {
      Console.WriteLine($"Network error: {ex.Message}");
      return false;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Unexpected error: {ex.Message}");
      return false;
    }
  }

  public void Dispose()
  {
    _httpClient?.Dispose();
  }

}