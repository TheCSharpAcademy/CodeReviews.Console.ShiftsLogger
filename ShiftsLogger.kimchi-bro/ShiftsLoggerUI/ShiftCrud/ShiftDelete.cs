using ShiftsLoggerUI.Helpers;
using Spectre.Console;

namespace ShiftsLoggerUI.ShiftCrud;

internal class ShiftDelete
{
    internal static void Delete()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Deleting shift.[/]\n");

        var shift = ShiftRead.GetShift();
        if (shift.Id == 0)
        {
            Console.Clear();
            return;
        }

        AnsiConsole.MarkupLine($"[red]WARNING![/] You want to delete that shift record permanently!");
        AnsiConsole.MarkupLine(ShiftRead.ShowShift(shift));
        if (!DisplayInfoHelpers.ConfirmDeletion())
        {
            Console.Clear();
            return;
        }

        DeleteShift(shift.Id);
    }

    private static void DeleteShift(int id)
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient().DeleteAsync(EndpointUrl.ShiftsEndpoint + $"/{id}").Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to delete shift record with Id: {id}.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            AnsiConsole.MarkupLine("[yellow]Shift record deleted successfully.[/]");
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
