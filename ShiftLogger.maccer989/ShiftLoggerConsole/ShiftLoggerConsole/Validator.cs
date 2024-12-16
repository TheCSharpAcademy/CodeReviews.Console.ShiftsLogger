using RestSharp;
using System.Globalization;
using System.Web;

namespace ShiftLogger
{
    public static class Validator
    {
        public static bool CheckValidTime(string timeInput)
        {
            bool output = false;
            while (!DateTime.TryParseExact(timeInput, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\nInvalid time. (Format: HH:mm). Try again:");
                timeInput = Console.ReadLine();
                if (DateTime.TryParseExact(timeInput, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                {
                    output = true;
                }
            }
            output = true;
            return output;
        }
        public static string CheckValidNumber(string message)
        {
            bool isValidNumber;
            int output;
            do
            {
                Console.WriteLine(message);
                string numberInput = Console.ReadLine();
                isValidNumber = int.TryParse(numberInput, out output);   
            } while (isValidNumber == false);

            return output.ToString();
        }
        public static string CheckShiftId(string recordId)
        {
            bool isValidRecord = false;
            do
            {
                var client = new RestClient("https://localhost:7048/");
                var request = new RestRequest($"api/ShiftLogger/{HttpUtility.UrlEncode(recordId)}", Method.Get);
                var response = client.ExecuteAsync(request);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isValidRecord = true;
                    Console.WriteLine("Shift Record is Valid");
                }
                else if (response.Result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("API Error: Status Code - " + response.Result.StatusCode + " Returning to Main Menu");
                    recordId = "0";
                    break;
                }
                else
                {
                    string message = "Shift Id does not exist.Enter a Shift Id:";
                    recordId = CheckValidNumber(message);
                }
            } while (isValidRecord == false);
            return recordId;
        }
        public static bool CheckIsValidDate(string inputDate)
        {
            string[] dateComponents = inputDate.Split(':');
            if (dateComponents.Length != 3)
            {
                return false;
            }
            if (int.TryParse(dateComponents[0], out int day) &&
                int.TryParse(dateComponents[1], out int month) &&
                int.TryParse(dateComponents[2], out int year))
            {
                if (day >= 1 && day <= 31 &&
                    month >= 1 && month <= 12 &&
                    year >= 0 && year <= 99)
                {
                    if (IsValidMonthDayCombination(day, month, year))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool IsValidMonthDayCombination(int day, int month, int year)
        {
            int[] maxDaysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (IsLeapYear(year))
            {
                maxDaysInMonth[1] = 29;
            }
            return day >= 1 && day <= maxDaysInMonth[month - 1];
        }
        static bool IsLeapYear(int year)
        {
           return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        public static string CheckIsValidName (string message)
        {
            bool isNullOrEmpty;
            string name ="";
            do
            {
                Console.WriteLine(message);
                name = Console.ReadLine();                
                if (string.IsNullOrWhiteSpace(name) || name.Contains(" "))
                {
                    isNullOrEmpty = true;
                }
                else
                {
                    isNullOrEmpty = false;
                }
            } while (isNullOrEmpty == true);
            return name;
        }
    }
}
