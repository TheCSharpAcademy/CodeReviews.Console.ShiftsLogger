using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text;

internal class ShiftCreate
{
    internal static void Create()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Creating new shift[/]\n");

        var employee = EmployeeRead.GetEmployee();
        if (employee.Id == 0)
        {
            AnsiConsole.MarkupLine("You need to add new employee first.\n");

            var answer = DisplayInfoHelpers.GetYesNoAnswer("Do you want to add new employee now?");
            if (!answer)
            {
                Console.Clear();
                return;
            }

            EmployeeCreate.Create();
            employee = EmployeeRead.GetEmployee();
        }

        var (exit, startTime, endTime, duration) = InputDataHelpers.GetData();
        if (exit)
        {
            Console.Clear();
            return;
        }

        AddNewShift(startTime, endTime, duration, employee);
    }

    private static void AddNewShift(
        DateTime startTime, DateTime endTime, TimeSpan duration, Employee employee)
    {
        try
        {
            var shift = new Shift
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                EmployeeId = employee.Id
            };

            var json = JsonConvert.SerializeObject(shift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient().PostAsync(EndpointUrl.ShiftsEndpoint, content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to create new shift record.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]New shift record created successfully.[/]");
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
