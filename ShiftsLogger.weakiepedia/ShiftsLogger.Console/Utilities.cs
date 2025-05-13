using System.Globalization;
using ShiftsLogger.Console.Models;
using Spectre.Console;

namespace ShiftsLogger.Console;

public class Utilities
{
    public long CalculateTimeDifference(DateTime startTime, DateTime endTime)
    {
        return (long)(endTime - startTime).TotalSeconds;
    }

    public TimeSpan CalculateDuration(long duration)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(duration);
        
        return timeSpan;
    }

    public DateTime ConvertToDate(string dateInput)
    {
        try
        {
            DateTime date = DateTime.ParseExact(dateInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            
            return date;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error - {ex.Message}[/]");
            
            return DateTime.MinValue;
        }
    }

    public bool ValidateDates(DateTime? startDate, DateTime? endDate)
    {
        if (startDate > endDate)
        {
            AnsiConsole.MarkupLine("[red]Error - start date can't be later than the end date.[/]");
            
            return false;
        }

        if (DateTime.Now < startDate || DateTime.Now < endDate)
        {
            AnsiConsole.MarkupLine("[red]Error - dates can't come from the future.[/]");
            
            return false;
        }

        return true;
    }
}