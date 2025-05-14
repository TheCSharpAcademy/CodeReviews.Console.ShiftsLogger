using ShiftLogger.Brozda.Models;
using ShiftLogger.Brozda.UIConsole.Helpers;
using Spectre.Console;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ShiftLogger.Brozda.UIConsole.InputOutput
{
    /// <summary>
    /// Static class handing all user input for the app
    /// </summary>
    internal static class AppInput
    {
        public static readonly Regex nameRegex = new Regex(Resources.NameRegex);
        public static readonly Regex descriptionRegex = new Regex(Resources.DescriptionRegex);

        /// <summary>
        /// Displays an interactive console menu to the user and returns the selected option.
        /// </summary>
        /// <param name="menuOptions">A list of valid menu option keys (typically integers) to display.</param>
        /// <param name="menuItems">
        /// A dictionary mapping option keys to a tuple containing the label to display and the associated asynchronous action.
        /// The label is used for display in the menu, but the action is not executed by this method.
        /// </param>
        /// <returns>
        /// A <see cref="Task{Int32}"/> representing the asynchronous operation, with the selected menu option as its result.
        /// </returns>
        public static Task<int> ShowMenuAndGetInput(List<int> menuOptions, Dictionary<int, (string label, Func<Task> action)> menuItems)
        {
            var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<int>()
                    .Title(AppConstants.MENU_NAVIGATION)
                    .AddChoices(menuOptions)
                    .UseConverter(option => menuItems.ContainsKey(option) ? menuItems[option].label : AppConstants.MENU_UNKNOWN_OPTION)
                    );

            return Task.FromResult(userChoice);
        }

        /// <summary>
        /// Gets a ShiftTypeDto based on values from user input
        /// </summary>
        /// <param name="dto">Optional parameter specifying already existing ShiftType to be presented to user as default value</param>
        /// <returns><see cref="ShiftTypeDto"/> containing new values</returns>
        public static ShiftTypeDto GetShiftType(ShiftTypeDto? dto = null)
        {
            string name;
            string? description;
            TimeSpan startTime;
            TimeSpan endTime;

            if (dto is null)
            {
                name = GetName("Please enter a shift type name: ", nameRegex);
                description = GetDescription("Please enter description, you can leave it blank to not set it: ", descriptionRegex);
                startTime = GetTimeSpan(true);
                endTime = GetTimeSpan(false);
            }
            else
            {
                name = GetName("Please enter a shift type name: ", nameRegex, dto.Name);
                description = GetDescription("Please enter description, you can leave it blank to not set it: ", descriptionRegex, dto.Description);
                startTime = GetTimeSpan(true, dto.StartTime);
                endTime = GetTimeSpan(false, dto.EndTime);
            }

            return new ShiftTypeDto()
            {
                Name = name,
                Description = description,
                StartTime = startTime,
                EndTime = endTime,
            };
        }

        /// <summary>
        /// Gets a WorkerDto based on values from user input
        /// </summary>
        /// <param name="dto">Optional parameter specifying already existing WorkerDto to be presented to user as default value</param>
        /// <returns><see cref="WorkerDto"/> containing new values</returns>
        public static WorkerDto GetWorker(WorkerDto? toBeUpdated = null)
        {
            string name;

            if (toBeUpdated is null)
            {
                name = GetName("Enter new worker name: ", nameRegex);

                return new WorkerDto() { Name = name };
            }
            else
            {
                name = GetName("Enter new worker name: ", nameRegex, toBeUpdated.Name);
                toBeUpdated.Name = name;

                return toBeUpdated;
            }
        }

        /// <summary>
        /// Gets a DateTime based on values from user input
        /// </summary>
        /// <param name="currentDate">Optional parameter specifying already existing DateTime to be presented to user as default value</param>
        /// <returns><see cref="DateTime"/> from user input in specified format</returns>
        public static DateTime GetDate(DateTime? currentDate = null)
        {
            var textPrompt = new TextPrompt<string>("Enter a date in format yyyy-mm-dd")
                .Validate(x =>
                    DateTime.TryParseExact(x.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Invalid date format[/]"));

            if (currentDate is not null)
            {
                textPrompt.DefaultValue(currentDate.Value.ToString("yyyy-MM-dd"));
            }

            string date = AnsiConsole.Prompt(textPrompt);

            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get and integer representing Id from user
        /// </summary>
        /// <param name="ids">List of valid Ids from which can user select</param>
        /// <param name="existingId">Optional parameter specifying already existing id to be presented to user as default value</param>
        /// <param name="allowCancel"></param>
        /// <returns><see cref="int"/> value representing selected id</returns>
        public static int GetId(List<int> ids, int? existingId = null, bool allowCancel = true)
        {
            string prompt;
            string errorMsg;

            if (allowCancel)
            {
                ids.Add(AppConstants.CancelledID);
                prompt = "Enter a ID of record you wish to select, alternatively you can enter 0 to return: ";
                errorMsg = "Input must be valid Id or 0";
            }
            else
            {
                prompt = "Enter a ID of record you wish to select: ";
                errorMsg = "Input must be valid Id";
            }

            var textPrompt = new TextPrompt<int>(prompt)
                .Validate(x => ids.Contains(x))
                .ValidationErrorMessage(errorMsg);

            if (existingId is not null)
            {
                textPrompt.DefaultValue(existingId.Value);
            }

            int input = AnsiConsole.Prompt(textPrompt);

            return input;
        }

        /// <summary>
        /// Gets a name from user input
        /// Input is validated against regex in parameter
        /// </summary>
        /// <param name="prompt">Prompt which will be shown to the user</param>
        /// <param name="validator">Regex for validation of user input</param>
        /// <param name="defaultValue">Optional parameter specifying already existing name to be presented to user as default value</param>
        /// <returns><see cref="string"/> value representing the name</returns>
        private static string GetName(string prompt, Regex validator, string? defaultValue = null)
        {
            var textPrompt = new TextPrompt<string>(prompt)
                .Validate(x => validator.IsMatch(x))
                .ValidationErrorMessage("Name can contain only letters separated with space");

            if (defaultValue is not null)
            {
                textPrompt.DefaultValue(defaultValue);
            }

            string input = AnsiConsole.Prompt(textPrompt);

            return input;
        }

        /// <summary>
        /// Gets a description from user input
        /// Input is validated against regex in parameter
        /// </summary>
        /// <param name="prompt">Prompt which will be shown to the user</param>
        /// <param name="validator">Regex for validation of user input</param>
        /// <param name="defaultValue">Optional parameter specifying already existing description to be presented to user as default value</param>
        /// <returns>Nullable <see cref="string"/> value representing the description</returns>
        private static string? GetDescription(string prompt, Regex validator, string? defaultValue = null)
        {
            var textPrompt = new TextPrompt<string>(prompt)
                .AllowEmpty()
                .Validate(x => validator.IsMatch(x) || x == string.Empty)
                .ValidationErrorMessage("Name can contain only alphanumeric characters along with space, - and _");

            if (defaultValue is not null)
            {
                textPrompt.DefaultValue(defaultValue);
            }

            string input = AnsiConsole.Prompt(textPrompt);

            return input == string.Empty ? null : input;
        }

        /// <summary>
        /// Gets a TimeSpan from user input
        /// </summary>
        /// <param name="startTime"><see cref="bool"/> value determining whether the prompt refers to start or end time
        /// </param>
        /// <param name="defaultValue">Optional parameter specifying already existing value to be presented to user as default value</param>
        /// <returns><see cref="TimeSpan"/> based on user input</returns>
        private static TimeSpan GetTimeSpan(bool startTime, TimeSpan? defaultValue = null)
        {
            string startEnd = startTime ? "starting" : "ending";

            var hourPrompt = new TextPrompt<int>($"Enter {startEnd} hour: ")
                .Validate(x => x >= 0 && x <= 23)
                .ValidationErrorMessage("Hour value must be number between 0 and 23: ");

            var minutePrompt = new TextPrompt<int>($"Enter {startEnd} minute: ")
                .Validate(x => x >= 0 && x <= 59)
                .ValidationErrorMessage("Hour value must be number between 0 and 59: ");

            if (defaultValue is not null)
            {
                hourPrompt.DefaultValue(defaultValue.Value.Hours);
                minutePrompt.DefaultValue(defaultValue.Value.Minutes);
            }

            int hourInput = AnsiConsole.Prompt(hourPrompt);

            int minuteInput = AnsiConsole.Prompt(minutePrompt);

            return new TimeSpan(hourInput, minuteInput, 0);
        }
    }
}