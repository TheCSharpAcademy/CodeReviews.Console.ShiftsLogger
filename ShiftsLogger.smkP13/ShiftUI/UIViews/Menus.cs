using ShiftUI.UIServices;
using Spectre.Console;
using static ShiftUI.Models.Enums;

namespace ShiftUI.Views;

class Menus
{

    internal void MainMenu()
    {
        bool run = true;
        while (run)
        {
            AnsiConsole.Clear();
            MenuOptions option = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>().Title("[Blue]Welcome to Shift Logger[/]")
                .AddChoices(MenuOptions.SeeShiftReports, MenuOptions.ManageShifts, MenuOptions.SeeUsers, MenuOptions.ManageUsers, MenuOptions.Quit));
            try
            {
                switch (option)
                {
                    case MenuOptions.SeeShiftReports:
                        SeeShiftReportsMenu();
                        break;
                    case MenuOptions.ManageShifts:
                        ManageShifts();
                        break;
                    case MenuOptions.SeeUsers:
                        SeeUsers();
                        break;
                    case MenuOptions.ManageUsers:
                        ManageUsers();
                        break;
                    default:
                        run = false;
                        break;
                }
            }
            catch (Exception ex) { AnsiConsole.WriteLine(ex.InnerException.InnerException.Message); }
            AnsiConsole.MarkupLine("Press any [green]Key[/] to continue");
            Console.ReadLine();
        }
    }

    internal void SeeShiftReportsMenu()
    {
        AnsiConsole.Clear();
        ShiftOptions option = AnsiConsole.Prompt(new SelectionPrompt<ShiftOptions>().Title("What would you like to do?").WrapAround()
            .AddChoices(ShiftOptions.SeeAllShifts, ShiftOptions.SeeShiftsByUserId, ShiftOptions.Return));
        switch (option)
        {
            case ShiftOptions.SeeAllShifts:
                UIShiftServices.SeeAllShifts();
                break;
            case ShiftOptions.SeeShiftsByUserId:
                UIShiftServices.SeeShiftsByUserId();
                break;
            default: break;
        }
    }

    internal void ManageShifts()
    {
        AnsiConsole.Clear();
        ShiftOptions option = AnsiConsole.Prompt(new SelectionPrompt<ShiftOptions>().Title("What would you like to do?").WrapAround()
            .AddChoices(ShiftOptions.CreateNewShift, ShiftOptions.UpdateShift, ShiftOptions.DeleteShift, ShiftOptions.Return));
        switch (option)
        {
            case ShiftOptions.CreateNewShift:
                UIShiftServices.CreateNewShift();
                break;
            case ShiftOptions.UpdateShift:
                UIShiftServices.UpdateShift();
                break;
            case ShiftOptions.DeleteShift:
                UIShiftServices.DeleteShift();
                break;
            default: break;
        }
    }

    internal void SeeUsers()
    {
        AnsiConsole.Clear();
        UserOptions option = AnsiConsole.Prompt(new SelectionPrompt<UserOptions>().Title("What would you like to do?").WrapAround()
            .AddChoices(UserOptions.SeeAllUsers, UserOptions.SeeUserById, UserOptions.Return));
        switch (option)
        {
            case UserOptions.SeeAllUsers:
                UIUserService.SeeAllUsers();
                break;
            case UserOptions.SeeUserById:
                UIUserService.SeeUserById();
                break;
            default: break;
        }
    }

    internal void ManageUsers()
    {
        AnsiConsole.Clear();
        UserOptions option = AnsiConsole.Prompt(new SelectionPrompt<UserOptions>().Title("What would you like to do?").WrapAround()
            .AddChoices(UserOptions.CreateNewUser, UserOptions.UpdateUser, UserOptions.DeleteUser, UserOptions.Return));
        switch (option)
        {
            case UserOptions.CreateNewUser:
                UIUserService.CreateNewUser();
                break;
            case UserOptions.UpdateUser:
                UIUserService.UpdateUser();
                break;
            case UserOptions.DeleteUser:
                UIUserService.DeleteUser();
                break;
            default: break;
        }
    }
}