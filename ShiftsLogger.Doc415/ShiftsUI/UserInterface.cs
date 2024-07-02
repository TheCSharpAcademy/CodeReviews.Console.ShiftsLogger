using Spectre.Console;
using static ShiftsUI.Enums;
namespace ShiftsUI;

internal class UserInterface
{
    private Dataaccess _dataAccess;

    public UserInterface()
    {
        _dataAccess = new Dataaccess();
    }

    public async Task MainMenu()
    {
        while (true)
        {
            AnsiConsole.Write(new FigletText("Shifts Logger").Color(Color.LightSalmon3_1).Centered());
            var selection = AnsiConsole.Prompt(new SelectionPrompt<MainMenuSelections>()
                                                    .Title("Main Menu")
                                                    .AddChoices(MainMenuSelections.AddShift,
                                                                MainMenuSelections.ViewShifts,
                                                                MainMenuSelections.ViewShiftById,
                                                                MainMenuSelections.UpdateShift,
                                                                MainMenuSelections.DeleteShift,
                                                                MainMenuSelections.Quit
                                                   ));
            switch (selection)
            {
                case MainMenuSelections.AddShift:
                    await Task.Run(() => AddShift());
                    break;
                case MainMenuSelections.ViewShifts:
                    await Task.Run(() => ViewShifts(true));
                    break;
                case MainMenuSelections.ViewShiftById:
                    await Task.Run(() => ViewShiftById());
                    break;
                case MainMenuSelections.DeleteShift:
                    await Task.Run(() => DeleteShift());
                    break;
                case MainMenuSelections.UpdateShift:
                    await Task.Run(() => UpdateShift());
                    break;
                case MainMenuSelections.Quit:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private async Task AddShift()
    {
        DateTime start = DateTime.MinValue;
        DateTime end = DateTime.MaxValue;
        string duration = "";
        bool valid = false;
        do
        {
            start = await GetDate("start");
            end = await GetDate("end");
            var difference = (end - start).TotalHours;
            duration = String.Format("{0:0.00}", difference);
            if (difference > 0) valid = true;
            else
                Console.WriteLine("Working hours can't be negative. Check your date-times.");
        } while (!valid);
        string startTime = start.ToString();
        string endTime = end.ToString();
        string name = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select shift type")
                                                                      .AddChoices("Morning Shift",
                                                                                  "Night Shift"));

        try
        {
            await _dataAccess.AddShift(name, startTime, endTime, duration);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error: {ex.Message}");
            EndOperation().Wait();
        }
    }

    private async Task ViewShifts(bool clear)
    {
        try
        {
            var table = new Table();
            table.AddColumns("Id", "Name", "Start Time", "End Time", "Duration (hours)");
            var shifts = await _dataAccess.GetShiftsList();
            foreach (var shift in shifts)
            {
                table.AddRow(shift.Id.ToString(), shift.Name, shift.StartTime, shift.EndTime, shift.Duration);
            }
            AnsiConsole.Write(table);
            if (clear) EndOperation().Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There is no avaible data: {ex.Message}");
            EndOperation().Wait();
            return;
        }
    }

    private async Task DeleteShift()
    {
        ViewShifts(false).Wait();
        int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter record id to delete: ").
                                                      ValidationErrorMessage("Enter a valid number.").
                                                      Validate(_id =>
                                                      {
                                                          return _id switch
                                                          {
                                                              < 0 => ValidationResult.Error("Id can't be negative number"),
                                                              _ => ValidationResult.Success()
                                                          };
                                                      }));
        if (!AnsiConsole.Confirm("This will delete record permanently. Are you sure? ", false)) return;
        try
        {
            await _dataAccess.DeleteShift(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error: {ex.Message}");
            EndOperation().Wait();
        }
    }

    private async Task UpdateShift()
    {
        ViewShifts(false).Wait();
        int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter record id to update: ").
                                                      ValidationErrorMessage("Enter a valid number.").
                                                      Validate(_id =>
                                                      {
                                                          return _id switch
                                                          {
                                                              < 0 => ValidationResult.Error("Id can't be negative number"),
                                                              _ => ValidationResult.Success()
                                                          };
                                                      }));

        try
        {
            string name = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select shift type")
                                                                      .AddChoices("Morning Shift",
                                                                                  "Night Shift"));

            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;
            string duration = "";
            bool valid = false;
            do
            {
                start = await GetDate("start");
                end = await GetDate("end");
                var difference = (end - start).TotalHours;
                duration = String.Format("{0:0.00}", difference);
                if (difference > 0) valid = true;
                else
                    Console.WriteLine("Working hours can't be negative. Check your date-times.");
            } while (!valid);


            string startTime = start.ToString();
            string endTime = end.ToString();

            await _dataAccess.UpdateShift(id, name, startTime, endTime, duration);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error: {ex.Message}");
            EndOperation().Wait();
            return;
        }
    }
    private async Task ViewShiftById()
    {
        try
        {
            int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter record id: ").
                                                      ValidationErrorMessage("Enter a valid number.").
                                                      Validate(_id =>
                                                      {
                                                          return _id switch
                                                          {
                                                              < 0 => ValidationResult.Error("Id can't be negative number"),
                                                              _ => ValidationResult.Success()
                                                          };
                                                      }));
            var shift = await _dataAccess.GetShiftById(id);
            var table = new Table();
            table.AddColumns("Id", "Name", "Start Time", "End Time", "Duration (hours)");
            table.AddRow(shift.Id.ToString(), shift.Name, shift.StartTime, shift.EndTime, shift.Duration);
            AnsiConsole.Write(table);
            EndOperation().Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There is no avaible data: {ex.Message}");
            EndOperation().Wait();
            return;
        }
    }

    private async Task<DateTime> GetDate(string input)
    {
        DateTime result = DateTime.Now;
        string dump;
        bool valid = false;
        do
        {
            string startTime = AnsiConsole.Prompt(new TextPrompt<string>($"Enter {input} time of shift (dd-mm-yyyy hh:mm): "));
            try
            {
                string[] dateTimeComponents = startTime.Split(" ");
                string[] dateComponents = dateTimeComponents[0].Split("-");
                string[] timeComponents = dateTimeComponents[1].Split(":");
                valid = int.TryParse(dateComponents[0], out int day);
                if (!valid) continue;
                valid = int.TryParse(dateComponents[1], out int month);
                if (!valid) continue;
                valid = int.TryParse(dateComponents[2], out int year);
                if (!valid) continue;
                valid = int.TryParse(timeComponents[0], out int hour);
                if (!valid) continue;
                valid = int.TryParse(timeComponents[1], out int minute);
                if (!valid) continue;
                result = new DateTime(year, month, day, hour, minute, 0);
                if (DateTime.TryParse(result.ToString(), out result))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date-time.");
                    valid = false;
                }
            }
            catch
            {
                valid = false;
                Console.WriteLine("Please enter a valid date-time.");
            }

        } while (!valid);
        return await Task.FromResult(result);
    }

    private async Task EndOperation()
    {
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }
}
