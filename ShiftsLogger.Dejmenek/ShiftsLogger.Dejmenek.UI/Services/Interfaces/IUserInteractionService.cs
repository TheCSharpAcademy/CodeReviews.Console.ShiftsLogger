using ShiftsLogger.Dejmenek.UI.Enums;
using ShiftsLogger.Dejmenek.UI.Models;

namespace ShiftsLogger.Dejmenek.UI.Services.Interfaces;
public interface IUserInteractionService
{
    string GetFirstName();
    string GetLastName();
    string GetDateTime();
    void GetUserInputToContinue();
    bool GetConfirmation(string title);
    Employee GetEmployee(List<Employee> employees);
    Shift GetShift(List<Shift> shifts);
    MenuOptions GetMenuOption();
    ManageShiftsOptions GetManageShiftsOptions();
    ManageEmployeesOptions GetManageEmployeesOptions();
}
