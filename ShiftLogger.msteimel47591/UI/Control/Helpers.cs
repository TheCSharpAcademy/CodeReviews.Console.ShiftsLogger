using Spectre.Console;
using System.Text.RegularExpressions;

namespace UI.Control;
internal static class Helpers
{
    public static void PrintHeader()
    {
        Console.Clear();
        AnsiConsole.Write(new FigletText("ShiftTracker").Color(Color.Green1));
    }

    public static string GetName()
    {
        bool isValid = false;
        string name = string.Empty;

        while (!isValid)
        {
            name = AnsiConsole.Ask<string>("Enter employee name");

            if (ValidateName(name))
            {
                isValid = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[Red]Invalid name. Only letters and spaces are allowed.[/]\n");
            }
        }

        return name.Trim();
    }

    internal static bool ValidateName(string name)
    {
        if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
