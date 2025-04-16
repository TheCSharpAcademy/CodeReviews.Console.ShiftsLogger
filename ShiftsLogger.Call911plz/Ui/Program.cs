namespace Ui;

using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new RestClientOptions("http://localhost:5295/api/Shifts/");
        var client = new RestClient(options);
        var request = new RestRequest("");

        var response = await client.GetAsync(request);
        
        if (response.Content == null)
        {
            AnsiConsole.MarkupLine("No content");
        }
        else
        {
            var jsonOptions = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };
            var jsonContent = JsonSerializer.Deserialize<List<Shift>>(response.Content, jsonOptions);

            AnsiConsole.Write(jsonContent[0].id);
            AnsiConsole.Write(jsonContent[0].employeeId);
            AnsiConsole.Write(jsonContent[0].employeeName);
            AnsiConsole.Write(jsonContent[0].startDateTime.ToString());
            AnsiConsole.Write(jsonContent[0].endDateTime.ToString());
        }
        
    }
}
