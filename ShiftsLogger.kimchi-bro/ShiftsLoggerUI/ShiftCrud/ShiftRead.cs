using ShiftsLoggerAPI.Models;
using ShiftsLoggerUI.Helpers;
using Spectre.Console;
using System.Text.Json;

namespace ShiftsLoggerUI.ShiftCrud;

internal class ShiftRead
{
    internal static void ShowAllShifts()
    {
        Console.Clear();
        var shifts = GetShiftList();

        if (DisplayInfoHelpers.NoRecordsAvailable(shifts)) return;

        AnsiConsole.MarkupLine("[yellow]List of all shifts:[/]\n");

        int num = 1;
        var table = new Table();
        table.AddColumn("[yellow]No.[/]");
        table.AddColumn("[yellow]Start Time[/]");
        table.AddColumn("[yellow]End Time[/]");
        table.AddColumn("[yellow]Duration[/]");

        foreach (var shift in shifts)
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

    private static List<Shift> GetShiftList()
    {
        try
        {
            var result = HttpClientFactory.GetHttpClient().GetAsync(EndpointUrl.ShiftsEndpoint).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"[red]Failed to read shift records from the database.[/]\n" +
                    $"Status code: [yellow]{result.StatusCode}[/]");
            }

            var json = result.Content.ReadAsStringAsync().Result;

            var jsonDocument = JsonDocument.Parse(json);

            var shifts = jsonDocument.RootElement.GetProperty("shifts").EnumerateArray()
                .Select(e => new Shift
                {
                    Id = (int)e.GetProperty("id").GetInt64(),
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

    internal static Shift GetShift()
    {
        var shiftMap = MakeShiftMap();
        if (DisplayInfoHelpers.NoRecordsAvailable(shiftMap.Keys)) return new Shift();

        var choice = DisplayInfoHelpers.GetChoiceFromSelectionPrompt(
            "Choose shift:", shiftMap.Keys);
        if (choice == DisplayInfoHelpers.Back) return new Shift();

        var success = shiftMap.TryGetValue(choice, out Shift? chosenShift);
        if (!success) return new Shift();

        return chosenShift!;
    }

    private static Dictionary<string, Shift> MakeShiftMap()
    {
        var shifts = GetShiftList();
        var shiftList = MakeShiftList(shifts);
        var shiftMap = new Dictionary<string, Shift>();

        for (int i = 0; i < shifts.Count; i++)
        {
            shiftMap.Add(shiftList[i], shifts[i]);
        }
        return shiftMap;
    }

    internal static string ShowShift(Shift shift)
    {
        return
            $"[yellow]Start time:[/] {shift.StartTime.ToString(InputDataHelpers.DateTimeFormat)} " +
            $"[yellow]End time:[/] {shift.EndTime.ToString(InputDataHelpers.DateTimeFormat)} " +
            $"[yellow]Duration:[/] {shift.Duration.Hours:D2}:{shift.Duration.Minutes:D2}";
    }

    private static List<string> MakeShiftList(List<Shift> shifts)
    {
        var tableData = new List<string>();
        int num = 1;
        foreach (var shift in shifts)
        {
            tableData.Add(
                $"[green]{num}:[/] " + 
                ShowShift(shift) + 
                $"[{Console.BackgroundColor}] =>id:{shift.Id}[/]");
            num++;
        }
        return tableData;
    }
}
