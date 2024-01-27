using ShiftsLoggerClient.UI;
using ShiftsLoggerClient.Validation;

namespace ShiftsLoggerClient.Controllers;

public class InputController
{
    public static int? InputID()
    {
        string idString = "";
        ConsoleKeyInfo pressedKey;
        string? errorMessage = null;
        while(true)
        {
            MainUI.EnterEmployeeID(errorMessage);
            Console.Write(idString);
            pressedKey = Console.ReadKey();
            switch(pressedKey.Key)
            {
                case(ConsoleKey.Enter):
                    if(InputValidation.IntValidation(idString))
                        return Convert.ToInt32(idString);

                    errorMessage = "Please enter a valid ID";
                    break;

                case(ConsoleKey.Backspace):
                {
                    if(idString.Length > 0)
                        idString = idString.Remove(idString.Length-1);
                    break;
                }
            
                case(ConsoleKey.Escape):
                    return null; 

                default:
                {
                    idString += pressedKey.KeyChar.ToString();
                    break;
                }
            }
        }
    }

    public static string? InputName()
    {
        string name = "";
        string? errorMessage = null;
        ConsoleKeyInfo pressedKey;            
        while(true)
        {
            MainUI.EnterEmployeeName(errorMessage);
            Console.Write(name);
            pressedKey = Console.ReadKey();
            switch(pressedKey.Key)
            {
                case(ConsoleKey.Enter):
                    if(InputValidation.NameValidation(name))
                        return name;
                    errorMessage = "Please enter a valid name";
                    break;

                case(ConsoleKey.Backspace):
                {
                    if(name.Length > 0)
                        name = name.Remove(name.Length-1);
                    errorMessage = null;
                    break;
                }
            
                case(ConsoleKey.Escape):
                    return null; 
            
                default:
                {
                    name += pressedKey.KeyChar.ToString();
                    errorMessage = null;
                    break;
                }
            }
        }
    }

    public static bool InputEmployeeAdmin()
    {
        string isEmployeeAdmin;
        MainUI.IsEmployeeAdmin();
        isEmployeeAdmin = Console.ReadLine() ?? "";
        if(isEmployeeAdmin.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            return true;
        return false;
    }
}