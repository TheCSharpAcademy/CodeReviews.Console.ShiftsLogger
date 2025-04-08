using Spectre.Console;
using System.Diagnostics;
using UI.Control;

namespace UI.View;

internal static class MainMenu
{
    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Helpers.PrintHeader();

            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[Green1]Main Menu[/]")
                .PageSize(10)
                .HighlightStyle(new Style(Color.Green1))
                .AddChoices<string>("View All Shifts", "Add Shift","View Shift By Employee Name", "Delete Shift", "Update Shift", "Exit"));

            switch (choice)
            {
                case "View All Shifts":
                    GetAllShifts.Get();
                    break;
                case "Add Shift":
                    AddShift.Insert();
                    break;
                case "View Shift By Employee Name":
                    GetShiftsByEmployee.Get();
                    break;
                case "Delete Shift":
                    DeleteShiftById.Delete();
                    break;
                case "Update Shift":
                    UpdateShift.Update();
                    break;
                case "Exit":
                    CloseApiProcesses();
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static void CloseApiProcesses()
    {
        var apiProcessName = "API";
        var apiProcesses = Process.GetProcessesByName(apiProcessName);
        foreach (var process in apiProcesses)
        {
            if (!process.CloseMainWindow())
            {
                process.Kill();
            }
            process.WaitForExit();
        }
    }
}
