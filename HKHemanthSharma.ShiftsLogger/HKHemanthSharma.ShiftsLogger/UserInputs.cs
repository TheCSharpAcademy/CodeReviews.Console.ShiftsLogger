using HKHemanthSharma.ShiftsLogger.Model;
using HKHemanthSharma.ShiftsLogger.Repository;
using Spectre.Console;

namespace HKHemanthSharma.ShiftsLogger
{
    public class UserInputs
    {
        private readonly IWorkerRepository Workerrepository;
        private readonly IShiftRepository Shiftrepository;

        public UserInputs(IWorkerRepository repo, IShiftRepository Shiftrepo)
        {
            Workerrepository = repo;
            Shiftrepository = Shiftrepo;
        }

        public int InputId()
        {
            return AnsiConsole.Ask<int>("[paleturquoise1]Please enter the Id[/]");
        }

        public async Task<Shift> GetNewShift()
        {
            string startTime = AnsiConsole.Ask<string>("Enter the StartTime of shift in 'HH:mm format'");
            while (!validations.validateTime(startTime))
            {
                startTime = AnsiConsole.Ask<string>("Wrong format!!! Enter the StartTime of shift in 'HH:mm format'");
            }

            string endTime = AnsiConsole.Ask<string>("Enter the EndTime of shift in 'HH:mm format'");
            while (!validations.validateTime(endTime))
            {
                endTime = AnsiConsole.Ask<string>("Wrong format!!! Enter the EndTime of shift in 'HH:mm format'");
            }

            string ShiftDate = AnsiConsole.Ask<string>("Enter the Date of shift in 'dd-MM-yyyy' format or leave it empty for today's Date:");
            if (!string.IsNullOrEmpty(ShiftDate))
            {
                while (!validations.validateDate(ShiftDate))
                {
                    ShiftDate = AnsiConsole.Ask<string>("Wrong input!!! Enter the Date of shift in 'dd-MM-yyyy' format or leave it empty for today's Date:");
                }
            }

            var workersResponse = await Workerrepository.GetAllWorker();
            List<Worker> workers = workersResponse.Data.ToList();
            List<string> workerNames = workers.Select(x => x.Name).ToList();

            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the Worker")
                    .AddChoices(workerNames));

            var chosenWorkerId = workers.First(x => x.Name == userChoice).WorkerId;

            return new Shift
            {
                WorkerId = chosenWorkerId,
                ShiftStartTime = startTime,
                ShiftEndTime = endTime,
                ShiftDate = ShiftDate
            };
        }
        public async Task<Shift> SelectShift()
        {
            var ShiftResponse = await Shiftrepository.GetAllShifts();
            List<Shift> Shifts = (List<Shift>)ShiftResponse.Data;
            List<string> ShiftNames = Shifts.Select(x => $"ShiftId-{x.ShiftId} : WorkerId-{x.WorkerId} :ShiftDate {DateTime.Parse(x.ShiftDate.ToString()).ToString("dd-MM-yyyy")}: StartTime {DateTime.Parse(x.ShiftStartTime.ToString()).ToString("HH:mm")}: EndTime {DateTime.Parse(x.ShiftEndTime.ToString()).ToString("HH:mm")}").ToList();
            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the Worker")
                    .AddChoices(ShiftNames));
            int SelectedShiftId = int.Parse(userChoice.Split(":")[0].Split("-")[1]);
            return Shifts.Where(x => x.ShiftId == SelectedShiftId).FirstOrDefault();
        }
        public async Task<int> SelectDeleteShift()
        {
            var ShiftResponse = await Shiftrepository.GetAllShifts();
            List<Shift> Shifts = (List<Shift>)ShiftResponse.Data;
            List<string> ShiftNames = Shifts.Select(x => $"ShiftId-{x.ShiftId} : WorkerId-{x.WorkerId} :ShiftDate {DateTime.Parse(x.ShiftDate.ToString()).ToString("dd-MM-yyyy")}: StartTime {DateTime.Parse(x.ShiftStartTime.ToString()).ToString("HH:mm")}: EndTime {DateTime.Parse(x.ShiftEndTime.ToString()).ToString("HH:mm")}").ToList();
            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the Worker")
                    .AddChoices(ShiftNames));
            int SelectedShiftId = int.Parse(userChoice.Split(":")[0].Split("-")[1]);
            return SelectedShiftId;
        }
        public async Task<Shift> GetUpdateShift(Shift updatedShift)
        {
            UserInterface.ShowShift(updatedShift);
            var confirm = AnsiConsole.Prompt(new ConfirmationPrompt("Do you want to Change WorkerId"));
            if (confirm)
            {
                var workersResponse = await Workerrepository.GetAllWorker();
                List<Worker> workers = workersResponse.Data.ToList();
                List<string> workerNames = workers.Select(x => x.Name).ToList();

                var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Please select the Worker")
                        .AddChoices(workerNames));

                var chosenWorkerId = workers.First(x => x.Name == userChoice).WorkerId;
            }
            confirm = AnsiConsole.Prompt(new ConfirmationPrompt("Do you want to Change StartTime of Shift?"));
            if (confirm)
            {
                string startTime = AnsiConsole.Ask<string>("Enter the StartTime of shift in 'HH:mm format'");
                while (!validations.validateTime(startTime))
                {
                    startTime = AnsiConsole.Ask<string>("Wrong format!!! Enter the StartTime of shift in 'HH:mm format'");
                }
                updatedShift.ShiftStartTime = startTime;
            }
            updatedShift.ShiftStartTime = DateTime.Parse(updatedShift.ShiftStartTime).ToString("HH:mm");
            confirm = AnsiConsole.Prompt(new ConfirmationPrompt("Do you want to Change EndTime of Shift?"));
            if (confirm)
            {
                string EndTime = AnsiConsole.Ask<string>("Enter the EndTime of shift in 'HH:mm format'");
                while (!validations.validateTime(EndTime))
                {
                    EndTime = AnsiConsole.Ask<string>("Wrong format!!! Enter the EndTime of shift in 'HH:mm format'");
                }
                updatedShift.ShiftEndTime = EndTime;
            }
            updatedShift.ShiftEndTime = DateTime.Parse(updatedShift.ShiftEndTime).ToString("HH:mm");
            confirm = AnsiConsole.Prompt(new ConfirmationPrompt("Do you want to Change Date of Shift?"));
            if (confirm)
            {
                 AnsiConsole.MarkupLine("Enter the Date of shift in 'dd-MM-yyyy' format or leave it empty for today's Date:");
                string ShiftDate = Console.ReadLine();
                if (!string.IsNullOrEmpty(ShiftDate))
                {
                    while (!validations.validateDate(ShiftDate))
                    {
                        ShiftDate = AnsiConsole.Ask<string>("Wrong input!!! Enter the Date of shift in 'dd-MM-yyyy' format or leave it empty for today's Date:");
                    }
                }
                else
                {
                    ShiftDate = DateTime.Now.ToString("dd-MM-yyyy");
                }
                updatedShift.ShiftDate = ShiftDate;
            }
            updatedShift.ShiftDate = DateTime.Parse(updatedShift.ShiftDate).ToString("dd-MM-yyyy");
            return updatedShift;
        }

        public async Task<string> GetNewName()
        {
            AnsiConsole.MarkupLine("[lightslateblue]What is the Name of the Worker[/]");
            string Name = Console.ReadLine();
            return Name;
        }
        public async Task<Worker> GetSelectWorker()
        {
            ResponseDto<List<Worker>> workers = await Workerrepository.GetAllWorker();
            List<Worker> WorkerList = workers.Data;
            List<string> WorkerNamesWithId = WorkerList.Select(x => $"Name-{x.Name}:WorkerId-{x.WorkerId}").ToList();
            var userChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<string>()
                       .Title("Please select the Worker")
                       .AddChoices(WorkerNamesWithId));
            int SelectedId = int.Parse(userChoice.Split(":")[1].Split("-")[1]);
            return WorkerList.FirstOrDefault(x => x.WorkerId == SelectedId);
        }
    }
}
