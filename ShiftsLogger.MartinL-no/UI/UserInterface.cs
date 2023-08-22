using Spectre.Console;
using UI.Models;
using UI.Services;
using static UI.Enums;

namespace UI;

static public class UserInterface
{
    static public async Task MainMenuAsync()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MainMenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MainMenuOptions.AddShift,
                MainMenuOptions.UpdateShift,
                MainMenuOptions.DeleteShift,
                MainMenuOptions.ViewAllShifts,
                MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.AddShift:
                    await ShiftService.InsertShiftAsync();
                    break;
                case MainMenuOptions.UpdateShift:
                    await ShiftService.UpdateShiftAsync();
                    break;
                case MainMenuOptions.DeleteShift:
                    await ShiftService.DeleteShiftAsync();
                    break;
                case MainMenuOptions.ViewAllShifts:
                    await ShiftService.ViewAllShiftsAsync();
                    break;
                case MainMenuOptions.Quit:
                    Console.WriteLine("Goodbye");
                    isAppRunning = false;
                    break;
            }
        }
    }

    static public void ShowShifts(IEnumerable<Shift> shifts)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.StartTime.ToString("yyyy-MM-dd HH:mm"),
                shift.EndTime.ToString("yyyy-MM-dd HH:mm"),
                shift.Duration.ToString(@"hh\:mm")
                ); ;
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press Any Key to Return to Menu");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowShifts(Shift shift)
    {
        var panel = new Panel($"""
            Id:         {shift.Id}
            Start Time: {shift.StartTime.ToString("yyyy-MM-dd HH:mm")}
            End Time:   {shift.EndTime.ToString("yyyy-MM-dd HH:mm")}
            Duration:   {shift.Duration.ToString(@"hh\:mm")}
            """);
        panel.Header = new PanelHeader("Shift info");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
    }
}