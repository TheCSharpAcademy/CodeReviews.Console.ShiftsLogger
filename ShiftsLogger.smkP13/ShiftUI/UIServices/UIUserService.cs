using ShiftUI.UIControllers;
using ShiftUI.UIModels;
using ShiftUI.UIViews;
using Spectre.Console;

namespace ShiftUI.UIServices;

internal interface IUIUserService
{
    internal static void SeeAllUsers() { }
    internal static void SeeUserById() { }
    internal static void CreateNewUser() { }
    internal static void DeleteUser() { }
    internal static void UpdateUser() { }
}
class UIUserService : IUIUserService
{
    internal static void SeeAllUsers()
    {
        List<UIUser>? users = UIUserController.GetAllUsers();
        UIOutput.PrintAllUsers(users);

    }

    internal static void SeeUserById()
    {
        UIUser? user = UserInputs.GetUser();
        user.Shifts = UIShiftController.GetShiftsByUserId(user.userId);
        UIOutput.PrintSingleUser(user);
    }

    internal static void CreateNewUser()
    {
        UIUser user = new();
        user.userId = 0;
        user.firstName = UserInputs.GetName("Enter users First Name: ");
        user.lastName = UserInputs.GetName("Enter users Last Name: ");
        UIUserController.CreateNewUser(user);
    }

    internal static void DeleteUser()
    {
        UIUser? user = UserInputs.GetUser();
        if (user != null) if (UserInputs.Validation("Are you sure you want to delete this user?")) UIUserController.DeleteUser(user.userId);
    }

    internal static void UpdateUser()
    {
        UIUser? user = UserInputs.GetUser();
        if (user != null)
        {
            string option = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("What do you want to update").AddChoices("First Name", "Last Name", "Is Active", "All", "Cancel"));
            switch (option)
            {
                case "First Name":
                    user.firstName = UserInputs.GetName("Enter a new First Name: ");
                    break;
                case "Last Name":
                    user.lastName = UserInputs.GetName("Enter a new Last Name: ");
                    break;
                case "IsActive":
                    user.isActive = UserInputs.Validation("Is current user active?");
                    break;
                default:
                    return;
            }
            UIUserController.UpdateUser(user);
        }
    }
}
