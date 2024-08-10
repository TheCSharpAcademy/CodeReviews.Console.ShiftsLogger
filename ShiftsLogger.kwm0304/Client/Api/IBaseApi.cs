using System.Net.Http.Json;
using Spectre.Console;

namespace Client.Api;

public class IBaseApi<T> where T : class
{
  private readonly HttpClient _http;
  private readonly string _endpoint;
  private const string baseUrl = "http://localhost:5062/api";
  private readonly string _url;
  public IBaseApi(HttpClient http, string endpoint)
  {
    _http = http;
    _http.BaseAddress = new Uri("http://localhost:5062/api");
    _endpoint = endpoint;
    _url = $"{_http.BaseAddress}{_endpoint}";
  }

  public async Task CreateAsync(T entity)
  {
    try
    {
      var response = await _http.PostAsJsonAsync(_url, entity);
      response.EnsureSuccessStatusCode();
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine(e.Message);
      return;
    }
  }

  public async Task DeleteByIdAsync(int id)
  {
    try
    {
      var response = await _http.DeleteAsync($"{_url}/{id}");
      response.EnsureSuccessStatusCode();
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine(e.Message);
      return;
    }

  }

  public async Task<List<T>> GetAllAsync()
  {
    try
    {
      return await _http.GetFromJsonAsync<List<T>>(_url) ?? [];
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine(e.Message);
      return [];
    }
  }

  public async Task<T> GetByIdAsync(int id)
  {
    try
    {
      T entity = await _http.GetFromJsonAsync<T>($"{_url}/{id}");
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
    try
    {
      var response = await _http.PutAsJsonAsync($"{_url}/{id}", entity);
      response.EnsureSuccessStatusCode();
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine(e.Message);
      return;
    }
  }
}