using ShiftUI.UIControllers;
using ShiftUI.UIModels;
using ShiftUI.UIViews;
using Spectre.Console;

namespace ShiftUI.UIServices;

internal interface IUIShiftServices
{
    public static void SeeAllShifts() { }
    public static void SeeShiftsByUserId() { }
    public static void CreateNewShift() { }
    public static void DeleteShift() { }
    public static void UpdateShift() { }
}

internal class UIShiftServices : IUIShiftServices
{
    internal static void CreateNewShift()
    {
        UIShift shift = new();
        UIUser? user = UserInputs.GetUser();
        if (user != null)
        {
            bool validTime = false;
            while (!validTime)
            {
                AnsiConsole.WriteLine("Shift Start:");
                shift.startTime = UserInputs.GetTime();
                AnsiConsole.WriteLine("Shift End:");
                shift.endTime = UserInputs.GetTime();
                if (shift.startTime < shift.endTime)
                {
                    validTime = true;
                    shift.userId = user.userId;
                }
                else { AnsiConsole.WriteLine("Start time must be before end time."); Console.ReadLine(); AnsiConsole.Clear(); }
            }
        }
        UIShiftController.CreateNewShift(shift);
    }

    internal static void DeleteShift()
    {
        UIShift shiftToDelete = UserInputs.GetShift();
        if (shiftToDelete.id == 0) return;
        UIShiftController.DeleteShift(shiftToDelete.id);
    }

    internal static void SeeAllShifts()
    {

        List<UIShift>? shifts = UIShiftController.GetAllShifts();
        List<UIUser>? users = UIUserController.GetAllUsers();
        foreach (UIShift shift in shifts)
        {
            shift.user = users.Single(x => x.userId == shift.userId);
        }
        UIOutput.PrintAllShifts(shifts);
    }

    internal static void SeeShiftsByUserId()
    {
        UIUser? user = UserInputs.GetUser();
        if (user != null)
        {
            user.Shifts = UIShiftController.GetShiftsByUserId(user.userId);
            UIOutput.PrintShiftForSingleUser(user);
        }
    }

    internal static void UpdateShift()
    {
        UIShift shift = UserInputs.GetShift();
        if (shift.id != 0)
        {
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("What do you want to update.").AddChoices("Start Time", "End Time", "User", "All", "Cancel"));
            bool valid = false;
            switch (option)
            {
                case "Start Time":
                    while (!valid)
                    {
                        shift.startTime = UserInputs.GetTime();
                        valid = shift.startTime < shift.endTime;
                        if (!valid) AnsiConsole.WriteLine($"Start Time must be before End Time({shift.endTime})");
                    }
                    break;
                case "end Time":
                    while (!valid)
                    {
                        shift.endTime = UserInputs.GetTime();
                        valid = shift.startTime < shift.endTime;
                        if (!valid) AnsiConsole.WriteLine($"End Time must be after Start Time({shift.startTime})");
                    }
                    break;
                case "User":
                    shift.userId = UserInputs.GetUser().userId;
                    if (shift.user == null) return;
                    break;
                case "All":
                    while (!valid)
                    {
                        shift.startTime = UserInputs.GetTime();
                        valid = shift.startTime < shift.endTime;
                        if (!valid) AnsiConsole.WriteLine($"Start Time must be before End Time({shift.endTime})");
                    }
                    valid = false;
                    while (!valid)
                    {
                        shift.endTime = UserInputs.GetTime();
                        valid = shift.startTime < shift.endTime;
                        if (!valid) AnsiConsole.WriteLine($"End Time must be after Start Time({shift.startTime})");
                    }
                    shift.userId = UserInputs.GetUser().userId;
                    if (shift.user == null) return;
                    break;
                default:
                    return;
            }
            UIShiftController.UpdateShift(shift);
        }
    }
}
