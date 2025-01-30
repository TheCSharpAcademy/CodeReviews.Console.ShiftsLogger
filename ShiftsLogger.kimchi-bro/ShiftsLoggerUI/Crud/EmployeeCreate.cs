using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text;

internal class EmployeeCreate
{
    internal static void Create()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Adding new employee[/]\n");

        var name = GetEmployeeName();

        AddNewEmployee(name);
    }

    private static void AddNewEmployee(string name)
    {
        try
        {
            var employee = new Employee
            {
                Name = name,
            };

            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient()
                .PostAsync(EndpointUrl.EmployeesEndpoint, content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to add new employee record.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]New employee added successfully.[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
        }
        catch (HttpRequestException ex)
        {
            ErrorInfoHelpers.Http(ex);
        }
        catch (Exception ex)
        {
            ErrorInfoHelpers.General(ex);
        }
    }

    internal static string GetEmployeeName()
    {
        var input = AnsiConsole.Ask<string>("Enter employee's name:");
        while (input.Length < 3)
        {
            AnsiConsole.Markup("[red]Invalid input. Name must be at least 3 characters long.[/]\n");
            input = AnsiConsole.Ask<string>("Enter a valid name:");
        }
        return input;
    }
}
