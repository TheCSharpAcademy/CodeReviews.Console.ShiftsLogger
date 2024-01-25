using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Controllers;

public class EmployeesController
{
    public HttpClient Client;
    private static readonly Uri ShiftsBaseAdress = new("http://localhost:5039/api/Employees/");

    public EmployeesController()
    {
        Client = new()
        {
            BaseAddress = ShiftsBaseAdress,
            // Timeout = new TimeSpan(0, 0, 10)
        };
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json-patch+json"));
    }

    public async Task<List<Employee>?> GetEmployeeById(int id)
    {
        var response = await Client.GetAsync($"{id}");
        if(response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<List<Employee>>(response.Content.ReadAsStream());
        }
        else
            throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<List<Employee>?> GetEmployeeByName(string name)
    {
        var response = await Client.GetAsync($"?name={name}");
        if(response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<List<Employee>>(response.Content.ReadAsStream());
        }
        else
            throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<HttpResponseMessage> PutEmployee(Employee employee)
    {
        var response = await Client.PutAsJsonAsync("", employee);
        return response;
    }

    public async Task<HttpResponseMessage> PostEmployee(Employee employee)
    {
        var response = await Client.PostAsJsonAsync("", employee);
        return response;
    }
}