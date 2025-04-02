using Microsoft.Extensions.Configuration;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using Newtonsoft.Json;
using System.Text;

namespace ShiftsLoggerUI.Services;

public class EmployeesService : BaseService, IService<Employee>
{

    public EmployeesService(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Employee> Create(Employee newEmployee)
    {
        var createdEmployee = new Employee();
        try
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Employee")
            {
                Content = new StringContent(JsonConvert.SerializeObject(newEmployee), Encoding.UTF8, "application/json")
            };
            using var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                createdEmployee = serializer.Deserialize<Employee>(jsonReader);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }
        return createdEmployee;
    }

    public async Task<string> Delete(int id)
    {
        var responseMessage = string.Empty;
        try
        {
            using var response = await _client.DeleteAsync($"/api/Employee/{id}");

            if (response.IsSuccessStatusCode)
                 responseMessage = await response.Content.ReadAsStringAsync();
            else
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }

        return responseMessage;
    }

    public async Task<Employee> Find(int id)
    {
        var employee = new Employee();
        try
        {
            using var response = await _client.GetAsync($"api/Employee/id/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                employee = serializer.Deserialize<Employee>(jsonReader);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }

        return employee;
    }

    public async Task<Employee> Find(string name)
    {
        var employee = new Employee();
        try
        {
            using var response = await _client.GetAsync($"api/Employee/name/{name}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                employee = serializer.Deserialize<Employee>(jsonReader);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }

        return employee;
    }


    public async Task<List<Employee>> GetAll()
    {
        var employees = new List<Employee>();
        try
        {
            using var response = await _client.GetAsync("api/Employee");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                employees = serializer.Deserialize<List<Employee>>(jsonReader);

            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }

        return employees;
    }

    public async Task<Employee> Update(int id, Employee updatedEmployee)
    {
        var responseUpdatedEmployee = new Employee();
        try
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/api/Employee/{id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json")
            };
            using var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                responseUpdatedEmployee = serializer.Deserialize<Employee>(jsonReader);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]{response.StatusCode}[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an error: " + ex.Message);
        }
        return responseUpdatedEmployee;
    }
}