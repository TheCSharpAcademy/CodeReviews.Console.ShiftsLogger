using System.Net.Http.Json;
using Spectre.Console;

namespace Client.Api;

public class IBaseApi<T> where T : class
{
  private readonly HttpClient _http;
  private readonly string _endpoint;
private const string baseUrl = "https://localhost:7066/api";
  public IBaseApi(HttpClient http, string endpoint)
  {
    _http = http;
    _http.BaseAddress = new Uri("https://localhost:7066/api");
    _endpoint = endpoint;
  }

  public async Task CreateAsync(T entity)
  {
    var response = await _http.PostAsJsonAsync(_endpoint, entity);
    response.EnsureSuccessStatusCode();
  }

  public async Task DeleteByIdAsync(int id)
  {
    var response = await _http.DeleteAsync($"{_endpoint}/{id}");
    response.EnsureSuccessStatusCode();
  }

  public async Task<List<T>> GetAllAsync()
  {
    
    return await _http.GetFromJsonAsync<List<T>>(baseUrl + _endpoint) ?? [];
  }

  public async Task<T> GetByIdAsync(int id)
  {
    try
    {
      T entity = await _http.GetFromJsonAsync<T>($"{_endpoint}/{id}");
      if (entity == null)
      {
        AnsiConsole.WriteLine("Nothing found with this id.");
        return default!;
      }
      return entity;
    }
    catch (Exception ex)
    {
      AnsiConsole.WriteLine($"Error retrieving entity with id {id}: {ex.Message}");
      return default!;
    }
  }
  public async Task UpdateAsync(int id, T entity)
  {
    var response = await _http.PutAsJsonAsync($"{_endpoint}/{id}", entity);
    response.EnsureSuccessStatusCode();
  }
}