using Microsoft.Extensions.Logging;
using ShiftsLogger.Domain;
using ShiftsLogger.App.Client;
using ShiftsLogger.App.Services.Interfaces;
using Spectre.Console;

namespace ShiftsLogger.App.App;

public class ShiftsLoggerApp
{
    private readonly ILogger<ShiftsLoggerApp> _log;
    private readonly ShiftsApiClient _shiftClient;
    //private readonly IInputCancellationService _inputCancellationService;
    private readonly IUserInputValidationService _userInputValidation;

    public ShiftsLoggerApp(
        ILogger<ShiftsLoggerApp> log,
        ShiftsApiClient shiftClient,
        //IInputCancellationService inputCancellationService,
        IUserInputValidationService userInputValidation)
    {
        _log = log;
        _shiftClient = shiftClient;
        //_inputCancellationService = inputCancellationService;
        _userInputValidation = userInputValidation;
    }

    public async Task RunApp()
    {
        try
        {
            await ShowApp();
        }
        catch (Exception ex)
        {
            await ShowApp();
        }

    }

    private async Task ShowApp()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AppHelperFunctions.AppHeader("ShiftsLogger", true);

            var fShifts = await _shiftClient.ShiftsAllAsync();
            var dummy = new Shift(nameof(Shift.EmployeeName), nameof(Shift.ShiftDescription), DateTime.Now, DateTime.Now);
            fShifts.Add(dummy);
            var listShifts = fShifts.ToList();

            var table = new Table().Centered().Expand();
            table.Border = TableBorder.Rounded;

            table.AddColumn("Employee").Centered();
            table.AddColumn("ShiftDescription").Centered();
            table.AddColumn("ShiftStart").Centered();
            table.AddColumn("ShiftEnd").Centered();
            table.AddColumn("ShiftDuration").Centered();





            int selectedIndex = 0;
            bool exit = false;
            Shift selectedShift = null;

            await AnsiConsole.Live(table)
                .Overflow(VerticalOverflow.Ellipsis)
                .StartAsync(async ctx =>
                {
                    while (!exit)
                    {
                        table.Rows.Clear();
                        table.Title("[[ [green] Shift Overview [/]]]");
                        table.Caption("[[[blue] [[Up/Down]] Navigation, [[Enter]] Select, [[ESC]] Escape[/]]]");

                        for (int i = 0; i < listShifts.Count; i++)
                        {
                            var shift = listShifts[i];
                            if (listShifts.Count - 1 == i)
                            {
                                if (i == selectedIndex)
                                {
                                    table.AddRow($"[blue] > {nameof(Shift.EmployeeName)} <[/]",
                                                 $"[blue]> {nameof(Shift.ShiftDescription)} <[/]",
                                                 $"[blue]> {shift.ShiftStart} <[/]",
                                                 $"[blue]> {shift.ShiftEnd} <[/]",
                                                 $"[blue]> 00:00:00 <[/]");

                                }
                                else
                                {
                                    table.AddRow($"[dim]> {nameof(Shift.EmployeeName)} <[/]",
                                                       $"[dim]> {nameof(Shift.ShiftDescription)} <[/]",
                                                       $"[dim]> {shift.ShiftStart} <[/]",
                                                       $"[dim]> {shift.ShiftEnd} <[/]",
                                                       $"[dim]> 00:00:00 <[/]");
                                }

                            }
                            else if (i == selectedIndex)
                            {
                                table.AddRow($"[blue]> {shift.EmployeeName}[/]",
                                       $"[blue]{shift.ShiftDescription}[/]",
                                       $"[blue]{shift.ShiftStart}[/]",
                                       $"[blue]{shift.ShiftEnd}[/]",
                                       $"[blue]{shift.ShiftDuration}[/]");
                            }
                            else
                            {
                                table.AddRow(shift.EmployeeName,
                                                      shift.ShiftDescription,
                                                      shift.ShiftStart.ToString(),
                                                      shift.ShiftEnd.ToString(),
                                                      shift.ShiftDuration.ToString());
                            }
                        }


                        ctx.Refresh();

                        var key = Console.ReadKey(true).Key;

                        switch (key)
                        {
                            case ConsoleKey.Escape:
                                exit = true;
                                break;

                            case ConsoleKey.UpArrow:
                                selectedIndex--;
                                if (selectedIndex < 0)
                                    selectedIndex = listShifts.Count - 1;
                                break;

                            case ConsoleKey.DownArrow:
                                selectedIndex++;
                                if (selectedIndex >= listShifts.Count)
                                    selectedIndex = 0;
                                break;

                            case ConsoleKey.Enter:

                                selectedShift = listShifts[selectedIndex];
                                exit = true;
                                break;
                        }
                    }
                });


            if (selectedShift != null)
            {
                if (selectedShift.Id == 0)
                {

                    var codSes = _userInputValidation.ValidateUserInput();
                    await _shiftClient.ShiftsPOSTAsync(codSes);

                }
                else
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[] {
                              "Update", "Delete", "Back"
                            }));



                    if (choice == "Update")
                    {

                        var codSes = _userInputValidation.ValidateUserInput(selectedShift);
                        await _shiftClient.ShiftsPUTAsync(codSes);

                    }
                    if (choice == "Delete")
                    {
                        var confirmation = AnsiConsole.Prompt(
                            new TextPrompt<bool>($"[yellow]Are you sure you want to [red]Delete[/] the Phonebook?: [/]")
                                .AddChoice(true)
                                .AddChoice(false)
                                .DefaultValue(false)
                                .WithConverter(choice => choice ? "y" : "n"));

                        if (confirmation)
                        {
                            await _shiftClient.ShiftsDELETEAsync(selectedShift.Id);
                        }
                    }
                }
            }
            else
            {


                if (AppHelperFunctions.ReturnMenu())
                {
                    break;
                }
            }
        }

    }
}
