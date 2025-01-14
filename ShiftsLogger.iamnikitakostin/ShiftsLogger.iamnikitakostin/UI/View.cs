using Spectre.Console;
using UI.Controllers;
using UI.Interfaces;
using UI.Models;

namespace UI;
internal class View : ConsoleController
{
    private readonly IShiftService _shiftService;
    private readonly IWorkerService _workerService;

    public View(IShiftService shiftService, IWorkerService workerService)
    {
        _shiftService = shiftService;
        _workerService = workerService;
    } 

    public async Task ShowMainMenu() {
        while(true)
        {
            AnsiConsole.Clear();

            var menuOptions = EnumToDisplayNames<Enums.MainMenuOptions>();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MainMenuOptions>()
                .Title("What you want to do next?")
                .AddChoices(menuOptions.Keys)
                .UseConverter(option => menuOptions[option]));

            switch (choice)
            {
                case Enums.MainMenuOptions.Shifts:
                    await ShowShiftsMenu();
                    break;
                case Enums.MainMenuOptions.Workers:
                    await ShowWorkersMenu();
                    break;
                default:
                    return;
            }
        }
    }
    public async Task ShowShiftsMenu() {
        AnsiConsole.Clear();

        var menuOptions = EnumToDisplayNames<Enums.ShiftsMenuOptions>();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.ShiftsMenuOptions>()
            .Title("What you want to do next?")
            .AddChoices(menuOptions.Keys)
            .UseConverter(option => menuOptions[option]));

        AnsiConsole.Clear();

        switch (choice)
        {
            case Enums.ShiftsMenuOptions.CurrentShift:
                await ShowCurrentShiftMenu();
                break;
            case Enums.ShiftsMenuOptions.ViewAllShifts:
                await ViewAllShifts();
                break;
            case Enums.ShiftsMenuOptions.EditShift:
                await EditShift();
                break;
            case Enums.ShiftsMenuOptions.DeleteShift:
                await DeleteShift();
                break;
            default:
                return;
        }
    }
    public async Task ShowCurrentShiftMenu() {
        var menuOptions = EnumToDisplayNames<Enums.CurrentShiftOptions>();

        Shift latestShift = await _shiftService.GetLatestShift();
        if (latestShift == null || latestShift.ShiftDuration != null)
        {
            menuOptions.Remove(Enums.CurrentShiftOptions.End);
            menuOptions.Remove(Enums.CurrentShiftOptions.CurrentShiftInformation);
        }
        else
        {
            menuOptions.Remove(Enums.CurrentShiftOptions.Start);
        }

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.CurrentShiftOptions>()
            .Title("What you want to do next?")
            .AddChoices(menuOptions.Keys)
            .UseConverter(option => menuOptions[option]));

        switch (choice) {
            case Enums.CurrentShiftOptions.CurrentShiftInformation:
                await ShowCurrentShift(latestShift);
                break;
            case Enums.CurrentShiftOptions.Start:
                int workerId = await ShowWorkerSelectorMenu();

                Shift newShift = new Shift();
                newShift.WorkerId = workerId;
                newShift.StartTime = DateTime.Now;

                bool isAdded = await _shiftService.CreateShift(newShift);

                if (isAdded)
                {
                    await ShowCurrentShift(newShift);
                }
                break;
            case Enums.CurrentShiftOptions.End:
                latestShift.EndTime = DateTime.Now;
                latestShift.ShiftDuration = latestShift.EndTime - latestShift.StartTime;

                bool isUpdated = await _shiftService.UpdateShift(latestShift);

                if (isUpdated)
                {
                    SuccessMessage($"Shift #{latestShift.Id}, performed by employee #{latestShift.WorkerId} has been finished with a duration of {latestShift.ShiftDuration}");
                }

                break;
            default:
                return;
        }
    }

    public async Task ShowCurrentShift(Shift shift)
    {
        Table table = new Table();
        table.AddColumns("Id", "Worker", "Department", "Start Time", "Duration");

        Worker? shiftWorker = await _workerService.GetWorkerById(shift.WorkerId);

        TimeSpan currentDuration = DateTime.Now - shift.StartTime;

        table.AddRow(shift.Id.ToString(), shiftWorker.Name, shiftWorker.Department, shift.StartTime.ToString(), currentDuration.ToString());

        AnsiConsole.Console.Write(table);
        AnsiConsole.Console.WriteLine("Press any button to exit...");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    public async Task ViewAllShifts() {
        List<Shift> shifts = await _shiftService.GetAll();
        List<Worker> workers = await _workerService.GetAll();

        Table table = new Table();
        table.AddColumns("Id", "Worker", "Department", "Start Time", "End Time", "Duration");

        foreach (Shift shift in shifts) {
            Worker? shiftWorker = workers.FirstOrDefault(w => w.Id == shift.WorkerId);

            table.AddRow(shift.Id.ToString(), shiftWorker.Name, shiftWorker.Department, shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftDuration.ToString());
        }

        AnsiConsole.Console.Write(table);
        AnsiConsole.Console.WriteLine("Press any button to exit...");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    public async Task EditShift() {
        int shiftId = await ShowShiftSelectorMenu();

        Shift shift = await _shiftService.GetShiftById(shiftId);

        var menuOptions = EnumToDisplayNames<Enums.EditShiftMenu>();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.EditShiftMenu>()
            .Title("What you want to edit?")
            .AddChoices(menuOptions.Keys)
            .UseConverter(option => menuOptions[option]));

        switch(choice)
        {
            case Enums.EditShiftMenu.Worker:
                AnsiConsole.Console.WriteLine("Select a new worker for the shift.");
                
                int workerId = await ShowWorkerSelectorMenu();

                shift.WorkerId = workerId;
                break;
            case Enums.EditShiftMenu.StartTime:
                string newStartTime;
                DateTime parsedNewStartTime;
     
                do
                {
                    newStartTime = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a new start time for the shift, which should earlier than the End of the Shift ({shift.EndTime}) in the format MM/dd/yyyy HH:mm"));
                }
                while (!DateTime.TryParseExact(newStartTime, "MM/dd/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedNewStartTime) && shift.EndTime > parsedNewStartTime);

                shift.StartTime = parsedNewStartTime;
                break;
            case Enums.EditShiftMenu.EndTime:
                string newEndTime;
                DateTime parsedNewEndTime;

                do
                {
                    newEndTime = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a new end time for the shift, which should later than the Start of the Shift ({shift.StartTime}) in the format MM/dd/yyyy HH:mm"));
                }
                while (!DateTime.TryParseExact(newEndTime, "MM/dd/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out parsedNewEndTime) && shift.StartTime < parsedNewEndTime);

                shift.EndTime = parsedNewEndTime;
                break;
            default:
                return;
        }
        
        bool isUpdated = await _shiftService.UpdateShift(shift);
        
        if (isUpdated)
        {
            SuccessMessage("The chosen shift has been updated.");
        }
    }

    public async Task DeleteShift() {
        int shiftId = await ShowShiftSelectorMenu();

        bool isConfirmed = ConfirmDeletion("shift");

        if (isConfirmed)
        {
            bool isDeleted = await _shiftService.DeleteShift(shiftId);
            if (isDeleted)
            {
                SuccessMessage($"The shift with an id of {shiftId} has been deleted.");
            }
        }
    }

    public async Task<int> ShowShiftSelectorMenu()
    {
        Dictionary<int, string> shifts = await _shiftService.GetAllAsDictionary();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
            .Title("Select a shift: ")
            .AddChoices(shifts.Keys)
            .UseConverter(option => shifts[option]));

        return choice;
    }

    public async Task<int> ShowWorkerSelectorMenu()
    {
        Dictionary<int, string> workers = await _workerService.GetAllAsDictionary();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
            .Title("Select a worker: ")
            .AddChoices(workers.Keys)
            .UseConverter(option => workers[option]));

        return choice;
    }

    public async Task ShowWorkersMenu() {
        AnsiConsole.Clear();

        var menuOptions = EnumToDisplayNames<Enums.WorkersMenuOptions>();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.WorkersMenuOptions>()
            .Title("What you want to do next?")
            .AddChoices(menuOptions.Keys)
            .UseConverter(option => menuOptions[option]));

        AnsiConsole.Clear();

        switch (choice)
        {
            case Enums.WorkersMenuOptions.AddWorker:
                await AddWorker();

                break;
            case Enums.WorkersMenuOptions.ViewAllWorkers:
                await ViewAllWorkers();

                break;
            case Enums.WorkersMenuOptions.ViewAllShiftsByWorker:
                await ViewAllShiftsByWorker();

                break;
            case Enums.WorkersMenuOptions.EditWorker:
                await EditWorker();

                break;
            case Enums.WorkersMenuOptions.DeleteWorker:
                await DeleteWorker();

                break;
            default:
                return;
        }
    }

    public async Task AddWorker()
    {
        string name;
        string department;

        do
        {
            name = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a name for the employee: "));
        }
        while (name.Length == 0);

        do
        {
            department = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a department for the employee: "));
        }
        while (department.Length == 0);

        Worker worker = new Worker
        {
            Name = name,
            Department = department
        };

        bool isAdded = await _workerService.CreateWorker(worker);

        if (isAdded) {
            SuccessMessage($"Employee {name} working in the {department} department has been added.");
        }
    }

    public async Task EditWorker()
    {
        int workerId = await ShowWorkerSelectorMenu();

        Worker worker = await _workerService.GetWorkerById(workerId);

        var menuOptions = EnumToDisplayNames<Enums.EditWorkerMenu>();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.EditWorkerMenu>()
            .Title("What you want to edit?")
            .AddChoices(menuOptions.Keys)
            .UseConverter(option => menuOptions[option]));

        AnsiConsole.Clear();

        switch (choice)
        {
            case Enums.EditWorkerMenu.Name:
                string newName;

                do
                {
                    newName = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a new name for the employee: "));
                }
                while (newName.Length == 0);

                worker.Name = newName;

                break;
            case Enums.EditWorkerMenu.Department:
                string newDepartment;

                do
                {
                    newDepartment = AnsiConsole.Prompt(new TextPrompt<string>($"Enter a new department for the employee: "));
                }
                while (newDepartment.Length == 0);

                worker.Department = newDepartment;

                break;
            default:
                return;
        }

        bool isUpdated = await _workerService.UpdateWorker(worker);

        if (isUpdated)
        {
            SuccessMessage("The chosen worker has been updated.");
        }
    }

    public async Task ViewAllWorkers()
    {
        List<Worker> workers = await _workerService.GetAll();

        Table table = new Table();
        table.AddColumns("Id", "Name", "Department");

        foreach (Worker worker in workers)
        {
            table.AddRow(worker.Id.ToString(), worker.Name, worker.Department);
        }

        AnsiConsole.Console.Write(table);
        AnsiConsole.Console.WriteLine("Press any button to exit...");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    public async Task ViewAllShiftsByWorker()
    {
        int workerId = await ShowWorkerSelectorMenu();

        List<Shift> shifts = await _shiftService.GetAllByWorker(workerId);
        
        Table table = new Table();
        table.AddColumns("Id", "Start Time", "End Time", "Duration");

        foreach (Shift shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.StartTime.ToString(), shift.EndTime.ToString(), shift.ShiftDuration.ToString());
        }

        AnsiConsole.Console.WriteLine($"There was a total of {shifts.Count} shifts.\n");
        AnsiConsole.Console.Write(table);
        AnsiConsole.Console.WriteLine("Press any button to exit...");
        AnsiConsole.Console.Input.ReadKey(false);
    }

    public async Task DeleteWorker()
    {
        int workerId = await ShowWorkerSelectorMenu();

        bool isConfirmed = ConfirmDeletion("worker");

        if (isConfirmed)
        {
            bool isDeleted = await _workerService.DeleteWorker(workerId);
            if (isDeleted)
            {
                SuccessMessage($"The worker with an id of {workerId} has been deleted.");
            }
        }
    }

    static Dictionary<TEnum, string> EnumToDisplayNames<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .ToDictionary(
                value => value,
                value => SplitCamelCase(value.ToString())
            );
    }

    internal static string SplitCamelCase(string input)
    {
        return string.Join(" ", System.Text.RegularExpressions.Regex
            .Split(input, @"(?<!^)(?=[A-Z])"));
    }
}
