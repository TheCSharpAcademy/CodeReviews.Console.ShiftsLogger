using System.Net.Sockets;
using ShiftsLoggerClientLibrary.ApiClients;
using ShiftsLoggerModels;

namespace ShiftsLoggerCLI;

public class SpectreController(IShiftApiClient client)
{
    private Queue<string> Notifications { get; } = [];

    public void Run()
    {
        MainMenu();
    }

    private void MainMenuHeader(int employeeId)
    {
        Console.Clear();
        SpectreUi.WelcomeMessage();
        SpectreUi.LoggedInMessage(employeeId);

        while (Notifications.Count > 0)
        {
            SpectreUi.DisplayNotification(Notifications.Dequeue());
        }
    }

    private void MainMenu()
    {
        SpectreUi.WelcomeMessage();
        var employeeId = SpectreUi.EmployeeIdPrompt();

        var selection = "";
        while (selection is not "Exit") {
            MainMenuHeader(employeeId);
            
            selection = SpectreUi.EmpMainMenu();
            
            // Redraw the header to clear any notifications.
            MainMenuHeader(employeeId);

            switch (selection)
            {
                case "Add Shift":
                    AddShift(employeeId);
                    break;
                case "Review Shifts":
                    ReviewShifts(employeeId);
                    break;
                case "Exit":
                    break;
            }
        }
        SpectreUi.GoodbyeMessage();
    }
    
    private void AddShift(int empId)
    {
        var startTime = SpectreUi.GetStartTime();
        var endTime = SpectreUi.GetEndTime(startTime);

        try
        {
            client.AddShift(new Shift
            {
                EmployeeId = empId,
                Start = startTime,
                End = endTime
            });
        }
        catch (Exception e) when (IsConnectionFailure(e))
        {
            Notifications.Enqueue(SpectreUi.Error("Failed to send new shift request to server. Try again later."));
            return;
        }
        
        Notifications.Enqueue(SpectreUi.Success("Added new shift to record."));
    }

    private void ReviewShifts(int empId)
    {
        List<Shift> shifts = [];
        try
        {
            shifts = client.GetShiftsByEmployeeId(empId)
                .Result
                .OrderBy(x => x.Start)
                .ToList();
        }
        catch (Exception e) when (IsConnectionFailure(e))
        {
            Notifications.Enqueue(SpectreUi.Error("Failed to retrieve shifts from server. Try again later."));
            return;
        }

        if (shifts.Count == 0)
        {
            Notifications.Enqueue(SpectreUi.Error("No shifts to review."));
            return;
        }
        
        // User can select a shift to manage, but if there are none then the menu returns null.
        var shift = SpectreUi.DisplayShiftLog(shifts);
        
        ShiftMenu(shift);
    }

    private void ShiftMenu(Shift shift)
    {
        var selection = SpectreUi.ShiftMenu(shift);
        switch (selection)
        {
            case "Edit Start Time": 
                EditStartTime(shift);
                break;
            case "Edit End Time":
                EditEndTime(shift);
                break;
            case "Delete Shift":
                DeleteShift(shift);
                break;
            case "Go Back":
                return;
        }
    }

    private void EditStartTime(Shift shift)
    {
        var newTime = SpectreUi.GetStartTime();
        shift.Start = newTime;

        try
        {
            client.UpdateShift(shift);
        }
        catch (Exception e) when (IsConnectionFailure(e))
        {
            Notifications.Enqueue(SpectreUi.Error("Failed to update shift start time."));
            return;
        }
        
        Notifications.Enqueue(SpectreUi.Success("Updated shift start time."));
    }

    private void EditEndTime(Shift shift)
    {
        var newTime = SpectreUi.GetEndTime(shift.Start);
        shift.End = newTime;

        try
        {
            client.UpdateShift(shift);
        }
        catch (Exception e) when (IsConnectionFailure(e))
        {
            Notifications.Enqueue(SpectreUi.Error("Failed to update shift end time."));
            return;
        }
        
        Notifications.Enqueue(SpectreUi.Success("Updated shift end time."));
    }

    private void DeleteShift(Shift shift)
    {
        if (!SpectreUi.Confirm("Are you sure you want to delete this shift?")) return;
        
        try
        {
            client.DeleteShift(shift.Id);
        }
        catch (Exception e) when (IsConnectionFailure(e))
        {
            Notifications.Enqueue(SpectreUi.Error("Failed to delete shift from server. Try again later."));
        }
        
        Notifications.Enqueue(SpectreUi.Success("Deleted shift from server."));
    }

    private static bool IsConnectionFailure(Exception e)
    {
        return e is HttpRequestException 
               || e is SocketException
               || (e is AggregateException ae 
                   && ae.InnerExceptions
                       .Any(ie => ie is HttpRequestException or SocketException));
    }
}