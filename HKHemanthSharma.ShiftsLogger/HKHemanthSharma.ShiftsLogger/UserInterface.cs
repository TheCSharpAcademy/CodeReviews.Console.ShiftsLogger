using HKHemanthSharma.ShiftsLogger.Model;
using HKHemanthSharma.ShiftsLogger.Services;
using Spectre.Console;
using System.Collections;
using System.Reflection;

namespace HKHemanthSharma.ShiftsLogger
{
    public class UserInterface
    {
        private readonly IShiftService Shiftservice;
        private readonly IWorkerService Workerservice;
        public UserInterface(IShiftService _service, IWorkerService _WorkerService)
        {
            Shiftservice = _service;
            Workerservice = _WorkerService;
        }
        public void MainMenu()
        {
            bool IsAppRunning = true;
            while (IsAppRunning)
            {
                Console.Clear();
                var userOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Please select an option")
                    .AddChoices(["Manage Shifts", "Manage Workers", "Exit"])
                    );

                switch (userOption)
                {
                    case "Manage Shifts":
                        ShiftServiceMenu();
                        break;
                    case "Manage Workers":
                        WorkerServiceMenu();
                        break;
                    case "Exit":
                        IsAppRunning = false;
                        break;
                }
            }
        }

        private void WorkerServiceMenu()
        {
            var userOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select an option")
                .AddChoices(["ViewAllWorkers", "View a single Worker", "Delete a Worker", "Create a new Worker", "Update a Worker", "Exit"])
                );

            switch (userOption)
            {
                case "ViewAllWorkers":
                    Workerservice.GetAllWorkers();
                    break;
                case "View a single Worker":
                    Workerservice.GetSingleWorker();
                    break;
                case "Delete a Worker":
                    Workerservice.DeleteWorker();
                    break;
                case "Create a new Worker":
                    Workerservice.CreateWorker();
                    break;
                case "Update a Worker":
                    Workerservice.UpdateWorker();
                    break;
                case "Exit":
                    break;
            }
        }

        public void ShiftServiceMenu()
        {
            var userOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select an option")
                .AddChoices(["ViewAllShifts", "View a single Shift", "Delete a shift", "Create a new Shift", "Update a Shift", "Exit"])
                );

            switch (userOption)
            {
                case "ViewAllShifts":
                    Shiftservice.GetAllShifts();
                    break;
                case "View a single Shift":
                    Shiftservice.GetSingleShift();
                    break;
                case "Delete a shift":
                    Shiftservice.DeleteShift();
                    break;
                case "Create a new Shift":
                    Shiftservice.CreateShift();
                    break;
                case "Update a Shift":
                    Shiftservice.UpdateShift();
                    break;
                case "Exit":
                    break;
            }
        }
        public static void ShowResponse<T>(ResponseDto<T> response)
        {
            if (response.IsSuccess)
            {
                Panel Messagepanel = new Panel($"[aqua]{Markup.Escape(response.Message)}[/]");
                Messagepanel.Header = new PanelHeader("[green3_1] Response Message:[/]");
                Messagepanel.Border = BoxBorder.Double;
                Messagepanel.Padding = new Padding(2, 2, 2, 2);
                AnsiConsole.Write(Messagepanel);
                Messagepanel.Expand = true;
                if (response.Message == "Successfully Fetched The Data!!!" || response.Message == "Successfully Fetched Data!"
                    || response.Message == "Successfully Fetched Workers" || response.Message == "Successfully Fetched The Data!!!")
                {
                    ICollection ResponseObjects = (ICollection)response.Data;
                    if (ResponseObjects.Count == 0)
                    {
                        Panel EmptyMessagepanel = new Panel($"[darkseagreen1]No Data To Show!!![/]");
                        return;
                    }
                    Table ResponseTable = new();
                    PropertyInfo[] props = null;
                    Type ElementType = GetElementType(typeof(T));
                    if (ElementType == typeof(Shift))
                    {
                        props = typeof(Shift).GetProperties();
                    }
                    if (ElementType == typeof(Worker))
                    {
                        props = typeof(Worker).GetProperties().Take(2).ToArray();
                    }
                    var columnValues = new List<string>();
                    foreach (var prop in props)
                    {
                        columnValues.Add(prop.Name.ToString());
                    }
                    ResponseTable.AddColumns(columnValues.ToArray());
                    foreach (var obj in ResponseObjects)
                    {
                        var RowValues = new List<string>();
                        foreach (var prop in props)
                        {
                            if (prop.Name == "ShiftStartTime" || prop.Name == "ShiftEndTime" || prop.Name == "ShiftDate")
                            {
                                if (prop.Name == "ShiftDate")
                                {
                                    string date = DateTime.Parse(prop.GetValue(obj).ToString()).ToString("dd-MM-yyyy");
                                    RowValues.Add(Markup.Escape(date));
                                }
                                else
                                {
                                    string time = DateTime.Parse(prop.GetValue(obj).ToString()).ToString("HH:mm");
                                    RowValues.Add(Markup.Escape(time));
                                }
                            }
                            else
                            {
                                RowValues.Add(Markup.Escape(prop.GetValue(obj).ToString()));
                            }
                        }
                        ResponseTable.AddRow(RowValues.ToArray());
                    }
                    ResponseTable.Title = new TableTitle("[orange3] Here is the Retrieved Data[/]");
                    ResponseTable.Border(TableBorder.AsciiDoubleHead);
                    AnsiConsole.Write(ResponseTable);
                    if (ElementType == typeof(Worker))
                    {
                        AnsiConsole.MarkupLine("[mistyrose3]Details of shifts done by workers[/]\n Note: [lightcyan1]Details not shown for Workers that have not done any shifts yet![/]");
                        foreach (var obj in ResponseObjects)
                        {
                            Worker worker = (Worker)obj;
                            List<Shift> Shifts = worker.Shifts;
                            Table WorkerShiftTable = new();
                            WorkerShiftTable.Title = new TableTitle($"[yellow3_1] Here is the Shift Details of the {worker.Name}[/]");
                            var TableProps = typeof(Shift).GetProperties().ToList();
                            TableProps.ForEach(x => WorkerShiftTable.AddColumn(Markup.Escape(x.Name.ToString())));
                            foreach (var shift in Shifts)
                            {
                                WorkerShiftTable.AddRow(shift.ShiftId.ToString(), shift.WorkerId.ToString(), DateTime.Parse(shift.ShiftStartTime).ToString("HH:mm"), DateTime.Parse(shift.ShiftEndTime).ToString("HH:mm"),
                                    shift.ShiftDuration.ToString(), DateTime.Parse(shift.ShiftDate.ToString()).ToString("dd:MM:yyyy"));
                            }
                            WorkerShiftTable.Border = TableBorder.HeavyEdge;
                            if (Shifts.Count > 0)
                            {
                                AnsiConsole.Write(WorkerShiftTable);
                            }
                        }
                    }
                }

                Console.ReadLine();
            }
            else
            {
                Panel Messagepanel = new Panel($"Read this to understand more: [plum2] {response.Message}[/]");
                Messagepanel.Header = new PanelHeader("[red3_1] Failed to Retrieve Data[/]");
                Messagepanel.Border = BoxBorder.Double;
                Messagepanel.Padding = new Padding(2, 2, 2, 2);
                AnsiConsole.Write(Messagepanel);
                Console.ReadLine();
            }
        }
        public static Type GetElementType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return type.GetGenericArguments()[0];
            }

            // Case 2: T is an array (e.g., Shift[])
            if (type.IsArray)
            {
                return type.GetElementType();
            }

            // Case 3: T is already the element type (e.g., T = Shift)
            return type;
        }
        public static void ShowShift(Shift shift)
        {
            Panel Messagepanel = new Panel($"[aqua] Shift Id:{shift.ShiftId}\n" +
                $"Worker Id:{shift.WorkerId}\n" +
                $"ShiftStartTime:{shift.ShiftStartTime}\n" +
                $"ShiftEndTime:{shift.ShiftEndTime}\n" +
                $"ShiftDate:{shift.ShiftDate}\n" +
                $"ShiftDuration:{shift.ShiftDuration}[/]");
            Messagepanel.Header = new PanelHeader("[green3_1] Present Shift[/]");
            Messagepanel.Border = BoxBorder.Double;
            Messagepanel.Padding = new Padding(2, 2, 2, 2);
            AnsiConsole.Write(Messagepanel);
        }
    }
}
