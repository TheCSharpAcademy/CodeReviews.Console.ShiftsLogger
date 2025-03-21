using ShiftsLogger.UI.Service;
using ShiftsLogger.API.Model;
using ShiftsLogger.UI.Helpers;
using Spectre.Console;

namespace ShiftsLogger.UI.Menu;

class Menu
{
    private readonly ShiftApiService _shiftApiService;

    public Menu(ShiftApiService shiftApiService)
    {
        _shiftApiService = shiftApiService;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Markup("[bold underline]Shift Logger[/]\n\n");
            AnsiConsole.WriteLine("Select an option:");
            var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuChoices>()
                        .Title("Please select an option:")
                        .AddChoices(Enum.GetValues<MainMenuChoices>())
            );

            switch (menuChoice)
            {
                case MainMenuChoices.Exit:
                    AnsiConsole.Markup("[bold green]Goodbye![/]");
                    return;
                case MainMenuChoices.View:
                    await ShowShifts();
                    break;
                case MainMenuChoices.Create:
                    await CreateShift();
                    break;
                case MainMenuChoices.Edit:
                    await UpdateShift();
                    break;
                case MainMenuChoices.Delete:
                    await DeleteShift();
                    break;
                default:
                    return;
            }
        }
    }

    public async Task ShowShifts()
    {
        AnsiConsole.Clear();

        try
        {
            List<Shift>? shifts = await _shiftApiService.GetAllShiftsAsync();
            AnsiConsole.MarkupLine("Shifts:\n");
            if (shifts.Any())
            {
                foreach (Shift shift in shifts)
                {
                    AnsiConsole.MarkupLine($"Employee: {shift.EmployeeName}\tDuration: {shift.Duration}\tStart: {shift.StartTime}\tEnd: {shift.EndTime}");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[blue]No shifts registered.[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]There was an error when retrieving the shifts! Please try again later.[/]");
        }

        AnsiConsole.MarkupLine("\nPress enter to continue");
        AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
    }

    public async Task CreateShift()
    {
        AnsiConsole.Clear();

        string employeeName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the employee name"));

        (DateTime?, DateTime?) times = GetStartAndEndTimes();

        if (times.Item1 == null || times.Item2 == null)
        {
            return;
        }

        Shift newShift = new Shift {
            EmployeeName = employeeName,
            StartTime = times.Item1.Value,
            EndTime = times.Item2.Value
        };

        try
        {
            await _shiftApiService.CreateShift(newShift);
            AnsiConsole.MarkupLine("[green]Shift created successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]There was an error while creating the shift! Please try again later.[/]");
        }

        AnsiConsole.MarkupLine("\nPress enter to continue");
        AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
    }

    public async Task UpdateShift()
    {
        AnsiConsole.Clear();

        Shift? selectedShift = await ChooseShiftAsync();
        if (selectedShift == null) return;

        string employeeName = AnsiConsole.Prompt(new TextPrompt<string>("Enter the new employee name"));

        (DateTime?, DateTime?) times = GetStartAndEndTimes();

        if (times.Item1 == null || times.Item2 == null)
        {
            return;
        }

        Shift updatedShift = new Shift
        {
            Id = selectedShift.Id,
            EmployeeName = employeeName,
            StartTime = times.Item1.Value,
            EndTime = times.Item2.Value,
        };

        try
        {
            await _shiftApiService.UpdateShift(updatedShift);
            AnsiConsole.MarkupLine("[green]Shift updated successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]There was an error while editing the shift! Please try again later.[/]");
        }

        AnsiConsole.MarkupLine("\nPress enter to continue");
        AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
    }

    public async Task DeleteShift()
    {
        AnsiConsole.Clear();

        Shift? selectedShift = await ChooseShiftAsync();
        if (selectedShift == null) return;

        try
        {
            await _shiftApiService.DeleteShift(selectedShift.Id);
            AnsiConsole.MarkupLine("[green]Shift deleted successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]There was an error while deleting the shift! Please try again later.[/]");
        }

        AnsiConsole.MarkupLine("\nPress enter to continue");
        AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
    }

    internal static (DateTime?, DateTime?) GetStartAndEndTimes()
    {
        DateTime? startTime;
        DateTime? endTime;

        startTime = Validation.ValidateTimeInput();
        if (startTime == null)
        {
            return (null, null);
        }

        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold]Now, type the ending time of your shift\n[/]");
        endTime = Validation.ValidateEndTimeInput(startTime);
        if (endTime == null)
        {
            return (null, null);
        }

        return (startTime, endTime);
    }

    private async Task<Shift?> ChooseShiftAsync()
    {
        try
        {
            List<Shift>? shifts = await _shiftApiService.GetAllShiftsAsync();

            if (shifts == null || shifts.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No shifts found.[/]");
                AnsiConsole.MarkupLine("\nPress enter to continue");
                AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
                return null;
            }

            // Create a list of choices with "None" as the first option.
            var choices = new List<object> { "None" };
            choices.AddRange(shifts);

            object selected = AnsiConsole.Prompt(
                new SelectionPrompt<object>()
                    .Title("Select a shift (choose 'None' to return to the menu):")
                    .PageSize(10)
                    .AddChoices(choices)
                    .UseConverter(obj =>
                    {
                        if (obj is string str)
                            return str;
                        else if (obj is Shift shift)
                            return $"Employee: {shift.EmployeeName}\tDuration: {shift.Duration}\tStart: {shift.StartTime:yyyy-MM-dd HH:mm}\tEnd: {shift.EndTime:yyyy-MM-dd HH:mm}";
                        return string.Empty;
                    })
            );

            if (selected is string s && s == "None")
            {
                return null;
            }
            else if (selected is Shift chosenShift)
            {
                return chosenShift;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]There was an error when retrieving the shifts! Please try again later.[/]");
            AnsiConsole.MarkupLine("\nPress enter to continue");
            AnsiConsole.Prompt(new TextPrompt<string>("").AllowEmpty());
            return null;
        }
    }

    public enum MainMenuChoices
    {
        View,
        Create,
        Edit,
        Delete,
        Exit
    }
}
