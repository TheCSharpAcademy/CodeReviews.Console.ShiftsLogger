using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

public class EmployeesWebService
{
    public HttpClient Client;
    private readonly Uri ShiftsBaseAdress;

    public EmployeesWebService(string appUrl)
    {
        ShiftsBaseAdress = new Uri(appUrl+"/api/Employees/");
        Client = new()
        {
            BaseAddress = ShiftsBaseAdress,
            Timeout = new TimeSpan(0, 0, 10)
        };
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json-patch+json"));
    }

    public async Task<HttpResponseMessage> GetEmployeeById(int id)
    {
        return await Client.GetAsync($"{id}");
    }

    public async Task<HttpResponseMessage> GetEmployeeByName(string name)
    {
        return await Client.GetAsync($"?name={name}");
    }

    public async Task<HttpResponseMessage> PutEmployee(Employee employee)
    {
        return await Client.PutAsJsonAsync("", employee);
    }

    public async Task<HttpResponseMessage> PostEmployee(Employee employee)
    {
        return await Client.PostAsJsonAsync("", employee);
    }
}