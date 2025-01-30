using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text;

internal class ShiftUpdate
{
    internal static void Update()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Updating shift[/]\n");

        var shift = ShiftRead.GetShift();
        if (shift.Id == 0)
        {
            Console.Clear();
            return;
        }

        AnsiConsole.MarkupLine(ShiftRead.ShowShift(shift));

        var (exit, startTime, endTime, duration) = InputDataHelpers.GetData();
        if (exit)
        {
            Console.Clear();
            return;
        }

        var employee = EmployeeRead.GetEmployee();
        if (employee.Id == 0)
        {
            Console.Clear();
            return;
        }

        UpdateShift(shift.Id, startTime, endTime, duration, employee);
    }

    private static void UpdateShift(
        int id, DateTime startTime, DateTime endTime, TimeSpan duration, Employee employee)
    {
        try
        {
            var shift = new Shift
            {
                Id = id,
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                EmployeeId = employee.Id
            };

            var json = JsonConvert.SerializeObject(shift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient()
                .PutAsync(EndpointUrl.ShiftsEndpoint + $"/{id}", content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to update shift record.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]Shift record updated successfully.[/]");
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
