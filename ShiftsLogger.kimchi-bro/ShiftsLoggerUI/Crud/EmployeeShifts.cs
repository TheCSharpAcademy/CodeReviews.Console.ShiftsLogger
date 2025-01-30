using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text.Json;

internal class EmployeeShifts
{
    internal static void ShowAllShifts()
    {
        Console.Clear();

        var employee = EmployeeRead.GetEmployee();
        if (employee.Id == 0)
        {
            AnsiConsole.MarkupLine("[red]No records found in database.[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return;
        }

        var employeeShifts = GetShiftsForEmployee(employee.Id);
        if (DisplayInfoHelpers.NoRecordsAvailable(employeeShifts)) return;

        AnsiConsole.MarkupLine($"List of shifts for [yellow]{employee.Name}[/]:\n");

        int num = 1;
        var table = new Table();
        table.AddColumn("[yellow]No.[/]");
        table.AddColumn("[yellow]Start Time[/]");
        table.AddColumn("[yellow]End Time[/]");
        table.AddColumn("[yellow]Duration[/]");

        foreach (var shift in employeeShifts)
        {
            table.AddRow(
                new Markup($"[green]{num}[/]"),
                new Markup($"{shift.StartTime.ToString(InputDataHelpers.DateTimeFormat)}"),
                new Markup($"{shift.EndTime.ToString(InputDataHelpers.DateTimeFormat)}"),
                new Markup($"{shift.Duration.Hours:D2}:{shift.Duration.Minutes:D2}"));
            num++;
        }
        AnsiConsole.Write(table);
        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/] ");
        Console.ReadKey(true);
        Console.Clear();
    }

    internal static List<Shift> GetShiftsForEmployee(int employeeId)
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient()
                .GetAsync(EndpointUrl.EmployeesEndpoint + $"/{employeeId}/shifts").Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to read employee's shifts from the database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            var json = result.Content.ReadAsStringAsync().Result;

            var jsonDocument = JsonDocument.Parse(json);

            var shifts = jsonDocument.RootElement.GetProperty("shifts").EnumerateArray()
                .Select(e => new Shift
                {
                    Id = e.GetProperty("id").GetInt32(),
                    StartTime = DateTime.Parse(e.GetProperty("startTime").GetString() ?? string.Empty),
                    EndTime = DateTime.Parse(e.GetProperty("endTime").GetString() ?? string.Empty),
                    Duration = TimeSpan.Parse(e.GetProperty("duration").GetString() ?? string.Empty)
                })
                .OrderBy(e => e.StartTime)
                .ToList();

            return shifts;
        }
        catch (HttpRequestException ex)
        {
            ErrorInfoHelpers.Http(ex);
            return [];
        }
        catch (Exception ex)
        {
            ErrorInfoHelpers.General(ex);
            return [];
        }
    }
}
