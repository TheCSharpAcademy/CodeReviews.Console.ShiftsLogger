using ShiftsLoggerAPI.Models;
using Spectre.Console;
using System.Text.Json;

internal class EmployeeRead
{
    internal static void ShowAllEmployees()
    {
        Console.Clear();
        var employees = GetEmployeesList();

        if (DisplayInfoHelpers.NoRecordsAvailable(employees)) return;

        AnsiConsole.MarkupLine("[yellow]List of all employees:[/]\n");

        int num = 1;
        var table = new Table();
        table.AddColumn("[yellow]No.[/]");
        table.AddColumn("[yellow]Name[/]");
        table.AddColumn("[yellow]Shifts[/]");

        foreach (var employee in employees)
        {
            table.AddRow(
                new Markup($"[green]{num}[/]"),
                new Markup($"{employee.Name}"),
                new Markup($"{EmployeeShifts.GetShiftsForEmployee(employee.Id).Count}"));
            num++;
        }
        AnsiConsole.Write(table);
        AnsiConsole.Markup("\n[yellow]Press any key to continue...[/] ");
        Console.ReadKey(true);
        Console.Clear();
    }

    internal static List<Employee> GetEmployeesList()
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient()
                .GetAsync(EndpointUrl.EmployeesEndpoint).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to read employees list from the database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            var json = result.Content.ReadAsStringAsync().Result;

            var jsonDocument = JsonDocument.Parse(json);

            var employees = jsonDocument.RootElement.GetProperty("employees").EnumerateArray()
                .Select(e => new Employee
                {
                    Id = e.GetProperty("id").GetInt32(),
                    Name = e.GetProperty("name").GetString() ?? string.Empty
                })
                .OrderBy(e => e.Name)
                .ToList();

            return employees;
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

    internal static Employee GetEmployee()
    {
        var empty = new Employee { Id = 0, Name = "" };

        var employeeMap = MakeEmployeeMap();

        if (!employeeMap.Any()) return empty;

        var choice = DisplayInfoHelpers.GetChoiceFromSelectionPrompt(
            "Choose employee:", employeeMap.Keys);
        if (choice == DisplayInfoHelpers.Back) return empty;

        var success = employeeMap.TryGetValue(choice, out Employee? chosenEmployee);
        if (!success) return empty;

        return chosenEmployee ?? empty;
    }

    private static Dictionary<string, Employee> MakeEmployeeMap()
    {
        var employees = GetEmployeesList();
        var employeeList = MakeEmployeeList(employees);
        var employeeMap = new Dictionary<string, Employee>();

        for (int i = 0; i < employees.Count; i++)
        {
            employeeMap.Add(employeeList[i], employees[i]);
        }
        return employeeMap;
    }

    private static List<string> MakeEmployeeList(List<Employee> employees)
    {
        var tableData = new List<string>();
        int num = 1;
        foreach (var employee in employees)
        {
            tableData.Add(
                $"[green]{num}:[/] " +
                $"[yellow]{employee.Name}[/]" +
                $"[{Console.BackgroundColor}] =>id:{employee.Id}[/]");
            num++;
        }
        return tableData;
    }
}
