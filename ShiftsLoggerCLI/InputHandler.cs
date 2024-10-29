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
    
    public ShiftCreate CreateShift(List<WorkerRead> workers)
    {
        int workerId = ChooseWorkerFromSelection(workers).Id;
        DateTime day;
        do
        {
            string dayStr = AnsiConsole.Prompt(new TextPrompt<string>($"Day of the shift ({DateFormat}) or press enter for Today: ").AllowEmpty());
            if (string.IsNullOrEmpty(dayStr))
            {
                day = DateTime.Today;
                break;
            }
            
            DateTime.TryParseExact(dayStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out day);
            
            if (day > DateTime.Today)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Dates[/]");
            }
        }while(day == DateTime.MinValue && day > DateTime.Today);
        DateTime start ;
        do
        {
            string startStr = AnsiConsole.Prompt(new TextPrompt<string>($"Enter Start Time ({TimeFormat})"));
            bool success = DateTime.TryParseExact(startStr, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start);
            if (!success)
                continue;
            if (day == DateTime.Today && new DateTime(day.Year,day.Month,day.Day,start.Hour,start.Minute,start.Second) > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Times[/]");
                continue;
            }
        }while(start == DateTime.MinValue && start > DateTime.Now);
        
        DateTime end ;
        do
        {
            string endStr = AnsiConsole.Prompt(new TextPrompt<string>($"Enter End Time ({TimeFormat}) or press enter for current time").AllowEmpty());
            if (string.IsNullOrEmpty(endStr))
            {
                end = DateTime.Now;
                break;
            }
            bool success = DateTime.TryParseExact(endStr, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end);
            if (!success)
                continue;
            if (day == DateTime.Today && new DateTime(day.Year,day.Month,day.Day,end.Hour,end.Minute,end.Second) > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Times[/]");
                continue;
            }
        }while(end == DateTime.MinValue);
        start = new DateTime(day.Year, day.Month, day.Day, start.Hour, start.Minute, start.Second);
        end = new DateTime(day.Year, day.Month, day.Day, end.Hour, end.Minute, end.Second);
        if (end < start)
            end = end.AddDays(1);
        return new ShiftCreate(workerId,start,end);
    }

    public ShiftRead ChooseShiftFromSelection(List<ShiftRead> shifts)
    {
        return AnsiConsole.Prompt(new SelectionPrompt<ShiftRead>().AddChoices(shifts)
            .Title("[yellow]--Shifts List--[/]")
            .HighlightStyle(new Style(Color.DarkTurquoise)));
    }

    
    public ShiftUpdate GetShiftUpdate(List<ShiftRead> shifts)
    {
        
        var shift = ChooseShiftFromSelection(shifts);
        AnsiConsole.MarkupLine("[green]Press Continue to skip any of the prompts[/]");
        DateTime day = DateTime.MinValue;
        do
        {
            string dayStr = AnsiConsole.Prompt(new TextPrompt<string>($"Day of the shift ({DateFormat}): ").AllowEmpty());
            if (string.IsNullOrEmpty(dayStr)) 
                break;
            
            
            DateTime.TryParseExact(dayStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out day);
            if (day > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Dates[/]");
                
            }
        }while(day == DateTime.MinValue || day > DateTime.Today);
        
        DateTime start = DateTime.MinValue ;
        do
        {
            string startStr = AnsiConsole.Prompt(new TextPrompt<string>($"Enter Start Time ({TimeFormat}):").AllowEmpty());
            if (string.IsNullOrEmpty(startStr))
                break;
            bool success =DateTime.TryParseExact(startStr, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start);
            if (!success)
                continue;
            if (day == DateTime.Today && new DateTime(day.Year,day.Month,day.Day,start.Hour,start.Minute,start.Second) > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Times[/]");
                continue;
            }
        }while(start == DateTime.MinValue || start > DateTime.Now);
        
        DateTime end = DateTime.MinValue; 
        do
        {
            string endStr = AnsiConsole.Prompt(new TextPrompt<string>($"Enter End Time ({TimeFormat}):").AllowEmpty());
            if (string.IsNullOrEmpty(endStr))
            {
                break;
            }
            bool success = DateTime.TryParseExact(endStr, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end);
            if (!success)
                continue;
            if (day == DateTime.Today && new DateTime(day.Year,day.Month,day.Day,end.Hour,end.Minute,end.Second) > DateTime.Now)
            {
                AnsiConsole.MarkupLine("[red]Cant Accept Future Times[/]");
                continue;
            }
        }while(end == DateTime.MinValue && end > DateTime.Now);

        if (start == DateTime.MinValue)
            start = shift.Start;
        if (end == DateTime.MinValue)
            end = shift.End;
        if (day != DateTime.MinValue)
        {
            start = new DateTime(day.Year, day.Month, day.Day, start.Hour, start.Minute, start.Second);
            end = new DateTime(day.Year, day.Month, day.Day, end.Hour, end.Minute, end.Second);
        }
        
        if (end < start )
            end = end.AddDays(1);
        return new ShiftUpdate(shift.Id,start,end);
    }
}