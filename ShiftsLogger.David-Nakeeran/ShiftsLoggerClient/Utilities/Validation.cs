using Spectre.Console;

namespace ShiftsLoggerClient.Utilities;

class Validation
{
    internal bool IsCharZero(string input)
    {
        if (input.Length == 1 && input.Equals("0")) return true;
        return false;
    }

    internal string CheckInputNullOrWhitespace(string message, string? input)
    {
        while (string.IsNullOrWhiteSpace(input))
        {
            input = AnsiConsole.Ask<string>(message);
        }
        return input;
    }

    internal bool IsCharValid(string input)
    {
        return input.All(c => Char.IsLetter(c) || c == ' ');
    }

    internal string ValidateString(string message, string input)
    {
        while (true)
        {
            input = CheckInputNullOrWhitespace(message, input);
            if (IsCharZero(input)) return input;
            if (IsCharValid(input)) break;
        }
        return input;
    }

    internal bool IsEndTimeLaterThanStartTime(DateTime start, DateTime end)
    {
        return end > start;
    }
}