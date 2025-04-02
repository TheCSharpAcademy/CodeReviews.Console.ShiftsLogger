using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShiftsLoggerUI.Models;
using Spectre.Console;
using System.Text;


namespace ShiftsLoggerUI.Services;

internal class ShiftsService : BaseService, IService<Shift>
{
    public ShiftsService(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Shift> Create(Shift newShift)
    {
        var createdShift = new Shift();
        try
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Shift")
            {
                Content = new StringContent(JsonConvert.SerializeObject(newShift), Encoding.UTF8, "application/json")
            };
            using var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                createdShift = serializer.Deserialize<Shift>(jsonReader);
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
        return createdShift;
    }

    public async Task<string> Delete(int id)
    {
        var responseMessage = string.Empty;
        try
        {
            using var response = await _client.DeleteAsync($"/api/Shift/{id}");

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

    public async Task<Shift> Find(int id)
    {
        var shift = new Shift();
        try
        {
            using var response = await _client.GetAsync($"/api/Shift/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                shift = serializer.Deserialize<Shift>(jsonReader);
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

        return shift;
    }

    public async Task<List<Shift>> GetAll()
    {
        var shifts = new List<Shift>();
        try
        {
            using var response = await _client.GetAsync("/api/Shift");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                shifts = serializer.Deserialize<List<Shift>>(jsonReader);
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

        return shifts;
    }

    public async Task<Shift> Update(int id, Shift updatedShift)
    {
        var responseUpdatedShift = new Shift();
        try
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"/api/Shift/{id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(updatedShift), Encoding.UTF8, "application/json")
            };
            using var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                responseUpdatedShift = serializer.Deserialize<Shift>(jsonReader);
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
        return responseUpdatedShift;
    }

    public async Task<List<Shift>> FindEmpShifts(int empId)
    {
        var shifts = new List<Shift>();
        try
        {
            using var response = await _client.GetAsync($"/api/Shift/empId/{empId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(content);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();
                shifts = serializer.Deserialize<List<Shift>>(jsonReader);
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
        return shifts;
    }
}