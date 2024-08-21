namespace ShiftsLoggerUI;

public class InputValidation
{
    public int NumberValidation()
    {
        string str = Console.ReadLine();
        int num;

        while (!int.TryParse(str, out num))
        {
            Console.WriteLine("Wrong input, please try again!");
            str = Console.ReadLine();
        }

        return num;
    }

    public string StringValidation()
    {
        string str = Console.ReadLine();

        while (string.IsNullOrEmpty(str))
        {
            Console.WriteLine("Wrong input, please try again!");
            str = Console.ReadLine();
        }

        return str;
    }

}
