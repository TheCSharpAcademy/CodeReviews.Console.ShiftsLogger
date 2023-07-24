using System.Globalization;

namespace ShiftLoggerApp
{
    internal class Validator
    {
        public static bool ValidateStartDateString(string startTimeString,  string format, CultureInfo culture, DateTimeStyles styles)
        {
            bool tryParseStartDateString = DateTime.TryParseExact(startTimeString, format, culture, styles, out _);
            DateTime startTime = DateTime.ParseExact(startTimeString, format, culture, styles);

            if (tryParseStartDateString == false)
            {
                Console.WriteLine("Wrong format. You can only use the one provided that is dd/mm/yyyy hh:mm. Example: 01/01/2023 12:00 ");

                return false;
            }

            return true;
        }

        public static bool ValidateEndDateString(string startTimeString, string endTimeString, string format, CultureInfo culture, DateTimeStyles styles)
        {
            bool tryParseStartDateString = DateTime.TryParseExact(startTimeString, format, culture, styles, out _);
            bool tryParseEndDateString = DateTime.TryParseExact(endTimeString, format, culture, styles, out _);
            DateTime startTime = DateTime.ParseExact(startTimeString, format, culture, styles);
            DateTime endTime = DateTime.ParseExact(endTimeString, format, culture, styles);

            if (tryParseEndDateString == false)
            {
                Console.WriteLine("Wrong format. You can only use the one provided that is dd/mm/yyyy hh:mm. Example: 01/01/2023 12:00 ");

                return false;
            }
            if (endTime < startTime)
            {
                Console.WriteLine("The time in which your shift ended was before it even started! Try again, or go back to the main menu to insert the time in which your shift started once again.");

                return false;
            }
            if ((endTime - startTime).TotalDays >= 1)
            {
                Console.WriteLine("You can't insert a shift of 24 hours or longer. In case it was 24 hours long, please insert a 23:59 hours shift long.");
                return false;
            }

            return true;
        }

        public static bool ValidateString(string stringToValidate)
        {
            if (String.IsNullOrEmpty(stringToValidate))
            {
                Console.WriteLine("You can't leave this field empty!");

                return false;
            }

            foreach(char c in stringToValidate)
            {
                if (!Char.IsLetter(c) && c != ' ')
                {
                    Console.WriteLine("Your name must only use letters from the latin alphabet. Please don't use numbers or special numbers and try again");
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateId(string idToValidate, List<Shift> shiftList)
        {
            if (!Int32.TryParse(idToValidate, out _))
            {
                Console.WriteLine("You didn't enter a valid id. Please enter a number.");

                return false;
            }

            if (!shiftList.Any(s => s.Id == Int32.Parse(idToValidate)))
            {
                Console.WriteLine("No shift contains that id. Please check your shifts and try again.");

                return false;
            }

            return true;
        }
    }
}
