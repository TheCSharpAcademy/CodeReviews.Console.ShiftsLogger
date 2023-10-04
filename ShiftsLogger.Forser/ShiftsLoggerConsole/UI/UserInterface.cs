using ShiftsLoggerConsole.UI;
using System.Globalization;

internal class UserInterface
{
    private ShiftLoggerController _controller;

    public UserInterface(ShiftLoggerController controller)
    {
        _controller = controller;
    }
    public async Task Run()
    {
        while (true)
        {
            AnsiConsole.Clear();
            Helpers.RenderTitle("Main Menu");

            int selectedOption = AnsiConsole.Prompt(DrawMenu()).Id;

            switch (selectedOption)
            {
                case 0:
                    await DisplayAllShiftsAsync();
                    Console.ReadLine();
                    break;
                case 1:
                    RegisterNewShift();
                    Console.ReadLine();
                    break;
                case 2:
                    await EditShift();
                    Console.ReadLine();
                    break;
                case 3:
                    await DeleteShift();
                    Console.ReadLine();
                    break;
                case -1:
                    Helpers.RenderMessage("Goodbye!");
                    return;
                default:
                    break;
            }
        }
    }
    private async Task EditShift()
    {
        AnsiConsole.Clear();
        Helpers.RenderTitle("Select which Shift to Edit");
        List<Shift> shifts = (await _controller.GetShifts()).ToList();

        Table table = new();
        table.AddColumns("Id", "Employee Name", "Start of Shift", "End of Shift");

        foreach (Shift shift in shifts)
        {
            table.AddRow(
                $"{shift.Id}",
                $"{shift.EmployeeName}",
                $"{shift.StartOfShift}",
                $"{shift.EndOfShift}");
        }

        AnsiConsole.Write(table);
        int selectedShift = AnsiConsole.Ask<int>("Enter the ID of the Shift you want to edit:");

        if (selectedShift != 0)
        {
            AnsiConsole.Clear();
            Helpers.RenderTitle($"Editing Shift with ID: {selectedShift}");
            string employeeName;
            string startOfDate;
            string endOfDate;
            string dateFormat = "dd-MM-yy HH:mm";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            var shift = await _controller.GetShift(selectedShift);

            if (shift != null)
            {
                employeeName = AnsiConsole.Ask("Update the Employee Name:", shift.EmployeeName);

                do
                {
                    startOfDate = AnsiConsole.Ask("Update the Start of Shift:", shift.StartOfShift.ToString(dateFormat, cultureInfo));
                    if (!Validator.ValidateDateTime(startOfDate))
                    {
                        AnsiConsole.WriteLine("Invalid date, make sure the format is (dd-MM-yy HH:mm)");
                    }
                } while (!Validator.ValidateDateTime(startOfDate));
                do
                {
                    do
                    {
                        endOfDate = AnsiConsole.Ask("Update the End of Shift:", shift.EndOfShift.ToString(dateFormat, cultureInfo));
                        if (!Validator.ValidateDateTime(endOfDate))
                        {
                            AnsiConsole.WriteLine("Invalid date, make sure the format is (dd-MM-yy HH:mm)");
                        }
                    } while (!Validator.ValidateDateTime(endOfDate));
                } while (!Validator.AreDatesValid(DateTime.Parse(startOfDate), DateTime.Parse(endOfDate)));



                if (employeeName != shift.EmployeeName || startOfDate != shift.StartOfShift.ToString(dateFormat, cultureInfo) || endOfDate != shift.EndOfShift.ToString("d", cultureInfo))
                {
                    ShiftDto newShift = new()
                    {  
                        Id = shift.Id,
                        EmployeeName = employeeName,
                        StartOfShift = DateTime.Parse(startOfDate),
                        EndOfShift = DateTime.Parse(endOfDate)
                    };
                    
                    bool result = await _controller.UpdateShift(newShift);

                    if (result)
                    {
                        AnsiConsole.WriteLine("Your shift has been updated");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Your shift hasn't been updated");
                    }

                    AnsiConsole.WriteLine("Press any key to return to Main Menu");
                }
            }
        }

    }
    private async Task DeleteShift()
    {
        AnsiConsole.Clear();
        Helpers.RenderTitle("Select which Shift to Remove");
        List<Shift> shifts = (await _controller.GetShifts()).ToList();

        Table table = new();
        table.AddColumns("Id", "Employee Name", "Start of Shift", "End of Shift");

        foreach (Shift shift in shifts)
        {
            table.AddRow(
                $"{shift.Id}", 
                $"{shift.EmployeeName}",
                $"{shift.StartOfShift}",
                $"{shift.EndOfShift}");
        }
        AnsiConsole.Write(table);

        int selectedShift = AnsiConsole.Ask<int>("Enter the ID of the Shift you want to remove:");

        var result = await _controller.DeleteShift(selectedShift);

        if (result)
        {
            AnsiConsole.WriteLine("Your selected Shift was deleted");
        }
        else
        {
            AnsiConsole.WriteLine("Couldn't delete selected Shift");
        }
        AnsiConsole.WriteLine("Press any key to return to Main Menu");
    }
    private async void RegisterNewShift()
    {
        AnsiConsole.Clear();
        Helpers.RenderTitle("Register a new shift");

        string startOfShift, endOfShift;

        string employeeName = AnsiConsole.Ask<string>("Enter the employee name:");
        do
        {
            startOfShift = AnsiConsole.Ask<string>("Enter the start of shift (dd-MM-yy HH:mm):");
            if (!Validator.ValidateDateTime(startOfShift))
            {
                AnsiConsole.WriteLine("Invalid date, make sure the format is (dd-MM-yy HH:mm)");
            }
        } while (!Validator.ValidateDateTime(startOfShift));
        do
        {
            endOfShift = AnsiConsole.Ask<string>("Enter the end of shift (dd-MM-yy HH:mm):");
            if (!Validator.ValidateDateTime(endOfShift))
            {
                AnsiConsole.WriteLine("Invalid date, make sure the format is (dd-MM-yy HH:mm)");
            }
        } while (!Validator.ValidateDateTime(endOfShift));

        ShiftDto newShift = new()
        {
            Id = 0,
            EmployeeName = employeeName,
            StartOfShift = DateTime.Parse(startOfShift),
            EndOfShift = DateTime.Parse(endOfShift)
        };

        var result = await _controller.PostShift(newShift);

        if (result)
        {
            AnsiConsole.WriteLine("Your shift has been registered.");
        }
        else
        {
            AnsiConsole.WriteLine("Your shift didn't register, try again!");
        }
        Console.WriteLine("Press any key to continue.");
    }
    private async Task DisplayAllShiftsAsync()
    {
        AnsiConsole.Clear();
        Helpers.RenderTitle("List of Shifts");
        List<Shift> shifts = (await _controller.GetShifts()).ToList();

        Table table = new Table();
        table.Expand();
        table.AddColumns("Employee Name", "Start of Shift", "End of Shift", "Duration");

        foreach (Shift shift in shifts)
        {
            table.AddRow($"{shift.EmployeeName}", $"{shift.StartOfShift}", $"{shift.EndOfShift}", $"{shift.Duration}");
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine("Press any key to return to main menu");
    }
    private SelectionPrompt<Menu> DrawMenu()
    {
        SelectionPrompt<Menu> selectionPrompt = new()
        {
            HighlightStyle = Helpers.HighLightStyle
        };

        List<Menu> drawMenu = new List<Menu>();
        drawMenu.Add(new Menu { Id = 0, Text = "List all registered shifts" });
        drawMenu.Add(new Menu { Id = 1, Text = "Register a new shift" });
        drawMenu.Add(new Menu { Id = 2, Text = "Edit a shift" });
        drawMenu.Add(new Menu { Id = 3, Text = "Delete a shift" });
        drawMenu.Add(new Menu { Id = -1, Text = "Exit" });

        selectionPrompt.Title("Select an [B]option[/]");
        selectionPrompt.AddChoices(drawMenu);

        return selectionPrompt;
    }
}