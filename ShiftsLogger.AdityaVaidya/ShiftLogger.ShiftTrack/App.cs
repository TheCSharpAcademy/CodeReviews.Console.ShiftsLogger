using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ShiftLogger.ShiftTrack.Views;
using ShiftLogger.ShiftTrack.Models;
using ShiftLogger.ShiftTrack.Services;

namespace ShiftLogger.ShiftTrack;

internal class App
{
    public async Task InitializeClientAsync()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        await GetSelectionAsync(client);
    }

    private async Task GetSelectionAsync(HttpClient client)
    {
        string selection = Display.GetSelection("Welcome to Shift Logger", new[] { "View Workers/Shifts", "Create a Worker/Shift", "Edit Worker/Shift", "Delete Worker/Shift", "Exit Application" });
        while (selection != "Exit Application")
        {
            if (selection == "View Workers/Shifts")
                await DisplayWorkersOrShifts(client);
            else if (selection == "Create a Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do?", new[] { "Create a Worker", "Create a shift", "Go to Main Menu" });
                if (selection == "Create a Worker")
                    await CreateWorker(client);
                else if (selection == "Create a shift")
                    await CreateShift(client);
            }
            else if (selection == "Edit Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do?", new[] { "Edit A Worker", "Edit a shift", "Go to Main Menu" });
                if (selection == "Edit A Worker")
                    await EditWorker(client);
                else if (selection == "Edit a shift")
                    await EditShift(client);
            }
            else if (selection == "Delete Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do?", new[] { "Delete a Worker", "Delete a shift", "Go to Main Menu" });
                if (selection == "Delete a Worker")
                    await DeleteWorker(client);
                else if (selection == "Delete a shift")
                    await DeleteShift(client);
            }
            Console.Clear();
            selection = Display.GetSelection("Welcome to Shift Logger", new[] { "View Workers/Shifts", "Create a Worker/Shift", "Edit Worker/Shift", "Delete Worker/Shift", "Exit Application" });
        }
    }

    private async Task DisplayWorkersOrShifts(HttpClient client)
    {
        string selection = Display.GetSelection("What do you wish to do?", new[] { "View Workers", "View shifts", "Go to Main Menu" });
        if (selection == "View Workers")
            await WorkerService.DisplayWorkersAsync(client);
        else if (selection == "View shifts")
            await ShiftService.DisplayShiftsAsync(client);
    }

    private async Task CreateWorker(HttpClient client)
    {
        Worker worker = Display.GetWorkerDetails();
        await WorkerService.PostWorkerAsync(client, worker);
    }

    private async Task CreateShift(HttpClient client)
    {
        bool workerExist = await WorkerService.DisplayWorkersAsync(client);
        if (workerExist)
        {
            int workerId = Display.GetWorkerId();
            bool workerIdExists = await WorkerService.CheckWorkerExistsAsync(client, workerId);
            if (workerIdExists)
            {
                Shift shift = Display.GetShiftDetails(workerId);
                await ShiftService.PostShiftAsync(client, shift);
            }
            else
            {
                Console.WriteLine("The entered worker does not exist. Please enter valid workerId. Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }

    private async Task EditWorker(HttpClient client)
    {
        bool workerExist = await WorkerService.DisplayWorkersAsync(client);
        if (workerExist)
        {
            int workerId = Display.GetWorkerId();
            bool workerExists = await WorkerService.CheckWorkerExistsAsync(client, workerId);
            if (workerExists)
            {
                Worker worker = Display.GetWorkerDetails();
                worker.WorkerId = workerId;
                await WorkerService.PutWorkersAsync(client, worker);
            }
            else
            {
                Console.WriteLine("The given worker id does not exist. Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }

    private async Task EditShift(HttpClient client)
    {
        bool shiftsExist = await ShiftService.DisplayShiftsAsync(client);
        if (shiftsExist)
        {
            int shiftId = Display.GetShiftIdEdit();
            bool shiftIdExists = await ShiftService.CheckShiftExistsAsync(client, shiftId);
            if (!shiftIdExists)
            {
                Console.WriteLine("The given shiftId does not exist. Please enter a valid shiftId. Press Enter to continue.");
                Console.ReadLine();
            }
            else
            {
                Shift shift = Display.GetEditedShiftDetails();
                int workerId = await WorkerService.GetWorkerIdForShift(client, shiftId);
                if (workerId != -1)
                {
                    shift.ShiftId = shiftId;
                    shift.WorkerId = workerId;
                    await ShiftService.PutShiftAsync(client, shift);
                }
            }
        }
    }

    private async Task DeleteWorker(HttpClient client)
    {
        bool workerExist = await WorkerService.DisplayWorkersAsync(client);
        if (workerExist)
        {
            int workerId = Display.GetWorkerId();
            await WorkerService.DeleteWorkerAsync(client, workerId);
        }
    }

    private async Task DeleteShift(HttpClient client)
    {
        bool shiftsExist = await ShiftService.DisplayShiftsAsync(client);
        if (shiftsExist)
        {
            int shiftId = Display.GetShiftId();
            await ShiftService.DeleteShiftAsync(client, shiftId);
        }
    }
}