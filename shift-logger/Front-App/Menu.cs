using API.Models;
using Front_App.Helpers;
using Front_App.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Front_App;

public class Menu
{
    private readonly IShiftClient _shiftApiClient;

    public Menu(IShiftClient shiftClient)
    {
        _shiftApiClient = shiftClient;
    }

    private MenuChoices GetUserMenuChoice()
    {
        var selection = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
            .Title("What would you want to do?")
            .AddChoices(Enum.GetValues<MenuChoices>())
            .UseConverter(choice => choice.GetDescription())
            );
        return selection;
    }

    public async Task ShowMenu()
    {
        IEnumerable<Shift> shifts = new List<Shift>();
        var selection = GetUserMenuChoice();
        while (selection != MenuChoices.Exit)
        {
            switch (selection)
            {
                case MenuChoices.ShowShifts:
                    shifts = await _shiftApiClient.GetShiftsAsync();
                    if (!shifts.Any())
                    {
                        AnsiConsole.MarkupLine("[cyan] No shifts yet,add some first![/]");
                        break;
                    }
                    AnsiConsole.MarkupLine("[green]Showing all shifts...[/]");
                    DataVisualizer.DisplayShifts(shifts);
                    break;

                case MenuChoices.AddShift:
                    var shift = UserSelection.CreateShift();
                    await _shiftApiClient.CreateShiftAsync(shift);
                    AnsiConsole.MarkupLine("[green]Adding a new shift...[/]");
                    break;

                case MenuChoices.EditShift:
                    shifts = await _shiftApiClient.GetShiftsAsync();
                    if (!shifts.Any())
                    {
                        AnsiConsole.MarkupLine("[cyan] No shifts yet,add some first![/]");
                        break;
                    }
                    DataVisualizer.DisplayShifts(shifts);
                    var IdToEdit = UserSelection.GetId(shifts);
                    var newShift = UserSelection.CreateShift();
                    newShift.Id = IdToEdit;
                    await _shiftApiClient.UpdateShiftAsync(newShift);
                    AnsiConsole.MarkupLine("[green]Editing an existing shift...[/]");
                    break;

                case MenuChoices.DeleteShift:
                    shifts = await _shiftApiClient.GetShiftsAsync();
                    if (!shifts.Any())
                    {
                        AnsiConsole.MarkupLine("[cyan] No shifts yet,add some first![/]");
                        break;
                    }
                    DataVisualizer.DisplayShifts(shifts);
                    var IdToDelete = UserSelection.GetId(shifts);
                    await _shiftApiClient.DeleteShiftAsync(IdToDelete);
                    AnsiConsole.MarkupLine("[green]Deleting a shift...[/]");
                    break;
            }
            selection = GetUserMenuChoice();
            AnsiConsole.Clear();
        }
    }
}