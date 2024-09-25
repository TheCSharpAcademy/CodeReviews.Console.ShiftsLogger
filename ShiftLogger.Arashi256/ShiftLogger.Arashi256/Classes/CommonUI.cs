using Spectre.Console;
using System.Text.RegularExpressions;

namespace ShiftLogger_Frontend.Arashi256.Classes
{
    internal class CommonUI
    {
        public static int MenuOption(string question, int min, int max)
        {
            int selectedValue = 0;
            var userInput = AnsiConsole.Ask<int>(question);
            selectedValue = userInput;
            if (selectedValue < min || selectedValue > max)
            {
                AnsiConsole.MarkupLine("[red]Invalid input. Please enter a value within the specified range.[/]");
            }
            return selectedValue;
        }

        public static void Pause(string colour)
        {
            AnsiConsole.Markup($"[{colour}]Press any key to continue...[/]");
            Console.ReadKey(true);
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            return Regex.IsMatch(email, emailPattern);
        }

        public static string? GetStringWithPrompt(string prompt, int lengthlimit)
        {
            AnsiConsole.MarkupLine("[white]Enter '0' to cancel[/]");
            string input = AnsiConsole.Ask<string>(prompt);
            if (input.Equals("0")) return null;
            while (input.Length < 2 || input.Length > lengthlimit)
            {
                AnsiConsole.MarkupLine($"\n[red]Please enter at least two characters and be less than {lengthlimit} characters. Try again.[/]\n\n");
                input = AnsiConsole.Ask<string>(prompt);
            }
            return input;
        }

        public static DateTime? GetDateTimeDialog(string format)
        {
            DateTime? dateTime = null;
            while (!dateTime.HasValue)
            {
                AnsiConsole.MarkupLine($"[steelblue1_1]Note: Enter '0' to abort[/]");
                var userInput = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a date/time in the format '{format}':").PromptStyle("white"));
                if (userInput.Trim() == "0")
                {
                    return null;
                }
                else
                {
                    if (DateTime.TryParseExact(userInput, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                    {
                        dateTime = result;
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Invalid date/time format. Please enter the date/time in the specified format.[/]");
                    }
                }
            }
            return dateTime;
        }
    }
}
