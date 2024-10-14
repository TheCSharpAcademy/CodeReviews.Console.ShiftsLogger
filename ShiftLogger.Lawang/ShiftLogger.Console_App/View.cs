using ShiftLogger.Console_App.Models;
using Spectre.Console;

namespace ShiftLogger.Console_App;

public static class View
{
    public static void RenderTitle(string title)
    {
        var panel = new Panel(new FigletText($"{title}").Color(Color.Red))
                   .BorderColor(Color.Aquamarine3)
                   .PadTop(1)
                   .PadBottom(1)
                   .Header(new PanelHeader("[blue3 bold]APPLICATION[/]"))
                   .Border(BoxBorder.Double)
                   .Expand();

        AnsiConsole.Write(panel);
    }

    public static void RenderResult(string title, string color, Color borderColor)
    {
        var panel = new Panel(new Markup($"[{color} bold]{title}[/]"))
                           .BorderColor(borderColor)
                           .PadTop(1)
                           .PadBottom(1)
                           .Header(new PanelHeader("[blue3 bold]RESULT[/]"))
                           .Border(BoxBorder.Rounded)
                           .Expand();

        AnsiConsole.Write(panel);
    }
    public static void RenderTable(IEnumerable<Shift> listOfShift, Color Aqua)
    {
        if (listOfShift.Count() == 0)
        {
            Panel panel = new Panel(new Markup("[red bold]CONTACT IS EMPTY![/]"))
                .Border(BoxBorder.Heavy)
                .BorderColor(Color.IndianRed1_1)
                .Padding(1, 1, 1, 1)
                .Header("Result");

            AnsiConsole.Write(panel);
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Expand()
            .BorderColor(Aqua)
            .ShowRowSeparators();

        table.AddColumns(new TableColumn[]
        {
           new TableColumn("[darkgreen bold]Id[/]").Centered(),
           new TableColumn("[darkcyan bold]Employee Name[/]").Centered(),
           new TableColumn("[darkcyan bold]Start Time[/]").Centered(),
           new TableColumn("[darkcyan bold]End Time[/]").Centered(),
           new TableColumn("[darkgreen bold]Duration[/]").Centered()
        });

        foreach (var shift in listOfShift)
        {
            table.AddRow(
                new Markup($"[cyan1]{shift.Id}[/]").Centered(),
                new Markup($"[turquoise2]{shift.EmployeeName}[/]").Centered(),
                new Markup($"[turquoise2]{shift.Start.ToString("h:mm tt")}[/]").Centered(),
                new Markup($"[turquoise2]{shift.End.ToString("h:mm tt")}[/]").Centered(),
                new Markup($"[turquoise2]{shift.Duration}[/]").Centered()

            );
        }

        AnsiConsole.Write(table);
    }

    public static void ShowDateInstruction()
    {
        Console.Clear();
        var panel = new Panel(new Markup("Please enter a [green]time[/] (e.g., 12:30 [cyan]AM[/] or 02:30 [cyan]PM[/]) in 12 hr format:\n\t\t[grey bold](press '0' to go back to Menu.)[/]"))
                .Header("[bold cyan]Time Input[/]", Justify.Center)
                .Padding(1, 1, 1, 1)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Blue3);

        // Render the panel
        AnsiConsole.Write(panel);
    }
}
