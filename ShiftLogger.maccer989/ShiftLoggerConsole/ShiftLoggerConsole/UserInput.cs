namespace ShiftLogger
{
    public class UserInput
    {
        public static string GetNumberInput(string message)
        {
            string finalInput = Validator.CheckValidNumber(message);            
            return finalInput;
        }
        public static string GetTime(string message)
        {
            Console.WriteLine(message);
            string timeInput = Console.ReadLine();
            Validator.CheckValidTime(timeInput);
            return timeInput; 
        }
        public static string GetEmployeeName(string message)
        {
            string employeeName = "";
            employeeName = Validator.CheckIsValidName(message);         
            return employeeName;
        }
        public static string GetShiftDate(string message)
        {
            bool isValidDate;
            string inputDate;
            do
            {
                Console.WriteLine(message);
                inputDate = Console.ReadLine(); 
                if (Validator.CheckIsValidDate(inputDate))
                {
                    isValidDate = true;
                }
                else
                {
                    isValidDate = false;
                    Console.WriteLine("Invalid date. Please enter again:");
                }
            } while (isValidDate == false);  
            return inputDate;
        }
    }
}
