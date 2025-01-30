using Bogus;
using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text;

namespace ShiftsLoggerUI.MockArea;

internal class MockEmployee
{
    internal static void Generate()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Generating random employees[/]\n");

        var numberOfEmployees = MockShift.GetPositiveNumberInput("Enter a number of employees to generate:");

        var faker = new Faker();
        var employees = Enumerable.Range(1, numberOfEmployees).Select(_ => new Employee
        {
            Name = faker.Name.FullName()
        })
        .ToList();

        AddEmployees(employees);
    }

    private static void AddEmployees(List<Employee> employees)
    {
        try
        {
            var json = JsonConvert.SerializeObject(employees);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient()
                .PostAsync(EndpointUrl.MockEndpoint + "/empadd", content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to add fake employees into database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]New random employees added successfully.[/]");
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

    internal static void DeleteAllEmployees()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting all employees[/]\n");

        AnsiConsole.MarkupLine($"[red]That action will delete all employees and shift records from database.[/]");
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        DeleteEmployees();
    }

    private static void DeleteEmployees()
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient()
                .DeleteAsync(EndpointUrl.MockEndpoint + "/empdel").Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to delete all employees and shift records from database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[yellow]All employees and shift records deleted successfully.[/]");
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
}
