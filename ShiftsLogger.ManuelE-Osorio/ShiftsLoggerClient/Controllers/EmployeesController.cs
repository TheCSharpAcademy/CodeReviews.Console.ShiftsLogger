using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using NewtonJson = Newtonsoft.Json;
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

    public async Task<Employee?> GetEmployee(int id)
    {
        var response = await Client.GetAsync($"{id}");
        if(response.IsSuccessStatusCode)
        {
            return await JsonSerializer.DeserializeAsync<Employee>(response.Content.ReadAsStream());
        }
        else
            throw new Exception(response.StatusCode.ToString());
    }
}