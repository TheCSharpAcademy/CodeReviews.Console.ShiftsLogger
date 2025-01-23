using Newtonsoft.Json;
using ShiftsLoggerAPI.Models;
using ShiftsLoggerUI.Helpers;
using Spectre.Console;
using System.Text;

namespace ShiftsLoggerUI.ShiftCrud;

internal class ShiftCreate
{
    internal static void Create()
    {
        Console.Clear();
        var (exit, startTime, endTime, duration) = InputDataHelpers.GetData();
        if (exit)
        {
            Console.Clear();
            return;
        }

        AddNewShift(startTime, endTime, duration);
    }

    private static void AddNewShift(DateTime startTime, DateTime endTime, TimeSpan duration)
    {
        try
        {
            var shift = new Shift
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration
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
