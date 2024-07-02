using Spectre.Console;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using static ShiftsLoggerUI.Enums;

namespace ShiftsLoggerUI;

internal class UserInterface
{
    public async static void ShowAccountMenu()
    {
        bool loginSuccessful = false;

        while (!loginSuccessful)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel("[cyan2]Welcome to your ShiftLogger![/]").BorderColor(Color.DarkOrange3_1));
            var userInput = AnsiConsole.Prompt(new SelectionPrompt<AccountMenuOptions>()
                .AddChoices(Enum.GetValues(typeof(AccountMenuOptions)).Cast<AccountMenuOptions>()));

            switch (userInput)
            {
                case AccountMenuOptions.Login:
                    loginSuccessful = await ShiftsController.Login(GetCredentials("Login", false));
                    break;
                case AccountMenuOptions.Register:
                    ShiftsController.Register(GetCredentials("Register"));
                    break;
                case AccountMenuOptions.Quit:
                    Console.Clear();
                    AnsiConsole.Write(new Panel("[cyan2]Goodbye[/]!").BorderColor(Color.DarkOrange3_1));
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public static void ShowMainMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel("[cyan2]Welcome to your ShiftLogger![/]").BorderColor(Color.DarkOrange3_1));
            var userInput = AnsiConsole.Prompt(new SelectionPrompt<MainMenuOptions>()
                .AddChoices(Enum.GetValues(typeof(MainMenuOptions)).Cast<MainMenuOptions>()));

            switch (userInput)
            {
                case MainMenuOptions.AddShift:
                    ShiftsController.AddShift(GetShiftTimes());
                    break;
                case MainMenuOptions.UpdateShift:
                    UpdateShiftInput();
                    break;
                case MainMenuOptions.DeleteShift:
                    DeleteShiftInput();
                    break;
                case MainMenuOptions.ViewAllShifts:
                    ShowAllShifts();
                    break;
                case MainMenuOptions.Logout:
                    exit = true;
                    break;
            }
        }
    }

    private static void PrintAllShifts(List<Shift> shifts)
    {
        var table = new Table()
                        .BorderColor(Color.DarkOrange3_1)
                        .AddColumns("[cyan2]ID[/]", "[cyan2]Start Time[/]", "[cyan2]End Time[/]", "[cyan2]Duration[/]");

        shifts.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));

        var data = CollectionsMarshal.AsSpan(shifts);

        for (int i = 0; i < data.Length; i++)
        {
            table.AddRow((i + 1).ToString(), data[i].StartTime.ToString("dd. MMM yyyy HH:mm"), data[i].EndTime.ToString("dd. MMM yyyy HH:mm"), string.Format("{0} h {1} m",
                                                                                                                                                    data[i].Duration.Hours
                                                                                                                                                    + data[i].Duration.Days * 24,
                                                                                                                                                    data[i].Duration.Minutes));
        }

        Console.Clear();
        AnsiConsole.Write(table);
    }

    private static void ShowAllShifts()
    {
        PrintAllShifts(ShiftsController.GetShifts().Result);
        Helpers.WriteAndWait("[cyan]Press Any Key To Return[/]");
    }

    private static void DeleteShiftInput()
    {
        var shifts = ShiftsController.GetShifts().Result;
        PrintAllShifts(shifts);

        int id;

        do
        {
            id = AnsiConsole.Ask<int>("[cyan]Please Enter The Id Or 0 To Exit:[/]");

            if (id == 0)
                return;

            if (id > shifts.Count || id < 0)
                AnsiConsole.Write(new Markup("[red]Invalid Input[/]\n"));

        } while (id > shifts.Count || id < 0);

        ShiftsController.DeleteShiftById(shifts[id - 1].Id);
    }

    private static void UpdateShiftInput()
    {
        var shifts = ShiftsController.GetShifts().Result;
        PrintAllShifts(shifts);

        int id;

        do
        {
            id = AnsiConsole.Ask<int>("[cyan]Please Enter The Id Or 0 To Exit:[/]");

            if (id == 0)
                return;

            if (id > shifts.Count || id < 0)
                AnsiConsole.Write(new Markup("[red]Invalid Input[/]\n"));

        } while (id > shifts.Count || id < 0);

        ShiftsController.UpdateShift(shifts[id - 1], GetShiftTimes());
    }

    private static (string, string) GetCredentials(string text, bool checkPassword = true)
    {
        Console.Clear();
        AnsiConsole.Write(new Panel($"[cyan2]{text}[/]")
                                .Padding(5, 0)
                                .BorderColor(Color.DarkOrange3_1));


        var eMail = AnsiConsole.Ask<string>("[cyan2]E-Mail:[/]");

        while (!Validator.EmailIsValid(eMail))
        {
            AnsiConsole.Write(new Panel("[red]Invalid Email-Adress[/]")
                                    .BorderColor(Color.DarkOrange3_1));
            eMail = AnsiConsole.Ask<string>("[cyan2]E-Mail:[/]");
        }

        var password = GetPassword();

        if (checkPassword)
        {
            while (!Validator.PasswordIsValid(password))
            {
                AnsiConsole.Write(new Panel("[red]Your Password Must Be At Least 6 Characters And Contain A Non Alphanumeric Character, A Digit And A Uppercase Letter![/]")
                                        .BorderColor(Color.DarkOrange3_1));
                password = GetPassword();
            }
        }

        return (eMail, password);
    }

    private static string GetPassword()
    {
        AnsiConsole.Write(new Markup("[cyan2]Password: [/]"));
        var sb = new StringBuilder();
        ConsoleKeyInfo key;

        bool enterPressed = false;

        while (!enterPressed)
        {
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Backspace:
                    if (sb.Length > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        Console.CursorLeft -= 1;
                        Console.Write(" ");
                        Console.CursorLeft -= 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (!string.IsNullOrEmpty(sb.ToString().Trim()))
                        enterPressed = true;
                    break;
                default:
                    if (!char.IsControl(key.KeyChar))
                    {
                        sb.Append(key.KeyChar);
                        Console.Write('*');
                    }
                    break;
            }
        }
        Console.Write('\n');
        return sb.ToString().Trim();
    }

    private static DateTime GetDate(string text, string format = "dd-mm-yy hh:mm")
    {
        var sb = new StringBuilder();
        bool enterPressed;
        ConsoleKeyInfo key;
        DateTime date;

        do
        {

            Console.Write($"{text}: ");
            Console.Write(format);
            Console.CursorLeft -= format.Length;

            enterPressed = false;
            sb.Clear();

            while (!enterPressed)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            Console.CursorLeft -= 1;
                            Console.Write(format[sb.Length]);
                            Console.CursorLeft -= 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (sb.Length == format.Length)
                        {
                            enterPressed = true;
                            Console.Write('\n');
                        }
                        break;
                    default:
                        if (sb.Length < format.Length && !char.IsControl(key.KeyChar))
                        {
                            sb.Append(key.KeyChar);
                            AnsiConsole.Write($"{key.KeyChar}");
                        }
                        break;
                }
            }
        } while (!DateTime.TryParseExact(sb.ToString().Trim(), "dd-MM-yy HH:mm", new CultureInfo("de-DE"), DateTimeStyles.None, out date));

        return date;
    }

    private static (DateTime, DateTime) GetShiftTimes()
    {
        var startTime = GetDate("Shift Start Time");
        var endTime = GetDate("Shift End Time");

        while (endTime <= startTime)
        {
            AnsiConsole.Write(new Panel("[red]The Shift End Time Cannot Be Or Be Before The Shift Start Time[/]").BorderColor(Color.DarkOrange3_1));
            startTime = GetDate("Shift Start Time");
            endTime = GetDate("Shift End Time");
        }

        return (startTime, endTime);
    }
}
