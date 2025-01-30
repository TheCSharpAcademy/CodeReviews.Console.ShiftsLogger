using ShiftsLoggerAPI.Models;
using Spectre.Console;
using Newtonsoft.Json;
using System.Text;

namespace ShiftsLoggerUI.MockArea;

internal class MockShift
{
    internal static void Generate()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Generating random shift records.[/]\n");

        var employees = EmployeeRead.GetEmployeesList();

        if (!employees.Any())
        {
            AnsiConsole.MarkupLine("Add at least one employee first.");
            AnsiConsole.MarkupLine("You can do it manually from main menu or use random generator.\n");

            var answer = DisplayInfoHelpers.GetYesNoAnswer("Do you want to generate new employees?");
            if (!answer)
            {
                Console.Clear();
                return;
            }

            MockEmployee.Generate();
            employees = EmployeeRead.GetEmployeesList();
        }

        var numberOfShifts = GetPositiveNumberInput("Enter a number of shifts to generate:");

        var shifts = GenerateShifts(numberOfShifts, employees);

        AddShifts(shifts);
    }

    private static void AddShifts(List<Shift> shifts)
    {
        try
        {
            var json = JsonConvert.SerializeObject(shifts);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient()
                .PostAsync(EndpointUrl.MockEndpoint + "/add", content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to add shift records into database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]New shift records created successfully.[/]");
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

    private static List<Shift> GenerateShifts(int number, List<Employee> employees)
    {
        var shifts = new List<Shift>();
        for (int i = 0; i < number; i++)
        {
            var startTime = DateTime.Today.AddDays(Random.Shared.Next(30)) +
                TimeSpan.FromHours(Random.Shared.Next(0, 24));
            var duration = TimeSpan.FromHours(Random.Shared.Next(1, 15));
            var endTime = startTime + duration;
            var employeeId = employees[Random.Shared.Next(employees.Count)].Id;

            shifts.Add(new Shift
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                EmployeeId = employeeId
            });
        }
        return shifts.OrderBy(s => s.StartTime).ToList();
    }

    internal static int GetPositiveNumberInput(string message)
    {
        var input = AnsiConsole.Ask<int>(message);
        while (input <= 0)
        {
            AnsiConsole.Markup("[red]Invalid input. Only positive numbers accepted.[/]\n");
            input = AnsiConsole.Ask<int>("Enter a valid number:");
        }
        return input;
    }

    internal static void DeleteAllShifts()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting all shift records[/]\n");

        AnsiConsole.MarkupLine($"[red]That action will delete all shift records from database.[/]");
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        DeleteShifts();
    }

    private static void DeleteShifts()
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient()
                .DeleteAsync(EndpointUrl.MockEndpoint + "/del").Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to delete all shift records from database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[yellow]All shift records deleted successfully.[/]");
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
