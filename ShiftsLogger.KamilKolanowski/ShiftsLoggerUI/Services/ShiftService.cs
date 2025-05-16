using ShiftsLogger.KamilKolanowski.Models;
using Spectre.Console;

namespace ShiftsLoggerUI.Services;

internal class ShiftService
{
    private readonly ApiDataService _apiDataService = new();
    private readonly DeserializeJson _deserializeJson = new();
    private readonly WorkerService _workerService = new();

    internal async Task CreateShift()
    {
        try
        {
            var shift = await GetUserInputForShiftCreation();

            await _apiDataService.PostShiftAsync(shift);

            AnsiConsole.MarkupLine(
                "\n[green]Successfully added shift![/]\nPress any key to continue..."
            );
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to add shift due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task EditWorker()
    {
        try
        {
            // var shiftDtoToUpdate = new ShiftDto();
            // await _apiDataService.PutWorkerAsync(shiftDtoToUpdate);

            AnsiConsole.MarkupLine(
                "\n[green]Successfully edited worker![/]\nPress any key to continue..."
            );
            
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to edit worker, due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task DeleteWorker()
    {
        try
        {
            var idMap = await CreateShiftsTable();

            while (true)
            {
                var workerId = AnsiConsole.Ask<int>("Choose [yellow2]worker id[/] to delete:");

                if (!idMap.TryGetValue(workerId, out var dbId))
                {
                    AnsiConsole.MarkupLine("[red]There is no worker with the specified id![/]");
                    continue;
                }

                await _apiDataService.DeleteWorkerAsync(dbId);
                AnsiConsole.MarkupLine(
                    "\n[green]Successfully deleted worker![/]\nPress any key to continue..."
                );
                return;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to delete worker, due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task<Dictionary<int, int>> CreateShiftsTable()
    {
        var table = new Table { Title = new TableTitle("[yellow]Shifts[/]") };

        table.AddColumn("[cyan2]Shift Id[/]");
        table.AddColumn("[cyan2]Shift Type[/]");
        table.AddColumn("[cyan2]Worker Id[/]");
        table.AddColumn("[cyan2]Start Time[/]");
        table.AddColumn("[cyan2]End Time[/]");
        table.AddColumn("[cyan2]Worked Hours[/]");

        var shifts = await GetShiftsAsync();
        var idMap = new Dictionary<int, int>();
        int idx = 1;
        foreach (var shiftDto in shifts)
        {
            table.AddRow(
                idx.ToString(),
                shiftDto.ShiftType,
                shiftDto.WorkerId.ToString(),
                shiftDto.WorkedHours,
                shiftDto.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                shiftDto.EndTime.ToString("yyyy-MM-dd HH:mm:ss")
            );
            idMap[idx] = shiftDto.ShiftId; // Mapping real id from database to artificial in the app
            idx++;
        }

        table.Border(TableBorder.HeavyEdge);

        AnsiConsole.Write(table);
        return idMap;
    }

    private async Task<List<ShiftDto>> GetShiftsAsync()
    {
        string response = await _apiDataService.GetShiftAsync("shifts");
        List<ShiftDto> shiftDtos = await _deserializeJson.DeserializeAsync<List<ShiftDto>>(
            response
        );

        return shiftDtos;
    }

    private async Task<ShiftDto> GetShiftAsync(int id)
    {
        string response = await _apiDataService.GetShiftAsync($"shifts/{id}");
        ShiftDto shiftDto = await _deserializeJson.DeserializeAsync<ShiftDto>(response);
        return shiftDto;
    }

    private async Task<ShiftDto> GetUserInputForShiftCreation()
    {
        var workers = await _workerService.GetWorkersAsync();
        var workerEmail = AnsiConsole.Ask<string>("Provide your email to register shift:");
        var workerId = workers
            .Where(w => w.Email == workerEmail)
            .Select(w => w.WorkerId)
            .FirstOrDefault();
        var shiftType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose ShiftType")
                .AddChoices("Day Shift", "Swing Shift", "Graveyard Shift")
        );
        var startTime = AnsiConsole.Prompt(
            new TextPrompt<DateTime>(
                "Enter shift start date and time [yellow](e.g. 2025-05-16 14:30)[/]:"
            ).Validate(input =>
                input > DateTime.MinValue
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid date.[/]")
            )
        );
        var endTime = AnsiConsole.Prompt(
            new TextPrompt<DateTime>(
                "Enter shift end date and time [yellow](e.g. 2025-05-16 14:30)[/]:"
            ).Validate(input =>
                input > DateTime.MinValue
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid date.[/]")
            )
        );

        TimeSpan difference = endTime - startTime;
        string workedHours =
            $"{(int)difference.TotalHours:D2}:{difference.Minutes:D2}:{difference.Seconds:D2}";

        return new ShiftDto()
        {
            ShiftType = shiftType,
            WorkerId = workerId,
            WorkedHours = workedHours,
            StartTime = startTime,
            EndTime = endTime,
        };
    }
}
