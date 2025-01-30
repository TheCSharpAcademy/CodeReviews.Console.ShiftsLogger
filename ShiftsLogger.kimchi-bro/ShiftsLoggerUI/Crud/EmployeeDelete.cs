using Spectre.Console;

internal class EmployeeDelete
{
    internal static void Delete()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting employee[/]\n");

        var employee = EmployeeRead.GetEmployee();
        if (employee.Id == 0)
        {
            AnsiConsole.MarkupLine("[red]No records found in database.[/]");
            DisplayInfoHelpers.PressAnyKeyToContinue();
            return;
        }

        AnsiConsole.MarkupLine($"[red]WARNING![/] You want to delete that employee permanently!");
        AnsiConsole.MarkupLine($"[yellow]{employee.Name}[/]");
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        DeleteEmployee(employee.Id);
    }

    private static void DeleteEmployee(int id)
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient()
                .DeleteAsync(EndpointUrl.EmployeesEndpoint + $"/{id}").Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to delete employee with Id: {id}.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[yellow]Employee deleted successfully.[/]");
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
