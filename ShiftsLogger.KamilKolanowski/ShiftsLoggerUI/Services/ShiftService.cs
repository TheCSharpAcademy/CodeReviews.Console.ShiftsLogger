using ShiftsLogger.KamilKolanowski.Models;
using ShiftsLoggerUI.Models;
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
            var shifts = await GetShiftsAsync();
            var shift = await GetUserInputForShiftCreation();

            if (
                shifts.Any(s =>
                    s.WorkerId == shift.WorkerId
                    && s.StartTime == shift.StartTime
                    && s.EndTime == shift.EndTime
                )
            )
            {
                AnsiConsole.MarkupLine("[red]Shift already logged![/]");
                AnsiConsole.MarkupLine("Press any key to continue...");
            }
            else
            {
                await _apiDataService.PostShiftAsync(shift);

                AnsiConsole.MarkupLine(
                    "\n[green]Successfully added shift![/]\nPress any key to continue..."
                );
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to add shift due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task EditShift()
    {
        try
        {
            var idMap = await CreateShiftsTable();
            var shifts = await GetShiftsAsync();

            while (true)
            {
                DateTime endTime;
                var shiftId = AnsiConsole.Ask<int>("Choose [yellow2]shift id[/] to edit:");

                if (!idMap.TryGetValue(shiftId, out var dbId))
                {
                    AnsiConsole.MarkupLine("[red]There is no shift with the specified id![/]");
                    continue;
                }

                var shiftDtoToUpdate = await GetShiftAsync(dbId);

                var columnToUpdate = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose the property to edit")
                        .AddChoices("StartTime", "EndTime")
                );

                var newValue = String.Empty;

                if (columnToUpdate == "StartTime")
                {
                    DateTime startTime = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>(
                            "Enter shift start date and time [yellow](e.g. 2025-05-16 14:30)[/]:"
                        ).Validate(input =>
                            input > DateTime.MinValue
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid date.[/]")
                        )
                    );

                    newValue = startTime.ToString("yyyy-MM-dd HH:mm");
                }
                else if (columnToUpdate == "EndTime")
                {
                    endTime = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>(
                            "Enter shift end date and time [yellow](e.g. 2025-05-16 14:30)[/]:"
                        ).Validate(input =>
                            input > shiftDtoToUpdate.StartTime
                                ? ValidationResult.Success()
                                : ValidationResult.Error(
                                    "[red]Invalid date (end date must be greater than start date).[/]"
                                )
                        )
                    );

                    newValue = endTime.ToString("yyyy-MM-dd HH:mm");
                }

                switch (columnToUpdate)
                {
                    case "StartTime":
                        shiftDtoToUpdate.StartTime = DateTime.Parse(newValue);
                        break;
                    case "EndTime":
                        shiftDtoToUpdate.EndTime = DateTime.Parse(newValue);
                        break;
                }

                if (shiftDtoToUpdate.EndTime < shiftDtoToUpdate.StartTime)
                {
                    AnsiConsole.MarkupLine(
                        "[green]Shift start time edited[/]\n"
                            + "[red]Start time can't be lower than end time.\n"
                            + "Adjust the end time![/]"
                    );

                    shiftDtoToUpdate.EndTime = AnsiConsole.Prompt(
                        new TextPrompt<DateTime>(
                            "Enter shift end date and time [yellow](e.g. 2025-05-16 14:30)[/]:"
                        ).Validate(input =>
                            input > shiftDtoToUpdate.StartTime
                                ? ValidationResult.Success()
                                : ValidationResult.Error(
                                    "[red]Invalid date (end date must be greater than start date).[/]"
                                )
                        )
                    );
                }

                shiftDtoToUpdate.ShiftType =
                    shiftDtoToUpdate.StartTime.TimeOfDay >= TimeSpan.FromHours(8)
                    && shiftDtoToUpdate.StartTime.TimeOfDay < TimeSpan.FromHours(16)
                        ? "Day Shift"
                    : shiftDtoToUpdate.StartTime.TimeOfDay >= TimeSpan.FromHours(16)
                    && shiftDtoToUpdate.StartTime.TimeOfDay < TimeSpan.FromHours(24)
                        ? "Swing Shift"
                    : "Graveyard Shift";

                if (
                    shifts.Any(s =>
                        s.WorkerId == shiftDtoToUpdate.WorkerId
                        && s.StartTime == shiftDtoToUpdate.StartTime
                        && s.EndTime == shiftDtoToUpdate.EndTime
                    )
                )
                {
                    AnsiConsole.MarkupLine("[red]Shift already logged![/]");
                    AnsiConsole.MarkupLine("Press any key to continue...");
                    return;
                }
                else
                {
                    await _apiDataService.PutShiftAsync(shiftDtoToUpdate);

                    AnsiConsole.MarkupLine(
                        "\n[green]Successfully edited shift![/]\nPress any key to continue..."
                    );
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to edit shift, due to:[/] {ex.Message}");
            AnsiConsole.MarkupLine("Press any key to continue...");
        }
    }

    internal async Task DeleteShift()
    {
        try
        {
            var idMap = await CreateShiftsTable();

            while (true)
            {
                var shiftId = AnsiConsole.Ask<int>("Choose [yellow2]shift id[/] to delete:");

                if (!idMap.TryGetValue(shiftId, out var dbId))
                {
                    AnsiConsole.MarkupLine("[red]There is no shift with the specified id![/]");
                    continue;
                }

                await _apiDataService.DeleteShiftAsync(dbId);
                AnsiConsole.MarkupLine(
                    "\n[green]Successfully deleted shift![/]\nPress any key to continue..."
                );
                return;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Failed to delete shift, due to:[/] {ex.Message}");
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
                shiftDto.StartTime.ToString("yyyy-MM-dd HH:mm:ss"),
                shiftDto.EndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                shiftDto.WorkedHours
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
        var workerEmail = AnsiConsole.Prompt(
            new TextPrompt<string>(
                "Provide your email to register shift: [yellow](e.g. john.doe@gmail.com)[/]: "
            ).Validate(input =>
            {
                var formatValidation = _workerService.ValidateEmail(input);
                return formatValidation.Successful
                    ? ValidationResult.Success()
                    : ValidationResult.Error(formatValidation.Message);
            })
        );

        var notExistsValidation = _workerService.ValidateIfUserWithEmailExists(
            workers,
            workerEmail
        );
        WorkerDto? createdWorker = null;

        if (notExistsValidation.Successful)
        {
            AnsiConsole.MarkupLine("[red]You're not registered yet![/]\nPlease create an account");
            createdWorker = await _workerService.CreateWorker(workerEmail); // ← You need this assignment
        }

        int workerId =
            createdWorker != null
                ? createdWorker.WorkerId
                : workers
                    .Where(w => w.Email == workerEmail)
                    .Select(w => w.WorkerId)
                    .FirstOrDefault();

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
                input > startTime
                    ? ValidationResult.Success()
                    : ValidationResult.Error(
                        "[red]Invalid date (end date must be greater than start date).[/]"
                    )
            )
        );

        var shiftType =
            startTime.TimeOfDay >= TimeSpan.FromHours(8)
            && startTime.TimeOfDay < TimeSpan.FromHours(16)
                ? "Day Shift"
            : startTime.TimeOfDay >= TimeSpan.FromHours(16)
            && startTime.TimeOfDay < TimeSpan.FromHours(24)
                ? "Swing Shift"
            : "Graveyard Shift";

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
