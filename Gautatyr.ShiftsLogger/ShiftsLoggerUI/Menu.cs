using static ShiftsLoggerUI.Helpers;
using static ShiftsLoggerUI.DataValidation;
using static ShiftsLoggerUI.InterfaceApi;
using ShiftsLoggerUI.Models;
using ConsoleTableExt;

namespace ShiftsLoggerUI;

public static class Menu
{
    public static async void MainMenu(string error = "")
    {
        Console.Clear();
        Console.WriteLine("\nMAIN MENU\n");
        await DisplayShifts();
        if (!string.IsNullOrEmpty(error)) DisplayError(error);

        Console.WriteLine("\n- Type 1 to Add a new Shift");
        Console.WriteLine("- Type 2 to Update a shift");
        Console.WriteLine("- Type 3 to Delete a shift");
        Console.WriteLine("- Type 0 to Close the Application");

        switch (GetNumberInput())
        {
            case 0:
                Environment.Exit(0);
                break;
            case 1:
                AddShiftMenu();
                break;
            case 2:
                UpdateShiftMenu();
                break;
            case 3:
                DeleteShiftMenu();
                break;
            default:
                error = "Wrong input ! Please type a number between 0 and 3";
                MainMenu(error);
                break;
        }

        MainMenu();
    }

    private static async Task DisplayShifts()
    {
        List<Shift> unformatedShifts = GetShifts().Result;
        List<ShiftDTODisplay> formatedShifts = new();
        
        foreach (Shift shift in unformatedShifts)
        {
            formatedShifts.Add(new ShiftDTODisplay
            {
                Id = shift.Id,
                Start = shift.Start.ToString("HH:mm"),
                End = shift.End.ToString("HH:mm"),
            });

            if (shift.End < shift.Start)
            {
                TimeSpan day = TimeSpan.Parse("24:00:00");
                TimeSpan dif = shift.Start - shift.End;
                var temp = (day - dif).ToString();
                formatedShifts.Last().Duration = temp.Substring(3);
            }
            else
            {
                formatedShifts.Last().Duration = (shift.End - shift.Start).ToString();
            }
        }

        ConsoleTableBuilder
                .From(formatedShifts)
                .ExportAndWriteLine();
    }

    private static void AddShiftMenu()
    {
        Console.WriteLine("\nEnter the shift's starting time (Format: HH:mm)\n");
        var shiftStart = GetShiftInput();
        Console.WriteLine("\nEnter the shift's ending time (Format: HH:mm)\n");
        var shiftEnd = GetShiftInput();

        CreateShift(shiftStart, shiftEnd);
    }

    private static void UpdateShiftMenu()
    {
        Console.WriteLine("\nType in the id of the shift you wish to Udpate\n");
        var id = GetShiftIdInput();
        Console.WriteLine("\nEnter the shift's starting time (Format: hh:MM)\n");
        var shiftStart = GetShiftInput();
        Console.WriteLine("\nEnter the shift's ending time (Format: hh:MM)\n");
        var shiftEnd = GetShiftInput();

        UpdateShift(id, shiftStart, shiftEnd);
    }

    private static void DeleteShiftMenu()
    {
        Console.WriteLine("\nType in the id of the shift you wish to Delete\n");
        var id = GetShiftIdInput();

        DeleteShift(id);
    }
}
