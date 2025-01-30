using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text;

internal class EmployeeUpdate
{
    internal static void Update()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Editing employee's name[/]\n");

        var employee = EmployeeRead.GetEmployee();
        if (employee.Id == 0)
        {
            AnsiConsole.MarkupLine("[red]No records found in database.[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]{employee.Name}[/]\n");

        var newName = EmployeeCreate.GetEmployeeName();

        UpdateEmployee(employee, newName);
    }

    private static void UpdateEmployee(Employee employee, string newName)
    {
        try
        {
            employee.Name = newName;

            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = HttpClientFactory.GetHttpClient()
                .PutAsync(EndpointUrl.EmployeesEndpoint + $"/{employee.Id}", content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to update employee's name.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[green]Employee's name updated successfully.[/]");
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
