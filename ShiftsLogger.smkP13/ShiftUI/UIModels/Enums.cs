namespace ShiftUI.Models;

internal class Enums
{
    internal enum MenuOptions
    {
        SeeShiftReports,
        ManageShifts,
        SeeUsers,
        ManageUsers,
        Quit,
    }

    internal enum ShiftOptions
    {
        SeeAllShifts,
        SeeShiftsByUserId,
        CreateNewShift,
        UpdateShift,
        DeleteShift,
        Return,
    }

    internal enum UserOptions
    {
        SeeAllUsers,
        SeeUserById,
        CreateNewUser,
        UpdateUser,
        DeleteUser,
        Return,
    }
}
