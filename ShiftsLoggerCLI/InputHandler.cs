using System.Globalization;
using System.Text.RegularExpressions;
using ShiftsLoggerCLI.Models;
using Spectre.Console;

namespace ShiftsLoggerCLI;

public partial class InputHandler
{
    private readonly Regex _emailValidation = MyRegex();
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]{2,}@[a-zA-Z]+\.[a-zA-Z]{2,}$")]
    private  static partial Regex MyRegex() ;
    private bool IsValidEmail(string email)
    {
        return _emailValidation.IsMatch(email);
    }
    
    const string DateFormat = "dd-MM-yyyy";
    const string TimeFormat = "HH:mm";
    public  WorkerCreate CreateWorker()
    {
        string name = AnsiConsole.Prompt(new TextPrompt<string>("Worker's name: "));
        string email = "placeholder";
        while (!IsValidEmail(email))
        {
            email = AnsiConsole.Prompt(new TextPrompt<string>("Worker's email: "));
        }

        string position = GetPosition();
        DateTime hireDate = DateTime.MinValue;
        do
        {
            string? dateStr = AnsiConsole.Prompt(new TextPrompt<string?>("Hiring date(dd-mm-yyyy) or press enter for current date: ").AllowEmpty());
             if (string.IsNullOrEmpty(dateStr))
            {
                hireDate = DateTime.Now;
                break;
            }
            DateTime.TryParseExact(dateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out hireDate);

            if (hireDate == DateTime.MinValue)
            {
                AnsiConsole.MarkupLine($"[red]Invalid date check format is {DateFormat}[/]");
                continue;
            }

            if (hireDate > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Doesnt Accept Future Dates[/]");
        
            }
        }while(hireDate == DateTime.MinValue || hireDate > DateTime.Now);
       
        return new WorkerCreate(name,email,position,hireDate);
    }

    public WorkerRead ChooseWorkerFromSelection(List<WorkerRead> workers)
    {
        return AnsiConsole.Prompt(new SelectionPrompt<WorkerRead>()
            .Title("[yellow]--Workers List--[/]")
            .AddChoices(workers)
            .HighlightStyle(new Style(Color.DarkTurquoise)));
    }
    public string GetPosition(bool optional = false)
    {
        var positions = PositionManager.Positions;
        if (optional) 
            positions.Add("Skip");
        string position = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("[yellow]Positions[/]")
            .HighlightStyle(new Style(Color.DarkTurquoise))
            .AddChoices(positions));
        if (position == "Add new position")
        {
            position = AnsiConsole.Prompt(new TextPrompt<string>("Positions: "));
            PositionManager.Positions.Add(position);
        }

        if (optional&&position == "Skip")
            position = string.Empty;
        return position;
    }
    public WorkerUpdate GetWorkerUpdate(int id)
    {
        AnsiConsole.MarkupLine("[green]In any choice you want to skip press enter[/]");
        string name = AnsiConsole.Prompt(new TextPrompt<string>("Worker's new name: ").AllowEmpty());
        string email = AnsiConsole.Prompt(new TextPrompt<string>("Worker's new email: ").AllowEmpty());
        string position = GetPosition(true);
        DateTime hireDate;
        do
        {
            string? dateStr = AnsiConsole.Prompt(new TextPrompt<string?>("Hiring date(dd-mm-yyyy) ").AllowEmpty());
            if (string.IsNullOrEmpty(dateStr))
            {
                hireDate = DateTime.MinValue;
                break;
            }
            DateTime.TryParseExact(dateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out hireDate);
            if (hireDate == DateTime.MinValue)
            {
                AnsiConsole.MarkupLine($"[red]Invalid date check format is {DateFormat}[/]");
                continue;
            }

            if (hireDate > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Doesnt Accept Future Dates[/]");
            }
        }while(hireDate > DateTime.Now);
        return new WorkerUpdate(id,name,email,position,hireDate);
    }

    
}