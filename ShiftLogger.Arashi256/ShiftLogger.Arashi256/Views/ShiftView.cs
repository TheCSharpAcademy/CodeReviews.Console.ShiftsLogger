using ShiftLogger_Frontend.Arashi256.Classes;
using ShiftLogger_Frontend.Arashi256.Models;
using ShiftLogger_Frontend.Arashi256.Services;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_Shared.Arashi256.Models;
using Spectre.Console;

namespace ShiftLogger_Frontend.Arashi256.Views
{
    class ShiftView
    {
        private const int RETURN_MAINVIEW_OPTION_NUM = 6;
        private Table _tblShiftMenu;
        private ShiftService _shiftService;
        private WorkerView _workerView;
        private string[] _menuOptions =
        {
            "Add a new shift",
            "Update an existing shift",
            "Delete an existing shift",
            "List all shifts",
            "List all shifts for a specific worker",
            "Return to main menu"
        };

        public ShiftView(ShiftService ss, WorkerView wv)
        {
            _shiftService = ss;
            _workerView = wv;
            _tblShiftMenu = new Table();
            _tblShiftMenu.AddColumn(new TableColumn("[steelblue]CHOICE[/]").Centered());
            _tblShiftMenu.AddColumn(new TableColumn("[steelblue]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblShiftMenu.AddRow($"[white]{i + 1}[/]", $"[aqua]{_menuOptions[i]}[/]");
            }
            _tblShiftMenu.Alignment(Justify.Center);
        }

        public void DisplayViewMenu()
        {
            int selectedValue = 0;
            do
            {
                AnsiConsole.Write(new Text("\nSHIFTS").Centered());
                AnsiConsole.Write(_tblShiftMenu);
                selectedValue = CommonUI.MenuOption($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessShiftMenu(selectedValue);
            } while (selectedValue != RETURN_MAINVIEW_OPTION_NUM);
        }

        private void ProcessShiftMenu(int option)
        {
            AnsiConsole.Markup($"[lightslategrey]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    // Add new shift.
                    AddNewShiftAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 2:
                    // Update an existing shift.
                    UpdateWorkerShiftAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 3:
                    // Delete existing shift.
                    DeleteExistingWorkerShiftAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 4:
                    // List all shifts.
                    ListWorkerShiftsAsync(true).GetAwaiter().GetResult(); // Blocks until async method completes.
                    CommonUI.Pause("grey53");
                    break;
                case 5:
                    // List all shifts for a specific worker.
                    ListWorkerShiftsForWorkerAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    CommonUI.Pause("grey53");
                    break;
            }
        }

        private async Task<List<WorkerShiftOutputDto>?> ListWorkerShiftsForWorkerAsync()
        {
            int workerId = 0;
            List<WorkerShiftOutputDto>? workershifts = null;
            List<WorkerOutputDto>? workers = await _workerView.ListWorkersAsync();
            if (workers != null && workers.Any())
            {
                // Get the worker selection
                int workerSelection;
                do
                {
                    workerSelection = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which worker do you wish to see shifts for? (1 - {workers.Count})[/]: ", 0, workers.Count);
                } while (workerSelection > workers.Count);
                if (workerSelection == 0) return null;
                workerId = workers[workerSelection - 1].Id;
                var response = await _shiftService.GetWorkerShiftsForWorkerAsync(workerId);
                switch (response.Status)
                {
                    case ResponseStatus.Success:
                        workershifts = response.Data as List<WorkerShiftOutputDto>;
                        if (workershifts != null)
                        {
                            DisplayWorkerShifts(FormatShiftDates(workershifts), true);
                        }
                        break;
                    case ResponseStatus.Failure:
                        AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]There are no workers to display[/]\n");
            }
            // Optional returned List of workershifts if you want to do something else with them
            return workershifts;
        }

        private async Task AddNewShiftAsync()
        {
            WorkerShiftDetails? workerShiftDetails = await GetWorkerShiftDetails(null, null, null);
            if (workerShiftDetails == null)
            {
                AnsiConsole.MarkupLine("\n[yellow]Operation cancelled[/]\n");
                return;
            }
            DisplayWorkerShiftDetails(workerShiftDetails.ShiftStart.ToString(_shiftService.DateTimeApplicationFormat), workerShiftDetails.ShiftEnd.ToString(_shiftService.DateTimeApplicationFormat), workerShiftDetails.DisplayFirstName, workerShiftDetails.DisplayLastName);
            if (AnsiConsole.Confirm($"[yellow]Are you sure you want to add this new shift for this worker?[/]"))
            {
                var newWorkerShift = PackageInputWorkerShift(null, workerShiftDetails.WorkerId, workerShiftDetails.ShiftStart, workerShiftDetails.ShiftEnd);
                var response = await _shiftService.AddWorkerShiftAsync(newWorkerShift);
                switch (response.Status)
                {
                    case ResponseStatus.Success:
                        AnsiConsole.MarkupLine("[lime]Shift successfully added[/]");
                        var addedWorkerShift = response.Data as WorkerShiftOutputDto;
                        if (addedWorkerShift != null)
                            DisplayWorkerShift(addedWorkerShift);
                        break;
                    case ResponseStatus.Failure:
                        AnsiConsole.MarkupLine("\n[red]Shift addition failed[/]");
                        AnsiConsole.MarkupLine($"[red]ERROR: '{response.Message}'[/]\n");
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("\n[yellow]Operation cancelled[/]\n");
            }
        }

        public void DisplayWorkerShift(WorkerShiftOutputDto workershift)
        {
            Table tblWorkerShift = new Table();
            tblWorkerShift.AddColumn(new TableColumn("[white]ID[/]").RightAligned());
            tblWorkerShift.AddColumn(new TableColumn($"[white]{workershift.DisplayId}[/]").LeftAligned());
            if (workershift.Worker != null)
                tblWorkerShift.AddRow($"[cyan]Shift Worker[/]", $"[white]{workershift.Worker.LastName.ToUpper()}, {workershift.Worker.FirstName.ToUpper()}[/]");
            tblWorkerShift.AddRow($"[cyan]Shift Start[/]", $"[white]{workershift.DisplayShiftStart}[/]");
            tblWorkerShift.AddRow($"[cyan]Shift End[/]", $"[white]{workershift.DisplayShiftEnd}[/]");
            tblWorkerShift.AddRow($"[cyan]Shift Duration[/]", $"[white]{workershift.DisplayDuration}[/]");
            AnsiConsole.Write(tblWorkerShift);
        }

        private void DisplayWorkerShiftDetails(string shiftStart, string shiftEnd, string workerFirstName, string workerLastName)
        {
            Table tblWorkerShift = new Table();
            tblWorkerShift.AddColumn(new TableColumn("[white]Shift Details[/]").RightAligned());
            tblWorkerShift.AddColumn(new TableColumn(" ").LeftAligned());
            tblWorkerShift.AddRow($"[cyan]Worker[/]", $"[white]{workerLastName.ToUpper()}, {workerFirstName.ToUpper()}[/]");
            tblWorkerShift.AddRow($"[cyan]Shift Start[/]", $"[white]{shiftStart}[/]");
            tblWorkerShift.AddRow($"[cyan]Shift End[/]", $"[white]{shiftEnd}[/]");
            AnsiConsole.Write(tblWorkerShift);
        }

        private async Task<WorkerShiftDetails?> GetWorkerShiftDetails(int? worker, DateTime? currentShiftStart, DateTime? currentShiftEnd)
        {
            bool isValidShift = false;
            int workerId;
            string firstName, lastName;
            DateTime workerShiftStart, workerShiftEnd;
            List<WorkerOutputDto>? workers = await _workerView.ListWorkersAsync();
            if (workers != null && workers.Any())
            {
                do
                {
                    // Get the worker selection
                    int workerSelection;
                    do
                    {
                        workerSelection = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which worker do you wish to assign a shift? (1 - {workers.Count})[/]: ", 0, workers.Count);
                    } while (workerSelection > workers.Count);
                    if (workerSelection == 0) return null;
                    workerId = workers[workerSelection - 1].Id;
                    firstName = workers[workerSelection - 1].FirstName;
                    lastName = workers[workerSelection - 1].LastName;
                    // Get shift start
                    AnsiConsole.Markup("\n[white]Enter the start of the shift[/]\n");
                    workerShiftStart = CommonUI.GetDateTimeDialog(_shiftService.DateTimeApplicationFormat) ?? DateTime.MinValue;
                    if (workerShiftStart == DateTime.MinValue) return null;
                    // Get shift end
                    AnsiConsole.Markup("\n[white]Enter the end of the shift[/]\n");
                    workerShiftEnd = CommonUI.GetDateTimeDialog(_shiftService.DateTimeApplicationFormat) ?? DateTime.MinValue;
                    if (workerShiftEnd == DateTime.MinValue) return null;
                    // Validate shift dates
                    isValidShift = ValidateShiftDates(workerShiftStart, workerShiftEnd);
                    if (!isValidShift)
                        AnsiConsole.MarkupLine("\n[yellow]The shift end cannot be before shift start. Try again.[/]\n");
                } while (!isValidShift);
                // Create the worker shift details
                return new WorkerShiftDetails { WorkerId = workerId, ShiftStart = workerShiftStart, ShiftEnd = workerShiftEnd, DisplayFirstName = firstName, DisplayLastName = lastName };
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]There are no workers to display[/]\n");
                return null;
            }
        }

        private bool ValidateShiftDates(DateTime? shiftStart, DateTime? shiftEnd)
        {
            if (shiftStart == null || shiftEnd == null) { return false; }
            // Check if the end time is not before the start time
            return shiftEnd >= shiftStart;
        }

        private WorkerShiftInputDto PackageInputWorkerShift(int? shiftId, int workerid, DateTime shiftStart, DateTime shiftEnd)
        {
            return new WorkerShiftInputDto { Id = shiftId ?? 0, WorkerId = workerid, ShiftStart = shiftStart.ToString("yyyy-MM-dd HH:mm"), ShiftEnd = shiftEnd.ToString("yyyy-MM-dd HH:mm") };
        }

        public async Task<List<WorkerShiftOutputDto>?> ListWorkerShiftsAsync(bool displayStats)
        {
            List<WorkerShiftOutputDto>? workershifts = null;
            var response = await _shiftService.GetWorkersShiftAsync();
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    workershifts = response.Data as List<WorkerShiftOutputDto>;
                    if (workershifts != null)
                    {
                        DisplayWorkerShifts(FormatShiftDates(workershifts), displayStats);
                    }
                    break;
                case ResponseStatus.Failure:
                    AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
                    break;
            }
            // Optional returned List of workershifts if you want to do something else with them
            return workershifts;
        }

        private void DisplayWorkerShifts(List<WorkerShiftOutputDto>? workershifts, bool displaystats)
        {
            if (workershifts == null || !workershifts.Any()) return;
            Table workershiftsTable = new Table();
            workershiftsTable.AddColumn(new TableColumn("[white]ID[/]").Centered());
            workershiftsTable.AddColumn(new TableColumn("[white]Worker[/]").LeftAligned());
            workershiftsTable.AddColumn(new TableColumn("[white]Shift Start[/]").LeftAligned());
            workershiftsTable.AddColumn(new TableColumn("[white]Shift End[/]").LeftAligned());
            workershiftsTable.AddColumn(new TableColumn("[white]Duration[/]").LeftAligned());
            workershiftsTable.Alignment(Justify.Center);
            foreach (var workershift in workershifts)
            {
                // Ensure returned information view handled possible errors in display.
                var displayId = workershift.DisplayId.HasValue ? workershift.DisplayId.Value.ToString() : "N/A";
                var workerName =  (workershift.Worker != null && workershift.Worker.FirstName != null && workershift.Worker.LastName != null) ? $"{workershift.Worker.LastName.ToUpper() ?? "Unknown"}, {workershift.Worker.FirstName.ToUpper() ?? "Unknown"}" : "Unknown";
                var shiftStart = workershift.DisplayShiftStart ?? "Unknown";
                var shiftEnd = workershift.DisplayShiftEnd ?? "Unknown";
                var shiftDuration = workershift.DisplayDuration ?? "Unknown";
                workershiftsTable.AddRow(
                    displayId,
                    workerName,
                    shiftStart,
                    shiftEnd,
                    shiftDuration
                );
            }
            if (displaystats)
            {
                workershiftsTable.AddRow("");
                string? totalDuration = _shiftService.CalculateTotalShiftsDuration(workershifts);
                string? averageDuration = _shiftService.CalculateAverageShiftsDuration(workershifts);
                if (totalDuration != null)
                    workershiftsTable.AddRow("", "", "", "[lightsteelblue3]Total Duration[/]", $"[paleturquoise1]{totalDuration}[/]");
                if (averageDuration != null)
                    workershiftsTable.AddRow("", "", "", "[lightsteelblue3]Average Duration[/]", $"[paleturquoise1]{averageDuration}[/]");
            }
            AnsiConsole.Write(new Text("ALL WORKER SHIFTS").Centered());
            AnsiConsole.Write(workershiftsTable);
        }

        private List<WorkerShiftOutputDto>? FormatShiftDates(List<WorkerShiftOutputDto>? workershifts)
        {
            // Format string shift dates to application's preferred format.
            if (workershifts == null) return null;
            foreach (var workershift in workershifts)
            {
                workershift.DisplayShiftStart = workershift.ShiftStart.ToString(_shiftService.DateTimeApplicationFormat);
                workershift.DisplayShiftEnd = workershift.ShiftEnd.ToString(_shiftService.DateTimeApplicationFormat);
            }
            return workershifts;
        }

        private async Task DeleteExistingWorkerShiftAsync()
        {
            AnsiConsole.MarkupLine("\n[white]DELETE A WORKER SHIFT[/]\n");
            List<WorkerShiftOutputDto>? workershifts = await ListWorkerShiftsAsync(false);
            if (workershifts != null && workershifts.Any())
            {
                int workershiftid;
                do
                {
                    workershiftid = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which shift do you wish to delete? (1 - {workershifts.Count})[/]: ", 0, workershifts.Count);
                } while (workershiftid > workershifts.Count);
                if (workershiftid == 0)
                {
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                }
                else
                {
                    WorkerShiftOutputDto shiftToDelete = workershifts[workershiftid - 1];
                    int pkid = shiftToDelete.Id;
                    DisplayWorkerShift(shiftToDelete);
                    if (AnsiConsole.Confirm($"[yellow]WARNING: This action is permanent.\nAre you sure you want to delete this shift?[/]"))
                    {
                        AnsiConsole.MarkupLine("[white]Deleting worker...[/]");
                        var response = await _shiftService.DeleteWorkerShiftAsync(shiftToDelete.Id);
                        switch (response.Status)
                        {
                            case ResponseStatus.Success:
                                AnsiConsole.MarkupLine("[lime]Shift successfully deleted[/]");
                                break;
                            case ResponseStatus.Failure:
                                AnsiConsole.MarkupLine("\n[red]Shift deletion failed[/]");
                                AnsiConsole.MarkupLine($"[red]ERROR: '{response.Message}'[/]\n");
                                break;
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                    }
                }
            }
            CommonUI.Pause("grey53");
        }

        private async Task UpdateWorkerShiftAsync()
        {
            AnsiConsole.MarkupLine("\n[white]UPDATE A WORKER SHIFT[/]\n");
            List<WorkerShiftOutputDto>? workershifts = await ListWorkerShiftsAsync(false);
            if (workershifts != null && workershifts.Any())
            {
                int workershiftId;
                do
                {
                    workershiftId = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which worker shift do you wish to update? (1 - {workershifts.Count})[/]: ", 0, workershifts.Count);
                } while (workershiftId > workershifts.Count);
                if (workershiftId == 0)
                {
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[white]Please update the Worker for this shift\n[/]");
                    WorkerShiftOutputDto shiftToUpdate = workershifts[workershiftId - 1];
                    int pkid = shiftToUpdate.Id;
                    WorkerShiftDetails? updatedShiftDetails = await GetWorkerShiftDetails(pkid, shiftToUpdate.ShiftStart, shiftToUpdate.ShiftEnd);
                    if (updatedShiftDetails != null)
                    {
                        WorkerShiftInputDto updatedWorkerInputDto = PackageInputWorkerShift(pkid, updatedShiftDetails.WorkerId, updatedShiftDetails.ShiftStart, updatedShiftDetails.ShiftEnd);
                        DisplayWorkerShiftDetails($"[yellow]{shiftToUpdate.DisplayShiftStart}[/] [lime]->[/] [white]{updatedShiftDetails.ShiftStart.ToString(_shiftService.DateTimeApplicationFormat)}[/]", 
                                                    $"[yellow]{shiftToUpdate.DisplayShiftEnd}[/] [lime]->[/] [white]{updatedShiftDetails.ShiftEnd.ToString(_shiftService.DateTimeApplicationFormat)}[/]",
                                                    $"[yellow]{shiftToUpdate.Worker?.FirstName.ToUpper() ?? "UNKNOWN"}[/] [lime]->[/] [white]{updatedShiftDetails.DisplayFirstName.ToUpper()}[/]", 
                                                    $"[yellow]{shiftToUpdate.Worker?.LastName.ToUpper() ?? "UNKNOWN"}[/] [lime]->[/] [white]{updatedShiftDetails.DisplayLastName.ToUpper()}[/]");
                        if (AnsiConsole.Confirm($"[yellow]Are you sure you want to update this shift's details?[/]"))
                        {
                            AnsiConsole.MarkupLine("[white]Updating shift...[/]");
                            var response = await _shiftService.UpdateWorkerShiftAsync(pkid, updatedWorkerInputDto);
                            switch (response.Status)
                            {
                                case ResponseStatus.Success:
                                    AnsiConsole.MarkupLine("[lime]Shift successfully updated[/]");
                                    break;
                                case ResponseStatus.Failure:
                                    AnsiConsole.MarkupLine("\n[red]Shift update failed[/]");
                                    AnsiConsole.MarkupLine($"[red]ERROR: '{response.Message}'[/]\n");
                                    break;
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                    }
                }
            }
            CommonUI.Pause("grey53");
        }
    }
}
