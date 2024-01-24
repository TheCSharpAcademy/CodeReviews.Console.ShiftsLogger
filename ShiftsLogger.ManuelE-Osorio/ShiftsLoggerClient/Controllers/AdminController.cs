using System.Net.Http.Headers;
using System.Text.Json;

namespace ShiftsLoggerClient.Controllers;

public class AdminController
{
    public HttpClient Client;
    private static readonly Uri ShiftsBaseAdress = new("http://localhost:5039/api/Admin/");

    public AdminController()
    {
        Client = new()
        {
            BaseAddress = ShiftsBaseAdress,
            // Timeout = new TimeSpan(0, 0, 10)
        };
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<bool> GetAdmin(int id)
    {
        var response = await Client.GetAsync($"{id}");
        if(response.IsSuccessStatusCode)
        {
            var admin = await JsonSerializer.DeserializeAsync<bool>(response.Content.ReadAsStream());
            return admin;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }
}