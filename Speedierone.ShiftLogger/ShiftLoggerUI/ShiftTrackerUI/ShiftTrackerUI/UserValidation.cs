using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTrackerUI
{
    internal class UserValidation
    {
        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            var numberInput = Console.ReadLine();

            if (numberInput == "0") MainMenu.ShowMenu();

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("\nInvalid number. Try again");
                numberInput = Console.ReadLine();
            }
            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }

        internal static string CheckDate()
        {            
            string dateString = Console.ReadLine();
            string format = "dd/MM/yyyy";
 
            while(!DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                if (dateString == "0") { MainMenu.ShowMenu(); }
                else
                {
                    Console.WriteLine("Invalid format. Please enter in dd/mm/yyyy format");
                    dateString = Console.ReadLine();
                }
            }
            return dateString.ToString();        
        }

        internal static string CheckTime()
        {
            string timeString = Console.ReadLine();
            string format = "HH:mm";

            while(!DateTime.TryParseExact(timeString, format,CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                if (timeString == "0") { MainMenu.ShowMenu(); }
                else
                {
                    Console.WriteLine("Invalid format. please enter in hh:mm format");
                    timeString = Console.ReadLine();
                }
            }
            return timeString.ToString();
        }
    }
}
