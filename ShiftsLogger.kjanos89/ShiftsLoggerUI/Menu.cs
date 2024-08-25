using Microsoft.IdentityModel.Tokens;
using ShiftsLoggerAPI;

namespace ShiftsLoggerUI;

public class Menu
{
    InputValidation validation;
    public void ShowMenu()
    {
        validation = new InputValidation();
        Console.Clear();
        Console.WriteLine("1 - Display all shifts");
        Console.WriteLine("2 - Add a new shift record");
        Console.WriteLine("3 - Update shift record by id");
        Console.WriteLine("4 - Delete shift record by id");
        Console.WriteLine("0 - Exit the application\n");
        Console.WriteLine("Choose the number of the option you want to do and press Enter");
        string choice = Console.ReadLine();
        if (choice != null)
        {
            MenuChoice(choice[0]);
        }
        else
        {
            Console.WriteLine("Try again!");
            Task.Delay(1000).Wait();
            ShowMenu();
            return;
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
                Task.Run(() => ShowAll()).GetAwaiter().GetResult();
                break;
            case '2':
                Task.Run(()=>AddShift()).GetAwaiter().GetResult();
                break;
            case '3':
                Task.Run(()=>UpdateShift()).GetAwaiter().GetResult();
                break;
            case '4':
                Task.Run(()=>DeleteShift()).GetAwaiter().GetResult();
                break;
            default:
                Console.WriteLine("Wrong input, try again!");
                Task.Delay(1000).Wait();
                ShowMenu();
                break;
        }
    }

    private async Task DeleteShift()
    {
        await ShowAllData();
        Console.WriteLine("Please enter the id of the shift you want to delete:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        try
        {
            await Service.DeleteShift(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during deletion: {ex.Message}");
        }
        Console.ReadLine(); 
        ShowMenu();
        return;
    }


    private async Task UpdateShift()
    {
        await ShowAllData();
        List<Shift> shifts = await Service.LoadShifts();
        List<int> ids = new List<int>();
        foreach (Shift s in shifts)
        {
            ids.Add(s.Id);
        }
        Console.WriteLine("Please enter the id of the shift you want to update or press '0' to return to the main menu:");
        string temp=Console.ReadLine();
        int id = validation.NumberValidation(temp);
        if(id==0)
        {
            ShowMenu();
            return;
        }
        if (!ids.Contains(id))
        {
            Console.WriteLine("Id does not exist! Try again!");
        }
        while (true)
        {
            Console.WriteLine("Enter the start of the shift in the correct format or press '0' to return to the main menu:");
            Console.WriteLine("Example: 2000.01.01. 01:01");
            temp = Console.ReadLine();
            if(temp == "0")
            {
                ShowMenu();
                return;
            }
            DateTime start;
            try
            {
                start = validation.DateValidation(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            Console.WriteLine("Enter the end of the shift in the correct format or press '0' to return to the main menu:");
            Console.WriteLine("Example: 2000.01.01. 09:01 - the end date must be a later date than the start date!");
            temp = Console.ReadLine();
            if (temp=="0")
            {
                ShowMenu();
                return;
            }
            DateTime end;
            try
            {
                end = validation.DateValidation(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            if (validation.IsEndLater(start, end))
            {
                Shift shift = new()
                {
                    Start = start,
                    End = end,
                };

                await Service.UpdateShift(id, shift);
                Task.Delay(1000).Wait();
                ShowMenu();
                return;
            }
            else
            {
                Console.WriteLine("The end date must be a later date than the start. Try again!");
                Task.Delay(1600).Wait();
                UpdateShift();
                return;
            }
        }
    }

    public async Task AddShift()
    {
        while (true)
        {
            Console.WriteLine("Enter the start of the shift in the correct format:");
            Console.WriteLine("Example: 2000.01.01. 01:01");
            string temp = Console.ReadLine();
            DateTime start;
            try
            {
                start = validation.DateValidation(temp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            Console.WriteLine("Enter the end of the shift in the correct format:");
            Console.WriteLine("Example: 2000.01.01. 09:01 - the end date must be a later date than the start date!" );
            temp = Console.ReadLine();
            DateTime end;
            try
            {
                end = validation.DateValidation(temp); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue; 
            }
            if (validation.IsEndLater(start, end)) 
            {
                Shift shift = new()
                {
                    Start = start,
                    End = end,
                };

                await Service.AddShift(shift); 
                await Task.Delay(1000); 
                ShowMenu();
                return;
            }
            else
            {
                Console.WriteLine("The end of the shift must be later than the start of the shift. Try again!");
            }
            ShowMenu();
            return;
        }
    }

    public async Task ShowAllData()
    {
        try
        {
            List<Shift> shifts = await Service.LoadShifts();
            if (shifts.IsNullOrEmpty())
            {
                Console.WriteLine("We could not find any shifts. Start adding them with the \"1 - Add a new shift\" option\n");
                Task.Delay(1000);
                ShowMenu();
                return;
            }
            string output = "Your shifts:\n";
            foreach (Shift shift in shifts)
            {
                output += $"Shift [{shift.Id}] {shift.Duration} ({shift.Start} - {shift.End})\n";
            }
            Console.WriteLine(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public async Task ShowAll()
    {
        try
        {
            List<Shift> shifts = await Service.LoadShifts();
            if (shifts.IsNullOrEmpty())
            {
                Console.WriteLine("We could not find any shifts. Start adding them with the \"1 - Add a new shift\" option\n");
                Task.Delay(1000);
                ShowMenu();
                return;
            }
            string output = "Your shifts:\n";
            foreach (Shift shift in shifts)
            {
                output += $"Shift [{shift.Id}] {shift.Duration} ({shift.Start} - {shift.End})\n";
            }
            Console.WriteLine(output);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadLine();
        ShowMenu();
        return;
    }

}
