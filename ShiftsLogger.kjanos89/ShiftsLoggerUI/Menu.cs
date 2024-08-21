namespace ShiftsLoggerUI;

public class Menu
{
    InputValidation validation;
    public void ShowMenu()
    {
        validation = new InputValidation();
        Console.Clear();
        Console.WriteLine("\n1 - Display all shifts");
        Console.WriteLine("2 - Search a shift by id");
        Console.WriteLine("3 - Add a new shift record");
        Console.WriteLine("4 - Update shift record by id");
        Console.WriteLine("5 - Delete shift record by id");
        Console.WriteLine("0 - Exit the application\n");
        Console.WriteLine("Choose the number of the option you want to do and press Enter");
        string choice = Console.ReadLine();
        if (!Char.IsNumber(choice[0]))
        {
            MenuChoice(choice[0]);
        }
    }

    public void MenuChoice(char choice)
    {
        switch (choice)
        {
            case '0':
                Environment.Exit(0);
                break;

            case '1':
                ShowAllData();
                break;

            case '2':
                ShowDataById();
                break;

            case '3':
                AddShift();
                break;
            case '4':
                UpdateShift();
                break;
            case '5':
                DeleteShift();
                break;
            default:
                Console.WriteLine("Wrong input, try again!");
                ShowMenu();
                break;
        }
    }

    private void DeleteShift()
    {
        throw new NotImplementedException();
    }

    private void UpdateShift()
    {
        throw new NotImplementedException();
    }

    private void AddShift()
    {
        throw new NotImplementedException();
    }

    private void ShowDataById()
    {
        throw new NotImplementedException();
    }

    private void ShowAllData()
    {
        throw new NotImplementedException();
    }
}
