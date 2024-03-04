using ShiftApi.Models;
using ShiftUI.Controllers;

namespace ShiftUI;

internal class Helper
{
    internal static TimeSpan GetTimeDifference(TimeSpan startTime, TimeSpan endTime)
    {
        var timeDifference = endTime - startTime;

        if (timeDifference < TimeSpan.Zero)
        {
            timeDifference = timeDifference.Add(TimeSpan.FromDays(1));
        }

        return timeDifference;
    }

    internal static async Task<Employee> GetEmployeeToUpdate()
    {
        var employeesToUpdate = await ApiService.GetEmployees();
        var employeeIdToUpdate = UserInput.GetEmployeeChoiceById(employeesToUpdate);
        var employeeToUpdate = await ApiService.GetEmployee(employeeIdToUpdate);

        return employeeToUpdate;
    }

    internal static async Task<int> GetIdOfEmployeeToDelete()
    {
        var employeeList = await ApiService.GetEmployees();
        var employeeIdToBeDeleted = UserInput.GetEmployeeChoiceById(employeeList);
        return employeeIdToBeDeleted;
    }
}
