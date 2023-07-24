using Spectre.Console;
using System.Globalization;

namespace ShiftLoggerApp
{
    public class UserInput
    {
        private static string format = "dd/MM/yyyy HH:mm";
        public static string GetUserFullName()
        {
            string fullName = AnsiConsole.Ask<string>("REMINDER: You can go back to the main menu anytime by typing M.\n\nEnter your full name:");

            if (fullName == "M")
            {
                Program.DisplayMainMenu();
            }

            while (!Validator.ValidateString(fullName))
            {
                fullName = GetUserFullName();
            }

            return fullName;
        }

        public static string GetStartDate()
        {
            string startTime = AnsiConsole.Ask<string>("Enter the date and hour in which your shift started with the following format: dd/mm/yyyy hh:mm (in 24 hour format)");

            if (startTime == "M")
            {
                Program.DisplayMainMenu();
            }

            while (!Validator.ValidateStartDateString(startTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None))
            {
                startTime = GetStartDate();
            }

            return startTime;
        }
        public static string GetEndDate(string startTime)
        {
            string endTime = AnsiConsole.Ask<string>("\n" +
                "Enter the date and hour in which your shift ended with the following format: dd/mm/yyyy hh:mm (in 24 hour format)");

            if (endTime == "M")
            {
                Program.DisplayMainMenu();
            }

            while (!Validator.ValidateEndDateString(startTime, endTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None))
            {
                endTime = GetEndDate(startTime);
            }

            return endTime;
        }

        public static string GetId(string message, List<Shift> shiftList)
        {
            string id = AnsiConsole.Ask<String>(message);

            if (id == "M")
            {
                Program.DisplayMainMenu();
            }

            while (!Validator.ValidateId(id, shiftList))
            {
                id = AnsiConsole.Ask<String>(message);
            }

            return id;
        }
    }
}
