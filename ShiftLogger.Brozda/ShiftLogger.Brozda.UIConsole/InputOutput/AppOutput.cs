using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using Spectre.Console;
using System.Text;

namespace ShiftLogger.Brozda.UIConsole.InputOutput
{
    /// <summary>
    /// Static class handing all output for the app
    /// </summary>
    ///
    internal static class AppOutput
    {
        /// <summary>
        /// Clears existing text from the console
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Prints menu panel to the user output
        /// </summary>
        /// <param name="panel">Panel representing short description of capabilities of current menu</param>
        public static void PrintMenuPanel(Panel panel)
        {
            AnsiConsole.Write(panel);
        }

        /// <summary>
        /// Prints data to the user screen
        /// Wrapper method which executes private methods based on data type of data passed as an argument
        /// </summary>
        /// <typeparam name="T">Data type of data </typeparam>
        /// <param name="data">Data to be printed to user output</param>
        public static void PrintData<T>(T data)
        {
            //shift types
            if (data is ShiftTypeDto shiftType)
            {
                PrintShiftTypes(new List<ShiftTypeDto> { shiftType });
            }
            else if (data is IEnumerable<ShiftTypeDto> shiftTypes)
            {
                PrintShiftTypes(shiftTypes.ToList());
            }
            //workers
            else if (data is WorkerDto worker)
            {
                PrintWorkers(new List<WorkerDto> { worker });
            }
            else if (data is IEnumerable<WorkerDto> workers)
            {
                PrintWorkers(workers.ToList());
            }
            //assigned shifts
            else if (data is AssignedShiftMappedDto shift)
            {
                PrintShifts(new List<AssignedShiftMappedDto> { shift });
            }
            else if (data is IEnumerable<AssignedShiftMappedDto> shifts)
            {
                PrintShifts(shifts.ToList());
            }
            //anything else
            else
            {
                PrintText(OutputConstants.OutputUnsuportedData);
            }
        }

        /// <summary>
        /// Prints out <see cref="ShiftTypeDto"/> to the user screen in form of table
        /// </summary>
        /// <param name="entities">List of <see cref="ShiftTypeDto"/> to be printed </param>
        private static void PrintShiftTypes(List<ShiftTypeDto> entities)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(OutputConstants.OutputPanelShiftTypes));
            var table = new Table();
            table.AddColumns(new string[] { "Id", "Name", "Description", "Start Time", "End Time" });

            foreach (var entity in entities)
            {
                table.AddRow(GetTableRow(entity));
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Prints out <see cref="WorkerDto"/> to the user screen in form of table
        /// </summary>
        /// <param name="entities">List of <see cref="WorkerDto"/> to be printed</param>
        private static void PrintWorkers(List<WorkerDto> entities)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(OutputConstants.OutputPanelWorker));

            var table = new Table();
            table.AddColumns(new string[] { "Id", "Name" });

            foreach (var entity in entities)
            {
                table.AddRow(GetTableRow(entity));
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Prints out single <see cref="WorkerDto"/> to the user screen in form of panel
        /// </summary>
        /// <param name="entities"><see cref="WorkerDto"/> to be printed</param
        public static void PrintSelectedWorker(WorkerDto? entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(OutputConstants.OutputPanelSelectedWorkerText);

            if (entity is null)
            {
                sb.Append(OutputConstants.OutputPanelNoSelectedWorker);
            }
            else
            {
                sb.Append($"Id: {entity.Id}, Name: {entity.Name}");
            }
            var panel = new Panel(sb.ToString());
            AnsiConsole.Write(panel);
        }

        /// <summary>
        /// Prints out <see cref="AssignedShiftMappedDto"/> to the user screen in form of table
        /// </summary>
        /// <param name="entities">List of <see cref="AssignedShiftMappedDto"/> to be printed</param>
        private static void PrintShifts(List<AssignedShiftMappedDto> entities)
        {
            Console.Clear();
            AnsiConsole.Write(new Panel(OutputConstants.OutputPanelAssignedShifts));
            var table = new Table();
            table.AddColumns(new string[] { "Id", "WorkerName", "ShiftTypeName", "Date", "Shift start", "Shift End" });

            foreach (var entity in entities)
            {
                table.AddRow(GetTableRow(entity));
            }
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Converts an <see cref="AssignedShiftMappedDto"/> object into a formatted string array
        /// </summary>
        /// <param name="dto"><see cref="AssignedShiftMappedDto"/> object from containing row values</param>
        /// <returns> <see cref="string"/> array containing values from passed object</returns>
        /// <remarks>
        /// String array contain these values: Id, WorkerName,ShiftTypeName,Date,StartTime,EndTime
        /// </remarks>
        private static string[] GetTableRow(AssignedShiftMappedDto dto)
        {
            return new string[]
            {
                dto.Id.ToString(),
                dto.WorkerName,
                dto.ShiftTypeName,
                dto.Date.ToString(OutputConstants.OutputDateFormat),
                dto.StartTime.ToString(),
                dto.EndTime.ToString(),
            };
        }

        /// <summary>
        /// Converts an <see cref="WorkerDto"/> object into a formatted string array
        /// </summary>
        /// <param name="dto"><see cref="WorkerDto"/> object from containing row values</param>
        /// <returns> <see cref="string"/> array containing values from passed object</returns>
        /// <remarks>
        /// String array contain these values: Id, Name
        /// </remarks>
        private static string[] GetTableRow(WorkerDto dto)
        {
            return new string[]
            {
                dto.Id.ToString(),
                dto.Name
            };
        }

        /// <summary>
        /// Converts an <see cref="ShiftTypeDto"/> object into a formatted string array
        /// </summary>
        /// <param name="dto"><see cref="ShiftTypeDto"/> object from containing row values</param>
        /// <returns> <see cref="string"/> array containing values from passed object</returns>
        /// <remarks>
        /// String array contain these values: Id, Name, Description, StartTime, EndTime
        /// </remarks>
        private static string[] GetTableRow(ShiftTypeDto dto)
        {
            return new string[]
            {
                dto.DisplayId.ToString(),
                dto.Name,
                dto.Description ?? OutputConstants.OutputNullValueSymbol,
                dto.StartTime.ToString(),
                dto.EndTime.ToString()
            };
        }

        /// <summary>
        /// Prints out "Press any key to continue" to the output and awaits user input
        /// </summary>
        public static void PrintPressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine(OutputConstants.OutputPressAnyKeyToContinue);
            Console.ReadKey();
        }

        /// <summary>
        /// Prints out an API error to the user screen
        /// </summary>
        /// <param name="prefix"><see cref="string"/> containing text to be shown before the error text</param>
        /// <param name="error"><see cref="string"/> error text to be shown</param>
        public static void PrintErrorApiResult(string prefix, string? error)
        {
            Console.WriteLine($"{prefix}: {error ?? SharedConstants.ActionErrorUnhandled}");
        }

        /// <summary>
        /// Prints out any text passed to the user output
        /// </summary>
        /// <param name="text">Text to be printed</param>
        public static void PrintText(string text)
        {
            Console.WriteLine(text);
        }
    }
}