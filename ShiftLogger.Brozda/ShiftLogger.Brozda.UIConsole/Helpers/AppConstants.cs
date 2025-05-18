using Microsoft.Extensions.ObjectPool;

namespace ShiftLogger.Brozda.UIConsole.Helpers
{
    /// <summary>
    /// Static class containing constant values across the app
    /// </summary>
    public static class AppConstants
    {
        public const int CancelledID = 0;


        //API HELPER
        public const string NotAvailable = "n/a";
        public const string BadRequest = "Bad request";
        public const string DeserializationFailure = "Unknown deserialization failure";
        public const string ResponseFormatException = "Response format exception";
        public const string ServerError = "Server error";
        public const string UnhandedServerError = "Unhandled Server error";

        //API Result
        public const string ResultNoRecord = "Your request did not match any record in the database";
        public const string ResultInvalidDataInRequest = "Invalid data passed";

        //INPUT
        public const string InputShiftName = "Please enter a shift type name: ";
        public const string InputWorkerName = "Enter worker name: ";

        public const string InputErrorName = "Name can contain only letters separated with space";

        public const string InputDescription = "Please enter description, you can leave it blank to not set it: ";
        public const string InputErrorDescription = "Name can contain only alphanumeric characters along with space, - and _";

        public const string InputDateFormat = "yyyy-mm-dd";
        public const string InputDate = $"Enter a date in format {InputDateFormat}: ";
        public const string InputInvalidDate = "Invalid date format";

        public const string InputRecordId = "Enter a ID of record you wish to select, alternatively you can enter 0 to return: ";
        public const string InputSelectRecordId = "Enter a ID of record you wish to select: ";
        public const string InputErrorRecordId = "Input must be valid Id or 0";

        public const string InputTimeHoursStart = "Enter starting hour: ";
        public const string InputTimeHoursEnd = "Enter ending hour: ";
        public const string InputTimeMinutesStart = "Enter starting minute: ";
        public const string InputTimeMinutesEnd = "Enter ending minute: ";
        public const string InputErrorTimeHour = "Hour value must be number between 0 and 23: ";
        public const string InputErrorTimeMinute = "Hour value must be number between 0 and 59: ";

        //OUTPUT
        public const string OutputUnsuportedData = "Unsuported data";
        public const string OutputPanelShiftTypes = "Shift type details";
        public const string OutputPanelWorker = "Worker details";
        public const string OutputPanelAssignedShifts = "Assigned shift(s) details"";

        public const string OutputPanelSelectedWorkerText = "Currently selected worker: ";
        public const string OutputPanelNoSelectedWorker = "No worker";

        public const string OutputDateFormat = "dd-MM-yyyy";
        public const string OutputNullValueSymbol = " - ";

        public const string OutputPressAnyKeyToContinue = "Press any key to continue...";

        //GENERAL
        public const string PlaceHolderText = "This text wasnt properly set by developer";

        public const string MenuInvalidOption = "Invalid menu option";
        public const string MenuUnknownOption = "Unknown option";

        public const string MenuNavigation = "Use arrow keys to select an option";
        

        public const string ActionSucessCreate = "Record created successfully";
        public const string ActionSuccessUpdate = "Record updated successfully";
        public const string ActionSuccessDelete = "Record deleted successfully";

        public const string ActionErrorCreate = "Error occured during create";
        public const string ActionErrorUpdate = "Error occured during update";
        public const string ActionErrorDelete = "Error occured during delete";

        public const string ActionErrorUnhandled = "Unexpected error";
        public const string ActionErrorGetAll = "Error occured during fetching records";
        public const string ActionErrorNotFound = "Record not found";

        
        

        //BASE MENU
        public const string BaseMenuReturnToMainMenu = "Return to main menu";
        public const string BaseMenuExitApp = "Exit the application";

        //MAIN MENU
        public const string MainMenuTitle = $"Welcome to shift logger app\n" +
                                            $"You can manage workers, assigned shifts and shifts that can be assigned in menu below";

        public const string MainMenuOptionWorkers = "Manage Workers";
        public const string MainMenuOptionAssignedShifts = "Manage Worker shifts";
        public const string MainMenuOptionShiftTypes = "Manage Shift types";

        //SHIFT TYPE MENU
        public const string ShiftTypesMenuTitle = "This sub menu allows you to manage shift types details";

        public const string ShiftTypesMenuOptionViewAll = "View all shift types";
        public const string ShiftTypesMenuOptionCreate = "Create new shift type";
        public const string ShiftTypesMenuOptionEdit = "Edit existing shift type";
        public const string ShiftTypesMenuOptionDelete = "Delete existing shift type";

        //WORKER MENU
        public const string WorkersMenuTitle = "This sub menu allows you to manage worker details";

        public const string WorkersMenuOptionViewAll = "View all workers";
        public const string WorkersMenuOptionViewById = "View specific worker details";
        public const string WorkersMenuOptionCreate = "Create new worker in the database";
        public const string WorkersMenuOptionEdit = "Update worker details";
        public const string WorkersMenuOptionDelete = "Delete worker from database";

        //ASSIGNED SHIFTS MENU
        public const string ShiftsWorkerNotSelected = "You need to select worker first";

        public const string ShiftsMenuTitle = "This sub menu allows you to manage worker shifts details";

        public const string ShiftsMenuOptionViewAll = "View all shifts for worker";
        public const string ShiftsMenuOptionViewByDate = "View all workers on shift for specific date";
        public const string ShiftsMenuOptionSelectWorker = "Select worker for operations";
        public const string ShiftsMenuOptionCreate = "Assign new shift";
        public const string ShiftsMenuOptionEdit = "Update existing assigned shift";
        public const string ShiftsMenuOptionDelete = "Unnasign shift for worker";
    }
}