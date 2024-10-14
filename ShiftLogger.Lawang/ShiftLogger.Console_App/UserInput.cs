using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using ShiftLogger.Console_App.Models;
using Spectre.Console;

namespace ShiftLogger.Console_App;

public class UserInput
{
    public string ChooseMenuOperation()
    {
        var listOfMenuOption = new List<string>()
        {
            "Get All Shifts",
            "Create Shift",
            "Update Shift",
            "Delete Shift",
            "Exit"
        };

        return GetSelection(listOfMenuOption, "[yellow bold]Choose your operation[/]", "Select from the Menu given below");
    }

    private string GetSelection(List<string> listOfOption, string heading, string title)
    {
        AnsiConsole.Write(new Rule($"{heading}").LeftJustified().RuleStyle("red"));
        Console.WriteLine();

        var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"{title}")
            .MoreChoicesText("[grey bold](Press 'up' and 'down' key to navigate)[/]")
            .AddChoices(listOfOption)
            .HighlightStyle(Color.Blue3)
            .WrapAround()
        );

        return selection;
    }

    public int GetId()
    {
        var id = AnsiConsole.Ask<int>("[yellow bold]Enter the Id: [/]");
        while (id < 1)
        {
            AnsiConsole.MarkupLine("[red bold]Id value cannot be '0' or negative number!![/]");
            id = AnsiConsole.Ask<int>("[yellow bold]Please Enter the Id again: [/]");
        }
        return id;
    }

    public Shift? CreateShift()
    {
        AnsiConsole.MarkupLine("[grey bold](Enter '0' to go back to menu)[/]");
        var name = AnsiConsole.Ask<string>("[yellow bold]Enter the Name of the Employee: [/]");
        if (name == "0") return null;

        View.ShowDateInstruction();

        var start = Validation.CheckDate("Start");
        if (start == null) return null;

        var end = Validation.CheckDate("End");
        if (end == null) return null;

        if (end < start)
        {
            end = end.Value.AddDays(1);
        }

        return new Shift()
        {
            EmployeeName = name,
            Start = start.Value,
            End = end.Value
        };
    }

    public Shift? UpdateShift(List<Shift> shifts)
    {
        AnsiConsole.MarkupLine("[grey bold](Press '0' to go back to menu)[/]");
        int shiftID = AnsiConsole.Ask<int>("[olive bold]Enter the Id to Update: [/]");
        while (Validation.CheckId(shifts, shiftID))
        {
            if (shiftID == 0) return null;
            AnsiConsole.MarkupLine($"[red bold]Id: {shiftID} doesn't exist in the database. Please Enter another Id.[/]");
            shiftID = AnsiConsole.Ask<int>("[olive bold]Please Enter the Id in present in the above table: [/]");
        }

        var updateShift = shifts.First(s => s.Id == shiftID);

        Console.Clear();

        if (Validation.UpdateConfirm("Do you want to update employee name: ", "yellow"))
        {
            AnsiConsole.MarkupLine("[grey bold](Enter '0' to go back to menu)[/]");
            var name = AnsiConsole.Ask<string>("[yellow bold]Enter the Name of the Employee: [/]");
            if (name == "0") return null;
            updateShift.EmployeeName = name;
        }


        View.ShowDateInstruction();

        if (Validation.UpdateConfirm("Do you want to update Start time of the shift: ", "green"))
        {
            var start = Validation.CheckDate("Start");
            if (start == null) return null;
            updateShift.Start = start.Value;
        }
        Console.WriteLine();
        if (Validation.UpdateConfirm("Do you want to update End time of the shift: ", "red"))
        {
            var end = Validation.CheckDate("End");
            if (end == null) return null;
            updateShift.End = end.Value;
        }



        if (updateShift.End < updateShift.Start)
        {
            updateShift.End = updateShift.End.AddDays(1);
        }

        return updateShift;
    }

    public int DeleteId(List<Shift> shifts)
    {
        AnsiConsole.MarkupLine("[grey bold](Enter '0' to go back to menu)[/]");
        int shiftId = AnsiConsole.Ask<int>("[greenyellow bold]Enter the Id to Delete: [/]");
        while (Validation.CheckId(shifts, shiftId))
        {
            if (shiftId == 0) return 0;
            AnsiConsole.MarkupLine($"[red bold]Id: {shiftId} doesn't exist in the database. Please Enter another Id.[/]");
            shiftId = AnsiConsole.Ask<int>("[olive bold]Please Enter the Id in present in the above table: [/]");
        }

        return shiftId;
    }
}

