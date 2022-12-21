using ShiftLoggerConsole.Validation;

namespace ShiftLoggerConsole.UserInput;

public class Input : IInput
{
    private readonly IInputValidator _validator;

    public Input(IInputValidator validator)
    {
        _validator = validator;
    }
    
    public int GetId()
    {
        int id = -1;
        Console.Write("Enter id: ");
        var temp = Console.ReadLine()!.Trim();
        while (!_validator.IsValidId(temp, ref id))
        {
            Console.WriteLine("Error!");
            Console.Write("Enter id: ");
            temp = Console.ReadLine()!.Trim();
        }

        return id;
    }
    
    public string GetName()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine()!.Trim();
        while (!_validator.IsValidName(name))
        {
            Console.WriteLine("Error!");
            Console.Write("Enter name: ");
            name = Console.ReadLine()!.Trim();
        }

        return name;
    }

    public string GetInput()
    {
        return Console.ReadLine()!.Trim();
    }
}