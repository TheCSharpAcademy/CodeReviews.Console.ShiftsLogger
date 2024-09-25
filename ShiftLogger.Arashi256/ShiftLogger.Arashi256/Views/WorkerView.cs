using ShiftLogger_Frontend.Arashi256.Classes;
using ShiftLogger_Frontend.Arashi256.Services;
using ShiftLogger_Frontend.Arashi256.Models;
using ShiftLogger_Shared.Arashi256.Classes;
using ShiftLogger_Shared.Arashi256.Models;
using Spectre.Console;

namespace ShiftLogger_Frontend.Arashi256.Views
{
    internal class WorkerView
    {
        private const int RETURN_MAINVIEW_OPTION_NUM = 5;
        private Table _tblWorkerMenu;
        private ShiftService _shiftService;
        private string[] _menuOptions =
        {
            "Add a new worker",
            "Update an existing worker",
            "Delete an existing worker",
            "List all workers",
            "Return to main menu"
        };

        public WorkerView(ShiftService ss)
        {
            _shiftService = ss;
            try
            {
                if (ss == null) _shiftService = new ShiftService();
            }
            catch (ArgumentException ae)
            {
                throw new ArgumentException(ae.Message);
            }
            _tblWorkerMenu = new Table();
            _tblWorkerMenu.AddColumn(new TableColumn("[steelblue]CHOICE[/]").Centered());
            _tblWorkerMenu.AddColumn(new TableColumn("[steelblue]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblWorkerMenu.AddRow($"[white]{i + 1}[/]", $"[aqua]{_menuOptions[i]}[/]");
            }
            _tblWorkerMenu.Alignment(Justify.Center);
        }

        public void DisplayViewMenu()
        {
            int selectedValue = 0;
            do
            {
                AnsiConsole.Write(new Text("\nWORKERS").Centered());
                AnsiConsole.Write(_tblWorkerMenu);
                selectedValue = CommonUI.MenuOption($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessWorkerMenu(selectedValue);
            } while (selectedValue != RETURN_MAINVIEW_OPTION_NUM);
        }

        public async Task<List<WorkerOutputDto>?> ListWorkersAsync()
        {
            List<WorkerOutputDto>? workers = null;
            var response = await _shiftService.GetWorkersAsync();
            switch (response.Status)
            {
                case ResponseStatus.Success:
                    workers = response.Data as List<WorkerOutputDto>;
                    if (workers != null)
                    {
                        DisplayWorkers(workers);
                    }
                    break;
                case ResponseStatus.Failure:
                    AnsiConsole.MarkupLine($"[red]{response.Message}[/]");
                    break;
            }
            // Optional returned List of workers if you want to do something else with them
            return workers;
        }

        public void DisplayWorkers(List<WorkerOutputDto>? workers)
        {
            if (workers != null && workers.Any())
            {
                Table workersTable = new Table();
                workersTable.AddColumn(new TableColumn("[white]ID[/]").Centered());
                workersTable.AddColumn(new TableColumn("[white]FIRST NAME[/]").LeftAligned());
                workersTable.AddColumn(new TableColumn("[white]LAST NAME[/]").LeftAligned());
                workersTable.AddColumn(new TableColumn("[white]EMAIL[/]").LeftAligned());
                workersTable.Alignment(Justify.Center);
                foreach (var worker in workers)
                {
                    // Ensure returned information view handled possible errors in display.
                    var displayId = worker.DisplayId.HasValue ? worker.DisplayId.Value.ToString() : "N/A";
                    var firstName = worker.FirstName ?? "UNKNOWN";
                    var lastName = worker.LastName ?? "UNKNOWN";
                    var email = worker.Email ?? "UNKNOWN";
                    workersTable.AddRow(
                        displayId,
                        firstName,
                        lastName,
                        email
                    );
                }
                AnsiConsole.Write(new Text("ALL WORKERS").Centered());
                AnsiConsole.Write(workersTable);
            }
            else
            {
                // No workers to display
                AnsiConsole.MarkupLine("\n[red]There are no workers to display[/]\n");
            }
        }

        public void DisplayWorker(WorkerOutputDto worker)
        {
            Table tblWorker = new Table();
            tblWorker.AddColumn(new TableColumn("[white]ID[/]").RightAligned());
            tblWorker.AddColumn(new TableColumn($"[white]{worker.DisplayId}[/]").LeftAligned());
            tblWorker.AddRow($"[cyan]First Name[/]", $"[white]{worker.FirstName.ToUpper()}[/]");
            tblWorker.AddRow($"[cyan]Last Name[/]", $"[white]{worker.LastName.ToUpper()}[/]");
            tblWorker.AddRow($"[cyan]Email[/]", $"[white]{worker.Email}[/]");
            AnsiConsole.Write(tblWorker);
        }

        private void ProcessWorkerMenu(int option)
        {
            AnsiConsole.Markup($"[lightslategrey]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    // Add new worker.
                    AddNewWorkerAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 2:
                    // Update an existing worker.
                    UpdateWorkerAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 3:
                    // Delete existing worker.
                    DeleteExistingWorkerAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    break;
                case 4:
                    // List workers.
                    ListWorkersAsync().GetAwaiter().GetResult(); // Blocks until async method completes.
                    CommonUI.Pause("grey53");
                    break;
            }
        }

        private async Task AddNewWorkerAsync()
        {
            AnsiConsole.MarkupLine("\n[white]ADD A NEW WORKER[/]\n");
            WorkerDetails? workerDetails = GetWorkerDetails(null, null, null);
            if (workerDetails != null)
            {
                var newWorker = PackageInputWorker(0, workerDetails.FirstName, workerDetails.LastName, workerDetails.Email);
                DisplayWorkerDetails($"[white]{newWorker.FirstName}[/]", $"[white]{newWorker.LastName}[/]", $"[white]{newWorker.Email}[/]");
                if (AnsiConsole.Confirm($"[yellow]Are you sure you want to add this new worker?[/]"))
                {
                    AnsiConsole.MarkupLine("[white]Adding new worker...[/]");
                    var response = await _shiftService.AddWorkerAsync(newWorker);
                    switch (response.Status)
                    {
                        case ResponseStatus.Success:
                            AnsiConsole.MarkupLine("[lime]Worker successfully added[/]");
                            var addedWorker = response.Data as WorkerOutputDto;
                            if (addedWorker != null)
                                DisplayWorker(addedWorker);
                            break;
                        case ResponseStatus.Failure:
                            AnsiConsole.MarkupLine("\n[red]Worker addition failed[/]");
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
            CommonUI.Pause("grey53");
        }

        private WorkerDetails? GetWorkerDetails(string? currentFirstName, string? currentLastName, string? currentEmail)
        {
            string? firstName, lastName, email;
            if (currentFirstName != null)
                firstName = CommonUI.GetStringWithPrompt($"Please enter the worker's new first name ({currentFirstName}): ", 100);
            else
                firstName = CommonUI.GetStringWithPrompt("Please enter the worker's first name: ", 100);
            if (firstName == null)
            {          
                return null;
            }
            if (currentLastName != null)
                lastName = CommonUI.GetStringWithPrompt($"Please enter the worker's new last name ({currentLastName}): ", 100);
            else
                lastName = CommonUI.GetStringWithPrompt("Please enter the worker's last name: ", 100);
            if (lastName == null)
            {
                return null;
            }
            do
            {
                if (currentEmail != null)
                    email = CommonUI.GetStringWithPrompt($"Please enter the worker's new email address ({currentEmail}): ", 100);
                else
                    email = CommonUI.GetStringWithPrompt("Please enter the worker's email address: ", 100);
                if (email == null)
                {
                    return null;
                }
            } while (!CommonUI.IsValidEmail(email));
            WorkerDetails? workerDetails = new WorkerDetails { FirstName = firstName, LastName = lastName, Email = email };
            return workerDetails;
        }

        private WorkerInputDto PackageInputWorker(int id, string fname, string lname, string email)
        {
            return new WorkerInputDto { Id = id, FirstName = fname, LastName = lname, Email = email };
        }

        private void DisplayWorkerDetails(string firstName, string lastName, string email)
        {
            Table tblWorker = new Table();
            tblWorker.AddColumn(new TableColumn("[cyan]ID[/]").RightAligned());
            tblWorker.AddColumn(new TableColumn($"[white]1[/]").LeftAligned());
            tblWorker.AddRow($"[cyan]First Name[/]", firstName.ToUpper());
            tblWorker.AddRow($"[cyan]Last Name[/]", lastName.ToUpper());
            tblWorker.AddRow($"[cyan]Email[/]", email);
            AnsiConsole.Write(tblWorker);
        }

        private async Task DeleteExistingWorkerAsync()
        {
            AnsiConsole.MarkupLine("\n[white]DELETE A WORKER[/]\n");
            List<WorkerOutputDto>? workers = await ListWorkersAsync();
            if (workers != null && workers.Any())
            {
                int workerid;
                do
                {
                    workerid = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which worker do you wish to delete? (1 - {workers.Count})[/]: ", 0, workers.Count);
                } while (workerid > workers.Count);
                if (workerid == 0)
                {
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                }
                else
                {
                    WorkerOutputDto workerToDelete = workers[workerid - 1];
                    int pkid = workerToDelete.Id;
                    DisplayWorker(workerToDelete);
                    if (AnsiConsole.Confirm($"[yellow]WARNING: Deleting this worker will also delete any associated worker shifts. This action is permanent.\nAre you sure you want to delete this worker?[/]"))
                    {
                        AnsiConsole.MarkupLine("[white]Deleting worker...[/]");
                        var response = await _shiftService.DeleteWorkerAsync(workerToDelete.Id);
                        switch (response.Status)
                        {
                            case ResponseStatus.Success:
                                AnsiConsole.MarkupLine("[lime]Worker successfully deleted[/]");
                                break;
                            case ResponseStatus.Failure:
                                AnsiConsole.MarkupLine("\n[red]Worker deletion failed[/]");
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

        private async Task UpdateWorkerAsync()
        {
            AnsiConsole.MarkupLine("\n[white]UPDATE A WORKER[/]\n");
            List<WorkerOutputDto>? workers = await ListWorkersAsync();
            if (workers != null && workers.Any())
            {
                int workerid;
                do
                {
                    workerid = CommonUI.MenuOption($"[grey53]Enter '0' to cancel[/]\n[white]Which worker do you wish to update? (1 - {workers.Count})[/]: ", 0, workers.Count);
                } while (workerid > workers.Count);
                if (workerid == 0)
                {
                    AnsiConsole.MarkupLine("[yellow]Operation cancelled[/]");
                }
                else
                {
                    WorkerOutputDto workerToUpdate = workers[workerid - 1];
                    int pkid = workerToUpdate.Id;
                    WorkerDetails? updatedWorkerDetails = GetWorkerDetails(workerToUpdate.FirstName, workerToUpdate.LastName, workerToUpdate.Email);
                    if (updatedWorkerDetails != null)
                    {
                        WorkerInputDto updatedWorkerInputDto = PackageInputWorker(pkid, updatedWorkerDetails.FirstName, updatedWorkerDetails.LastName, updatedWorkerDetails.Email);
                        DisplayWorkerDetails($"[yellow]{workerToUpdate.FirstName}[/] [lime]->[/] [white]{updatedWorkerInputDto.FirstName}[/]", $"[yellow]{workerToUpdate.LastName}[/] [lime]->[/] [white]{updatedWorkerInputDto.LastName}[/]", $"[yellow]{workerToUpdate.Email}[/] [lime]->[/] [white]{updatedWorkerInputDto.Email}[/]");
                        if (AnsiConsole.Confirm($"[yellow]Are you sure you want to update this worker's details?[/]"))
                        {
                            AnsiConsole.MarkupLine("[white]Updating worker...[/]");
                            var response = await _shiftService.UpdateWorkerAsync(pkid, updatedWorkerInputDto);
                            switch (response.Status)
                            {
                                case ResponseStatus.Success:
                                    AnsiConsole.MarkupLine("[lime]Worker successfully updated[/]");
                                    break;
                                case ResponseStatus.Failure:
                                    AnsiConsole.MarkupLine("\n[red]Worker update failed[/]");
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