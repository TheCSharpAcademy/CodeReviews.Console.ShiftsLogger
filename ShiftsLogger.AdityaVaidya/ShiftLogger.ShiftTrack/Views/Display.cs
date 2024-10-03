using Spectre.Console;
using ShiftLogger.ShiftTrack.Models;
using ShiftLogger.ShiftTrack.Helper;

namespace ShiftLogger.ShiftTrack.Views;

internal static class Display
{
    public static string GetSelection(string title, string[] choices)
    {
        var selectedCategory = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(title).AddChoices(choices).HighlightStyle(new Style(foreground: Color.Blue)));
        return selectedCategory;
    }

    public static void DisplayWorkers(List<Worker> workers, string[] columns, string title = "Worker List")
    {
        var table = new Table();
        table.Title = new TableTitle(title);
        foreach (var column in columns)
        {
            table.AddColumn(column);
        }
        foreach (var worker in workers)
        {
            table.AddRow(worker.WorkerId.ToString(), worker.Name, worker.EmailId);
        }
        AnsiConsole.Write(table);
    }

    public static void DisplayShifts(List<Shift> shifts, string[] columns, string title = "All Shifts")
    {
        var table = new Table();
        table.Title = new TableTitle(title);
        foreach (var column in columns)
        {
            table.AddColumn(column);
        }
        foreach (var shift in shifts)
        {
            table.AddRow(shift.Date,shift.StartTime, shift.EndTime, shift.Duration, shift.WorkerId.ToString());
        }
        AnsiConsole.Write(table);
    }

    public static Shift GetShiftDetails()
    {
        Shift shift = new Shift();
        string input;
        do
        {
            Console.Write("Enter the id of the worker for whom you wish to create the post:");
            input = Console.ReadLine();
        } while (!Validation.IsGivenInputInteger(input));
        do
        {
            Console.Write("Enter a date in the format yyyy-MM-dd (e.g., 2024-03-14) :");
            shift.Date = Console.ReadLine();
        } while (!Validation.ValidateInputDate(shift.Date));
        do
        {
            Console.Write("Enter valid Start-time in the format HH:mm (e.g., 12:30) :");
            shift.StartTime = Console.ReadLine();
        } while (!Validation.ValidateStartSessionTime(shift.StartTime));
        do
        {
            Console.Write("Enter valid End-time in the format HH:mm (e.g., 12:35) :");
            shift.EndTime = Console.ReadLine();
        } while (!Validation.ValidateEndSessionTime(shift.StartTime, shift.EndTime));
        shift.Duration = Validation.CalculateShiftDuration(shift.StartTime, shift.EndTime);
        return shift;
    }

    public static Worker GetWorkerDetails()
    {
        Worker worker = new Worker();
        Console.Write("Enter Name: ");
        worker.Name = Console.ReadLine();
        do
        {
            Console.Write("Enter Email: ");
            worker.EmailId = Console.ReadLine();
        } while (!Validation.IsValidEmail(worker.EmailId));
        return worker;
    }
}