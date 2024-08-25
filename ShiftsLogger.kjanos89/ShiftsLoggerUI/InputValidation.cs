namespace ShiftsLoggerUI;

public class InputValidation
{
    public int NumberValidation(string str)
    {
        int num;
        while (String.IsNullOrEmpty(str) || !int.TryParse(str, out num))
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

    public DateTime DateValidation(string input)
    {
        DateTime date;
        if(DateTime.TryParse(input, out date))
        {
            return date;
        }
        else
        {
            Console.WriteLine("Invalid input, try again.");
            input = Console.ReadLine();
            return DateValidation(input);
        }
    }
    
    public bool IsEndLater(DateTime start, DateTime end)
    {
        return start<end;
    }

}
